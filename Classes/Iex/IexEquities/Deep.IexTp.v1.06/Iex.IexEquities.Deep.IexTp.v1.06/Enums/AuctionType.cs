namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  Auction type identifier
/// </summary>
/// <remarks>
///  Each value corresponds to the ASCII byte value used as the wire-format discriminator.
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
