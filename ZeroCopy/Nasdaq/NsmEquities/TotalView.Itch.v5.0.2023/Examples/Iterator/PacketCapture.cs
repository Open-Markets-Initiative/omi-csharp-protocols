using System;
using System.Buffers.Binary;
namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Iterator;
/// <summary>
///  Reads the Packet of each UDP datagram in a capture.
/// </summary>
public sealed class PacketCapture
{
    /// <summary>
    ///  Gets the sender address and port of the last datagram.
    /// </summary>
    public global::System.Net.IPEndPoint? CurrentSource { get; private set; }
    /// <summary>
    ///  Gets the receiver address and port of the last datagram.
    /// </summary>
    public global::System.Net.IPEndPoint? CurrentDestination { get; private set; }
    /// <summary>
    ///  Gets a value that indicates whether the last frame was a UDP datagram that this capture read.
    /// </summary>
    public bool FrameRead { get; private set; }
    /// <summary>
    ///  Reads the packet of one captured UDP datagram.
    /// </summary>
    /// <remarks>
    ///  The Reader borrows the frame.
    /// </remarks>
    /// <returns>
    ///  <see langword="true"/> when the frame is a UDP datagram that holds the fixed fields at the start of the packet; otherwise, <see langword="false"/>. FrameRead tells whether the frame is a UDP datagram.
    /// </returns>
    public bool TryRead(ReadOnlySpan<byte> frame, global::Omi.Pcap.LinkType linkType, out global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Packet.Reader reading)
    {
        reading = default;
        if (!global::Omi.Pcap.NetworkHeaders.TryGetUdpPayload(frame, linkType, out var bytes) || !global::Omi.Pcap.NetworkHeaders.TryGetEndpoints(frame, linkType, out var source, out var destination))
        {
            this.FrameRead = false;
            return false;
        }
        this.CurrentSource = source;
        this.CurrentDestination = destination;
        this.FrameRead = true;
        return global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Packet.TryRead(bytes, out reading);
    }
}
