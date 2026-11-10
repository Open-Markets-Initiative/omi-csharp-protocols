using System.Runtime.CompilerServices;

namespace Databento.MarketData.Normalized.Dbn.v3;

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

/// <summary>
///  Reads and writes the Stype Out field in place.
/// </summary>
public struct StypeOutField
{
    /// <summary>
    ///  The size of the Stype Out field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  The output symbology type
    /// </summary>
    public StypeOut Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Stype Out field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly StypeOut Decode()
        => (StypeOut)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Stype Out field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(StypeOut value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
