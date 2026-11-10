namespace Nasdaq.NtxEquities.TotalView.Itch.v5_0;

/// <summary>
///  Indicates the type of price improvement available
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum InterestFlag : byte
{
    /// <summary>
    ///  PI orders available on the buy side
    /// </summary>
    BuySide = (byte)'B',

    /// <summary>
    ///  PI orders available on the sell side
    /// </summary>
    SellSide = (byte)'S',

    /// <summary>
    ///  PI orders available on both sides (buy and sell)
    /// </summary>
    BothSides = (byte)'A',

    /// <summary>
    ///  No PI orders available
    /// </summary>
    NoPiAvailable = (byte)'N',
}
