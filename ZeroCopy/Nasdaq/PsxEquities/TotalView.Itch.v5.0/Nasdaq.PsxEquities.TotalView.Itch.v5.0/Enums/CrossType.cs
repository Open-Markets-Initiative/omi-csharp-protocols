using System.Runtime.CompilerServices;

namespace Nasdaq.PsxEquities.TotalView.Itch.v5_0;

/// <summary>
///  The Nasdaq PSX cross session for which the message is being generated.
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum CrossType : byte
{
    /// <summary>
    ///  NASDAQ PSX Opening Cross.
    /// </summary>
    OpeningCross = (byte)'O',

    /// <summary>
    ///  NASDAQ PSX Closing Cross.
    /// </summary>
    ClosingCross = (byte)'C',

    /// <summary>
    ///  Cross for IPO and halted / paused securities.
    /// </summary>
    CrossHaltedOrPaused = (byte)'H',

    /// <summary>
    ///  NASDAQ PSX Cross Network: Intraday Cross and Post-Close Cross
    /// </summary>
    IntradayCrossAndPostCloseCross = (byte)'I',

}

/// <summary>
///  Reads and writes the Cross Type field in place.
/// </summary>
public struct CrossTypeField
{
    /// <summary>
    ///  The size of the Cross Type field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  The Nasdaq PSX cross session for which the message is being generated.
    /// </summary>
    public CrossType Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Cross Type field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly CrossType Decode()
        => (CrossType)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Cross Type field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(CrossType value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
