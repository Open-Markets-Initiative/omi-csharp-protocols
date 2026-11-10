namespace Iex.IexEquities.Tops.IexTp.v1_64.Manager;

/// <summary>
///  Passes each message, header, and packet of a reading to the method of the same name.
/// </summary>
public sealed partial class MessageHandler
{
    /// <summary>
    ///  Visits the packet and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitPacket(global::Iex.IexEquities.Tops.IexTp.v1_64.Packet.Reader packet)
    {
        this.OnIextpHeader(in packet.IextpHeader);
        var messages = packet.Messages;
        switch (messages.Case)
        {
            case global::Iex.IexEquities.Tops.IexTp.v1_64.Messages.Case.Message:
            {
                var messageEntries = messages.Message;
                while (messageEntries.MoveNext())
                {
                    var message = messageEntries.Current;
                    var messageData = message.MessageData;
                    switch (messageData.Case)
                    {
                        case global::Iex.IexEquities.Tops.IexTp.v1_64.MessageData.Case.SystemEventMessage:
                        {
                            this.OnSystemEventMessage(in messageData.SystemEventMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Tops.IexTp.v1_64.MessageData.Case.SecurityDirectoryMessage:
                        {
                            this.OnSecurityDirectoryMessage(in messageData.SecurityDirectoryMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Tops.IexTp.v1_64.MessageData.Case.TradingStatusMessage:
                        {
                            this.OnTradingStatusMessage(in messageData.TradingStatusMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Tops.IexTp.v1_64.MessageData.Case.OperationalHaltStatusMessage:
                        {
                            this.OnOperationalHaltStatusMessage(in messageData.OperationalHaltStatusMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Tops.IexTp.v1_64.MessageData.Case.ShortSalePriceTestStatusMessage:
                        {
                            this.OnShortSalePriceTestStatusMessage(in messageData.ShortSalePriceTestStatusMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Tops.IexTp.v1_64.MessageData.Case.SecurityEventMessage:
                        {
                            this.OnSecurityEventMessage(in messageData.SecurityEventMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Tops.IexTp.v1_64.MessageData.Case.QuoteUpdateMessage:
                        {
                            this.OnQuoteUpdateMessage(in messageData.QuoteUpdateMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Tops.IexTp.v1_64.MessageData.Case.TradeReportMessage:
                        {
                            this.OnTradeReportMessage(in messageData.TradeReportMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Tops.IexTp.v1_64.MessageData.Case.OfficialPriceMessage:
                        {
                            this.OnOfficialPriceMessage(in messageData.OfficialPriceMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Tops.IexTp.v1_64.MessageData.Case.TradeBreakMessage:
                        {
                            this.OnTradeBreakMessage(in messageData.TradeBreakMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Tops.IexTp.v1_64.MessageData.Case.AuctionInformationMessage:
                        {
                            this.OnAuctionInformationMessage(in messageData.AuctionInformationMessage, in message.MessageHeader, message.SequenceNumber);
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
    partial void OnIextpHeader(in global::Iex.IexEquities.Tops.IexTp.v1_64.IextpHeader.Layout value);

    /// <summary>
    ///  Called with each System Event Message of a reading.
    /// </summary>
    partial void OnSystemEventMessage(in global::Iex.IexEquities.Tops.IexTp.v1_64.SystemEventMessage.Layout message, in global::Iex.IexEquities.Tops.IexTp.v1_64.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Security Directory Message of a reading.
    /// </summary>
    partial void OnSecurityDirectoryMessage(in global::Iex.IexEquities.Tops.IexTp.v1_64.SecurityDirectoryMessage.Layout message, in global::Iex.IexEquities.Tops.IexTp.v1_64.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Trading Status Message of a reading.
    /// </summary>
    partial void OnTradingStatusMessage(in global::Iex.IexEquities.Tops.IexTp.v1_64.TradingStatusMessage.Layout message, in global::Iex.IexEquities.Tops.IexTp.v1_64.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Operational Halt Status Message of a reading.
    /// </summary>
    partial void OnOperationalHaltStatusMessage(in global::Iex.IexEquities.Tops.IexTp.v1_64.OperationalHaltStatusMessage.Layout message, in global::Iex.IexEquities.Tops.IexTp.v1_64.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Short Sale Price Test Status Message of a reading.
    /// </summary>
    partial void OnShortSalePriceTestStatusMessage(in global::Iex.IexEquities.Tops.IexTp.v1_64.ShortSalePriceTestStatusMessage.Layout message, in global::Iex.IexEquities.Tops.IexTp.v1_64.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Security Event Message of a reading.
    /// </summary>
    partial void OnSecurityEventMessage(in global::Iex.IexEquities.Tops.IexTp.v1_64.SecurityEventMessage.Layout message, in global::Iex.IexEquities.Tops.IexTp.v1_64.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Quote Update Message of a reading.
    /// </summary>
    partial void OnQuoteUpdateMessage(in global::Iex.IexEquities.Tops.IexTp.v1_64.QuoteUpdateMessage.Layout message, in global::Iex.IexEquities.Tops.IexTp.v1_64.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Trade Report Message of a reading.
    /// </summary>
    partial void OnTradeReportMessage(in global::Iex.IexEquities.Tops.IexTp.v1_64.TradeReportMessage.Layout message, in global::Iex.IexEquities.Tops.IexTp.v1_64.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Official Price Message of a reading.
    /// </summary>
    partial void OnOfficialPriceMessage(in global::Iex.IexEquities.Tops.IexTp.v1_64.OfficialPriceMessage.Layout message, in global::Iex.IexEquities.Tops.IexTp.v1_64.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Trade Break Message of a reading.
    /// </summary>
    partial void OnTradeBreakMessage(in global::Iex.IexEquities.Tops.IexTp.v1_64.TradeBreakMessage.Layout message, in global::Iex.IexEquities.Tops.IexTp.v1_64.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Auction Information Message of a reading.
    /// </summary>
    partial void OnAuctionInformationMessage(in global::Iex.IexEquities.Tops.IexTp.v1_64.AuctionInformationMessage.Layout message, in global::Iex.IexEquities.Tops.IexTp.v1_64.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Packet of a reading.
    /// </summary>
    partial void OnPacket(global::Iex.IexEquities.Tops.IexTp.v1_64.Packet.Reader value);
}
