using System.Runtime.CompilerServices;

namespace Iex.Deep;

/// <summary>
///  Scheduled Auction Time: Projected time of the auction match
/// </summary>

public struct ScheduledAuctionTime
{
    /// <summary>
    ///  Size of Scheduled Auction Time in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Scheduled Auction Time value
    /// </summary>
    public readonly uint Value
        => Decode();

    /// <summary>
    ///  Read Scheduled Auction Time
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => Underlying;

    /// <summary>
    ///  Write Scheduled Auction Time
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
        => Underlying = value;

    /// <summary>
    ///  Scheduled Auction Time as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal uint Underlying;
}
