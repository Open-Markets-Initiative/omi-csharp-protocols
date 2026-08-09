using System.Runtime.CompilerServices;

namespace Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
///  Represents the Reserved1 field as a single ASCII character.
/// </summary>

public struct Reserved1
{
    /// <summary>
    ///  Size of Reserved1 in bytes
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Gets the decoded Reserved 1 value.
    /// </summary>
    public readonly char Value
        => (char)Byte;

    /// <summary>
    ///  Decodes the raw bytes into the Reserved 1 value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly char Decode()
        => (char)Byte;

    /// <summary>
    ///  Encodes a value into the underlying Reserved 1 bytes.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(char value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the string representation of the Reserved 1 value.
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying byte
    /// </summary>
    internal byte Byte;
}
