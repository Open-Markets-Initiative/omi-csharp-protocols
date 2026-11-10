namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Manager;

/// <summary>
///  Passes each message, header, and packet of a reading to the method of the same name.
/// </summary>
public sealed partial class MessageHandler
{
    /// <summary>
    ///  Visits the packet and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitPacket(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Packet.Reader packet)
    {
        this.OnPacketHeader(in packet.PacketHeader);
        var messages = packet.Messages;
        switch (messages.Case)
        {
            case global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Messages.Case.Heartbeat:
            {
                this.OnHeartbeat();
                break;
            }
            case global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Messages.Case.EndOfSession:
            {
                this.OnEndOfSession();
                break;
            }
            case global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Messages.Case.Message:
            {
                var messageEntries = messages.Message;
                while (messageEntries.MoveNext())
                {
                    var message = messageEntries.Current;
                    var payload = message.Payload;
                    switch (payload.Case)
                    {
                        case global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.SystemEventMessage:
                        {
                            this.OnSystemEventMessage(in payload.SystemEventMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.ComplexStrategyDirectoryMessage:
                        {
                            this.OnComplexStrategyDirectoryMessage(payload.ComplexStrategyDirectoryMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.StrategyTradingActionMessage:
                        {
                            this.OnStrategyTradingActionMessage(in payload.StrategyTradingActionMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.StrategyOpenClosedMessage:
                        {
                            this.OnStrategyOpenClosedMessage(in payload.StrategyOpenClosedMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.ComplexStrategyOrderOnBookMessage:
                        {
                            this.OnComplexStrategyOrderOnBookMessage(in payload.ComplexStrategyOrderOnBookMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.ComplexStrategyAuctionMessage:
                        {
                            this.OnComplexStrategyAuctionMessage(payload.ComplexStrategyAuctionMessage, in message.MessageHeader, message.SequenceNumber);
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
    partial void OnPacketHeader(in global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.PacketHeader.Layout value);

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
    partial void OnSystemEventMessage(in global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.SystemEventMessage.Layout message, in global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Complex Strategy Directory Message of a reading.
    /// </summary>
    partial void OnComplexStrategyDirectoryMessage(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyDirectoryMessage.Reader message, in global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Strategy Trading Action Message of a reading.
    /// </summary>
    partial void OnStrategyTradingActionMessage(in global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyTradingActionMessage.Layout message, in global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Strategy Open Closed Message of a reading.
    /// </summary>
    partial void OnStrategyOpenClosedMessage(in global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyOpenClosedMessage.Layout message, in global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Complex Strategy Order On Book Message of a reading.
    /// </summary>
    partial void OnComplexStrategyOrderOnBookMessage(in global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyOrderOnBookMessage.Layout message, in global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Complex Strategy Auction Message of a reading.
    /// </summary>
    partial void OnComplexStrategyAuctionMessage(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Reader message, in global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Packet of a reading.
    /// </summary>
    partial void OnPacket(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Packet.Reader value);
}
