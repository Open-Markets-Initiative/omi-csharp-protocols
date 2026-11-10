using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Tops.IexTp.v1_66;

/// <summary>
///  Time stamp of the system event
/// </summary>
public struct Timestamp
{
    /// <summary>
    ///  The size of the Timestamp field in bytes.
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Time stamp of the system event
    /// </summary>
    /// <remarks>
    ///  The time is truncated to a multiple of 100 nanoseconds. <see cref="Decode"/> returns the exact nanosecond count.
    /// </remarks>
    public DateTime Value
    {
        readonly get => DateTime.UnixEpoch.AddTicks(Decode() / TimeSpan.NanosecondsPerTick);
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Timestamp field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Decode()
        => global::System.Buffers.Binary.BinaryPrimitives.ReadInt64LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.ReadOnlySpan<long>(in Underlying)));

    /// <summary>
    ///  Writes an unscaled wire value into the Timestamp field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(long value)
        => global::System.Buffers.Binary.BinaryPrimitives.WriteInt64LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.Span<long>(ref Underlying)), value);

    /// <summary>
    ///  Writes a value into the Timestamp field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(DateTime timestamp)
        => Encode((timestamp - DateTime.UnixEpoch).Ticks * TimeSpan.NanosecondsPerTick);

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => Value.ToString("O", global::System.Globalization.CultureInfo.InvariantCulture);

    internal long Underlying;
}
