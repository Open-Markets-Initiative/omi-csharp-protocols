using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Number Of Orders Optional: In Book entry - aggregate number of orders at given price level
/// </summary>

public struct NumberOfOrdersOptional
{
    /// <summary>
    ///  Fix Tag for Number Of Orders Optional
    /// </summary>
    public const ushort FixTag = 346;

    /// <summary>
    ///  Sentinel null value for Number Of Orders Optional
    /// </summary>
    public const int NoValue = 2147483647;

    /// <summary>
    ///  Size of Number Of Orders Optional in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Number Of Orders Optional value
    /// </summary>
    public readonly int Value
        => Decode();

    /// <summary>
    ///  Does Number Of Orders Optional field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Number Of Orders Optional
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Number Of Orders Optional
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out int value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Number Of Orders Optional
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(int value)
        => Underlying = value;

    /// <summary>
    ///  Set Number Of Orders Optional to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Number Of Orders Optional as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal int Underlying;
}
