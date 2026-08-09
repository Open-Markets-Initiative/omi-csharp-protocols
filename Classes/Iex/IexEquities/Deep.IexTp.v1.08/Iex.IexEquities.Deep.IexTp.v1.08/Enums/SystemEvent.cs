namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  System event identifier
/// </summary>
/// <remarks>
///  Each value corresponds to the ASCII byte value used as the wire-format discriminator.
/// </remarks>
public enum SystemEvent : byte
{
    /// <summary>
    ///  Outside Of Heartbeat Messages On The Lower Level Protocol The Start Of Day Message Is The First Message Sent In Any Trading Session
    /// </summary>
    StartOfMessages = (byte)'O',

    /// <summary>
    ///  This Message Indicates That Iex Is Open And Ready To Start Accepting Orders
    /// </summary>
    StartOfSystemHours = (byte)'S',

    /// <summary>
    ///  This Message Indicates That Day And Gtx Orders As Well As Market Orders And Pegged Orders Are Available For Execution On Iex
    /// </summary>
    StartOfRegularMarketHours = (byte)'R',

    /// <summary>
    ///  This Message Indicates That Day Orders Market Orders And Pegged Orders Are No Longer Accepted By Iex
    /// </summary>
    EndOfRegularMarketHours = (byte)'M',

    /// <summary>
    ///  This Message Indicates That Iex Is Now Closed And Will Not Accept Any New Orders During This Trading Session
    /// </summary>
    EndOfSystemHours = (byte)'E',

    /// <summary>
    ///  This Is Always The Last Message Sent In Any Trading Session
    /// </summary>
    EndOfMessages = (byte)'C',
}
