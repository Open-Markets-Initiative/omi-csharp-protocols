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
