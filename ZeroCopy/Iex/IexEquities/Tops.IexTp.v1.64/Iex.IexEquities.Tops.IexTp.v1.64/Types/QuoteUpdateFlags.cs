using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Quote Update Flags: Bitfield
/// </summary>

public struct QuoteUpdateFlags
{
    /// <summary>
    ///  Applies to the container's normalized Value, not the raw underlying bytes.
    /// </summary>
    public const byte MarketSessionMask = unchecked((byte)0x40UL);
    /// <summary>
    ///  Market Session
    /// </summary>
    public readonly bool MarketSession => (Value & MarketSessionMask) != 0;

    /// <summary>
    ///  Applies to the container's normalized Value, not the raw underlying bytes.
    /// </summary>
    public const byte SymbolAvailabilityMask = unchecked((byte)0x80UL);
    /// <summary>
    ///  Symbol Availability
    /// </summary>
    public readonly bool SymbolAvailability => (Value & SymbolAvailabilityMask) != 0;

    /// <summary>
    ///  Size of QuoteUpdateFlags in bytes
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Quote Update Flags value
    /// </summary>
    public readonly byte Value
        => Underlying;

    /// <summary>
    ///  Quote Update Flags as string
    /// </summary>
    public readonly override string ToString()
        => $"0x{Value:X}";

    /// <summary>
    ///  Encodes a Quote Update Flags value into the underlying bytes
    /// </summary>
    public void Encode(byte value)
        => Underlying = value;

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal byte Underlying;
}
