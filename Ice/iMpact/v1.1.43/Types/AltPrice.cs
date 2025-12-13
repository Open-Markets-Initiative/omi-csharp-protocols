using System.Buffers.Binary;
using System.Runtime.CompilerServices;

namespace Ice.iMpact;

/// <summary>
///  Alt Price: Eris Futures Price
/// </summary>

public struct AltPrice
{
    /// <summary>
    ///  Size of Alt Price in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Alt Price value
    /// </summary>
    public readonly long Value
        => Decode();

    /// <summary>
    ///  Read Alt Price
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Decode()
        => BinaryPrimitives.ReverseEndianness(Underlying);

    /// <summary>
    ///  Write Alt Price
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(long value)
        => Underlying = BinaryPrimitives.ReverseEndianness(value);

    /// <summary>
    ///  Alt Price as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal long Underlying;
}
