namespace Iex.IexEquities.Deep.IexTp.v1_06;

/// <summary>
///  Price type identifier
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum PriceType : byte
{
    /// <summary>
    ///  Iex Official Opening Price
    /// </summary>
    IexOfficialOpeningPrice = (byte)'Q',

    /// <summary>
    ///  Iex Official Closing Price
    /// </summary>
    IexOfficialClosingPrice = (byte)'M',
}
