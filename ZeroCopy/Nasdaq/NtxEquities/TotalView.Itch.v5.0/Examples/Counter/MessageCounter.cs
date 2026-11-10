namespace Nasdaq.NtxEquities.TotalView.Itch.v5_0.Counter;

/// <summary>
///  Counts the messages, headers, and packets of each reading, and reads none of their fields.
/// </summary>
public struct MessageCounter
{
    const string Separator = ": ";
    const string CaptureBytes = "Capture bytes";
    const string ElapsedTime = "Elapsed time";
    readonly long started;
    long onPacketHeaderCount;
    long onHeartbeatCount;
    long onEndOfSessionCount;
    long onSystemEventMessageCount;
    long onStockDirectoryMessageCount;
    long onStockTradingActionMessageCount;
    long onRegShoShortSalePriceTestRestrictedIndicatorMessageCount;
    long onMarketParticipantPositionMessageCount;
    long onMwcbDeclineLevelMessageCount;
    long onMwcbStatusMessageCount;
    long onLuldAuctionCollarMessageCount;
    long onOperationalHaltMessageCount;
    long onAddOrderNoMpidAttributionMessageCount;
    long onAddOrderMpidAttributionMessageCount;
    long onOrderExecutedMessageCount;
    long onOrderExecutedWithPriceMessageCount;
    long onOrderCancelMessageCount;
    long onOrderDeleteMessageCount;
    long onOrderReplaceMessageCount;
    long onNonCrossTradeMessageCount;
    long onCrossTradeMessageCount;
    long onBrokenTradeMessageCount;
    long onNetOrderImbalanceIndicatorMessageCount;
    long onRetailInterestMessageCount;
    long onPacketCount;

    /// <summary>
    ///  Starts the clock for the elapsed time.
    /// </summary>
    public MessageCounter() => this.started = global::System.Diagnostics.Stopwatch.GetTimestamp();

