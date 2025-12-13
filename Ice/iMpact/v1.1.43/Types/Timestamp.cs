using System.Buffers.Binary;
using System.Runtime.CompilerServices;

namespace Ice.iMpact;

/// <summary>
///  Timestamp: Timestamp of last update used to derive the price level message
/// </summary>

public struct Timestamp
{
    /// <summary>
    ///  Size of Timestamp in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Timestamp value
    /// </summary>
    public readonly long Value
        => Decode();

    /// <summary>
    ///  Read Timestamp
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Decode()
        => BinaryPrimitives.ReverseEndianness(Underlying);

    /// <summary>
    ///  Write Timestamp
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(long value)
        => Underlying = BinaryPrimitives.ReverseEndianness(value);

    /// <summary>
    ///  Timestamp as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal long Underlying;
}
