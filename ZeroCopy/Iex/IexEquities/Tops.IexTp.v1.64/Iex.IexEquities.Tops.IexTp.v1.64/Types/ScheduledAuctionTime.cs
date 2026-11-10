using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Tops.IexTp.v1_64;

/// <summary>
///  Projected time of the auction match
/// </summary>
public struct ScheduledAuctionTime
{
    /// <summary>
    ///  The size of the Scheduled Auction Time field in bytes.
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Projected time of the auction match
    /// </summary>
    public DateTime Value
    {
        readonly get => DateTime.UnixEpoch.AddSeconds(Decode());
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Scheduled Auction Time field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => global::System.Buffers.Binary.BinaryPrimitives.ReadUInt32LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.ReadOnlySpan<uint>(in Underlying)));

    /// <summary>
    ///  Writes an unscaled wire value into the Scheduled Auction Time field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
        => global::System.Buffers.Binary.BinaryPrimitives.WriteUInt32LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.Span<uint>(ref Underlying)), value);

    /// <summary>
    ///  Writes a value into the Scheduled Auction Time field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(DateTime timestamp)
        => Encode((uint)((timestamp - DateTime.UnixEpoch).Ticks / TimeSpan.TicksPerSecond));

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => Value.ToString("O", global::System.Globalization.CultureInfo.InvariantCulture);

    internal uint Underlying;
}
