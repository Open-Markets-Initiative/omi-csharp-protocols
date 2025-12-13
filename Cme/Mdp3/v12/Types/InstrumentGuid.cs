using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Instrument Guid: External unique instrument ID
/// </summary>

public struct InstrumentGuid
{
    /// <summary>
    ///  Fix Tag for Instrument Guid
    /// </summary>
    public const ushort FixTag = 37513;

    /// <summary>
    ///  Sentinel null value for Instrument Guid
    /// </summary>
    public const ulong NoValue = 18446744073709551615;

    /// <summary>
    ///  Size of Instrument Guid in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Instrument Guid value
    /// </summary>
    public readonly ulong Value
        => Decode();

    /// <summary>
    ///  Does Instrument Guid field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Instrument Guid
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ulong Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Instrument Guid
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out ulong value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Instrument Guid
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ulong value)
        => Underlying = value;

    /// <summary>
    ///  Set Instrument Guid to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Instrument Guid as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ulong Underlying;
}
