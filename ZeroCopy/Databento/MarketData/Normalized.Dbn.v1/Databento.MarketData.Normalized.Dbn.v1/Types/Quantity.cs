using System.Runtime.CompilerServices;

namespace Databento.MarketData.Normalized.Dbn.v1;

/// <summary>
///  The value for non-price statistics
/// </summary>
public struct Quantity
{
    /// <summary>
    ///  The value of the Quantity field that marks no value.
    /// </summary>
    public const int NoValue = 2147483647;

    /// <summary>
    ///  The size of the Quantity field in bytes.
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  The value for non-price statistics
    /// </summary>
    public int Value
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
    public readonly int Decode()
        => global::System.Buffers.Binary.BinaryPrimitives.ReadInt32LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.ReadOnlySpan<int>(in Underlying)));

    /// <summary>
    ///  Writes an unscaled wire value into the Quantity field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(int value)
        => global::System.Buffers.Binary.BinaryPrimitives.WriteInt32LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.Span<int>(ref Underlying)), value);

    /// <summary>
    ///  Tries to get the value of the Quantity field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryGetValue(out int value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => TryGetValue(out var value) ? string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{value}") : "Not Applicable";

    internal int Underlying;

    /// <summary>
    ///  Gets the Quantity field with no value.
    /// </summary>
    public static Quantity Absent => global::System.Runtime.InteropServices.MemoryMarshal.Read<Quantity>([0xFF, 0xFF, 0xFF, 0x7F]);
}
