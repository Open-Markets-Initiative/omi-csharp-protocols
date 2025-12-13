using System.Runtime.CompilerServices;

namespace Iex.Tops;

/// <summary>
///  Message Length: Length of the message
/// </summary>

public struct MessageLength
{
    /// <summary>
    ///  Size of Message Length in bytes
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Message Length value
    /// </summary>
    public readonly ushort Value
        => Decode();

    /// <summary>
    ///  Read Message Length
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ushort Decode()
        => Underlying;

    /// <summary>
    ///  Write Message Length
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ushort value)
        => Underlying = value;

    /// <summary>
    ///  Message Length as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ushort Underlying;
}
