using System.Runtime.CompilerServices;

namespace Iex.Tops;

/// <summary>
///  Collar Reference Price: Reference priced used for the auction collar, if any
/// </summary>

public struct CollarReferencePrice
{
    /// <summary>
    ///  Decimal place factor for Collar Reference Price
    /// </summary>
    public const long Factor = 10000;

    /// <summary>
    ///  Size of Collar Reference Price in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Collar Reference Price value
    /// </summary>
    public readonly long Value
        => Decode();

    /// <summary>
    ///  Read Collar Reference Price
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Decode()
        => Underlying / Factor;

    /// <summary>
    ///  Write Collar Reference Price
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(long value)
        => Underlying = value * Factor;

    /// <summary>
    ///  Collar Reference Price as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal long Underlying;
}
