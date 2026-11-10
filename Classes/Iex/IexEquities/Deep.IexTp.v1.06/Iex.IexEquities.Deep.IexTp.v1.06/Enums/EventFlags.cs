namespace Iex.IexEquities.Deep.IexTp.v1_06;

/// <summary>
///  Identifies event processing by the System
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
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
