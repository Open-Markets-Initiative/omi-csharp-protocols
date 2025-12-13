using System.Runtime.CompilerServices;

namespace Eurex.Eobi;

/// <summary>
///  Last Msg Seq Num Processed
/// </summary>

public struct LastMsgSeqNumProcessed
{
    /// <summary>
    ///  Sentinel null value for Last Msg Seq Num Processed
    /// </summary>
    public const uint NoValue = 0xFFFFFFFF;

    /// <summary>
    ///  Maximum value for Last Msg Seq Num Processed
    /// </summary>
    public const uint Maximum = 4294967294;

    /// <summary>
    ///  Minimum value for Last Msg Seq Num Processed
    /// </summary>
    public const uint Minimum = 0;

    /// <summary>
    ///  Fix Tag for Last Msg Seq Num Processed
    /// </summary>
    public const ushort FixTag = 369;

    /// <summary>
    ///  Size of Last Msg Seq Num Processed in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Last Msg Seq Num Processed value
    /// </summary>
    public readonly uint Value
        => Decode();

    /// <summary>
    ///  Does Last Msg Seq Num Processed field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Last Msg Seq Num Processed
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Last Msg Seq Num Processed
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out uint value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Last Msg Seq Num Processed
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
        => Underlying = value;

    /// <summary>
    ///  Set Last Msg Seq Num Processed to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Last Msg Seq Num Processed as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal uint Underlying;
}
