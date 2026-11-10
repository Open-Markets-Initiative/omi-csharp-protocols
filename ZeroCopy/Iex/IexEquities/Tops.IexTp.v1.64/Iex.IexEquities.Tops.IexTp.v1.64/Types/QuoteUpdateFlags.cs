using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Tops.IexTp.v1_64;

/// <summary>
///  Quote Update Flags
/// </summary>
public struct QuoteUpdateFlags
{
    /// <summary>
    ///  The bits of the Market Session flag in <see cref="Value"/>.
    /// </summary>
    public const byte MarketSessionMask = unchecked((byte)0x40UL);
    /// <summary>
    ///  Market Session Flag
    /// </summary>
    public bool MarketSession
    {
        readonly get => (Value & MarketSessionMask) != 0;
        set => Value = value ? (byte)(Value | MarketSessionMask) : (byte)(Value & ~MarketSessionMask);
    }

    /// <summary>
    ///  The bits of the Symbol Availability flag in <see cref="Value"/>.
    /// </summary>
    public const byte SymbolAvailabilityMask = unchecked((byte)0x80UL);
    /// <summary>
    ///  Symbol is halted, paused, or otherwise not available for trading on IEX
    /// </summary>
    public bool SymbolAvailability
    {
        readonly get => (Value & SymbolAvailabilityMask) != 0;
        set => Value = value ? (byte)(Value | SymbolAvailabilityMask) : (byte)(Value & ~SymbolAvailabilityMask);
    }

    /// <summary>
    ///  The size of the Quote Update Flags field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Quote Update Flags
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
    ///  Writes a value into the Quote Update Flags field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(byte value)
        => Underlying = value;

    internal byte Underlying;
}
