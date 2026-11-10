namespace Iex.IexEquities.DeepPlus.IexTp.v1_05.Manager;

/// <summary>
///  Passes each message, header, and packet of a reading to the method of the same name.
/// </summary>
public sealed partial class MessageHandler
{
    /// <summary>
    ///  Visits the packet and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitPacket(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Packet.Reader packet)
    {
        this.OnIextpHeader(in packet.IextpHeader);
        var messages = packet.Messages;
        switch (messages.Case)
        {
            case global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Messages.Case.Message:
            {
                var messageEntries = messages.Message;
                while (messageEntries.MoveNext())
                {
                    var message = messageEntries.Current;
                    var messageData = message.MessageData;
                    switch (messageData.Case)
                    {
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageData.Case.SystemEventMessage:
                        {
                            this.OnSystemEventMessage(in messageData.SystemEventMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageData.Case.SecurityDirectoryMessage:
                        {
                            this.OnSecurityDirectoryMessage(in messageData.SecurityDirectoryMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageData.Case.TradingStatusMessage:
                        {
                            this.OnTradingStatusMessage(in messageData.TradingStatusMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageData.Case.RetailLiquidityIndicatorMessage:
                        {
                            this.OnRetailLiquidityIndicatorMessage(in messageData.RetailLiquidityIndicatorMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageData.Case.OperationalHaltStatusMessage:
                        {
                            this.OnOperationalHaltStatusMessage(in messageData.OperationalHaltStatusMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageData.Case.ShortSalePriceTestStatusMessage:
                        {
                            this.OnShortSalePriceTestStatusMessage(in messageData.ShortSalePriceTestStatusMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageData.Case.SecurityEventMessage:
                        {
                            this.OnSecurityEventMessage(in messageData.SecurityEventMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageData.Case.AddOrderMessage:
                        {
                            this.OnAddOrderMessage(in messageData.AddOrderMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageData.Case.OrderModifyMessage:
                        {
                            this.OnOrderModifyMessage(in messageData.OrderModifyMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageData.Case.OrderDeleteMessage:
                        {
                            this.OnOrderDeleteMessage(in messageData.OrderDeleteMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageData.Case.OrderExecutedMessage:
                        {
                            this.OnOrderExecutedMessage(in messageData.OrderExecutedMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageData.Case.TradeMessage:
                        {
                            this.OnTradeMessage(in messageData.TradeMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageData.Case.TradeBreakMessage:
                        {
                            this.OnTradeBreakMessage(in messageData.TradeBreakMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageData.Case.ClearBookMessage:
                        {
                            this.OnClearBookMessage(in messageData.ClearBookMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                    }
                }
                break;
            }
        }
        this.OnPacket(packet);
    }

    /// <summary>
    ///  Called with each Iextp Header of a reading.
    /// </summary>
    partial void OnIextpHeader(in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.IextpHeader.Layout value);

    /// <summary>
    ///  Called with each System Event Message of a reading.
    /// </summary>
    partial void OnSystemEventMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SystemEventMessage.Layout message, in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Security Directory Message of a reading.
    /// </summary>
    partial void OnSecurityDirectoryMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityDirectoryMessage.Layout message, in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Trading Status Message of a reading.
    /// </summary>
    partial void OnTradingStatusMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradingStatusMessage.Layout message, in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Retail Liquidity Indicator Message of a reading.
    /// </summary>
    partial void OnRetailLiquidityIndicatorMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.RetailLiquidityIndicatorMessage.Layout message, in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Operational Halt Status Message of a reading.
    /// </summary>
    partial void OnOperationalHaltStatusMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OperationalHaltStatusMessage.Layout message, in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Short Sale Price Test Status Message of a reading.
    /// </summary>
    partial void OnShortSalePriceTestStatusMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ShortSalePriceTestStatusMessage.Layout message, in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Security Event Message of a reading.
    /// </summary>
    partial void OnSecurityEventMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityEventMessage.Layout message, in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Add Order Message of a reading.
    /// </summary>
    partial void OnAddOrderMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.AddOrderMessage.Layout message, in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Order Modify Message of a reading.
    /// </summary>
    partial void OnOrderModifyMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderModifyMessage.Layout message, in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Order Delete Message of a reading.
    /// </summary>
    partial void OnOrderDeleteMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderDeleteMessage.Layout message, in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Order Executed Message of a reading.
    /// </summary>
    partial void OnOrderExecutedMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderExecutedMessage.Layout message, in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Trade Message of a reading.
    /// </summary>
    partial void OnTradeMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeMessage.Layout message, in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Trade Break Message of a reading.
    /// </summary>
    partial void OnTradeBreakMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeBreakMessage.Layout message, in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Clear Book Message of a reading.
    /// </summary>
    partial void OnClearBookMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ClearBookMessage.Layout message, in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Packet of a reading.
    /// </summary>
    partial void OnPacket(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Packet.Reader value);
}
