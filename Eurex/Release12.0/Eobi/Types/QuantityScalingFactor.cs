using System.Runtime.CompilerServices;

namespace Eurex.Eobi;

/// <summary>
///  Quantity Scaling Factor
/// </summary>

public struct QuantityScalingFactor
{
    /// <summary>
    ///  Sentinel null value for Quantity Scaling Factor
    /// </summary>
    public const ushort NoValue = 0xFFFF;

    /// <summary>
    ///  Maximum value for Quantity Scaling Factor
    /// </summary>
    public const ushort Maximum = 1000;

    /// <summary>
    ///  Minimum value for Quantity Scaling Factor
    /// </summary>
    public const ushort Minimum = 1;

    /// <summary>
    ///  Fix Tag for Quantity Scaling Factor
    /// </summary>
    public const ushort FixTag = 28907;

    /// <summary>
    ///  Size of Quantity Scaling Factor in bytes
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Quantity Scaling Factor value
    /// </summary>
    public readonly ushort Value
        => Decode();

    /// <summary>
    ///  Does Quantity Scaling Factor field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Quantity Scaling Factor
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ushort Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Quantity Scaling Factor
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out ushort value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Quantity Scaling Factor
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ushort value)
        => Underlying = value;

    /// <summary>
    ///  Set Quantity Scaling Factor to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Quantity Scaling Factor as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ushort Underlying;
}
