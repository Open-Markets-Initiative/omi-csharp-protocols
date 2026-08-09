namespace Iex.IexEquities.DeepPlus.IexTp.Manager;

/// <summary>
///  Consumer-fillable partial manager: iterates over packet messages and dispatches
///  each to a typed <c>partial void On{Msg}</c> hook. Unimplemented hooks elide at zero cost.
/// </summary>
public sealed partial class IexIexEquitiesDeepPlusIexTpManager
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
            case AddOrderMessage msg:
                OnAddOrderMessage(msg, seq);
                break;
            case ClearBookMessage msg:
                OnClearBookMessage(msg, seq);
                break;
            case OperationalHaltStatusMessage msg:
                OnOperationalHaltStatusMessage(msg, seq);
                break;
            case OrderDeleteMessage msg:
                OnOrderDeleteMessage(msg, seq);
                break;
            case OrderExecutedMessage msg:
                OnOrderExecutedMessage(msg, seq);
                break;
            case OrderModifyMessage msg:
                OnOrderModifyMessage(msg, seq);
                break;
            case RetailLiquidityIndicatorMessage msg:
                OnRetailLiquidityIndicatorMessage(msg, seq);
                break;
            case SecurityDirectoryMessage msg:
                OnSecurityDirectoryMessage(msg, seq);
                break;
            case SecurityEventMessage msg:
                OnSecurityEventMessage(msg, seq);
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
            case TradeMessage msg:
                OnTradeMessage(msg, seq);
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
    ///  Called for each <c>AddOrderMessage</c> message. Add handler logic in the implementing
    ///  partial. Unimplemented partials are elided by the compiler.
    /// </summary>
    partial void OnAddOrderMessage(AddOrderMessage msg, int seq);

    /// <summary>
    ///  Called for each <c>ClearBookMessage</c> message. Add handler logic in the implementing
    ///  partial. Unimplemented partials are elided by the compiler.
    /// </summary>
    partial void OnClearBookMessage(ClearBookMessage msg, int seq);

    /// <summary>
    ///  Called for each <c>OperationalHaltStatusMessage</c> message. Add handler logic in the implementing
    ///  partial. Unimplemented partials are elided by the compiler.
    /// </summary>
    partial void OnOperationalHaltStatusMessage(OperationalHaltStatusMessage msg, int seq);

    /// <summary>
    ///  Called for each <c>OrderDeleteMessage</c> message. Add handler logic in the implementing
    ///  partial. Unimplemented partials are elided by the compiler.
    /// </summary>
    partial void OnOrderDeleteMessage(OrderDeleteMessage msg, int seq);

    /// <summary>
    ///  Called for each <c>OrderExecutedMessage</c> message. Add handler logic in the implementing
    ///  partial. Unimplemented partials are elided by the compiler.
    /// </summary>
    partial void OnOrderExecutedMessage(OrderExecutedMessage msg, int seq);

    /// <summary>
    ///  Called for each <c>OrderModifyMessage</c> message. Add handler logic in the implementing
    ///  partial. Unimplemented partials are elided by the compiler.
    /// </summary>
    partial void OnOrderModifyMessage(OrderModifyMessage msg, int seq);

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
    ///  Called for each <c>SecurityEventMessage</c> message. Add handler logic in the implementing
    ///  partial. Unimplemented partials are elided by the compiler.
    /// </summary>
    partial void OnSecurityEventMessage(SecurityEventMessage msg, int seq);

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
    ///  Called for each <c>TradeMessage</c> message. Add handler logic in the implementing
    ///  partial. Unimplemented partials are elided by the compiler.
    /// </summary>
    partial void OnTradeMessage(TradeMessage msg, int seq);

    /// <summary>
    ///  Called for each <c>TradingStatusMessage</c> message. Add handler logic in the implementing
    ///  partial. Unimplemented partials are elided by the compiler.
    /// </summary>
    partial void OnTradingStatusMessage(TradingStatusMessage msg, int seq);

}
