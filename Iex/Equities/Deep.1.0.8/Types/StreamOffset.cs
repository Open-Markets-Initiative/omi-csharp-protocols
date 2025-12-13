using System.Runtime.CompilerServices;

namespace Iex.Deep;

/// <summary>
///  Stream Offset: Byte offset of the data stream
/// </summary>

public struct StreamOffset
{
    /// <summary>
    ///  Size of Stream Offset in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Stream Offset value
    /// </summary>
    public readonly ulong Value
        => Decode();

    /// <summary>
    ///  Read Stream Offset
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ulong Decode()
        => Underlying;

    /// <summary>
    ///  Write Stream Offset
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ulong value)
        => Underlying = value;

    /// <summary>
    ///  Stream Offset as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ulong Underlying;
}
