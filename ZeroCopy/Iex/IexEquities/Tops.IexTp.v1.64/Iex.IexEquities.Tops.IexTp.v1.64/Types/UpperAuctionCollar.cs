using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Represents the UpperAuctionCollar field as a fixed-point decimal (8-byte little-endian integer divided by 10000).
/// </summary>

public struct UpperAuctionCollar
{
    /// <summary>
    ///  Decimal place factor for Upper Auction Collar
    /// </summary>
    public const long Factor = 10000;

    /// <summary>
    ///  Size of UpperAuctionCollar in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Gets the decoded Upper Auction Collar value.
    /// </summary>
    public readonly decimal Value
        => (decimal)Decode() / Factor;

    /// <summary>
    ///  Decodes the raw bytes into the Upper Auction Collar value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Decode()
        => Underlying;

    /// <summary>
    ///  Encodes a value into the underlying Upper Auction Collar bytes.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(long value)
        => Underlying = value;

    /// <summary>
    ///  Returns the string representation of the Upper Auction Collar value.
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal long Underlying;
}
