namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Identifies the type of a message contained in Server Packet by its message type.
/// </summary>
public enum ServerMessageCode : byte
{
    /// <summary>
    ///  Debug packets are intended to provide human readable text that may aid in debugging problems
    /// </summary>
    DebugPacket = (byte)'+',

    /// <summary>
    ///  The SoupBinTCP server sends a Login Accepted Packet in response to receiving a valid Login Request from the client
    /// </summary>
    LoginAcceptedPacket = (byte)'A',

    /// <summary>
    ///  The SoupBinTCP server sends this packet in response to an invalid Login Request Packet from the client
    /// </summary>
    LoginRejectedPacket = (byte)'J',

    /// <summary>
    ///  The Sequenced Data Packets act as an envelope to carry the actual sequenced data messages that are transferred from the server to the client. Each Sequenced Data Packet carries one message from the higher-lever protocol
    /// </summary>
    SequencedDataPacket = (byte)'S',
}
