using System.Runtime.CompilerServices;

namespace Eurex.Eobi;

/// <summary>
///  Leg Symbol
/// </summary>

public struct LegSymbol
{
    /// <summary>
    ///  Sentinel null value for Leg Symbol
    /// </summary>
    public const int NoValue = 0x80000000;

    /// <summary>
    ///  Maximum value for Leg Symbol
    /// </summary>
    public const int Maximum = 2147483647;

    /// <summary>
    ///  Minimum value for Leg Symbol
    /// </summary>
    public const int Minimum = -2147483647;

    /// <summary>
    ///  Fix Tag for Leg Symbol
    /// </summary>
    public const ushort FixTag = 600;

    /// <summary>
    ///  Size of Leg Symbol in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Leg Symbol value
    /// </summary>
    public readonly int Value
        => Decode();

    /// <summary>
    ///  Does Leg Symbol field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Leg Symbol
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Leg Symbol
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out int value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Leg Symbol
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(int value)
        => Underlying = value;

    /// <summary>
    ///  Set Leg Symbol to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Leg Symbol as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal int Underlying;
}
