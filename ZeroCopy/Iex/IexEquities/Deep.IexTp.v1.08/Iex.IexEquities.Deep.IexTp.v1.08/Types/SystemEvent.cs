using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  System event identifier
/// </summary>

public struct SystemEvent
{
    /// <summary>
    ///  Outside Of Heartbeat Messages On The Lower Level Protocol The Start Of Day Message Is The First Message Sent In Any Trading Session
    /// </summary>
    public const char StartOfMessages = 'O';

    /// <summary>
    ///  This Message Indicates That Iex Is Open And Ready To Start Accepting Orders
    /// </summary>
    public const char StartOfSystemHours = 'S';

    /// <summary>
    ///  This Message Indicates That Day And Gtx Orders As Well As Market Orders And Pegged Orders Are Available For Execution On Iex
    /// </summary>
    public const char StartOfRegularMarketHours = 'R';

    /// <summary>
    ///  This Message Indicates That Day Orders Market Orders And Pegged Orders Are No Longer Accepted By Iex
    /// </summary>
    public const char EndOfRegularMarketHours = 'M';

    /// <summary>
    ///  This Message Indicates That Iex Is Now Closed And Will Not Accept Any New Orders During This Trading Session
    /// </summary>
    public const char EndOfSystemHours = 'E';

    /// <summary>
    ///  This Is Always The Last Message Sent In Any Trading Session
    /// </summary>
    public const char EndOfMessages = 'C';

    /// <summary>
    ///  Size of SystemEvent in bytes
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Gets the decoded System Event value.
    /// </summary>
    public readonly char Value
        => (char)Byte;

    /// <summary>
    ///  Decodes the raw bytes into the System Event value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly char Decode()
        => (char)Byte;

    /// <summary>
    ///  Encodes a value into the underlying System Event bytes.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(char value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the string representation of the System Event value.
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying byte
    /// </summary>
    internal byte Byte;
}
