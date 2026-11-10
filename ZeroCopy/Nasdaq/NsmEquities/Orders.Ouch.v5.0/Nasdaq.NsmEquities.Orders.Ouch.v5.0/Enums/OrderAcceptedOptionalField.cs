using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Appendage Id
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum OrderAcceptedOptionalField : byte
{
    /// <summary>
    ///  Order Accepted Optional Firm Enum
    /// </summary>
    Firm = 2,

    /// <summary>
    ///  Order Accepted Optional MinQty Enum
    /// </summary>
    MinQty = 3,

    /// <summary>
    ///  Order Accepted Optional CustomerType Enum
    /// </summary>
    CustomerType = 4,

    /// <summary>
    ///  Order Accepted Optional MaxFloor Enum
    /// </summary>
    MaxFloor = 5,

    /// <summary>
    ///  Order Accepted Optional PriceType Enum
    /// </summary>
    PriceType = 6,

    /// <summary>
    ///  Order Accepted Optional PegOffset Enum
    /// </summary>
    PegOffset = 7,

    /// <summary>
    ///  Order Accepted Optional DiscretionPrice Enum
    /// </summary>
    DiscretionPrice = 9,

    /// <summary>
    ///  Order Accepted Optional DiscretionPriceType Enum
    /// </summary>
    DiscretionPriceType = 10,

    /// <summary>
    ///  Order Accepted Optional DiscretionPegOffset Enum
    /// </summary>
    DiscretionPegOffset = 11,

    /// <summary>
    ///  Order Accepted Optional PostOnly Enum
    /// </summary>
    PostOnly = 12,

    /// <summary>
    ///  Order Accepted Optional RandomReserves Enum
    /// </summary>
    RandomReserves = 13,

    /// <summary>
    ///  Order Accepted Optional Route Enum
    /// </summary>
    Route = 14,

    /// <summary>
    ///  Order Accepted Optional ExpireTime Enum
    /// </summary>
    ExpireTime = 15,

    /// <summary>
    ///  Order Accepted Optional TradeNow Enum
    /// </summary>
    TradeNow = 16,

    /// <summary>
    ///  Order Accepted Optional HandleInst Enum
    /// </summary>
    HandleInst = 17,

    /// <summary>
    ///  Order Accepted Optional BBO Weight Indicator Enum
    /// </summary>
    BboWeightIndicator = 18,

    /// <summary>
    ///  Order Accepted Optional Group ID Enum
    /// </summary>
    GroupId = 24,

    /// <summary>
    ///  Order Accepted Optional Shares Located Enum
    /// </summary>
    SharesLocated = 25,

    /// <summary>
    ///  Order Accepted Optional UserRefIdx Enum
    /// </summary>
    UserRefIdx = 28,

    /// <summary>
    ///  Order Accepted Optional AIQ Strategy Enum
    /// </summary>
    AiqStrategy = 29,

    /// <summary>
    ///  Order Accepted Optional AIQ Group ID Enum
    /// </summary>
    AiqGroupId = 30,

}

/// <summary>
///  Reads and writes the Order Accepted Optional Field field in place.
/// </summary>
public struct OrderAcceptedOptionalFieldField
{
    /// <summary>
    ///  The size of the Order Accepted Optional Field field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Appendage Id
    /// </summary>
    public OrderAcceptedOptionalField Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Order Accepted Optional Field field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly OrderAcceptedOptionalField Decode()
        => (OrderAcceptedOptionalField)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Order Accepted Optional Field field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(OrderAcceptedOptionalField value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
