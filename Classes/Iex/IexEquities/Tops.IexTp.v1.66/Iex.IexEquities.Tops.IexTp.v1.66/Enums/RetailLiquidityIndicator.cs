namespace Iex.IexEquities.Tops.IexTp.v1_66;

/// <summary>
///  Retail Liquidity Indicator identifier
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
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
