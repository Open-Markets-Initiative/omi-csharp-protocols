using System.Runtime.CompilerServices;

namespace Iex.Tops;

/// <summary>
///  Bid Size: Aggregate quoted best bid size
/// </summary>

public struct BidSize
{
    /// <summary>
    ///  Size of Bid Size in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Bid Size value
    /// </summary>
    public readonly uint Value
        => Decode();

    /// <summary>
    ///  Read Bid Size
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => Underlying;

    /// <summary>
    ///  Write Bid Size
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
        => Underlying = value;

    /// <summary>
    ///  Bid Size as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal uint Underlying;
}
