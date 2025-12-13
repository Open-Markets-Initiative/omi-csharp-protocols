using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Tradeable Size: Tradeable qty
/// </summary>

public struct TradeableSize
{
    /// <summary>
    ///  Fix Tag for Tradeable Size
    /// </summary>
    public const ushort FixTag = 37719;

    /// <summary>
    ///  Sentinel null value for Tradeable Size
    /// </summary>
    public const int NoValue = 2147483647;

    /// <summary>
    ///  Size of Tradeable Size in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Tradeable Size value
    /// </summary>
    public readonly int Value
        => Decode();

    /// <summary>
    ///  Does Tradeable Size field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Tradeable Size
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Tradeable Size
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out int value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Tradeable Size
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(int value)
        => Underlying = value;

    /// <summary>
    ///  Set Tradeable Size to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Tradeable Size as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal int Underlying;
}
