namespace Cme.Mdp3;

using System.Runtime.CompilerServices;

/// <summary>
///  Settl Price Type: Bitfield
/// </summary>

public sealed class SettlPriceType
{
    /// <summary>
    ///  Final Daily
    /// </summary>
    public const byte FinalDaily = 1 << 0;

    /// <summary>
    ///  Actual
    /// </summary>
    public const byte Actual = 1 << 1;

    /// <summary>
    ///  Rounded
    /// </summary>
    public const byte Rounded = 1 << 2;

    /// <summary>
    ///  Intraday
    /// </summary>
    public const byte Intraday = 1 << 3;

    /// <summary>
    ///  Reserved Bits
    /// </summary>
    public const byte ReservedBits = 1 << 4;

    /// <summary>
    ///  Unused Settl Price Type 5
    /// </summary>
    public const byte UnusedSettlPriceType5 = 1 << 5;

    /// <summary>
    ///  Unused Settl Price Type 6
    /// </summary>
    public const byte UnusedSettlPriceType6 = 1 << 6;

    /// <summary>
    ///  Null Value
    /// </summary>
    public const byte NullValue = 1 << 7;

    /// <summary>
    ///  Length of Settl Price Type in bytes
    /// </summary>
    public const int Length = 1;
}
