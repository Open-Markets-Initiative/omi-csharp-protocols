using System.Runtime.CompilerServices;

namespace Nasdaq.PsxEquities.TotalView.Itch.v5_0;

/// <summary>
///  Indicates the market participant’s current registration status in the issue
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum MarketParticipantState : byte
{
    /// <summary>
    ///  Active
    /// </summary>
    Active = (byte)'A',

    /// <summary>
    ///  Excused/Withdrawn
    /// </summary>
    ExcusedWithdrawn = (byte)'E',

    /// <summary>
    ///  Withdrawn
    /// </summary>
    Withdrawn = (byte)'W',

    /// <summary>
    ///  Suspended
    /// </summary>
    Suspended = (byte)'S',

    /// <summary>
    ///  Deleted
    /// </summary>
    Deleted = (byte)'D',

}

/// <summary>
///  Reads and writes the Market Participant State field in place.
/// </summary>
public struct MarketParticipantStateField
{
    /// <summary>
    ///  The size of the Market Participant State field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Indicates the market participant’s current registration status in the issue
    /// </summary>
    public MarketParticipantState Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Market Participant State field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly MarketParticipantState Decode()
        => (MarketParticipantState)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Market Participant State field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(MarketParticipantState value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
