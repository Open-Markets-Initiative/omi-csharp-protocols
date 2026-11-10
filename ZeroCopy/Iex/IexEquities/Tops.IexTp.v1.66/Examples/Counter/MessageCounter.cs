namespace Iex.IexEquities.Tops.IexTp.v1_66.Counter;

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
    long onQuoteUpdateMessageCount;
    long onTradeReportMessageCount;
    long onOfficialPriceMessageCount;
    long onTradeBreakMessageCount;
    long onAuctionInformationMessageCount;
    long onPacketCount;

    /// <summary>
    ///  Starts the clock for the elapsed time.
    /// </summary>
    public MessageCounter() => this.started = global::System.Diagnostics.Stopwatch.GetTimestamp();

    /// <summary>
    ///  Visits the packet and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitPacket(global::Iex.IexEquities.Tops.IexTp.v1_66.Packet.Reader packet)
    {
        this.OnIextpHeader(in packet.IextpHeader);
        var messages = packet.Messages;
        switch (messages.Case)
        {
            case global::Iex.IexEquities.Tops.IexTp.v1_66.Messages.Case.Message:
            {
                var messageEntries = messages.Message;
                while (messageEntries.MoveNext())
                {
                    var message = messageEntries.Current;
                    var messageData = message.MessageData;
                    switch (messageData.Case)
                    {
                        case global::Iex.IexEquities.Tops.IexTp.v1_66.MessageData.Case.SystemEventMessage:
                        {
                            this.OnSystemEventMessage(in messageData.SystemEventMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Tops.IexTp.v1_66.MessageData.Case.SecurityDirectoryMessage:
                        {
                            this.OnSecurityDirectoryMessage(in messageData.SecurityDirectoryMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Tops.IexTp.v1_66.MessageData.Case.TradingStatusMessage:
                        {
                            this.OnTradingStatusMessage(in messageData.TradingStatusMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Tops.IexTp.v1_66.MessageData.Case.RetailLiquidityIndicatorMessage:
                        {
                            this.OnRetailLiquidityIndicatorMessage(in messageData.RetailLiquidityIndicatorMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Tops.IexTp.v1_66.MessageData.Case.OperationalHaltStatusMessage:
                        {
                            this.OnOperationalHaltStatusMessage(in messageData.OperationalHaltStatusMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Tops.IexTp.v1_66.MessageData.Case.ShortSalePriceTestStatusMessage:
                        {
                            this.OnShortSalePriceTestStatusMessage(in messageData.ShortSalePriceTestStatusMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Tops.IexTp.v1_66.MessageData.Case.QuoteUpdateMessage:
                        {
                            this.OnQuoteUpdateMessage(in messageData.QuoteUpdateMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Tops.IexTp.v1_66.MessageData.Case.TradeReportMessage:
                        {
                            this.OnTradeReportMessage(in messageData.TradeReportMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Tops.IexTp.v1_66.MessageData.Case.OfficialPriceMessage:
                        {
                            this.OnOfficialPriceMessage(in messageData.OfficialPriceMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Tops.IexTp.v1_66.MessageData.Case.TradeBreakMessage:
                        {
                            this.OnTradeBreakMessage(in messageData.TradeBreakMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Tops.IexTp.v1_66.MessageData.Case.AuctionInformationMessage:
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
    public void OnIextpHeader(in global::Iex.IexEquities.Tops.IexTp.v1_66.IextpHeader.Layout value) => this.onIextpHeaderCount++;

    /// <summary>
    ///  Called with each System Event Message of a reading.
    /// </summary>
    public void OnSystemEventMessage(in global::Iex.IexEquities.Tops.IexTp.v1_66.SystemEventMessage.Layout value, in global::Iex.IexEquities.Tops.IexTp.v1_66.MessageHeader.Layout header, ulong sequenceNumber) => this.onSystemEventMessageCount++;

    /// <summary>
    ///  Called with each Security Directory Message of a reading.
    /// </summary>
    public void OnSecurityDirectoryMessage(in global::Iex.IexEquities.Tops.IexTp.v1_66.SecurityDirectoryMessage.Layout value, in global::Iex.IexEquities.Tops.IexTp.v1_66.MessageHeader.Layout header, ulong sequenceNumber) => this.onSecurityDirectoryMessageCount++;

    /// <summary>
    ///  Called with each Trading Status Message of a reading.
    /// </summary>
    public void OnTradingStatusMessage(in global::Iex.IexEquities.Tops.IexTp.v1_66.TradingStatusMessage.Layout value, in global::Iex.IexEquities.Tops.IexTp.v1_66.MessageHeader.Layout header, ulong sequenceNumber) => this.onTradingStatusMessageCount++;

    /// <summary>
    ///  Called with each Retail Liquidity Indicator Message of a reading.
    /// </summary>
    public void OnRetailLiquidityIndicatorMessage(in global::Iex.IexEquities.Tops.IexTp.v1_66.RetailLiquidityIndicatorMessage.Layout value, in global::Iex.IexEquities.Tops.IexTp.v1_66.MessageHeader.Layout header, ulong sequenceNumber) => this.onRetailLiquidityIndicatorMessageCount++;

    /// <summary>
    ///  Called with each Operational Halt Status Message of a reading.
    /// </summary>
    public void OnOperationalHaltStatusMessage(in global::Iex.IexEquities.Tops.IexTp.v1_66.OperationalHaltStatusMessage.Layout value, in global::Iex.IexEquities.Tops.IexTp.v1_66.MessageHeader.Layout header, ulong sequenceNumber) => this.onOperationalHaltStatusMessageCount++;

    /// <summary>
    ///  Called with each Short Sale Price Test Status Message of a reading.
    /// </summary>
    public void OnShortSalePriceTestStatusMessage(in global::Iex.IexEquities.Tops.IexTp.v1_66.ShortSalePriceTestStatusMessage.Layout value, in global::Iex.IexEquities.Tops.IexTp.v1_66.MessageHeader.Layout header, ulong sequenceNumber) => this.onShortSalePriceTestStatusMessageCount++;

    /// <summary>
    ///  Called with each Quote Update Message of a reading.
    /// </summary>
    public void OnQuoteUpdateMessage(in global::Iex.IexEquities.Tops.IexTp.v1_66.QuoteUpdateMessage.Layout value, in global::Iex.IexEquities.Tops.IexTp.v1_66.MessageHeader.Layout header, ulong sequenceNumber) => this.onQuoteUpdateMessageCount++;

    /// <summary>
    ///  Called with each Trade Report Message of a reading.
    /// </summary>
    public void OnTradeReportMessage(in global::Iex.IexEquities.Tops.IexTp.v1_66.TradeReportMessage.Layout value, in global::Iex.IexEquities.Tops.IexTp.v1_66.MessageHeader.Layout header, ulong sequenceNumber) => this.onTradeReportMessageCount++;

    /// <summary>
    ///  Called with each Official Price Message of a reading.
    /// </summary>
    public void OnOfficialPriceMessage(in global::Iex.IexEquities.Tops.IexTp.v1_66.OfficialPriceMessage.Layout value, in global::Iex.IexEquities.Tops.IexTp.v1_66.MessageHeader.Layout header, ulong sequenceNumber) => this.onOfficialPriceMessageCount++;

    /// <summary>
    ///  Called with each Trade Break Message of a reading.
    /// </summary>
    public void OnTradeBreakMessage(in global::Iex.IexEquities.Tops.IexTp.v1_66.TradeBreakMessage.Layout value, in global::Iex.IexEquities.Tops.IexTp.v1_66.MessageHeader.Layout header, ulong sequenceNumber) => this.onTradeBreakMessageCount++;

    /// <summary>
    ///  Called with each Auction Information Message of a reading.
    /// </summary>
    public void OnAuctionInformationMessage(in global::Iex.IexEquities.Tops.IexTp.v1_66.AuctionInformationMessage.Layout value, in global::Iex.IexEquities.Tops.IexTp.v1_66.MessageHeader.Layout header, ulong sequenceNumber) => this.onAuctionInformationMessageCount++;

    /// <summary>
    ///  Called with each Packet of a reading.
    /// </summary>
    public void OnPacket(global::Iex.IexEquities.Tops.IexTp.v1_66.Packet.Reader value) => this.onPacketCount++;

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
        Console.WriteLine($"{nameof(OnQuoteUpdateMessage)}{Separator}{this.onQuoteUpdateMessageCount}");
        Console.WriteLine($"{nameof(OnTradeReportMessage)}{Separator}{this.onTradeReportMessageCount}");
        Console.WriteLine($"{nameof(OnOfficialPriceMessage)}{Separator}{this.onOfficialPriceMessageCount}");
        Console.WriteLine($"{nameof(OnTradeBreakMessage)}{Separator}{this.onTradeBreakMessageCount}");
        Console.WriteLine($"{nameof(OnAuctionInformationMessage)}{Separator}{this.onAuctionInformationMessageCount}");
        Console.WriteLine($"{nameof(OnPacket)}{Separator}{this.onPacketCount}");
        Console.WriteLine($"{CaptureBytes}{Separator}{captureBytes}");
        Console.WriteLine($"{ElapsedTime}{Separator}{elapsed}");
    }
}
