using System.Runtime.CompilerServices;

namespace Databento.MarketData.Normalized.Dbn.v2;

/// <summary>
///  The input symbology type
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum StypeIn : byte
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

    /// <summary>
    ///  The input symbology type
    /// </summary>
    NoValue = 255,

}

/// <summary>
///  Reads and writes the Stype In field in place.
/// </summary>
public struct StypeInField
{
    /// <summary>
    ///  The size of the Stype In field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  The value of the Stype In field when it holds no value.
    /// </summary>
    public const StypeIn NullSentinel = (StypeIn)255;

    /// <summary>
    ///  Gets a value that indicates whether the Stype In field holds a value.
    /// </summary>
    public readonly bool HasValue
        => Decode() != NullSentinel;

    /// <summary>
    ///  The input symbology type
    /// </summary>
    public StypeIn Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Stype In field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly StypeIn Decode()
        => (StypeIn)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Stype In field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(StypeIn value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;

    /// <summary>
    ///  Gets the Stype In field with no value.
    /// </summary>
    public static StypeInField Absent => global::System.Runtime.InteropServices.MemoryMarshal.Read<StypeInField>([0xFF]);
}
