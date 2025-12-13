using System.Runtime.CompilerServices;

namespace Iex.Tops;

/// <summary>
///  Reference Price: Clearing price at or within the Reference Price Range using orders on the Auction Book
/// </summary>

public struct ReferencePrice
{
    /// <summary>
    ///  Decimal place factor for Reference Price
    /// </summary>
    public const long Factor = 10000;

    /// <summary>
    ///  Size of Reference Price in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Reference Price value
    /// </summary>
    public readonly long Value
        => Decode();

    /// <summary>
    ///  Read Reference Price
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Decode()
        => Underlying / Factor;

    /// <summary>
    ///  Write Reference Price
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(long value)
        => Underlying = value * Factor;

    /// <summary>
    ///  Reference Price as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal long Underlying;
}
