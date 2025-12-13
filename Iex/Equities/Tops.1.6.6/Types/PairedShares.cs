using System.Runtime.CompilerServices;

namespace Iex.Tops;

/// <summary>
///  Paired Shares: Number of shares paired at the Reference Price using orders on the Auction Book
/// </summary>

public struct PairedShares
{
    /// <summary>
    ///  Size of Paired Shares in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Paired Shares value
    /// </summary>
    public readonly uint Value
        => Decode();

    /// <summary>
    ///  Read Paired Shares
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => Underlying;

    /// <summary>
    ///  Write Paired Shares
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
        => Underlying = value;

    /// <summary>
    ///  Paired Shares as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal uint Underlying;
}
