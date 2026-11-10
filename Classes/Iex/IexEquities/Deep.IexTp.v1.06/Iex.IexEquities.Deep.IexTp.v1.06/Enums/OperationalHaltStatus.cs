namespace Iex.IexEquities.Deep.IexTp.v1_06;

/// <summary>
///  Operational halt status identifier
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum OperationalHaltStatus : byte
{
    /// <summary>
    ///  Iex Specific Operational Trading Halt
    /// </summary>
    IexSpecificOperationalTradingHalt = (byte)'O',

    /// <summary>
    ///  Not Operationally Halted On Iex
    /// </summary>
    NotOperationallyHaltedOnIex = (byte)'N',
}
