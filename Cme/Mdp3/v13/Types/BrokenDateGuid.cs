using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Broken Date Guid: External unique broken date Instrument ID
/// </summary>

public struct BrokenDateGuid
{
    /// <summary>
    ///  Fix Tag for Broken Date Guid
    /// </summary>
    public const ushort FixTag = 39031;

    /// <summary>
    ///  Size of Broken Date Guid in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Broken Date Guid value
    /// </summary>
    public readonly ulong Value
        => Decode();

    /// <summary>
    ///  Read Broken Date Guid
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ulong Decode()
        => Underlying;

    /// <summary>
    ///  Write Broken Date Guid
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ulong value)
        => Underlying = value;

    /// <summary>
    ///  Broken Date Guid as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ulong Underlying;
}
