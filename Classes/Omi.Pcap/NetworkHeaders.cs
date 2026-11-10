using System.Buffers.Binary;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Omi.Pcap;

/// <summary>
///  Reads IPv4 UDP and TCP payloads and endpoints from captured packets of each supported <see cref="LinkType"/>.
/// </summary>
public static class NetworkHeaders
{
    /// <summary>
    ///  The size, in bytes, of an Ethernet header without VLAN tags.
    /// </summary>
    internal const int EthernetHeaderSize = 14;
    /// <summary>
    ///  The size, in bytes, of one VLAN tag.
    /// </summary>
    internal const int VlanTagSize = 4;
    /// <summary>
    ///  The size, in bytes, of a BSD loopback header.
    /// </summary>
    internal const int LoopbackHeaderSize = 4;
    /// <summary>
    ///  The size, in bytes, of a Linux cooked capture (SLL) header.
    /// </summary>
    internal const int LinuxCookedHeaderSize = 16;
    /// <summary>
    ///  The size, in bytes, of a Linux cooked capture version 2 (SLL2) header.
    /// </summary>
    internal const int LinuxCooked2HeaderSize = 20;
    /// <summary>
    ///  The EtherType of an IPv4 packet.
    /// </summary>
    internal const ushort EtherTypeIPv4 = 0x0800;
    /// <summary>
    ///  The EtherType of an IEEE 802.1Q VLAN tag.
    /// </summary>
    internal const ushort EtherTypeVlan = 0x8100;
    /// <summary>
    ///  The EtherType of an IEEE 802.1ad service VLAN tag.
    /// </summary>
    internal const ushort EtherTypeServiceVlan = 0x88A8;
    /// <summary>
    ///  The EtherType that older equipment uses for a service VLAN tag.
    /// </summary>
    internal const ushort EtherTypeLegacyServiceVlan = 0x9100;
    /// <summary>
    ///  The address family value of IPv4 in a BSD loopback header.
    /// </summary>
    internal const uint AddressFamilyIPv4 = 2;
    /// <summary>
    ///  The IPv4 protocol number of UDP.
    /// </summary>
    internal const byte ProtocolUdp = 17;
    /// <summary>
    ///  The IPv4 protocol number of TCP.
    /// </summary>
    internal const byte ProtocolTcp = 6;
    /// <summary>
    ///  The size, in bytes, of a UDP header.
    /// </summary>
    internal const int UdpHeaderSize = 8;
    /// <summary>
    ///  The size, in bytes, of an IPv4 header without options.
    /// </summary>
    internal const int IPv4MinimumHeaderSize = 20;
    /// <summary>
    ///  The size, in bytes, of a TCP header without options.
    /// </summary>
    internal const int TcpMinimumHeaderSize = 20;
    /// <summary>
    ///  The TCP flag that the sender sets when it has no more data to send.
    /// </summary>
    internal const byte TcpFlagFin = 0x01;
    /// <summary>
    ///  The TCP flag that the sender sets to open a connection.
    /// </summary>
    internal const byte TcpFlagSyn = 0x02;
    /// <summary>
    ///  The TCP flag that the sender sets to reset a connection.
    /// </summary>
    internal const byte TcpFlagRst = 0x04;
    /// <summary>
    ///  The TCP flag that the sender sets when the acknowledgment number is valid.
    /// </summary>
    internal const byte TcpFlagAck = 0x10;

