using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Represents the SendTime field as a Unix nanosecond timestamp (8-byte little-endian).
/// </summary>

public struct SendTime
{
    /// <summary>
    ///  Size of SendTime in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Gets the decoded Send Time value.
    /// </summary>
    public readonly DateTime Value
        => Decode();

    /// <summary>
    ///  Decodes the raw bytes into the Send Time value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly DateTime Decode()
        => DateTime.UnixEpoch.AddTicks(Underlying / TimeSpan.NanosecondsPerTick);

    /// <summary>
    ///  Encodes a value into the underlying Send Time bytes.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(DateTime timestamp)
        => Underlying = (timestamp - DateTime.UnixEpoch).Ticks * TimeSpan.NanosecondsPerTick;

    /// <summary>
    ///  Returns the string representation of the Send Time value.
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal long Underlying;
}
