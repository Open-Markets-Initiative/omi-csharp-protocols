namespace Nasdaq.NtxEquities.TotalView.Itch.v5_0;

/// <summary>
///  Denotes the operational halt action
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
    Resumed = (byte)'T',
}
