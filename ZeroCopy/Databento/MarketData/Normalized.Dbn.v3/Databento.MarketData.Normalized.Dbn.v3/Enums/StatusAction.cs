using System.Runtime.CompilerServices;

namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  The type of status change
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum StatusAction : ushort
{
    /// <summary>
    ///  None
    /// </summary>
    None = 0,

    /// <summary>
    ///  Pre Open
    /// </summary>
    PreOpen = 1,

    /// <summary>
    ///  Pre Cross
    /// </summary>
    PreCross = 2,

    /// <summary>
    ///  Quoting
    /// </summary>
    Quoting = 3,

    /// <summary>
    ///  Cross
    /// </summary>
    Cross = 4,

    /// <summary>
    ///  Rotation
    /// </summary>
    Rotation = 5,

    /// <summary>
    ///  New Price Indication
    /// </summary>
    NewPriceIndication = 6,

    /// <summary>
    ///  Trading
    /// </summary>
    Trading = 7,

    /// <summary>
    ///  Halt
    /// </summary>
    Halt = 8,

    /// <summary>
    ///  Pause
    /// </summary>
    Pause = 9,

    /// <summary>
    ///  Suspend
    /// </summary>
    Suspend = 10,

    /// <summary>
    ///  Pre Close
    /// </summary>
    PreClose = 11,

    /// <summary>
    ///  Close
    /// </summary>
    Close = 12,

    /// <summary>
    ///  Post Close
    /// </summary>
    PostClose = 13,

    /// <summary>
    ///  Ssr Change
    /// </summary>
    SsrChange = 14,

    /// <summary>
    ///  Not Available For Trading
    /// </summary>
    NotAvailableForTrading = 15,

}

/// <summary>
///  Reads and writes the Status Action field in place.
/// </summary>
public struct StatusActionField
{
    /// <summary>
    ///  The size of the Status Action field in bytes.
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  The type of status change
    /// </summary>
    public StatusAction Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Status Action field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly StatusAction Decode()
        => (StatusAction)global::System.Buffers.Binary.BinaryPrimitives.ReadUInt16LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.ReadOnlySpan<ushort>(in Underlying)));

    /// <summary>
    ///  Writes an unscaled wire value into the Status Action field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(StatusAction value)
        => global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.Span<ushort>(ref Underlying)), (ushort)value);

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal ushort Underlying;
}
