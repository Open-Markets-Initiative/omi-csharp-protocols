using System.Runtime.CompilerServices;

namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

/// <summary>
///  Order Capacity value
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum OrderCapacity : byte
{
    /// <summary>
    ///  Customer
    /// </summary>
    Customer = (byte)'C',

    /// <summary>
    ///  Customer Professional
    /// </summary>
    CustomerProfessional = (byte)'D',

    /// <summary>
    ///  Firm
    /// </summary>
    Firm = (byte)'F',

    /// <summary>
    ///  Broker/Dealer - Customer
    /// </summary>
    BrokerDealerCustomer = (byte)'B',

    /// <summary>
    ///  Broker/Dealer - Firm
    /// </summary>
    BrokerDealerFirm = (byte)'K',

    /// <summary>
    ///  Proprietary
    /// </summary>
    Proprietary = (byte)'E',

    /// <summary>
    ///  Away Market Maker
    /// </summary>
    AwayMarketMaker = (byte)'N',

    /// <summary>
    ///  Market Maker
    /// </summary>
    MarketMaker = (byte)'M',

}

/// <summary>
///  Reads and writes the Order Capacity field in place.
/// </summary>
public struct OrderCapacityField
{
    /// <summary>
    ///  The size of the Order Capacity field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Order Capacity value
    /// </summary>
    public OrderCapacity Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Order Capacity field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly OrderCapacity Decode()
        => (OrderCapacity)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Order Capacity field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(OrderCapacity value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
