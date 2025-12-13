using System.Runtime.CompilerServices;

namespace Iex.Deep;

/// <summary>
///  Upper Auction Collar: Upper threshold price of the auction collar, if any
/// </summary>

public struct UpperAuctionCollar
{
    /// <summary>
    ///  Decimal place factor for Upper Auction Collar
    /// </summary>
    public const long Factor = 10000;

    /// <summary>
    ///  Size of Upper Auction Collar in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Upper Auction Collar value
    /// </summary>
    public readonly long Value
        => Decode();

    /// <summary>
    ///  Read Upper Auction Collar
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Decode()
        => Underlying / Factor;

    /// <summary>
    ///  Write Upper Auction Collar
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(long value)
        => Underlying = value * Factor;

    /// <summary>
    ///  Upper Auction Collar as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal long Underlying;
}
