namespace Databento.MarketData.Normalized.Dbn.v2.Counter;

/// <summary>
///  Counts the messages, headers, and packets of each reading, and reads none of their fields.
/// </summary>
public struct MessageCounter
{
    const string Separator = ": ";
    const string CaptureBytes = "Capture bytes";
    const string ElapsedTime = "Elapsed time";
    readonly long started;
    long onTradeMsgCount;
    long onMbp1MsgCount;
    long onMbp10MsgCount;
    long onOhlcvMsgCount;
    long onStatusMsgCount;
    long onInstrumentDefMsgCount;
    long onImbalanceMsgCount;
    long onErrorMsgCount;
    long onSymbolMappingMsgCount;
    long onSystemMsgCount;
    long onStatMsgCount;
    long onMboMsgCount;
    long onCmbp1MsgCount;
    long onCbboMsgCount;
    long onBboMsgCount;
    long onFileCount;
    long onGatewayStreamCount;
    long onClientStreamCount;

    /// <summary>
    ///  Starts the clock for the elapsed time.
    /// </summary>
    public MessageCounter() => this.started = global::System.Diagnostics.Stopwatch.GetTimestamp();

    /// <summary>
    ///  Visits the packet and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitFile(global::Databento.MarketData.Normalized.Dbn.v2.File.Reader file)
    {
        var recordEntries = file.Record;
        while (recordEntries.MoveNext())
        {
            var record = recordEntries.Current;
            this.VisitRecord(record);
        }
        this.OnFile(file);
    }

    /// <summary>
    ///  Visits the packet and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitGatewayStream(global::Databento.MarketData.Normalized.Dbn.v2.GatewayStream.Reader gatewayStream)
    {
        var recordEntries = gatewayStream.Record;
        while (recordEntries.MoveNext())
        {
            var record = recordEntries.Current;
            this.VisitRecord(record);
        }
        this.OnGatewayStream(gatewayStream);
    }

