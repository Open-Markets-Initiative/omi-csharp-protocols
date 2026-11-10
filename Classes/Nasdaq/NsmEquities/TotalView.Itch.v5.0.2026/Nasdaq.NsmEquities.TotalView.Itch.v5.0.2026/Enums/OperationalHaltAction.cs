namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

/// <summary>
///  Indicates the operational halt action for the security
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum OperationalHaltAction : byte
{
    /// <summary>
    ///  Operationally Halted on the identified Market
    /// </summary>
    Halted = (byte)'H',

    /// <summary>
    ///  Operational Halt has been lifted and Trading resumed
    /// </summary>
    TradingResumed = (byte)'T',
}
