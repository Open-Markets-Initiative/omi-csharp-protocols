using System.Buffers.Binary;

namespace Pcap.CSharp;

/// <summary>
///  Minimal network header parsing to extract UDP payload from Ethernet frames
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
    ///  Returns the byte offset of the UDP payload within an Ethernet frame, or -1 if the
    ///  frame is not IPv4/UDP. Handles optional 802.1Q VLAN tags and variable-length IP headers.
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
    ///  Try to extract the UDP payload from an Ethernet frame.
    ///  Returns false if the frame is not IPv4/UDP.
    /// </summary>
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
    ///  Try to find the byte offset of the UDP payload within an Ethernet frame.
    ///  Returns false (offset -1) if the frame is not IPv4/UDP or the offset is out of range.
    ///  Offset form of <see cref="TryGetUdpPayload"/> for callers (e.g. iterators) that cannot
    ///  hold a <c>ReadOnlySpan&lt;byte&gt;</c> across a yield.
    /// </summary>
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
    ///  Returns the byte offset of the TCP payload within an Ethernet frame, or -1 if the
    ///  frame is not IPv4/TCP. Handles optional 802.1Q VLAN tags, variable-length IP headers,
    ///  and variable-length TCP headers (options).
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

        // TCP data offset is the high nibble of byte 12 of the TCP header, in 32-bit words
        var tcpDataOffset = (frame[tcpStart + 12] >> 4) * 4;
        if (tcpDataOffset < 20)
            return -1;

        return tcpStart + tcpDataOffset;
    }

    /// <summary>
    ///  Try to extract the TCP payload from an Ethernet frame.
    ///  Returns false if the frame is not IPv4/TCP. Span companion to <see cref="TryGetUdpPayload"/>.
    /// </summary>
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
    ///  Try to find the byte offset of the TCP payload within an Ethernet frame.
    ///  Returns false (offset -1) if the frame is not IPv4/TCP or the offset is out of range.
    ///  Offset companion to <see cref="TryGetUdpPayloadOffset"/> for transport-branching callers.
    /// </summary>
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
    ///  Parses Ethernet, optional VLAN, and IPv4 headers to find the start of the IP payload.
    ///  Sets <paramref name="protocol"/> to the IPv4 protocol field (e.g. 6=TCP, 17=UDP).
    ///  Returns -1 and sets protocol to 0 if the frame is too short or not IPv4.
    /// </summary>
    private static int GetIpPayloadOffset(ReadOnlySpan<byte> frame, out byte protocol)
    {
        protocol = 0;

        if (frame.Length < EthernetHeaderSize)
            return -1;

        var etherType = BinaryPrimitives.ReadUInt16BigEndian(frame[12..]);

        // Skip optional 802.1Q VLAN tag (4 bytes: 2 TPID + 2 TCI)
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
