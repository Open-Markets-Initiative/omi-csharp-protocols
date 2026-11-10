namespace Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0;

/// <summary>
///  Option Type:
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum OptionType : byte
{
    /// <summary>
    ///  Call option
    /// </summary>
    Call = (byte)'C',

    /// <summary>
    ///  Put option
    /// </summary>
    Put = (byte)'P',

    /// <summary>
    ///  Blank for Stock Leg
    /// </summary>
    Na = (byte)' ',
}
