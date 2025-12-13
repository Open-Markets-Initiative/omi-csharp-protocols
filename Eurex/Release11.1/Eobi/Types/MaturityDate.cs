using System.Runtime.CompilerServices;

namespace Eurex.Eobi;

/// <summary>
///  Maturity Date
/// </summary>

public struct MaturityDate
{
    /// <summary>
    ///  Sentinel null value for Maturity Date
    /// </summary>
    public const uint NoValue = 0xFFFFFFFF;

    /// <summary>
    ///  Maximum value for Maturity Date
    /// </summary>
    public const uint Maximum = 99991231;

    /// <summary>
    ///  Minimum value for Maturity Date
    /// </summary>
    public const uint Minimum = 18000101;

    /// <summary>
    ///  Fix Tag for Maturity Date
    /// </summary>
    public const ushort FixTag = 541;

    /// <summary>
    ///  Size of Maturity Date in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Maturity Date value
    /// </summary>
    public readonly uint Value
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
    public readonly uint Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Maturity Date
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out uint value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Maturity Date
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
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
    internal uint Underlying;
}
