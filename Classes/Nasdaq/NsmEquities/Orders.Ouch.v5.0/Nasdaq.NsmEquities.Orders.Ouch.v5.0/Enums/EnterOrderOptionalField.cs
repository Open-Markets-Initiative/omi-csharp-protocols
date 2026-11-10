namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Appendage Id
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum EnterOrderOptionalField : byte
{
    /// <summary>
    ///  Enter Order Optional Firm Enum
    /// </summary>
    Firm = 2,

    /// <summary>
    ///  Enter Order Optional MinQty Enum
    /// </summary>
    MinQty = 3,

    /// <summary>
    ///  Enter Order Optional CustomerType Enum
    /// </summary>
    CustomerType = 4,

    /// <summary>
    ///  Enter Order Optional MaxFloor Enum
    /// </summary>
    MaxFloor = 5,

    /// <summary>
    ///  Enter Order Optional PriceType Enum
    /// </summary>
    PriceType = 6,

    /// <summary>
    ///  Enter Order Optional PegOffset Enum
    /// </summary>
    PegOffset = 7,

    /// <summary>
    ///  Enter Order Optional DiscretionPrice Enum
    /// </summary>
    DiscretionPrice = 9,

    /// <summary>
    ///  Enter Order Optional DiscretionPriceType Enum
    /// </summary>
    DiscretionPriceType = 10,

    /// <summary>
    ///  Enter Order Optional DiscretionPegOffset Enum
    /// </summary>
    DiscretionPegOffset = 11,

    /// <summary>
    ///  Enter Order Optional PostOnly Enum
    /// </summary>
    PostOnly = 12,

    /// <summary>
    ///  Enter Order Optional RandomReserves Enum
    /// </summary>
    RandomReserves = 13,

    /// <summary>
    ///  Enter Order Optional ExpireTime Enum
    /// </summary>
    ExpireTime = 15,

    /// <summary>
    ///  Enter Order Optional TradeNow Enum
    /// </summary>
    TradeNow = 16,

    /// <summary>
    ///  Enter Order Optional HandleInst Enum
    /// </summary>
    HandleInst = 17,

    /// <summary>
    ///  Enter Order Optional Group ID Enum
    /// </summary>
    GroupId = 24,

    /// <summary>
    ///  Enter Order Optional Shares Located Enum
    /// </summary>
    SharesLocated = 25,

    /// <summary>
    ///  Enter Order Optional Locate Broker Enum
    /// </summary>
    LocateBroker = 26,

    /// <summary>
    ///  Enter Order Optional UserRefIdx Enum
    /// </summary>
    UserRefIdx = 28,

    /// <summary>
    ///  Enter Order Optional AIQ Strategy Enum
    /// </summary>
    AiqStrategy = 29,

    /// <summary>
    ///  Enter Order Optional AIQ Group ID Enum
    /// </summary>
    AiqGroupId = 30,
}
