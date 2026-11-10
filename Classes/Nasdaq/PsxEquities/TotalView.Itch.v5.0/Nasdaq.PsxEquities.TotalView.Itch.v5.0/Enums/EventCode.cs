namespace Nasdaq.PsxEquities.TotalView.Itch.v5_0;

/// <summary>
///  System Event Codes
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum EventCode : byte
{
    /// <summary>
    ///  Outside of time stamp messages, the start of day message is the first message sent in any trading day.
    /// </summary>
    StartOfMessages = (byte)'O',

    /// <summary>
    ///  This message indicates that NASDAQ is open and ready to start accepting orders.
    /// </summary>
    StartOfSystemHours = (byte)'S',

    /// <summary>
    ///  This message is intended to indicate that Market Hours orders are available for execution.
    /// </summary>
    StartOfMarketHours = (byte)'Q',

    /// <summary>
    ///  This message is intended to indicate that Market Hours orders are no longer available for execution.
    /// </summary>
    EndOfMarketHours = (byte)'M',

    /// <summary>
    ///  It indicates that NASDAQ is now closed and will not accept any new orders today. It is still possible to receive Broken Trade messages and Order Delete messages after the End of Day.
    /// </summary>
    EndOfSystemHours = (byte)'E',

    /// <summary>
    ///  This is always the last message sent in any trading day.
    /// </summary>
    EndOfMessages = (byte)'C',
}
