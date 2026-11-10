namespace Nasdaq.NsmEquities.Qbbo.Itch.v2_1;

/// <summary>
///  Indicates which Limit Up / Limit Down price band calculation parameter is to be used for the instrument.
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum LuldReferencePriceTier : byte
{
    /// <summary>
    ///  Tier 1 NMS Stocks and select ETPs
    /// </summary>
    Tier1NmsStocksAndSelectEtPs = (byte)'1',

    /// <summary>
    ///  Tier 2 NMS Stocks
    /// </summary>
    Tier2NmsStocks = (byte)'2',

    /// <summary>
    ///  Not Available
    /// </summary>
    NotAvailable = (byte)' ',
}
