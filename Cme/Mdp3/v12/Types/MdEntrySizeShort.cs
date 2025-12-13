using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Md Entry Size Short: Cumulative traded volume
/// </summary>

public struct MdEntrySizeShort
{
    /// <summary>
    ///  Fix Tag for Md Entry Size Short
    /// </summary>
    public const ushort FixTag = 271;

    /// <summary>
    ///  Size of Md Entry Size Short in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Md Entry Size Short value
    /// </summary>
    public readonly int Value
        => Decode();

    /// <summary>
    ///  Read Md Entry Size Short
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int Decode()
        => Underlying;

    /// <summary>
    ///  Write Md Entry Size Short
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(int value)
        => Underlying = value;

    /// <summary>
    ///  Md Entry Size Short as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal int Underlying;
}
