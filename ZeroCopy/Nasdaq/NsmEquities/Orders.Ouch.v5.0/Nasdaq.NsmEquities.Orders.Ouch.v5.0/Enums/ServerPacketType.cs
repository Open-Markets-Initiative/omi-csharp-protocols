using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Code identifying this packet type sent by the server
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum ServerPacketType : byte
{
    /// <summary>
    ///  SoupbinTcp Debug Packet
    /// </summary>
    DebugPacket = (byte)'+',

    /// <summary>
    ///  SoupbinTcp Login Accepted Packet
    /// </summary>
    LoginAcceptedPacket = (byte)'A',

    /// <summary>
    ///  SoupbinTcp Login Rejected Packet
    /// </summary>
    LoginRejectedPacket = (byte)'J',

    /// <summary>
    ///  Sequenced Data Packet
    /// </summary>
    SequencedDataPacket = (byte)'S',

    /// <summary>
    ///  SoupbinTcp Server Heartbeat Packet
    /// </summary>
    ServerHeartbeatPacket = (byte)'H',

    /// <summary>
    ///  SoupbinTcp Login End of Session Packet
    /// </summary>
    EndOfSessionPacket = (byte)'Z',

}

/// <summary>
///  Reads and writes the Server Packet Type field in place.
/// </summary>
public struct ServerPacketTypeField
{
    /// <summary>
    ///  The size of the Server Packet Type field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Code identifying this packet type sent by the server
    /// </summary>
    public ServerPacketType Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Server Packet Type field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ServerPacketType Decode()
        => (ServerPacketType)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Server Packet Type field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ServerPacketType value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
