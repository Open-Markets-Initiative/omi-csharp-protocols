namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Identifies the type of a message contained in Unsequenced Message by its message type.
/// </summary>
public enum UnsequencedMessageCode : byte
{
    /// <summary>
    ///  Enter Order Message
    /// </summary>
    EnterOrderMessage = (byte)'O',

    /// <summary>
    ///  The Replace Order Message allows you to cancel and replace an existing order in a single message. This is more efficient than canceling an existing order and immediately succeeding it with a new order. Replacing an order always gives it a new timestamp for its time priority on the book. If you wish you simply partially cancel an order and retain its time priority, send a Cancel Order Message instead.
    /// </summary>
    ReplaceOrderMessage = (byte)'U',

    /// <summary>
    ///  The Cancel Order Message is used to request that an order be canceled or reduced. In the Cancel Order Message, you must specify the new "intended order size" for the order. The "intended order size" is the maximum number of shares that can be executed in total after the cancel is applied.
    /// </summary>
    CancelOrderMessage = (byte)'X',

    /// <summary>
    ///  The Modify Order Message is used to request modifications that will not affect order priority on the book. Since priority of the order does not change, allowed modifications are restricted. Increasing share amount is not allowed and requests to do so will be ignored.
    /// </summary>
    ModifyOrderMessage = (byte)'M',

    /// <summary>
    ///  The Account Query Request message can be used when recovering state to request the next available UserRefNum that can be used for identifying new transactions.
    /// </summary>
    AccountQueryMessage = (byte)'Q',

    /// <summary>
    ///  The Mass Cancel Request message allows firms to initiate the cancellation of one or more orders that are currently booked at the exchange if they match the criteria specified in the request. If no optional criteria are specified then all orders for the specified firm on the account will be cancelled. Optional criteria can be specified in the Appendage field.
    /// </summary>
    MassCancelRequestMessage = (byte)'C',

    /// <summary>
    ///  The Disable Order Entry Request allows firms the ability to block submissions of new orders on a particular account.
    /// </summary>
    DisableOrderEntryRequestMessage = (byte)'D',

    /// <summary>
    ///  The Enable Order Entry Request allows firms the ability to unblock submission of new orders on a particular account.
    /// </summary>
    EnableOrderEntryRequestMessage = (byte)'E',
}
