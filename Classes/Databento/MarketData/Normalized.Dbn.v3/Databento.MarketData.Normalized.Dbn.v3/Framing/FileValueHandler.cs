namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  Handles each File and the messages that it carries.
/// </summary>
public abstract class FileValueHandler
{
    /// <summary>
    ///  Gets the packet that the handler is handling.
    /// </summary>
    public FileValue Packet { get; private set; } = null!;

    /// <summary>
    ///  Calls the handler method for each message, and for each entry or packet of a kind that carries none.
    /// </summary>
    /// <remarks>
    ///  This method does not check whether the packet is valid.
    /// </remarks>
    public void Handle(FileValue packet)
    {
        Packet = packet;
        foreach (var entry in packet.RecordFrames)
        {
            Visit(entry);
        }
    }

    private void Visit(RecordFrame frame)
    {
        if (frame.RecordBody is { } recordBodyReceived) Handle(recordBodyReceived, frame);
    }

    private void Handle(IMessage message, RecordFrame frame)
    {
        switch (message)
        {
            case TradeMsg received:
                OnTradeMsg(received, frame);
                break;
            case Mbp1Msg received:
                OnMbp1Msg(received, frame);
                break;
            case Mbp10Msg received:
                OnMbp10Msg(received, frame);
                break;
            case OhlcvMsg received:
                OnOhlcvMsg(received, frame);
                break;
            case StatusMsg received:
                OnStatusMsg(received, frame);
                break;
            case InstrumentDefMsg received:
                OnInstrumentDefMsg(received, frame);
                break;
            case ImbalanceMsg received:
                OnImbalanceMsg(received, frame);
                break;
            case ErrorMsg received:
                OnErrorMsg(received, frame);
                break;
            case SymbolMappingMsg received:
                OnSymbolMappingMsg(received, frame);
                break;
            case SystemMsg received:
                OnSystemMsg(received, frame);
                break;
            case StatMsg received:
                OnStatMsg(received, frame);
                break;
            case MboMsg received:
                OnMboMsg(received, frame);
                break;
            case Cmbp1Msg received:
                OnCmbp1Msg(received, frame);
                break;
            case CbboMsg received:
                OnCbboMsg(received, frame);
                break;
            case BboMsg received:
                OnBboMsg(received, frame);
                break;
            case UnknownFileMessage received:
                OnUnknownFileMessage(received, frame);
                break;
        }
    }

    /// <summary>
    ///  Called for each Trade Msg.
    /// </summary>
    /// <param name="message">The received Trade Msg.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnTradeMsg(TradeMsg message, RecordFrame frame) { }

    /// <summary>
    ///  Called for each Mbp 1 Msg.
    /// </summary>
    /// <param name="message">The received Mbp 1 Msg.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnMbp1Msg(Mbp1Msg message, RecordFrame frame) { }

    /// <summary>
    ///  Called for each Mbp 10 Msg.
    /// </summary>
    /// <param name="message">The received Mbp 10 Msg.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnMbp10Msg(Mbp10Msg message, RecordFrame frame) { }

    /// <summary>
    ///  Called for each Ohlcv Msg.
    /// </summary>
    /// <param name="message">The received Ohlcv Msg.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnOhlcvMsg(OhlcvMsg message, RecordFrame frame) { }

    /// <summary>
    ///  Called for each Status Msg.
    /// </summary>
    /// <param name="message">The received Status Msg.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnStatusMsg(StatusMsg message, RecordFrame frame) { }

    /// <summary>
    ///  Called for each Instrument Def Msg.
    /// </summary>
    /// <param name="message">The received Instrument Def Msg.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnInstrumentDefMsg(InstrumentDefMsg message, RecordFrame frame) { }

    /// <summary>
    ///  Called for each Imbalance Msg.
    /// </summary>
    /// <param name="message">The received Imbalance Msg.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnImbalanceMsg(ImbalanceMsg message, RecordFrame frame) { }

    /// <summary>
    ///  Called for each Error Msg.
    /// </summary>
    /// <param name="message">The received Error Msg.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnErrorMsg(ErrorMsg message, RecordFrame frame) { }

    /// <summary>
    ///  Called for each Symbol Mapping Msg.
    /// </summary>
    /// <param name="message">The received Symbol Mapping Msg.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnSymbolMappingMsg(SymbolMappingMsg message, RecordFrame frame) { }

    /// <summary>
    ///  Called for each System Msg.
    /// </summary>
    /// <param name="message">The received System Msg.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnSystemMsg(SystemMsg message, RecordFrame frame) { }

    /// <summary>
    ///  Called for each Stat Msg.
    /// </summary>
    /// <param name="message">The received Stat Msg.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnStatMsg(StatMsg message, RecordFrame frame) { }

    /// <summary>
    ///  Called for each Mbo Msg.
    /// </summary>
    /// <param name="message">The received Mbo Msg.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnMboMsg(MboMsg message, RecordFrame frame) { }

    /// <summary>
    ///  Called for each Cmbp 1 Msg.
    /// </summary>
    /// <param name="message">The received Cmbp 1 Msg.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnCmbp1Msg(Cmbp1Msg message, RecordFrame frame) { }

    /// <summary>
    ///  Called for each Cbbo Msg.
    /// </summary>
    /// <param name="message">The received Cbbo Msg.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnCbboMsg(CbboMsg message, RecordFrame frame) { }

    /// <summary>
    ///  Called for each Bbo Msg.
    /// </summary>
    /// <param name="message">The received Bbo Msg.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnBboMsg(BboMsg message, RecordFrame frame) { }

    /// <summary>
    ///  Called for a message whose message type the protocol does not define.
    /// </summary>
    /// <param name="message">The received message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnUnknownFileMessage(UnknownFileMessage message, RecordFrame frame) { }
}
