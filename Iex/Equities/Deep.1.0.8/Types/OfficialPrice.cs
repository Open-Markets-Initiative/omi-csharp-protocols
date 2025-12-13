using System.Runtime.CompilerServices;

namespace Iex.Deep;

/// <summary>
///  Official Price: Official opening or closing price, as specified
/// </summary>

public struct OfficialPrice
{
    /// <summary>
    ///  Decimal place factor for Official Price
    /// </summary>
    public const long Factor = 10000;

    /// <summary>
    ///  Size of Official Price in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Official Price value
    /// </summary>
    public readonly long Value
        => Decode();

    /// <summary>
    ///  Read Official Price
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Decode()
        => Underlying / Factor;

    /// <summary>
    ///  Write Official Price
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(long value)
        => Underlying = value * Factor;

    /// <summary>
    ///  Official Price as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal long Underlying;
}
