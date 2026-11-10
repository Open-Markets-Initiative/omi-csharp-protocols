namespace Iex.IexEquities.Tops.IexTp.v1_64;

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
