namespace Iex.IexEquities.Deep.IexTp.v1_08.Manager;

/// <summary>
///  Passes each message, header, and packet of a reading to the method of the same name.
/// </summary>
public sealed partial class MessageHandler
{
    /// <summary>
    ///  Visits the packet and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitPacket(global::Iex.IexEquities.Deep.IexTp.v1_08.Packet.Reader packet)
    {
        this.OnIextpHeader(in packet.IextpHeader);
        var messages = packet.Messages;
        switch (messages.Case)
        {
            case global::Iex.IexEquities.Deep.IexTp.v1_08.Messages.Case.Message:
            {
                var messageEntries = messages.Message;
                while (messageEntries.MoveNext())
                {
                    var message = messageEntries.Current;
                    var messageData = message.MessageData;
                    switch (messageData.Case)
                    {
                        case global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.SystemEventMessage:
                        {
                            this.OnSystemEventMessage(in messageData.SystemEventMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.SecurityDirectoryMessage:
                        {
                            this.OnSecurityDirectoryMessage(in messageData.SecurityDirectoryMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.TradingStatusMessage:
                        {
                            this.OnTradingStatusMessage(in messageData.TradingStatusMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.RetailLiquidityIndicatorMessage:
                        {
                            this.OnRetailLiquidityIndicatorMessage(in messageData.RetailLiquidityIndicatorMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.OperationalHaltStatusMessage:
                        {
                            this.OnOperationalHaltStatusMessage(in messageData.OperationalHaltStatusMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.ShortSalePriceTestStatusMessage:
                        {
                            this.OnShortSalePriceTestStatusMessage(in messageData.ShortSalePriceTestStatusMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.SecurityEventMessage:
                        {
                            this.OnSecurityEventMessage(in messageData.SecurityEventMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.PriceLevelBuyUpdateMessage:
                        {
                            this.OnPriceLevelBuyUpdateMessage(in messageData.PriceLevelBuyUpdateMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.PriceLevelSellUpdateMessage:
                        {
                            this.OnPriceLevelSellUpdateMessage(in messageData.PriceLevelSellUpdateMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.TradeReportMessage:
                        {
                            this.OnTradeReportMessage(in messageData.TradeReportMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.OfficialPriceMessage:
                        {
                            this.OnOfficialPriceMessage(in messageData.OfficialPriceMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.TradeBreakMessage:
                        {
                            this.OnTradeBreakMessage(in messageData.TradeBreakMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.AuctionInformationMessage:
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
    partial void OnIextpHeader(in global::Iex.IexEquities.Deep.IexTp.v1_08.IextpHeader.Layout value);

    /// <summary>
    ///  Called with each System Event Message of a reading.
    /// </summary>
    partial void OnSystemEventMessage(in global::Iex.IexEquities.Deep.IexTp.v1_08.SystemEventMessage.Layout message, in global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Security Directory Message of a reading.
    /// </summary>
    partial void OnSecurityDirectoryMessage(in global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityDirectoryMessage.Layout message, in global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Trading Status Message of a reading.
    /// </summary>
    partial void OnTradingStatusMessage(in global::Iex.IexEquities.Deep.IexTp.v1_08.TradingStatusMessage.Layout message, in global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Retail Liquidity Indicator Message of a reading.
    /// </summary>
    partial void OnRetailLiquidityIndicatorMessage(in global::Iex.IexEquities.Deep.IexTp.v1_08.RetailLiquidityIndicatorMessage.Layout message, in global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Operational Halt Status Message of a reading.
    /// </summary>
    partial void OnOperationalHaltStatusMessage(in global::Iex.IexEquities.Deep.IexTp.v1_08.OperationalHaltStatusMessage.Layout message, in global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Short Sale Price Test Status Message of a reading.
    /// </summary>
    partial void OnShortSalePriceTestStatusMessage(in global::Iex.IexEquities.Deep.IexTp.v1_08.ShortSalePriceTestStatusMessage.Layout message, in global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Security Event Message of a reading.
    /// </summary>
    partial void OnSecurityEventMessage(in global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityEventMessage.Layout message, in global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Price Level Buy Update Message of a reading.
    /// </summary>
    partial void OnPriceLevelBuyUpdateMessage(in global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelBuyUpdateMessage.Layout message, in global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Price Level Sell Update Message of a reading.
    /// </summary>
    partial void OnPriceLevelSellUpdateMessage(in global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelSellUpdateMessage.Layout message, in global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Trade Report Message of a reading.
    /// </summary>
    partial void OnTradeReportMessage(in global::Iex.IexEquities.Deep.IexTp.v1_08.TradeReportMessage.Layout message, in global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Official Price Message of a reading.
    /// </summary>
    partial void OnOfficialPriceMessage(in global::Iex.IexEquities.Deep.IexTp.v1_08.OfficialPriceMessage.Layout message, in global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Trade Break Message of a reading.
    /// </summary>
    partial void OnTradeBreakMessage(in global::Iex.IexEquities.Deep.IexTp.v1_08.TradeBreakMessage.Layout message, in global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Auction Information Message of a reading.
    /// </summary>
    partial void OnAuctionInformationMessage(in global::Iex.IexEquities.Deep.IexTp.v1_08.AuctionInformationMessage.Layout message, in global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Packet of a reading.
    /// </summary>
    partial void OnPacket(global::Iex.IexEquities.Deep.IexTp.v1_08.Packet.Reader value);
}
