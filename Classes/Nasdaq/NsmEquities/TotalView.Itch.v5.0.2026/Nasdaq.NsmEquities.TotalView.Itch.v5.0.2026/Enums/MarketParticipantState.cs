namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

/// <summary>
///  Indicates the market participant's current registration status in the issue
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum MarketParticipantState : byte
{
    /// <summary>
    ///  Active
    /// </summary>
    Active = (byte)'A',

    /// <summary>
    ///  Excused/Withdrawn
    /// </summary>
    Excused = (byte)'E',

    /// <summary>
    ///  Withdrawn
    /// </summary>
    Withdrawn = (byte)'W',

    /// <summary>
    ///  Suspended
    /// </summary>
    Suspended = (byte)'S',

    /// <summary>
    ///  Deleted
    /// </summary>
    Deleted = (byte)'D',
}
