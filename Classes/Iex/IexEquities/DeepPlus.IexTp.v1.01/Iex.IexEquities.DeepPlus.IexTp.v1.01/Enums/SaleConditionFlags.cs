namespace Iex.IexEquities.DeepPlus.IexTp.v1_01;

/// <summary>
///  Sale Condition Flags
/// </summary>
public readonly record struct SaleConditionFlags
{
    internal SaleConditionFlags(byte value)
    {
        Value = value;
    }

    /// <summary>
    ///  Gets the integer that contains the bits.
    /// </summary>
    public byte Value { get; init; }

    /// <summary>
    ///  The bit mask of Single Price Cross Trade.
    /// </summary>
    public const byte SinglePriceCrossTradeMask = 0x8;

    /// <summary>
    ///  The position of the lowest bit of Single Price Cross Trade.
    /// </summary>
    public const int SinglePriceCrossTradeShift = 3;

    /// <summary>
    ///  The number of bits in Single Price Cross Trade.
    /// </summary>
    public const int SinglePriceCrossTradeWidth = 1;

    /// <summary>
    ///  Trade resulting from a single-price cross
    /// </summary>
    public bool SinglePriceCrossTrade
    {
        get => (Value & SinglePriceCrossTradeMask) != 0;
        init => Value = (byte)(value ? (ulong)Value | SinglePriceCrossTradeMask : (ulong)Value & ~(ulong)SinglePriceCrossTradeMask);
    }

    /// <summary>
    ///  The bit mask of Trade Through Exempt.
    /// </summary>
    public const byte TradeThroughExemptMask = 0x10;

    /// <summary>
    ///  The position of the lowest bit of Trade Through Exempt.
    /// </summary>
    public const int TradeThroughExemptShift = 4;

    /// <summary>
    ///  The number of bits in Trade Through Exempt.
    /// </summary>
    public const int TradeThroughExemptWidth = 1;

    /// <summary>
    ///  Trade is not subject to Rule 611
    /// </summary>
    public bool TradeThroughExempt
    {
        get => (Value & TradeThroughExemptMask) != 0;
        init => Value = (byte)(value ? (ulong)Value | TradeThroughExemptMask : (ulong)Value & ~(ulong)TradeThroughExemptMask);
    }

    /// <summary>
    ///  The bit mask of Odd Lot.
    /// </summary>
    public const byte OddLotMask = 0x20;

    /// <summary>
    ///  The position of the lowest bit of Odd Lot.
    /// </summary>
    public const int OddLotShift = 5;

    /// <summary>
    ///  The number of bits in Odd Lot.
    /// </summary>
    public const int OddLotWidth = 1;

    /// <summary>
    ///  Odd Lot
    /// </summary>
    public bool OddLot
    {
        get => (Value & OddLotMask) != 0;
        init => Value = (byte)(value ? (ulong)Value | OddLotMask : (ulong)Value & ~(ulong)OddLotMask);
    }

    /// <summary>
    ///  The bit mask of Extended Hours.
    /// </summary>
    public const byte ExtendedHoursMask = 0x40;

    /// <summary>
    ///  The position of the lowest bit of Extended Hours.
    /// </summary>
    public const int ExtendedHoursShift = 6;

    /// <summary>
    ///  The number of bits in Extended Hours.
    /// </summary>
    public const int ExtendedHoursWidth = 1;

    /// <summary>
    ///  Extended Hours Trade
    /// </summary>
    public bool ExtendedHours
    {
        get => (Value & ExtendedHoursMask) != 0;
        init => Value = (byte)(value ? (ulong)Value | ExtendedHoursMask : (ulong)Value & ~(ulong)ExtendedHoursMask);
    }

    /// <summary>
    ///  The bit mask of Intermarket Sweep.
    /// </summary>
    public const byte IntermarketSweepMask = 0x80;

    /// <summary>
    ///  The position of the lowest bit of Intermarket Sweep.
    /// </summary>
    public const int IntermarketSweepShift = 7;

    /// <summary>
    ///  The number of bits in Intermarket Sweep.
    /// </summary>
    public const int IntermarketSweepWidth = 1;

    /// <summary>
    ///  Intermarket Sweep Order
    /// </summary>
    public bool IntermarketSweep
    {
        get => (Value & IntermarketSweepMask) != 0;
        init => Value = (byte)(value ? (ulong)Value | IntermarketSweepMask : (ulong)Value & ~(ulong)IntermarketSweepMask);
    }
}
