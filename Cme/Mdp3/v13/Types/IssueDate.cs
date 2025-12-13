using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Issue Date: Issue Date
/// </summary>

public struct IssueDate
{
    /// <summary>
    ///  Fix Tag for Issue Date
    /// </summary>
    public const ushort FixTag = 225;

    /// <summary>
    ///  Sentinel null value for Issue Date
    /// </summary>
    public const ushort NoValue = 65535;

    /// <summary>
    ///  Size of Issue Date in bytes
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Issue Date value
    /// </summary>
    public readonly ushort Value
        => Decode();

    /// <summary>
    ///  Does Issue Date field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Issue Date
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ushort Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Issue Date
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out ushort value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Issue Date
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ushort value)
        => Underlying = value;

    /// <summary>
    ///  Set Issue Date to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Issue Date as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ushort Underlying;
}
