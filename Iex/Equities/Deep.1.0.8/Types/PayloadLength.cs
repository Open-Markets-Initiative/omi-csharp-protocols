using System.Runtime.CompilerServices;

namespace Iex.Deep;

/// <summary>
///  Payload Length: Byte length of the payload
/// </summary>

public struct PayloadLength
{
    /// <summary>
    ///  Size of Payload Length in bytes
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Payload Length value
    /// </summary>
    public readonly ushort Value
        => Decode();

    /// <summary>
    ///  Read Payload Length
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ushort Decode()
        => Underlying;

    /// <summary>
    ///  Write Payload Length
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ushort value)
        => Underlying = value;

    /// <summary>
    ///  Payload Length as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ushort Underlying;
}
