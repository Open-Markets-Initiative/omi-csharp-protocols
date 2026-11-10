namespace Databento.MarketData.Normalized.Dbn.v2;

/// <summary>
///  Represents a message contained in File, identified by its message type.
/// </summary>
public interface IFileMessage : IMessage
{
    /// <summary>
    ///  Gets the message type.
    /// </summary>
    RecordBodyCode DataValue { get; }
}
