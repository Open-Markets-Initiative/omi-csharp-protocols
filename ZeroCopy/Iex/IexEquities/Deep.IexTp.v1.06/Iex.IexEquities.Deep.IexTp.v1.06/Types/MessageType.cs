using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  Code identifying this message type
/// </summary>

public struct MessageType
{
    /// <summary>
    ///  The System Event Message is used to indicate events that apply to the market or the data feed.
    /// </summary>
    public const char SystemEventMessage = 'S';

    /// <summary>
    ///  The System Event Message is used to indicate events that apply to the market or the data feed.
    /// </summary>
    public const char SecurityDirectoryMessage = 'D';

    /// <summary>
    ///  The Trading Status Message is used to indicate the current trading status of a security.
    /// </summary>
    public const char TradingStatusMessage = 'H';

    /// <summary>
    ///  The Exchange may suspend trading of one or more securities on IEX for operational reasons and indicates such operational halt using the Operational Halt Status Message.
    /// </summary>
    public const char OperationalHaltStatusMessage = 'O';

    /// <summary>
    ///  The Short Sale Price Test Message is used to indicate when a short sale price test restriction is in effect for a security.
    /// </summary>
    public const char ShortSalePriceTestStatusMessage = 'P';

    /// <summary>
    ///  The Security Event Message is used to indicate events that apply to a security
    /// </summary>
    public const char SecurityEventMessage = 'E';

    /// <summary>
    ///  Deep broadcasts a real-time Price Level Update Message each time a displayed price level on IEX is updated during the trading day
    /// </summary>
    public const char PriceLevelBuyUpdateMessage = '8';

    /// <summary>
    ///  Deep broadcasts a real-time Price Level Update Message each time a displayed price level on IEX is updated during the trading day
    /// </summary>
    public const char PriceLevelSellUpdateMessage = '5';

    /// <summary>
    ///  Trade Report Messages are sent when an order on the IEX Order Book is executed in whole or in part
    /// </summary>
    public const char TradeReportMessage = 'T';

    /// <summary>
    ///  Official Price Messages are sent for each IEX-listed security to indicate the IEX Official Opening Price and IEX Official Closing Price.
    /// </summary>
    public const char OfficialPriceMessage = 'X';

    /// <summary>
    ///  Trade Break Messages are sent when an execution on IEX is broken on that same trading day
    /// </summary>
    public const char TradeBreakMessage = 'B';

    /// <summary>
    ///  Broadcasts an Auction Information Message every one second between the Lock-in Time and the auction match for Opening and Closing Auctions, and during the Display Only Period for IPO, Halt, and Volatility Auctions.
    /// </summary>
    public const char AuctionInformationMessage = 'A';

    /// <summary>
    ///  Size of MessageType in bytes
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Gets the decoded Message Type value.
    /// </summary>
    public readonly char Value
        => (char)Byte;

    /// <summary>
    ///  Decodes the raw bytes into the Message Type value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly char Decode()
        => (char)Byte;

    /// <summary>
    ///  Encodes a value into the underlying Message Type bytes.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(char value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the string representation of the Message Type value.
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying byte
    /// </summary>
    internal byte Byte;
}
