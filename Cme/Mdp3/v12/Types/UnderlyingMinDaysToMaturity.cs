using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Underlying Min Days To Maturity: Minimum days to maturity remaining of the underlying instruments to qualify for GC basket. Will contain null value for Repo specials
/// </summary>

public struct UnderlyingMinDaysToMaturity
{
    /// <summary>
    ///  Fix Tag for Underlying Min Days To Maturity
    /// </summary>
    public const ushort FixTag = 37718;

    /// <summary>
    ///  Sentinel null value for Underlying Min Days To Maturity
    /// </summary>
    public const ushort NoValue = 65535;

    /// <summary>
    ///  Size of Underlying Min Days To Maturity in bytes
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Underlying Min Days To Maturity value
    /// </summary>
    public readonly ushort Value
        => Decode();

    /// <summary>
    ///  Does Underlying Min Days To Maturity field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Underlying Min Days To Maturity
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ushort Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Underlying Min Days To Maturity
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out ushort value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Underlying Min Days To Maturity
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ushort value)
        => Underlying = value;

    /// <summary>
    ///  Set Underlying Min Days To Maturity to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Underlying Min Days To Maturity as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ushort Underlying;
}
