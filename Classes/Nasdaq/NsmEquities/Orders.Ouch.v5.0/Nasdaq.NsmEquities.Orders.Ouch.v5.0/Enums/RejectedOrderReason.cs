namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  The reason the order was rejected.
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum RejectedOrderReason : ushort
{
    /// <summary>
    ///  Quote Unavailable
    /// </summary>
    QuoteUnavailable = 0x0001,

    /// <summary>
    ///  Destination Closed
    /// </summary>
    DestinationClosed = 0x0002,

    /// <summary>
    ///  Invalid Display
    /// </summary>
    InvalidDisplay = 0x0003,

    /// <summary>
    ///  Invalid Max Floor
    /// </summary>
    InvalidMaxFloor = 0x0004,

    /// <summary>
    ///  Invalid Peg Type
    /// </summary>
    InvalidPegType = 0x0005,

    /// <summary>
    ///  Fat Finger
    /// </summary>
    FatFinger = 0x0006,

    /// <summary>
    ///  Halted
    /// </summary>
    Halted = 0x0007,

    /// <summary>
    ///  ISO Not Allowed
    /// </summary>
    IsoNotAllowed = 0x0008,

    /// <summary>
    ///  Invalid Side
    /// </summary>
    InvalidSide = 0x0009,

    /// <summary>
    ///  Processing Error
    /// </summary>
    ProcessingError = 0x000A,

    /// <summary>
    ///  Cancel Pending
    /// </summary>
    CancelPending = 0x000B,

    /// <summary>
    ///  Firm Not Authorized
    /// </summary>
    FirmNotAuthorized = 0x000C,

    /// <summary>
    ///  Invalid Min Quantity
    /// </summary>
    InvalidMinQuantity = 0x000D,

    /// <summary>
    ///  No Closing Reference Price
    /// </summary>
    NoClosingReferencePrice = 0x000E,

    /// <summary>
    ///  Other
    /// </summary>
    Other = 0x000F,

    /// <summary>
    ///  Cancel Not Allowed
    /// </summary>
    CancelNotAllowed = 0x0010,

    /// <summary>
    ///  Pegging Not Allowed
    /// </summary>
    PeggingNotAllowed = 0x0011,

    /// <summary>
    ///  Crossed Market
    /// </summary>
    CrossedMarket = 0x0012,

    /// <summary>
    ///  Invalid Quantity
    /// </summary>
    InvalidQuantity = 0x0013,

    /// <summary>
    ///  Invalid Cross Order
    /// </summary>
    InvalidCrossOrder = 0x0014,

    /// <summary>
    ///  Replace Not Allowed
    /// </summary>
    ReplaceNotAllowed = 0x0015,

    /// <summary>
    ///  Routing Not Allowed
    /// </summary>
    RoutingNotAllowed = 0x0016,

    /// <summary>
    ///  Invalid Symbol
    /// </summary>
    InvalidSymbol = 0x0017,

    /// <summary>
    ///  Test
    /// </summary>
    Test = 0x0018,

    /// <summary>
    ///  Late LOC Too Aggressive
    /// </summary>
    LateLocTooAggressive = 0x0019,

    /// <summary>
    ///  Retail Not Allowed
    /// </summary>
    RetailNotAllowed = 0x001A,

    /// <summary>
    ///  Invalid Midpoint Post Only Price
    /// </summary>
    InvalidMidpointPostOnlyPrice = 0x001B,

    /// <summary>
    ///  Invalid Destination
    /// </summary>
    InvalidDestination = 0x001C,

    /// <summary>
    ///  Invalid Price
    /// </summary>
    InvalidPrice = 0x001D,

    /// <summary>
    ///  Shares Exceed Threshold
    /// </summary>
    SharesExceedThreshold = 0x001E,

    /// <summary>
    ///  Exceeds Maximum Allowed Notional Value
    /// </summary>
    ExceedsMaximumAllowedNotionalValue = 0x001F,

    /// <summary>
    ///  Risk: Aggregate Exposure Exceeded
    /// </summary>
    RiskAggregateExposureExceeded = 0x0020,

    /// <summary>
    ///  Risk: Market Impact
    /// </summary>
    RiskMarketImpact = 0x0021,

    /// <summary>
    ///  Risk: Restricted Stock
    /// </summary>
    RiskRestrictedStock = 0x0022,

    /// <summary>
    ///  Risk: Short Sell Restricted
    /// </summary>
    RiskShortSellRestricted = 0x0023,

    /// <summary>
    ///  Risk: ISO Not Allowed.
    /// </summary>
    RiskIsoNotAllowed = 0x0024,

    /// <summary>
    ///  Risk: Exceeds ADV Limit
    /// </summary>
    RiskExceedsAdvLimit = 0x0025,

    /// <summary>
    ///  Risk: Fat Finger
    /// </summary>
    RiskFatFinger = 0x0026,

    /// <summary>
    ///  Risk: Locate Required
    /// </summary>
    RiskLocateRequired = 0x0027,

    /// <summary>
    ///  Risk: Symbol Message Rate Restriction
    /// </summary>
    RiskSymbolMessageRateRestriction = 0x0028,

    /// <summary>
    ///  Risk: Port Message Rate Restriction
    /// </summary>
    RiskPortMessageRateRestriction = 0x0029,

    /// <summary>
    ///  Risk: Duplicate Message Rate Restriction
    /// </summary>
    RiskDuplicateMessageRateRestriction = 0x002A,

    /// <summary>
    ///  Risk: Short Sell Not Allowed
    /// </summary>
    RiskShortSellNotAllowed = 0x002B,

    /// <summary>
    ///  Risk: Market Order Not Allowed
    /// </summary>
    RiskMarketOrderNotAllowed = 0x002C,

    /// <summary>
    ///  Risk: Pre-Market Not Allowed
    /// </summary>
    RiskPreMarketNotAllowed = 0x002D,

    /// <summary>
    ///  Risk: Post-Market Not Allowed
    /// </summary>
    RiskPostMarketNotAllowed = 0x002E,

    /// <summary>
    ///  Risk: Short Sell Exempt Not Allowed
    /// </summary>
    RiskShortSellExemptNotAllowed = 0x002F,

    /// <summary>
    ///  Risk: Single Order Notional Exceeded
    /// </summary>
    RiskSingleOrderNotionalExceeded = 0x0030,

    /// <summary>
    ///  Risk: Max Quantity Exceeded
    /// </summary>
    RiskMaxQuantityExceeded = 0x0031,

    /// <summary>
    ///  Reg SHO State Not Available
    /// </summary>
    RegShoStateNotAvailable = 0x0032,

    /// <summary>
    ///  Risk: IPO Market Buy Not Allowed
    /// </summary>
    RiskIpoMarketBuyNotAllowed = 0x0033,

    /// <summary>
    ///  Invalid AIQ
    /// </summary>
    InvalidAiq = 0x0040,
}
