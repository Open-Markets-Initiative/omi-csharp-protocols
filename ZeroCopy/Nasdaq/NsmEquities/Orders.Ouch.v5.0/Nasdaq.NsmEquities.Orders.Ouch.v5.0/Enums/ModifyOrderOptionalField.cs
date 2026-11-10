using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Appendage Id
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum ModifyOrderOptionalField : byte
{
    /// <summary>
    ///  Modify Order Optional UserRefIdx Enum
    /// </summary>
    UserRefIdx = 28,

    /// <summary>
    ///  Modify Order Optional Shares Located Enum
    /// </summary>
    SharesLocated = 25,

    /// <summary>
    ///  Modify Order Optional Locate Broker Enum
    /// </summary>
    LocateBroker = 26,

}

/// <summary>
///  Reads and writes the Modify Order Optional Field field in place.
/// </summary>
public struct ModifyOrderOptionalFieldField
{
    /// <summary>
    ///  The size of the Modify Order Optional Field field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Appendage Id
    /// </summary>
    public ModifyOrderOptionalField Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Modify Order Optional Field field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ModifyOrderOptionalField Decode()
        => (ModifyOrderOptionalField)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Modify Order Optional Field field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ModifyOrderOptionalField value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
