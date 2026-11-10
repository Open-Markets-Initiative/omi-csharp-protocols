using System.Buffers.Binary;
using System.Net;

namespace Omi.Pcap;

/// <summary>
///  Reassembles the bytes of captured TCP packets in sequence order for each flow, where a flow is one direction of one connection.
/// </summary>
/// <remarks>
/// <para>
///  <see cref="Add"/> gives the bytes of each <see cref="TcpFlow"/> in sequence order, and gives each byte once.
/// </para>
/// <para>
///  One flow keeps at most 1 MiB (1,048,576 bytes) of payload that arrives after a gap. When a segment makes the flow
///  keep more, the capture is taken to miss the bytes of the gap: that call gives no bytes,
///  <see cref="TcpFlow.Ended"/> and <see cref="TcpFlow.Lost"/> become <see langword="true"/>, and the kept bytes are
///  discarded. Later segments on the same addresses and ports are ignored until a SYN starts a new <see cref="TcpFlow"/>.
/// </para>
/// <para>
///  This type is not thread safe. Use one instance from one thread.
/// </para>
/// </remarks>
public sealed class TcpReassembler
{
    private readonly Dictionary<FlowKey, TcpFlow> flows = new();
    private readonly HashSet<FlowKey> ended = new();

    /// <summary>
    ///  Initializes a new instance of the <see cref="TcpReassembler"/> class.
    /// </summary>
    public TcpReassembler()
    {
    }

    /// <summary>
    ///  Adds one captured packet to its flow and gives the new in-order bytes of that flow.
    /// </summary>
    /// <param name="frame">The captured packet, starting at the link-layer header.</param>
    /// <param name="linkType">The link type of the capture.</param>
    /// <param name="data">The new in-order bytes of the flow, or an empty span when the packet makes no new bytes available.</param>
    /// <returns>
    ///  The flow of the segment, or <see langword="null"/> when the packet does not carry IPv4 TCP, its headers do not
    ///  parse, the capture stops before the end of the packet, the packet is an IP fragment, the packet belongs to an
    ///  ended flow and has no SYN, or the packet has no SYN and no payload and no flow is open for it.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="linkType"/> is not a supported link type.</exception>
    /// <remarks>
    /// <para>
    ///  <paramref name="data"/> is also empty when a flow is returned for a segment with no payload, such as a SYN,
    ///  FIN, RST, or pure ACK; for bytes that follow a gap and are not yet in order; for bytes that the flow already
    ///  gave; and for the call that ends the flow at a gap. Such a segment can still change its flow, so check the
    ///  returned flow even when <paramref name="data"/> is empty.
    /// </para>
    /// <para>
    ///  The same <see cref="TcpFlow"/> instance comes back for each segment of a flow, so a caller can key its state on it.
    ///  After the call, <see cref="TcpFlow.Ended"/> shows that the flow ended and gives no more bytes, and
    ///  <see cref="TcpFlow.Lost"/> that it ended at a gap in the captured bytes; the caller first uses
    ///  <paramref name="data"/>, then can release its own state for the flow.
    /// </para>
    /// <para>
    ///  <paramref name="data"/> can hold more than the payload of this packet when the packet fills a gap before kept
    ///  segments. It is valid until the next call to this method, and no longer than <paramref name="frame"/>.
    /// </para>
    /// </remarks>
    public TcpFlow? Add(ReadOnlySpan<byte> frame, LinkType linkType, out ReadOnlySpan<byte> data)
    {
        data = default;

        if (!NetworkHeaders.TryGetTcpSegment(frame, linkType, out var segment))
            return null;

        var key = new FlowKey(
            BinaryPrimitives.ReadUInt32BigEndian(segment.SourceAddress), segment.SourcePort,
            BinaryPrimitives.ReadUInt32BigEndian(segment.DestinationAddress), segment.DestinationPort);
        var synchronize = (segment.Flags & NetworkHeaders.TcpFlagSyn) != 0;
        if (!flows.TryGetValue(key, out var current) || (synchronize && current.IsOtherConnection(segment.Sequence)))
        {
            if (!synchronize && (segment.Payload.IsEmpty || ended.Contains(key)))
                return null;

            if (current is { Ended: false })
                current.EndForNewConnection();

            ended.Remove(key);
            current = new TcpFlow(
                new IPEndPoint(new IPAddress(segment.SourceAddress), segment.SourcePort),
                new IPEndPoint(new IPAddress(segment.DestinationAddress), segment.DestinationPort));
            if (flows.TryGetValue(key.Reverse, out var opposite))
                current.Link(opposite);
            flows[key] = current;
        }

        current.Add(segment.Flags, segment.Sequence, segment.Payload, out data);
        if (current.Ended)
        {
            flows.Remove(key);
            ended.Add(key);
        }
        return current;
    }

    private readonly record struct FlowKey(uint SourceAddress, ushort SourcePort, uint DestinationAddress, ushort DestinationPort)
    {
        public FlowKey Reverse => new(DestinationAddress, DestinationPort, SourceAddress, SourcePort);
    }
}
