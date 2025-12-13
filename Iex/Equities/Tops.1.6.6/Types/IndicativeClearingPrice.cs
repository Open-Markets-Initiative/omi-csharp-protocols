using System.Runtime.CompilerServices;

namespace Iex.Tops;

/// <summary>
///  Indicative Clearing Price: Clearing price using Eligible Auction Orders
/// </summary>

public struct IndicativeClearingPrice
{
    /// <summary>
    ///  Decimal place factor for Indicative Clearing Price
    /// </summary>
    public const long Factor = 10000;

    /// <summary>
    ///  Size of Indicative Clearing Price in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Indicative Clearing Price value
    /// </summary>
    public readonly long Value
        => Decode();

    /// <summary>
    ///  Read Indicative Clearing Price
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Decode()
        => Underlying / Factor;

    /// <summary>
    ///  Write Indicative Clearing Price
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(long value)
        => Underlying = value * Factor;

    /// <summary>
    ///  Indicative Clearing Price as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal long Underlying;
}
