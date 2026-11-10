namespace Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Manager;

/// <summary>
///  Passes each message, header, and packet of a reading to the method of the same name.
/// </summary>
public sealed partial class MessageHandler
{
    /// <summary>
    ///  Visits the packet and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitPacket(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Packet.Reader packet)
    {
        this.OnPacketHeader(in packet.PacketHeader);
        var messages = packet.Messages;
        switch (messages.Case)
        {
            case global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Messages.Case.Heartbeat:
            {
                this.OnHeartbeat();
                break;
            }
            case global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Messages.Case.EndOfSession:
            {
                this.OnEndOfSession();
                break;
            }
            case global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Messages.Case.Message:
            {
                var messageEntries = messages.Message;
                while (messageEntries.MoveNext())
                {
                    var message = messageEntries.Current;
                    var payload = message.Payload;
                    switch (payload.Case)
                    {
                        case global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.SystemEventMessage:
                        {
                            this.OnSystemEventMessage(in payload.SystemEventMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.StockDirectoryMessage:
                        {
                            this.OnStockDirectoryMessage(in payload.StockDirectoryMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.StockTradingActionMessage:
                        {
                            this.OnStockTradingActionMessage(in payload.StockTradingActionMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.RegShoRestrictionMessage:
                        {
                            this.OnRegShoRestrictionMessage(in payload.RegShoRestrictionMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.MwcbDeclineLevelMessage:
                        {
                            this.OnMwcbDeclineLevelMessage(in payload.MwcbDeclineLevelMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.MwcbBreachMessage:
                        {
                            this.OnMwcbBreachMessage(in payload.MwcbBreachMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.OperationalHaltMessage:
                        {
                            this.OnOperationalHaltMessage(in payload.OperationalHaltMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.BboQuotationMessage:
                        {
                            this.OnBboQuotationMessage(in payload.BboQuotationMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.PriceImprovementMessage:
                        {
                            this.OnPriceImprovementMessage(in payload.PriceImprovementMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.IpoQuotingPeriodUpdateMessage:
                        {
                            this.OnIpoQuotingPeriodUpdateMessage(in payload.IpoQuotingPeriodUpdateMessage, in message.MessageHeader, message.SequenceNumber);
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
    partial void OnPacketHeader(in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PacketHeader.Layout value);

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
    partial void OnSystemEventMessage(in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.SystemEventMessage.Layout message, in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Stock Directory Message of a reading.
    /// </summary>
    partial void OnStockDirectoryMessage(in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockDirectoryMessage.Layout message, in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Stock Trading Action Message of a reading.
    /// </summary>
    partial void OnStockTradingActionMessage(in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockTradingActionMessage.Layout message, in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Reg Sho Restriction Message of a reading.
    /// </summary>
    partial void OnRegShoRestrictionMessage(in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.RegShoRestrictionMessage.Layout message, in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Mwcb Decline Level Message of a reading.
    /// </summary>
    partial void OnMwcbDeclineLevelMessage(in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbDeclineLevelMessage.Layout message, in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Mwcb Breach Message of a reading.
    /// </summary>
    partial void OnMwcbBreachMessage(in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbBreachMessage.Layout message, in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Operational Halt Message of a reading.
    /// </summary>
    partial void OnOperationalHaltMessage(in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.OperationalHaltMessage.Layout message, in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Bbo Quotation Message of a reading.
    /// </summary>
    partial void OnBboQuotationMessage(in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.BboQuotationMessage.Layout message, in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Price Improvement Message of a reading.
    /// </summary>
    partial void OnPriceImprovementMessage(in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PriceImprovementMessage.Layout message, in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Ipo Quoting Period Update Message of a reading.
    /// </summary>
    partial void OnIpoQuotingPeriodUpdateMessage(in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.IpoQuotingPeriodUpdateMessage.Layout message, in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Packet of a reading.
    /// </summary>
    partial void OnPacket(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Packet.Reader value);
}
