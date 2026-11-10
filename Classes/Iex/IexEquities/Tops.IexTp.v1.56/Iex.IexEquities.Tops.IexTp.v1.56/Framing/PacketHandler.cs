namespace Iex.IexEquities.Tops.IexTp.v1_56;

/// <summary>
///  Handles each Packet and the messages that it carries.
/// </summary>
public abstract class PacketHandler
{
    /// <summary>
    ///  Gets the packet that the handler is handling.
    /// </summary>
    public Packet Packet { get; private set; } = null!;

    /// <summary>
    ///  Calls the handler method for each message, and for each entry or packet of a kind that carries none.
    /// </summary>
    /// <remarks>
    ///  This method does not check whether the packet is valid.
    /// </remarks>
    public void Handle(Packet packet)
    {
        Packet = packet;
        if (packet.MessagesKind == MessagesKind.Heartbeat) OnHeartbeat(packet);
        if (packet.MessagesKind == MessagesKind.Message)
        {
            foreach (var entry in packet.MessageFrames)
            {
                Visit(entry);
            }
        }
    }

    private void Visit(MessageFrame frame)
    {
        if (frame.MessageData is { } messageDataReceived) Handle(messageDataReceived, frame);
    }

    private void Handle(IMessage message, MessageFrame frame)
    {
        switch (message)
        {
            case QuoteUpdateMessage received:
                OnQuoteUpdateMessage(received, frame);
                break;
            case TradeReportMessage received:
                OnTradeReportMessage(received, frame);
                break;
            case TradeBreakMessage received:
                OnTradeBreakMessage(received, frame);
                break;
            case UnknownPacketMessage received:
                OnUnknownPacketMessage(received, frame);
                break;
        }
    }

    /// <summary>
    ///  Called for each Quote Update Message.
    /// </summary>
    /// <param name="message">The received Quote Update Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnQuoteUpdateMessage(QuoteUpdateMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Trade Report Message.
    /// </summary>
    /// <param name="message">The received Trade Report Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnTradeReportMessage(TradeReportMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Trade Break Message.
    /// </summary>
    /// <param name="message">The received Trade Break Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnTradeBreakMessage(TradeBreakMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for a message whose message type the protocol does not define.
    /// </summary>
    /// <param name="message">The received message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnUnknownPacketMessage(UnknownPacketMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Heartbeat.
    /// </summary>
    /// <param name="packet">The received packet.</param>
    protected virtual void OnHeartbeat(Packet packet) { }
}
