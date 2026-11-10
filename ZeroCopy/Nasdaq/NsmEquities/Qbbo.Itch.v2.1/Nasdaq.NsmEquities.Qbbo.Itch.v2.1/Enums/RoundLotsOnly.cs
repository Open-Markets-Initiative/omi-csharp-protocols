using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Qbbo.Itch.v2_1;

/// <summary>
///  Indicates if Nasdaq system limits order entry for issue
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum RoundLotsOnly : byte
{
    /// <summary>
    ///  only round lots are accepted in this stock
    /// </summary>
    RoundLotsOnlyValue = (byte)'Y',

    /// <summary>
    ///  odd/mixed lots are allowed
    /// </summary>
    NoRestrictions = (byte)'N',

}

/// <summary>
///  Reads and writes the Round Lots Only field in place.
/// </summary>
public struct RoundLotsOnlyField
{
    /// <summary>
    ///  The size of the Round Lots Only field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Indicates if Nasdaq system limits order entry for issue
    /// </summary>
    public RoundLotsOnly Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Round Lots Only field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly RoundLotsOnly Decode()
        => (RoundLotsOnly)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Round Lots Only field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(RoundLotsOnly value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
