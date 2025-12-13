using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Packet Sequence Number: Packet Sequence Number
/// </summary>

public struct PacketSequenceNumber
{
    /// <summary>
    ///  Size of Packet Sequence Number in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Packet Sequence Number value
    /// </summary>
    public readonly uint Value
        => Decode();

    /// <summary>
    ///  Read Packet Sequence Number
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => Underlying;

    /// <summary>
    ///  Write Packet Sequence Number
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
        => Underlying = value;

    /// <summary>
    ///  Packet Sequence Number as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal uint Underlying;
}
