using System;
using System.Buffers.Binary;
namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Iterator;
/// <summary>
///  Reads the Client Packet of each TCP flow in a capture.
/// </summary>
public sealed class ClientPacketCapture
{
    private const global::Omi.Pcap.TcpSide RequiredSide = global::Omi.Pcap.TcpSide.Client;
    private readonly global::Omi.Pcap.TcpReassembler reassembler = new();
    private readonly global::System.Collections.Generic.Dictionary<global::Omi.Pcap.TcpFlow, Flow?> flows = new();
    private readonly global::System.Collections.Generic.Dictionary<global::Omi.Pcap.TcpFlow, Held> heldFlows = new();
    private readonly global::Omi.Pcap.TcpSide capturedSide;
    private Flow? last;
    private int incompleteFlows;
    private int invalidFlows;
    private int unreadFlows;
    private int heldFramesRead;
    /// <summary>
    ///  Gets the flow of the last frame.
    /// </summary>
    public global::Omi.Pcap.TcpFlow? CurrentFlow { get; private set; }
    /// <summary>
    ///  Gets a value that indicates whether the last frame belongs to a flow that this capture reads.
    /// </summary>
    public bool FrameRead { get; private set; }
    private const long NothingConsumed = 0;
    /// <summary>
    ///  Gets the parser offset in the flow of the last frame, or -1 after invalid message bytes.
    /// </summary>
    public long Consumed => this.last?.Consumed ?? NothingConsumed;
    /// <summary>
    ///  Gets the flows with invalid message bytes.
    /// </summary>
    public int InvalidFlows
    {
        get
        {
            int count = this.invalidFlows;
            foreach (var flow in this.flows.Values) if (flow is { Invalid: true }) count++;
            return count;
        }
    }
    /// <summary>
    ///  Gets the flows excluded by the selected connection role, or whose connection role their bytes do not show.
    /// </summary>
    public int UnreadFlows => this.unreadFlows + this.heldFlows.Count;
    /// <summary>
    ///  Gets the number of frames that waited for the side of their flow and that this capture then read.
    /// </summary>
    public int HeldFramesRead => this.heldFramesRead;
    /// <summary>
    ///  Gets the flows with missing captured bytes or incomplete messages.
    /// </summary>
    public int IncompleteFlows
    {
        get
        {
            int count = this.incompleteFlows;
            foreach (var item in this.flows) if (item.Value is { Invalid: false } flow && (item.Key.Waiting || item.Key.Lost || flow.Incomplete)) count++;
            return count;
        }
    }
    /// <summary>
    ///  Gets the bytes retained for incomplete messages.
    /// </summary>
    public long PendingBytes
    {
        get
        {
            long count = 0;
            foreach (var flow in this.flows.Values) if (flow is not null) count += flow.PendingBytes;
            foreach (var held in this.heldFlows.Values) count += held.Bytes.WrittenCount;
            return count;
        }
    }
    /// <summary>
    ///  Selects the sender side for flows whose capture has no connection handshake.
    /// </summary>
    /// <remarks>
    ///  With Unknown, the sender side is the side from which exactly one packet parser opens an entry without failure.
    /// </remarks>
    /// <param name="capturedSide">The sender side of flows with no captured handshake, or Unknown to detect it from the bytes.</param>
    public ClientPacketCapture(global::Omi.Pcap.TcpSide capturedSide = global::Omi.Pcap.TcpSide.Unknown)
    {
        if (!global::System.Enum.IsDefined(capturedSide)) throw new global::System.ArgumentOutOfRangeException(nameof(capturedSide));
        this.capturedSide = capturedSide;
    }
    /// <summary>
    ///  Gives the newly available bytes of the flow of one captured TCP packet.
    /// </summary>
    /// <remarks>
    ///  The bytes stay valid until the next frame. A frame that waits for the side of its flow gives none. HeldFramesRead counts it once this capture reads it. FrameRead tells whether the frame belongs to a flow that this capture reads.
    /// </remarks>
    /// <returns>
    ///  <see langword="true"/> when the frame gives new, valid bytes of a flow that this capture reads; otherwise, <see langword="false"/>.
    /// </returns>
    public bool TryGetSegment(ReadOnlySpan<byte> frame, global::Omi.Pcap.LinkType linkType, out ReadOnlySpan<byte> bytes)
    {
        this.FrameRead = false;
        ReleaseEnded();
        var tcp = this.reassembler.Add(frame, linkType, out bytes);
        if (tcp is null) return false;
        if (!this.flows.TryGetValue(tcp, out var flow))
        {
            var side = tcp.Side == global::Omi.Pcap.TcpSide.Unknown ? this.capturedSide : tcp.Side;
            if (RequiredSide != global::Omi.Pcap.TcpSide.Unknown && side == global::Omi.Pcap.TcpSide.Unknown)
            {
                if (bytes.IsEmpty) return false;
                if (!this.heldFlows.TryGetValue(tcp, out var held)) this.heldFlows.Add(tcp, held = new());
                global::System.Buffers.BuffersExtensions.Write(held.Bytes, bytes);
                if (!FlowSide.Infer(held.Bytes.WrittenSpan, out side))
                {
                    held.Frames++;
                    return false;
                }
                this.heldFlows.Remove(tcp);
                bytes = held.Bytes.WrittenSpan;
                if (side == RequiredSide) this.heldFramesRead += held.Frames;
            }
            flow = RequiredSide == global::Omi.Pcap.TcpSide.Unknown || side == RequiredSide ? new Flow() : null;
            if (flow is null) this.unreadFlows++;
            this.flows.Add(tcp, flow);
        }
        this.CurrentFlow = tcp;
        if (flow is null) return false;
        this.last = flow;
        this.FrameRead = true;
        return !flow.Invalid && !bytes.IsEmpty;
    }
    /// <summary>
    ///  Reads the messages in the newly available bytes of one captured TCP packet.
    /// </summary>
    /// <remarks>
    ///  The packets borrow the bytes of the capture until its next frame. FrameRead tells whether the frame belongs to a flow that this capture reads.
    /// </remarks>
    /// <returns>
    ///  <see langword="true"/> when the capture read new bytes of a flow and the flow is valid; otherwise, <see langword="false"/>.
    /// </returns>
    public bool TryRead(ReadOnlySpan<byte> frame, global::Omi.Pcap.LinkType linkType, out global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Iterator.ClientPacketStream.Frames reading)
    {
        reading = default;
        if (!TryGetSegment(frame, linkType, out var bytes)) return false;
        var flow = this.last!;
        reading = flow.Stream.Receive(bytes);
        return !flow.Invalid;
    }
    /// <summary>
    ///  Gives bytes of the flow of the last frame to the stream of that flow.
    /// </summary>
    /// <param name="segment">
    ///  Bytes of the flow, in the order that the flow gives them.
    /// </param>
    /// <returns>
    ///  The packets of the whole frames that the bytes complete.
    /// </returns>
    /// <exception cref="global::System.InvalidOperationException">
    ///  No frame of a flow that this capture reads came before.
    /// </exception>
    public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Iterator.ClientPacketStream.Frames Receive(ReadOnlySpan<byte> segment) => (this.last ?? throw new global::System.InvalidOperationException("No frame of a flow that this capture reads came before.")).Stream.Receive(segment);
    internal static FlowSide.Outcome Attempt(ReadOnlySpan<byte> bytes)
    {
        var flow = new Flow();
        global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Iterator.ClientPacketStream.Frames reading;
        var recognized = false;
        try
        {
            reading = flow.Stream.Receive(bytes);
            foreach (var root in reading)
            {
                if (Recognizes(root))
                {
                    recognized = true;
                    break;
                }
            }
        }
        catch (global::System.ArgumentOutOfRangeException)
        {
            return FlowSide.Outcome.Failed;
        }
        return flow.Invalid ? FlowSide.Outcome.Failed : recognized ? FlowSide.Outcome.Recognized : FlowSide.Outcome.Waiting;
    }
    private static bool Recognizes(ClientPacket.Reader reader)
    {
        foreach (var entry in reader.ClientSoupBinTcpPacket)
        {
            if (Recognizes(entry)) return true;
        }
        return false;
    }
    private static bool Recognizes(ClientSoupBinTcpPacket.Reader reader)
    {
        if (Recognizes(reader.ClientPayload)) return true;
        return false;
    }
    private static bool Recognizes(ClientPayload.Reader reader)
    {
        if (reader.Case != global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.None) return true;
        return false;
    }
    private void ReleaseEnded()
    {
        foreach (var item in this.flows)
        {
            if (!item.Key.Ended) continue;
            if (item.Value is { Invalid: true }) this.invalidFlows++;
            else if (item.Value is { } flow && (item.Key.Lost || flow.Incomplete)) this.incompleteFlows++;
            this.flows.Remove(item.Key);
        }
        foreach (var item in this.heldFlows)
        {
            if (!item.Key.Ended) continue;
            this.unreadFlows++;
            this.heldFlows.Remove(item.Key);
        }
    }
    private sealed class Flow
    {
        internal readonly ClientPacketStream Stream = new();
        internal bool Invalid => this.Stream.Invalid;
        internal bool Incomplete => this.Stream.NeededBytes > 0;
        internal int PendingBytes => this.Stream.PendingBytes;
        internal long Consumed => this.Stream.Consumed;
    }
    private sealed class Held
    {
        internal readonly global::System.Buffers.ArrayBufferWriter<byte> Bytes = new();
        internal int Frames;
    }
}
