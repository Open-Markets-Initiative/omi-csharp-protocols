namespace Databento.MarketData.Normalized.Dbn.v2;

/// <summary>
///  Type of system message
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum SystemCode : byte
{
    /// <summary>
    ///  Heartbeat
    /// </summary>
    Heartbeat = 0,

    /// <summary>
    ///  Subscription Ack
    /// </summary>
    SubscriptionAck = 1,

    /// <summary>
    ///  Slow Reader Warning
    /// </summary>
    SlowReaderWarning = 2,

    /// <summary>
    ///  Replay Completed
    /// </summary>
    ReplayCompleted = 3,

    /// <summary>
    ///  End Of Interval
    /// </summary>
    EndOfInterval = 4,

    /// <summary>
    ///  Unsubscribe Ack
    /// </summary>
    UnsubscribeAck = 5,

    /// <summary>
    ///  Unset
    /// </summary>
    Unset = 255,
}
