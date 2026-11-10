using System.Runtime.CompilerServices;

namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  The value for non-price statistics
/// </summary>
public struct Quantity
{
    /// <summary>
    ///  The value of the Quantity field that marks no value.
    /// </summary>
    public const long NoValue = 9223372036854775807;

    /// <summary>
    ///  The size of the Quantity field in bytes.
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  The value for non-price statistics
    /// </summary>
    public long Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Gets a value that indicates whether the Quantity field holds a value.
    /// </summary>
    public readonly bool HasValue
        => Decode() != NoValue;

    /// <summary>
    ///  Returns the wire value of the Quantity field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Decode()
        => global::System.Buffers.Binary.BinaryPrimitives.ReadInt64LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.ReadOnlySpan<long>(in Underlying)));

    /// <summary>
    ///  Writes an unscaled wire value into the Quantity field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(long value)
        => global::System.Buffers.Binary.BinaryPrimitives.WriteInt64LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.Span<long>(ref Underlying)), value);

    /// <summary>
    ///  Tries to get the value of the Quantity field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryGetValue(out long value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => TryGetValue(out var value) ? string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{value}") : "Not Applicable";

    internal long Underlying;

    /// <summary>
    ///  Gets the Quantity field with no value.
    /// </summary>
    public static Quantity Absent => global::System.Runtime.InteropServices.MemoryMarshal.Read<Quantity>([0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x7F]);
}
