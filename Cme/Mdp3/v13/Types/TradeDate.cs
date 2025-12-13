using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Trade Date: Trade Session Date
/// </summary>

public struct TradeDate
{
    /// <summary>
    ///  Fix Tag for Trade Date
    /// </summary>
    public const ushort FixTag = 75;

    /// <summary>
    ///  Sentinel null value for Trade Date
    /// </summary>
    public const ushort NoValue = 65535;

    /// <summary>
    ///  Size of Trade Date in bytes
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Trade Date value
    /// </summary>
    public readonly ushort Value
        => Decode();

    /// <summary>
    ///  Does Trade Date field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Trade Date
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ushort Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Trade Date
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out ushort value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Trade Date
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ushort value)
        => Underlying = value;

    /// <summary>
    ///  Set Trade Date to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Trade Date as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ushort Underlying;
}
