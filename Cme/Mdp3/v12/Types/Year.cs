using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Year: YYYY
/// </summary>

public struct Year
{
    /// <summary>
    ///  Sentinel null value for Year
    /// </summary>
    public const ushort NoValue = 65535;

    /// <summary>
    ///  Size of Year in bytes
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Year value
    /// </summary>
    public readonly ushort Value
        => Decode();

    /// <summary>
    ///  Does Year field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Year
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ushort Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Year
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out ushort value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Year
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ushort value)
        => Underlying = value;

    /// <summary>
    ///  Set Year to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Year as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ushort Underlying;
}
