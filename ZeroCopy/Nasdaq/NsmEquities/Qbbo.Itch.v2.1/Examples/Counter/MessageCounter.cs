namespace Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Counter;

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
    long onRegShoRestrictionMessageCount;
    long onMwcbDeclineLevelMessageCount;
    long onMwcbBreachMessageCount;
    long onOperationalHaltMessageCount;
    long onBboQuotationMessageCount;
    long onPriceImprovementMessageCount;
    long onIpoQuotingPeriodUpdateMessageCount;
    long onPacketCount;

    /// <summary>
    ///  Starts the clock for the elapsed time.
    /// </summary>
    public MessageCounter() => this.started = global::System.Diagnostics.Stopwatch.GetTimestamp();

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
    public void OnPacketHeader(in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PacketHeader.Layout value) => this.onPacketHeaderCount++;

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
    public void OnSystemEventMessage(in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.SystemEventMessage.Layout value, in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout header, ulong sequenceNumber) => this.onSystemEventMessageCount++;

    /// <summary>
    ///  Called with each Stock Directory Message of a reading.
    /// </summary>
    public void OnStockDirectoryMessage(in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockDirectoryMessage.Layout value, in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout header, ulong sequenceNumber) => this.onStockDirectoryMessageCount++;

    /// <summary>
    ///  Called with each Stock Trading Action Message of a reading.
    /// </summary>
    public void OnStockTradingActionMessage(in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockTradingActionMessage.Layout value, in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout header, ulong sequenceNumber) => this.onStockTradingActionMessageCount++;

    /// <summary>
    ///  Called with each Reg Sho Restriction Message of a reading.
    /// </summary>
    public void OnRegShoRestrictionMessage(in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.RegShoRestrictionMessage.Layout value, in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout header, ulong sequenceNumber) => this.onRegShoRestrictionMessageCount++;

    /// <summary>
    ///  Called with each Mwcb Decline Level Message of a reading.
    /// </summary>
    public void OnMwcbDeclineLevelMessage(in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbDeclineLevelMessage.Layout value, in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout header, ulong sequenceNumber) => this.onMwcbDeclineLevelMessageCount++;

    /// <summary>
    ///  Called with each Mwcb Breach Message of a reading.
    /// </summary>
    public void OnMwcbBreachMessage(in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbBreachMessage.Layout value, in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout header, ulong sequenceNumber) => this.onMwcbBreachMessageCount++;

    /// <summary>
    ///  Called with each Operational Halt Message of a reading.
    /// </summary>
    public void OnOperationalHaltMessage(in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.OperationalHaltMessage.Layout value, in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout header, ulong sequenceNumber) => this.onOperationalHaltMessageCount++;

    /// <summary>
    ///  Called with each Bbo Quotation Message of a reading.
    /// </summary>
    public void OnBboQuotationMessage(in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.BboQuotationMessage.Layout value, in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout header, ulong sequenceNumber) => this.onBboQuotationMessageCount++;

    /// <summary>
    ///  Called with each Price Improvement Message of a reading.
    /// </summary>
    public void OnPriceImprovementMessage(in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PriceImprovementMessage.Layout value, in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout header, ulong sequenceNumber) => this.onPriceImprovementMessageCount++;

    /// <summary>
    ///  Called with each Ipo Quoting Period Update Message of a reading.
    /// </summary>
    public void OnIpoQuotingPeriodUpdateMessage(in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.IpoQuotingPeriodUpdateMessage.Layout value, in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout header, ulong sequenceNumber) => this.onIpoQuotingPeriodUpdateMessageCount++;

    /// <summary>
    ///  Called with each Packet of a reading.
    /// </summary>
    public void OnPacket(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Packet.Reader value) => this.onPacketCount++;

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
        Console.WriteLine($"{nameof(OnRegShoRestrictionMessage)}{Separator}{this.onRegShoRestrictionMessageCount}");
        Console.WriteLine($"{nameof(OnMwcbDeclineLevelMessage)}{Separator}{this.onMwcbDeclineLevelMessageCount}");
        Console.WriteLine($"{nameof(OnMwcbBreachMessage)}{Separator}{this.onMwcbBreachMessageCount}");
        Console.WriteLine($"{nameof(OnOperationalHaltMessage)}{Separator}{this.onOperationalHaltMessageCount}");
        Console.WriteLine($"{nameof(OnBboQuotationMessage)}{Separator}{this.onBboQuotationMessageCount}");
        Console.WriteLine($"{nameof(OnPriceImprovementMessage)}{Separator}{this.onPriceImprovementMessageCount}");
        Console.WriteLine($"{nameof(OnIpoQuotingPeriodUpdateMessage)}{Separator}{this.onIpoQuotingPeriodUpdateMessageCount}");
        Console.WriteLine($"{nameof(OnPacket)}{Separator}{this.onPacketCount}");
        Console.WriteLine($"{CaptureBytes}{Separator}{captureBytes}");
        Console.WriteLine($"{ElapsedTime}{Separator}{elapsed}");
    }
}
