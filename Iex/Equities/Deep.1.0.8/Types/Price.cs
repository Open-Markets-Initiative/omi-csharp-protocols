using System.Runtime.CompilerServices;

namespace Iex.Deep;

/// <summary>
///  Price: Price level to add/update in the IEX Order Book
/// </summary>

public struct Price
{
    /// <summary>
    ///  Decimal place factor for Price
    /// </summary>
    public const long Factor = 10000;

    /// <summary>
    ///  Size of Price in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Price value
    /// </summary>
    public readonly long Value
        => Decode();

    /// <summary>
    ///  Read Price
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Decode()
        => Underlying / Factor;

    /// <summary>
    ///  Write Price
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(long value)
        => Underlying = value * Factor;

    /// <summary>
    ///  Price as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal long Underlying;
}
