using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Qbbo.Itch.v2_1;

/// <summary>
///  Code identifying this message type
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum MessageType : byte
{
    /// <summary>
    ///  The System Event message is used to signal key market or data feed control events.
    /// </summary>
    SystemEventMessage = (byte)'S',

    /// <summary>
    ///  At the start of each trading day, Nasdaq disseminates stock directory messages for all active symbols in its market center system.
    /// </summary>
    StockDirectoryMessage = (byte)'R',

    /// <summary>
    ///  Nasdaq uses this administrative message to indicate the current trading status of a security to the trading community.
    /// </summary>
    StockTradingActionMessage = (byte)'H',

    /// <summary>
    ///  Nasdaq introduced the Reg SHO Short Sale Price Test Restricted Indicator message format.
    /// </summary>
    RegShoRestrictionMessage = (byte)'Y',

    /// <summary>
    ///  Informs data recipients what the daily MWCB breach points are set to for the current trading day.
    /// </summary>
    MwcbDeclineLevelMessage = (byte)'V',

    /// <summary>
    ///  Informs data recipients when a MWCB has breached one of the established levels
    /// </summary>
    MwcbBreachMessage = (byte)'W',

    /// <summary>
    ///  The Exchange uses this message to indicate the current Operational Status of a security to the trading community.
    /// </summary>
    OperationalHaltMessage = (byte)'h',

    /// <summary>
    ///  BBO products will broadcast a real-time update every time that the exchange's best bid and offer quote is updated during the trading day.
    /// </summary>
    BboQuotationMessage = (byte)'Q',

    /// <summary>
    ///  Indicates a retail interest indication of the Bid, Ask or both the Bid and Ask for Nasdaq-listed securities.
    /// </summary>
    PriceImprovementMessage = (byte)'N',

    /// <summary>
    ///  Indicates the anticipated IPO quotation release time of a security.
    /// </summary>
    IpoQuotingPeriodUpdateMessage = (byte)'K',

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
