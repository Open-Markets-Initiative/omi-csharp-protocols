using System.Runtime.CompilerServices;

namespace Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
///  Represents the TradeId field as 8-byte little-endian unsigned integer.
/// </summary>

public struct TradeId
{
    /// <summary>
    ///  Size of TradeId in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Gets the decoded Trade Id value.
    /// </summary>
    public readonly ulong Value
        => Decode();

    /// <summary>
    ///  Decodes the raw bytes into the Trade Id value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ulong Decode()
        => Underlying;

    /// <summary>
    ///  Encodes a value into the underlying Trade Id bytes.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ulong value)
        => Underlying = value;

    /// <summary>
    ///  Returns the string representation of the Trade Id value.
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ulong Underlying;
}
