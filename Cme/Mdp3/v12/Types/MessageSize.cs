using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Message Size: Message Size
/// </summary>

public struct MessageSize
{
    /// <summary>
    ///  Size of Message Size in bytes
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Message Size value
    /// </summary>
    public readonly ushort Value
        => Decode();

    /// <summary>
    ///  Read Message Size
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ushort Decode()
        => Underlying;

    /// <summary>
    ///  Write Message Size
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ushort value)
        => Underlying = value;

    /// <summary>
    ///  Message Size as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ushort Underlying;
}
