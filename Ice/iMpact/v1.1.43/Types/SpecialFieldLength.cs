using System.Buffers.Binary;
using System.Runtime.CompilerServices;

namespace Ice.iMpact;

/// <summary>
///  Special Field Length: Length of this field
/// </summary>

public struct SpecialFieldLength
{
    /// <summary>
    ///  Size of Special Field Length in bytes
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Special Field Length value
    /// </summary>
    public readonly short Value
        => Decode();

    /// <summary>
    ///  Read Special Field Length
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly short Decode()
        => BinaryPrimitives.ReverseEndianness(Underlying);

    /// <summary>
    ///  Write Special Field Length
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(short value)
        => Underlying = BinaryPrimitives.ReverseEndianness(value);

    /// <summary>
    ///  Special Field Length as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal short Underlying;
}
