using System.Buffers.Binary;
using System.Runtime.CompilerServices;

namespace Ice.iMpact;

/// <summary>
///  Alt Last Trade Price: Last Trade Eris Futures Price
/// </summary>

public struct AltLastTradePrice
{
    /// <summary>
    ///  Size of Alt Last Trade Price in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Alt Last Trade Price value
    /// </summary>
    public readonly long Value
        => Decode();

    /// <summary>
    ///  Read Alt Last Trade Price
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Decode()
        => BinaryPrimitives.ReverseEndianness(Underlying);

    /// <summary>
    ///  Write Alt Last Trade Price
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(long value)
        => Underlying = BinaryPrimitives.ReverseEndianness(value);

    /// <summary>
    ///  Alt Last Trade Price as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal long Underlying;
}
