using System.Runtime.CompilerServices;

namespace Iex.Tops;

/// <summary>
///  Ask Price: Best quoted ask price
/// </summary>

public struct AskPrice
{
    /// <summary>
    ///  Decimal place factor for Ask Price
    /// </summary>
    public const long Factor = 10000;

    /// <summary>
    ///  Size of Ask Price in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Ask Price value
    /// </summary>
    public readonly long Value
        => Decode();

    /// <summary>
    ///  Read Ask Price
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Decode()
        => Underlying / Factor;

    /// <summary>
    ///  Write Ask Price
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(long value)
        => Underlying = value * Factor;

    /// <summary>
    ///  Ask Price as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal long Underlying;
}
