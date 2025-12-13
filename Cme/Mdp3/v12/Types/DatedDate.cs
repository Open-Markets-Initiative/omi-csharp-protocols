using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Dated Date: Dated Date
/// </summary>

public struct DatedDate
{
    /// <summary>
    ///  Fix Tag for Dated Date
    /// </summary>
    public const ushort FixTag = 873;

    /// <summary>
    ///  Sentinel null value for Dated Date
    /// </summary>
    public const ushort NoValue = 65535;

    /// <summary>
    ///  Size of Dated Date in bytes
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Dated Date value
    /// </summary>
    public readonly ushort Value
        => Decode();

    /// <summary>
    ///  Does Dated Date field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Dated Date
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ushort Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Dated Date
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out ushort value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Dated Date
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ushort value)
        => Underlying = value;

    /// <summary>
    ///  Set Dated Date to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Dated Date as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ushort Underlying;
}
