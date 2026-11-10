using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Qbbo.Itch.v2_1;

/// <summary>
///  Indicates Listing market or listing market tier for the issue
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum MarketCategory : byte
{
    /// <summary>
    ///  Nasdaq Global Select Market
    /// </summary>
    NasdaqGlobalSelectMarket = (byte)'Q',

    /// <summary>
    ///  Nasdaq Global Market
    /// </summary>
    NasdaqGlobalMarket = (byte)'G',

    /// <summary>
    ///  Nasdaq Capital Market
    /// </summary>
    NasdaqCapitalMarket = (byte)'S',

    /// <summary>
    ///  New York Stock Exchange (NYSE)
    /// </summary>
    NewYorkStockExchange = (byte)'N',

    /// <summary>
    ///  NYSE American
    /// </summary>
    NyseAmerican = (byte)'A',

    /// <summary>
    ///  NYSE Arca
    /// </summary>
    NyseArca = (byte)'P',

    /// <summary>
    ///  NYSE Texas
    /// </summary>
    NyseTexas = (byte)'M',

    /// <summary>
    ///  BATS BZX Exchange
    /// </summary>
    BatsBzxExchange = (byte)'Z',

    /// <summary>
    ///  Investors' Exchange, LLC
    /// </summary>
    InvestorsExchangeLlc = (byte)'V',

    /// <summary>
    ///  Not Available
    /// </summary>
    NotAvailable = (byte)' ',

}

/// <summary>
///  Reads and writes the Market Category field in place.
/// </summary>
public struct MarketCategoryField
{
    /// <summary>
    ///  The size of the Market Category field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Indicates Listing market or listing market tier for the issue
    /// </summary>
    public MarketCategory Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Market Category field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly MarketCategory Decode()
        => (MarketCategory)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Market Category field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(MarketCategory value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
