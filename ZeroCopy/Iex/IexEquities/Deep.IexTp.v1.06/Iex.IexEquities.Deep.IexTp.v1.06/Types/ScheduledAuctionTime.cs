using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  Represents the ScheduledAuctionTime field as a Unix second timestamp (4-byte little-endian).
/// </summary>

public struct ScheduledAuctionTime
{
    /// <summary>
    ///  Size of ScheduledAuctionTime in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Gets the decoded Scheduled Auction Time value.
    /// </summary>
    public readonly DateTime Value
        => Decode();

    /// <summary>
    ///  Decodes the raw bytes into the Scheduled Auction Time value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly DateTime Decode()
        => DateTime.UnixEpoch.AddSeconds(Underlying);

    /// <summary>
    ///  Encodes a value into the underlying Scheduled Auction Time bytes.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(DateTime timestamp)
        => Underlying = (uint)(timestamp - DateTime.UnixEpoch).TotalSeconds;

    /// <summary>
    ///  Returns the string representation of the Scheduled Auction Time value.
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal uint Underlying;
}
