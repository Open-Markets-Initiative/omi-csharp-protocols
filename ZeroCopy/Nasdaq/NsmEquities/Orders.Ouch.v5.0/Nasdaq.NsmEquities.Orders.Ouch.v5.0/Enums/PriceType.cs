using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  PriceType
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum PriceType : byte
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

    /// <summary>
    ///  market maker peg
    /// </summary>
    MarketMakerPeg = (byte)'Q',

    /// <summary>
    ///  midpoint
    /// </summary>
    Midpoint = (byte)'m',

}

/// <summary>
///  Reads and writes the Price Type field in place.
/// </summary>
public struct PriceTypeField
{
    /// <summary>
    ///  The size of the Price Type field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  PriceType
    /// </summary>
    public PriceType Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Price Type field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly PriceType Decode()
        => (PriceType)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Price Type field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(PriceType value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
