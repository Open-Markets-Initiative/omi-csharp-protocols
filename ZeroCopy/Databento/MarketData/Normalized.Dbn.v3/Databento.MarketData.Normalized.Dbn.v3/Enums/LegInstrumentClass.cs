using System.Runtime.CompilerServices;

namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  The classification of the leg instrument
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum LegInstrumentClass : byte
{
    /// <summary>
    ///  Bond
    /// </summary>
    Bond = (byte)'B',

    /// <summary>
    ///  Call
    /// </summary>
    Call = (byte)'C',

    /// <summary>
    ///  Future
    /// </summary>
    Future = (byte)'F',

    /// <summary>
    ///  Index
    /// </summary>
    Index = (byte)'I',

    /// <summary>
    ///  Stock
    /// </summary>
    Stock = (byte)'K',

    /// <summary>
    ///  Mixed Spread
    /// </summary>
    MixedSpread = (byte)'M',

    /// <summary>
    ///  Put
    /// </summary>
    Put = (byte)'P',

    /// <summary>
    ///  Future Spread
    /// </summary>
    FutureSpread = (byte)'S',

    /// <summary>
    ///  Option Spread
    /// </summary>
    OptionSpread = (byte)'T',

    /// <summary>
    ///  Fx Spot
    /// </summary>
    FxSpot = (byte)'X',

    /// <summary>
    ///  Commodity Spot
    /// </summary>
    CommoditySpot = (byte)'Y',

}

/// <summary>
///  Reads and writes the Leg Instrument Class field in place.
/// </summary>
public struct LegInstrumentClassField
{
    /// <summary>
    ///  The size of the Leg Instrument Class field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  The classification of the leg instrument
    /// </summary>
    public LegInstrumentClass Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Leg Instrument Class field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly LegInstrumentClass Decode()
        => (LegInstrumentClass)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Leg Instrument Class field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(LegInstrumentClass value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
