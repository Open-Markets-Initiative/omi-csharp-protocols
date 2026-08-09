namespace Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
///  Retail Liquidity Indicator identifier
/// </summary>
/// <remarks>
///  Each value corresponds to the ASCII byte value used as the wire-format discriminator.
/// </remarks>
public enum RetailLiquidityIndicator : byte
{
    /// <summary>
    ///  Not Applicable
    /// </summary>
    NotApplicable = (byte)' ',

    /// <summary>
    ///  Buy Interest
    /// </summary>
    BuyInterest = (byte)'A',

    /// <summary>
    ///  Sell Interest
    /// </summary>
    SellInterest = (byte)'B',

    /// <summary>
    ///  Buy And Sell Interest
    /// </summary>
    BuyAndSellInterest = (byte)'C',
}
