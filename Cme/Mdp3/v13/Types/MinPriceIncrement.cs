using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Min Price Increment: Minimum constant tick for the instrument, sent only if instrument is non-VTT (Variable Tick table) eligible
/// </summary>

public struct MinPriceIncrement
{
    /// <summary>
    ///  Fix Tag for Min Price Increment
    /// </summary>
    public const ushort FixTag = 969;

    /// <summary>
    ///  Decimal place factor for Min Price Increment
    /// </summary>
    public const long Factor = 1000000000;

    /// <summary>
    ///  Size of Min Price Increment in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Min Price Increment value
    /// </summary>
    public readonly long Value
        => Decode();

    /// <summary>
    ///  Read Min Price Increment
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Decode()
        => Underlying / Factor;

    /// <summary>
    ///  Write Min Price Increment
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(long value)
        => Underlying = value * Factor;

    /// <summary>
    ///  Min Price Increment as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal long Underlying;
}
