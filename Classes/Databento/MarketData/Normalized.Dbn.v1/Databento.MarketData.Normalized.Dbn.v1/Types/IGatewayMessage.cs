namespace Databento.MarketData.Normalized.Dbn.v1;

/// <summary>
///  Represents a message contained in Gateway Stream, identified by its message type.
/// </summary>
public interface IGatewayMessage : IMessage
{
    /// <summary>
    ///  Gets the message type.
    /// </summary>
    RecordBodyCode DataValue { get; }
}
