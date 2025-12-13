using System.Runtime.CompilerServices;

namespace Iex.Deep;

/// <summary>
///  Message Protocol Id: Unique identifier of the higher layer protocol
/// </summary>

public struct MessageProtocolId
{
    /// <summary>
    ///  Size of Message Protocol Id in bytes
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Message Protocol Id value
    /// </summary>
    public readonly ushort Value
        => Decode();

    /// <summary>
    ///  Read Message Protocol Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ushort Decode()
        => Underlying;

    /// <summary>
    ///  Write Message Protocol Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ushort value)
        => Underlying = value;

    /// <summary>
    ///  Message Protocol Id as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ushort Underlying;
}
