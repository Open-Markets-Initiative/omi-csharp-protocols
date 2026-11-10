namespace Databento.MarketData.Normalized.Dbn.v2;

/// <summary>
///  The error code
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum ErrorCode : byte
{
    /// <summary>
    ///  Auth Failed
    /// </summary>
    AuthFailed = 1,

    /// <summary>
    ///  Api Key Deactivated
    /// </summary>
    ApiKeyDeactivated = 2,

    /// <summary>
    ///  Connection Limit Exceeded
    /// </summary>
    ConnectionLimitExceeded = 3,

    /// <summary>
    ///  Symbol Resolution Failed
    /// </summary>
    SymbolResolutionFailed = 4,

    /// <summary>
    ///  Invalid Subscription
    /// </summary>
    InvalidSubscription = 5,

    /// <summary>
    ///  Internal Error
    /// </summary>
    InternalError = 6,

    /// <summary>
    ///  Skipped Records After Slow Reading
    /// </summary>
    SkippedRecordsAfterSlowReading = 7,

    /// <summary>
    ///  Replay Data Aged Out
    /// </summary>
    ReplayDataAgedOut = 8,

    /// <summary>
    ///  Unset
    /// </summary>
    Unset = 255,
}
