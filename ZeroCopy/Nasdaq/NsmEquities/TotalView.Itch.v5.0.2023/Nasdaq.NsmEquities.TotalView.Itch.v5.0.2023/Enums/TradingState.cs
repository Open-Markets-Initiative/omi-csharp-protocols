using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

/// <summary>
///  Indicates the current trading state for the stock
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum TradingState : byte
{
    /// <summary>
    ///  Halted across all U.S. equity markets / SROs
    /// </summary>
    Halted = (byte)'H',

    /// <summary>
    ///  Paused across all U.S. equity markets / SROs (NASDAQ-listed securities only)
    /// </summary>
    Paused = (byte)'P',

    /// <summary>
    ///  Quotation only period for cross-SRO halt or pause
    /// </summary>
    QuotationOnlyPeriod = (byte)'Q',

    /// <summary>
    ///  Trading on Nasdaq
    /// </summary>
    Trading = (byte)'T',

}

/// <summary>
///  Reads and writes the Trading State field in place.
/// </summary>
public struct TradingStateField
{
    /// <summary>
    ///  The size of the Trading State field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Indicates the current trading state for the stock
    /// </summary>
    public TradingState Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Trading State field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly TradingState Decode()
        => (TradingState)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Trading State field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(TradingState value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
