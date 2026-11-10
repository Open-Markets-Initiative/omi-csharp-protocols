namespace Databento.MarketData.Normalized.Dbn.v1;

/// <summary>
///  The state of trading in the instrument
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum IsTrading : byte
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
