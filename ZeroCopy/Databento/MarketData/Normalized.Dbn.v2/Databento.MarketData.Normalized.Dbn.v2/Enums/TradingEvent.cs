using System.Runtime.CompilerServices;

namespace Databento.MarketData.Normalized.Dbn.v2;

/// <summary>
///  Further information about a status update
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum TradingEvent : ushort
{
    /// <summary>
    ///  None
    /// </summary>
    None = 0,

    /// <summary>
    ///  No Cancel
    /// </summary>
    NoCancel = 1,

    /// <summary>
    ///  Change Trading Session
    /// </summary>
    ChangeTradingSession = 2,

    /// <summary>
    ///  Implied Matching On
    /// </summary>
    ImpliedMatchingOn = 3,

    /// <summary>
    ///  Implied Matching Off
    /// </summary>
    ImpliedMatchingOff = 4,

}

/// <summary>
///  Reads and writes the Trading Event field in place.
/// </summary>
public struct TradingEventField
{
    /// <summary>
    ///  The size of the Trading Event field in bytes.
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Further information about a status update
    /// </summary>
    public TradingEvent Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Trading Event field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly TradingEvent Decode()
        => (TradingEvent)global::System.Buffers.Binary.BinaryPrimitives.ReadUInt16LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.ReadOnlySpan<ushort>(in Underlying)));

    /// <summary>
    ///  Writes an unscaled wire value into the Trading Event field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(TradingEvent value)
        => global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.Span<ushort>(ref Underlying)), (ushort)value);

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal ushort Underlying;
}
