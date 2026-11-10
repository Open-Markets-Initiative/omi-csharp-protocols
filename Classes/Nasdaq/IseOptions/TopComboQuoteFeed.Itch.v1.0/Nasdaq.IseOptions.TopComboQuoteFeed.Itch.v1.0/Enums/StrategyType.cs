namespace Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0;

/// <summary>
///  StrategyType value
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum StrategyType : byte
{
    /// <summary>
    ///  Vertical Spread
    /// </summary>
    VerticalSpread = (byte)'V',

    /// <summary>
    ///  Time Spread
    /// </summary>
    TimeSpread = (byte)'T',

    /// <summary>
    ///  Diagonal Spread
    /// </summary>
    DiagonalSpread = (byte)'D',

    /// <summary>
    ///  Straddle
    /// </summary>
    Straddle = (byte)'S',

    /// <summary>
    ///  Strangle
    /// </summary>
    Strangle = (byte)'G',

    /// <summary>
    ///  Combo
    /// </summary>
    Combo = (byte)'C',

    /// <summary>
    ///  Risk Reversal
    /// </summary>
    RiskReversal = (byte)'R',

    /// <summary>
    ///  Ratio Spread
    /// </summary>
    RatioSpread = (byte)'A',

    /// <summary>
    ///  Custom
    /// </summary>
    Custom = (byte)'U',
}
