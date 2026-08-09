using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  Represents the MessageProtocolId field as 2-byte little-endian unsigned integer.
/// </summary>

public struct MessageProtocolId
{
    /// <summary>
    ///  Required value for Message Protocol Id
    /// </summary>
    public const ushort StaticValue = 32772;

    /// <summary>
    ///  Size of MessageProtocolId in bytes
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Gets the decoded Message Protocol Id value.
    /// </summary>
    public readonly ushort Value
        => Decode();

    /// <summary>
    ///  Decodes the raw bytes into the Message Protocol Id value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ushort Decode()
        => Underlying;

    /// <summary>
    ///  Encodes a value into the underlying Message Protocol Id bytes.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ushort value)
        => Underlying = value;

    /// <summary>
    ///  Returns the string representation of the Message Protocol Id value.
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ushort Underlying;
}
