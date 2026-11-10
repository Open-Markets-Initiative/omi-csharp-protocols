using System.Runtime.CompilerServices;

namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

/// <summary>
///  StrategyType value
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum StrategyType : byte
{
    /// <summary>
    ///  Vertical Spread
    /// </summary>
    VerticalSpread = (byte)'V',

    /// <summary>
    ///  Time Spread
    /// </summary>
    TimeSpread = (byte)'T',

    /// <summary>
    ///  Diagonal Spread
    /// </summary>
    DiagonalSpread = (byte)'D',

    /// <summary>
    ///  Straddle
    /// </summary>
    Straddle = (byte)'S',

    /// <summary>
    ///  Strangle
    /// </summary>
    Strangle = (byte)'G',

    /// <summary>
    ///  Combo
    /// </summary>
    Combo = (byte)'C',

    /// <summary>
    ///  Risk Reversal
    /// </summary>
    RiskReversal = (byte)'R',

    /// <summary>
    ///  Ratio Spread
    /// </summary>
    RatioSpread = (byte)'A',

    /// <summary>
    ///  Custom
    /// </summary>
    Custom = (byte)'U',

}

/// <summary>
///  Reads and writes the Strategy Type field in place.
/// </summary>
public struct StrategyTypeField
{
    /// <summary>
    ///  The size of the Strategy Type field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  StrategyType value
    /// </summary>
    public StrategyType Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Strategy Type field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly StrategyType Decode()
        => (StrategyType)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Strategy Type field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(StrategyType value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
