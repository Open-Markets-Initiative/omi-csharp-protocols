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
