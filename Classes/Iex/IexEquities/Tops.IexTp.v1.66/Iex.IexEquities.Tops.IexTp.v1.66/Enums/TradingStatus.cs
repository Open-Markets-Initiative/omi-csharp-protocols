namespace Iex.IexEquities.Tops.IexTp.v1_66;

/// <summary>
///  Trading status identifier
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum TradingStatus : byte
{
    /// <summary>
    ///  Trading Halted Across All Us Equity Markets
    /// </summary>
    TradingHaltedAcrossAllUsEquityMarkets = (byte)'H',

    /// <summary>
    ///  Trading Halt Released Into An Order Acceptance Period On Iex
    /// </summary>
    TradingHaltReleasedIntoAnOrderAcceptancePeriodOnIex = (byte)'O',

    /// <summary>
    ///  Trading Paused And Order Acceptance Period On Iex
    /// </summary>
    TradingPausedAndOrderAcceptancePeriodOnIex = (byte)'P',

    /// <summary>
    ///  Trading On Iex
    /// </summary>
    TradingOnIex = (byte)'T',
}
