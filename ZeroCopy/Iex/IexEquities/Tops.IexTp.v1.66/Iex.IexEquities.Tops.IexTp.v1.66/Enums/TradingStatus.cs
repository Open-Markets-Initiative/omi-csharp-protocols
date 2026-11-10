using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Tops.IexTp.v1_66;

/// <summary>
///  Trading status identifier
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum TradingStatus : byte
{
    /// <summary>
    ///  Trading Halted Across All Us Equity Markets
    /// </summary>
    TradingHaltedAcrossAllUsEquityMarkets = (byte)'H',

    /// <summary>
    ///  Trading Halt Released Into An Order Acceptance Period On Iex
    /// </summary>
    TradingHaltReleasedIntoAnOrderAcceptancePeriodOnIex = (byte)'O',

    /// <summary>
    ///  Trading Paused And Order Acceptance Period On Iex
    /// </summary>
    TradingPausedAndOrderAcceptancePeriodOnIex = (byte)'P',

    /// <summary>
    ///  Trading On Iex
    /// </summary>
    TradingOnIex = (byte)'T',

}

/// <summary>
///  Reads and writes the Trading Status field in place.
/// </summary>
public struct TradingStatusField
{
    /// <summary>
    ///  The size of the Trading Status field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Trading status identifier
    /// </summary>
    public TradingStatus Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Trading Status field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly TradingStatus Decode()
        => (TradingStatus)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Trading Status field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(TradingStatus value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
