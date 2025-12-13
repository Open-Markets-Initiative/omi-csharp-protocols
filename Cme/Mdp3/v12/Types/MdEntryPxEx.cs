using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Md Entry Px Ex: Trade price
/// </summary>

public struct MdEntryPxEx
{
    /// <summary>
    ///  Fix Tag for Md Entry Px Ex
    /// </summary>
    public const ushort FixTag = 270;

    /// <summary>
    ///  Decimal place factor for Md Entry Px Ex
    /// </summary>
    public const long Factor = 1000000000;

    /// <summary>
    ///  Size of Md Entry Px Ex in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Md Entry Px Ex value
    /// </summary>
    public readonly long Value
        => Decode();

    /// <summary>
    ///  Read Md Entry Px Ex
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Decode()
        => Underlying / Factor;

    /// <summary>
    ///  Write Md Entry Px Ex
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(long value)
        => Underlying = value * Factor;

    /// <summary>
    ///  Md Entry Px Ex as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal long Underlying;
}
