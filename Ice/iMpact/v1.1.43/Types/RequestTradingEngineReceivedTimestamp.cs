using System.Buffers.Binary;
using System.Runtime.CompilerServices;

namespace Ice.iMpact;

/// <summary>
///  Request Trading Engine Received Timestamp: time the trading engine received the request that triggers this message
/// </summary>

public struct RequestTradingEngineReceivedTimestamp
{
    /// <summary>
    ///  Size of Request Trading Engine Received Timestamp in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Request Trading Engine Received Timestamp value
    /// </summary>
    public readonly long Value
        => Decode();

    /// <summary>
    ///  Read Request Trading Engine Received Timestamp
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Decode()
        => BinaryPrimitives.ReverseEndianness(Underlying);

    /// <summary>
    ///  Write Request Trading Engine Received Timestamp
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(long value)
        => Underlying = BinaryPrimitives.ReverseEndianness(value);

    /// <summary>
    ///  Request Trading Engine Received Timestamp as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal long Underlying;
}
