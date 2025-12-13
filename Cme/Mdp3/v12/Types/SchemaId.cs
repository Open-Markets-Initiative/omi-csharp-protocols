using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Schema Id
/// </summary>

public struct SchemaId
{
    /// <summary>
    ///  Required value for Schema Id
    /// </summary>
    public const ushort StaticValue = 1;

    /// <summary>
    ///  Size of Schema Id in bytes
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Schema Id value
    /// </summary>
    public readonly ushort Value
        => Decode();

    /// <summary>
    ///  Read Schema Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ushort Decode()
        => Underlying;

    /// <summary>
    ///  Write Schema Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ushort value)
        => Underlying = value;

    /// <summary>
    ///  Schema Id as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ushort Underlying;
}
