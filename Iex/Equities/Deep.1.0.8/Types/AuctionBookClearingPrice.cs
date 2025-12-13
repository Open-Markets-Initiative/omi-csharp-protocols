using System.Runtime.CompilerServices;

namespace Iex.Deep;

/// <summary>
///  Auction Book Clearing Price: Clearing price using orders on the Auction Book
/// </summary>

public struct AuctionBookClearingPrice
{
    /// <summary>
    ///  Decimal place factor for Auction Book Clearing Price
    /// </summary>
    public const long Factor = 10000;

    /// <summary>
    ///  Size of Auction Book Clearing Price in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Auction Book Clearing Price value
    /// </summary>
    public readonly long Value
        => Decode();

    /// <summary>
    ///  Read Auction Book Clearing Price
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Decode()
        => Underlying / Factor;

    /// <summary>
    ///  Write Auction Book Clearing Price
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(long value)
        => Underlying = value * Factor;

    /// <summary>
    ///  Auction Book Clearing Price as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal long Underlying;
}
