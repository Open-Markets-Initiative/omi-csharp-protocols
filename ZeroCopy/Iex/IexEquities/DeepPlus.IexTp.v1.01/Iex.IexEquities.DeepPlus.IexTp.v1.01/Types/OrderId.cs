using System.Runtime.CompilerServices;

namespace Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
///  Represents the OrderId field as 8-byte little-endian unsigned integer.
/// </summary>

public struct OrderId
{
    /// <summary>
    ///  Size of OrderId in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Gets the decoded Order Id value.
    /// </summary>
    public readonly ulong Value
        => Decode();

    /// <summary>
    ///  Decodes the raw bytes into the Order Id value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ulong Decode()
        => Underlying;

    /// <summary>
    ///  Encodes a value into the underlying Order Id bytes.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ulong value)
        => Underlying = value;

    /// <summary>
    ///  Returns the string representation of the Order Id value.
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ulong Underlying;
}
