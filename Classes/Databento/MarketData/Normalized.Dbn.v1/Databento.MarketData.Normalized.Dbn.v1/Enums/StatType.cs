namespace Databento.MarketData.Normalized.Dbn.v1;

/// <summary>
///  The type of statistic value contained in the message
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum StatType : ushort
{
    /// <summary>
    ///  Opening Price
    /// </summary>
    OpeningPrice = 1,

    /// <summary>
    ///  Indicative Opening Price
    /// </summary>
    IndicativeOpeningPrice = 2,

    /// <summary>
    ///  Settlement Price
    /// </summary>
    SettlementPrice = 3,

    /// <summary>
    ///  Trading Session Low Price
    /// </summary>
    TradingSessionLowPrice = 4,

    /// <summary>
    ///  Trading Session High Price
    /// </summary>
    TradingSessionHighPrice = 5,

    /// <summary>
    ///  Cleared Volume
    /// </summary>
    ClearedVolume = 6,

    /// <summary>
    ///  Lowest Offer
    /// </summary>
    LowestOffer = 7,

    /// <summary>
    ///  Highest Bid
    /// </summary>
    HighestBid = 8,

    /// <summary>
    ///  Open Interest
    /// </summary>
    OpenInterest = 9,

    /// <summary>
    ///  Fixing Price
    /// </summary>
    FixingPrice = 10,

    /// <summary>
    ///  Close Price
    /// </summary>
    ClosePrice = 11,

    /// <summary>
    ///  Net Change
    /// </summary>
    NetChange = 12,

    /// <summary>
    ///  Vwap
    /// </summary>
    Vwap = 13,

    /// <summary>
    ///  Volatility
    /// </summary>
    Volatility = 14,

    /// <summary>
    ///  Delta
    /// </summary>
    Delta = 15,

    /// <summary>
    ///  Uncrossing Price
    /// </summary>
    UncrossingPrice = 16,

    /// <summary>
    ///  Upper Price Limit
    /// </summary>
    UpperPriceLimit = 17,

    /// <summary>
    ///  Lower Price Limit
    /// </summary>
    LowerPriceLimit = 18,

    /// <summary>
    ///  Block Volume
    /// </summary>
    BlockVolume = 19,

    /// <summary>
    ///  Indicative Close Price
    /// </summary>
    IndicativeClosePrice = 20,

    /// <summary>
    ///  Mwcb Level1
    /// </summary>
    MwcbLevel1 = 21,

    /// <summary>
    ///  Mwcb Level2
    /// </summary>
    MwcbLevel2 = 22,

    /// <summary>
    ///  Mwcb Level3
    /// </summary>
    MwcbLevel3 = 23,

    /// <summary>
    ///  Auction Collar Reference Price
    /// </summary>
    AuctionCollarReferencePrice = 24,

    /// <summary>
    ///  Auction Collar Upper Price
    /// </summary>
    AuctionCollarUpperPrice = 25,

    /// <summary>
    ///  Auction Collar Lower Price
    /// </summary>
    AuctionCollarLowerPrice = 26,

    /// <summary>
    ///  Venue Specific Volume1
    /// </summary>
    VenueSpecificVolume1 = 10001,

    /// <summary>
    ///  Venue Specific Price1
    /// </summary>
    VenueSpecificPrice1 = 10002,
}
