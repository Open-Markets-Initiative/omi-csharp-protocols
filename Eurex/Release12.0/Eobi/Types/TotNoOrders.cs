using System.Runtime.CompilerServices;

namespace Eurex.Eobi;

/// <summary>
///  Tot No Orders
/// </summary>

public struct TotNoOrders
{
    /// <summary>
    ///  Sentinel null value for Tot No Orders
    /// </summary>
    public const ushort NoValue = 0xFFFF;

    /// <summary>
    ///  Maximum value for Tot No Orders
    /// </summary>
    public const ushort Maximum = 65534;

    /// <summary>
    ///  Minimum value for Tot No Orders
    /// </summary>
    public const ushort Minimum = 0;

    /// <summary>
    ///  Fix Tag for Tot No Orders
    /// </summary>
    public const ushort FixTag = 68;

    /// <summary>
    ///  Size of Tot No Orders in bytes
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Tot No Orders value
    /// </summary>
    public readonly ushort Value
        => Decode();

    /// <summary>
    ///  Does Tot No Orders field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Tot No Orders
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ushort Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Tot No Orders
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out ushort value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Tot No Orders
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ushort value)
        => Underlying = value;

    /// <summary>
    ///  Set Tot No Orders to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Tot No Orders as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ushort Underlying;
}
