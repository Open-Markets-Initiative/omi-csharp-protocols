using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Underlying Instrument Guid: Underlying Instrument GUID. Collateral security identifier in REF Data API services
/// </summary>

public struct UnderlyingInstrumentGuid
{
    /// <summary>
    ///  Fix Tag for Underlying Instrument Guid
    /// </summary>
    public const ushort FixTag = 37519;

    /// <summary>
    ///  Size of Underlying Instrument Guid in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Underlying Instrument Guid value
    /// </summary>
    public readonly ulong Value
        => Decode();

    /// <summary>
    ///  Read Underlying Instrument Guid
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ulong Decode()
        => Underlying;

    /// <summary>
    ///  Write Underlying Instrument Guid
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ulong value)
        => Underlying = value;

    /// <summary>
    ///  Underlying Instrument Guid as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ulong Underlying;
}
