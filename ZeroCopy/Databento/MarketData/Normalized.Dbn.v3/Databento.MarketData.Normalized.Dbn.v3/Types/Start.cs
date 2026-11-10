using System.Runtime.CompilerServices;

namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  The start time of query range in UNIX epoch nanoseconds
/// </summary>
public struct Start
{
    /// <summary>
    ///  The size of the Start field in bytes.
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  The start time of query range in UNIX epoch nanoseconds
    /// </summary>
    /// <remarks>
    ///  The time is truncated to a multiple of 100 nanoseconds. <see cref="Decode"/> returns the exact nanosecond count.
    /// </remarks>
    public DateTime Value
    {
        readonly get => DateTime.UnixEpoch.AddTicks((long)(Decode() / TimeSpan.NanosecondsPerTick));
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Start field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ulong Decode()
        => global::System.Buffers.Binary.BinaryPrimitives.ReadUInt64LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.ReadOnlySpan<ulong>(in Underlying)));

    /// <summary>
    ///  Writes an unscaled wire value into the Start field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ulong value)
        => global::System.Buffers.Binary.BinaryPrimitives.WriteUInt64LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.Span<ulong>(ref Underlying)), value);

    /// <summary>
    ///  Writes a value into the Start field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(DateTime timestamp)
        => Encode((ulong)(timestamp - DateTime.UnixEpoch).Ticks * TimeSpan.NanosecondsPerTick);

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => Value.ToString("O", global::System.Globalization.CultureInfo.InvariantCulture);

    internal ulong Underlying;
}
