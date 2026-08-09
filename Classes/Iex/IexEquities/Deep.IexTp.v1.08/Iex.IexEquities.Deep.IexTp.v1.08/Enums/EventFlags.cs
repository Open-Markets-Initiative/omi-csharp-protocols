namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  Identifies event processing by the System
/// </summary>
/// <remarks>
///  Backed by byte. Values correspond to the wire-format integer encoding.
/// </remarks>
public enum EventFlags : byte
{
    /// <summary>
    ///  Order Book Is Processing An Event
    /// </summary>
    OrderBookIsProcessingAnEvent = 0,

    /// <summary>
    ///  Event Processing Complete
    /// </summary>
    EventProcessingComplete = 1,
}
