using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Shares located for short sale order
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum SharesLocated : byte
{
    /// <summary>
    ///  yes
    /// </summary>
    Yes = (byte)'Y',

    /// <summary>
    ///  no (default)
    /// </summary>
    No = (byte)'N',

}

/// <summary>
///  Reads and writes the Shares Located field in place.
/// </summary>
public struct SharesLocatedField
{
    /// <summary>
    ///  The size of the Shares Located field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Shares located for short sale order
    /// </summary>
    public SharesLocated Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Shares Located field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly SharesLocated Decode()
        => (SharesLocated)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Shares Located field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(SharesLocated value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
