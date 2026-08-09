using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Represents the CollarReferencePrice field as a fixed-point decimal (8-byte little-endian integer divided by 10000).
/// </summary>

public struct CollarReferencePrice
{
    /// <summary>
    ///  Decimal place factor for Collar Reference Price
    /// </summary>
    public const long Factor = 10000;

    /// <summary>
    ///  Size of CollarReferencePrice in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Gets the decoded Collar Reference Price value.
    /// </summary>
    public readonly decimal Value
        => (decimal)Decode() / Factor;

    /// <summary>
    ///  Decodes the raw bytes into the Collar Reference Price value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Decode()
        => Underlying;

    /// <summary>
    ///  Encodes a value into the underlying Collar Reference Price bytes.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(long value)
        => Underlying = value;

    /// <summary>
    ///  Returns the string representation of the Collar Reference Price value.
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal long Underlying;
}
