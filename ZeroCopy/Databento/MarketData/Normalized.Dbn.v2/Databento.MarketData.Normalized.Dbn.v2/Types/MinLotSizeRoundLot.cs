using System.Runtime.CompilerServices;

namespace Databento.MarketData.Normalized.Dbn.v2;

/// <summary>
///  The min lot size round lot
/// </summary>
public struct MinLotSizeRoundLot
{
    /// <summary>
    ///  The size of the Min Lot Size Round Lot field in bytes.
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  The min lot size round lot
    /// </summary>
    public int Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Min Lot Size Round Lot field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int Decode()
        => global::System.Buffers.Binary.BinaryPrimitives.ReadInt32LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.ReadOnlySpan<int>(in Underlying)));

    /// <summary>
    ///  Writes an unscaled wire value into the Min Lot Size Round Lot field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(int value)
        => global::System.Buffers.Binary.BinaryPrimitives.WriteInt32LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.Span<int>(ref Underlying)), value);

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal int Underlying;
}
