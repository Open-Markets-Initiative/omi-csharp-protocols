using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Appendage Id
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum OrderModifiedOptionalField : byte
{
    /// <summary>
    ///  Order Modified Optional UserRefIdx Enum
    /// </summary>
    UserRefIdx = 28,

    /// <summary>
    ///  Order Modified Optional Shares Located Enum
    /// </summary>
    SharesLocated = 25,

    /// <summary>
    ///  Order Modified Optional Locate Broker Enum
    /// </summary>
    LocateBroker = 26,

}

/// <summary>
///  Reads and writes the Order Modified Optional Field field in place.
/// </summary>
public struct OrderModifiedOptionalFieldField
{
    /// <summary>
    ///  The size of the Order Modified Optional Field field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Appendage Id
    /// </summary>
    public OrderModifiedOptionalField Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Order Modified Optional Field field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly OrderModifiedOptionalField Decode()
        => (OrderModifiedOptionalField)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Order Modified Optional Field field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(OrderModifiedOptionalField value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
