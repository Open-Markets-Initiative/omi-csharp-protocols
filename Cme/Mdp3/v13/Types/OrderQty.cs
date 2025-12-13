using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Order Qty: Quantity requested
/// </summary>

public struct OrderQty
{
    /// <summary>
    ///  Fix Tag for Order Qty
    /// </summary>
    public const ushort FixTag = 38;

    /// <summary>
    ///  Sentinel null value for Order Qty
    /// </summary>
    public const int NoValue = 2147483647;

    /// <summary>
    ///  Size of Order Qty in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Order Qty value
    /// </summary>
    public readonly int Value
        => Decode();

    /// <summary>
    ///  Does Order Qty field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Order Qty
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Order Qty
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out int value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Order Qty
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(int value)
        => Underlying = value;

    /// <summary>
    ///  Set Order Qty to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Order Qty as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal int Underlying;
}
