using System.Runtime.CompilerServices;

namespace Iex.Tops;

/// <summary>
///  Lower Auction Collar: Lower threshold price of the auction collar, if any
/// </summary>

public struct LowerAuctionCollar
{
    /// <summary>
    ///  Decimal place factor for Lower Auction Collar
    /// </summary>
    public const long Factor = 10000;

    /// <summary>
    ///  Size of Lower Auction Collar in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Lower Auction Collar value
    /// </summary>
    public readonly long Value
        => Decode();

    /// <summary>
    ///  Read Lower Auction Collar
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Decode()
        => Underlying / Factor;

    /// <summary>
    ///  Write Lower Auction Collar
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(long value)
        => Underlying = value * Factor;

    /// <summary>
    ///  Lower Auction Collar as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal long Underlying;
}
