namespace Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
///  Indicates which Limit Up-Limit Down price band calculation parameter is to be used
/// </summary>
/// <remarks>
///  Backed by byte. Values correspond to the wire-format integer encoding.
/// </remarks>
public enum LuldTier : byte
{
    /// <summary>
    ///  Not Applicable
    /// </summary>
    NotApplicable = 0,

    /// <summary>
    ///  Tier 1 Nms Stock
    /// </summary>
    Tier1NmsStock = 1,

    /// <summary>
    ///  Tier 2 Nms Stock
    /// </summary>
    Tier2NmsStock = 2,
}
