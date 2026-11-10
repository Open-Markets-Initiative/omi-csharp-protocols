using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Appendage Id
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum MassCancelResponseOptionalField : byte
{
    /// <summary>
    ///  Mass Cancel Response Optional Side Enum
    /// </summary>
    Side = 27,

    /// <summary>
    ///  Mass Cancel Response Optional Group ID Enum
    /// </summary>
    GroupId = 24,

    /// <summary>
    ///  Mass Cancel Response Optional UserRefIdx Enum
    /// </summary>
    UserRefIdx = 28,

}

/// <summary>
///  Reads and writes the Mass Cancel Response Optional Field field in place.
/// </summary>
public struct MassCancelResponseOptionalFieldField
{
    /// <summary>
    ///  The size of the Mass Cancel Response Optional Field field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Appendage Id
    /// </summary>
    public MassCancelResponseOptionalField Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Mass Cancel Response Optional Field field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly MassCancelResponseOptionalField Decode()
        => (MassCancelResponseOptionalField)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Mass Cancel Response Optional Field field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(MassCancelResponseOptionalField value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
