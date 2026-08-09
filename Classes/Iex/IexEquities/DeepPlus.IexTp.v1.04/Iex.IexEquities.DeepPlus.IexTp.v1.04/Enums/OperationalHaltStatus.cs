namespace Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
///  Operational halt status identifier
/// </summary>
/// <remarks>
///  Each value corresponds to the ASCII byte value used as the wire-format discriminator.
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
