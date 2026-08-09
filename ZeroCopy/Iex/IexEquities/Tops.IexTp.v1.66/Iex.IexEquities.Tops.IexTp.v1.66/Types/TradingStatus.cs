using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Trading status identifier
/// </summary>

public struct TradingStatus
{
    /// <summary>
    ///  Trading Halted Across All Us Equity Markets
    /// </summary>
    public const char TradingHaltedAcrossAllUsEquityMarkets = 'H';

    /// <summary>
    ///  Trading Halt Released Into An Order Acceptance Period On Iex
    /// </summary>
    public const char TradingHaltReleasedIntoAnOrderAcceptancePeriodOnIex = 'O';

    /// <summary>
    ///  Trading Paused And Order Acceptance Period On Iex
    /// </summary>
    public const char TradingPausedAndOrderAcceptancePeriodOnIex = 'P';

    /// <summary>
    ///  Trading On Iex
    /// </summary>
    public const char TradingOnIex = 'T';

    /// <summary>
    ///  Size of TradingStatus in bytes
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Gets the decoded Trading Status value.
    /// </summary>
    public readonly char Value
        => (char)Byte;

    /// <summary>
    ///  Decodes the raw bytes into the Trading Status value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly char Decode()
        => (char)Byte;

    /// <summary>
    ///  Encodes a value into the underlying Trading Status bytes.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(char value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the string representation of the Trading Status value.
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying byte
    /// </summary>
    internal byte Byte;
}
