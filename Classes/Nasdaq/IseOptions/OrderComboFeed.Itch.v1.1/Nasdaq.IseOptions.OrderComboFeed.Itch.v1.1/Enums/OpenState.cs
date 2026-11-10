namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

/// <summary>
///  Reflects the current eligibility for auto execution of the options security in the options market.
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum OpenState : byte
{
    /// <summary>
    ///  Open for auto execution
    /// </summary>
    OpenForAutoExecution = (byte)'Y',

    /// <summary>
    ///  Closed for auto execution
    /// </summary>
    ClosedForAutoExecution = (byte)'N',
}
