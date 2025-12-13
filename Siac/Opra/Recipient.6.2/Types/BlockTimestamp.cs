using System.Runtime.CompilerServices;

namespace Siac.Recipient;

/// <summary>
///  Block Timestamp
/// </summary>

public struct BlockTimestamp
{
    /// <summary>
    ///  Size of Block Timestamp in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Block Timestamp value
    /// </summary>
    public readonly DateTime Value
        => Decode();

    /// <summary>
    ///  Read Block Timestamp
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly DateTime Decode()
        => DateTime.UnixEpoch.AddTicks(Underlying / TimeSpan.NanosecondsPerTick);

    /// <summary>
    ///  Write Block Timestamp as Nanoseconds since Jan 1st, 1970, 00:00:00 GMT
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(DateTime timestamp)
        => Underlying = timestamp.Ticks * TimeSpan.NanosecondsPerTick;

    /// <summary>
    ///  Block Timestamp as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal long Underlying;
}