    private const int EthernetTypeOffset = 12;
    private const int LinuxCookedProtocolOffset = 14;
    private const int LinuxCooked2ProtocolOffset = 0;
    private const ushort VlanIdentifierMask = 0x0FFF;
    private const int VlanInnerTypeOffset = 2;
    private const int IPv4Version = 4;
    private const int IPVersionShift = 4;
    private const int IPv4HeaderLengthMask = 0x0F;
    private const int IPv4TotalLengthOffset = 2;
    private const int IPv4FragmentOffset = 6;
    private const ushort FragmentOffsetMask = 0x1FFF;
    private const ushort MoreFragmentsFlag = 0x2000;
    private const int IPv4ProtocolOffset = 9;
    private const int IPv4SourceAddressOffset = 12;
    private const int IPv4DestinationAddressOffset = 16;
    private const int IPv4AddressSize = 4;
    private const int SourcePortOffset = 0;
    private const int DestinationPortOffset = 2;
    private const int UdpLengthOffset = 4;
    private const int TcpSequenceNumberOffset = 4;
    private const int TcpDataOffsetOffset = 12;
    private const int TcpFlagsOffset = 13;
    private const int TcpDataOffsetShift = 4;
    private const int BytesPerWord = 4;

    /// <summary>
    ///  Gets the UDP payload of an IPv4 UDP packet.
    /// </summary>
    /// <param name="frame">The captured packet, starting at the link-layer header.</param>
    /// <param name="linkType">The link type of the capture.</param>
    /// <param name="payload">The UDP payload when the method returns <see langword="true"/>; otherwise, empty.</param>
    /// <returns><see langword="true"/> if the packet holds a whole UDP datagram; otherwise, <see langword="false"/>, for example when the packet is not IPv4 UDP, is a fragment, its lengths disagree, or the capture stops before the stated end of the datagram.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="linkType"/> is not a supported link type.</exception>
    /// <remarks>
    ///  The payload ends at the UDP length, so link-layer padding after the datagram is not part of it.
    /// </remarks>
    public static bool TryGetUdpPayload(ReadOnlySpan<byte> frame, LinkType linkType, out ReadOnlySpan<byte> payload)
    {
        payload = default;
        if (!TryGetUdpPayloadOffset(frame, linkType, out var offset, out var length))
            return false;
        payload = frame.Slice(offset, length);
        return true;
    }

    /// <summary>
    ///  Gets the offset and length of the UDP payload in an IPv4 UDP packet.
    /// </summary>
    /// <param name="frame">The captured packet, starting at the link-layer header.</param>
    /// <param name="linkType">The link type of the capture.</param>
    /// <param name="offset">The offset of the first payload byte when the method returns <see langword="true"/>; otherwise, -1.</param>
    /// <param name="length">The number of payload bytes when the method returns <see langword="true"/>; otherwise, zero.</param>
    /// <returns><see langword="true"/> if the packet holds a whole UDP datagram; otherwise, <see langword="false"/>, for example when the packet is not IPv4 UDP, is a fragment, its lengths disagree, or the capture stops before the stated end of the datagram.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="linkType"/> is not a supported link type.</exception>
    /// <remarks>
    ///  The payload ends at the UDP length, so link-layer padding after the datagram is not part of it.
    /// </remarks>
    public static bool TryGetUdpPayloadOffset(ReadOnlySpan<byte> frame, LinkType linkType, out int offset, out int length)
    {
        offset = -1;
        length = 0;

        var udpStart = GetIPv4PayloadOffset(frame, linkType, out var protocol, out var ipPayloadLength);
        if (udpStart < 0 || protocol != ProtocolUdp || ipPayloadLength < UdpHeaderSize)
            return false;

        var udpLength = BinaryPrimitives.ReadUInt16BigEndian(frame[(udpStart + UdpLengthOffset)..]);
        if (udpLength < UdpHeaderSize || udpLength > ipPayloadLength)
            return false;

        offset = udpStart + UdpHeaderSize;
        length = udpLength - UdpHeaderSize;
        return true;
    }

