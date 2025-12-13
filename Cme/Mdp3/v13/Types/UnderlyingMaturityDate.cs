using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Underlying Maturity Date: Underlying Security's Maturity Date. Will be populated with Maturity Date of the underlying security instrument for Repo Specials only
/// </summary>

public struct UnderlyingMaturityDate
{
    /// <summary>
    ///  Fix Tag for Underlying Maturity Date
    /// </summary>
    public const ushort FixTag = 542;

    /// <summary>
    ///  Sentinel null value for Underlying Maturity Date
    /// </summary>
    public const ushort NoValue = 65535;

    /// <summary>
    ///  Size of Underlying Maturity Date in bytes
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Underlying Maturity Date value
    /// </summary>
    public readonly ushort Value
        => Decode();

    /// <summary>
    ///  Does Underlying Maturity Date field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Underlying Maturity Date
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ushort Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Underlying Maturity Date
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out ushort value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Underlying Maturity Date
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ushort value)
        => Underlying = value;

    /// <summary>
    ///  Set Underlying Maturity Date to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Underlying Maturity Date as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ushort Underlying;
}
