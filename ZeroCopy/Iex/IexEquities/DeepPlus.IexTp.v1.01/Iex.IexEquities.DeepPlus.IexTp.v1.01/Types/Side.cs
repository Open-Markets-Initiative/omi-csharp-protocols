using System.Runtime.CompilerServices;

namespace Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
///  Side of order
/// </summary>

public struct Side
{
    /// <summary>
    ///  Buy
    /// </summary>
    public const char Buy = '8';

    /// <summary>
    ///  Sell
    /// </summary>
    public const char Sell = '5';

    /// <summary>
    ///  Size of Side in bytes
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Gets the decoded Side value.
    /// </summary>
    public readonly char Value
        => (char)Byte;

    /// <summary>
    ///  Decodes the raw bytes into the Side value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly char Decode()
        => (char)Byte;

    /// <summary>
    ///  Encodes a value into the underlying Side bytes.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(char value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the string representation of the Side value.
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying byte
    /// </summary>
    internal byte Byte;
}
