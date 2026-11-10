namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Represents a message contained in Sequenced Message, identified by its message type.
/// </summary>
public interface ISequencedMessage : IMessage
{
    /// <summary>
    ///  Gets the message type.
    /// </summary>
    SequencedMessageCode DataValue { get; }
}
