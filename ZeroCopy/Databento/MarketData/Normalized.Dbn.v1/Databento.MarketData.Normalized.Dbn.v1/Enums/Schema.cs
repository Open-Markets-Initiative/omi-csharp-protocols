using System.Runtime.CompilerServices;

namespace Databento.MarketData.Normalized.Dbn.v1;

/// <summary>
///  The data record schema; no value for a file of mixed schemas
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum Schema : ushort
{
    /// <summary>
    ///  Mbo
    /// </summary>
    Mbo = 0,

    /// <summary>
    ///  Mbp1
    /// </summary>
    Mbp1 = 1,

    /// <summary>
    ///  Mbp10
    /// </summary>
    Mbp10 = 2,

    /// <summary>
    ///  Tbbo
    /// </summary>
    Tbbo = 3,

    /// <summary>
    ///  Trades
    /// </summary>
    Trades = 4,

    /// <summary>
    ///  Ohlcv1 S
    /// </summary>
    Ohlcv1S = 5,

    /// <summary>
    ///  Ohlcv1 M
    /// </summary>
    Ohlcv1M = 6,

    /// <summary>
    ///  Ohlcv1 H
    /// </summary>
    Ohlcv1H = 7,

    /// <summary>
    ///  Ohlcv1 D
    /// </summary>
    Ohlcv1D = 8,

    /// <summary>
    ///  Definition
    /// </summary>
    Definition = 9,

    /// <summary>
    ///  Statistics
    /// </summary>
    Statistics = 10,

    /// <summary>
    ///  Status
    /// </summary>
    Status = 11,

    /// <summary>
    ///  Imbalance
    /// </summary>
    Imbalance = 12,

    /// <summary>
    ///  Ohlcv Eod
    /// </summary>
    OhlcvEod = 13,

    /// <summary>
    ///  Cmbp1
    /// </summary>
    Cmbp1 = 14,

    /// <summary>
    ///  Cbbo1 S
    /// </summary>
    Cbbo1S = 15,

    /// <summary>
    ///  Cbbo1 M
    /// </summary>
    Cbbo1M = 16,

    /// <summary>
    ///  Tcbbo
    /// </summary>
    Tcbbo = 17,

    /// <summary>
    ///  Bbo1 S
    /// </summary>
    Bbo1S = 18,

    /// <summary>
    ///  Bbo1 M
    /// </summary>
    Bbo1M = 19,

    /// <summary>
    ///  The data record schema; no value for a file of mixed schemas
    /// </summary>
    NoValue = 65535,

}

/// <summary>
///  Reads and writes the Schema field in place.
/// </summary>
public struct SchemaField
{
    /// <summary>
    ///  The size of the Schema field in bytes.
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  The value of the Schema field when it holds no value.
    /// </summary>
    public const Schema NullSentinel = (Schema)65535;

    /// <summary>
    ///  Gets a value that indicates whether the Schema field holds a value.
    /// </summary>
    public readonly bool HasValue
        => Decode() != NullSentinel;

    /// <summary>
    ///  The data record schema; no value for a file of mixed schemas
    /// </summary>
    public Schema Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Schema field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly Schema Decode()
        => (Schema)global::System.Buffers.Binary.BinaryPrimitives.ReadUInt16LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.ReadOnlySpan<ushort>(in Underlying)));

    /// <summary>
    ///  Writes an unscaled wire value into the Schema field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(Schema value)
        => global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.Span<ushort>(ref Underlying)), (ushort)value);

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal ushort Underlying;

    /// <summary>
    ///  Gets the Schema field with no value.
    /// </summary>
    public static SchemaField Absent => global::System.Runtime.InteropServices.MemoryMarshal.Read<SchemaField>([0xFF, 0xFF]);
}