    /// <summary>
    ///  Gets the TCP payload of an IPv4 TCP packet.
    /// </summary>
    /// <param name="frame">The captured packet, starting at the link-layer header.</param>
    /// <param name="linkType">The link type of the capture.</param>
    /// <param name="payload">The TCP payload when the method returns <see langword="true"/>; otherwise, empty.</param>
    /// <returns><see langword="true"/> if the packet holds a whole TCP segment; otherwise, <see langword="false"/>, for example when the packet is not IPv4 TCP, is a fragment, its lengths disagree, or the capture stops before the stated end of the segment.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="linkType"/> is not a supported link type.</exception>
    /// <remarks>
    ///  The payload ends at the IPv4 total length, so link-layer padding after the segment is not part of it.
    /// </remarks>
    public static bool TryGetTcpPayload(ReadOnlySpan<byte> frame, LinkType linkType, out ReadOnlySpan<byte> payload)
    {
        payload = default;
        if (!TryGetTcpPayloadOffset(frame, linkType, out var offset, out var length))
            return false;
        payload = frame.Slice(offset, length);
        return true;
    }

    /// <summary>
    ///  Gets the offset and length of the TCP payload in an IPv4 TCP packet.
    /// </summary>
    /// <param name="frame">The captured packet, starting at the link-layer header.</param>
    /// <param name="linkType">The link type of the capture.</param>
    /// <param name="offset">The offset of the first payload byte when the method returns <see langword="true"/>; otherwise, -1.</param>
    /// <param name="length">The number of payload bytes when the method returns <see langword="true"/>; otherwise, zero.</param>
    /// <returns><see langword="true"/> if the packet holds a whole TCP segment; otherwise, <see langword="false"/>, for example when the packet is not IPv4 TCP, is a fragment, its lengths disagree, or the capture stops before the stated end of the segment.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="linkType"/> is not a supported link type.</exception>
    /// <remarks>
    ///  The payload ends at the IPv4 total length, so link-layer padding after the segment is not part of it.
    /// </remarks>
    public static bool TryGetTcpPayloadOffset(ReadOnlySpan<byte> frame, LinkType linkType, out int offset, out int length)
    {
        offset = -1;
        length = 0;

        var tcpStart = GetIPv4PayloadOffset(frame, linkType, out var protocol, out var ipPayloadLength);
        if (tcpStart < 0 || protocol != ProtocolTcp || ipPayloadLength < TcpMinimumHeaderSize)
            return false;

        var tcpHeaderSize = (frame[tcpStart + TcpDataOffsetOffset] >> TcpDataOffsetShift) * BytesPerWord;
        if (tcpHeaderSize < TcpMinimumHeaderSize || tcpHeaderSize > ipPayloadLength)
            return false;

        offset = tcpStart + tcpHeaderSize;
        length = ipPayloadLength - tcpHeaderSize;
        return true;
    }

    /// <summary>
    ///  Gets the source and destination addresses and ports of an IPv4 TCP or UDP packet.
    /// </summary>
    /// <param name="frame">The captured packet, starting at the link-layer header.</param>
    /// <param name="linkType">The link type of the capture.</param>
    /// <param name="source">The sender address and port when the method returns <see langword="true"/>; otherwise, <see langword="null"/>.</param>
    /// <param name="destination">The receiver address and port when the method returns <see langword="true"/>; otherwise, <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if the packet holds a TCP or UDP header; otherwise, <see langword="false"/>, for example when the packet is not IPv4 TCP or UDP, is a TCP fragment or a UDP fragment after the first, its lengths disagree, or the capture stops before the stated end of the packet.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="linkType"/> is not a supported link type.</exception>
    public static bool TryGetEndpoints(
        ReadOnlySpan<byte> frame, LinkType linkType,
        [NotNullWhen(true)] out IPEndPoint? source, [NotNullWhen(true)] out IPEndPoint? destination)
    {
        source = null;
        destination = null;

        var transportStart = GetIPv4PayloadOffset(frame, linkType, out var protocol, out var ipPayloadLength);
        var headerSize = protocol switch
        {
            ProtocolTcp => TcpMinimumHeaderSize,
            ProtocolUdp => UdpHeaderSize,
            _ => -1,
        };
        if (transportStart < 0 || headerSize < 0 || ipPayloadLength < headerSize)
            return false;

        var ipStart = GetIPv4HeaderOffset(frame, linkType, out _);
        source = new IPEndPoint(
            new IPAddress(frame.Slice(ipStart + IPv4SourceAddressOffset, IPv4AddressSize)),
            BinaryPrimitives.ReadUInt16BigEndian(frame[(transportStart + SourcePortOffset)..]));
        destination = new IPEndPoint(
            new IPAddress(frame.Slice(ipStart + IPv4DestinationAddressOffset, IPv4AddressSize)),
            BinaryPrimitives.ReadUInt16BigEndian(frame[(transportStart + DestinationPortOffset)..]));
        return true;
    }

