namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  TradeNow
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum TradeNow : byte
{
    /// <summary>
    ///  yes
    /// </summary>
    Yes = (byte)'Y',

    /// <summary>
    ///  no (default)
    /// </summary>
    No = (byte)'N',
}
