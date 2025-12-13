using System.Runtime.CompilerServices;

namespace Iex.Tops;

/// <summary>
///  Imbalance Shares: Number of unpaired shares at the Reference Price using orders on the Auction Book
/// </summary>

public struct ImbalanceShares
{
    /// <summary>
    ///  Size of Imbalance Shares in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Imbalance Shares value
    /// </summary>
    public readonly uint Value
        => Decode();

    /// <summary>
    ///  Read Imbalance Shares
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => Underlying;

    /// <summary>
    ///  Write Imbalance Shares
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
        => Underlying = value;

    /// <summary>
    ///  Imbalance Shares as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal uint Underlying;
}
