using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

/// <summary>
///  The type of order being added
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum BuySellIndicator : byte
{
    /// <summary>
    ///  Buy order
    /// </summary>
    Buy = (byte)'B',

    /// <summary>
    ///  Sell order
    /// </summary>
    Sell = (byte)'S',

}

/// <summary>
///  Reads and writes the Buy Sell Indicator field in place.
/// </summary>
public struct BuySellIndicatorField
{
    /// <summary>
    ///  The size of the Buy Sell Indicator field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  The type of order being added
    /// </summary>
    public BuySellIndicator Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Buy Sell Indicator field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly BuySellIndicator Decode()
        => (BuySellIndicator)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Buy Sell Indicator field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(BuySellIndicator value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
