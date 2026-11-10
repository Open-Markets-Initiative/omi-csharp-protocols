using System.Diagnostics.CodeAnalysis;
using Omi.Pcap;

namespace Iex.IexEquities.Deep.IexTp.v1_08;

/// <summary>
///  Reassembles the TCP flows of a capture and gives the packets of each flow to the feed that reads it.
/// </summary>
/// <remarks>
///  A feed reads the flows that its side sends, as the TCP handshake shows. Some flows have no handshake, or can be read by a feed of either side. Such a flow goes to the one feed that decodes its first packets. If no feed or more than one feed decodes them, the flow is not read. A flow stops when the capture misses some of its bytes or a frame length is not valid. The other flows go on.
/// </remarks>
internal sealed class TcpFlowReader(IReadOnlyList<TcpFlowReader.Feed> feeds)
{
    private readonly TcpReassembler reassembler = new();

    private readonly Dictionary<TcpFlow, Flow> flows = new();

    private int stoppedFlows;

    /// <summary>
    ///  Gets the number of TCP flows not read because no feed, or more than one feed, decodes their first packets.
    /// </summary>
    public int UnreadFlows { get; private set; }

    /// <summary>
    ///  Gets the number of TCP flows that stopped before their end because the capture misses some of their bytes or a frame has a length that is not valid.
    /// </summary>
    public int StoppedFlows => this.stoppedFlows + this.flows.Count(entry => entry.Key.Waiting && entry.Value.Reads);

    /// <summary>
    ///  Adds a captured frame to its TCP flow.
    /// </summary>
    /// <returns><see langword="true"/> if the frame gives new bytes of its flow, in sequence order; otherwise, <see langword="false"/>.</returns>
    /// <remarks>
    ///  The bytes are valid until the next call.
    /// </remarks>
    public bool TryAdd(ReadOnlySpan<byte> frame, LinkType linkType, [NotNullWhen(true)] out TcpFlow? flow, out ReadOnlySpan<byte> data)
    {
        flow = this.reassembler.Add(frame, linkType, out data);
        if (flow is not null && !data.IsEmpty)
        {
            return true;
        }
        if (flow is { Ended: true })
        {
            End(flow);
        }
        else if (flow is { Waiting: true } && !this.flows.ContainsKey(flow))
        {
            this.flows.Add(flow, Start(flow));
        }
        return false;
    }

    /// <summary>
    ///  Gives the new bytes of a flow to the stream of the feed that reads the flow.
    /// </summary>
    /// <returns>The feed that reads the flow, or <see langword="null"/> if no feed reads it or the flow stopped.</returns>
    public Feed? Read(TcpFlow flow, ReadOnlySpan<byte> data, out IReadOnlyList<IPacket> packets)
    {
        if (!this.flows.TryGetValue(flow, out var state))
        {
            state = Start(flow);
            this.flows.Add(flow, state);
        }
        var owner = Choose(state, data, out packets);
        if (flow.Ended)
        {
            End(flow);
        }
        return owner;
    }

    /// <summary>
    ///  Gets the feeds that can read a flow.
    /// </summary>
    public IReadOnlyList<Feed> Candidates(TcpFlow flow) => this.flows.TryGetValue(flow, out var state) ? state.Candidates : Offered(flow);

    private Feed? Choose(Flow state, ReadOnlySpan<byte> data, out IReadOnlyList<IPacket> packets)
    {
        packets = [];
        if (state.Owner is { } owner)
        {
            packets = state.Stream!(data, out var failed);
            if (failed)
            {
                this.stoppedFlows++;
                state.Owner = null;
                state.Stream = null;
            }
            return owner;
        }
        if (state.Trials.Count == 0)
        {
            return null;
        }
        foreach (var trial in state.Trials)
        {
            trial.Add(data);
        }
        var decoded = state.Trials.Where(trial => trial.Decoded).ToList();
        if (decoded.Count == 0 && state.Trials.Any(trial => !trial.Rejected))
        {
            return null;
        }
        state.Trials.Clear();
        if (decoded.Count != 1)
        {
            this.UnreadFlows++;
            return null;
        }
        var chosen = decoded[0];
        packets = chosen.Packets;
        if (chosen.Failed)
        {
            this.stoppedFlows++;
            return chosen.Feed;
        }
        state.Owner = chosen.Feed;
        state.Stream = chosen.Stream;
        return state.Owner;
    }

    private void End(TcpFlow flow)
    {
        if (this.flows.Remove(flow, out var state) && flow.Lost && state.Reads)
        {
            this.stoppedFlows++;
        }
    }

    private Flow Start(TcpFlow flow)
    {
        var candidates = Offered(flow);
        return flow.Side != TcpSide.Unknown && candidates is [var sided] && sided.Side == flow.Side
            ? new Flow(candidates) { Owner = sided, Stream = sided.Open() }
            : new Flow(candidates) { Trials = [.. candidates.Select(feed => new Trial(feed))] };
    }

    private IReadOnlyList<Feed> Offered(TcpFlow flow)
    {
        var sided = feeds.Where(feed => flow.Side != TcpSide.Unknown && feed.Side == flow.Side).ToList();
        return sided.Count > 0
            ? sided
            : feeds.Where(feed => flow.Side == TcpSide.Unknown || feed.Side == TcpSide.Unknown).ToList();
    }

    /// <summary>
    ///  Appends received bytes to the packet stream of one flow and gives the packets that they complete.
    /// </summary>
    /// <remarks>
    ///  Sets failed to <see langword="true"/> when the bytes state a frame length that is not valid.
    /// </remarks>
    public delegate IReadOnlyList<IPacket> PacketStream(ReadOnlySpan<byte> data, out bool failed);

    /// <summary>
    ///  A feed that reads TCP flows.
    /// </summary>
    /// <param name="Name">The name of the feed.</param>
    /// <param name="Side">The side whose flows the feed reads, or Unknown if the feed reads either side.</param>
    /// <param name="Open">Creates the packet stream of one flow.</param>
    public sealed record Feed(string Name, TcpSide Side, Func<PacketStream> Open);

    private sealed class Flow(IReadOnlyList<Feed> candidates)
    {
        public IReadOnlyList<Feed> Candidates { get; } = candidates;
        public Feed? Owner { get; set; }
        public PacketStream? Stream { get; set; }
        public List<Trial> Trials { get; init; } = [];
        public bool Reads => this.Owner is not null || this.Trials.Count > 0;
    }

    private sealed class Trial(Feed feed)
    {
        public Feed Feed { get; } = feed;
        public PacketStream Stream { get; } = feed.Open();
        public List<IPacket> Packets { get; } = [];
        public bool Rejected { get; private set; }
        public bool Failed { get; private set; }
        public bool Decoded => !this.Rejected && this.Packets.Count > 0;

        public void Add(ReadOnlySpan<byte> data)
        {
            if (this.Rejected || this.Failed)
            {
                return;
            }
            try
            {
                var completed = this.Stream(data, out var failed);
                this.Packets.AddRange(completed);
                this.Failed = failed;
                this.Rejected = completed.Any(packet => !(packet.IsParsed && packet.IsValid)) || (failed && this.Packets.Count == 0);
            }
            catch (Exception)
            {
                this.Rejected = true;
            }
        }
    }
}
