namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  The record type, which selects the record that follows the header
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum Rtype : byte
{
    /// <summary>
    ///  Market By Price 0
    /// </summary>
    MarketByPrice0 = 0,

    /// <summary>
    ///  Market By Price 1
    /// </summary>
    MarketByPrice1 = 1,

    /// <summary>
    ///  Market By Price 10
    /// </summary>
    MarketByPrice10 = 10,

    /// <summary>
    ///  Ohlcv Deprecated
    /// </summary>
    OhlcvDeprecated = 17,

    /// <summary>
    ///  Status
    /// </summary>
    Status = 18,

    /// <summary>
    ///  Instrument Definition
    /// </summary>
    InstrumentDefinition = 19,

    /// <summary>
    ///  Imbalance
    /// </summary>
    Imbalance = 20,

    /// <summary>
    ///  Error
    /// </summary>
    Error = 21,

    /// <summary>
    ///  Symbol Mapping
    /// </summary>
    SymbolMapping = 22,

    /// <summary>
    ///  System
    /// </summary>
    System = 23,

    /// <summary>
    ///  Statistics
    /// </summary>
    Statistics = 24,

    /// <summary>
    ///  Ohlcv 1 Second
    /// </summary>
    Ohlcv1Second = 32,

    /// <summary>
    ///  Ohlcv 1 Minute
    /// </summary>
    Ohlcv1Minute = 33,

    /// <summary>
    ///  Ohlcv 1 Hour
    /// </summary>
    Ohlcv1Hour = 34,

    /// <summary>
    ///  Ohlcv 1 Day
    /// </summary>
    Ohlcv1Day = 35,

    /// <summary>
    ///  Ohlcv End Of Day
    /// </summary>
    OhlcvEndOfDay = 36,

    /// <summary>
    ///  Market By Order
    /// </summary>
    MarketByOrder = 160,

    /// <summary>
    ///  Consolidated Market By Price 1
    /// </summary>
    ConsolidatedMarketByPrice1 = 177,

    /// <summary>
    ///  Consolidated Bbo 1 Second
    /// </summary>
    ConsolidatedBbo1Second = 192,

    /// <summary>
    ///  Consolidated Bbo 1 Minute
    /// </summary>
    ConsolidatedBbo1Minute = 193,

    /// <summary>
    ///  Trade With Consolidated Bbo
    /// </summary>
    TradeWithConsolidatedBbo = 194,

    /// <summary>
    ///  Bbo 1 Second
    /// </summary>
    Bbo1Second = 195,

    /// <summary>
    ///  Bbo 1 Minute
    /// </summary>
    Bbo1Minute = 196,
}
