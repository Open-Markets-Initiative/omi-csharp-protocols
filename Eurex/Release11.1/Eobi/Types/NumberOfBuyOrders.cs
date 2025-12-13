using System.Runtime.CompilerServices;

namespace Eurex.Eobi;

/// <summary>
///  Number Of Buy Orders
/// </summary>

public struct NumberOfBuyOrders
{
    /// <summary>
    ///  Sentinel null value for Number Of Buy Orders
    /// </summary>
    public const ushort NoValue = 0xFFFF;

    /// <summary>
    ///  Maximum value for Number Of Buy Orders
    /// </summary>
    public const ushort Maximum = 65534;

    /// <summary>
    ///  Minimum value for Number Of Buy Orders
    /// </summary>
    public const ushort Minimum = 0;

    /// <summary>
    ///  Fix Tag for Number Of Buy Orders
    /// </summary>
    public const ushort FixTag = 2449;

    /// <summary>
    ///  Size of Number Of Buy Orders in bytes
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Number Of Buy Orders value
    /// </summary>
    public readonly ushort Value
        => Decode();

    /// <summary>
    ///  Does Number Of Buy Orders field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Number Of Buy Orders
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ushort Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Number Of Buy Orders
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out ushort value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Number Of Buy Orders
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ushort value)
        => Underlying = value;

    /// <summary>
    ///  Set Number Of Buy Orders to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Number Of Buy Orders as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ushort Underlying;
}
