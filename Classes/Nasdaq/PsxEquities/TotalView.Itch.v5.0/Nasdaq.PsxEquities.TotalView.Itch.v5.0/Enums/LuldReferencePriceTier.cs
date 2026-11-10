namespace Nasdaq.PsxEquities.TotalView.Itch.v5_0;

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
    Tier1 = (byte)'1',

    /// <summary>
    ///  Tier 2 NMS Stocks
    /// </summary>
    Tier2 = (byte)'2',

    /// <summary>
    ///  Not available
    /// </summary>
    Na = (byte)' ',
}
