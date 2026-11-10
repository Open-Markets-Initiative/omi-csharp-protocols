using System.Runtime.CompilerServices;

namespace Iex.IexEquities.DeepPlus.IexTp.v1_05;

/// <summary>
///  Identifies the stream of bytes sequenced messages
/// </summary>
public struct ChannelId
{
    /// <summary>
    ///  The channel the single-channel DEEP+ feed is published on
    /// </summary>
    public const uint StaticValue = 1u;

    /// <summary>
    ///  The size of the Channel Id field in bytes.
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Identifies the stream of bytes sequenced messages
    /// </summary>
    public uint Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Channel Id field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => global::System.Buffers.Binary.BinaryPrimitives.ReadUInt32LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.ReadOnlySpan<uint>(in Underlying)));

    /// <summary>
    ///  Writes an unscaled wire value into the Channel Id field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
        => global::System.Buffers.Binary.BinaryPrimitives.WriteUInt32LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.Span<uint>(ref Underlying)), value);

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal uint Underlying;
}
