using System.Buffers.Binary;
using System.Runtime.CompilerServices;

namespace Ice.iMpact;

/// <summary>
///  Alt Low Price: Low Eris Futures Price
/// </summary>

public struct AltLowPrice
{
    /// <summary>
    ///  Size of Alt Low Price in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Alt Low Price value
    /// </summary>
    public readonly long Value
        => Decode();

    /// <summary>
    ///  Read Alt Low Price
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Decode()
        => BinaryPrimitives.ReverseEndianness(Underlying);

    /// <summary>
    ///  Write Alt Low Price
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(long value)
        => Underlying = BinaryPrimitives.ReverseEndianness(value);

    /// <summary>
    ///  Alt Low Price as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal long Underlying;
}
