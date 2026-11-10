using System.Runtime.CompilerServices;

namespace Nasdaq.PsxEquities.TotalView.Itch.v5_0;

/// <summary>
///  Denotes the Reg SHO Short Sale Price Test Restriction status for the issue at the time of the message dissemination
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum RegShoAction : byte
{
    /// <summary>
    ///  No price test in place
    /// </summary>
    NoPriceTest = (byte)'0',

    /// <summary>
    ///  Reg SHO Short Sale Price Test Restriction in effect due to an intra-day price drop in security
    /// </summary>
    RegShoShortSalePriceTestRestriction = (byte)'1',

    /// <summary>
    ///  Reg SHO Short Sale Price Test Restriction remains in effect
    /// </summary>
    TestRestrictionRemains = (byte)'2',

}

/// <summary>
///  Reads and writes the Reg Sho Action field in place.
/// </summary>
public struct RegShoActionField
{
    /// <summary>
    ///  The size of the Reg Sho Action field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Denotes the Reg SHO Short Sale Price Test Restriction status for the issue at the time of the message dissemination
    /// </summary>
    public RegShoAction Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Reg Sho Action field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly RegShoAction Decode()
        => (RegShoAction)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Reg Sho Action field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(RegShoAction value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
