namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Liquidity flag the order would have received.
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum LiquidityFlag : byte
{
    /// <summary>
    ///  Added
    /// </summary>
    Added = (byte)'A',

    /// <summary>
    ///  Closing Cross
    /// </summary>
    Closing = (byte)'C',

    /// <summary>
    ///  Retail designated execution that added displayed liquidity
    /// </summary>
    RetailDesignated = (byte)'e',

    /// <summary>
    ///  Halt/IPO Cross
    /// </summary>
    HaltIpo = (byte)'H',

    /// <summary>
    ///  After hours closing cross
    /// </summary>
    AfterHoursClosing = (byte)'i',

    /// <summary>
    ///  Non-displayed adding liquidity
    /// </summary>
    NonDisplayedAddingLiquidity = (byte)'J',

    /// <summary>
    ///  RPI (Retail Price Improving) order provides liquidity
    /// </summary>
    RpiOrderProvidesLiquidity = (byte)'j',

    /// <summary>
    ///  Added liquidity via a midpoint order
    /// </summary>
    AddedLiquidityViaAMidpointOrder = (byte)'k',

    /// <summary>
    ///  Halt Cross
    /// </summary>
    HaltCross = (byte)'K',

    /// <summary>
    ///  Closing Cross (imbalance-only)
    /// </summary>
    ClosingCross = (byte)'L',

    /// <summary>
    ///  Opening Cross (imbalance-only)
    /// </summary>
    OpeningCross = (byte)'M',

    /// <summary>
    ///  Removed liquidity at a midpoint
    /// </summary>
    RemovedLiquidityAtAMidpoint = (byte)'m',

    /// <summary>
    ///  Passive midpoint execution
    /// </summary>
    PassiveMidpointExecution = (byte)'N',

    /// <summary>
    ///  Midpoint Extended Life Order execution
    /// </summary>
    MidpointExtendedLifeOrderExecution = (byte)'n',

    /// <summary>
    ///  Opening Cross
    /// </summary>
    Opening = (byte)'O',

    /// <summary>
    ///  Removed price improving non-displayed liquidity
    /// </summary>
    RemovedPriceImprovingNonDisplayedLiquidity = (byte)'p',

    /// <summary>
    ///  RMO Retail Order removes non-RPI midpoint liquidity
    /// </summary>
    RmoRetailOrderRemovesNonRpiMidpointLiquidity = (byte)'q',

    /// <summary>
    ///  Removed
    /// </summary>
    Removed = (byte)'R',

    /// <summary>
    ///  Retail Order removes RPI liquidity
    /// </summary>
    RetailOrderRemovesRpiLiquidity = (byte)'r',

    /// <summary>
    ///  Retail Order removes price improving non-displayed liquidity other than RPI liquidity
    /// </summary>
    RetailOrderRemovesPriceImprovingNonDisplayedLiquidityOtherThanRpiLiquidity = (byte)'t',

    /// <summary>
    ///  Added non-displayed liquidity via a Reserve order
    /// </summary>
    AddedNonDisplayedLiquidityViaAReserveOrder = (byte)'u',

    /// <summary>
    ///  Supplemental order execution
    /// </summary>
    Supplemental = (byte)'0',

    /// <summary>
    ///  Displayed, liquidity-adding order improves the NBBO
    /// </summary>
    DisplayedLiquidityAddingOrderImprovesTheNbbo = (byte)'7',

    /// <summary>
    ///  Displayed, liquidity-adding order sets the QBBO while joining the NBBO
    /// </summary>
    DisplayedLiquidityAddingOrderSetsTheQbboWhileJoiningTheNbbo = (byte)'8',

    /// <summary>
    ///  RPI order provides liquidity, No RPII
    /// </summary>
    RpiOrderProvidesLiquidityNoRpii = (byte)'1',

    /// <summary>
    ///  Added in the pre-market
    /// </summary>
    AddedInThePreMarket = (byte)'2',

    /// <summary>
    ///  Removed in the pre-market
    /// </summary>
    RemovedInThePreMarket = (byte)'3',

    /// <summary>
    ///  Displayed, liquidity-adding order improves the NBBO in the pre-market
    /// </summary>
    DisplayedLiquidityAddingOrderImprovesTheNbboInThePreMarket = (byte)'5',

    /// <summary>
    ///  Non-displayed adding liquidity in the pre-market
    /// </summary>
    NonDisplayedAddingLiquidityInThePreMarket = (byte)'9',
}
