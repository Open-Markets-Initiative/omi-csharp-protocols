namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Identifies the type of a message contained in Sequenced Message by its message type.
/// </summary>
public enum SequencedMessageCode : byte
{
    /// <summary>
    ///  System Event Messages signal events that affect the entire NASDAQ system.
    /// </summary>
    SystemEventMessage = (byte)'S',

    /// <summary>
    ///  This message acknowledges the receipt and acceptance of a valid Enter Order Message. The data fields from the Enter Order Message are echoed back in this message. Note that the accepted values may differ from the entered values for some fields.
    /// </summary>
    OrderAcceptedMessage = (byte)'A',

    /// <summary>
    ///  This message acknowledges the receipt and acceptance of a valid Replace Order Message. The data fields from the Replace Order Message are echoed back in this message. Note that the accepted values may differ from the entered values for some fields. You will receive one and only one of these two for each replacement.
    /// </summary>
    ReplacedMessage = (byte)'U',

    /// <summary>
    ///  A Canceled Message informs you that an order has been reduced or canceled. This could be acknowledging a Cancel Order Message, or it could be the result of the order timing out or being canceled automatically.
    /// </summary>
    CanceledMessage = (byte)'C',

    /// <summary>
    ///  AIQ Canceled Message
    /// </summary>
    AiqCanceledMessage = (byte)'D',

    /// <summary>
    ///  An Executed Order Message informs you that all or part of an order has been executed.
    /// </summary>
    OrderExecutedMessage = (byte)'E',

    /// <summary>
    ///  A Broken Trade Message informs you that an execution has been broken. The trade is no longer good and will not clear. The reason for the break is given.
    /// </summary>
    BrokenTradeMessage = (byte)'B',

    /// <summary>
    ///  A Rejected Message may be sent in response to an Enter Order Message or Replace Order Message if the order or replace cannot be accepted at this time. The reason for the rejection is given.
    /// </summary>
    RejectedOrderMessage = (byte)'J',

    /// <summary>
    ///  A Cancel Pending Message is sent in response to a cancel request for a cross order during a pre-cross late period signifying that it cannot be canceled at this time, but any unexecuted portion of this order will automatically be canceled immediately after the cross completes.
    /// </summary>
    CancelPendingMessage = (byte)'P',

    /// <summary>
    ///  A Cancel Reject Message is sent in response to a partial cancel request (with non-zero “intended order size”) for a cross order during a pre-cross late period signifying that it cannot be partially canceled at this time. No automatic cancel will be scheduled for this order. Clients could repeat their request for any unexecuted portion of the order after the cross completes.
    /// </summary>
    CancelRejectMessage = (byte)'I',

    /// <summary>
    ///  A Priority Update Message is sent whenever priority of the order has been changed by the system.
    /// </summary>
    OrderPriorityUpdateMessage = (byte)'T',

    /// <summary>
    ///  An Order Modified Message is sent in response to an order modify request.
    /// </summary>
    OrderModifiedMessage = (byte)'M',

    /// <summary>
    ///  The Order Restated Message is sent to indicate that the system has modified an order as part of its order management.
    /// </summary>
    OrderRestatedMessage = (byte)'R',

    /// <summary>
    ///  The Account Query Response message in sent in response to an Account Query Request to indicate the next available UserRefNum that can be used to identify new transactions.
    /// </summary>
    AccountQueryResponseMessage = (byte)'Q',

    /// <summary>
    ///  This message acknowledges the receipt of a valid Mass Cancel Request message. The data fields from the Mass Cancel Request message are echoed back in this message.
    /// </summary>
    MassCancelResponseMessage = (byte)'X',

    /// <summary>
    ///  This message acknowledges the receipt of the Disable Order Entry Request message. The data fields from the Disable Order Entry Request message are echoed back in this message.
    /// </summary>
    DisableOrderEntryResponseMessage = (byte)'G',

    /// <summary>
    ///  This message acknowledges the receipt of the Enable Order Entry Request message. The data fields from the Enable Order Entry Request message are echoed back in this message.
    /// </summary>
    EnableOrderEntryResponseMessage = (byte)'K',
}