    /// <summary>
    ///  Returns the offset of the IPv4 header, or -1 when the packet is short or does not carry IPv4, and sets vlanId to the innermost VLAN identifier or zero.
    /// </summary>
    internal static int GetIPv4HeaderOffset(ReadOnlySpan<byte> frame, LinkType linkType, out ushort vlanId)
    {
        vlanId = 0;
        var ipStart = linkType switch
        {
            LinkType.Ethernet => AfterEtherType(frame, EthernetTypeOffset, EthernetHeaderSize, out vlanId),
            LinkType.LinuxCooked => AfterEtherType(frame, LinuxCookedProtocolOffset, LinuxCookedHeaderSize, out vlanId),
            LinkType.LinuxCooked2 => AfterEtherType(frame, LinuxCooked2ProtocolOffset, LinuxCooked2HeaderSize, out vlanId),
            LinkType.Null => AfterLoopback(frame),
            LinkType.Raw or LinkType.IPv4 => 0,
            LinkType.IPv6 => -1,
            _ => throw new ArgumentOutOfRangeException(nameof(linkType), linkType, "unsupported link type"),
        };

        if (ipStart < 0 || frame.Length < ipStart + IPv4MinimumHeaderSize)
            return -1;

        return frame[ipStart] >> IPVersionShift == IPv4Version ? ipStart : -1;
    }

    /// <summary>
    ///  Returns <see langword="true"/> for an IPv4 fragment that does not hold a whole TCP segment or UDP datagram, because this library does not reassemble fragments.
    /// </summary>
    internal static bool IsPartialFragment(ReadOnlySpan<byte> frame, LinkType linkType)
    {
        var ipStart = GetIPv4HeaderOffset(frame, linkType, out _);
        if (ipStart < 0)
            return false;

        var fragment = BinaryPrimitives.ReadUInt16BigEndian(frame[(ipStart + IPv4FragmentOffset)..]);
        return (fragment & (FragmentOffsetMask | MoreFragmentsFlag)) != 0 && !TryGetUdpPayloadOffset(frame, linkType, out _, out _);
    }

    internal static bool TryGetTcpSegment(ReadOnlySpan<byte> frame, LinkType linkType, out TcpSegment segment)
    {
        segment = default;

        var tcpStart = GetIPv4PayloadOffset(frame, linkType, out var ipStart, out var protocol, out var ipPayloadLength);
        if (tcpStart < 0 || protocol != ProtocolTcp || ipPayloadLength < TcpMinimumHeaderSize)
            return false;

        var tcpHeaderSize = (frame[tcpStart + TcpDataOffsetOffset] >> TcpDataOffsetShift) * BytesPerWord;
        if (tcpHeaderSize < TcpMinimumHeaderSize || tcpHeaderSize > ipPayloadLength)
            return false;

        segment = new TcpSegment(
            frame.Slice(ipStart + IPv4SourceAddressOffset, IPv4AddressSize),
            BinaryPrimitives.ReadUInt16BigEndian(frame[(tcpStart + SourcePortOffset)..]),
            frame.Slice(ipStart + IPv4DestinationAddressOffset, IPv4AddressSize),
            BinaryPrimitives.ReadUInt16BigEndian(frame[(tcpStart + DestinationPortOffset)..]),
            frame[tcpStart + TcpFlagsOffset],
            BinaryPrimitives.ReadUInt32BigEndian(frame[(tcpStart + TcpSequenceNumberOffset)..]),
            frame.Slice(tcpStart + tcpHeaderSize, ipPayloadLength - tcpHeaderSize));
        return true;
    }

