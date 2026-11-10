namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  The reason the trade was broken.
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum BrokenTradeReason : byte
{
    /// <summary>
    ///  Erroneous (trade deemed clearly erroneous)
    /// </summary>
    Erroneous = (byte)'E',

    /// <summary>
    ///  Consent (both parties agreed to break the trade)
    /// </summary>
    Consent = (byte)'C',

    /// <summary>
    ///  Supervisory (manually broken by supervisory)
    /// </summary>
    Supervisory = (byte)'S',

    /// <summary>
    ///  External (trade broken by an external third party)
    /// </summary>
    External = (byte)'X',
}
