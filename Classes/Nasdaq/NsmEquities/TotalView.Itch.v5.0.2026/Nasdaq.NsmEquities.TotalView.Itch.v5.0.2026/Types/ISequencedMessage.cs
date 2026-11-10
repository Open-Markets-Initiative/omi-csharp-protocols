namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

/// <summary>
///  Represents a message contained in Sequenced Message, identified by its message type.
/// </summary>
public interface ISequencedMessage : IMessage
{
    /// <summary>
    ///  Gets the message type.
    /// </summary>
    MessageCode DataValue { get; }
}
