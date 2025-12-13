using System.Runtime.CompilerServices;

namespace Iex.Deep;

/// <summary>
///  Adjusted Poc Price: Corporate action adjusted previous official closing price
/// </summary>

public struct AdjustedPocPrice
{
    /// <summary>
    ///  Decimal place factor for Adjusted Poc Price
    /// </summary>
    public const long Factor = 10000;

    /// <summary>
    ///  Size of Adjusted Poc Price in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Adjusted Poc Price value
    /// </summary>
    public readonly long Value
        => Decode();

    /// <summary>
    ///  Read Adjusted Poc Price
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Decode()
        => Underlying / Factor;

    /// <summary>
    ///  Write Adjusted Poc Price
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(long value)
        => Underlying = value * Factor;

    /// <summary>
    ///  Adjusted Poc Price as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal long Underlying;
}
