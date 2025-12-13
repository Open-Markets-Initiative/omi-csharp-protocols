using System.Runtime.CompilerServices;

namespace Iex.Deep;

/// <summary>
///  Channel Id: Identifies the stream of bytes sequenced messages
/// </summary>

public struct ChannelId
{
    /// <summary>
    ///  Size of Channel Id in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Channel Id value
    /// </summary>
    public readonly uint Value
        => Decode();

    /// <summary>
    ///  Read Channel Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => Underlying;

    /// <summary>
    ///  Write Channel Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
        => Underlying = value;

    /// <summary>
    ///  Channel Id as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal uint Underlying;
}
