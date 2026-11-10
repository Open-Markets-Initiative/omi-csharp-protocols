using System.Runtime.CompilerServices;

namespace Iex.IexEquities.DeepPlus.IexTp.v1_05;

/// <summary>
///  Byte offset of the data stream
/// </summary>
public struct StreamOffset
{
    /// <summary>
    ///  The size of the Stream Offset field in bytes.
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Byte offset of the data stream
    /// </summary>
    public ulong Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Stream Offset field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ulong Decode()
        => global::System.Buffers.Binary.BinaryPrimitives.ReadUInt64LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.ReadOnlySpan<ulong>(in Underlying)));

    /// <summary>
    ///  Writes an unscaled wire value into the Stream Offset field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ulong value)
        => global::System.Buffers.Binary.BinaryPrimitives.WriteUInt64LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.Span<ulong>(ref Underlying)), value);

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal ulong Underlying;
}
