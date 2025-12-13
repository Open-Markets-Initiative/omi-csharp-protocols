using System.Runtime.CompilerServices;

namespace Iex.Tops;

/// <summary>
///  Version: Version of transport specification
/// </summary>

public struct Version
{
    /// <summary>
    ///  Length of Version in bytes
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Read Version
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public byte Decode()
        => Byte;

    /// <summary>
    ///  Write Version
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(byte value)
        => Byte = value;

    /// <summary>
    ///  Version as string
    /// </summary>
    public readonly override string ToString()
        => $"{Decode()}";

    /// <summary>
    ///  Underlying byte
    /// </summary>
    internal byte Byte;
}
