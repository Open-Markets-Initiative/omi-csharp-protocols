namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

/// <summary>
///  Denotes the MWCB Level that was breached
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum BreachedLevel : byte
{
    /// <summary>
    ///  Level 1
    /// </summary>
    Level1 = (byte)'1',

    /// <summary>
    ///  Level 2
    /// </summary>
    Level2 = (byte)'2',

    /// <summary>
    ///  Level 3
    /// </summary>
    Level3 = (byte)'3',
}
