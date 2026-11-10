namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  InterMarket Sweep Eligibility
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum InterMarketSweepEligibility : byte
{
    /// <summary>
    ///  eligible
    /// </summary>
    Eligible = (byte)'Y',

    /// <summary>
    ///  not eligible
    /// </summary>
    NotEligible = (byte)'N',
}
