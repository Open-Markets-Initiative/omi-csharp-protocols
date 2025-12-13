using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  No Chunks: Total number of packets that constitutes a single instrument order book
/// </summary>

public struct NoChunks
{
    /// <summary>
    ///  Fix Tag for No Chunks
    /// </summary>
    public const ushort FixTag = 37709;

    /// <summary>
    ///  Size of No Chunks in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  No Chunks value
    /// </summary>
    public readonly uint Value
        => Decode();

    /// <summary>
    ///  Read No Chunks
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => Underlying;

    /// <summary>
    ///  Write No Chunks
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
        => Underlying = value;

    /// <summary>
    ///  No Chunks as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal uint Underlying;
}
