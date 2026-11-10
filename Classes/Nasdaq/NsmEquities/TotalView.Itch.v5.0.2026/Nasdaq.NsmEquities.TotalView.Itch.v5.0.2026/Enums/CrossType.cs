namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

/// <summary>
///  The Nasdaq cross session for which the message is being generated
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum CrossType : byte
{
    /// <summary>
    ///  Nasdaq Opening Cross
    /// </summary>
    Opening = (byte)'O',

    /// <summary>
    ///  Nasdaq Closing Cross
    /// </summary>
    Closing = (byte)'C',

    /// <summary>
    ///  Cross for IPO and halted / paused securities
    /// </summary>
    HaltedOrPaused = (byte)'H',

    /// <summary>
    ///  Extended Trading Close
    /// </summary>
    ExtendedClose = (byte)'A',
}
