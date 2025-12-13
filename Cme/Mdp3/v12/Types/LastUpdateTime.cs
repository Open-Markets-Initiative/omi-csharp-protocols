using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Last Update Time: UTC Date and time of last Security Definition add, update or delete on a given Market Data channel
/// </summary>

public struct LastUpdateTime
{
    /// <summary>
    ///  Fix Tag for Last Update Time
    /// </summary>
    public const ushort FixTag = 779;

    /// <summary>
    ///  Size of Last Update Time in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Last Update Time value
    /// </summary>
    public readonly DateTime Value
        => Decode();

    /// <summary>
    ///  Read Last Update Time
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly DateTime Decode()
        => DateTime.UnixEpoch.AddTicks(Underlying / TimeSpan.NanosecondsPerTick);

    /// <summary>
    ///  Write Last Update Time as Nanoseconds since Jan 1st, 1970, 00:00:00 GMT
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(DateTime timestamp)
        => Underlying = timestamp.Ticks * TimeSpan.NanosecondsPerTick;

    /// <summary>
    ///  Last Update Time as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal long Underlying;
}
