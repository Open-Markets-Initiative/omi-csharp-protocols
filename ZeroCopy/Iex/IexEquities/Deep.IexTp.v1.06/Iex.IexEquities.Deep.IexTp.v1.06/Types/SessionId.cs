using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  Represents the SessionId field as 4-byte little-endian unsigned integer.
/// </summary>

public struct SessionId
{
    /// <summary>
    ///  Size of SessionId in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Gets the decoded Session Id value.
    /// </summary>
    public readonly uint Value
        => Decode();

    /// <summary>
    ///  Decodes the raw bytes into the Session Id value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => Underlying;

    /// <summary>
    ///  Encodes a value into the underlying Session Id bytes.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
        => Underlying = value;

    /// <summary>
    ///  Returns the string representation of the Session Id value.
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal uint Underlying;
}
