using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  BBO Weight Indicator
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum BboWeightIndicator : byte
{
    /// <summary>
    ///  0-0.2%
    /// </summary>
    Weight0To02Percent = (byte)'0',

    /// <summary>
    ///  0.2%-1%
    /// </summary>
    Weight02To1Percent = (byte)'1',

    /// <summary>
    ///  1%-2%
    /// </summary>
    Weight1To2Percent = (byte)'2',

    /// <summary>
    ///  greater than 2%
    /// </summary>
    GreaterThan2 = (byte)'3',

    /// <summary>
    ///  unspecified (default)
    /// </summary>
    Unspecified = (byte)' ',

    /// <summary>
    ///  Sets the QBBO while joining the NBBO
    /// </summary>
    SetsTheQbboWhileJoiningTheNbbo = (byte)'S',

    /// <summary>
    ///  Improves the NBBO upon entry
    /// </summary>
    ImprovesTheNbboUponEntry = (byte)'N',

}

/// <summary>
///  Reads and writes the Bbo Weight Indicator field in place.
/// </summary>
public struct BboWeightIndicatorField
{
    /// <summary>
    ///  The size of the Bbo Weight Indicator field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  BBO Weight Indicator
    /// </summary>
    public BboWeightIndicator Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Bbo Weight Indicator field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly BboWeightIndicator Decode()
        => (BboWeightIndicator)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Bbo Weight Indicator field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(BboWeightIndicator value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
