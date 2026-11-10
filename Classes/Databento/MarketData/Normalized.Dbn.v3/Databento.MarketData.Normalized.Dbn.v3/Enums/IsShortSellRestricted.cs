namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  The state of short sell restrictions for the instrument
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum IsShortSellRestricted : byte
{
    /// <summary>
    ///  Not Available
    /// </summary>
    NotAvailable = (byte)'~',

    /// <summary>
    ///  No
    /// </summary>
    No = (byte)'N',

    /// <summary>
    ///  Yes
    /// </summary>
    Yes = (byte)'Y',
}
