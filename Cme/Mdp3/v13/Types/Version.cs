using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Version
/// </summary>

public struct Version
{
    /// <summary>
    ///  Required value for Version
    /// </summary>
    public const ushort StaticValue = 13;

    /// <summary>
    ///  Size of Version in bytes
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Version value
    /// </summary>
    public readonly ushort Value
        => Decode();

    /// <summary>
    ///  Read Version
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ushort Decode()
        => Underlying;

    /// <summary>
    ///  Write Version
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ushort value)
        => Underlying = value;

    /// <summary>
    ///  Version as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ushort Underlying;
}
