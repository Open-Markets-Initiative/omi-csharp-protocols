namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

/// <summary>
///  Order Type
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum OrderType : byte
{
    /// <summary>
    ///  Market
    /// </summary>
    Market = (byte)'M',

    /// <summary>
    ///  Limit
    /// </summary>
    Limit = (byte)'L',
}
