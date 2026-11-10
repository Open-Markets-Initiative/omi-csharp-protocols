namespace Nasdaq.NtxEquities.TotalView.Itch.v5_0;

/// <summary>
///  The type of order being added.
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum BuySellIndicator : byte
{
    /// <summary>
    ///  Buy Order
    /// </summary>
    BuyOrder = (byte)'B',

    /// <summary>
    ///  Sell Order
    /// </summary>
    SellOrder = (byte)'S',
}
