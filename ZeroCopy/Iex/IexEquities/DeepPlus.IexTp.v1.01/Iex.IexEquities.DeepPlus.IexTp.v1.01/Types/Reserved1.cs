using System.Runtime.CompilerServices;

namespace Iex.IexEquities.DeepPlus.IexTp.v1_01;

/// <summary>
///  Reserved for future use
/// </summary>
public struct Reserved1
{
    /// <summary>
    ///  The size of the Reserved 1 field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Reserved for future use
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    ///  The value does not fit its field.
    /// </exception>
    public char Value
    {
        readonly get => (char)Byte;
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Reserved 1 field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly byte Decode()
        => Byte;

    /// <summary>
    ///  Writes a value into the Reserved 1 field.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    ///  The value does not fit its field.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(char value)
    {
        if (value > byte.MaxValue)
        {
            throw new ArgumentOutOfRangeException(nameof(value), global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterValueDoesNotFit);
        }
        Byte = (byte)value;
    }

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    internal byte Byte;
}
