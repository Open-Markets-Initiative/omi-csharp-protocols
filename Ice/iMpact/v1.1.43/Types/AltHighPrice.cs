using System.Buffers.Binary;
using System.Runtime.CompilerServices;

namespace Ice.iMpact;

/// <summary>
///  Alt High Price: High Eris Futures Price
/// </summary>

public struct AltHighPrice
{
    /// <summary>
    ///  Size of Alt High Price in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Alt High Price value
    /// </summary>
    public readonly long Value
        => Decode();

    /// <summary>
    ///  Read Alt High Price
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Decode()
        => BinaryPrimitives.ReverseEndianness(Underlying);

    /// <summary>
    ///  Write Alt High Price
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(long value)
        => Underlying = BinaryPrimitives.ReverseEndianness(value);

    /// <summary>
    ///  Alt High Price as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal long Underlying;
}
