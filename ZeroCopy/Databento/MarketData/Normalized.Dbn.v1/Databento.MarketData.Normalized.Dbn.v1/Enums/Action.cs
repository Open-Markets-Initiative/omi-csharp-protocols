using System.Runtime.CompilerServices;

namespace Databento.MarketData.Normalized.Dbn.v1;

/// <summary>
///  The event action
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum Action : byte
{
    /// <summary>
    ///  A new order was added to the book
    /// </summary>
    Add = (byte)'A',

    /// <summary>
    ///  An order was fully or partially cancelled
    /// </summary>
    Cancel = (byte)'C',

    /// <summary>
    ///  An existing order was filled; does not affect the book
    /// </summary>
    Fill = (byte)'F',

    /// <summary>
    ///  An existing order was modified: price and/or size
    /// </summary>
    Modify = (byte)'M',

    /// <summary>
    ///  Has no effect on the book, but may carry flags or other information
    /// </summary>
    None = (byte)'N',

    /// <summary>
    ///  Reset the book; clear all orders for an instrument
    /// </summary>
    Clear = (byte)'R',

    /// <summary>
    ///  An aggressing order traded; does not affect the book
    /// </summary>
    Trade = (byte)'T',

}

/// <summary>
///  Reads and writes the Action field in place.
/// </summary>
public struct ActionField
{
    /// <summary>
    ///  The size of the Action field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  The event action
    /// </summary>
    public Action Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Action field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly Action Decode()
        => (Action)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Action field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(Action value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
