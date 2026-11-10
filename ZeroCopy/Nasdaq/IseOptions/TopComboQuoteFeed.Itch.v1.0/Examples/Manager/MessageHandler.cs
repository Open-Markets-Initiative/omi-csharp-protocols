namespace Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Manager;

/// <summary>
///  Passes each message, header, and packet of a reading to the method of the same name.
/// </summary>
public sealed partial class MessageHandler
{
    /// <summary>
    ///  Visits the packet and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitPacket(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Packet.Reader packet)
    {
        this.OnPacketHeader(in packet.PacketHeader);
        var messages = packet.Messages;
        switch (messages.Case)
        {
            case global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Messages.Case.Heartbeat:
            {
                this.OnHeartbeat();
                break;
            }
            case global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Messages.Case.EndOfSession:
            {
                this.OnEndOfSession();
                break;
            }
            case global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Messages.Case.Message:
            {
                var messageEntries = messages.Message;
                while (messageEntries.MoveNext())
                {
                    var message = messageEntries.Current;
                    var payload = message.Payload;
                    switch (payload.Case)
                    {
                        case global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.SystemEventMessage:
                        {
                            this.OnSystemEventMessage(in payload.SystemEventMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.ComplexStrategyDirectoryMessage:
                        {
                            this.OnComplexStrategyDirectoryMessage(payload.ComplexStrategyDirectoryMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyOpenClosedMessage:
                        {
                            this.OnStrategyOpenClosedMessage(in payload.StrategyOpenClosedMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyTradingActionMessage:
                        {
                            this.OnStrategyTradingActionMessage(in payload.StrategyTradingActionMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyBestBidAndAskUpdate:
                        {
                            this.OnStrategyBestBidAndAskUpdate(in payload.StrategyBestBidAndAskUpdate, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyBestBidUpdate:
                        {
                            this.OnStrategyBestBidUpdate(in payload.StrategyBestBidUpdate, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyBestAskUpdate:
                        {
                            this.OnStrategyBestAskUpdate(in payload.StrategyBestAskUpdate, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.ComplexStrategyTickerMessage:
                        {
                            this.OnComplexStrategyTickerMessage(in payload.ComplexStrategyTickerMessage, in message.MessageHeader, message.SequenceNumber);
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
    partial void OnPacketHeader(in global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.PacketHeader.Layout value);

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
    partial void OnSystemEventMessage(in global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.SystemEventMessage.Layout message, in global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Complex Strategy Directory Message of a reading.
    /// </summary>
    partial void OnComplexStrategyDirectoryMessage(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyDirectoryMessage.Reader message, in global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Strategy Open Closed Message of a reading.
    /// </summary>
    partial void OnStrategyOpenClosedMessage(in global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyOpenClosedMessage.Layout message, in global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Strategy Trading Action Message of a reading.
    /// </summary>
    partial void OnStrategyTradingActionMessage(in global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyTradingActionMessage.Layout message, in global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Strategy Best Bid And Ask Update of a reading.
    /// </summary>
    partial void OnStrategyBestBidAndAskUpdate(in global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidAndAskUpdate.Layout message, in global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Strategy Best Bid Update of a reading.
    /// </summary>
    partial void OnStrategyBestBidUpdate(in global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidUpdate.Layout message, in global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Strategy Best Ask Update of a reading.
    /// </summary>
    partial void OnStrategyBestAskUpdate(in global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestAskUpdate.Layout message, in global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Complex Strategy Ticker Message of a reading.
    /// </summary>
    partial void OnComplexStrategyTickerMessage(in global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyTickerMessage.Layout message, in global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageHeader.Layout header, ulong sequenceNumber);

    /// <summary>
    ///  Called with each Packet of a reading.
    /// </summary>
    partial void OnPacket(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Packet.Reader value);
}
