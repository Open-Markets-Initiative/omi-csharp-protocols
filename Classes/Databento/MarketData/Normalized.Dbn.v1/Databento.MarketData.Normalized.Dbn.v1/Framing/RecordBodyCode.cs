namespace Databento.MarketData.Normalized.Dbn.v1;

/// <summary>
///  Identifies the type of a message contained in File or Gateway Stream by its message type.
/// </summary>
public enum RecordBodyCode : byte
{
    /// <summary>
    ///  Market by price implementation with a book depth of 0; equivalent to MBP-0, the record of the trades schema
    /// </summary>
    TradeMsg = 0,

    /// <summary>
    ///  Market by price implementation with a known book depth of 1; the record of the mbp-1 and tbbo schemas
    /// </summary>
    Mbp1Msg = 1,

    /// <summary>
    ///  Market by price implementation with a known book depth of 10
    /// </summary>
    Mbp10Msg = 10,

    /// <summary>
    ///  Ohlcv Deprecated
    /// </summary>
    OhlcvMsg = 17,

    /// <summary>
    ///  A trading status update message; the record of the status schema
    /// </summary>
    StatusMsg = 18,

    /// <summary>
    ///  A definition of an instrument; the record of the definition schema
    /// </summary>
    InstrumentDefMsg = 19,

    /// <summary>
    ///  An auction imbalance message; the record of the imbalance schema
    /// </summary>
    ImbalanceMsg = 20,

    /// <summary>
    ///  An error message from the Databento Live Subscription Gateway (LSG)
    /// </summary>
    ErrorMsg = 21,

    /// <summary>
    ///  A symbol mapping message, which maps a symbol of one SType to another
    /// </summary>
    SymbolMappingMsg = 22,

    /// <summary>
    ///  A non-error message from the Databento Live Subscription Gateway (LSG); also used for heartbeating
    /// </summary>
    SystemMsg = 23,

    /// <summary>
    ///  A statistics message; the record of the statistics schema
    /// </summary>
    StatMsg = 24,

    /// <summary>
    ///  Ohlcv 1 Second
    /// </summary>
    OhlcvMsg2 = 32,

    /// <summary>
    ///  Ohlcv 1 Minute
    /// </summary>
    OhlcvMsg3 = 33,

    /// <summary>
    ///  Ohlcv 1 Hour
    /// </summary>
    OhlcvMsg4 = 34,

    /// <summary>
    ///  Ohlcv 1 Day
    /// </summary>
    OhlcvMsg5 = 35,

    /// <summary>
    ///  Ohlcv End Of Day
    /// </summary>
    OhlcvMsg6 = 36,

    /// <summary>
    ///  A market-by-order (MBO) tick message
    /// </summary>
    MboMsg = 160,

    /// <summary>
    ///  Consolidated Market By Price 1
    /// </summary>
    Cmbp1Msg = 177,

    /// <summary>
    ///  Consolidated Bbo 1 Second
    /// </summary>
    CbboMsg = 192,

    /// <summary>
    ///  Consolidated Bbo 1 Minute
    /// </summary>
    CbboMsg2 = 193,

    /// <summary>
    ///  Trade With Consolidated Bbo
    /// </summary>
    Cmbp1Msg2 = 194,

    /// <summary>
    ///  Bbo 1 Second
    /// </summary>
    BboMsg = 195,

    /// <summary>
    ///  Bbo 1 Minute
    /// </summary>
    BboMsg2 = 196,
}
