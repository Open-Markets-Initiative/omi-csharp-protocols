using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Settl Date: Settle (Value) Date corresponding to Trade Date
/// </summary>

public struct SettlDate
{
    /// <summary>
    ///  Fix Tag for Settl Date
    /// </summary>
    public const ushort FixTag = 64;

    /// <summary>
    ///  Sentinel null value for Settl Date
    /// </summary>
    public const ushort NoValue = 65535;

    /// <summary>
    ///  Size of Settl Date in bytes
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Settl Date value
    /// </summary>
    public readonly ushort Value
        => Decode();

    /// <summary>
    ///  Does Settl Date field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Settl Date
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ushort Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Settl Date
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out ushort value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Settl Date
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ushort value)
        => Underlying = value;

    /// <summary>
    ///  Set Settl Date to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Settl Date as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ushort Underlying;
}
