namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  Whether every record carries a ts_out trailer, the live gateway send timestamp
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum TsOut : byte
{
    /// <summary>
    ///  No
    /// </summary>
    No = 0,

    /// <summary>
    ///  Yes
    /// </summary>
    Yes = 1,
}
