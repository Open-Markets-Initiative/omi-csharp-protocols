using System.Runtime.CompilerServices;

namespace Eurex.Eobi;

/// <summary>
///  Body Len
/// </summary>

public struct BodyLen
{
    /// <summary>
    ///  Sentinel null value for Body Len
    /// </summary>
    public const ushort NoValue = 0xFFFF;

    /// <summary>
    ///  Maximum value for Body Len
    /// </summary>
    public const ushort Maximum = 65534;

    /// <summary>
    ///  Minimum value for Body Len
    /// </summary>
    public const ushort Minimum = 0;

    /// <summary>
    ///  Fix Tag for Body Len
    /// </summary>
    public const ushort FixTag = 9;

    /// <summary>
    ///  Size of Body Len in bytes
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Body Len value
    /// </summary>
    public readonly ushort Value
        => Decode();

    /// <summary>
    ///  Does Body Len field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Body Len
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ushort Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Body Len
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out ushort value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Body Len
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ushort value)
        => Underlying = value;

    /// <summary>
    ///  Set Body Len to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Body Len as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ushort Underlying;
}
