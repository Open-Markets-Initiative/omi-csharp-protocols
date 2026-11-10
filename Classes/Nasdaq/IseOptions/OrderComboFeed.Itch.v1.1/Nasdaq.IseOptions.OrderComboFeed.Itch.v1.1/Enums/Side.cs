namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

/// <summary>
///  ‘B’ = Bid ‘A’ = Offer (Ask) ‘ ’ (blank) = not set (hidden)
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum Side : byte
{
    /// <summary>
    ///  Bid
    /// </summary>
    Bid = (byte)'B',

    /// <summary>
    ///  Offer (Ask)
    /// </summary>
    OfferAsk = (byte)'A',

    /// <summary>
    ///  Hidden
    /// </summary>
    Hidden = (byte)' ',
}
