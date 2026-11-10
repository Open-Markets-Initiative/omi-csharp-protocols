using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

/// <summary>
///  Indicates if the market participant firm qualifies as a Primary Market Maker in accordance with NASDAQ marketplace rules
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum PrimaryMarketMaker : byte
{
    /// <summary>
    ///  Primary market maker
    /// </summary>
    Primary = (byte)'Y',

    /// <summary>
    ///  Non-primary market maker
    /// </summary>
    NonPrimary = (byte)'N',

}

/// <summary>
///  Reads and writes the Primary Market Maker field in place.
/// </summary>
public struct PrimaryMarketMakerField
{
    /// <summary>
    ///  The size of the Primary Market Maker field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Indicates if the market participant firm qualifies as a Primary Market Maker in accordance with NASDAQ marketplace rules
    /// </summary>
    public PrimaryMarketMaker Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Primary Market Maker field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly PrimaryMarketMaker Decode()
        => (PrimaryMarketMaker)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Primary Market Maker field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(PrimaryMarketMaker value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
