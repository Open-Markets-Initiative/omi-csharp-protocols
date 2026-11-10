using System.Runtime.CompilerServices;

namespace Databento.MarketData.Normalized.Dbn.v1;

/// <summary>
///  The maximum number of records to return; no value when unlimited
/// </summary>
public struct Limit
{
    /// <summary>
    ///  The value of the Limit field that marks no value.
    /// </summary>
    public const ulong NoValue = 0UL;

    /// <summary>
    ///  The size of the Limit field in bytes.
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  The maximum number of records to return; no value when unlimited
    /// </summary>
    public ulong Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Gets a value that indicates whether the Limit field holds a value.
    /// </summary>
    public readonly bool HasValue
        => Decode() != NoValue;

    /// <summary>
    ///  Returns the wire value of the Limit field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ulong Decode()
        => global::System.Buffers.Binary.BinaryPrimitives.ReadUInt64LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.ReadOnlySpan<ulong>(in Underlying)));

    /// <summary>
    ///  Writes an unscaled wire value into the Limit field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ulong value)
        => global::System.Buffers.Binary.BinaryPrimitives.WriteUInt64LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.Span<ulong>(ref Underlying)), value);

    /// <summary>
    ///  Tries to get the value of the Limit field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryGetValue(out ulong value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => TryGetValue(out var value) ? string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{value}") : "Not Applicable";

    internal ulong Underlying;

    /// <summary>
    ///  Gets the Limit field with no value.
    /// </summary>
    public static Limit Absent => global::System.Runtime.InteropServices.MemoryMarshal.Read<Limit>([0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00]);
}
