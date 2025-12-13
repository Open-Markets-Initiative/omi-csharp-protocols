using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Decay Start Date: Indicates the date at which a decaying contract will begin to decay
/// </summary>

public struct DecayStartDate
{
    /// <summary>
    ///  Fix Tag for Decay Start Date
    /// </summary>
    public const ushort FixTag = 5819;

    /// <summary>
    ///  Sentinel null value for Decay Start Date
    /// </summary>
    public const ushort NoValue = 65535;

    /// <summary>
    ///  Size of Decay Start Date in bytes
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Decay Start Date value
    /// </summary>
    public readonly ushort Value
        => Decode();

    /// <summary>
    ///  Does Decay Start Date field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Decay Start Date
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ushort Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Decay Start Date
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out ushort value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Decay Start Date
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ushort value)
        => Underlying = value;

    /// <summary>
    ///  Set Decay Start Date to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Decay Start Date as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ushort Underlying;
}
