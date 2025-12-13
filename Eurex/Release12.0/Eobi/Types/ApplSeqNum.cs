using System.Runtime.CompilerServices;

namespace Eurex.Eobi;

/// <summary>
///  Appl Seq Num
/// </summary>

public struct ApplSeqNum
{
    /// <summary>
    ///  Sentinel null value for Appl Seq Num
    /// </summary>
    public const uint NoValue = 0xFFFFFFFF;

    /// <summary>
    ///  Maximum value for Appl Seq Num
    /// </summary>
    public const uint Maximum = 4294967294;

    /// <summary>
    ///  Minimum value for Appl Seq Num
    /// </summary>
    public const uint Minimum = 0;

    /// <summary>
    ///  Fix Tag for Appl Seq Num
    /// </summary>
    public const ushort FixTag = 1181;

    /// <summary>
    ///  Size of Appl Seq Num in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Appl Seq Num value
    /// </summary>
    public readonly uint Value
        => Decode();

    /// <summary>
    ///  Does Appl Seq Num field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Appl Seq Num
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Appl Seq Num
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out uint value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Appl Seq Num
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
        => Underlying = value;

    /// <summary>
    ///  Set Appl Seq Num to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Appl Seq Num as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal uint Underlying;
}
