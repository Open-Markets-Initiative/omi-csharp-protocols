using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Cleared Volume: The total cleared volume of instrument traded during the prior trading session.
/// </summary>

public struct ClearedVolume
{
    /// <summary>
    ///  Fix Tag for Cleared Volume
    /// </summary>
    public const ushort FixTag = 5791;

    /// <summary>
    ///  Sentinel null value for Cleared Volume
    /// </summary>
    public const int NoValue = 2147483647;

    /// <summary>
    ///  Size of Cleared Volume in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Cleared Volume value
    /// </summary>
    public readonly int Value
        => Decode();

    /// <summary>
    ///  Does Cleared Volume field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Cleared Volume
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Cleared Volume
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out int value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Cleared Volume
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(int value)
        => Underlying = value;

    /// <summary>
    ///  Set Cleared Volume to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Cleared Volume as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal int Underlying;
}
