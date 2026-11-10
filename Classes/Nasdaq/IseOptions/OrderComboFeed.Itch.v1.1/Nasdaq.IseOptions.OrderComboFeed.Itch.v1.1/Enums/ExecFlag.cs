namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

/// <summary>
///  ‘N’ = None ‘A’ = AON
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum ExecFlag : byte
{
    /// <summary>
    ///  None
    /// </summary>
    None = (byte)'N',

    /// <summary>
    ///  AON
    /// </summary>
    Aon = (byte)'A',
}
