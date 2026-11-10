using System.Runtime.CompilerServices;

namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  The UTC end date of the mapping interval, as a YYYYMMDD integer
/// </summary>
public struct EndDate
{
    /// <summary>
    ///  The size of the End Date field in bytes.
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  The UTC end date of the mapping interval, as a YYYYMMDD integer
    /// </summary>
    public uint Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the End Date field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => global::System.Buffers.Binary.BinaryPrimitives.ReadUInt32LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.ReadOnlySpan<uint>(in Underlying)));

    /// <summary>
    ///  Writes an unscaled wire value into the End Date field.
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
