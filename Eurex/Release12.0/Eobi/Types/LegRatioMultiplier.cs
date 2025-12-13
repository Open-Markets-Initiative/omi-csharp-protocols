using System.Runtime.CompilerServices;

namespace Eurex.Eobi;

/// <summary>
///  Leg Ratio Multiplier
/// </summary>

public struct LegRatioMultiplier
{
    /// <summary>
    ///  Sentinel null value for Leg Ratio Multiplier
    /// </summary>
    public const uint NoValue = 0xFFFFFFFF;

    /// <summary>
    ///  Maximum value for Leg Ratio Multiplier
    /// </summary>
    public const uint Maximum = 2147483647;

    /// <summary>
    ///  Minimum value for Leg Ratio Multiplier
    /// </summary>
    public const uint Minimum = 1;

    /// <summary>
    ///  Fix Tag for Leg Ratio Multiplier
    /// </summary>
    public const ushort FixTag = 28900;

    /// <summary>
    ///  Size of Leg Ratio Multiplier in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Leg Ratio Multiplier value
    /// </summary>
    public readonly uint Value
        => Decode();

    /// <summary>
    ///  Does Leg Ratio Multiplier field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Leg Ratio Multiplier
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Leg Ratio Multiplier
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out uint value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Leg Ratio Multiplier
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
        => Underlying = value;

    /// <summary>
    ///  Set Leg Ratio Multiplier to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Leg Ratio Multiplier as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal uint Underlying;
}
