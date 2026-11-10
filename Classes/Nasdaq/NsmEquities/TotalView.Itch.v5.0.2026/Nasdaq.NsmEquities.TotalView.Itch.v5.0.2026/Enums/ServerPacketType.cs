namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

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
