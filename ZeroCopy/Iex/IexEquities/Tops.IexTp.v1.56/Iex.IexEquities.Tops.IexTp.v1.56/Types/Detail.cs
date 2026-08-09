using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Detail of the Reg. SHO short sale price test restriction status
/// </summary>

public struct Detail
{
    /// <summary>
    ///  No Price Test In Place
    /// </summary>
    public const char NoPriceTestInPlace = ' ';

    /// <summary>
    ///  Short Sale Price Test Restriction In Effect Due To An Intraday Price Drop In The Security
    /// </summary>
    public const char ShortSalePriceTestRestrictionInEffectDueToAnIntradayPriceDropInTheSecurity = 'A';

    /// <summary>
    ///  Short Sale Price Test Restriction Remains In Effect From Prior Day
    /// </summary>
    public const char ShortSalePriceTestRestrictionRemainsInEffectFromPriorDay = 'C';

    /// <summary>
    ///  Short Sale Price Test Restriction Deactivated
    /// </summary>
    public const char ShortSalePriceTestRestrictionDeactivated = 'D';

    /// <summary>
    ///  Detail Not Available
    /// </summary>
    public const char DetailNotAvailable = 'N';

    /// <summary>
    ///  Size of Detail in bytes
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Gets the decoded Detail value.
    /// </summary>
    public readonly char Value
        => (char)Byte;

    /// <summary>
    ///  Decodes the raw bytes into the Detail value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly char Decode()
        => (char)Byte;

    /// <summary>
    ///  Encodes a value into the underlying Detail bytes.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(char value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the string representation of the Detail value.
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying byte
    /// </summary>
    internal byte Byte;
}
