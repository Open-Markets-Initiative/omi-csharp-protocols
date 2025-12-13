using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Underlying Security Id Optional: Underlying Security ID as qualified by tag 305-UnderlyingSecurityIDSource. Provided only if the underlying is a Globex listed instrument, this value will be the same as that contained in Security Definition Tag 48-SecurityID.
/// </summary>

public struct UnderlyingSecurityIdOptional
{
    /// <summary>
    ///  Fix Tag for Underlying Security Id Optional
    /// </summary>
    public const ushort FixTag = 309;

    /// <summary>
    ///  Sentinel null value for Underlying Security Id Optional
    /// </summary>
    public const int NoValue = 2147483647;

    /// <summary>
    ///  Size of Underlying Security Id Optional in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Underlying Security Id Optional value
    /// </summary>
    public readonly int Value
        => Decode();

    /// <summary>
    ///  Does Underlying Security Id Optional field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Underlying Security Id Optional
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Underlying Security Id Optional
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out int value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Underlying Security Id Optional
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(int value)
        => Underlying = value;

    /// <summary>
    ///  Set Underlying Security Id Optional to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Underlying Security Id Optional as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal int Underlying;
}
