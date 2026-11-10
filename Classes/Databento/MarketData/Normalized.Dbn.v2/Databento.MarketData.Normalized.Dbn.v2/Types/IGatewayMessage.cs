namespace Databento.MarketData.Normalized.Dbn.v2;

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
