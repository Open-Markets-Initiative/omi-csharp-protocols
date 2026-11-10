namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  HandleInst
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum HandleInst : byte
{
    /// <summary>
    ///  imbalance only (CrossType O and C)
    /// </summary>
    ImbalanceOnly = (byte)'I',

    /// <summary>
    ///  retail order type 1 (CrossType R)
    /// </summary>
    RetailOrderType1 = (byte)'O',

    /// <summary>
    ///  retail order type 2 (CrossType R)
    /// </summary>
    RetailOrderType2 = (byte)'T',

    /// <summary>
    ///  retail price improvement (CrossType R)
    /// </summary>
    RetailPriceImprovement = (byte)'Q',

    /// <summary>
    ///  Extended Life + Continuous (CrossType E)
    /// </summary>
    ExtendedLifeContinuous = (byte)'B',

    /// <summary>
    ///  Direct Listing Capital Raise (CrossType H)
    /// </summary>
    DirectListingCapitalRaise = (byte)'D',
}