    /// <summary>
    ///  Visits the packet and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitClientStream(global::Databento.MarketData.Normalized.Dbn.v2.ClientStream.Reader clientStream)
    {
        this.OnClientStream(clientStream);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitRecord(global::Databento.MarketData.Normalized.Dbn.v2.Record.Reader record)
    {
        var recordBody = record.RecordBody;
        switch (recordBody.Case)
        {
            case global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.TradeMsg:
            {
                this.OnTradeMsg(in recordBody.TradeMsg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.Mbp1Msg:
            {
                this.OnMbp1Msg(in recordBody.Mbp1Msg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.Mbp10Msg:
            {
                this.OnMbp10Msg(recordBody.Mbp10Msg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.OhlcvMsg:
            {
                this.OnOhlcvMsg(in recordBody.OhlcvMsg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.StatusMsg:
            {
                this.OnStatusMsg(in recordBody.StatusMsg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.InstrumentDefMsg:
            {
                this.OnInstrumentDefMsg(in recordBody.InstrumentDefMsg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.ImbalanceMsg:
            {
                this.OnImbalanceMsg(in recordBody.ImbalanceMsg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.ErrorMsg:
            {
                this.OnErrorMsg(in recordBody.ErrorMsg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.SymbolMappingMsg:
            {
                this.OnSymbolMappingMsg(in recordBody.SymbolMappingMsg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.SystemMsg:
            {
                this.OnSystemMsg(in recordBody.SystemMsg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.StatMsg:
            {
                this.OnStatMsg(in recordBody.StatMsg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.MboMsg:
            {
                this.OnMboMsg(in recordBody.MboMsg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.Cmbp1Msg:
            {
                this.OnCmbp1Msg(in recordBody.Cmbp1Msg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.CbboMsg:
            {
                this.OnCbboMsg(in recordBody.CbboMsg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.BboMsg:
            {
                this.OnBboMsg(in recordBody.BboMsg);
                break;
            }
        }
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitControlMessage(global::Databento.MarketData.Normalized.Dbn.v2.ControlMessage.Reader controlMessage)
    {
    }

    /// <summary>
    ///  Called with each Trade Msg of a reading.
    /// </summary>
    public void OnTradeMsg(in global::Databento.MarketData.Normalized.Dbn.v2.TradeMsg.Layout value) => this.onTradeMsgCount++;

    /// <summary>
    ///  Called with each Mbp 1 Msg of a reading.
    /// </summary>
    public void OnMbp1Msg(in global::Databento.MarketData.Normalized.Dbn.v2.Mbp1Msg.Layout value) => this.onMbp1MsgCount++;

    /// <summary>
    ///  Called with each Mbp 10 Msg of a reading.
    /// </summary>
    public void OnMbp10Msg(global::Databento.MarketData.Normalized.Dbn.v2.Mbp10Msg.Reader value) => this.onMbp10MsgCount++;

    /// <summary>
    ///  Called with each Ohlcv Msg of a reading.
    /// </summary>
    public void OnOhlcvMsg(in global::Databento.MarketData.Normalized.Dbn.v2.OhlcvMsg.Layout value) => this.onOhlcvMsgCount++;

    /// <summary>
    ///  Called with each Status Msg of a reading.
    /// </summary>
    public void OnStatusMsg(in global::Databento.MarketData.Normalized.Dbn.v2.StatusMsg.Layout value) => this.onStatusMsgCount++;

    /// <summary>
    ///  Called with each Instrument Def Msg of a reading.
    /// </summary>
    public void OnInstrumentDefMsg(in global::Databento.MarketData.Normalized.Dbn.v2.InstrumentDefMsg.Layout value) => this.onInstrumentDefMsgCount++;

    /// <summary>
    ///  Called with each Imbalance Msg of a reading.
    /// </summary>
    public void OnImbalanceMsg(in global::Databento.MarketData.Normalized.Dbn.v2.ImbalanceMsg.Layout value) => this.onImbalanceMsgCount++;

    /// <summary>
    ///  Called with each Error Msg of a reading.
    /// </summary>
    public void OnErrorMsg(in global::Databento.MarketData.Normalized.Dbn.v2.ErrorMsg.Layout value) => this.onErrorMsgCount++;

    /// <summary>
    ///  Called with each Symbol Mapping Msg of a reading.
    /// </summary>
    public void OnSymbolMappingMsg(in global::Databento.MarketData.Normalized.Dbn.v2.SymbolMappingMsg.Layout value) => this.onSymbolMappingMsgCount++;

    /// <summary>
    ///  Called with each System Msg of a reading.
    /// </summary>
    public void OnSystemMsg(in global::Databento.MarketData.Normalized.Dbn.v2.SystemMsg.Layout value) => this.onSystemMsgCount++;

    /// <summary>
    ///  Called with each Stat Msg of a reading.
    /// </summary>
    public void OnStatMsg(in global::Databento.MarketData.Normalized.Dbn.v2.StatMsg.Layout value) => this.onStatMsgCount++;

    /// <summary>
    ///  Called with each Mbo Msg of a reading.
    /// </summary>
    public void OnMboMsg(in global::Databento.MarketData.Normalized.Dbn.v2.MboMsg.Layout value) => this.onMboMsgCount++;

    /// <summary>
    ///  Called with each Cmbp 1 Msg of a reading.
    /// </summary>
    public void OnCmbp1Msg(in global::Databento.MarketData.Normalized.Dbn.v2.Cmbp1Msg.Layout value) => this.onCmbp1MsgCount++;

    /// <summary>
    ///  Called with each Cbbo Msg of a reading.
    /// </summary>
    public void OnCbboMsg(in global::Databento.MarketData.Normalized.Dbn.v2.CbboMsg.Layout value) => this.onCbboMsgCount++;

    /// <summary>
    ///  Called with each Bbo Msg of a reading.
    /// </summary>
    public void OnBboMsg(in global::Databento.MarketData.Normalized.Dbn.v2.BboMsg.Layout value) => this.onBboMsgCount++;

    /// <summary>
    ///  Called with each File of a reading.
    /// </summary>
    public void OnFile(global::Databento.MarketData.Normalized.Dbn.v2.File.Reader value) => this.onFileCount++;

    /// <summary>
    ///  Called with each Gateway Stream of a reading.
    /// </summary>
    public void OnGatewayStream(global::Databento.MarketData.Normalized.Dbn.v2.GatewayStream.Reader value) => this.onGatewayStreamCount++;

    /// <summary>
    ///  Called with each Client Stream of a reading.
    /// </summary>
    public void OnClientStream(global::Databento.MarketData.Normalized.Dbn.v2.ClientStream.Reader value) => this.onClientStreamCount++;

    /// <summary>
    ///  Writes the counts of messages, headers and packets, the capture file size and the elapsed time to the console.
    /// </summary>
    /// <param name="captureBytes">
    ///  The number of bytes of the capture file.
    /// </param>
    public readonly void WriteReport(long captureBytes)
    {
        var elapsed = global::System.Diagnostics.Stopwatch.GetElapsedTime(this.started);
        Console.WriteLine($"{nameof(OnTradeMsg)}{Separator}{this.onTradeMsgCount}");
        Console.WriteLine($"{nameof(OnMbp1Msg)}{Separator}{this.onMbp1MsgCount}");
        Console.WriteLine($"{nameof(OnMbp10Msg)}{Separator}{this.onMbp10MsgCount}");
        Console.WriteLine($"{nameof(OnOhlcvMsg)}{Separator}{this.onOhlcvMsgCount}");
        Console.WriteLine($"{nameof(OnStatusMsg)}{Separator}{this.onStatusMsgCount}");
        Console.WriteLine($"{nameof(OnInstrumentDefMsg)}{Separator}{this.onInstrumentDefMsgCount}");
        Console.WriteLine($"{nameof(OnImbalanceMsg)}{Separator}{this.onImbalanceMsgCount}");
        Console.WriteLine($"{nameof(OnErrorMsg)}{Separator}{this.onErrorMsgCount}");
        Console.WriteLine($"{nameof(OnSymbolMappingMsg)}{Separator}{this.onSymbolMappingMsgCount}");
        Console.WriteLine($"{nameof(OnSystemMsg)}{Separator}{this.onSystemMsgCount}");
        Console.WriteLine($"{nameof(OnStatMsg)}{Separator}{this.onStatMsgCount}");
        Console.WriteLine($"{nameof(OnMboMsg)}{Separator}{this.onMboMsgCount}");
        Console.WriteLine($"{nameof(OnCmbp1Msg)}{Separator}{this.onCmbp1MsgCount}");
        Console.WriteLine($"{nameof(OnCbboMsg)}{Separator}{this.onCbboMsgCount}");
        Console.WriteLine($"{nameof(OnBboMsg)}{Separator}{this.onBboMsgCount}");
        Console.WriteLine($"{nameof(OnFile)}{Separator}{this.onFileCount}");
        Console.WriteLine($"{nameof(OnGatewayStream)}{Separator}{this.onGatewayStreamCount}");
        Console.WriteLine($"{nameof(OnClientStream)}{Separator}{this.onClientStreamCount}");
        Console.WriteLine($"{CaptureBytes}{Separator}{captureBytes}");
        Console.WriteLine($"{ElapsedTime}{Separator}{elapsed}");
    }
}
