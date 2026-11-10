namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Appendage Id
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum ReplaceOrderOptionalField : byte
{
    /// <summary>
    ///  Replace Order Optional MinQty Enum
    /// </summary>
    MinQty = 3,

    /// <summary>
    ///  Replace Order Optional MaxFloor Enum
    /// </summary>
    MaxFloor = 5,

    /// <summary>
    ///  Replace Order Optional PriceType Enum
    /// </summary>
    PriceType = 6,

    /// <summary>
    ///  Replace Order Optional PostOnly Enum
    /// </summary>
    PostOnly = 12,

    /// <summary>
    ///  Replace Order Optional ExpireTime Enum
    /// </summary>
    ExpireTime = 15,

    /// <summary>
    ///  Replace Order Optional TradeNow Enum
    /// </summary>
    TradeNow = 16,

    /// <summary>
    ///  Replace Order Optional HandleInst Enum
    /// </summary>
    HandleInst = 17,

    /// <summary>
    ///  Replace Order Optional RandomReserves Enum
    /// </summary>
    RandomReserves = 13,

    /// <summary>
    ///  Replace Order Optional PegOffset Enum
    /// </summary>
    PegOffset = 7,

    /// <summary>
    ///  Replace Order Optional DiscretionPrice Enum
    /// </summary>
    DiscretionPrice = 9,

    /// <summary>
    ///  Replace Order Optional DiscretionPriceType Enum
    /// </summary>
    DiscretionPriceType = 10,

    /// <summary>
    ///  Replace Order Optional DiscretionPegOffset Enum
    /// </summary>
    DiscretionPegOffset = 11,

    /// <summary>
    ///  Replace Order Optional Shares Located Enum
    /// </summary>
    SharesLocated = 25,

    /// <summary>
    ///  Replace Order Optional Locate Broker Enum
    /// </summary>
    LocateBroker = 26,

    /// <summary>
    ///  Replace Order Optional UserRefIdx Enum
    /// </summary>
    UserRefIdx = 28,

    /// <summary>
    ///  Replace Order Optional Side Enum
    /// </summary>
    Side = 27,

    /// <summary>
    ///  Replace Order Optional AIQ Strategy Enum
    /// </summary>
    AiqStrategy = 29,

    /// <summary>
    ///  Replace Order Optional AIQ Group ID Enum
    /// </summary>
    AiqGroupId = 30,
}
