namespace Databento.MarketData.Normalized.Dbn.v1.GatewayManager;

/// <summary>
///  Handles parsed packets, assigns a number to each message, and calls the partial method for its type.
/// </summary>
/// <remarks>
///  Implement the partial methods that you need in another file of this partial class, as file DatabentoMarketDataNormalizedDbnGatewayManager.Handlers.cs does for OnTradeMsg.
/// </remarks>
public sealed partial class DatabentoMarketDataNormalizedDbnGatewayManager : GatewayStreamHandler
{
    /// <summary>
    ///  The position of the last message received, counted from 1.
    /// </summary>
    private int index;

    /// <summary>
    ///  Gets the number of packets received that parsed.
    /// </summary>
    public long Packets { get; private set; }

    /// <summary>
    ///  Gets the number of messages in the packets received that parsed.
    /// </summary>
    public long Messages { get; private set; }

    /// <summary>
    ///  Dispatches a parsed packet to the handler methods, with the position of each message in the order received.
    /// </summary>
    /// <remarks>
    ///  A packet that failed to parse is ignored. Call this method for each packet. Handle dispatches a packet without this check.
    /// </remarks>
    public void Receive(GatewayStream packet)
    {
        if (!packet.IsParsed)
        {
            return;
        }
        ++this.Packets;
        this.Messages += packet.Messages.Count;
        Handle(packet);
    }

    /// <inheritdoc/>
    protected override void OnTradeMsg(TradeMsg message, GatewayRecordFrame frame)
    {
        ++this.index;
        OnTradeMsg(message, this.index);
    }

    /// <inheritdoc/>
    protected override void OnMbp1Msg(Mbp1Msg message, GatewayRecordFrame frame)
    {
        ++this.index;
        OnMbp1Msg(message, this.index);
    }

    /// <inheritdoc/>
    protected override void OnMbp10Msg(Mbp10Msg message, GatewayRecordFrame frame)
    {
        ++this.index;
        OnMbp10Msg(message, this.index);
    }

    /// <inheritdoc/>
    protected override void OnOhlcvMsg(OhlcvMsg message, GatewayRecordFrame frame)
    {
        ++this.index;
        OnOhlcvMsg(message, this.index);
    }

    /// <inheritdoc/>
    protected override void OnStatusMsg(StatusMsg message, GatewayRecordFrame frame)
    {
        ++this.index;
        OnStatusMsg(message, this.index);
    }

    /// <inheritdoc/>
    protected override void OnInstrumentDefMsg(InstrumentDefMsg message, GatewayRecordFrame frame)
    {
        ++this.index;
        OnInstrumentDefMsg(message, this.index);
    }

    /// <inheritdoc/>
    protected override void OnImbalanceMsg(ImbalanceMsg message, GatewayRecordFrame frame)
    {
        ++this.index;
        OnImbalanceMsg(message, this.index);
    }

    /// <inheritdoc/>
    protected override void OnErrorMsg(ErrorMsg message, GatewayRecordFrame frame)
    {
        ++this.index;
        OnErrorMsg(message, this.index);
    }

    /// <inheritdoc/>
    protected override void OnSymbolMappingMsg(SymbolMappingMsg message, GatewayRecordFrame frame)
    {
        ++this.index;
        OnSymbolMappingMsg(message, this.index);
    }

    /// <inheritdoc/>
    protected override void OnSystemMsg(SystemMsg message, GatewayRecordFrame frame)
    {
        ++this.index;
        OnSystemMsg(message, this.index);
    }

    /// <inheritdoc/>
    protected override void OnStatMsg(StatMsg message, GatewayRecordFrame frame)
    {
        ++this.index;
        OnStatMsg(message, this.index);
    }

    /// <inheritdoc/>
    protected override void OnMboMsg(MboMsg message, GatewayRecordFrame frame)
    {
        ++this.index;
        OnMboMsg(message, this.index);
    }

    /// <inheritdoc/>
    protected override void OnCmbp1Msg(Cmbp1Msg message, GatewayRecordFrame frame)
    {
        ++this.index;
        OnCmbp1Msg(message, this.index);
    }

    /// <inheritdoc/>
    protected override void OnCbboMsg(CbboMsg message, GatewayRecordFrame frame)
    {
        ++this.index;
        OnCbboMsg(message, this.index);
    }

    /// <inheritdoc/>
    protected override void OnBboMsg(BboMsg message, GatewayRecordFrame frame)
    {
        ++this.index;
        OnBboMsg(message, this.index);
    }

    /// <inheritdoc/>
    protected override void OnUnknownGatewayMessage(UnknownGatewayMessage message, GatewayRecordFrame frame)
    {
        ++this.index;
        OnUnknownGatewayMessage(message, this.index);
    }

    /// <summary>
    ///  Called for each Trade Msg.
    /// </summary>
    /// <param name="message">The received Trade Msg.</param>
    /// <param name="index">The position of the message in the order received, counted from 1. A nested message has the position of the message that contains it.</param>
    partial void OnTradeMsg(TradeMsg message, int index);

