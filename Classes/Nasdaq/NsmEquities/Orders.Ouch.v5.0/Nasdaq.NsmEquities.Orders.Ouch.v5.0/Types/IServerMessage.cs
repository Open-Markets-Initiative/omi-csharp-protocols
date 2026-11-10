namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Represents a message contained in Server Packet, identified by its message type.
/// </summary>
public interface IServerMessage : IMessage
{
    /// <summary>
    ///  Gets the message type.
    /// </summary>
    ServerMessageCode DataValue { get; }
}
