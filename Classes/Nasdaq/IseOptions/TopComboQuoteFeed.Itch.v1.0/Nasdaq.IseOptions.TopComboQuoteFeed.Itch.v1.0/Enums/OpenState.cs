namespace Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0;

/// <summary>
///  Reflects the current eligibility for auto execution of the options security in the options market.
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum OpenState : byte
{
    /// <summary>
    ///  Open for auto execution
    /// </summary>
    Open = (byte)'Y',

    /// <summary>
    ///  Closed for auto execution
    /// </summary>
    Closed = (byte)'N',
}
