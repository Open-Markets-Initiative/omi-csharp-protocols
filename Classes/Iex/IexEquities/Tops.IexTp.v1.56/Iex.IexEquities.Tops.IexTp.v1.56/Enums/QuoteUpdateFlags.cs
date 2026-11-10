namespace Iex.IexEquities.Tops.IexTp.v1_56;

/// <summary>
///  Quote Update Flags
/// </summary>
public readonly record struct QuoteUpdateFlags
{
    internal QuoteUpdateFlags(byte value)
    {
        Value = value;
    }

    /// <summary>
    ///  Gets the integer that contains the bits.
    /// </summary>
    public byte Value { get; init; }

    /// <summary>
    ///  The bit mask of Market Session.
    /// </summary>
    public const byte MarketSessionMask = 0x40;

    /// <summary>
    ///  The position of the lowest bit of Market Session.
    /// </summary>
    public const int MarketSessionShift = 6;

    /// <summary>
    ///  The number of bits in Market Session.
    /// </summary>
    public const int MarketSessionWidth = 1;

    /// <summary>
    ///  Market Session Flag
    /// </summary>
    public bool MarketSession
    {
        get => (Value & MarketSessionMask) != 0;
        init => Value = (byte)(value ? (ulong)Value | MarketSessionMask : (ulong)Value & ~(ulong)MarketSessionMask);
    }

    /// <summary>
    ///  The bit mask of Symbol Availability.
    /// </summary>
    public const byte SymbolAvailabilityMask = 0x80;

    /// <summary>
    ///  The position of the lowest bit of Symbol Availability.
    /// </summary>
    public const int SymbolAvailabilityShift = 7;

    /// <summary>
    ///  The number of bits in Symbol Availability.
    /// </summary>
    public const int SymbolAvailabilityWidth = 1;

    /// <summary>
    ///  Symbol is halted, paused, or otherwise not available for trading on IEX
    /// </summary>
    public bool SymbolAvailability
    {
        get => (Value & SymbolAvailabilityMask) != 0;
        init => Value = (byte)(value ? (ulong)Value | SymbolAvailabilityMask : (ulong)Value & ~(ulong)SymbolAvailabilityMask);
    }
}
