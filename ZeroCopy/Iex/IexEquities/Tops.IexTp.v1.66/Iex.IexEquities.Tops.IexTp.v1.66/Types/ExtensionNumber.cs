using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Tops.IexTp.v1_66;

/// <summary>
///  Number of extensions an auction received
/// </summary>
public struct ExtensionNumber
{
    /// <summary>
    ///  The size of the Extension Number field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Number of extensions an auction received
    /// </summary>
    public byte Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Extension Number field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly byte Decode()
        => Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Extension Number field.
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
