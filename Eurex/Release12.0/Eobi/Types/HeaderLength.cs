using System.Runtime.CompilerServices;

namespace Eurex.Eobi;

/// <summary>
///  Header Length: Packet header Length in Bytes
/// </summary>

public struct HeaderLength
{
    /// <summary>
    ///  Size of Header Length in bytes
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Header Length value
    /// </summary>
    public readonly ushort Value
        => Decode();

    /// <summary>
    ///  Read Header Length
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ushort Decode()
        => Underlying;

    /// <summary>
    ///  Write Header Length
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ushort value)
        => Underlying = value;

    /// <summary>
    ///  Header Length as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ushort Underlying;
}
