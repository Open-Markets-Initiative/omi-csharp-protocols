using System.Runtime.CompilerServices;

namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  Reserved for future use
/// </summary>
public struct IndMatchPrice
{
    /// <summary>
    ///  The value of the Ind Match Price field that marks no value.
    /// </summary>
    public const long NoValue = 9223372036854775807;

    /// <summary>
    ///  The number by which the wire value of Ind Match Price is divided to give its value.
    /// </summary>
    public const decimal Factor = 1000000000m;

    private const string NotMultiple = "The value is not a multiple of 0.000000001.";

    /// <summary>
    ///  The size of the Ind Match Price field in bytes.
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Reserved for future use
    /// </summary>
    /// <remarks>
    ///  The value is <see cref="Decode"/> divided by <see cref="Factor"/>.
    /// </remarks>
    /// <exception cref="ArgumentException">
    ///  The value is not a multiple of 0.000000001.
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
    ///  Gets a value that indicates whether the Ind Match Price field holds a value.
    /// </summary>
    public readonly bool HasValue
        => Decode() != NoValue;

    /// <summary>
    ///  Returns the wire value of the Ind Match Price field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Decode()
        => global::System.Buffers.Binary.BinaryPrimitives.ReadInt64LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.ReadOnlySpan<long>(in Underlying)));

    /// <summary>
    ///  Writes an unscaled wire value into the Ind Match Price field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(long value)
        => global::System.Buffers.Binary.BinaryPrimitives.WriteInt64LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.Span<long>(ref Underlying)), value);

    /// <summary>
    ///  Tries to get the value of the Ind Match Price field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryGetValue(out decimal value)
    {
        if (HasValue)
        {
            value = Value;
            return true;
        }

        value = default;
        return false;
    }

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => TryGetValue(out var value) ? string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{value}") : "Not Applicable";

    internal long Underlying;

    /// <summary>
    ///  Gets the Ind Match Price field with no value.
    /// </summary>
    public static IndMatchPrice Absent => global::System.Runtime.InteropServices.MemoryMarshal.Read<IndMatchPrice>([0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x7F]);
}
