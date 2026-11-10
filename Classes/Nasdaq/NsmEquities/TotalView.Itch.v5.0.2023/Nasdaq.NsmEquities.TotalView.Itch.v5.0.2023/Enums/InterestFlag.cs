namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

/// <summary>
///  Interest Flag
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum InterestFlag : byte
{
    /// <summary>
    ///  RPI orders available on the buy side
    /// </summary>
    BuySide = (byte)'B',

    /// <summary>
    ///  RPI orders available on the sell side
    /// </summary>
    SellSide = (byte)'S',

    /// <summary>
    ///  RPI orders available on both sides (buy and sell)
    /// </summary>
    BothSides = (byte)'A',

    /// <summary>
    ///  No RPI Orders Available
    /// </summary>
    NoRpiOrdersAvailable = (byte)'N',
}