    /// <summary>
    ///  Called for each Mbp 1 Msg.
    /// </summary>
    /// <param name="message">The received Mbp 1 Msg.</param>
    /// <param name="index">The position of the message in the order received, counted from 1. A nested message has the position of the message that contains it.</param>
    partial void OnMbp1Msg(Mbp1Msg message, int index);

    /// <summary>
    ///  Called for each Mbp 10 Msg.
    /// </summary>
    /// <param name="message">The received Mbp 10 Msg.</param>
    /// <param name="index">The position of the message in the order received, counted from 1. A nested message has the position of the message that contains it.</param>
    partial void OnMbp10Msg(Mbp10Msg message, int index);

    /// <summary>
    ///  Called for each Ohlcv Msg.
    /// </summary>
    /// <param name="message">The received Ohlcv Msg.</param>
    /// <param name="index">The position of the message in the order received, counted from 1. A nested message has the position of the message that contains it.</param>
    partial void OnOhlcvMsg(OhlcvMsg message, int index);

    /// <summary>
    ///  Called for each Status Msg.
    /// </summary>
    /// <param name="message">The received Status Msg.</param>
    /// <param name="index">The position of the message in the order received, counted from 1. A nested message has the position of the message that contains it.</param>
    partial void OnStatusMsg(StatusMsg message, int index);

    /// <summary>
    ///  Called for each Instrument Def Msg.
    /// </summary>
    /// <param name="message">The received Instrument Def Msg.</param>
    /// <param name="index">The position of the message in the order received, counted from 1. A nested message has the position of the message that contains it.</param>
    partial void OnInstrumentDefMsg(InstrumentDefMsg message, int index);

    /// <summary>
    ///  Called for each Imbalance Msg.
    /// </summary>
    /// <param name="message">The received Imbalance Msg.</param>
    /// <param name="index">The position of the message in the order received, counted from 1. A nested message has the position of the message that contains it.</param>
    partial void OnImbalanceMsg(ImbalanceMsg message, int index);

    /// <summary>
    ///  Called for each Error Msg.
    /// </summary>
    /// <param name="message">The received Error Msg.</param>
    /// <param name="index">The position of the message in the order received, counted from 1. A nested message has the position of the message that contains it.</param>
    partial void OnErrorMsg(ErrorMsg message, int index);

    /// <summary>
    ///  Called for each Symbol Mapping Msg.
    /// </summary>
    /// <param name="message">The received Symbol Mapping Msg.</param>
    /// <param name="index">The position of the message in the order received, counted from 1. A nested message has the position of the message that contains it.</param>
    partial void OnSymbolMappingMsg(SymbolMappingMsg message, int index);

    /// <summary>
    ///  Called for each System Msg.
    /// </summary>
    /// <param name="message">The received System Msg.</param>
    /// <param name="index">The position of the message in the order received, counted from 1. A nested message has the position of the message that contains it.</param>
    partial void OnSystemMsg(SystemMsg message, int index);

    /// <summary>
    ///  Called for each Stat Msg.
    /// </summary>
    /// <param name="message">The received Stat Msg.</param>
    /// <param name="index">The position of the message in the order received, counted from 1. A nested message has the position of the message that contains it.</param>
    partial void OnStatMsg(StatMsg message, int index);

    /// <summary>
    ///  Called for each Mbo Msg.
    /// </summary>
    /// <param name="message">The received Mbo Msg.</param>
    /// <param name="index">The position of the message in the order received, counted from 1. A nested message has the position of the message that contains it.</param>
    partial void OnMboMsg(MboMsg message, int index);

    /// <summary>
    ///  Called for each Cmbp 1 Msg.
    /// </summary>
    /// <param name="message">The received Cmbp 1 Msg.</param>
    /// <param name="index">The position of the message in the order received, counted from 1. A nested message has the position of the message that contains it.</param>
    partial void OnCmbp1Msg(Cmbp1Msg message, int index);

    /// <summary>
    ///  Called for each Cbbo Msg.
    /// </summary>
    /// <param name="message">The received Cbbo Msg.</param>
    /// <param name="index">The position of the message in the order received, counted from 1. A nested message has the position of the message that contains it.</param>
    partial void OnCbboMsg(CbboMsg message, int index);

    /// <summary>
    ///  Called for each Bbo Msg.
    /// </summary>
    /// <param name="message">The received Bbo Msg.</param>
    /// <param name="index">The position of the message in the order received, counted from 1. A nested message has the position of the message that contains it.</param>
    partial void OnBboMsg(BboMsg message, int index);

    /// <summary>
    ///  Called for a message whose message type the protocol does not define.
    /// </summary>
    /// <param name="message">The received message.</param>
    /// <param name="index">The position of the message in the order received, counted from 1. A nested message has the position of the message that contains it.</param>
    partial void OnUnknownGatewayMessage(UnknownGatewayMessage message, int index);

}
