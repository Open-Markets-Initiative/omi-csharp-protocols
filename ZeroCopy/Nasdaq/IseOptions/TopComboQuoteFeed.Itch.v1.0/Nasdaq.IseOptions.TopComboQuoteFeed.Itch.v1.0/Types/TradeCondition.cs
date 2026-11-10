using System.Runtime.CompilerServices;

namespace Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0;

/// <summary>
///  Same value as the Trade Condition sent to OPRA for this trade. To obtain a list of Trade Conditions, refer to the NOTES below.
/// </summary>
public struct TradeCondition
{
    /// <summary>
    ///  The size of the Trade Condition field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Same value as the Trade Condition sent to OPRA for this trade. To obtain a list of Trade Conditions, refer to the NOTES below.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    ///  The value does not fit its field.
    /// </exception>
    public char Value
    {
        readonly get => (char)Byte;
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Trade Condition field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly byte Decode()
        => Byte;

    /// <summary>
    ///  Writes a value into the Trade Condition field.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    ///  The value does not fit its field.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(char value)
    {
        if (value > byte.MaxValue)
        {
            throw new ArgumentOutOfRangeException(nameof(value), global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterValueDoesNotFit);
        }
        Byte = (byte)value;
    }

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    internal byte Byte;
}
