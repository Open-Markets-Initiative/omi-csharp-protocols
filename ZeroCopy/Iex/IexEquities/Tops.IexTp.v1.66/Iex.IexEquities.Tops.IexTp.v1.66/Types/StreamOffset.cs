using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Represents the StreamOffset field as 8-byte little-endian unsigned integer.
/// </summary>

public struct StreamOffset
{
    /// <summary>
    ///  Size of StreamOffset in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Gets the decoded Stream Offset value.
    /// </summary>
    public readonly ulong Value
        => Decode();

    /// <summary>
    ///  Decodes the raw bytes into the Stream Offset value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ulong Decode()
        => Underlying;

    /// <summary>
    ///  Encodes a value into the underlying Stream Offset bytes.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ulong value)
        => Underlying = value;

    /// <summary>
    ///  Returns the string representation of the Stream Offset value.
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ulong Underlying;
}
