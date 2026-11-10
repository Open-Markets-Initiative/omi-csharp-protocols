using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Shares that would have executed if the trade would have occurred. Depending on AIQ type and the sizes of orders in question the value for this field could either be the same or different than the Decrement Shares field. For “Decrement both” they are always the same. For “Cancel oldest” they will be different if the incoming order is smaller than the resting order.
/// </summary>
public struct QuantityPreventedFromTrading
{
    /// <summary>
    ///  The size of the Quantity Prevented From Trading field in bytes.
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Shares that would have executed if the trade would have occurred. Depending on AIQ type and the sizes of orders in question the value for this field could either be the same or different than the Decrement Shares field. For “Decrement both” they are always the same. For “Cancel oldest” they will be different if the incoming order is smaller than the resting order.
    /// </summary>
    public uint Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Quantity Prevented From Trading field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => global::System.Buffers.Binary.BinaryPrimitives.ReadUInt32BigEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.ReadOnlySpan<uint>(in Underlying)));

    /// <summary>
    ///  Writes an unscaled wire value into the Quantity Prevented From Trading field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
        => global::System.Buffers.Binary.BinaryPrimitives.WriteUInt32BigEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.Span<uint>(ref Underlying)), value);

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal uint Underlying;
}
