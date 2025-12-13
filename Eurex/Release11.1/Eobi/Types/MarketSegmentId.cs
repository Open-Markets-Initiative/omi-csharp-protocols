using System.Runtime.CompilerServices;

namespace Eurex.Eobi;

/// <summary>
///  Market Segment Id
/// </summary>

public struct MarketSegmentId
{
    /// <summary>
    ///  Sentinel null value for Market Segment Id
    /// </summary>
    public const int NoValue = 0x80000000;

    /// <summary>
    ///  Maximum value for Market Segment Id
    /// </summary>
    public const int Maximum = 2147483647;

    /// <summary>
    ///  Minimum value for Market Segment Id
    /// </summary>
    public const int Minimum = -2147483647;

    /// <summary>
    ///  Fix Tag for Market Segment Id
    /// </summary>
    public const ushort FixTag = 1300;

    /// <summary>
    ///  Size of Market Segment Id in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Market Segment Id value
    /// </summary>
    public readonly int Value
        => Decode();

    /// <summary>
    ///  Does Market Segment Id field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Market Segment Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Market Segment Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out int value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Market Segment Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(int value)
        => Underlying = value;

    /// <summary>
    ///  Set Market Segment Id to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Market Segment Id as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal int Underlying;
}
