using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  Represents the PayloadLength field as 2-byte little-endian unsigned integer.
/// </summary>

public struct PayloadLength
{
    /// <summary>
    ///  Size of PayloadLength in bytes
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Gets the decoded Payload Length value.
    /// </summary>
    public readonly ushort Value
        => Decode();

    /// <summary>
    ///  Decodes the raw bytes into the Payload Length value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ushort Decode()
        => Underlying;

    /// <summary>
    ///  Encodes a value into the underlying Payload Length bytes.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ushort value)
        => Underlying = value;

    /// <summary>
    ///  Returns the string representation of the Payload Length value.
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ushort Underlying;
}
