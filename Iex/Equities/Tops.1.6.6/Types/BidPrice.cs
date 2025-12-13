using System.Runtime.CompilerServices;

namespace Iex.Tops;

/// <summary>
///  Bid Price: Best quoted bid price
/// </summary>

public struct BidPrice
{
    /// <summary>
    ///  Decimal place factor for Bid Price
    /// </summary>
    public const long Factor = 10000;

    /// <summary>
    ///  Size of Bid Price in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Bid Price value
    /// </summary>
    public readonly long Value
        => Decode();

    /// <summary>
    ///  Read Bid Price
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Decode()
        => Underlying / Factor;

    /// <summary>
    ///  Write Bid Price
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(long value)
        => Underlying = value * Factor;

    /// <summary>
    ///  Bid Price as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal long Underlying;
}
