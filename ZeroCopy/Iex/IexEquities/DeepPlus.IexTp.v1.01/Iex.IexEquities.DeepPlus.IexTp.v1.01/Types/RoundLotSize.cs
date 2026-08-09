using System.Runtime.CompilerServices;

namespace Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
///  Represents the RoundLotSize field as 4-byte little-endian unsigned integer.
/// </summary>

public struct RoundLotSize
{
    /// <summary>
    ///  Size of RoundLotSize in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Gets the decoded Round Lot Size value.
    /// </summary>
    public readonly uint Value
        => Decode();

    /// <summary>
    ///  Decodes the raw bytes into the Round Lot Size value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => Underlying;

    /// <summary>
    ///  Encodes a value into the underlying Round Lot Size bytes.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
        => Underlying = value;

    /// <summary>
    ///  Returns the string representation of the Round Lot Size value.
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal uint Underlying;
}
