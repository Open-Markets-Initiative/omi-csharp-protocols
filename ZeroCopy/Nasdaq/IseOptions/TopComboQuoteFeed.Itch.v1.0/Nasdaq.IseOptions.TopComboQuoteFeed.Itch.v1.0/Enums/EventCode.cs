using System.Runtime.CompilerServices;

namespace Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0;

/// <summary>
///  Refer to System Event Codes below
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum EventCode : byte
{
    /// <summary>
    ///  Start of Messages
    /// </summary>
    StartOfMessages = (byte)'O',

    /// <summary>
    ///  Start of System Hours
    /// </summary>
    StartOfSystemHours = (byte)'S',

    /// <summary>
    ///  Start of Opening Process
    /// </summary>
    StartOfOpeningProcess = (byte)'Q',

    /// <summary>
    ///  Start of Normal Hours Closing Process
    /// </summary>
    StartOfNormalHoursClosingProcess = (byte)'N',

    /// <summary>
    ///  Start of Late Hours Closing Process
    /// </summary>
    StartOfLateHoursClosingProcess = (byte)'L',

    /// <summary>
    ///  End of System Hours
    /// </summary>
    EndOfSystemHours = (byte)'E',

    /// <summary>
    ///  End of Messages
    /// </summary>
    EndOfMessages = (byte)'C',

    /// <summary>
    ///  End of WCO Early closing
    /// </summary>
    EndOfWcoEarlyClosing = (byte)'W',

}

/// <summary>
///  Reads and writes the Event Code field in place.
/// </summary>
public struct EventCodeField
{
    /// <summary>
    ///  The size of the Event Code field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Refer to System Event Codes below
    /// </summary>
    public EventCode Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Event Code field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly EventCode Decode()
        => (EventCode)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Event Code field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(EventCode value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
