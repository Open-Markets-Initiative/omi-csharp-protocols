using System.Runtime.CompilerServices;

namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

/// <summary>
///  Denotes the explicit strike price of the option. Refer to Data Types for field processing notes.
/// </summary>
public struct ExplicitStrikePrice
{
    /// <summary>
    ///  The number by which the wire value of Explicit Strike Price is divided to give its value.
    /// </summary>
    public const decimal Factor = 100000000m;

    private const string NotMultiple = "The value is not a multiple of 0.00000001.";

    /// <summary>
    ///  The size of the Explicit Strike Price field in bytes.
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Denotes the explicit strike price of the option. Refer to Data Types for field processing notes.
    /// </summary>
    /// <remarks>
    ///  The value is <see cref="Decode"/> divided by <see cref="Factor"/>.
    /// </remarks>
    /// <exception cref="ArgumentException">
    ///  The value is not a multiple of 0.00000001.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  The value does not fit its field.
    /// </exception>
    public decimal Value
    {
        readonly get => (decimal)Decode() / Factor;
        set
        {
            var units = value * Factor;
            if (units != decimal.Truncate(units) || units / Factor != value)
            {
                throw new ArgumentException(NotMultiple, nameof(value));
            }
            Encode(checked((long)units));
        }
    }

    /// <summary>
    ///  Returns the wire value of the Explicit Strike Price field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Decode()
        => global::System.Buffers.Binary.BinaryPrimitives.ReadInt64BigEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.ReadOnlySpan<long>(in Underlying)));

    /// <summary>
    ///  Writes an unscaled wire value into the Explicit Strike Price field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(long value)
        => global::System.Buffers.Binary.BinaryPrimitives.WriteInt64BigEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.Span<long>(ref Underlying)), value);

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal long Underlying;
}
