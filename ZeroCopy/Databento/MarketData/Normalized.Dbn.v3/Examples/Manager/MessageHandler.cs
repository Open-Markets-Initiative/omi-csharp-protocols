namespace Databento.MarketData.Normalized.Dbn.v3.Manager;

/// <summary>
///  Passes each message, header, and packet of a reading to the method of the same name.
/// </summary>
public sealed partial class MessageHandler
{
    /// <summary>
    ///  Gets or sets the values of the conversation that a later packet reads.
    /// </summary>
    public ConversationState ConversationState { get; set; } = new();

    /// <summary>
    ///  Visits the packet and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitFile(global::Databento.MarketData.Normalized.Dbn.v3.File.Reader file)
    {
        this.ConversationState.Apply(file);
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
    public void VisitGatewayStream(global::Databento.MarketData.Normalized.Dbn.v3.GatewayStream.Reader gatewayStream)
    {
        this.ConversationState.Apply(gatewayStream);
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
    public void VisitClientStream(global::Databento.MarketData.Normalized.Dbn.v3.ClientStream.Reader clientStream)
    {
        this.OnClientStream(clientStream);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitRecord(global::Databento.MarketData.Normalized.Dbn.v3.Record.Reader record)
    {
        var recordBody = record.RecordBody;
        switch (recordBody.Case)
        {
            case global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Case.TradeMsg:
            {
                this.OnTradeMsg(in recordBody.TradeMsg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Case.Mbp1Msg:
            {
                this.OnMbp1Msg(in recordBody.Mbp1Msg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Case.Mbp10Msg:
            {
                this.OnMbp10Msg(recordBody.Mbp10Msg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Case.OhlcvMsg:
            {
                this.OnOhlcvMsg(in recordBody.OhlcvMsg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Case.StatusMsg:
            {
                this.OnStatusMsg(in recordBody.StatusMsg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Case.InstrumentDefMsg:
            {
                this.OnInstrumentDefMsg(in recordBody.InstrumentDefMsg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Case.ImbalanceMsg:
            {
                this.OnImbalanceMsg(in recordBody.ImbalanceMsg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Case.ErrorMsg:
            {
                this.OnErrorMsg(in recordBody.ErrorMsg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Case.SymbolMappingMsg:
            {
                this.OnSymbolMappingMsg(in recordBody.SymbolMappingMsg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Case.SystemMsg:
            {
                this.OnSystemMsg(in recordBody.SystemMsg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Case.StatMsg:
            {
                this.OnStatMsg(in recordBody.StatMsg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Case.MboMsg:
            {
                this.OnMboMsg(in recordBody.MboMsg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Case.Cmbp1Msg:
            {
                this.OnCmbp1Msg(in recordBody.Cmbp1Msg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Case.CbboMsg:
            {
                this.OnCbboMsg(in recordBody.CbboMsg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Case.BboMsg:
            {
                this.OnBboMsg(in recordBody.BboMsg);
                break;
            }
        }
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitControlMessage(global::Databento.MarketData.Normalized.Dbn.v3.ControlMessage.Reader controlMessage)
    {
    }

    /// <summary>
    ///  Called with each Trade Msg of a reading.
    /// </summary>
    partial void OnTradeMsg(in global::Databento.MarketData.Normalized.Dbn.v3.TradeMsg.Layout message);

    /// <summary>
    ///  Called with each Mbp 1 Msg of a reading.
    /// </summary>
    partial void OnMbp1Msg(in global::Databento.MarketData.Normalized.Dbn.v3.Mbp1Msg.Layout message);

    /// <summary>
    ///  Called with each Mbp 10 Msg of a reading.
    /// </summary>
    partial void OnMbp10Msg(global::Databento.MarketData.Normalized.Dbn.v3.Mbp10Msg.Reader message);

    /// <summary>
    ///  Called with each Ohlcv Msg of a reading.
    /// </summary>
    partial void OnOhlcvMsg(in global::Databento.MarketData.Normalized.Dbn.v3.OhlcvMsg.Layout message);

    /// <summary>
    ///  Called with each Status Msg of a reading.
    /// </summary>
    partial void OnStatusMsg(in global::Databento.MarketData.Normalized.Dbn.v3.StatusMsg.Layout message);

    /// <summary>
    ///  Called with each Instrument Def Msg of a reading.
    /// </summary>
    partial void OnInstrumentDefMsg(in global::Databento.MarketData.Normalized.Dbn.v3.InstrumentDefMsg.Layout message);

    /// <summary>
    ///  Called with each Imbalance Msg of a reading.
    /// </summary>
    partial void OnImbalanceMsg(in global::Databento.MarketData.Normalized.Dbn.v3.ImbalanceMsg.Layout message);

    /// <summary>
    ///  Called with each Error Msg of a reading.
    /// </summary>
    partial void OnErrorMsg(in global::Databento.MarketData.Normalized.Dbn.v3.ErrorMsg.Layout message);

    /// <summary>
    ///  Called with each Symbol Mapping Msg of a reading.
    /// </summary>
    partial void OnSymbolMappingMsg(in global::Databento.MarketData.Normalized.Dbn.v3.SymbolMappingMsg.Layout message);

    /// <summary>
    ///  Called with each System Msg of a reading.
    /// </summary>
    partial void OnSystemMsg(in global::Databento.MarketData.Normalized.Dbn.v3.SystemMsg.Layout message);

    /// <summary>
    ///  Called with each Stat Msg of a reading.
    /// </summary>
    partial void OnStatMsg(in global::Databento.MarketData.Normalized.Dbn.v3.StatMsg.Layout message);

    /// <summary>
    ///  Called with each Mbo Msg of a reading.
    /// </summary>
    partial void OnMboMsg(in global::Databento.MarketData.Normalized.Dbn.v3.MboMsg.Layout message);

    /// <summary>
    ///  Called with each Cmbp 1 Msg of a reading.
    /// </summary>
    partial void OnCmbp1Msg(in global::Databento.MarketData.Normalized.Dbn.v3.Cmbp1Msg.Layout message);

    /// <summary>
    ///  Called with each Cbbo Msg of a reading.
    /// </summary>
    partial void OnCbboMsg(in global::Databento.MarketData.Normalized.Dbn.v3.CbboMsg.Layout message);

    /// <summary>
    ///  Called with each Bbo Msg of a reading.
    /// </summary>
    partial void OnBboMsg(in global::Databento.MarketData.Normalized.Dbn.v3.BboMsg.Layout message);

    /// <summary>
    ///  Called with each File of a reading.
    /// </summary>
    partial void OnFile(global::Databento.MarketData.Normalized.Dbn.v3.File.Reader value);

    /// <summary>
    ///  Called with each Gateway Stream of a reading.
    /// </summary>
    partial void OnGatewayStream(global::Databento.MarketData.Normalized.Dbn.v3.GatewayStream.Reader value);

    /// <summary>
    ///  Called with each Client Stream of a reading.
    /// </summary>
    partial void OnClientStream(global::Databento.MarketData.Normalized.Dbn.v3.ClientStream.Reader value);
}
