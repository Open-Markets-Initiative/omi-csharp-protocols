using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Tops.IexTp.v1_64;

/// <summary>
///  Sale Condition Flags
/// </summary>
public struct SaleConditionFlags
{
    /// <summary>
    ///  The bits of the Single Price Cross Trade flag in <see cref="Value"/>.
    /// </summary>
    public const byte SinglePriceCrossTradeMask = unchecked((byte)0x8UL);
    /// <summary>
    ///  Trade resulting from a single-price cross
    /// </summary>
    public bool SinglePriceCrossTrade
    {
        readonly get => (Value & SinglePriceCrossTradeMask) != 0;
        set => Value = value ? (byte)(Value | SinglePriceCrossTradeMask) : (byte)(Value & ~SinglePriceCrossTradeMask);
    }

    /// <summary>
    ///  The bits of the Trade Through Exempt flag in <see cref="Value"/>.
    /// </summary>
    public const byte TradeThroughExemptMask = unchecked((byte)0x10UL);
    /// <summary>
    ///  Trade is not subject to Rule 611
    /// </summary>
    public bool TradeThroughExempt
    {
        readonly get => (Value & TradeThroughExemptMask) != 0;
        set => Value = value ? (byte)(Value | TradeThroughExemptMask) : (byte)(Value & ~TradeThroughExemptMask);
    }

    /// <summary>
    ///  The bits of the Odd Lot flag in <see cref="Value"/>.
    /// </summary>
    public const byte OddLotMask = unchecked((byte)0x20UL);
    /// <summary>
    ///  Odd Lot
    /// </summary>
    public bool OddLot
    {
        readonly get => (Value & OddLotMask) != 0;
        set => Value = value ? (byte)(Value | OddLotMask) : (byte)(Value & ~OddLotMask);
    }

    /// <summary>
    ///  The bits of the Extended Hours flag in <see cref="Value"/>.
    /// </summary>
    public const byte ExtendedHoursMask = unchecked((byte)0x40UL);
    /// <summary>
    ///  Extended Hours Trade
    /// </summary>
    public bool ExtendedHours
    {
        readonly get => (Value & ExtendedHoursMask) != 0;
        set => Value = value ? (byte)(Value | ExtendedHoursMask) : (byte)(Value & ~ExtendedHoursMask);
    }

    /// <summary>
    ///  The bits of the Intermarket Sweep flag in <see cref="Value"/>.
    /// </summary>
    public const byte IntermarketSweepMask = unchecked((byte)0x80UL);
    /// <summary>
    ///  Intermarket Sweep Order
    /// </summary>
    public bool IntermarketSweep
    {
        readonly get => (Value & IntermarketSweepMask) != 0;
        set => Value = value ? (byte)(Value | IntermarketSweepMask) : (byte)(Value & ~IntermarketSweepMask);
    }

    /// <summary>
    ///  The size of the Sale Condition Flags field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Sale Condition Flags
    /// </summary>
    public byte Value
    {
        readonly get => Underlying;
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => $"0x{Value:X}";

    /// <summary>
    ///  Writes a value into the Sale Condition Flags field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(byte value)
        => Underlying = value;

    internal byte Underlying;
}
