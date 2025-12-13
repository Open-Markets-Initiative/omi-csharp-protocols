using System.Runtime.CompilerServices;

namespace Eurex.Eobi;

/// <summary>
///  Packet Id: Packet Identifier
/// </summary>

public struct PacketId
{
    /// <summary>
    ///  Required value for Packet Id
    /// </summary>
    public const ushort StaticValue = 13000;

    /// <summary>
    ///  Size of Packet Id in bytes
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Packet Id value
    /// </summary>
    public readonly ushort Value
        => Decode();

    /// <summary>
    ///  Read Packet Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ushort Decode()
        => Underlying;

    /// <summary>
    ///  Write Packet Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ushort value)
        => Underlying = value;

    /// <summary>
    ///  Packet Id as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ushort Underlying;
}
