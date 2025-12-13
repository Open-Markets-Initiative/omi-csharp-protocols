using System.Runtime.CompilerServices;

namespace Iex.Tops;

/// <summary>
///  First Message Sequence Number: Sequence of the first message in the segment
/// </summary>

public struct FirstMessageSequenceNumber
{
    /// <summary>
    ///  Size of First Message Sequence Number in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  First Message Sequence Number value
    /// </summary>
    public readonly ulong Value
        => Decode();

    /// <summary>
    ///  Read First Message Sequence Number
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ulong Decode()
        => Underlying;

    /// <summary>
    ///  Write First Message Sequence Number
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ulong value)
        => Underlying = value;

    /// <summary>
    ///  First Message Sequence Number as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ulong Underlying;
}
