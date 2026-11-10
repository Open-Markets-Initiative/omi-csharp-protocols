namespace Nasdaq.NsmEquities.Qbbo.Itch.v2_1;

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
