namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  Reserved for future use
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum UnpairedSide : byte
{
    /// <summary>
    ///  A sell order or sell aggressor in a trade
    /// </summary>
    Ask = (byte)'A',

    /// <summary>
    ///  A buy order or a buy aggressor in a trade
    /// </summary>
    Bid = (byte)'B',

    /// <summary>
    ///  No side specified by the original source
    /// </summary>
    None = (byte)'N',
}
