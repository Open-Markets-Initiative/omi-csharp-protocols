using System.Runtime.CompilerServices;

namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

/// <summary>
///  Reflects the current trading state for the strategy on the ISE market.
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum CurrentTradingState : byte
{
    /// <summary>
    ///  Halt in effect
    /// </summary>
    HaltInEffect = (byte)'H',

    /// <summary>
    ///  Trading Resumed
    /// </summary>
    TradingResumed = (byte)'T',

}

/// <summary>
///  Reads and writes the Current Trading State field in place.
/// </summary>
public struct CurrentTradingStateField
{
    /// <summary>
    ///  The size of the Current Trading State field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Reflects the current trading state for the strategy on the ISE market.
    /// </summary>
    public CurrentTradingState Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Current Trading State field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly CurrentTradingState Decode()
        => (CurrentTradingState)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Current Trading State field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(CurrentTradingState value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
