using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  TradeNow
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum TradeNow : byte
{
    /// <summary>
    ///  yes
    /// </summary>
    Yes = (byte)'Y',

    /// <summary>
    ///  no (default)
    /// </summary>
    No = (byte)'N',

}

/// <summary>
///  Reads and writes the Trade Now field in place.
/// </summary>
public struct TradeNowField
{
    /// <summary>
    ///  The size of the Trade Now field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  TradeNow
    /// </summary>
    public TradeNow Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Trade Now field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly TradeNow Decode()
        => (TradeNow)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Trade Now field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(TradeNow value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
