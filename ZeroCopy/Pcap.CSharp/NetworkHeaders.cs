using System.Buffers.Binary;

namespace Pcap.CSharp;

/// <summary>
///  Reads IPv4 UDP and TCP payloads from Ethernet frames.
/// </summary>
public static class NetworkHeaders
{
    public const int EthernetHeaderSize = 14;
    public const ushort EtherTypeIPv4 = 0x0800;
    public const ushort EtherTypeVlan = 0x8100;
    public const byte ProtocolUDP = 17;
    public const byte ProtocolTCP = 6;
    public const int UdpHeaderSize = 8;

    /// <summary>
    ///  Gets the UDP payload offset, or -1 when the frame is not IPv4 UDP.
    ///  Supports one 802.1Q VLAN tag and variable IPv4 header lengths.
    /// </summary>
    /// <param name="frame">The full Ethernet frame, starting at the destination MAC address.</param>
    /// <returns>Byte offset of the first UDP payload byte, or -1 if not IPv4/UDP.</returns>
    public static int GetUdpPayloadOffset(ReadOnlySpan<byte> frame)
    {
        var ipPayloadOffset = GetIpPayloadOffset(frame, out var protocol);
        if (ipPayloadOffset < 0 || protocol != ProtocolUDP)
            return -1;

        return ipPayloadOffset + UdpHeaderSize;
    }

    /// <summary>
    ///  Gets the UDP payload when the frame is IPv4 UDP.
    /// </summary>
    /// <param name="frame">The Ethernet frame.</param>
    /// <param name="payload">The UDP payload when the method returns <see langword="true"/>.</param>
    /// <returns><see langword="true"/> when the frame contains a UDP payload.</returns>
    public static bool TryGetUdpPayload(ReadOnlySpan<byte> frame, out ReadOnlySpan<byte> payload)
    {
        payload = default;
        var offset = GetUdpPayloadOffset(frame);
        if (offset < 0 || offset > frame.Length)
            return false;
        payload = frame[offset..];
        return true;
    }

    /// <summary>
    ///  Gets the UDP payload offset when the frame is IPv4 UDP.
    ///  This form supports callers that cannot retain a <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    /// <param name="frame">The Ethernet frame.</param>
    /// <param name="offset">The payload offset when the method returns <see langword="true"/>; otherwise -1.</param>
    /// <returns><see langword="true"/> when the frame contains a UDP payload.</returns>
    public static bool TryGetUdpPayloadOffset(ReadOnlySpan<byte> frame, out int offset)
    {
        offset = GetUdpPayloadOffset(frame);
        if (offset < 0 || offset > frame.Length)
        {
            offset = -1;
            return false;
        }
        return true;
    }

    /// <summary>
    ///  Gets the TCP payload offset, or -1 when the frame is not IPv4 TCP.
    ///  Supports one 802.1Q VLAN tag and variable IPv4 and TCP header lengths.
    /// </summary>
    /// <param name="frame">The full Ethernet frame, starting at the destination MAC address.</param>
    /// <returns>Byte offset of the first TCP payload byte, or -1 if not IPv4/TCP.</returns>
    public static int GetTcpPayloadOffset(ReadOnlySpan<byte> frame)
    {
        var tcpStart = GetIpPayloadOffset(frame, out var protocol);
        if (tcpStart < 0 || protocol != ProtocolTCP)
            return -1;

        if (frame.Length < tcpStart + 20)
            return -1;

        // TCP byte 12 stores the header length in 32-bit words.
        var tcpDataOffset = (frame[tcpStart + 12] >> 4) * 4;
        if (tcpDataOffset < 20)
            return -1;

        return tcpStart + tcpDataOffset;
    }

    /// <summary>
    ///  Gets the TCP payload when the frame is IPv4 TCP.
    /// </summary>
    /// <param name="frame">The Ethernet frame.</param>
    /// <param name="payload">The TCP payload when the method returns <see langword="true"/>.</param>
    /// <returns><see langword="true"/> when the frame contains a TCP payload.</returns>
    public static bool TryGetTcpPayload(ReadOnlySpan<byte> frame, out ReadOnlySpan<byte> payload)
    {
        payload = default;
        var offset = GetTcpPayloadOffset(frame);
        if (offset < 0 || offset > frame.Length)
            return false;
        payload = frame[offset..];
        return true;
    }

    /// <summary>
    ///  Gets the TCP payload offset when the frame is IPv4 TCP.
    /// </summary>
    /// <param name="frame">The Ethernet frame.</param>
    /// <param name="offset">The payload offset when the method returns <see langword="true"/>; otherwise -1.</param>
    /// <returns><see langword="true"/> when the frame contains a TCP payload.</returns>
    public static bool TryGetTcpPayloadOffset(ReadOnlySpan<byte> frame, out int offset)
    {
        offset = GetTcpPayloadOffset(frame);
        if (offset < 0 || offset > frame.Length)
        {
            offset = -1;
            return false;
        }
        return true;
    }

    /// <summary>
    ///  Gets the IPv4 payload offset and protocol value.
    ///  Returns -1 and sets <paramref name="protocol"/> to zero for a short or non-IPv4 frame.
    /// </summary>
    private static int GetIpPayloadOffset(ReadOnlySpan<byte> frame, out byte protocol)
    {
        protocol = 0;

        if (frame.Length < EthernetHeaderSize)
            return -1;

        var etherType = BinaryPrimitives.ReadUInt16BigEndian(frame[12..]);

        // An 802.1Q VLAN tag adds four bytes before the IPv4 header.
        var ipStart = EthernetHeaderSize;
        if (etherType == EtherTypeVlan)
        {
            if (frame.Length < EthernetHeaderSize + 4)
                return -1;
            etherType = BinaryPrimitives.ReadUInt16BigEndian(frame[16..]);
            ipStart = EthernetHeaderSize + 4;
        }

        if (etherType != EtherTypeIPv4)
            return -1;

        if (frame.Length < ipStart + 20)
            return -1;

        var ihl = (frame[ipStart] & 0x0F) * 4;
        if (ihl < 20)
            return -1;

        protocol = frame[ipStart + 9];
        return ipStart + ihl;
    }
}
