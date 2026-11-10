namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Side
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum Side : byte
{
    /// <summary>
    ///  buy
    /// </summary>
    Buy = (byte)'B',

    /// <summary>
    ///  sell
    /// </summary>
    Sell = (byte)'S',

    /// <summary>
    ///  sell short
    /// </summary>
    SellShort = (byte)'T',

    /// <summary>
    ///  sell short exempt
    /// </summary>
    SellShortExempt = (byte)'E',
}
