namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Appendage Id
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum ReplacedMessageOptionalField : byte
{
    /// <summary>
    ///  Replaced Message Optional Firm Enum
    /// </summary>
    Firm = 2,

    /// <summary>
    ///  Replaced Message Optional MinQty Enum
    /// </summary>
    MinQty = 3,

    /// <summary>
    ///  Replaced Message Optional MaxFloor Enum
    /// </summary>
    MaxFloor = 5,

    /// <summary>
    ///  Replaced Message Optional PriceType Enum
    /// </summary>
    PriceType = 6,

    /// <summary>
    ///  Replaced Message Optional PostOnly Enum
    /// </summary>
    PostOnly = 12,

    /// <summary>
    ///  Replaced Message Optional ExpireTime Enum
    /// </summary>
    ExpireTime = 15,

    /// <summary>
    ///  Replaced Message Optional TradeNow Enum
    /// </summary>
    TradeNow = 16,

    /// <summary>
    ///  Replaced Message Optional HandleInst Enum
    /// </summary>
    HandleInst = 17,

    /// <summary>
    ///  Replaced Message Optional BBO Weight Indicator Enum
    /// </summary>
    BboWeightIndicator = 18,

    /// <summary>
    ///  Replaced Message Optional UserRefIdx Enum
    /// </summary>
    UserRefIdx = 28,

    /// <summary>
    ///  Replaced Message Optional AIQ Strategy Enum
    /// </summary>
    AiqStrategy = 29,

    /// <summary>
    ///  Replaced Message Optional AIQ Group ID Enum
    /// </summary>
    AiqGroupId = 30,
}
