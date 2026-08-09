using System.Runtime.CompilerServices;

namespace Iex.IexEquities.DeepPlus.IexTp;

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
    ///  broadcasts a real-time Retail Liquidity Indicator Message each time there is an update to IEX's eligible retail liquidity interest during the trading day
    /// </summary>
    public const char RetailLiquidityIndicatorMessage = 'I';

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
    ///  A displayed order that has been added to the IEX Book
    /// </summary>
    public const char AddOrderMessage = 'a';

    /// <summary>
    ///  A displayed order that had its Price, Size, or Priority component changed as a result of user or system action
    /// </summary>
    public const char OrderModifyMessage = 'M';

    /// <summary>
    ///  A displayed order that was removed from the IEX Book
    /// </summary>
    public const char OrderDeleteMessage = 'R';

    /// <summary>
    ///  A displayed order that was executed against
    /// </summary>
    public const char OrderExecutedMessage = 'L';

    /// <summary>
    ///  A non-displayed order on the book that executed against another non-displayed order on the book
    /// </summary>
    public const char TradeMessage = 'T';

    /// <summary>
    ///  Trade Break Messages are sent when an execution on IEX is broken on that same trading day
    /// </summary>
    public const char TradeBreakMessage = 'B';

    /// <summary>
    ///  This message is used to indicate that the IEX Book for a symbol has been cleared of all orders
    /// </summary>
    public const char ClearBookMessage = 'C';

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
