using System.Runtime.CompilerServices;

namespace Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
///  Represents the MessageCount field as 2-byte little-endian unsigned integer.
/// </summary>

public struct MessageCount
{
    /// <summary>
    ///  Size of MessageCount in bytes
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Gets the decoded Message Count value.
    /// </summary>
    public readonly ushort Value
        => Decode();

    /// <summary>
    ///  Decodes the raw bytes into the Message Count value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ushort Decode()
        => Underlying;

    /// <summary>
    ///  Encodes a value into the underlying Message Count bytes.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ushort value)
        => Underlying = value;

    /// <summary>
    ///  Returns the string representation of the Message Count value.
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ushort Underlying;
}
