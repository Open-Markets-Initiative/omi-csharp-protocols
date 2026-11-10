using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Tops.IexTp.v1_64;

/// <summary>
///  System event identifier
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum SystemEvent : byte
{
    /// <summary>
    ///  This Message Indicates That Iex Is Open And Ready To Start Accepting Orders
    /// </summary>
    StartOfSystemHours = (byte)'S',

    /// <summary>
    ///  This Message Indicates That Day And Gtx Orders As Well As Market Orders And Pegged Orders Are Available For Execution On Iex
    /// </summary>
    StartOfRegularMarketHours = (byte)'R',

    /// <summary>
    ///  This Message Indicates That Day Orders Market Orders And Pegged Orders Are No Longer Accepted By Iex
    /// </summary>
    EndOfRegularMarketHours = (byte)'M',

    /// <summary>
    ///  This Message Indicates That Iex Is Now Closed And Will Not Accept Any New Orders During This Trading Session
    /// </summary>
    EndOfSystemHours = (byte)'E',

    /// <summary>
    ///  This Is Always The Last Message Sent In Any Trading Session
    /// </summary>
    EndOfMessages = (byte)'C',

}

/// <summary>
///  Reads and writes the System Event field in place.
/// </summary>
public struct SystemEventField
{
    /// <summary>
    ///  The size of the System Event field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  System event identifier
    /// </summary>
    public SystemEvent Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the System Event field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly SystemEvent Decode()
        => (SystemEvent)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the System Event field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(SystemEvent value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
