namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

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
