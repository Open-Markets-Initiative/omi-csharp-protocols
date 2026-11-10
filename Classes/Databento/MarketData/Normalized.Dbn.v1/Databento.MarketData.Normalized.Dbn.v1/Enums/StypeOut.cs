namespace Databento.MarketData.Normalized.Dbn.v1;

/// <summary>
///  The output symbology type
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum StypeOut : byte
{
    /// <summary>
    ///  Instrument Id
    /// </summary>
    InstrumentId = 0,

    /// <summary>
    ///  Raw Symbol
    /// </summary>
    RawSymbol = 1,

    /// <summary>
    ///  Smart
    /// </summary>
    Smart = 2,

    /// <summary>
    ///  Continuous
    /// </summary>
    Continuous = 3,

    /// <summary>
    ///  Parent
    /// </summary>
    Parent = 4,

    /// <summary>
    ///  Nasdaq Symbol
    /// </summary>
    NasdaqSymbol = 5,

    /// <summary>
    ///  Cms Symbol
    /// </summary>
    CmsSymbol = 6,

    /// <summary>
    ///  Isin
    /// </summary>
    Isin = 7,

    /// <summary>
    ///  Us Code
    /// </summary>
    UsCode = 8,

    /// <summary>
    ///  Bbg Comp Id
    /// </summary>
    BbgCompId = 9,

    /// <summary>
    ///  Bbg Comp Ticker
    /// </summary>
    BbgCompTicker = 10,

    /// <summary>
    ///  Figi
    /// </summary>
    Figi = 11,

    /// <summary>
    ///  Figi Ticker
    /// </summary>
    FigiTicker = 12,

    /// <summary>
    ///  Listing Id
    /// </summary>
    ListingId = 13,

    /// <summary>
    ///  Issuer Id
    /// </summary>
    IssuerId = 14,

    /// <summary>
    ///  Security Id
    /// </summary>
    SecurityId = 15,
}
