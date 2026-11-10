namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

/// <summary>
///  Market Code
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum MarketCode : byte
{
    /// <summary>
    ///  Nasdaq
    /// </summary>
    Nasdaq = (byte)'Q',

    /// <summary>
    ///  BX
    /// </summary>
    Bx = (byte)'B',

    /// <summary>
    ///  PSX
    /// </summary>
    Psx = (byte)'X',
}
