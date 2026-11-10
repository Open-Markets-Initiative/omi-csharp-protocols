namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Counter;

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
    long onComplexStrategyDirectoryMessageCount;
    long onStrategyTradingActionMessageCount;
    long onStrategyOpenClosedMessageCount;
    long onComplexStrategyOrderOnBookMessageCount;
    long onComplexStrategyAuctionMessageCount;
    long onPacketCount;

    /// <summary>
    ///  Starts the clock for the elapsed time.
    /// </summary>
    public MessageCounter() => this.started = global::System.Diagnostics.Stopwatch.GetTimestamp();

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
    public void OnPacketHeader(in global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.PacketHeader.Layout value) => this.onPacketHeaderCount++;

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
    public void OnSystemEventMessage(in global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.SystemEventMessage.Layout value, in global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageHeader.Layout header, ulong sequenceNumber) => this.onSystemEventMessageCount++;

    /// <summary>
    ///  Called with each Complex Strategy Directory Message of a reading.
    /// </summary>
    public void OnComplexStrategyDirectoryMessage(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyDirectoryMessage.Reader value, in global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageHeader.Layout header, ulong sequenceNumber) => this.onComplexStrategyDirectoryMessageCount++;

    /// <summary>
    ///  Called with each Strategy Trading Action Message of a reading.
    /// </summary>
    public void OnStrategyTradingActionMessage(in global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyTradingActionMessage.Layout value, in global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageHeader.Layout header, ulong sequenceNumber) => this.onStrategyTradingActionMessageCount++;

    /// <summary>
    ///  Called with each Strategy Open Closed Message of a reading.
    /// </summary>
    public void OnStrategyOpenClosedMessage(in global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyOpenClosedMessage.Layout value, in global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageHeader.Layout header, ulong sequenceNumber) => this.onStrategyOpenClosedMessageCount++;

    /// <summary>
    ///  Called with each Complex Strategy Order On Book Message of a reading.
    /// </summary>
    public void OnComplexStrategyOrderOnBookMessage(in global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyOrderOnBookMessage.Layout value, in global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageHeader.Layout header, ulong sequenceNumber) => this.onComplexStrategyOrderOnBookMessageCount++;

    /// <summary>
    ///  Called with each Complex Strategy Auction Message of a reading.
    /// </summary>
    public void OnComplexStrategyAuctionMessage(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Reader value, in global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageHeader.Layout header, ulong sequenceNumber) => this.onComplexStrategyAuctionMessageCount++;

    /// <summary>
    ///  Called with each Packet of a reading.
    /// </summary>
    public void OnPacket(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Packet.Reader value) => this.onPacketCount++;

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
        Console.WriteLine($"{nameof(OnComplexStrategyDirectoryMessage)}{Separator}{this.onComplexStrategyDirectoryMessageCount}");
        Console.WriteLine($"{nameof(OnStrategyTradingActionMessage)}{Separator}{this.onStrategyTradingActionMessageCount}");
        Console.WriteLine($"{nameof(OnStrategyOpenClosedMessage)}{Separator}{this.onStrategyOpenClosedMessageCount}");
        Console.WriteLine($"{nameof(OnComplexStrategyOrderOnBookMessage)}{Separator}{this.onComplexStrategyOrderOnBookMessageCount}");
        Console.WriteLine($"{nameof(OnComplexStrategyAuctionMessage)}{Separator}{this.onComplexStrategyAuctionMessageCount}");
        Console.WriteLine($"{nameof(OnPacket)}{Separator}{this.onPacketCount}");
        Console.WriteLine($"{CaptureBytes}{Separator}{captureBytes}");
        Console.WriteLine($"{ElapsedTime}{Separator}{elapsed}");
    }
}
