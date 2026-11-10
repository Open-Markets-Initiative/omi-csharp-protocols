namespace Databento.MarketData.Normalized.Dbn.v2;

/// <summary>
///  The data record schema; no value for a file of mixed schemas
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum Schema : ushort
{
    /// <summary>
    ///  Mbo
    /// </summary>
    Mbo = 0,

    /// <summary>
    ///  Mbp1
    /// </summary>
    Mbp1 = 1,

    /// <summary>
    ///  Mbp10
    /// </summary>
    Mbp10 = 2,

    /// <summary>
    ///  Tbbo
    /// </summary>
    Tbbo = 3,

    /// <summary>
    ///  Trades
    /// </summary>
    Trades = 4,

    /// <summary>
    ///  Ohlcv1 S
    /// </summary>
    Ohlcv1S = 5,

    /// <summary>
    ///  Ohlcv1 M
    /// </summary>
    Ohlcv1M = 6,

    /// <summary>
    ///  Ohlcv1 H
    /// </summary>
    Ohlcv1H = 7,

    /// <summary>
    ///  Ohlcv1 D
    /// </summary>
    Ohlcv1D = 8,

    /// <summary>
    ///  Definition
    /// </summary>
    Definition = 9,

    /// <summary>
    ///  Statistics
    /// </summary>
    Statistics = 10,

    /// <summary>
    ///  Status
    /// </summary>
    Status = 11,

    /// <summary>
    ///  Imbalance
    /// </summary>
    Imbalance = 12,

    /// <summary>
    ///  Ohlcv Eod
    /// </summary>
    OhlcvEod = 13,

    /// <summary>
    ///  Cmbp1
    /// </summary>
    Cmbp1 = 14,

    /// <summary>
    ///  Cbbo1 S
    /// </summary>
    Cbbo1S = 15,

    /// <summary>
    ///  Cbbo1 M
    /// </summary>
    Cbbo1M = 16,

    /// <summary>
    ///  Tcbbo
    /// </summary>
    Tcbbo = 17,

    /// <summary>
    ///  Bbo1 S
    /// </summary>
    Bbo1S = 18,

    /// <summary>
    ///  Bbo1 M
    /// </summary>
    Bbo1M = 19,

    /// <summary>
    ///  The data record schema; no value for a file of mixed schemas
    /// </summary>
    NoValue = 65535,
}
