using System.Runtime.CompilerServices;

namespace Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
///  Represents the Timestamp field as a Unix nanosecond timestamp (8-byte little-endian).
/// </summary>

public struct Timestamp
{
    /// <summary>
    ///  Size of Timestamp in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Gets the decoded Timestamp value.
    /// </summary>
    public readonly DateTime Value
        => Decode();

    /// <summary>
    ///  Decodes the raw bytes into the Timestamp value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly DateTime Decode()
        => DateTime.UnixEpoch.AddTicks(Underlying / TimeSpan.NanosecondsPerTick);

    /// <summary>
    ///  Encodes a value into the underlying Timestamp bytes.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(DateTime timestamp)
        => Underlying = (timestamp - DateTime.UnixEpoch).Ticks * TimeSpan.NanosecondsPerTick;

    /// <summary>
    ///  Returns the string representation of the Timestamp value.
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal long Underlying;
}
