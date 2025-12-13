using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Min Quote Life: Minimum Quote Life in number of microseconds
/// </summary>

public struct MinQuoteLife
{
    /// <summary>
    ///  Fix Tag for Min Quote Life
    /// </summary>
    public const ushort FixTag = 37731;

    /// <summary>
    ///  Size of Min Quote Life in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Min Quote Life value
    /// </summary>
    public readonly uint Value
        => Decode();

    /// <summary>
    ///  Read Min Quote Life
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => Underlying;

    /// <summary>
    ///  Write Min Quote Life
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
        => Underlying = value;

    /// <summary>
    ///  Min Quote Life as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal uint Underlying;
}
