namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Appendage Id
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum ModifyOrderOptionalField : byte
{
    /// <summary>
    ///  Modify Order Optional UserRefIdx Enum
    /// </summary>
    UserRefIdx = 28,

    /// <summary>
    ///  Modify Order Optional Shares Located Enum
    /// </summary>
    SharesLocated = 25,

    /// <summary>
    ///  Modify Order Optional Locate Broker Enum
    /// </summary>
    LocateBroker = 26,
}
