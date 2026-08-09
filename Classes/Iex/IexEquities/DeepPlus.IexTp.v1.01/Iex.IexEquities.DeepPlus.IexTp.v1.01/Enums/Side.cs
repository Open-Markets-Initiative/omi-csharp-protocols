namespace Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
///  Side of order
/// </summary>
/// <remarks>
///  Each value corresponds to the ASCII byte value used as the wire-format discriminator.
/// </remarks>
public enum Side : byte
{
    /// <summary>
    ///  Buy
    /// </summary>
    Buy = (byte)'8',

    /// <summary>
    ///  Sell
    /// </summary>
    Sell = (byte)'5',
}
