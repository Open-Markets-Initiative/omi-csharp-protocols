namespace Iex.IexEquities.Deep.IexTp.v1_06;

/// <summary>
///  Represents a message contained in Packet, identified by its message type.
/// </summary>
public interface IPacketMessage : IMessage
{
    /// <summary>
    ///  Gets the message type.
    /// </summary>
    PacketMessageCode DataValue { get; }
}
