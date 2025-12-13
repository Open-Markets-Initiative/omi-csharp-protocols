namespace Ice.iMpact;

/// <summary>
///  Special Field Id: Special field type
/// </summary>

public enum SpecialFieldId : sbyte
{
    /// <summary>
    ///  Eris Futures Price
    /// </summary>
    AltPrice = 1,

    /// <summary>
    ///  High Eris Futures Price
    /// </summary>
    AltHighPrice = 2,

    /// <summary>
    ///  Low Eris Futures Price
    /// </summary>
    AltLowPrice = 3,

    /// <summary>
    ///  Volume-weighted Average Eris Futures Price
    /// </summary>
    AltVwap = 4,

    /// <summary>
    ///  Last Trade Eris Futures Price
    /// </summary>
    AltLastTradePrice = 5,

    /// <summary>
    ///  Order is All-Or-None
    /// </summary>
    Aon = 6,

}
