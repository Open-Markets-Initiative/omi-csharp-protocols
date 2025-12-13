using System.Runtime.CompilerServices;

namespace Iex.Tops;

/// <summary>
///  Message Count: Number of messages in the payload
/// </summary>

public struct MessageCount
{
    /// <summary>
    ///  Size of Message Count in bytes
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Message Count value
    /// </summary>
    public readonly ushort Value
        => Decode();

    /// <summary>
    ///  Read Message Count
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ushort Decode()
        => Underlying;

    /// <summary>
    ///  Write Message Count
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ushort value)
        => Underlying = value;

    /// <summary>
    ///  Message Count as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ushort Underlying;
}
