using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Max Price Discretion Offset: Max allowed discretionary offset from Limit order price. When the value in this field = 0, discretionary price cannot be submitted for the instrument
/// </summary>

public struct MaxPriceDiscretionOffset
{
    /// <summary>
    ///  Fix Tag for Max Price Discretion Offset
    /// </summary>
    public const ushort FixTag = 37728;

    /// <summary>
    ///  Decimal place factor for Max Price Discretion Offset
    /// </summary>
    public const long Factor = 1000000000;

    /// <summary>
    ///  Size of Max Price Discretion Offset in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Max Price Discretion Offset value
    /// </summary>
    public readonly long Value
        => Decode();

    /// <summary>
    ///  Read Max Price Discretion Offset
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Decode()
        => Underlying / Factor;

    /// <summary>
    ///  Write Max Price Discretion Offset
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(long value)
        => Underlying = value * Factor;

    /// <summary>
    ///  Max Price Discretion Offset as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal long Underlying;
}
