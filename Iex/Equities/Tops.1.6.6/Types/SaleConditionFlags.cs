namespace Iex.Tops;

using System.Runtime.CompilerServices;

/// <summary>
///  Sale Condition Flags: Bitfield
/// </summary>

public sealed class SaleConditionFlags
{
    /// <summary>
    ///  Unused 3
    /// </summary>
    public const byte Unused3 = 1 << 0;

    /// <summary>
    ///  Singleprice Cross Trade
    /// </summary>
    public const byte SinglepriceCrossTrade = 1 << 3;

    /// <summary>
    ///  Trade Through Exempt
    /// </summary>
    public const byte TradeThroughExempt = 1 << 4;

    /// <summary>
    ///  Odd Lot
    /// </summary>
    public const byte OddLot = 1 << 5;

    /// <summary>
    ///  Extended Hours
    /// </summary>
    public const byte ExtendedHours = 1 << 6;

    /// <summary>
    ///  Intermarket Sweep
    /// </summary>
    public const byte IntermarketSweep = 1 << 7;

    /// <summary>
    ///  Length of Sale Condition Flags in bytes
    /// </summary>
    public const int Length = 1;
}
