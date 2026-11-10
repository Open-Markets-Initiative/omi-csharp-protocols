namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

/// <summary>
///  Reflects the current trading state for the strategy on the ISE market.
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum CurrentTradingState : byte
{
    /// <summary>
    ///  Halt in effect
    /// </summary>
    HaltInEffect = (byte)'H',

    /// <summary>
    ///  Trading Resumed
    /// </summary>
    TradingResumed = (byte)'T',
}
