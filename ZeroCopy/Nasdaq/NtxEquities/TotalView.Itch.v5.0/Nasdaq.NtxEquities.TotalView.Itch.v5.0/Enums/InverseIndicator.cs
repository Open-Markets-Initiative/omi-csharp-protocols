using System.Runtime.CompilerServices;

namespace Nasdaq.NtxEquities.TotalView.Itch.v5_0;

/// <summary>
///  Indicates the directional relationship between the ETP and underlying index
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum InverseIndicator : byte
{
    /// <summary>
    ///  ETP is an Inverse ETP
    /// </summary>
    InverseEtp = (byte)'Y',

    /// <summary>
    ///  ETP is not an Inverse ETP
    /// </summary>
    NotInverseEtp = (byte)'N',

}

/// <summary>
///  Reads and writes the Inverse Indicator field in place.
/// </summary>
public struct InverseIndicatorField
{
    /// <summary>
    ///  The size of the Inverse Indicator field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Indicates the directional relationship between the ETP and underlying index
    /// </summary>
    public InverseIndicator Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Inverse Indicator field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly InverseIndicator Decode()
        => (InverseIndicator)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Inverse Indicator field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(InverseIndicator value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
