namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

/// <summary>
///  Identifies the type of a message contained in Sequenced Message or Packet or File by its message type.
/// </summary>
public enum MessageCode : byte
{
    /// <summary>
    ///  The system event message type is used to signal a market or data feed handler event
    /// </summary>
    SystemEventMessage = (byte)'S',

    /// <summary>
    ///  Market data redistributors should process this message to populate the Financial Status Indicator (required display field) and the Market Category (recommended display field) for NASDAQ-listed issues.
    /// </summary>
    StockDirectoryMessage = (byte)'R',

    /// <summary>
    ///  Nasdaq uses this administrative message to indicate the current trading status of a security to the trading community
    /// </summary>
    StockTradingActionMessage = (byte)'H',

    /// <summary>
    ///  Nasaq processes orders based on the most Reg SHO Restriction status value
    /// </summary>
    RegShoShortSalePriceTestRestrictedIndicatorMessage = (byte)'Y',

    /// <summary>
    ///  Nasdaq will send out this message only if Nasdaq Operations changes the status of a market participant firm in an issue
    /// </summary>
    MarketParticipantPositionMessage = (byte)'L',

    /// <summary>
    ///  Informs data recipients what the daily MWCB breach points are set to for the current trading day.
    /// </summary>
    MwcbDeclineLevelMessage = (byte)'V',

    /// <summary>
    ///  Informs data recipients when a MWCB has breached one of the established levels
    /// </summary>
    MwcbStatusLevelMessage = (byte)'W',

    /// <summary>
    ///  Indicates the anticipated IPO quotation release time of a security.
    /// </summary>
    IpoQuotingPeriodUpdate = (byte)'K',

    /// <summary>
    ///  Indicates the auction collar thresholds within which a paused security can reopen following a LULD trading pause
    /// </summary>
    LuldAuctionCollarMessage = (byte)'J',

    /// <summary>
    ///  An Operational Halt means that there has been an interruption of service on the identified security impacting only the designated Market Center
    /// </summary>
    OperationalHaltMessage = (byte)'h',

    /// <summary>
    ///  This message will be generated for unattributed orders accepted by the Nasdaq system
    /// </summary>
    AddOrderNoMpidAttributionMessage = (byte)'A',

    /// <summary>
    ///  This message will be generated for attributed orders and quotations accepted by the Nasdaq system
    /// </summary>
    AddOrderWithMpidAttributionMessage = (byte)'F',

    /// <summary>
    ///  This message is sent whenever an order on the book is executed in whole or in part
    /// </summary>
    OrderExecutedMessage = (byte)'E',

    /// <summary>
    ///  This message is sent whenever an order on the book is executed in whole or in part at a price different from the initial display price
    /// </summary>
    OrderExecutedWithPriceMessage = (byte)'C',

    /// <summary>
    ///  This message is sent whenever an order on the book is modified as a result of a partial cancellation
    /// </summary>
    OrderCancelMessage = (byte)'X',

    /// <summary>
    ///  This message is sent whenever an order on the book is being cancelled
    /// </summary>
    OrderDeleteMessage = (byte)'D',

    /// <summary>
    ///  This message is sent whenever an order on the book has been cancel-replaced
    /// </summary>
    OrderReplaceMessage = (byte)'U',

    /// <summary>
    ///  Trade Messages should be included in NASDAQ time-and-sales displays as well as volume and other market statistics
    /// </summary>
    NonCrossTradeMessage = (byte)'P',

    /// <summary>
    ///  Cross Trade message indicates that Nasdaq has completed its cross process for a specific security
    /// </summary>
    CrossTradeMessage = (byte)'Q',

    /// <summary>
    ///  The Broken Trade Message is sent whenever an execution on Nasdaq is broken
    /// </summary>
    BrokenTradeMessage = (byte)'B',

    /// <summary>
    ///  NASDAQ disseminates NOII data
    /// </summary>
    NetOrderImbalanceIndicatorMessage = (byte)'I',

    /// <summary>
    ///  Identifies a retail interest indication of the Bid, Ask or both the Bid and Ask for NASDAQ-listed securities.
    /// </summary>
    RetailPriceImprovementIndicatorMessage = (byte)'N',

    /// <summary>
    ///  This message is disseminated only for Direct Listing with Capital Raise (DLCR) securities
    /// </summary>
    DirectListingWithCapitalRaisePriceDiscoveryMessage = (byte)'O',
}
