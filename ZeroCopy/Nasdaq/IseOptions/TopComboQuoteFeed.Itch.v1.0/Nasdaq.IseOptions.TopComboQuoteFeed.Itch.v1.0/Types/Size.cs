using System.Runtime.CompilerServices;

namespace Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0;

/// <summary>
///  Aggregated number of contracts on the bid or ask side being displayed in the options market at the current time.
/// </summary>
public struct Size
{
    /// <summary>
    ///  The size of the Size field in bytes.
    /// </summary>
    public const int ByteCount = 4;

    /// <summary>
    ///  Aggregated number of contracts on the bid or ask side being displayed in the options market at the current time.
    /// </summary>
    public uint Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Size field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => global::System.Buffers.Binary.BinaryPrimitives.ReadUInt32BigEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.ReadOnlySpan<uint>(in Underlying)));

    /// <summary>
    ///  Writes an unscaled wire value into the Size field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
        => global::System.Buffers.Binary.BinaryPrimitives.WriteUInt32BigEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.Span<uint>(ref Underlying)), value);

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal uint Underlying;
}
