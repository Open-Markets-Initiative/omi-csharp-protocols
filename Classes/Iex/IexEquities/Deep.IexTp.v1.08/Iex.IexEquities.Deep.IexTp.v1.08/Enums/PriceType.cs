namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  Price type identifier
/// </summary>
/// <remarks>
///  Each value corresponds to the ASCII byte value used as the wire-format discriminator.
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
