using System.Runtime.CompilerServices;

namespace Databento.MarketData.Normalized.Dbn.v1;

/// <summary>
///  The trading session date corresponding to the settlement price in trading_reference_price, in number of days since the UNIX epoch
/// </summary>
public struct TradingReferenceDate
{
    /// <summary>
    ///  The size of the Trading Reference Date field in bytes.
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  The trading session date corresponding to the settlement price in trading_reference_price, in number of days since the UNIX epoch
    /// </summary>
    public ushort Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Trading Reference Date field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ushort Decode()
        => global::System.Buffers.Binary.BinaryPrimitives.ReadUInt16LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.ReadOnlySpan<ushort>(in Underlying)));

    /// <summary>
    ///  Writes an unscaled wire value into the Trading Reference Date field.
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
