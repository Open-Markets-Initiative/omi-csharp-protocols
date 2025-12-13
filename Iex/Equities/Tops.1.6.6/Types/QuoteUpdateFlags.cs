namespace Iex.Tops;

using System.Runtime.CompilerServices;

/// <summary>
///  Quote Update Flags: Bitfield
/// </summary>

public sealed class QuoteUpdateFlags
{
    /// <summary>
    ///  Unused 6
    /// </summary>
    public const byte Unused6 = 1 << 0;

    /// <summary>
    ///  Market Session
    /// </summary>
    public const byte MarketSession = 1 << 6;

    /// <summary>
    ///  Symbol Availability
    /// </summary>
    public const byte SymbolAvailability = 1 << 7;

    /// <summary>
    ///  Length of Quote Update Flags in bytes
    /// </summary>
    public const int Length = 1;
}
