namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  The reason for this order being restated
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum OrderRestatedReason : byte
{
    /// <summary>
    ///  Refresh of display (on an order with reserves)
    /// </summary>
    RefreshOfDisplay = (byte)'R',

    /// <summary>
    ///  Update of displayed price
    /// </summary>
    UpdateOfDisplayedPrice = (byte)'P',
}
