namespace Iex.IexEquities.DeepPlus.IexTp.v1_05;

/// <summary>
///  Side of order
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
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
