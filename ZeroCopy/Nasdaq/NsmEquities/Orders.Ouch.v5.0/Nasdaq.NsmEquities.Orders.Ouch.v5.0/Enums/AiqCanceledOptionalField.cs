using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Appendage Id
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum AiqCanceledOptionalField : byte
{
    /// <summary>
    ///  AIQ Canceled Optional UserRefIdx Enum
    /// </summary>
    UserRefIdx = 28,

}

/// <summary>
///  Reads and writes the Aiq Canceled Optional Field field in place.
/// </summary>
public struct AiqCanceledOptionalFieldField
{
    /// <summary>
    ///  The size of the Aiq Canceled Optional Field field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Appendage Id
    /// </summary>
    public AiqCanceledOptionalField Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Aiq Canceled Optional Field field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly AiqCanceledOptionalField Decode()
        => (AiqCanceledOptionalField)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Aiq Canceled Optional Field field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(AiqCanceledOptionalField value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
