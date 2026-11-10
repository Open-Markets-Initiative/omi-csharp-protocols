using System.Runtime.CompilerServices;

namespace Nasdaq.PsxEquities.TotalView.Itch.v5_0;

/// <summary>
///  Identifies the security class for the issue as assigned by NASDAQ. See Appendix for allowable values.
/// </summary>
public struct IssueClassification
{
    /// <summary>
    ///  The size of the Issue Classification field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Identifies the security class for the issue as assigned by NASDAQ. See Appendix for allowable values.
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
    ///  Returns the wire value of the Issue Classification field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly byte Decode()
        => Byte;

    /// <summary>
    ///  Writes a value into the Issue Classification field.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    ///  The value does not fit its field.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(char value)
    {
        if (value > byte.MaxValue)
        {
            throw new ArgumentOutOfRangeException(nameof(value), global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.WriterValueDoesNotFit);
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
