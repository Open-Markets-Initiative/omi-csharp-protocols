using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Represents the ChannelId field as 4-byte little-endian unsigned integer.
/// </summary>

public struct ChannelId
{
    /// <summary>
    ///  Size of ChannelId in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Gets the decoded Channel Id value.
    /// </summary>
    public readonly uint Value
        => Decode();

    /// <summary>
    ///  Decodes the raw bytes into the Channel Id value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => Underlying;

    /// <summary>
    ///  Encodes a value into the underlying Channel Id bytes.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
        => Underlying = value;

    /// <summary>
    ///  Returns the string representation of the Channel Id value.
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal uint Underlying;
}
