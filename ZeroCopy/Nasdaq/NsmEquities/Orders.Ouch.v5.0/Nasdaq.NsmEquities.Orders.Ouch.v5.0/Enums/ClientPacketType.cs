using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Code identifying this packet type sent by the client
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum ClientPacketType : byte
{
    /// <summary>
    ///  SoupbinTcp Debug Packet
    /// </summary>
    DebugPacket = (byte)'+',

    /// <summary>
    ///  SoupbinTcp Login Request Packet
    /// </summary>
    LoginRequestPacket = (byte)'L',

    /// <summary>
    ///  Soupbin Tcp Unsequenced Data Packet
    /// </summary>
    UnsequencedDataPacket = (byte)'U',

    /// <summary>
    ///  SoupbinTcp Client Heartbeat Packet
    /// </summary>
    ClientHeartbeatPacket = (byte)'R',

    /// <summary>
    ///  SoupbinTcp Logout Request Packet
    /// </summary>
    LogoutRequestPacket = (byte)'O',

}

/// <summary>
///  Reads and writes the Client Packet Type field in place.
/// </summary>
public struct ClientPacketTypeField
{
    /// <summary>
    ///  The size of the Client Packet Type field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Code identifying this packet type sent by the client
    /// </summary>
    public ClientPacketType Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Client Packet Type field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ClientPacketType Decode()
        => (ClientPacketType)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Client Packet Type field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ClientPacketType value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
