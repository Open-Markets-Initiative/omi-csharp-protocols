namespace Iex.IexEquities.Tops.IexTp.v1_64;

/// <summary>
///  Auction type identifier
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum AuctionType : byte
{
    /// <summary>
    ///  Opening Auction
    /// </summary>
    OpeningAuction = (byte)'O',

    /// <summary>
    ///  Closing Auction
    /// </summary>
    ClosingAuction = (byte)'C',

    /// <summary>
    ///  Ipo Auction
    /// </summary>
    IpoAuction = (byte)'I',

    /// <summary>
    ///  Halt Auction
    /// </summary>
    HaltAuction = (byte)'H',

    /// <summary>
    ///  Volatility Auction
    /// </summary>
    VolatilityAuction = (byte)'V',
}
