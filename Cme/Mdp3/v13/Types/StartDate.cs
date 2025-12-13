using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Start Date: Start date of a financing deal, i.e. the date the buyer pays the seller cash and takes control of the collateral
/// </summary>

public struct StartDate
{
    /// <summary>
    ///  Fix Tag for Start Date
    /// </summary>
    public const ushort FixTag = 916;

    /// <summary>
    ///  Sentinel null value for Start Date
    /// </summary>
    public const ushort NoValue = 65535;

    /// <summary>
    ///  Size of Start Date in bytes
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Start Date value
    /// </summary>
    public readonly ushort Value
        => Decode();

    /// <summary>
    ///  Does Start Date field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Start Date
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ushort Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Start Date
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out ushort value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Start Date
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ushort value)
        => Underlying = value;

    /// <summary>
    ///  Set Start Date to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Start Date as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ushort Underlying;
}
