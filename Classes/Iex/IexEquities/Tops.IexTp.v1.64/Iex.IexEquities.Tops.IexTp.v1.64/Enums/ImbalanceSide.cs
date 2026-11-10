namespace Iex.IexEquities.Tops.IexTp.v1_64;

/// <summary>
///  Side of the unpaired shares at the Reference Price using orders on the Auction Book
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum ImbalanceSide : byte
{
    /// <summary>
    ///  Buy
    /// </summary>
    Buy = (byte)'B',

    /// <summary>
    ///  Sell
    /// </summary>
    Sell = (byte)'S',

    /// <summary>
    ///  None
    /// </summary>
    None = (byte)'N',
}
