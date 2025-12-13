using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Sending Time: Packet Sending Time
/// </summary>

public struct SendingTime
{
    /// <summary>
    ///  Size of Sending Time in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Sending Time value
    /// </summary>
    public readonly DateTime Value
        => Decode();

    /// <summary>
    ///  Read Sending Time
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly DateTime Decode()
        => DateTime.UnixEpoch.AddTicks(Underlying / TimeSpan.NanosecondsPerTick);

    /// <summary>
    ///  Write Sending Time as Nanoseconds since Jan 1st, 1970, 00:00:00 GMT
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(DateTime timestamp)
        => Underlying = timestamp.Ticks * TimeSpan.NanosecondsPerTick;

    /// <summary>
    ///  Sending Time as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal long Underlying;
}
