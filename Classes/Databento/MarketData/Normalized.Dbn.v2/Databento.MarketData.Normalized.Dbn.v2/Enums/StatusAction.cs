namespace Databento.MarketData.Normalized.Dbn.v2;

/// <summary>
///  The type of status change
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum StatusAction : ushort
{
    /// <summary>
    ///  None
    /// </summary>
    None = 0,

    /// <summary>
    ///  Pre Open
    /// </summary>
    PreOpen = 1,

    /// <summary>
    ///  Pre Cross
    /// </summary>
    PreCross = 2,

    /// <summary>
    ///  Quoting
    /// </summary>
    Quoting = 3,

    /// <summary>
    ///  Cross
    /// </summary>
    Cross = 4,

    /// <summary>
    ///  Rotation
    /// </summary>
    Rotation = 5,

    /// <summary>
    ///  New Price Indication
    /// </summary>
    NewPriceIndication = 6,

    /// <summary>
    ///  Trading
    /// </summary>
    Trading = 7,

    /// <summary>
    ///  Halt
    /// </summary>
    Halt = 8,

    /// <summary>
    ///  Pause
    /// </summary>
    Pause = 9,

    /// <summary>
    ///  Suspend
    /// </summary>
    Suspend = 10,

    /// <summary>
    ///  Pre Close
    /// </summary>
    PreClose = 11,

    /// <summary>
    ///  Close
    /// </summary>
    Close = 12,

    /// <summary>
    ///  Post Close
    /// </summary>
    PostClose = 13,

    /// <summary>
    ///  Ssr Change
    /// </summary>
    SsrChange = 14,

    /// <summary>
    ///  Not Available For Trading
    /// </summary>
    NotAvailableForTrading = 15,
}
