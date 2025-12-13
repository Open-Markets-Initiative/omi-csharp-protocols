using System.Runtime.CompilerServices;

namespace Eurex.Eobi;

/// <summary>
///  Opt Attribute
/// </summary>

public struct OptAttribute
{
    /// <summary>
    ///  Sentinel null value for Opt Attribute
    /// </summary>
    public const uint NoValue = 0xFFFFFFFF;

    /// <summary>
    ///  Maximum value for Opt Attribute
    /// </summary>
    public const uint Maximum = 4294967294;

    /// <summary>
    ///  Minimum value for Opt Attribute
    /// </summary>
    public const uint Minimum = 0;

    /// <summary>
    ///  Fix Tag for Opt Attribute
    /// </summary>
    public const ushort FixTag = 206;

    /// <summary>
    ///  Size of Opt Attribute in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Opt Attribute value
    /// </summary>
    public readonly uint Value
        => Decode();

    /// <summary>
    ///  Does Opt Attribute field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Opt Attribute
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Opt Attribute
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out uint value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Opt Attribute
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
        => Underlying = value;

    /// <summary>
    ///  Set Opt Attribute to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Opt Attribute as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal uint Underlying;
}
