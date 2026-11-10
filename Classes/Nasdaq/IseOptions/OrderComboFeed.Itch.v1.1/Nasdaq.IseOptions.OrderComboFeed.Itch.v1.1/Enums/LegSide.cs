namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

/// <summary>
///  Indicates the side of the leg
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum LegSide : byte
{
    /// <summary>
    ///  Buy
    /// </summary>
    Buy = (byte)'B',

    /// <summary>
    ///  Sell
    /// </summary>
    Sell = (byte)'S',
}
