namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  DiscretionPriceType
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum DiscretionPriceType : byte
{
    /// <summary>
    ///  limit (default)
    /// </summary>
    Limit = (byte)'L',

    /// <summary>
    ///  market peg
    /// </summary>
    MarketPeg = (byte)'P',

    /// <summary>
    ///  midpoint peg
    /// </summary>
    MidpointPeg = (byte)'M',

    /// <summary>
    ///  primary peg
    /// </summary>
    PrimaryPeg = (byte)'R',
}