    /// <summary>
    ///  Visits the packet and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitPacket(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Packet.Reader packet)
    {
        this.OnPacketHeader(in packet.PacketHeader);
        var messages = packet.Messages;
        switch (messages.Case)
        {
            case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Messages.Case.Heartbeat:
            {
                this.OnHeartbeat();
                break;
            }
            case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Messages.Case.EndOfSession:
            {
                this.OnEndOfSession();
                break;
            }
            case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Messages.Case.Message:
            {
                var messageEntries = messages.Message;
                while (messageEntries.MoveNext())
                {
                    var message = messageEntries.Current;
                    var payload = message.Payload;
                    switch (payload.Case)
                    {
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.SystemEventMessage:
                        {
                            this.OnSystemEventMessage(in payload.SystemEventMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.StockDirectoryMessage:
                        {
                            this.OnStockDirectoryMessage(in payload.StockDirectoryMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.StockTradingActionMessage:
                        {
                            this.OnStockTradingActionMessage(in payload.StockTradingActionMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.RegShoShortSalePriceTestRestrictedIndicatorMessage:
                        {
                            this.OnRegShoShortSalePriceTestRestrictedIndicatorMessage(in payload.RegShoShortSalePriceTestRestrictedIndicatorMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.MarketParticipantPositionMessage:
                        {
                            this.OnMarketParticipantPositionMessage(in payload.MarketParticipantPositionMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.MwcbDeclineLevelMessage:
                        {
                            this.OnMwcbDeclineLevelMessage(in payload.MwcbDeclineLevelMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.MwcbStatusMessage:
                        {
                            this.OnMwcbStatusMessage(in payload.MwcbStatusMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.LuldAuctionCollarMessage:
                        {
                            this.OnLuldAuctionCollarMessage(in payload.LuldAuctionCollarMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.OperationalHaltMessage:
                        {
                            this.OnOperationalHaltMessage(in payload.OperationalHaltMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.AddOrderNoMpidAttributionMessage:
                        {
                            this.OnAddOrderNoMpidAttributionMessage(in payload.AddOrderNoMpidAttributionMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.AddOrderMpidAttributionMessage:
                        {
                            this.OnAddOrderMpidAttributionMessage(in payload.AddOrderMpidAttributionMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.OrderExecutedMessage:
                        {
                            this.OnOrderExecutedMessage(in payload.OrderExecutedMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.OrderExecutedWithPriceMessage:
                        {
                            this.OnOrderExecutedWithPriceMessage(in payload.OrderExecutedWithPriceMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.OrderCancelMessage:
                        {
                            this.OnOrderCancelMessage(in payload.OrderCancelMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.OrderDeleteMessage:
                        {
                            this.OnOrderDeleteMessage(in payload.OrderDeleteMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.OrderReplaceMessage:
                        {
                            this.OnOrderReplaceMessage(in payload.OrderReplaceMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.NonCrossTradeMessage:
                        {
                            this.OnNonCrossTradeMessage(in payload.NonCrossTradeMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.CrossTradeMessage:
                        {
                            this.OnCrossTradeMessage(in payload.CrossTradeMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.BrokenTradeMessage:
                        {
                            this.OnBrokenTradeMessage(in payload.BrokenTradeMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.NetOrderImbalanceIndicatorMessage:
                        {
                            this.OnNetOrderImbalanceIndicatorMessage(in payload.NetOrderImbalanceIndicatorMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.RetailInterestMessage:
                        {
                            this.OnRetailInterestMessage(in payload.RetailInterestMessage, in message.MessageHeader, message.SequenceNumber);
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
    public void OnPacketHeader(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.PacketHeader.Layout value) => this.onPacketHeaderCount++;

    /// <summary>
    ///  Called with each Heartbeat of a reading.
    /// </summary>
    public void OnHeartbeat() => this.onHeartbeatCount++;

    /// <summary>
    ///  Called with each End Of Session of a reading.
    /// </summary>
    public void OnEndOfSession() => this.onEndOfSessionCount++;

    /// <summary>
    ///  Called with each System Event Message of a reading.
    /// </summary>
    public void OnSystemEventMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.SystemEventMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber) => this.onSystemEventMessageCount++;

    /// <summary>
    ///  Called with each Stock Directory Message of a reading.
    /// </summary>
    public void OnStockDirectoryMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockDirectoryMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber) => this.onStockDirectoryMessageCount++;

    /// <summary>
    ///  Called with each Stock Trading Action Message of a reading.
    /// </summary>
    public void OnStockTradingActionMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockTradingActionMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber) => this.onStockTradingActionMessageCount++;

    /// <summary>
    ///  Called with each Reg Sho Short Sale Price Test Restricted Indicator Message of a reading.
    /// </summary>
    public void OnRegShoShortSalePriceTestRestrictedIndicatorMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber) => this.onRegShoShortSalePriceTestRestrictedIndicatorMessageCount++;

    /// <summary>
    ///  Called with each Market Participant Position Message of a reading.
    /// </summary>
    public void OnMarketParticipantPositionMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MarketParticipantPositionMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber) => this.onMarketParticipantPositionMessageCount++;

    /// <summary>
    ///  Called with each Mwcb Decline Level Message of a reading.
    /// </summary>
    public void OnMwcbDeclineLevelMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbDeclineLevelMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber) => this.onMwcbDeclineLevelMessageCount++;

    /// <summary>
    ///  Called with each Mwcb Status Message of a reading.
    /// </summary>
    public void OnMwcbStatusMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbStatusMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber) => this.onMwcbStatusMessageCount++;

    /// <summary>
    ///  Called with each Luld Auction Collar Message of a reading.
    /// </summary>
    public void OnLuldAuctionCollarMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.LuldAuctionCollarMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber) => this.onLuldAuctionCollarMessageCount++;

    /// <summary>
    ///  Called with each Operational Halt Message of a reading.
    /// </summary>
    public void OnOperationalHaltMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OperationalHaltMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber) => this.onOperationalHaltMessageCount++;

    /// <summary>
    ///  Called with each Add Order No Mpid Attribution Message of a reading.
    /// </summary>
    public void OnAddOrderNoMpidAttributionMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderNoMpidAttributionMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber) => this.onAddOrderNoMpidAttributionMessageCount++;

    /// <summary>
    ///  Called with each Add Order Mpid Attribution Message of a reading.
    /// </summary>
    public void OnAddOrderMpidAttributionMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderMpidAttributionMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber) => this.onAddOrderMpidAttributionMessageCount++;

    /// <summary>
    ///  Called with each Order Executed Message of a reading.
    /// </summary>
    public void OnOrderExecutedMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber) => this.onOrderExecutedMessageCount++;

    /// <summary>
    ///  Called with each Order Executed With Price Message of a reading.
    /// </summary>
    public void OnOrderExecutedWithPriceMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedWithPriceMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber) => this.onOrderExecutedWithPriceMessageCount++;

    /// <summary>
    ///  Called with each Order Cancel Message of a reading.
    /// </summary>
    public void OnOrderCancelMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderCancelMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber) => this.onOrderCancelMessageCount++;

    /// <summary>
    ///  Called with each Order Delete Message of a reading.
    /// </summary>
    public void OnOrderDeleteMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderDeleteMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber) => this.onOrderDeleteMessageCount++;

    /// <summary>
    ///  Called with each Order Replace Message of a reading.
    /// </summary>
    public void OnOrderReplaceMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderReplaceMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber) => this.onOrderReplaceMessageCount++;

    /// <summary>
    ///  Called with each Non Cross Trade Message of a reading.
    /// </summary>
    public void OnNonCrossTradeMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NonCrossTradeMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber) => this.onNonCrossTradeMessageCount++;

    /// <summary>
    ///  Called with each Cross Trade Message of a reading.
    /// </summary>
    public void OnCrossTradeMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.CrossTradeMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber) => this.onCrossTradeMessageCount++;

    /// <summary>
    ///  Called with each Broken Trade Message of a reading.
    /// </summary>
    public void OnBrokenTradeMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.BrokenTradeMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber) => this.onBrokenTradeMessageCount++;

    /// <summary>
    ///  Called with each Net Order Imbalance Indicator Message of a reading.
    /// </summary>
    public void OnNetOrderImbalanceIndicatorMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NetOrderImbalanceIndicatorMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber) => this.onNetOrderImbalanceIndicatorMessageCount++;

    /// <summary>
    ///  Called with each Retail Interest Message of a reading.
    /// </summary>
    public void OnRetailInterestMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RetailInterestMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber) => this.onRetailInterestMessageCount++;

    /// <summary>
    ///  Called with each Packet of a reading.
    /// </summary>
    public void OnPacket(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Packet.Reader value) => this.onPacketCount++;

    /// <summary>
    ///  Writes the counts of messages, headers and packets, the capture file size and the elapsed time to the console.
    /// </summary>
    /// <param name="captureBytes">
    ///  The number of bytes of the capture file.
    /// </param>
    public readonly void WriteReport(long captureBytes)
    {
        var elapsed = global::System.Diagnostics.Stopwatch.GetElapsedTime(this.started);
        Console.WriteLine($"{nameof(OnPacketHeader)}{Separator}{this.onPacketHeaderCount}");
        Console.WriteLine($"{nameof(OnHeartbeat)}{Separator}{this.onHeartbeatCount}");
        Console.WriteLine($"{nameof(OnEndOfSession)}{Separator}{this.onEndOfSessionCount}");
        Console.WriteLine($"{nameof(OnSystemEventMessage)}{Separator}{this.onSystemEventMessageCount}");
        Console.WriteLine($"{nameof(OnStockDirectoryMessage)}{Separator}{this.onStockDirectoryMessageCount}");
        Console.WriteLine($"{nameof(OnStockTradingActionMessage)}{Separator}{this.onStockTradingActionMessageCount}");
        Console.WriteLine($"{nameof(OnRegShoShortSalePriceTestRestrictedIndicatorMessage)}{Separator}{this.onRegShoShortSalePriceTestRestrictedIndicatorMessageCount}");
        Console.WriteLine($"{nameof(OnMarketParticipantPositionMessage)}{Separator}{this.onMarketParticipantPositionMessageCount}");
        Console.WriteLine($"{nameof(OnMwcbDeclineLevelMessage)}{Separator}{this.onMwcbDeclineLevelMessageCount}");
        Console.WriteLine($"{nameof(OnMwcbStatusMessage)}{Separator}{this.onMwcbStatusMessageCount}");
        Console.WriteLine($"{nameof(OnLuldAuctionCollarMessage)}{Separator}{this.onLuldAuctionCollarMessageCount}");
        Console.WriteLine($"{nameof(OnOperationalHaltMessage)}{Separator}{this.onOperationalHaltMessageCount}");
        Console.WriteLine($"{nameof(OnAddOrderNoMpidAttributionMessage)}{Separator}{this.onAddOrderNoMpidAttributionMessageCount}");
        Console.WriteLine($"{nameof(OnAddOrderMpidAttributionMessage)}{Separator}{this.onAddOrderMpidAttributionMessageCount}");
        Console.WriteLine($"{nameof(OnOrderExecutedMessage)}{Separator}{this.onOrderExecutedMessageCount}");
        Console.WriteLine($"{nameof(OnOrderExecutedWithPriceMessage)}{Separator}{this.onOrderExecutedWithPriceMessageCount}");
        Console.WriteLine($"{nameof(OnOrderCancelMessage)}{Separator}{this.onOrderCancelMessageCount}");
        Console.WriteLine($"{nameof(OnOrderDeleteMessage)}{Separator}{this.onOrderDeleteMessageCount}");
        Console.WriteLine($"{nameof(OnOrderReplaceMessage)}{Separator}{this.onOrderReplaceMessageCount}");
        Console.WriteLine($"{nameof(OnNonCrossTradeMessage)}{Separator}{this.onNonCrossTradeMessageCount}");
        Console.WriteLine($"{nameof(OnCrossTradeMessage)}{Separator}{this.onCrossTradeMessageCount}");
        Console.WriteLine($"{nameof(OnBrokenTradeMessage)}{Separator}{this.onBrokenTradeMessageCount}");
        Console.WriteLine($"{nameof(OnNetOrderImbalanceIndicatorMessage)}{Separator}{this.onNetOrderImbalanceIndicatorMessageCount}");
        Console.WriteLine($"{nameof(OnRetailInterestMessage)}{Separator}{this.onRetailInterestMessageCount}");
        Console.WriteLine($"{nameof(OnPacket)}{Separator}{this.onPacketCount}");
        Console.WriteLine($"{CaptureBytes}{Separator}{captureBytes}");
        Console.WriteLine($"{ElapsedTime}{Separator}{elapsed}");
    }
}
