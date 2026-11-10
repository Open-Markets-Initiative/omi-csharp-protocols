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
