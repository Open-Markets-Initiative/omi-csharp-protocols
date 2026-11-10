using System.Runtime.CompilerServices;

namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  The leg delta, where every 1 unit corresponds to 1e-9
/// </summary>
public struct LegDelta
{
    /// <summary>
    ///  The value of the Leg Delta field that marks no value.
    /// </summary>
    public const long NoValue = 9223372036854775807;

    /// <summary>
    ///  The number by which the wire value of Leg Delta is divided to give its value.
    /// </summary>
    public const decimal Factor = 1000000000m;

    private const string NotMultiple = "The value is not a multiple of 0.000000001.";

    /// <summary>
    ///  The size of the Leg Delta field in bytes.
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  The leg delta, where every 1 unit corresponds to 1e-9
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
    ///  Gets a value that indicates whether the Leg Delta field holds a value.
    /// </summary>
    public readonly bool HasValue
        => Decode() != NoValue;

    /// <summary>
    ///  Returns the wire value of the Leg Delta field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Decode()
        => global::System.Buffers.Binary.BinaryPrimitives.ReadInt64LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.ReadOnlySpan<long>(in Underlying)));

    /// <summary>
    ///  Writes an unscaled wire value into the Leg Delta field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(long value)
        => global::System.Buffers.Binary.BinaryPrimitives.WriteInt64LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.Span<long>(ref Underlying)), value);

    /// <summary>
    ///  Tries to get the value of the Leg Delta field.
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
    ///  Gets the Leg Delta field with no value.
    /// </summary>
    public static LegDelta Absent => global::System.Runtime.InteropServices.MemoryMarshal.Read<LegDelta>([0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x7F]);
}
