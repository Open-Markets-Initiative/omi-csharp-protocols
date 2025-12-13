using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Maturity Date: Maturity Date
/// </summary>

public struct MaturityDate
{
    /// <summary>
    ///  Fix Tag for Maturity Date
    /// </summary>
    public const ushort FixTag = 541;

    /// <summary>
    ///  Sentinel null value for Maturity Date
    /// </summary>
    public const ushort NoValue = 65535;

    /// <summary>
    ///  Size of Maturity Date in bytes
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Maturity Date value
    /// </summary>
    public readonly ushort Value
        => Decode();

    /// <summary>
    ///  Does Maturity Date field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Maturity Date
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ushort Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Maturity Date
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out ushort value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Maturity Date
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ushort value)
        => Underlying = value;

    /// <summary>
    ///  Set Maturity Date to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Maturity Date as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ushort Underlying;
}
