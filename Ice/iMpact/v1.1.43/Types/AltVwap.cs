using System.Buffers.Binary;
using System.Runtime.CompilerServices;

namespace Ice.iMpact;

/// <summary>
///  Alt Vwap: Volume-weighted Average Eris Futures Price
/// </summary>

public struct AltVwap
{
    /// <summary>
    ///  Size of Alt Vwap in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Alt Vwap value
    /// </summary>
    public readonly long Value
        => Decode();

    /// <summary>
    ///  Read Alt Vwap
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Decode()
        => BinaryPrimitives.ReverseEndianness(Underlying);

    /// <summary>
    ///  Write Alt Vwap
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(long value)
        => Underlying = BinaryPrimitives.ReverseEndianness(value);

    /// <summary>
    ///  Alt Vwap as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal long Underlying;
}
