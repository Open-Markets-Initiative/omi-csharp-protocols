using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Side of the unpaired shares at the Reference Price using orders on the Auction Book
/// </summary>

public struct ImbalanceSide
{
    /// <summary>
    ///  Buy
    /// </summary>
    public const char Buy = 'B';

    /// <summary>
    ///  Sell
    /// </summary>
    public const char Sell = 'S';

    /// <summary>
    ///  No Imbalance
    /// </summary>
    public const char None = 'N';

    /// <summary>
    ///  Size of ImbalanceSide in bytes
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Gets the decoded Imbalance Side value.
    /// </summary>
    public readonly char Value
        => (char)Byte;

    /// <summary>
    ///  Decodes the raw bytes into the Imbalance Side value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly char Decode()
        => (char)Byte;

    /// <summary>
    ///  Encodes a value into the underlying Imbalance Side bytes.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(char value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the string representation of the Imbalance Side value.
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying byte
    /// </summary>
    internal byte Byte;
}
