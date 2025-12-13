using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Current Chunk: Chunk sequence
/// </summary>

public struct CurrentChunk
{
    /// <summary>
    ///  Fix Tag for Current Chunk
    /// </summary>
    public const ushort FixTag = 37710;

    /// <summary>
    ///  Size of Current Chunk in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Current Chunk value
    /// </summary>
    public readonly uint Value
        => Decode();

    /// <summary>
    ///  Read Current Chunk
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => Underlying;

    /// <summary>
    ///  Write Current Chunk
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
        => Underlying = value;

    /// <summary>
    ///  Current Chunk as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal uint Underlying;
}
