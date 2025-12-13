using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Original Contract Size: Fixed contract value assigned to each product
/// </summary>

public struct OriginalContractSize
{
    /// <summary>
    ///  Fix Tag for Original Contract Size
    /// </summary>
    public const ushort FixTag = 5849;

    /// <summary>
    ///  Sentinel null value for Original Contract Size
    /// </summary>
    public const int NoValue = 2147483647;

    /// <summary>
    ///  Size of Original Contract Size in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Original Contract Size value
    /// </summary>
    public readonly int Value
        => Decode();

    /// <summary>
    ///  Does Original Contract Size field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Original Contract Size
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Original Contract Size
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out int value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Original Contract Size
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(int value)
        => Underlying = value;

    /// <summary>
    ///  Set Original Contract Size to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Original Contract Size as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal int Underlying;
}
