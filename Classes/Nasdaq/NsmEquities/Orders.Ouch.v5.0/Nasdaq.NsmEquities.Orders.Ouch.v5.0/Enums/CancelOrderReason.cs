namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Reason the order was reduced or canceled.
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum CancelOrderReason : byte
{
    /// <summary>
    ///  Sent in response to a Cancel Request Message.
    /// </summary>
    UserRequestedCancel = (byte)'U',

    /// <summary>
    ///  Immediate or Cancel Order.
    /// </summary>
    ImmediateOrCancelOrder = (byte)'I',

    /// <summary>
    ///  The Time In Force for this order has expired
    /// </summary>
    Timeout = (byte)'T',

    /// <summary>
    ///  The order was manually canceled or reduced by an NASDAQ supervisory terminal.
    /// </summary>
    Supervisory = (byte)'S',

    /// <summary>
    ///  This order cannot be executed because of a regulatory restriction (e.g.: trade through restrictions).
    /// </summary>
    RegulatoryRestriction = (byte)'D',

    /// <summary>
    ///  The order was cancelled because it would have executed with an existing order entered by the same MPID.
    /// </summary>
    SelfMatchPrevention = (byte)'Q',

    /// <summary>
    ///  This order was cancelled by the system.
    /// </summary>
    SystemCancel = (byte)'Z',

    /// <summary>
    ///  This order cannot be executed because of Market Collars
    /// </summary>
    ThisOrderCannotBeExecuted = (byte)'K',

    /// <summary>
    ///  The on-open order was canceled because the symbol remained halted after the opening cross completed.
    /// </summary>
    Halted = (byte)'H',

    /// <summary>
    ///  Orders that are cancelled as a result of the Opening Price Protection Threshold.
    /// </summary>
    OpenProtection = (byte)'X',

    /// <summary>
    ///  Any DAY order that was received after the closing cross is complete in a given symbol will receive this cancel reason.
    /// </summary>
    Closed = (byte)'E',

    /// <summary>
    ///  This Post Only order was cancelled because it would have been price slid for NMS.
    /// </summary>
    PostOnlyCancel = (byte)'F',

    /// <summary>
    ///  This Post Only order was cancelled because it would have been price slid due to a contra side displayed order on the book.
    /// </summary>
    PostOnlyCancelContraSideDisplayed = (byte)'G',

    /// <summary>
    ///  Company Direct Listing Capital Raise order exceeds allowable shares offered
    /// </summary>
    CompanyDirectListingCapitalRaiseOrderExceedsAllowableSharesOffered = (byte)'e',
}
