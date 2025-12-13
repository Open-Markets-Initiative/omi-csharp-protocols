using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Underlying Instrument Guid Optional: Underlying GUID. For Repo specials populated with individual instrument GUID of the underlying security
/// </summary>

public struct UnderlyingInstrumentGuidOptional
{
    /// <summary>
    ///  Fix Tag for Underlying Instrument Guid Optional
    /// </summary>
    public const ushort FixTag = 37519;

    /// <summary>
    ///  Sentinel null value for Underlying Instrument Guid Optional
    /// </summary>
    public const ulong NoValue = 18446744073709551615;

    /// <summary>
    ///  Size of Underlying Instrument Guid Optional in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Underlying Instrument Guid Optional value
    /// </summary>
    public readonly ulong Value
        => Decode();

    /// <summary>
    ///  Does Underlying Instrument Guid Optional field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Underlying Instrument Guid Optional
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ulong Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Underlying Instrument Guid Optional
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out ulong value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Underlying Instrument Guid Optional
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ulong value)
        => Underlying = value;

    /// <summary>
    ///  Set Underlying Instrument Guid Optional to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Underlying Instrument Guid Optional as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ulong Underlying;
}
