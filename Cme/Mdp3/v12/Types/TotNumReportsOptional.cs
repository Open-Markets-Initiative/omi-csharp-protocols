using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Tot Num Reports Optional: Total number of instruments in the Replay loop. Used on Replay Feed only
/// </summary>

public struct TotNumReportsOptional
{
    /// <summary>
    ///  Fix Tag for Tot Num Reports Optional
    /// </summary>
    public const ushort FixTag = 911;

    /// <summary>
    ///  Sentinel null value for Tot Num Reports Optional
    /// </summary>
    public const uint NoValue = 4294967295;

    /// <summary>
    ///  Size of Tot Num Reports Optional in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Tot Num Reports Optional value
    /// </summary>
    public readonly uint Value
        => Decode();

    /// <summary>
    ///  Does Tot Num Reports Optional field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Tot Num Reports Optional
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Tot Num Reports Optional
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out uint value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Tot Num Reports Optional
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
        => Underlying = value;

    /// <summary>
    ///  Set Tot Num Reports Optional to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Tot Num Reports Optional as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal uint Underlying;
}
