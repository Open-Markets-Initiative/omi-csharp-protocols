namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

/// <summary>
///  ‘E’ = Exposure ‘C’ = Facilitation ‘S’ = Solicitation ‘P’ = PIM
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum AuctionType : byte
{
    /// <summary>
    ///  Exposure
    /// </summary>
    Exposure = (byte)'E',

    /// <summary>
    ///  Facilitation
    /// </summary>
    Facilitation = (byte)'C',

    /// <summary>
    ///  Solicitation
    /// </summary>
    Solicitation = (byte)'S',

    /// <summary>
    ///  PIM
    /// </summary>
    Pim = (byte)'P',
}
