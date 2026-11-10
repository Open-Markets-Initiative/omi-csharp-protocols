namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

/// <summary>
///  Indicates if the market participant firm qualifies as a Primary Market Maker in accordance with NASDAQ marketplace rules
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum PrimaryMarketMaker : byte
{
    /// <summary>
    ///  Primary market maker
    /// </summary>
    Primary = (byte)'Y',

    /// <summary>
    ///  Non-primary market maker
    /// </summary>
    NonPrimary = (byte)'N',
}
