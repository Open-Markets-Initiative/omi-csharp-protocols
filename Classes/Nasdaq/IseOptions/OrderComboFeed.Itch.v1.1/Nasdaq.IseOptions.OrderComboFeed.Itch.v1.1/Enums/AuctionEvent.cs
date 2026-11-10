namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

/// <summary>
///  ‘S’ = Start ‘U’ = Auction Update ‘E’ = End of Auction
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum AuctionEvent : byte
{
    /// <summary>
    ///  Start
    /// </summary>
    Start = (byte)'S',

    /// <summary>
    ///  Auction Update
    /// </summary>
    AuctionUpdate = (byte)'U',

    /// <summary>
    ///  End of Auction
    /// </summary>
    EndOfAuction = (byte)'E',
}
