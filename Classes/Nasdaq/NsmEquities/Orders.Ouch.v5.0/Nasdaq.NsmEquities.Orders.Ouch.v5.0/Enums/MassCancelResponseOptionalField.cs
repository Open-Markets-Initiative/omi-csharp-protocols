namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Appendage Id
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum MassCancelResponseOptionalField : byte
{
    /// <summary>
    ///  Mass Cancel Response Optional Side Enum
    /// </summary>
    Side = 27,

    /// <summary>
    ///  Mass Cancel Response Optional Group ID Enum
    /// </summary>
    GroupId = 24,

    /// <summary>
    ///  Mass Cancel Response Optional UserRefIdx Enum
    /// </summary>
    UserRefIdx = 28,
}
