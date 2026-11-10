using System.Runtime.CompilerServices;

namespace Nasdaq.NtxEquities.TotalView.Itch.v5_0;

/// <summary>
///  Identifies the applicable Nasdaq market center
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum MarketCode : byte
{
    /// <summary>
    ///  Nasdaq
    /// </summary>
    Nasdaq = (byte)'Q',

    /// <summary>
    ///  NASDAQ TEXAS
    /// </summary>
    NasdaqTexas = (byte)'B',

    /// <summary>
    ///  PSX
    /// </summary>
    Psx = (byte)'X',

}

/// <summary>
///  Reads and writes the Market Code field in place.
/// </summary>
public struct MarketCodeField
{
    /// <summary>
    ///  The size of the Market Code field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Identifies the applicable Nasdaq market center
    /// </summary>
    public MarketCode Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Market Code field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly MarketCode Decode()
        => (MarketCode)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Market Code field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(MarketCode value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
