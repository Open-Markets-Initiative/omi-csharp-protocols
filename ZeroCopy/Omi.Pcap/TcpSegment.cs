namespace Omi.Pcap;

/// <summary>
///  The header values and the payload of one captured IPv4 TCP segment, as slices of the captured packet.
/// </summary>
internal readonly ref struct TcpSegment(
    ReadOnlySpan<byte> sourceAddress, ushort sourcePort, ReadOnlySpan<byte> destinationAddress, ushort destinationPort,
    byte flags, uint sequence, ReadOnlySpan<byte> payload)
{
    public ReadOnlySpan<byte> SourceAddress { get; } = sourceAddress;

    public ushort SourcePort { get; } = sourcePort;

    public ReadOnlySpan<byte> DestinationAddress { get; } = destinationAddress;

    public ushort DestinationPort { get; } = destinationPort;

    public byte Flags { get; } = flags;

    public uint Sequence { get; } = sequence;

    public ReadOnlySpan<byte> Payload { get; } = payload;
}
