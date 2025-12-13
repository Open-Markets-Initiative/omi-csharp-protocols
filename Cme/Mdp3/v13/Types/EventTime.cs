using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Event Time: Date and Time of instument Activation or Expiration event sent as number of nanoseconds since Unix epoch
/// </summary>

public struct EventTime
{
    /// <summary>
    ///  Fix Tag for Event Time
    /// </summary>
    public const ushort FixTag = 1145;

    /// <summary>
    ///  Size of Event Time in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Event Time value
    /// </summary>
    public readonly DateTime Value
        => Decode();

    /// <summary>
    ///  Read Event Time
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly DateTime Decode()
        => DateTime.UnixEpoch.AddTicks(Underlying / TimeSpan.NanosecondsPerTick);

    /// <summary>
    ///  Write Event Time as Nanoseconds since Jan 1st, 1970, 00:00:00 GMT
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(DateTime timestamp)
        => Underlying = timestamp.Ticks * TimeSpan.NanosecondsPerTick;

    /// <summary>
    ///  Event Time as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal long Underlying;
}
