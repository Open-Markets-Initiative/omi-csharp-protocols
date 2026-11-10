using System.Runtime.CompilerServices;

namespace Nasdaq.PsxEquities.TotalView.Itch.v5_0;

/// <summary>
///  Code identifying this message type
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum MessageType : byte
{
    /// <summary>
    ///  Reflects the number of seconds past midnight that the Timestamp message was generated.
    /// </summary>
    SystemEventMessage = (byte)'S',

    /// <summary>
    ///  Market data redistributors should process this message to populate the Financial Status Indicator (required display field) and the Market Category (recommended display field) for NASDAQ-listed issues.
    /// </summary>
    StockDirectoryMessage = (byte)'R',

    /// <summary>
    ///  Nasdaq PSX uses this administrative message to indicate the current trading status of a security to the trading community.
    /// </summary>
    StockTradingActionMessage = (byte)'H',

    /// <summary>
    ///  NASDAQ PSX processes orders based on the most Reg SHO Restriction status value.
    /// </summary>
    RegShoShortSalePriceTestRestrictedIndicatorMessage = (byte)'Y',

    /// <summary>
    ///  Throughout the day, Nasdaq PSX will send out this message only if Nasdaq Operations changes the status of a market participant firm in an issue.
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
    ///  Indicates the auction collar thresholds within which a paused security can reopen following a LULD Trading Pause
    /// </summary>
    LuldAuctionCollarMessage = (byte)'J',

    /// <summary>
    ///  Nasdaq uses this administrative message to indicate the current trading status of the three market centers operated by Nasdaq.
    /// </summary>
    OperationalHaltMessage = (byte)'h',

    /// <summary>
    ///  This message will be generated for unattributed orders accepted by the NASDAQ PSX system.
    /// </summary>
    AddOrderNoMpidAttributionMessage = (byte)'A',

    /// <summary>
    ///  This message will be generated for attributed orders and quotations accepted by the NASDAQ PSX system.
    /// </summary>
    AddOrderWithMpidAttributionMessage = (byte)'F',

    /// <summary>
    ///  This message is sent whenever an order on the book is executed in whole or in part. It is possible to receive several Order Executed Messages for the same order reference number if that order is executed in several parts. The multiple Order Executed Messages on the same order are cumulative.
    /// </summary>
    OrderExecutedMessage = (byte)'E',

    /// <summary>
    ///  This message is sent whenever an order on the book is executed in whole or in part at a price different from the initial display price. Since the execution price is different than the display price of the original Add Order, Nasdaq PSX includes a price field within this execution message.
    /// </summary>
    OrderExecutedWithPriceMessage = (byte)'C',

    /// <summary>
    ///  This message is sent whenever an order on the book is modified as a result of a partial cancellation.
    /// </summary>
    OrderCancelMessage = (byte)'X',

    /// <summary>
    ///  This message is sent whenever an order on the book is being cancelled. All remaining shares are no longer accessible so the order must be removed from the book.
    /// </summary>
    OrderDeleteMessage = (byte)'D',

    /// <summary>
    ///  This message is sent whenever an order on the book has been cancel-replaced. All remaining shares from the original order are no longer accessible, and must be removed. The new order details are provided for the replacement, along with a new order reference number which will be used henceforth. Since the side, stock symbol and attribution (if any) cannot be changed by an Order Replace event, these fields are not included in the message. Firms should retain the side, stock symbol and MPID from the original Add Order message.
    /// </summary>
    OrderReplaceMessage = (byte)'U',

    /// <summary>
    ///  The Trade Message is designed to provide execution details for normal match events involving non-displayable order types. (Note: There is a separate message for Nasdaq PSX cross events.)
    /// </summary>
    TradeMessageNonCross = (byte)'P',

    /// <summary>
    ///  Cross Trade message indicates that Nasdaq PSX has completed its cross process for a specific security. Nasdaq PSX sends out a Cross Trade message for all active issues in the system following the Opening, Closing and EMC cross events. Firms may use the Cross Trade message to determine when the cross for each security has been completed. (Note: For the halted / paused securities, firms should use the Trading Action message to determine when an issue has been released for trading.)
    /// </summary>
    CrossTradeMessage = (byte)'Q',

    /// <summary>
    ///  The Broken Trade Message is sent whenever an execution on Nasdaq PSX is broken. An execution may be broken if it is found to be “clearly erroneous” pursuant to Nasdaq’s Clearly Erroneous Policy. A trade break is final; once a trade is broken, it cannot be reinstated.
    /// </summary>
    BrokenTradeMessage = (byte)'B',

    /// <summary>
    ///  NASDAQ PSX disseminates NOII data at 5-second intervals in the minutes leading up to the NASDAQ Opening Cross and NASDAQ Closing Cross
    /// </summary>
    NetOrderImbalanceIndicatorMessage = (byte)'I',

}

/// <summary>
///  Reads and writes the Message Type field in place.
/// </summary>
public struct MessageTypeField
{
    /// <summary>
    ///  The size of the Message Type field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Code identifying this message type
    /// </summary>
    public MessageType Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Message Type field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly MessageType Decode()
        => (MessageType)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Message Type field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(MessageType value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
