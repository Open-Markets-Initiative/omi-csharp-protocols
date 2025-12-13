using System.Buffers.Binary;
using System.Runtime.CompilerServices;

namespace Ice.iMpact;

/// <summary>
///  Min Price: Minimum Price, OrderPriceDenominator should be applied to this field
/// </summary>

public struct MinPrice
{
    /// <summary>
    ///  Size of Min Price in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Min Price value
    /// </summary>
    public readonly long Value
        => Decode();

    /// <summary>
    ///  Read Min Price
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Decode()
        => BinaryPrimitives.ReverseEndianness(Underlying);

    /// <summary>
    ///  Write Min Price
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(long value)
        => Underlying = BinaryPrimitives.ReverseEndianness(value);

    /// <summary>
    ///  Min Price as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal long Underlying;
}
