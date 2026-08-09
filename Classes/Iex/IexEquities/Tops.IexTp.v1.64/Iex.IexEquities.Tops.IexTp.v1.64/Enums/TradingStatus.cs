namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Trading status identifier
/// </summary>
/// <remarks>
///  Each value corresponds to the ASCII byte value used as the wire-format discriminator.
/// </remarks>
public enum TradingStatus : byte
{
    /// <summary>
    ///  Trading Halted Across All Us Equity Markets
    /// </summary>
    TradingHaltedAcrossAllUsEquityMarkets = (byte)'H',

    /// <summary>
    ///  Trading Paused And Order Acceptance Period On Iex
    /// </summary>
    TradingPausedAndOrderAcceptancePeriodOnIex = (byte)'P',

    /// <summary>
    ///  Trading On Iex
    /// </summary>
    TradingOnIex = (byte)'T',
}
