namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  Reg. SHO short sale price test restriction status
/// </summary>

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
