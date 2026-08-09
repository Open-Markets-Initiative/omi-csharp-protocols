using System.Runtime.CompilerServices;

namespace Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
///  Represents the AdjustedPocPrice field as a fixed-point decimal (8-byte little-endian integer divided by 10000).
/// </summary>

public struct AdjustedPocPrice
{
    /// <summary>
    ///  Decimal place factor for Adjusted Poc Price
    /// </summary>
    public const long Factor = 10000;

    /// <summary>
    ///  Size of AdjustedPocPrice in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Gets the decoded Adjusted Poc Price value.
    /// </summary>
    public readonly decimal Value
        => (decimal)Decode() / Factor;

    /// <summary>
    ///  Decodes the raw bytes into the Adjusted Poc Price value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Decode()
        => Underlying;

    /// <summary>
    ///  Encodes a value into the underlying Adjusted Poc Price bytes.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(long value)
        => Underlying = value;

    /// <summary>
    ///  Returns the string representation of the Adjusted Poc Price value.
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal long Underlying;
}
