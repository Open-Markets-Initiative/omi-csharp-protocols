using System.Runtime.CompilerServices;

namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  Indicates if the instrument is user defined
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum UserDefinedInstrument : byte
{
    /// <summary>
    ///  No
    /// </summary>
    No = (byte)'N',

    /// <summary>
    ///  Yes
    /// </summary>
    Yes = (byte)'Y',

}

/// <summary>
///  Reads and writes the User Defined Instrument field in place.
/// </summary>
public struct UserDefinedInstrumentField
{
    /// <summary>
    ///  The size of the User Defined Instrument field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Indicates if the instrument is user defined
    /// </summary>
    public UserDefinedInstrument Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the User Defined Instrument field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly UserDefinedInstrument Decode()
        => (UserDefinedInstrument)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the User Defined Instrument field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(UserDefinedInstrument value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
