namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

/// <summary>
///  Refer to System Event Codes below
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum EventCode : byte
{
    /// <summary>
    ///  Start of Messages
    /// </summary>
    StartOfMessages = (byte)'O',

    /// <summary>
    ///  Start of System Hours
    /// </summary>
    StartOfSystemHours = (byte)'S',

    /// <summary>
    ///  Start of Opening Process
    /// </summary>
    StartOfOpeningProcess = (byte)'Q',

    /// <summary>
    ///  Start of Normal Hours Closing Process
    /// </summary>
    StartOfNormalHoursClosingProcess = (byte)'N',

    /// <summary>
    ///  Start of Late Hours Closing Process
    /// </summary>
    StartOfLateHoursClosingProcess = (byte)'L',

    /// <summary>
    ///  End of System Hours
    /// </summary>
    EndOfSystemHours = (byte)'E',

    /// <summary>
    ///  End of Messages
    /// </summary>
    EndOfMessages = (byte)'C',

    /// <summary>
    ///  End of WCO Early closing
    /// </summary>
    EndOfWcoEarlyClosing = (byte)'W',
}
