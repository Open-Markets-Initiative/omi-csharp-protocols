using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Deep.IexTp.v1_08;

/// <summary>
///  Version of transport specification
/// </summary>
public struct Version
{
    /// <summary>
    ///  Version of the IEX-TP transport specification
    /// </summary>
    public const byte StaticValue = 1;

    /// <summary>
    ///  The size of the Version field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Version of transport specification
    /// </summary>
    public byte Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Version field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly byte Decode()
        => Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Version field.
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
