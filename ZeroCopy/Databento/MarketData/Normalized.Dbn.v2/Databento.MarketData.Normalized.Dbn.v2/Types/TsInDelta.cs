using System.Runtime.CompilerServices;

namespace Databento.MarketData.Normalized.Dbn.v2;

/// <summary>
///  The matching-engine-sending timestamp expressed as the number of nanoseconds before ts_recv
/// </summary>
public struct TsInDelta
{
    /// <summary>
    ///  The size of the Ts In Delta field in bytes.
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  The matching-engine-sending timestamp expressed as the number of nanoseconds before ts_recv
    /// </summary>
    public int Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Ts In Delta field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int Decode()
        => global::System.Buffers.Binary.BinaryPrimitives.ReadInt32LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.ReadOnlySpan<int>(in Underlying)));

    /// <summary>
    ///  Writes an unscaled wire value into the Ts In Delta field.
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
