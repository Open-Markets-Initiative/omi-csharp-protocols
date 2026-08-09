namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Side of the unpaired shares at the Reference Price using orders on the Auction Book
/// </summary>
/// <remarks>
///  Each value corresponds to the ASCII byte value used as the wire-format discriminator.
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
    ///  No Imbalance
    /// </summary>
    None = (byte)'N',
}
