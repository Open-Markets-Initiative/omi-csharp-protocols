using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Alt Min Quote Life: MQL duration in number of microseconds applied to orders at AltMinPriceIncrement
/// </summary>

public struct AltMinQuoteLife
{
    /// <summary>
    ///  Fix Tag for Alt Min Quote Life
    /// </summary>
    public const ushort FixTag = 37738;

    /// <summary>
    ///  Sentinel null value for Alt Min Quote Life
    /// </summary>
    public const uint NoValue = 4294967295;

    /// <summary>
    ///  Size of Alt Min Quote Life in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Alt Min Quote Life value
    /// </summary>
    public readonly uint Value
        => Decode();

    /// <summary>
    ///  Does Alt Min Quote Life field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Alt Min Quote Life
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Alt Min Quote Life
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out uint value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Alt Min Quote Life
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
        => Underlying = value;

    /// <summary>
    ///  Set Alt Min Quote Life to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Alt Min Quote Life as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal uint Underlying;
}
