using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Related Instrument Guid: Related Instrument GUID
/// </summary>

public struct RelatedInstrumentGuid
{
    /// <summary>
    ///  Fix Tag for Related Instrument Guid
    /// </summary>
    public const ushort FixTag = 37724;

    /// <summary>
    ///  Sentinel null value for Related Instrument Guid
    /// </summary>
    public const ulong NoValue = 18446744073709551615;

    /// <summary>
    ///  Size of Related Instrument Guid in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Related Instrument Guid value
    /// </summary>
    public readonly ulong Value
        => Decode();

    /// <summary>
    ///  Does Related Instrument Guid field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Related Instrument Guid
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ulong Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Related Instrument Guid
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out ulong value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Related Instrument Guid
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ulong value)
        => Underlying = value;

    /// <summary>
    ///  Set Related Instrument Guid to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Related Instrument Guid as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ulong Underlying;
}
