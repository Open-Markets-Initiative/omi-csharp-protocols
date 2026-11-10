namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

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
    ///  Stock Leg
    /// </summary>
    Stock = (byte)' ',
}
