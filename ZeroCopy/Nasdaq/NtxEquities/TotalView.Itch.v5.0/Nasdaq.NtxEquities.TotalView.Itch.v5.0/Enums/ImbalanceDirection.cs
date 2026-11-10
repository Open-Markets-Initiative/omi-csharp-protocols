using System.Runtime.CompilerServices;

namespace Nasdaq.NtxEquities.TotalView.Itch.v5_0;

/// <summary>
///  The market side of the order imbalance.
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum ImbalanceDirection : byte
{
    /// <summary>
    ///  buy imbalance
    /// </summary>
    Buy = (byte)'B',

    /// <summary>
    ///  sell imbalance
    /// </summary>
    Sell = (byte)'S',

    /// <summary>
    ///  no imbalance
    /// </summary>
    NoImbalance = (byte)'N',

    /// <summary>
    ///  Insufficient orders to calculate
    /// </summary>
    Insufficient = (byte)'O',

}

/// <summary>
///  Reads and writes the Imbalance Direction field in place.
/// </summary>
public struct ImbalanceDirectionField
{
    /// <summary>
    ///  The size of the Imbalance Direction field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  The market side of the order imbalance.
    /// </summary>
    public ImbalanceDirection Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Imbalance Direction field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ImbalanceDirection Decode()
        => (ImbalanceDirection)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Imbalance Direction field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ImbalanceDirection value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
