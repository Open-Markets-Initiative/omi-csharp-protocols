using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Collateral Market Price: Market price of the collateral, either from external market sources or a result of trading activity on the platform. Clean price
/// </summary>

public struct CollateralMarketPrice
{
    /// <summary>
    ///  Fix Tag for Collateral Market Price
    /// </summary>
    public const ushort FixTag = 2689;

    /// <summary>
    ///  Decimal place factor for Collateral Market Price
    /// </summary>
    public const long Factor = 1000000000;

    /// <summary>
    ///  Size of Collateral Market Price in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Collateral Market Price value
    /// </summary>
    public readonly long Value
        => Decode();

    /// <summary>
    ///  Read Collateral Market Price
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Decode()
        => Underlying / Factor;

    /// <summary>
    ///  Write Collateral Market Price
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(long value)
        => Underlying = value * Factor;

    /// <summary>
    ///  Collateral Market Price as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal long Underlying;
}
