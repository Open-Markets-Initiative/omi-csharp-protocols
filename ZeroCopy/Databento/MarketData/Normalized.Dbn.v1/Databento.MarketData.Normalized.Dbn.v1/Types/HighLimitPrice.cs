using System.Runtime.CompilerServices;

namespace Databento.MarketData.Normalized.Dbn.v1;

/// <summary>
///  The high limit price, where every 1 unit corresponds to 1e-9
/// </summary>
public struct HighLimitPrice
{
    /// <summary>
    ///  The value of the High Limit Price field that marks no value.
    /// </summary>
    public const long NoValue = 9223372036854775807;

    /// <summary>
    ///  The number by which the wire value of High Limit Price is divided to give its value.
    /// </summary>
    public const decimal Factor = 1000000000m;

    private const string NotMultiple = "The value is not a multiple of 0.000000001.";

    /// <summary>
    ///  The size of the High Limit Price field in bytes.
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  The high limit price, where every 1 unit corresponds to 1e-9
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
    ///  Gets a value that indicates whether the High Limit Price field holds a value.
    /// </summary>
    public readonly bool HasValue
        => Decode() != NoValue;

    /// <summary>
    ///  Returns the wire value of the High Limit Price field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Decode()
        => global::System.Buffers.Binary.BinaryPrimitives.ReadInt64LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.ReadOnlySpan<long>(in Underlying)));

    /// <summary>
    ///  Writes an unscaled wire value into the High Limit Price field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(long value)
        => global::System.Buffers.Binary.BinaryPrimitives.WriteInt64LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.Span<long>(ref Underlying)), value);

    /// <summary>
    ///  Tries to get the value of the High Limit Price field.
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
    ///  Gets the High Limit Price field with no value.
    /// </summary>
    public static HighLimitPrice Absent => global::System.Runtime.InteropServices.MemoryMarshal.Read<HighLimitPrice>([0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x7F]);
}
