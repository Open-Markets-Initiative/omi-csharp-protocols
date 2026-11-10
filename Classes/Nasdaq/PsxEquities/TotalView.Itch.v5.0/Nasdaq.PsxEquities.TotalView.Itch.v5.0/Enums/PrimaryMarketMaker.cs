namespace Nasdaq.PsxEquities.TotalView.Itch.v5_0;

/// <summary>
///  Indicates if the market participant firm qualifies as a Primary Market Maker in accordance with NASDAQ marketplace rules
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum PrimaryMarketMaker : byte
{
    /// <summary>
    ///  primary market maker
    /// </summary>
    Primary = (byte)'Y',

    /// <summary>
    ///  non-primary market maker
    /// </summary>
    NonPrimary = (byte)'N',
}
