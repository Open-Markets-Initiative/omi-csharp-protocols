using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Reason the order was reduced or canceled. For an AIQ Cancel message, this value will always be “Q”.
/// </summary>
public struct OrderCancelReason
{
    /// <summary>
    ///  The size of the Order Cancel Reason field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Reason the order was reduced or canceled. For an AIQ Cancel message, this value will always be “Q”.
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
    ///  Returns the wire value of the Order Cancel Reason field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly byte Decode()
        => Byte;

    /// <summary>
    ///  Writes a value into the Order Cancel Reason field.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    ///  The value does not fit its field.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(char value)
    {
        if (value > byte.MaxValue)
        {
            throw new ArgumentOutOfRangeException(nameof(value), global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValueDoesNotFit);
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
