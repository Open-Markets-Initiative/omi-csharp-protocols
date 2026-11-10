using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  CustomerType
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum CustomerType : byte
{
    /// <summary>
    ///  Retail Designated Order
    /// </summary>
    RetailDesignatedOrder = (byte)'R',

    /// <summary>
    ///  Not Retail Designated (default)
    /// </summary>
    NotRetailDesignated = (byte)'N',

}

/// <summary>
///  Reads and writes the Customer Type field in place.
/// </summary>
public struct CustomerTypeField
{
    /// <summary>
    ///  The size of the Customer Type field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  CustomerType
    /// </summary>
    public CustomerType Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Customer Type field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly CustomerType Decode()
        => (CustomerType)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Customer Type field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(CustomerType value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
