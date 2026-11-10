using System.Runtime.CompilerServices;

namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

/// <summary>
///  Number of auction Responses. Next two fields repeat that number of times. Allowable values for this field are 0 or 1.
/// </summary>
public struct NumberOfResponses
{
    /// <summary>
    ///  The size of the Number Of Responses field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Number of auction Responses. Next two fields repeat that number of times. Allowable values for this field are 0 or 1.
    /// </summary>
    public byte Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Number Of Responses field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly byte Decode()
        => Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Number Of Responses field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(byte value)
        => Byte = value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
