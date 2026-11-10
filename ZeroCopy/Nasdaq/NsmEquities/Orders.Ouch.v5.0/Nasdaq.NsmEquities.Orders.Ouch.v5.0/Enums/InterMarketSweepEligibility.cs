using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  InterMarket Sweep Eligibility
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum InterMarketSweepEligibility : byte
{
    /// <summary>
    ///  eligible
    /// </summary>
    Eligible = (byte)'Y',

    /// <summary>
    ///  not eligible
    /// </summary>
    NotEligible = (byte)'N',

}

/// <summary>
///  Reads and writes the Inter Market Sweep Eligibility field in place.
/// </summary>
public struct InterMarketSweepEligibilityField
{
    /// <summary>
    ///  The size of the Inter Market Sweep Eligibility field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  InterMarket Sweep Eligibility
    /// </summary>
    public InterMarketSweepEligibility Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Inter Market Sweep Eligibility field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly InterMarketSweepEligibility Decode()
        => (InterMarketSweepEligibility)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Inter Market Sweep Eligibility field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(InterMarketSweepEligibility value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
