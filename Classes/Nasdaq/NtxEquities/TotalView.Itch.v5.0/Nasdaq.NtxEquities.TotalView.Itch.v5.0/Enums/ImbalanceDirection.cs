namespace Nasdaq.NtxEquities.TotalView.Itch.v5_0;

/// <summary>
///  The market side of the order imbalance.
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum ImbalanceDirection : byte
{
    /// <summary>
    ///  buy imbalance
    /// </summary>
    Buy = (byte)'B',

    /// <summary>
    ///  sell imbalance
    /// </summary>
    Sell = (byte)'S',

    /// <summary>
    ///  no imbalance
    /// </summary>
    NoImbalance = (byte)'N',

    /// <summary>
    ///  Insufficient orders to calculate
    /// </summary>
    Insufficient = (byte)'O',
}
