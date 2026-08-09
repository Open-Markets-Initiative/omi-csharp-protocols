using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  Represents the AuctionBookClearingPrice field as a fixed-point decimal (8-byte little-endian integer divided by 10000).
/// </summary>

public struct AuctionBookClearingPrice
{
    /// <summary>
    ///  Decimal place factor for Auction Book Clearing Price
    /// </summary>
    public const long Factor = 10000;

    /// <summary>
    ///  Size of AuctionBookClearingPrice in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Gets the decoded Auction Book Clearing Price value.
    /// </summary>
    public readonly decimal Value
        => (decimal)Decode() / Factor;

    /// <summary>
    ///  Decodes the raw bytes into the Auction Book Clearing Price value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Decode()
        => Underlying;

    /// <summary>
    ///  Encodes a value into the underlying Auction Book Clearing Price bytes.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(long value)
        => Underlying = value;

    /// <summary>
    ///  Returns the string representation of the Auction Book Clearing Price value.
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal long Underlying;
}
