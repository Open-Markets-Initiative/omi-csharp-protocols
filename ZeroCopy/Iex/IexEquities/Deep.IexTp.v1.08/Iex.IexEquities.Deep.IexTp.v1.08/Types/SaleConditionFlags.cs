using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  Sale Condition Flags: Bitfield
/// </summary>

public struct SaleConditionFlags
{
    /// <summary>
    ///  Applies to the container's normalized Value, not the raw underlying bytes.
    /// </summary>
    public const byte SinglepriceCrossTradeMask = unchecked((byte)0x8UL);
    /// <summary>
    ///  Singleprice Cross Trade
    /// </summary>
    public readonly bool SinglepriceCrossTrade => (Value & SinglepriceCrossTradeMask) != 0;

    /// <summary>
    ///  Applies to the container's normalized Value, not the raw underlying bytes.
    /// </summary>
    public const byte TradeThroughExemptMask = unchecked((byte)0x10UL);
    /// <summary>
    ///  Trade Through Exempt
    /// </summary>
    public readonly bool TradeThroughExempt => (Value & TradeThroughExemptMask) != 0;

    /// <summary>
    ///  Applies to the container's normalized Value, not the raw underlying bytes.
    /// </summary>
    public const byte OddLotMask = unchecked((byte)0x20UL);
    /// <summary>
    ///  Odd Lot
    /// </summary>
    public readonly bool OddLot => (Value & OddLotMask) != 0;

    /// <summary>
    ///  Applies to the container's normalized Value, not the raw underlying bytes.
    /// </summary>
    public const byte ExtendedHoursMask = unchecked((byte)0x40UL);
    /// <summary>
    ///  Extended Hours
    /// </summary>
    public readonly bool ExtendedHours => (Value & ExtendedHoursMask) != 0;

    /// <summary>
    ///  Applies to the container's normalized Value, not the raw underlying bytes.
    /// </summary>
    public const byte IntermarketSweepMask = unchecked((byte)0x80UL);
    /// <summary>
    ///  Intermarket Sweep
    /// </summary>
    public readonly bool IntermarketSweep => (Value & IntermarketSweepMask) != 0;

    /// <summary>
    ///  Size of SaleConditionFlags in bytes
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Sale Condition Flags value
    /// </summary>
    public readonly byte Value
        => Underlying;

    /// <summary>
    ///  Sale Condition Flags as string
    /// </summary>
    public readonly override string ToString()
        => $"0x{Value:X}";

    /// <summary>
    ///  Encodes a Sale Condition Flags value into the underlying bytes
    /// </summary>
    public void Encode(byte value)
        => Underlying = value;

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal byte Underlying;
}
