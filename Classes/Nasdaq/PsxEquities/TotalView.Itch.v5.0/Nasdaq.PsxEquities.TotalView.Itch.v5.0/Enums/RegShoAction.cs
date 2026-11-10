namespace Nasdaq.PsxEquities.TotalView.Itch.v5_0;

/// <summary>
///  Denotes the Reg SHO Short Sale Price Test Restriction status for the issue at the time of the message dissemination
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
    ///  Reg SHO Short Sale Price Test Restriction in effect due to an intra-day price drop in security
    /// </summary>
    RegShoShortSalePriceTestRestriction = (byte)'1',

    /// <summary>
    ///  Reg SHO Short Sale Price Test Restriction remains in effect
    /// </summary>
    TestRestrictionRemains = (byte)'2',
}