    private static int GetIPv4PayloadOffset(ReadOnlySpan<byte> frame, LinkType linkType, out byte protocol, out int payloadLength)
        => GetIPv4PayloadOffset(frame, linkType, out _, out protocol, out payloadLength);

    /// <summary>
    ///  Returns -1 for a fragment after the first and for a first TCP fragment, which has no length field to show that it is incomplete; a first UDP fragment fails the UDP length check instead.
    /// </summary>
    private static int GetIPv4PayloadOffset(ReadOnlySpan<byte> frame, LinkType linkType, out int ipStart, out byte protocol, out int payloadLength)
    {
        protocol = 0;
        payloadLength = 0;

        ipStart = GetIPv4HeaderOffset(frame, linkType, out _);
        if (ipStart < 0)
            return -1;

        var ipHeaderSize = (frame[ipStart] & IPv4HeaderLengthMask) * BytesPerWord;
        var totalLength = BinaryPrimitives.ReadUInt16BigEndian(frame[(ipStart + IPv4TotalLengthOffset)..]);
        if (ipHeaderSize < IPv4MinimumHeaderSize || totalLength < ipHeaderSize || ipStart + totalLength > frame.Length)
            return -1;

        var fragment = BinaryPrimitives.ReadUInt16BigEndian(frame[(ipStart + IPv4FragmentOffset)..]);
        var transport = frame[ipStart + IPv4ProtocolOffset];
        if ((fragment & FragmentOffsetMask) != 0 || (fragment & MoreFragmentsFlag) != 0 && transport == ProtocolTcp)
            return -1;

        protocol = transport;
        payloadLength = totalLength - ipHeaderSize;
        return ipStart + ipHeaderSize;
    }

    /// <summary>
    ///  Returns the offset of the IPv4 header after the EtherType and any VLAN tags, or -1 when the packet is short or does not carry IPv4.
    /// </summary>
    private static int AfterEtherType(ReadOnlySpan<byte> frame, int typeOffset, int headerSize, out ushort vlanId)
    {
        vlanId = 0;
        if (frame.Length < headerSize)
            return -1;

        var etherType = BinaryPrimitives.ReadUInt16BigEndian(frame[typeOffset..]);
        var offset = headerSize;

        while (etherType is EtherTypeVlan or EtherTypeServiceVlan or EtherTypeLegacyServiceVlan)
        {
            if (frame.Length < offset + VlanTagSize)
                return -1;
            vlanId = (ushort)(BinaryPrimitives.ReadUInt16BigEndian(frame[offset..]) & VlanIdentifierMask);
            etherType = BinaryPrimitives.ReadUInt16BigEndian(frame[(offset + VlanInnerTypeOffset)..]);
            offset += VlanTagSize;
        }

        return etherType == EtherTypeIPv4 ? offset : -1;
    }

    /// <summary>
    ///  The capture host writes the address family in its own byte order, so both byte orders are accepted.
    /// </summary>
    private static int AfterLoopback(ReadOnlySpan<byte> frame)
    {
        if (frame.Length < LoopbackHeaderSize)
            return -1;

        var isIPv4 = BinaryPrimitives.ReadUInt32LittleEndian(frame) == AddressFamilyIPv4
            || BinaryPrimitives.ReadUInt32BigEndian(frame) == AddressFamilyIPv4;
        return isIPv4 ? LoopbackHeaderSize : -1;
    }
}
