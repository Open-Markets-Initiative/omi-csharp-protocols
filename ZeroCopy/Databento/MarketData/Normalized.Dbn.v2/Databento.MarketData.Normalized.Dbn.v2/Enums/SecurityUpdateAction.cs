using System.Runtime.CompilerServices;

namespace Databento.MarketData.Normalized.Dbn.v2;

/// <summary>
///  Indicates if the instrument definition has been added, modified, or deleted
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum SecurityUpdateAction : byte
{
    /// <summary>
    ///  Add
    /// </summary>
    Add = (byte)'A',

    /// <summary>
    ///  Delete
    /// </summary>
    Delete = (byte)'D',

    /// <summary>
    ///  Modify
    /// </summary>
    Modify = (byte)'M',

    /// <summary>
    ///  Invalid
    /// </summary>
    Invalid = (byte)'~',

}

/// <summary>
///  Reads and writes the Security Update Action field in place.
/// </summary>
public struct SecurityUpdateActionField
{
    /// <summary>
    ///  The size of the Security Update Action field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Indicates if the instrument definition has been added, modified, or deleted
    /// </summary>
    public SecurityUpdateAction Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Security Update Action field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly SecurityUpdateAction Decode()
        => (SecurityUpdateAction)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Security Update Action field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(SecurityUpdateAction value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
