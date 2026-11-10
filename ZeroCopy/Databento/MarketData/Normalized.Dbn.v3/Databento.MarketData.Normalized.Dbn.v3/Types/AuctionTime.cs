using System.Runtime.CompilerServices;

namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  Reserved for future use
/// </summary>
public struct AuctionTime
{
    /// <summary>
    ///  The value of the Auction Time field that marks no value.
    /// </summary>
    public const ulong NoValue = 18446744073709551615UL;

    /// <summary>
    ///  The size of the Auction Time field in bytes.
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Reserved for future use
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
    ///  Returns the wire value of the Auction Time field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ulong Decode()
        => global::System.Buffers.Binary.BinaryPrimitives.ReadUInt64LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.ReadOnlySpan<ulong>(in Underlying)));

    /// <summary>
    ///  Writes an unscaled wire value into the Auction Time field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ulong value)
        => global::System.Buffers.Binary.BinaryPrimitives.WriteUInt64LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.Span<ulong>(ref Underlying)), value);

    /// <summary>
    ///  Writes a value into the Auction Time field.
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

    /// <summary>
    ///  Gets the Auction Time field with no value.
    /// </summary>
    public static AuctionTime Absent => global::System.Runtime.InteropServices.MemoryMarshal.Read<AuctionTime>([0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF]);
}
