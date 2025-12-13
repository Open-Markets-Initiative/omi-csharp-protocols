using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Coupon Frequency Period: Time unit multiplier for the frequency of the bond's coupon payment
/// </summary>

public struct CouponFrequencyPeriod
{
    /// <summary>
    ///  Fix Tag for Coupon Frequency Period
    /// </summary>
    public const ushort FixTag = 1948;

    /// <summary>
    ///  Sentinel null value for Coupon Frequency Period
    /// </summary>
    public const ushort NoValue = 65535;

    /// <summary>
    ///  Size of Coupon Frequency Period in bytes
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Coupon Frequency Period value
    /// </summary>
    public readonly ushort Value
        => Decode();

    /// <summary>
    ///  Does Coupon Frequency Period field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Coupon Frequency Period
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ushort Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Coupon Frequency Period
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out ushort value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Coupon Frequency Period
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ushort value)
        => Underlying = value;

    /// <summary>
    ///  Set Coupon Frequency Period to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Coupon Frequency Period as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ushort Underlying;
}
