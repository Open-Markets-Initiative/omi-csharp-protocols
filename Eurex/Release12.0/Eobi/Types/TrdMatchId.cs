using System.Runtime.CompilerServices;

namespace Eurex.Eobi;

/// <summary>
///  Trd Match Id
/// </summary>

public struct TrdMatchId
{
    /// <summary>
    ///  Sentinel null value for Trd Match Id
    /// </summary>
    public const uint NoValue = 0xFFFFFFFF;

    /// <summary>
    ///  Maximum value for Trd Match Id
    /// </summary>
    public const uint Maximum = 4294967294;

    /// <summary>
    ///  Minimum value for Trd Match Id
    /// </summary>
    public const uint Minimum = 0;

    /// <summary>
    ///  Fix Tag for Trd Match Id
    /// </summary>
    public const ushort FixTag = 880;

    /// <summary>
    ///  Size of Trd Match Id in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Trd Match Id value
    /// </summary>
    public readonly uint Value
        => Decode();

    /// <summary>
    ///  Does Trd Match Id field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Trd Match Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Trd Match Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out uint value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Trd Match Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
        => Underlying = value;

    /// <summary>
    ///  Set Trd Match Id to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Trd Match Id as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal uint Underlying;
}
