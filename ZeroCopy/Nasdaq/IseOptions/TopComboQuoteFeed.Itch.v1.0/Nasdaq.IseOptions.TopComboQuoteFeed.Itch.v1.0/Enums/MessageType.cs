using System.Runtime.CompilerServices;

namespace Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0;

/// <summary>
///  Code identifying this message type
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum MessageType : byte
{
    /// <summary>
    ///  The system event message type is used to signal a market or data feed handler event.
    /// </summary>
    SystemEventMessage = (byte)'S',

    /// <summary>
    ///  Whenever a complex order is added in the system for an underlying, the order is normalized and results in either the creation of a new complex strategy or is added to an existing strategy. A Complex Strategy Message containing the strategy definition will be sent.
    /// </summary>
    ComplexStrategyDirectoryMessage = (byte)'R',

    /// <summary>
    ///  The options system uses this administrative message to indicate when an strategy has completed the opening process and is now available for auto execution or when the option has closed and is no longer available for auto execution.
    /// </summary>
    StrategyOpenClosedMessage = (byte)'O',

    /// <summary>
    ///  ISE uses this administrative message to indicate the current trading status of a strategy within the ISE Options Market.
    /// </summary>
    StrategyTradingActionMessage = (byte)'H',

    /// <summary>
    ///  The options system uses this administrative message to indicate when an strategy has completed the opening process and is now available for auto execution or when the option has closed and is no longer available for auto execution.
    /// </summary>
    StrategyBestBidAndAskUpdate = (byte)'C',

    /// <summary>
    ///  The options system will continuously calculate its best bid position for active strategies on the options market during the trading day. Whenever the best bid position changes on one side but not the other side, the options system will send its best bid update via this feed for the affected security.
    /// </summary>
    StrategyBestBidUpdate = (byte)'D',

    /// <summary>
    ///  The options system will continuously calculate its best offer position for active strategies on the options market during the trading day. Whenever the best ask position changes on one side but not the other side, the options system will send its best ask update via this feed for the affected security
    /// </summary>
    StrategyBestAskUpdate = (byte)'E',

    /// <summary>
    ///  The ticker message is used to send real time trade information
    /// </summary>
    ComplexStrategyTickerMessage = (byte)'t',

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
