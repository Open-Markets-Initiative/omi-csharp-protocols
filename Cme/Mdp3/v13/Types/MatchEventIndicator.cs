namespace Cme.Mdp3;

using System.Runtime.CompilerServices;

/// <summary>
///  Match Event Indicator: Bitfield
/// </summary>

public sealed class MatchEventIndicator
{
    /// <summary>
    ///  Last Trade Msg
    /// </summary>
    public const byte LastTradeMsg = 1 << 0;

    /// <summary>
    ///  Last Volume Msg
    /// </summary>
    public const byte LastVolumeMsg = 1 << 1;

    /// <summary>
    ///  Last Quote Msg
    /// </summary>
    public const byte LastQuoteMsg = 1 << 2;

    /// <summary>
    ///  Last Stats Msg
    /// </summary>
    public const byte LastStatsMsg = 1 << 3;

    /// <summary>
    ///  Last Implied Msg
    /// </summary>
    public const byte LastImpliedMsg = 1 << 4;

    /// <summary>
    ///  Recovery Msg
    /// </summary>
    public const byte RecoveryMsg = 1 << 5;

    /// <summary>
    ///  Reserved
    /// </summary>
    public const byte Reserved = 1 << 6;

    /// <summary>
    ///  End Of Event
    /// </summary>
    public const byte EndOfEvent = 1 << 7;

    /// <summary>
    ///  Length of Match Event Indicator in bytes
    /// </summary>
    public const int Length = 1;
}
