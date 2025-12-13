using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Md Trade Entry Optional: Market Data Trade entry ID
/// </summary>

public struct MdTradeEntryOptional
{
    /// <summary>
    ///  Fix Tag for Md Trade Entry Optional
    /// </summary>
    public const ushort FixTag = 37711;

    /// <summary>
    ///  Sentinel null value for Md Trade Entry Optional
    /// </summary>
    public const uint NoValue = 4294967295;

    /// <summary>
    ///  Size of Md Trade Entry Optional in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Md Trade Entry Optional value
    /// </summary>
    public readonly uint Value
        => Decode();

    /// <summary>
    ///  Does Md Trade Entry Optional field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Md Trade Entry Optional
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Md Trade Entry Optional
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out uint value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Md Trade Entry Optional
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
        => Underlying = value;

    /// <summary>
    ///  Set Md Trade Entry Optional to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Md Trade Entry Optional as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal uint Underlying;
}
