using System.Runtime.CompilerServices;

namespace Databento.MarketData.Normalized.Dbn.v2;

/// <summary>
///  The publisher ID assigned by Databento, which denotes the dataset and venue
/// </summary>
public struct PublisherId
{
    /// <summary>
    ///  The size of the Publisher Id field in bytes.
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  The publisher ID assigned by Databento, which denotes the dataset and venue
    /// </summary>
    public ushort Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Publisher Id field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ushort Decode()
        => global::System.Buffers.Binary.BinaryPrimitives.ReadUInt16LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.ReadOnlySpan<ushort>(in Underlying)));

    /// <summary>
    ///  Writes an unscaled wire value into the Publisher Id field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ushort value)
        => global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.Span<ushort>(ref Underlying)), value);

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal ushort Underlying;
}
