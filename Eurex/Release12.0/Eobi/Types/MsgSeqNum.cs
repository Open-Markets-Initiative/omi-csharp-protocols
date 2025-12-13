using System.Runtime.CompilerServices;

namespace Eurex.Eobi;

/// <summary>
///  Msg Seq Num
/// </summary>

public struct MsgSeqNum
{
    /// <summary>
    ///  Sentinel null value for Msg Seq Num
    /// </summary>
    public const uint NoValue = 0xFFFFFFFF;

    /// <summary>
    ///  Maximum value for Msg Seq Num
    /// </summary>
    public const uint Maximum = 4294967294;

    /// <summary>
    ///  Minimum value for Msg Seq Num
    /// </summary>
    public const uint Minimum = 0;

    /// <summary>
    ///  Fix Tag for Msg Seq Num
    /// </summary>
    public const ushort FixTag = 34;

    /// <summary>
    ///  Size of Msg Seq Num in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Msg Seq Num value
    /// </summary>
    public readonly uint Value
        => Decode();

    /// <summary>
    ///  Does Msg Seq Num field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Msg Seq Num
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Msg Seq Num
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out uint value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Msg Seq Num
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
        => Underlying = value;

    /// <summary>
    ///  Set Msg Seq Num to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Msg Seq Num as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal uint Underlying;
}
