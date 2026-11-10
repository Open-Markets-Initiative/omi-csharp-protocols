using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  DiscretionPriceType
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum DiscretionPriceType : byte
{
    /// <summary>
    ///  limit (default)
    /// </summary>
    Limit = (byte)'L',

    /// <summary>
    ///  market peg
    /// </summary>
    MarketPeg = (byte)'P',

    /// <summary>
    ///  midpoint peg
    /// </summary>
    MidpointPeg = (byte)'M',

    /// <summary>
    ///  primary peg
    /// </summary>
    PrimaryPeg = (byte)'R',

}

/// <summary>
///  Reads and writes the Discretion Price Type field in place.
/// </summary>
public struct DiscretionPriceTypeField
{
    /// <summary>
    ///  The size of the Discretion Price Type field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  DiscretionPriceType
    /// </summary>
    public DiscretionPriceType Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Discretion Price Type field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly DiscretionPriceType Decode()
        => (DiscretionPriceType)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Discretion Price Type field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(DiscretionPriceType value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
