namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Corresponds to TimeInForce (59) in Nasdaq FIX
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum TimeInForce : byte
{
    /// <summary>
    ///  Day (Market Hours)
    /// </summary>
    Day = (byte)'0',

    /// <summary>
    ///  IOC
    /// </summary>
    Ioc = (byte)'3',

    /// <summary>
    ///  GTX (Extended Hours)
    /// </summary>
    GtxExtendedHours = (byte)'5',

    /// <summary>
    ///  GTT (ExpireTime needs to be specified)
    /// </summary>
    Gtt = (byte)'6',

    /// <summary>
    ///  After hours
    /// </summary>
    AfterHours = (byte)'E',
}
