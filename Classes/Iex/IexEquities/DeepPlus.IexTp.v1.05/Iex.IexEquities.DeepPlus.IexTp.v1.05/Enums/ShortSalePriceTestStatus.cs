namespace Iex.IexEquities.DeepPlus.IexTp.v1_05;

/// <summary>
///  Reg. SHO short sale price test restriction status
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum ShortSalePriceTestStatus : byte
{
    /// <summary>
    ///  Not In Effect
    /// </summary>
    NotInEffect = 0,

    /// <summary>
    ///  In Effect
    /// </summary>
    InEffect = 1,
}
