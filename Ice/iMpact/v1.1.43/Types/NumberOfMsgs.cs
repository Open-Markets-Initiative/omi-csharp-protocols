using System.Buffers.Binary;
using System.Runtime.CompilerServices;

namespace Ice.iMpact;

/// <summary>
///  Number Of Msgs: Field indicates the number of messages contained in the block.
/// </summary>

public struct NumberOfMsgs
{
    /// <summary>
    ///  Size of Number Of Msgs in bytes
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Number Of Msgs value
    /// </summary>
    public readonly ushort Value
        => Decode();

    /// <summary>
    ///  Read Number Of Msgs
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ushort Decode()
        => BinaryPrimitives.ReverseEndianness(Underlying);

    /// <summary>
    ///  Write Number Of Msgs
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ushort value)
        => Underlying = BinaryPrimitives.ReverseEndianness(value);

    /// <summary>
    ///  Number Of Msgs as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ushort Underlying;
}
