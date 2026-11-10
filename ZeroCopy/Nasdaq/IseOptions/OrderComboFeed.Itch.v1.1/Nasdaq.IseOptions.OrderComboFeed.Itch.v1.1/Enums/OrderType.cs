using System.Runtime.CompilerServices;

namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

/// <summary>
///  Order Type
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum OrderType : byte
{
    /// <summary>
    ///  Market
    /// </summary>
    Market = (byte)'M',

    /// <summary>
    ///  Limit
    /// </summary>
    Limit = (byte)'L',

}

/// <summary>
///  Reads and writes the Order Type field in place.
/// </summary>
public struct OrderTypeField
{
    /// <summary>
    ///  The size of the Order Type field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Order Type
    /// </summary>
    public OrderType Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Order Type field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly OrderType Decode()
        => (OrderType)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Order Type field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(OrderType value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
