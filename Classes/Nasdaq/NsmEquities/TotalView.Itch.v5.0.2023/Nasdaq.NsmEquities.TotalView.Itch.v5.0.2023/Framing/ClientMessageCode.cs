namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

/// <summary>
///  Identifies the type of a message contained in Client Packet by its message type.
/// </summary>
public enum ClientMessageCode : byte
{
    /// <summary>
    ///  Debug packets are intended to provide human readable text that may aid in debugging problems
    /// </summary>
    DebugPacket = (byte)'+',

    /// <summary>
    ///  The SoupBinTCP client must send a Login Request Packet immediately upon establishing a new TCP/IP socket connection to the server
    /// </summary>
    LoginRequestPacket = (byte)'L',

    /// <summary>
    ///  The Unsequenced Data Packets act as an envelope to carry the actual data messages that are transferred from the client to the server
    /// </summary>
    UnsequencedDataPacket = (byte)'U',
}
