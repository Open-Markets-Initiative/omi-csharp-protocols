namespace Nasdaq.NsmEquities.Qbbo.Itch.v2_1;

/// <summary>
///  Denotes the Reg SHO Short Sale Price Test Restriction status for the issue at the time of the message dissemination.
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum RegShoAction : byte
{
    /// <summary>
    ///  No price test in place
    /// </summary>
    NoPriceTest = (byte)'0',

    /// <summary>
    ///  Reg SHO Short Sale Price Test Restriction in effect due to an intraday price drop in security
    /// </summary>
    RestrictionInEffect = (byte)'1',

    /// <summary>
    ///  Reg SHO Short Sale Price Test Restriction remains in effect
    /// </summary>
    RestrictionRemains = (byte)'2',
}
