using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  Price type identifier
/// </summary>

public struct PriceType
{
    /// <summary>
    ///  Iex Official Opening Price
    /// </summary>
    public const char IexOfficialOpeningPrice = 'Q';

    /// <summary>
    ///  Iex Official Closing Price
    /// </summary>
    public const char IexOfficialClosingPrice = 'M';

    /// <summary>
    ///  Size of PriceType in bytes
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Gets the decoded Price Type value.
    /// </summary>
    public readonly char Value
        => (char)Byte;

    /// <summary>
    ///  Decodes the raw bytes into the Price Type value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly char Decode()
        => (char)Byte;

    /// <summary>
    ///  Encodes a value into the underlying Price Type bytes.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(char value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the string representation of the Price Type value.
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying byte
    /// </summary>
    internal byte Byte;
}
