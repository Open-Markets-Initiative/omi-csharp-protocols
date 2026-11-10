namespace Iex.IexEquities.DeepPlus.IexTp.v1_01.Counter;

/// <summary>
///  Counts the messages, headers, and packets of each reading, and reads none of their fields.
/// </summary>
public struct MessageCounter
{
    const string Separator = ": ";
    const string CaptureBytes = "Capture bytes";
    const string ElapsedTime = "Elapsed time";
    readonly long started;
    long onIextpHeaderCount;
    long onSystemEventMessageCount;
    long onSecurityDirectoryMessageCount;
    long onTradingStatusMessageCount;
    long onRetailLiquidityIndicatorMessageCount;
    long onOperationalHaltStatusMessageCount;
    long onShortSalePriceTestStatusMessageCount;
    long onSecurityEventMessageCount;
    long onAddOrderMessageCount;
    long onOrderModifyMessageCount;
    long onOrderDeleteMessageCount;
    long onOrderExecutedMessageCount;
    long onTradeMessageCount;
    long onTradeBreakMessageCount;
    long onClearBookMessageCount;
    long onPacketCount;

    /// <summary>
    ///  Starts the clock for the elapsed time.
    /// </summary>
    public MessageCounter() => this.started = global::System.Diagnostics.Stopwatch.GetTimestamp();

