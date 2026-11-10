using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  The reason for this order being restated
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum OrderRestatedReason : byte
{
    /// <summary>
    ///  Refresh of display (on an order with reserves)
    /// </summary>
    RefreshOfDisplay = (byte)'R',

    /// <summary>
    ///  Update of displayed price
    /// </summary>
    UpdateOfDisplayedPrice = (byte)'P',

}

/// <summary>
///  Reads and writes the Order Restated Reason field in place.
/// </summary>
public struct OrderRestatedReasonField
{
    /// <summary>
    ///  The size of the Order Restated Reason field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  The reason for this order being restated
    /// </summary>
    public OrderRestatedReason Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Order Restated Reason field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly OrderRestatedReason Decode()
        => (OrderRestatedReason)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Order Restated Reason field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(OrderRestatedReason value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
