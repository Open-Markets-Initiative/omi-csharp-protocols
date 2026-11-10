using System.Runtime.CompilerServices;

namespace Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0;

/// <summary>
///  &lt;space&gt; = regular quote/autox eligible “X” = Pre-Open/Halted
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum QuoteCondition : byte
{
    /// <summary>
    ///  Regular Quote Auto Execution Eligible
    /// </summary>
    RegularQuote = (byte)' ',

    /// <summary>
    ///  Halsted or Preopen
    /// </summary>
    Halted = (byte)'X',

}

/// <summary>
///  Reads and writes the Quote Condition field in place.
/// </summary>
public struct QuoteConditionField
{
    /// <summary>
    ///  The size of the Quote Condition field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  &lt;space&gt; = regular quote/autox eligible “X” = Pre-Open/Halted
    /// </summary>
    public QuoteCondition Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Quote Condition field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly QuoteCondition Decode()
        => (QuoteCondition)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Quote Condition field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(QuoteCondition value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
