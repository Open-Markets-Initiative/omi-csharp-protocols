namespace Nasdaq.NtxEquities.TotalView.Itch.v5_0;

/// <summary>
///  Identifies the type of a message contained in Packet by its message type.
/// </summary>
public enum PacketMessageCode : byte
{
    /// <summary>
    ///  The system event message type is used to signal a market or data feed handler event.
    /// </summary>
    SystemEventMessage = (byte)'S',

    /// <summary>
    ///  At the start of each trading day, Nasdaq Texas disseminates stock directory messages for all active symbols in the Nasdaq Texas execution system.
    /// </summary>
    StockDirectoryMessage = (byte)'R',

    /// <summary>
    ///  Nasdaq Texas uses this administrative message to indicate the current trading status of a security to the trading community.
    /// </summary>
    StockTradingActionMessage = (byte)'H',

    /// <summary>
    ///  In February 2011, the Securities and Exchange Commission (SEC) implemented changes to Rule 201 of the Regulation SHO (Reg SHO).
    /// </summary>
    RegShoShortSalePriceTestRestrictedIndicatorMessage = (byte)'Y',

    /// <summary>
    ///  At the start of each trading day, Nasdaq Texas disseminates a spin of market participant position messages.
    /// </summary>
    MarketParticipantPositionMessage = (byte)'L',

    /// <summary>
    ///  Informs data recipients what the daily MWCB breach points are set to for the current trading day.
    /// </summary>
    MwcbDeclineLevelMessage = (byte)'V',

    /// <summary>
    ///  Informs data recipients when a MWCB has breached one of the established levels
    /// </summary>
    MwcbStatusMessage = (byte)'W',

    /// <summary>
    ///  Indicates the auction collar thresholds within which a paused security can reopen following a LULD Trading Pause
    /// </summary>
    LuldAuctionCollarMessage = (byte)'J',

    /// <summary>
    ///  The Exchange uses this message to indicate the current Operational Status of a security to the trading community.
    /// </summary>
    OperationalHaltMessage = (byte)'h',

    /// <summary>
    ///  This message will be generated for unattributed orders accepted by the Nasdaq Texas system.
    /// </summary>
    AddOrderNoMpidAttributionMessage = (byte)'A',

    /// <summary>
    ///  This message will be generated for attributed orders and quotations accepted by the Nasdaq Texas system.
    /// </summary>
    AddOrderMpidAttributionMessage = (byte)'F',

    /// <summary>
    ///  This message is sent whenever an order on the book is executed in whole or in part.
    /// </summary>
    OrderExecutedMessage = (byte)'E',

    /// <summary>
    ///  This message is sent whenever an order on the book is executed in whole or in part at a price different from the initial display price.
    /// </summary>
    OrderExecutedWithPriceMessage = (byte)'C',

    /// <summary>
    ///  This message is sent whenever an order on the book is modified as a result of a partial cancellation
    /// </summary>
    OrderCancelMessage = (byte)'X',

    /// <summary>
    ///  This message is sent whenever an order on the book is being cancelled. All remaining shares are no longer accessible so the order must be removed from the book.
    /// </summary>
    OrderDeleteMessage = (byte)'D',

    /// <summary>
    ///  This message is sent whenever an order on the book has been cancel-replaced.
    /// </summary>
    OrderReplaceMessage = (byte)'U',

    /// <summary>
    ///  The Trade Message is designed to provide execution details for normal match events involving non-displayable order types.
    /// </summary>
    NonCrossTradeMessage = (byte)'P',

    /// <summary>
    ///  Cross Trade message indicates that Nasdaq Texas has completed its cross process for a specific security.
    /// </summary>
    CrossTradeMessage = (byte)'Q',

    /// <summary>
    ///  The Broken Trade Message is sent whenever an execution on Nasdaq Texas is broken.
    /// </summary>
    BrokenTradeMessage = (byte)'B',

    /// <summary>
    ///  Nasdaq Texas disseminates NOII data at 5-second intervals in the minutes leading up to the Nasdaq Opening Cross and Nasdaq Closing Cross.
    /// </summary>
    NetOrderImbalanceIndicatorMessage = (byte)'I',

    /// <summary>
    ///  Indicates price improving interest of the Bid, Ask or both the Bid and Ask for a security.
    /// </summary>
    RetailInterestMessage = (byte)'N',
}
