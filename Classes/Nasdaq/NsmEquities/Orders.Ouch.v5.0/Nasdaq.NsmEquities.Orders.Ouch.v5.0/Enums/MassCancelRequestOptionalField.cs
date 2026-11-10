namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Appendage Id
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum MassCancelRequestOptionalField : byte
{
    /// <summary>
    ///  Mass Cancel Request Optional Side Enum
    /// </summary>
    Side = 27,

    /// <summary>
    ///  Mass Cancel Request Optional Group ID Enum
    /// </summary>
    GroupId = 24,

    /// <summary>
    ///  Mass Cancel Request Optional UserRefIdx Enum
    /// </summary>
    UserRefIdx = 28,
}
