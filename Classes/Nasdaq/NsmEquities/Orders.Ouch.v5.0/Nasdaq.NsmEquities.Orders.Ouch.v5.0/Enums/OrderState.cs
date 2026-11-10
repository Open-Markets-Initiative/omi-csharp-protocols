namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Order State
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum OrderState : byte
{
    /// <summary>
    ///  Order Live
    /// </summary>
    OrderLive = (byte)'L',

    /// <summary>
    ///  Order Dead
    /// </summary>
    OrderDead = (byte)'D',
}
