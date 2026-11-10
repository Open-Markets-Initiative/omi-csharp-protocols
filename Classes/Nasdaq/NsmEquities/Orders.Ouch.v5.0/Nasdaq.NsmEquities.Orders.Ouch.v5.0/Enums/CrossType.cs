namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  CrossType
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum CrossType : byte
{
    /// <summary>
    ///  continuous market
    /// </summary>
    ContinuousMarket = (byte)'N',

    /// <summary>
    ///  opening cross (Nasdaq only)
    /// </summary>
    OpeningCross = (byte)'O',

    /// <summary>
    ///  closing cross (Nasdaq only)
    /// </summary>
    ClosingCross = (byte)'C',

    /// <summary>
    ///  halt/IPO (Nasdaq only)
    /// </summary>
    HaltIpo = (byte)'H',

    /// <summary>
    ///  supplemental (Nasdaq only)
    /// </summary>
    Supplemental = (byte)'S',

    /// <summary>
    ///  retail (BX only)
    /// </summary>
    Retail = (byte)'R',

    /// <summary>
    ///  extended life (Nasdaq only)
    /// </summary>
    ExtendedLife = (byte)'E',

    /// <summary>
    ///  after hours close (Nasdaq only)
    /// </summary>
    AfterHoursClose = (byte)'A',
}
