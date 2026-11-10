namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Represents a message contained in Client Packet, identified by its message type.
/// </summary>
public interface IClientMessage : IMessage
{
    /// <summary>
    ///  Gets the message type.
    /// </summary>
    ClientMessageCode DataValue { get; }
}
