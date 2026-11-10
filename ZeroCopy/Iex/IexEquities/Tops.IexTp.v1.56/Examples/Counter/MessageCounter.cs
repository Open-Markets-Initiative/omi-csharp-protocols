namespace Iex.IexEquities.Tops.IexTp.v1_56.Counter;

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
    long onQuoteUpdateMessageCount;
    long onTradeReportMessageCount;
    long onTradeBreakMessageCount;
    long onPacketCount;

    /// <summary>
    ///  Starts the clock for the elapsed time.
    /// </summary>
    public MessageCounter() => this.started = global::System.Diagnostics.Stopwatch.GetTimestamp();

    /// <summary>
    ///  Visits the packet and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitPacket(global::Iex.IexEquities.Tops.IexTp.v1_56.Packet.Reader packet)
    {
        this.OnIextpHeader(in packet.IextpHeader);
        var messages = packet.Messages;
        switch (messages.Case)
        {
            case global::Iex.IexEquities.Tops.IexTp.v1_56.Messages.Case.Message:
            {
                var messageEntries = messages.Message;
                while (messageEntries.MoveNext())
                {
                    var message = messageEntries.Current;
                    var messageData = message.MessageData;
                    switch (messageData.Case)
                    {
                        case global::Iex.IexEquities.Tops.IexTp.v1_56.MessageData.Case.QuoteUpdateMessage:
                        {
                            this.OnQuoteUpdateMessage(in messageData.QuoteUpdateMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Tops.IexTp.v1_56.MessageData.Case.TradeReportMessage:
                        {
                            this.OnTradeReportMessage(in messageData.TradeReportMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Tops.IexTp.v1_56.MessageData.Case.TradeBreakMessage:
                        {
                            this.OnTradeBreakMessage(in messageData.TradeBreakMessage, in message.MessageHeader, message.SequenceNumber);
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
    public void OnIextpHeader(in global::Iex.IexEquities.Tops.IexTp.v1_56.IextpHeader.Layout value) => this.onIextpHeaderCount++;

    /// <summary>
    ///  Called with each Quote Update Message of a reading.
    /// </summary>
    public void OnQuoteUpdateMessage(in global::Iex.IexEquities.Tops.IexTp.v1_56.QuoteUpdateMessage.Layout value, in global::Iex.IexEquities.Tops.IexTp.v1_56.MessageHeader.Layout header, ulong sequenceNumber) => this.onQuoteUpdateMessageCount++;

    /// <summary>
    ///  Called with each Trade Report Message of a reading.
    /// </summary>
    public void OnTradeReportMessage(in global::Iex.IexEquities.Tops.IexTp.v1_56.TradeReportMessage.Layout value, in global::Iex.IexEquities.Tops.IexTp.v1_56.MessageHeader.Layout header, ulong sequenceNumber) => this.onTradeReportMessageCount++;

    /// <summary>
    ///  Called with each Trade Break Message of a reading.
    /// </summary>
    public void OnTradeBreakMessage(in global::Iex.IexEquities.Tops.IexTp.v1_56.TradeBreakMessage.Layout value, in global::Iex.IexEquities.Tops.IexTp.v1_56.MessageHeader.Layout header, ulong sequenceNumber) => this.onTradeBreakMessageCount++;

    /// <summary>
    ///  Called with each Packet of a reading.
    /// </summary>
    public void OnPacket(global::Iex.IexEquities.Tops.IexTp.v1_56.Packet.Reader value) => this.onPacketCount++;

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
        Console.WriteLine($"{nameof(OnQuoteUpdateMessage)}{Separator}{this.onQuoteUpdateMessageCount}");
        Console.WriteLine($"{nameof(OnTradeReportMessage)}{Separator}{this.onTradeReportMessageCount}");
        Console.WriteLine($"{nameof(OnTradeBreakMessage)}{Separator}{this.onTradeBreakMessageCount}");
        Console.WriteLine($"{nameof(OnPacket)}{Separator}{this.onPacketCount}");
        Console.WriteLine($"{CaptureBytes}{Separator}{captureBytes}");
        Console.WriteLine($"{ElapsedTime}{Separator}{elapsed}");
    }
}
