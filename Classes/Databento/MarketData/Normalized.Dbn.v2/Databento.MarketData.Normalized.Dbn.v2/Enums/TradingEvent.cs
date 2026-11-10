namespace Databento.MarketData.Normalized.Dbn.v2;

/// <summary>
///  Further information about a status update
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum TradingEvent : ushort
{
    /// <summary>
    ///  None
    /// </summary>
    None = 0,

    /// <summary>
    ///  No Cancel
    /// </summary>
    NoCancel = 1,

    /// <summary>
    ///  Change Trading Session
    /// </summary>
    ChangeTradingSession = 2,

    /// <summary>
    ///  Implied Matching On
    /// </summary>
    ImpliedMatchingOn = 3,

    /// <summary>
    ///  Implied Matching Off
    /// </summary>
    ImpliedMatchingOff = 4,
}
