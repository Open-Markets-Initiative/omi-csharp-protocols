namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

/// <summary>
///  The market side of the order imbalance
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum ImbalanceDirection : byte
{
    /// <summary>
    ///  Buy imbalance
    /// </summary>
    Buy = (byte)'B',

    /// <summary>
    ///  Sell imbalance
    /// </summary>
    Sell = (byte)'S',

    /// <summary>
    ///  No imbalance
    /// </summary>
    None = (byte)'N',

    /// <summary>
    ///  Insufficient orders to calculate
    /// </summary>
    InsufficientOrders = (byte)'O',

    /// <summary>
    ///  Paused
    /// </summary>
    Paused = (byte)'P',
}
