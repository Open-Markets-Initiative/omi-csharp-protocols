namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

/// <summary>
///  Represents a message contained in File, identified by its message type.
/// </summary>
public interface IFileMessage : IMessage
{
    /// <summary>
    ///  Gets the message type.
    /// </summary>
    MessageCode DataValue { get; }
}
