namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

/// <summary>
///  Scope value
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum Scope : byte
{
    /// <summary>
    ///  Local
    /// </summary>
    Local = (byte)'L',

    /// <summary>
    ///  National
    /// </summary>
    National = (byte)'N',
}
