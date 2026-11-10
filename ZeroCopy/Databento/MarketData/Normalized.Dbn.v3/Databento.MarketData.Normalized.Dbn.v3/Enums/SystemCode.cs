using System.Runtime.CompilerServices;

namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  Type of system message
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum SystemCode : byte
{
    /// <summary>
    ///  Heartbeat
    /// </summary>
    Heartbeat = 0,

    /// <summary>
    ///  Subscription Ack
    /// </summary>
    SubscriptionAck = 1,

    /// <summary>
    ///  Slow Reader Warning
    /// </summary>
    SlowReaderWarning = 2,

    /// <summary>
    ///  Replay Completed
    /// </summary>
    ReplayCompleted = 3,

    /// <summary>
    ///  End Of Interval
    /// </summary>
    EndOfInterval = 4,

    /// <summary>
    ///  Unsubscribe Ack
    /// </summary>
    UnsubscribeAck = 5,

    /// <summary>
    ///  Unset
    /// </summary>
    Unset = 255,

}

/// <summary>
///  Reads and writes the System Code field in place.
/// </summary>
public struct SystemCodeField
{
    /// <summary>
    ///  The size of the System Code field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Type of system message
    /// </summary>
    public SystemCode Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the System Code field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly SystemCode Decode()
        => (SystemCode)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the System Code field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(SystemCode value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
