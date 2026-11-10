using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Appendage Id
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum OrderRestatedOptionalField : byte
{
    /// <summary>
    ///  Order Restated Optional Display Quantity Enum
    /// </summary>
    DisplayQuantity = 22,

    /// <summary>
    ///  Order Restated Optional Display Price Enum
    /// </summary>
    DisplayPrice = 23,

    /// <summary>
    ///  Order Restated Optional SecondaryOrdRefNum Enum
    /// </summary>
    SecondaryOrdRefNum = 1,

    /// <summary>
    ///  Order Restated Optional UserRefIdx Enum
    /// </summary>
    UserRefIdx = 28,

}

/// <summary>
///  Reads and writes the Order Restated Optional Field field in place.
/// </summary>
public struct OrderRestatedOptionalFieldField
{
    /// <summary>
    ///  The size of the Order Restated Optional Field field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Appendage Id
    /// </summary>
    public OrderRestatedOptionalField Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Order Restated Optional Field field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly OrderRestatedOptionalField Decode()
        => (OrderRestatedOptionalField)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Order Restated Optional Field field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(OrderRestatedOptionalField value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
