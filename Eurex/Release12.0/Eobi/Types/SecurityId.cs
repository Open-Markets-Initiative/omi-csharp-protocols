using System.Runtime.CompilerServices;

namespace Eurex.Eobi;

/// <summary>
///  Security Id
/// </summary>

public struct SecurityId
{
    /// <summary>
    ///  Sentinel null value for Security Id
    /// </summary>
    public const long NoValue = 0x8000000000000000;

    /// <summary>
    ///  Maximum value for Security Id
    /// </summary>
    public const long Maximum = 9223372036854775807;

    /// <summary>
    ///  Minimum value for Security Id
    /// </summary>
    public const long Minimum = -9223372036854775807;

    /// <summary>
    ///  Fix Tag for Security Id
    /// </summary>
    public const ushort FixTag = 48;

    /// <summary>
    ///  Size of Security Id in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Security Id value
    /// </summary>
    public readonly long Value
        => Decode();

    /// <summary>
    ///  Does Security Id field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Security Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Security Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out long value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Security Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(long value)
        => Underlying = value;

    /// <summary>
    ///  Set Security Id to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Security Id as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal long Underlying;
}
