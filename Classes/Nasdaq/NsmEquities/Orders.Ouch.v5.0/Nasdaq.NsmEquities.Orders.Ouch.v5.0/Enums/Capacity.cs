namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Capacity
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum Capacity : byte
{
    /// <summary>
    ///  Other
    /// </summary>
    Other = (byte)'O',

    /// <summary>
    ///  agency
    /// </summary>
    Agency = (byte)'A',

    /// <summary>
    ///  principal
    /// </summary>
    Principal = (byte)'P',

    /// <summary>
    ///  riskless
    /// </summary>
    Riskless = (byte)'R',
}