    /// <summary>
    ///  Visits the packet and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitPacket(global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Packet.Reader packet)
    {
        this.OnIextpHeader(in packet.IextpHeader);
        var messages = packet.Messages;
        switch (messages.Case)
        {
            case global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Messages.Case.Message:
            {
                var messageEntries = messages.Message;
                while (messageEntries.MoveNext())
                {
                    var message = messageEntries.Current;
                    var messageData = message.MessageData;
                    switch (messageData.Case)
                    {
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.SystemEventMessage:
                        {
                            this.OnSystemEventMessage(in messageData.SystemEventMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.SecurityDirectoryMessage:
                        {
                            this.OnSecurityDirectoryMessage(in messageData.SecurityDirectoryMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.TradingStatusMessage:
                        {
                            this.OnTradingStatusMessage(in messageData.TradingStatusMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.RetailLiquidityIndicatorMessage:
                        {
                            this.OnRetailLiquidityIndicatorMessage(in messageData.RetailLiquidityIndicatorMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.OperationalHaltStatusMessage:
                        {
                            this.OnOperationalHaltStatusMessage(in messageData.OperationalHaltStatusMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.ShortSalePriceTestStatusMessage:
                        {
                            this.OnShortSalePriceTestStatusMessage(in messageData.ShortSalePriceTestStatusMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.SecurityEventMessage:
                        {
                            this.OnSecurityEventMessage(in messageData.SecurityEventMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.AddOrderMessage:
                        {
                            this.OnAddOrderMessage(in messageData.AddOrderMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.OrderModifyMessage:
                        {
                            this.OnOrderModifyMessage(in messageData.OrderModifyMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.OrderDeleteMessage:
                        {
                            this.OnOrderDeleteMessage(in messageData.OrderDeleteMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.OrderExecutedMessage:
                        {
                            this.OnOrderExecutedMessage(in messageData.OrderExecutedMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.TradeMessage:
                        {
                            this.OnTradeMessage(in messageData.TradeMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.TradeBreakMessage:
                        {
                            this.OnTradeBreakMessage(in messageData.TradeBreakMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.ClearBookMessage:
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
    public void OnIextpHeader(in global::Iex.IexEquities.DeepPlus.IexTp.v1_01.IextpHeader.Layout value) => this.onIextpHeaderCount++;

    /// <summary>
    ///  Called with each System Event Message of a reading.
    /// </summary>
    public void OnSystemEventMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_01.SystemEventMessage.Layout value, in global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageHeader.Layout header, ulong sequenceNumber) => this.onSystemEventMessageCount++;

    /// <summary>
    ///  Called with each Security Directory Message of a reading.
    /// </summary>
    public void OnSecurityDirectoryMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_01.SecurityDirectoryMessage.Layout value, in global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageHeader.Layout header, ulong sequenceNumber) => this.onSecurityDirectoryMessageCount++;

    /// <summary>
    ///  Called with each Trading Status Message of a reading.
    /// </summary>
    public void OnTradingStatusMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_01.TradingStatusMessage.Layout value, in global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageHeader.Layout header, ulong sequenceNumber) => this.onTradingStatusMessageCount++;

    /// <summary>
    ///  Called with each Retail Liquidity Indicator Message of a reading.
    /// </summary>
    public void OnRetailLiquidityIndicatorMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_01.RetailLiquidityIndicatorMessage.Layout value, in global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageHeader.Layout header, ulong sequenceNumber) => this.onRetailLiquidityIndicatorMessageCount++;

    /// <summary>
    ///  Called with each Operational Halt Status Message of a reading.
    /// </summary>
    public void OnOperationalHaltStatusMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_01.OperationalHaltStatusMessage.Layout value, in global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageHeader.Layout header, ulong sequenceNumber) => this.onOperationalHaltStatusMessageCount++;

    /// <summary>
    ///  Called with each Short Sale Price Test Status Message of a reading.
    /// </summary>
    public void OnShortSalePriceTestStatusMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_01.ShortSalePriceTestStatusMessage.Layout value, in global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageHeader.Layout header, ulong sequenceNumber) => this.onShortSalePriceTestStatusMessageCount++;

    /// <summary>
    ///  Called with each Security Event Message of a reading.
    /// </summary>
    public void OnSecurityEventMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_01.SecurityEventMessage.Layout value, in global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageHeader.Layout header, ulong sequenceNumber) => this.onSecurityEventMessageCount++;

    /// <summary>
    ///  Called with each Add Order Message of a reading.
    /// </summary>
    public void OnAddOrderMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_01.AddOrderMessage.Layout value, in global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageHeader.Layout header, ulong sequenceNumber) => this.onAddOrderMessageCount++;

    /// <summary>
    ///  Called with each Order Modify Message of a reading.
    /// </summary>
    public void OnOrderModifyMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_01.OrderModifyMessage.Layout value, in global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageHeader.Layout header, ulong sequenceNumber) => this.onOrderModifyMessageCount++;

    /// <summary>
    ///  Called with each Order Delete Message of a reading.
    /// </summary>
    public void OnOrderDeleteMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_01.OrderDeleteMessage.Layout value, in global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageHeader.Layout header, ulong sequenceNumber) => this.onOrderDeleteMessageCount++;

    /// <summary>
    ///  Called with each Order Executed Message of a reading.
    /// </summary>
    public void OnOrderExecutedMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_01.OrderExecutedMessage.Layout value, in global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageHeader.Layout header, ulong sequenceNumber) => this.onOrderExecutedMessageCount++;

    /// <summary>
    ///  Called with each Trade Message of a reading.
    /// </summary>
    public void OnTradeMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_01.TradeMessage.Layout value, in global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageHeader.Layout header, ulong sequenceNumber) => this.onTradeMessageCount++;

    /// <summary>
    ///  Called with each Trade Break Message of a reading.
    /// </summary>
    public void OnTradeBreakMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_01.TradeBreakMessage.Layout value, in global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageHeader.Layout header, ulong sequenceNumber) => this.onTradeBreakMessageCount++;

    /// <summary>
    ///  Called with each Clear Book Message of a reading.
    /// </summary>
    public void OnClearBookMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_01.ClearBookMessage.Layout value, in global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageHeader.Layout header, ulong sequenceNumber) => this.onClearBookMessageCount++;

    /// <summary>
    ///  Called with each Packet of a reading.
    /// </summary>
    public void OnPacket(global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Packet.Reader value) => this.onPacketCount++;

    /// <summary>
    ///  Writes the counts of messages, headers and packets, the capture file size and the elapsed time to the console.
    /// </summary>
    /// <param name="captureBytes">
    ///  The number of bytes of the capture file.
    /// </param>
    public readonly void WriteReport(long captureBytes)
    {
        var elapsed = global::System.Diagnostics.Stopwatch.GetElapsedTime(this.started);
        Console.WriteLine($"{nameof(OnIextpHeader)}{Separator}{this.onIextpHeaderCount}");
        Console.WriteLine($"{nameof(OnSystemEventMessage)}{Separator}{this.onSystemEventMessageCount}");
        Console.WriteLine($"{nameof(OnSecurityDirectoryMessage)}{Separator}{this.onSecurityDirectoryMessageCount}");
        Console.WriteLine($"{nameof(OnTradingStatusMessage)}{Separator}{this.onTradingStatusMessageCount}");
        Console.WriteLine($"{nameof(OnRetailLiquidityIndicatorMessage)}{Separator}{this.onRetailLiquidityIndicatorMessageCount}");
        Console.WriteLine($"{nameof(OnOperationalHaltStatusMessage)}{Separator}{this.onOperationalHaltStatusMessageCount}");
        Console.WriteLine($"{nameof(OnShortSalePriceTestStatusMessage)}{Separator}{this.onShortSalePriceTestStatusMessageCount}");
        Console.WriteLine($"{nameof(OnSecurityEventMessage)}{Separator}{this.onSecurityEventMessageCount}");
        Console.WriteLine($"{nameof(OnAddOrderMessage)}{Separator}{this.onAddOrderMessageCount}");
        Console.WriteLine($"{nameof(OnOrderModifyMessage)}{Separator}{this.onOrderModifyMessageCount}");
        Console.WriteLine($"{nameof(OnOrderDeleteMessage)}{Separator}{this.onOrderDeleteMessageCount}");
        Console.WriteLine($"{nameof(OnOrderExecutedMessage)}{Separator}{this.onOrderExecutedMessageCount}");
        Console.WriteLine($"{nameof(OnTradeMessage)}{Separator}{this.onTradeMessageCount}");
        Console.WriteLine($"{nameof(OnTradeBreakMessage)}{Separator}{this.onTradeBreakMessageCount}");
        Console.WriteLine($"{nameof(OnClearBookMessage)}{Separator}{this.onClearBookMessageCount}");
        Console.WriteLine($"{nameof(OnPacket)}{Separator}{this.onPacketCount}");
        Console.WriteLine($"{CaptureBytes}{Separator}{captureBytes}");
        Console.WriteLine($"{ElapsedTime}{Separator}{elapsed}");
    }
}
