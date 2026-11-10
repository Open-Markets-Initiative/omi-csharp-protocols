using System.Runtime.CompilerServices;

namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

/// <summary>
///  Option Type:
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum OptionType : byte
{
    /// <summary>
    ///  Call option
    /// </summary>
    Call = (byte)'C',

    /// <summary>
    ///  Put option
    /// </summary>
    Put = (byte)'P',

    /// <summary>
    ///  Stock Leg
    /// </summary>
    Stock = (byte)' ',

}

/// <summary>
///  Reads and writes the Option Type field in place.
/// </summary>
public struct OptionTypeField
{
    /// <summary>
    ///  The size of the Option Type field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Option Type:
    /// </summary>
    public OptionType Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Option Type field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly OptionType Decode()
        => (OptionType)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Option Type field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(OptionType value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
