using System.Runtime.CompilerServices;

namespace Iex.Tops;

/// <summary>
///  Round Lot Size: Number of shares that represent a round lot
/// </summary>

public struct RoundLotSize
{
    /// <summary>
    ///  Size of Round Lot Size in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Round Lot Size value
    /// </summary>
    public readonly uint Value
        => Decode();

    /// <summary>
    ///  Read Round Lot Size
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => Underlying;

    /// <summary>
    ///  Write Round Lot Size
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
        => Underlying = value;

    /// <summary>
    ///  Round Lot Size as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal uint Underlying;
}
