namespace Nasdaq.PsxEquities.TotalView.Itch.v5_0.Manager;

/// <summary>
///  Passes each message, header, and packet of a reading to the method of the same name.
/// </summary>
public sealed partial class MessageHandler
{
    /// <summary>
    ///  Visits the packet and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitPacket(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Packet.Reader packet)
    {
        this.OnPacketHeader(in packet.PacketHeader);
        var messages = packet.Messages;
        switch (messages.Case)
        {
            case global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Messages.Case.Heartbeat:
            {
                this.OnHeartbeat();
                break;
            }
            case global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Messages.Case.EndOfSession:
            {
                this.OnEndOfSession();
                break;
            }
            case global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Messages.Case.Message:
            {
                var messageEntries = messages.Message;
                while (messageEntries.MoveNext())
                {
                    var message = messageEntries.Current;
                    var payload = message.Payload;
                    switch (payload.Case)
                    {
                        case global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.SystemEventMessage:
                        {
                            this.OnSystemEventMessage(in payload.SystemEventMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.StockDirectoryMessage:
                        {
                            this.OnStockDirectoryMessage(in payload.StockDirectoryMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.StockTradingActionMessage:
                        {
                            this.OnStockTradingActionMessage(in payload.StockTradingActionMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.RegShoShortSalePriceTestRestrictedIndicatorMessage:
                        {
                            this.OnRegShoShortSalePriceTestRestrictedIndicatorMessage(in payload.RegShoShortSalePriceTestRestrictedIndicatorMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.MarketParticipantPositionMessage:
                        {
                            this.OnMarketParticipantPositionMessage(in payload.MarketParticipantPositionMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.MwcbDeclineLevelMessage:
                        {
                            this.OnMwcbDeclineLevelMessage(in payload.MwcbDeclineLevelMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.MwcbStatusLevelMessage:
                        {
                            this.OnMwcbStatusLevelMessage(in payload.MwcbStatusLevelMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.LuldAuctionCollarMessage:
                        {
                            this.OnLuldAuctionCollarMessage(in payload.LuldAuctionCollarMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.OperationalHaltMessage:
                        {
                            this.OnOperationalHaltMessage(in payload.OperationalHaltMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.AddOrderNoMpidAttributionMessage:
                        {
                            this.OnAddOrderNoMpidAttributionMessage(in payload.AddOrderNoMpidAttributionMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.AddOrderWithMpidAttributionMessage:
                        {
                            this.OnAddOrderWithMpidAttributionMessage(in payload.AddOrderWithMpidAttributionMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.OrderExecutedMessage:
                        {
                            this.OnOrderExecutedMessage(in payload.OrderExecutedMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.OrderExecutedWithPriceMessage:
                        {
                            this.OnOrderExecutedWithPriceMessage(in payload.OrderExecutedWithPriceMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.OrderCancelMessage:
                        {
                            this.OnOrderCancelMessage(in payload.OrderCancelMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.OrderDeleteMessage:
                        {
                            this.OnOrderDeleteMessage(in payload.OrderDeleteMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.OrderReplaceMessage:
                        {
                            this.OnOrderReplaceMessage(in payload.OrderReplaceMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.TradeMessageNonCross:
                        {
                            this.OnTradeMessageNonCross(in payload.TradeMessageNonCross, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.CrossTradeMessage:
                        {
                            this.OnCrossTradeMessage(in payload.CrossTradeMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.BrokenTradeMessage:
                        {
                            this.OnBrokenTradeMessage(in payload.BrokenTradeMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.NetOrderImbalanceIndicatorMessage:
                        {
                            this.OnNetOrderImbalanceIndicatorMessage(in payload.NetOrderImbalanceIndicatorMessage, in message.MessageHeader, message.SequenceNumber);
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
    ///  Called with each Packet Header of a reading.
    /// </summary>
    partial void OnPacketHeader(in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.PacketHeader.Layout value);

    /// <summary>
    ///  Called with each Heartbeat of a reading.
    /// </summary>
    partial void OnHeartbeat();

    /// <summary>
    ///  Called with each End Of Session of a reading.
    /// </summary>
    partial void OnEndOfSession();

    /// <summary>
    ///  Called with each System Event Message of a reading.
    /// </summary>
    partial void OnSystemEventMessage(in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.SystemEventMessage.Layout message, in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Stock Directory Message of a reading.
    /// </summary>
    partial void OnStockDirectoryMessage(in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.StockDirectoryMessage.Layout message, in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Stock Trading Action Message of a reading.
    /// </summary>
    partial void OnStockTradingActionMessage(in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.StockTradingActionMessage.Layout message, in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Reg Sho Short Sale Price Test Restricted Indicator Message of a reading.
    /// </summary>
    partial void OnRegShoShortSalePriceTestRestrictedIndicatorMessage(in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout message, in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Market Participant Position Message of a reading.
    /// </summary>
    partial void OnMarketParticipantPositionMessage(in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.MarketParticipantPositionMessage.Layout message, in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Mwcb Decline Level Message of a reading.
    /// </summary>
    partial void OnMwcbDeclineLevelMessage(in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.MwcbDeclineLevelMessage.Layout message, in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Mwcb Status Level Message of a reading.
    /// </summary>
    partial void OnMwcbStatusLevelMessage(in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.MwcbStatusLevelMessage.Layout message, in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Luld Auction Collar Message of a reading.
    /// </summary>
    partial void OnLuldAuctionCollarMessage(in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.LuldAuctionCollarMessage.Layout message, in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Operational Halt Message of a reading.
    /// </summary>
    partial void OnOperationalHaltMessage(in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.OperationalHaltMessage.Layout message, in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Add Order No Mpid Attribution Message of a reading.
    /// </summary>
    partial void OnAddOrderNoMpidAttributionMessage(in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.AddOrderNoMpidAttributionMessage.Layout message, in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Add Order With Mpid Attribution Message of a reading.
    /// </summary>
    partial void OnAddOrderWithMpidAttributionMessage(in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.AddOrderWithMpidAttributionMessage.Layout message, in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Order Executed Message of a reading.
    /// </summary>
    partial void OnOrderExecutedMessage(in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.OrderExecutedMessage.Layout message, in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Order Executed With Price Message of a reading.
    /// </summary>
    partial void OnOrderExecutedWithPriceMessage(in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.OrderExecutedWithPriceMessage.Layout message, in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Order Cancel Message of a reading.
    /// </summary>
    partial void OnOrderCancelMessage(in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.OrderCancelMessage.Layout message, in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Order Delete Message of a reading.
    /// </summary>
    partial void OnOrderDeleteMessage(in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.OrderDeleteMessage.Layout message, in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Order Replace Message of a reading.
    /// </summary>
    partial void OnOrderReplaceMessage(in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.OrderReplaceMessage.Layout message, in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Trade Message Non Cross of a reading.
    /// </summary>
    partial void OnTradeMessageNonCross(in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.TradeMessageNonCross.Layout message, in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Cross Trade Message of a reading.
    /// </summary>
    partial void OnCrossTradeMessage(in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.CrossTradeMessage.Layout message, in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Broken Trade Message of a reading.
    /// </summary>
    partial void OnBrokenTradeMessage(in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.BrokenTradeMessage.Layout message, in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Net Order Imbalance Indicator Message of a reading.
    /// </summary>
    partial void OnNetOrderImbalanceIndicatorMessage(in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.NetOrderImbalanceIndicatorMessage.Layout message, in global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Packet of a reading.
    /// </summary>
    partial void OnPacket(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Packet.Reader value);
}
