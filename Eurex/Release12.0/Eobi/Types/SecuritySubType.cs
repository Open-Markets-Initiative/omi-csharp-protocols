using System.Runtime.CompilerServices;

namespace Eurex.Eobi;

/// <summary>
///  Security Sub Type
/// </summary>

public struct SecuritySubType
{
    /// <summary>
    ///  Sentinel null value for Security Sub Type
    /// </summary>
    public const int NoValue = 0x80000000;

    /// <summary>
    ///  Maximum value for Security Sub Type
    /// </summary>
    public const int Maximum = 2147483647;

    /// <summary>
    ///  Minimum value for Security Sub Type
    /// </summary>
    public const int Minimum = -2147483647;

    /// <summary>
    ///  Fix Tag for Security Sub Type
    /// </summary>
    public const ushort FixTag = 762;

    /// <summary>
    ///  Size of Security Sub Type in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Security Sub Type value
    /// </summary>
    public readonly int Value
        => Decode();

    /// <summary>
    ///  Does Security Sub Type field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Security Sub Type
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Security Sub Type
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out int value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Security Sub Type
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(int value)
        => Underlying = value;

    /// <summary>
    ///  Set Security Sub Type to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Security Sub Type as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal int Underlying;
}
