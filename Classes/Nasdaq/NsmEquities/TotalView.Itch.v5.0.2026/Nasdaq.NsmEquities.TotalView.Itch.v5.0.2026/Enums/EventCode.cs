namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

/// <summary>
///  System Event Codes
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum EventCode : byte
{
    /// <summary>
    ///  Outside of time stamp messages, the start of day message is the first message sent in any trading day
    /// </summary>
    StartOfMessages = (byte)'O',

    /// <summary>
    ///  This message indicates that Nasdaq is open and ready to start accepting orders
    /// </summary>
    StartOfSystemHours = (byte)'S',

    /// <summary>
    ///  This message is intended to indicate that Market Hours orders are available for execution
    /// </summary>
    StartOfMarketHours = (byte)'Q',

    /// <summary>
    ///  This message is intended to indicate that Market Hours orders are no longer available for execution
    /// </summary>
    EndOfMarketHours = (byte)'M',

    /// <summary>
    ///  Nasdaq is now closed and will not accept any new orders today
    /// </summary>
    EndOfSystemHours = (byte)'E',

    /// <summary>
    ///  Always the last message sent in any trading day
    /// </summary>
    EndOfMessages = (byte)'C',
}
