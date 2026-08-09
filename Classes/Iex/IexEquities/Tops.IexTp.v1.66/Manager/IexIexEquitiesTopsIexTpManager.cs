namespace Iex.IexEquities.Tops.IexTp.Manager;

/// <summary>
///  Consumer-fillable partial manager: iterates over packet messages and dispatches
///  each to a typed <c>partial void On{Msg}</c> hook. Unimplemented hooks elide at zero cost.
/// </summary>
public sealed partial class IexIexEquitiesTopsIexTpManager
{
    /// <summary>
    ///  Per-message sequence counter. Increments once per dispatched message.
    /// </summary>
    private int seq;

    /// <summary>
    ///  Strips the transport header, parses the packet, and dispatches its messages.
    ///  Silently returns if the frame does not carry a recognized transport payload.
    /// </summary>
    public void Process(ReadOnlyMemory<byte> frame)
    {
        if (!NetworkHeaders.TryGetUdpPayload(frame.Span, out var payload)) return;
        var packet = Packet.Parse(payload);
        Handle(packet);
    }

    /// <summary>
    ///  Iterates over parsed packet messages and dispatches each to its typed handler.
    /// </summary>
    public void Handle(Packet packet)
    {
        foreach (var message in packet.Messages)
        {
            var n = ++seq;
            Dispatch(message, n);
        }
    }

    /// <summary>
    ///  Routes a message to its typed partial handler.
    /// </summary>
    void Dispatch(IMessage message, int seq)
    {
        switch (message)
        {
            case AuctionInformationMessage msg:
                OnAuctionInformationMessage(msg, seq);
                break;
            case OfficialPriceMessage msg:
                OnOfficialPriceMessage(msg, seq);
                break;
            case OperationalHaltStatusMessage msg:
                OnOperationalHaltStatusMessage(msg, seq);
                break;
            case QuoteUpdateMessage msg:
                OnQuoteUpdateMessage(msg, seq);
                break;
            case RetailLiquidityIndicatorMessage msg:
                OnRetailLiquidityIndicatorMessage(msg, seq);
                break;
            case SecurityDirectoryMessage msg:
                OnSecurityDirectoryMessage(msg, seq);
                break;
            case ShortSalePriceTestStatusMessage msg:
                OnShortSalePriceTestStatusMessage(msg, seq);
                break;
            case SystemEventMessage msg:
                OnSystemEventMessage(msg, seq);
                break;
            case TradeBreakMessage msg:
                OnTradeBreakMessage(msg, seq);
                break;
            case TradeReportMessage msg:
                OnTradeReportMessage(msg, seq);
                break;
            case TradingStatusMessage msg:
                OnTradingStatusMessage(msg, seq);
                break;
            default:
                Console.WriteLine($"[seq={seq}] Unhandled message type {message.GetType().Name}");
                break;
        }
    }

    // ── Partial hooks — implement these in your partial class ──────────────────

    /// <summary>
    ///  Called for each <c>AuctionInformationMessage</c> message. Add handler logic in the implementing
    ///  partial. Unimplemented partials are elided by the compiler.
    /// </summary>
    partial void OnAuctionInformationMessage(AuctionInformationMessage msg, int seq);

    /// <summary>
    ///  Called for each <c>OfficialPriceMessage</c> message. Add handler logic in the implementing
    ///  partial. Unimplemented partials are elided by the compiler.
    /// </summary>
    partial void OnOfficialPriceMessage(OfficialPriceMessage msg, int seq);

    /// <summary>
    ///  Called for each <c>OperationalHaltStatusMessage</c> message. Add handler logic in the implementing
    ///  partial. Unimplemented partials are elided by the compiler.
    /// </summary>
    partial void OnOperationalHaltStatusMessage(OperationalHaltStatusMessage msg, int seq);

    /// <summary>
    ///  Called for each <c>QuoteUpdateMessage</c> message. Add handler logic in the implementing
    ///  partial. Unimplemented partials are elided by the compiler.
    /// </summary>
    partial void OnQuoteUpdateMessage(QuoteUpdateMessage msg, int seq);

    /// <summary>
    ///  Called for each <c>RetailLiquidityIndicatorMessage</c> message. Add handler logic in the implementing
    ///  partial. Unimplemented partials are elided by the compiler.
    /// </summary>
    partial void OnRetailLiquidityIndicatorMessage(RetailLiquidityIndicatorMessage msg, int seq);

    /// <summary>
    ///  Called for each <c>SecurityDirectoryMessage</c> message. Add handler logic in the implementing
    ///  partial. Unimplemented partials are elided by the compiler.
    /// </summary>
    partial void OnSecurityDirectoryMessage(SecurityDirectoryMessage msg, int seq);

    /// <summary>
    ///  Called for each <c>ShortSalePriceTestStatusMessage</c> message. Add handler logic in the implementing
    ///  partial. Unimplemented partials are elided by the compiler.
    /// </summary>
    partial void OnShortSalePriceTestStatusMessage(ShortSalePriceTestStatusMessage msg, int seq);

    /// <summary>
    ///  Called for each <c>SystemEventMessage</c> message. Add handler logic in the implementing
    ///  partial. Unimplemented partials are elided by the compiler.
    /// </summary>
    partial void OnSystemEventMessage(SystemEventMessage msg, int seq);

    /// <summary>
    ///  Called for each <c>TradeBreakMessage</c> message. Add handler logic in the implementing
    ///  partial. Unimplemented partials are elided by the compiler.
    /// </summary>
    partial void OnTradeBreakMessage(TradeBreakMessage msg, int seq);

    /// <summary>
    ///  Called for each <c>TradeReportMessage</c> message. Add handler logic in the implementing
    ///  partial. Unimplemented partials are elided by the compiler.
    /// </summary>
    partial void OnTradeReportMessage(TradeReportMessage msg, int seq);

    /// <summary>
    ///  Called for each <c>TradingStatusMessage</c> message. Add handler logic in the implementing
    ///  partial. Unimplemented partials are elided by the compiler.
    /// </summary>
    partial void OnTradingStatusMessage(TradingStatusMessage msg, int seq);

}
