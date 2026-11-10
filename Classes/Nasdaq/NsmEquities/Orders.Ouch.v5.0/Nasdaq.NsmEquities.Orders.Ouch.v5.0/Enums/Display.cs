namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Display
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum Display : byte
{
    /// <summary>
    ///  Attributable
    /// </summary>
    Attributable = (byte)'A',

    /// <summary>
    ///  Visible
    /// </summary>
    Visible = (byte)'Y',

    /// <summary>
    ///  Hidden
    /// </summary>
    Hidden = (byte)'N',

    /// <summary>
    ///  conformant
    /// </summary>
    Conformant = (byte)'Z',
}
