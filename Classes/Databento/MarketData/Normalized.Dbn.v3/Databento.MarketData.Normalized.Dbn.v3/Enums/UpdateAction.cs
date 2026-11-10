namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  Indicates if the statistic is newly added or deleted
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum UpdateAction : byte
{
    /// <summary>
    ///  New
    /// </summary>
    New = 1,

    /// <summary>
    ///  Delete
    /// </summary>
    Delete = 2,
}
