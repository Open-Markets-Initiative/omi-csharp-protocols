namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  PriceType
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum PriceType : byte
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

    /// <summary>
    ///  market maker peg
    /// </summary>
    MarketMakerPeg = (byte)'Q',

    /// <summary>
    ///  midpoint
    /// </summary>
    Midpoint = (byte)'m',
}
