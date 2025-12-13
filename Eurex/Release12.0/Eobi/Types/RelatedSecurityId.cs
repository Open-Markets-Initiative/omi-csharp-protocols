using System.Runtime.CompilerServices;

namespace Eurex.Eobi;

/// <summary>
///  Related Security Id
/// </summary>

public struct RelatedSecurityId
{
    /// <summary>
    ///  Sentinel null value for Related Security Id
    /// </summary>
    public const long NoValue = 0x8000000000000000;

    /// <summary>
    ///  Maximum value for Related Security Id
    /// </summary>
    public const long Maximum = 9223372036854775807;

    /// <summary>
    ///  Minimum value for Related Security Id
    /// </summary>
    public const long Minimum = -9223372036854775807;

    /// <summary>
    ///  Fix Tag for Related Security Id
    /// </summary>
    public const ushort FixTag = 1650;

    /// <summary>
    ///  Size of Related Security Id in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Related Security Id value
    /// </summary>
    public readonly long Value
        => Decode();

    /// <summary>
    ///  Does Related Security Id field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Related Security Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Related Security Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out long value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Related Security Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(long value)
        => Underlying = value;

    /// <summary>
    ///  Set Related Security Id to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Related Security Id as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal long Underlying;
}
