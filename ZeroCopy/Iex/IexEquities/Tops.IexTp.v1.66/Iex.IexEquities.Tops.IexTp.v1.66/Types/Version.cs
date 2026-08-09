using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Represents the Version field as 1-byte little-endian unsigned integer.
/// </summary>

public struct Version
{
    /// <summary>
    ///  Size of Version in bytes
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Gets the decoded Version value.
    /// </summary>
    public readonly byte Value
        => Decode();

    /// <summary>
    ///  Decodes the raw bytes into the Version value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly byte Decode()
        => Byte;

    /// <summary>
    ///  Encodes a value into the underlying Version bytes.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(byte value)
        => Byte = value;

    /// <summary>
    ///  Returns the string representation of the Version value.
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying byte
    /// </summary>
    internal byte Byte;
}
