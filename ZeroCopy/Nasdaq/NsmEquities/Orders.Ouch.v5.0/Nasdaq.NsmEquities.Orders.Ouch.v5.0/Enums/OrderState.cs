using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Order State
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum OrderState : byte
{
    /// <summary>
    ///  Order Live
    /// </summary>
    OrderLive = (byte)'L',

    /// <summary>
    ///  Order Dead
    /// </summary>
    OrderDead = (byte)'D',

}

/// <summary>
///  Reads and writes the Order State field in place.
/// </summary>
public struct OrderStateField
{
    /// <summary>
    ///  The size of the Order State field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Order State
    /// </summary>
    public OrderState Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Order State field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly OrderState Decode()
        => (OrderState)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Order State field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(OrderState value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
