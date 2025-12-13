using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Md Entry Size: Consolidated trade quantity, notional
/// </summary>

public struct MdEntrySize
{
    /// <summary>
    ///  Fix Tag for Md Entry Size
    /// </summary>
    public const ushort FixTag = 271;

    /// <summary>
    ///  Size of Md Entry Size in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Md Entry Size value
    /// </summary>
    public readonly ulong Value
        => Decode();

    /// <summary>
    ///  Read Md Entry Size
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ulong Decode()
        => Underlying;

    /// <summary>
    ///  Write Md Entry Size
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ulong value)
        => Underlying = value;

    /// <summary>
    ///  Md Entry Size as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ulong Underlying;
}
