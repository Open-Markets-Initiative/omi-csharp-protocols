namespace Iex.IexEquities.Tops.IexTp.v1_64;

/// <summary>
///  Detail of the Reg. SHO short sale price test restriction status
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum Detail : byte
{
    /// <summary>
    ///  No Price Test In Place
    /// </summary>
    NoPriceTestInPlace = (byte)' ',

    /// <summary>
    ///  Short Sale Price Test Restriction In Effect Due To An Intraday Price Drop In The Security
    /// </summary>
    ShortSalePriceTestRestrictionInEffectDueToAnIntradayPriceDropInTheSecurity = (byte)'A',

    /// <summary>
    ///  Short Sale Price Test Restriction Remains In Effect From Prior Day
    /// </summary>
    ShortSalePriceTestRestrictionRemainsInEffectFromPriorDay = (byte)'C',

    /// <summary>
    ///  Short Sale Price Test Restriction Deactivated
    /// </summary>
    ShortSalePriceTestRestrictionDeactivated = (byte)'D',

    /// <summary>
    ///  Detail Not Available
    /// </summary>
    DetailNotAvailable = (byte)'N',
}
