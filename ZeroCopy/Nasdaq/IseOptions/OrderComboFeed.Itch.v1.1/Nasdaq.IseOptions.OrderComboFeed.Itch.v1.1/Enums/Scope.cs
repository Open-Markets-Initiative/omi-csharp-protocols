using System.Runtime.CompilerServices;

namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

/// <summary>
///  Scope value
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum Scope : byte
{
    /// <summary>
    ///  Local
    /// </summary>
    Local = (byte)'L',

    /// <summary>
    ///  National
    /// </summary>
    National = (byte)'N',

}

/// <summary>
///  Reads and writes the Scope field in place.
/// </summary>
public struct ScopeField
{
    /// <summary>
    ///  The size of the Scope field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Scope value
    /// </summary>
    public Scope Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Scope field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly Scope Decode()
        => (Scope)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Scope field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(Scope value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
