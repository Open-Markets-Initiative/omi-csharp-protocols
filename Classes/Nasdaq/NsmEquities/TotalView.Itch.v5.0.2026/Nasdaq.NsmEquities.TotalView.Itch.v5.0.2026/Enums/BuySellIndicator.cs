namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

/// <summary>
///  The type of order being added
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum BuySellIndicator : byte
{
    /// <summary>
    ///  Buy order
    /// </summary>
    Buy = (byte)'B',

    /// <summary>
    ///  Sell order
    /// </summary>
    Sell = (byte)'S',
}
