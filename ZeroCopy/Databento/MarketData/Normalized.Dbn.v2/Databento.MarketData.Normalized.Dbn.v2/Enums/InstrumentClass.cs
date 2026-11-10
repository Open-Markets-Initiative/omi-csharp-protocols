using System.Runtime.CompilerServices;

namespace Databento.MarketData.Normalized.Dbn.v2;

/// <summary>
///  The classification of the instrument
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum InstrumentClass : byte
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
///  Reads and writes the Instrument Class field in place.
/// </summary>
public struct InstrumentClassField
{
    /// <summary>
    ///  The size of the Instrument Class field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  The classification of the instrument
    /// </summary>
    public InstrumentClass Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Instrument Class field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly InstrumentClass Decode()
        => (InstrumentClass)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Instrument Class field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(InstrumentClass value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
