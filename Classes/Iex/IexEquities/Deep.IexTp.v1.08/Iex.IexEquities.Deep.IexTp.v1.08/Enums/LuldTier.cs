namespace Iex.IexEquities.Deep.IexTp.v1_08;

/// <summary>
///  Indicates which Limit Up-Limit Down price band calculation parameter is to be used
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
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
