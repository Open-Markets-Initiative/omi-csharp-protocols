using System.Buffers.Binary;
using System.Runtime.CompilerServices;

namespace Ice.iMpact;

/// <summary>
///  Max Price: Maximum Price, OrderPriceDenominator should be applied to this field
/// </summary>

public struct MaxPrice
{
    /// <summary>
    ///  Size of Max Price in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Max Price value
    /// </summary>
    public readonly long Value
        => Decode();

    /// <summary>
    ///  Read Max Price
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Decode()
        => BinaryPrimitives.ReverseEndianness(Underlying);

    /// <summary>
    ///  Write Max Price
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(long value)
        => Underlying = BinaryPrimitives.ReverseEndianness(value);

    /// <summary>
    ///  Max Price as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal long Underlying;
}
