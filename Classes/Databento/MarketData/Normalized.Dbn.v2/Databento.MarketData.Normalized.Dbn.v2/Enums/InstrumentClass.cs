namespace Databento.MarketData.Normalized.Dbn.v2;

/// <summary>
///  The classification of the instrument
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum InstrumentClass : byte
{
    /// <summary>
    ///  Bond
    /// </summary>
    Bond = (byte)'B',

    /// <summary>
    ///  Call
    /// </summary>
    Call = (byte)'C',

    /// <summary>
    ///  Future
    /// </summary>
    Future = (byte)'F',

    /// <summary>
    ///  Index
    /// </summary>
    Index = (byte)'I',

    /// <summary>
    ///  Stock
    /// </summary>
    Stock = (byte)'K',

    /// <summary>
    ///  Mixed Spread
    /// </summary>
    MixedSpread = (byte)'M',

    /// <summary>
    ///  Put
    /// </summary>
    Put = (byte)'P',

    /// <summary>
    ///  Future Spread
    /// </summary>
    FutureSpread = (byte)'S',

    /// <summary>
    ///  Option Spread
    /// </summary>
    OptionSpread = (byte)'T',

    /// <summary>
    ///  Fx Spot
    /// </summary>
    FxSpot = (byte)'X',

    /// <summary>
    ///  Commodity Spot
    /// </summary>
    CommoditySpot = (byte)'Y',
}
