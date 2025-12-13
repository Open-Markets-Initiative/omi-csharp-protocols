using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  End Date: End date of a financing deal, i.e. the date the seller reimburses the buyer and takes back control of the collateral
/// </summary>

public struct EndDate
{
    /// <summary>
    ///  Fix Tag for End Date
    /// </summary>
    public const ushort FixTag = 917;

    /// <summary>
    ///  Sentinel null value for End Date
    /// </summary>
    public const ushort NoValue = 65535;

    /// <summary>
    ///  Size of End Date in bytes
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  End Date value
    /// </summary>
    public readonly ushort Value
        => Decode();

    /// <summary>
    ///  Does End Date field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read End Date
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ushort Decode()
        => Underlying;

    /// <summary>
    ///  Try Read End Date
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out ushort value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write End Date
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ushort value)
        => Underlying = value;

    /// <summary>
    ///  Set End Date to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  End Date as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ushort Underlying;
}
