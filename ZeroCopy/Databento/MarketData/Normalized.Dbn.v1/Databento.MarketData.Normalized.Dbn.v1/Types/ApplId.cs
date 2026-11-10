using System.Runtime.CompilerServices;

namespace Databento.MarketData.Normalized.Dbn.v1;

/// <summary>
///  The appl id
/// </summary>
public struct ApplId
{
    /// <summary>
    ///  The size of the Appl Id field in bytes.
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  The appl id
    /// </summary>
    public short Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Appl Id field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly short Decode()
        => global::System.Buffers.Binary.BinaryPrimitives.ReadInt16LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.ReadOnlySpan<short>(in Underlying)));

    /// <summary>
    ///  Writes an unscaled wire value into the Appl Id field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(short value)
        => global::System.Buffers.Binary.BinaryPrimitives.WriteInt16LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.Span<short>(ref Underlying)), value);

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal short Underlying;
}
