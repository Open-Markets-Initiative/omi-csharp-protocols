namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Represents a message contained in Unsequenced Message, identified by its message type.
/// </summary>
public interface IUnsequencedMessage : IMessage
{
    /// <summary>
    ///  Gets the message type.
    /// </summary>
    UnsequencedMessageCode DataValue { get; }
}
