namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Event Code
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum EventCode : byte
{
    /// <summary>
    ///  Start Of Day, indicating that exchange is open and is ready to accept orders.
    /// </summary>
    StartOfDay = (byte)'S',

    /// <summary>
    ///  End Of Day, indicating that the exchange is closed and will no longer accept new orders. Note that it is still possible to receive breaks and cancels after this event.
    /// </summary>
    EndOfDay = (byte)'E',
}
