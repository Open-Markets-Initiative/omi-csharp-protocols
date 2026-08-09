namespace Iex.IexEquities.DeepPlus.IexTp.Manager;

using global::Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
///  Generated manager dispatch: routes framed messages to typed partial handlers.
/// </summary>
public sealed partial class IexIexEquitiesDeepPlusIexTpManager
{
    /// <summary>
    ///  Per-message sequence counter. Placeholder until the model-derived sequence
    ///  number is available. Increments once per dispatched message.
    /// </summary>
    private int seq;

    /// <summary>
    ///  Strips the transport header, walks the framing layer, and dispatches each message.
    /// </summary>
    public unsafe void Handle(ReadOnlySpan<byte> frame, int transportOffset)
    {
        fixed (byte* framePtr = frame)
        {
            var frameLength = frame.Length;
            foreach (var message in new MessageWalker(framePtr + transportOffset, frameLength - transportOffset))
            {
                var type = message.Type;
                var n = ++seq;

                switch (type)
                {
                    case MessageCode.SystemEventMessage:
                        OnSystemEventMessage(framePtr, frameLength, transportOffset, message.Payload, message.PayloadLength, n);
                        break;
                    case MessageCode.SecurityDirectoryMessage:
                        OnSecurityDirectoryMessage(framePtr, frameLength, transportOffset, message.Payload, message.PayloadLength, n);
                        break;
                    case MessageCode.TradingStatusMessage:
                        OnTradingStatusMessage(framePtr, frameLength, transportOffset, message.Payload, message.PayloadLength, n);
                        break;
                    case MessageCode.RetailLiquidityIndicatorMessage:
                        OnRetailLiquidityIndicatorMessage(framePtr, frameLength, transportOffset, message.Payload, message.PayloadLength, n);
                        break;
                    case MessageCode.OperationalHaltStatusMessage:
                        OnOperationalHaltStatusMessage(framePtr, frameLength, transportOffset, message.Payload, message.PayloadLength, n);
                        break;
                    case MessageCode.ShortSalePriceTestStatusMessage:
                        OnShortSalePriceTestStatusMessage(framePtr, frameLength, transportOffset, message.Payload, message.PayloadLength, n);
                        break;
                    case MessageCode.SecurityEventMessage:
                        OnSecurityEventMessage(framePtr, frameLength, transportOffset, message.Payload, message.PayloadLength, n);
                        break;
                    case MessageCode.AddOrderMessage:
                        OnAddOrderMessage(framePtr, frameLength, transportOffset, message.Payload, message.PayloadLength, n);
                        break;
                    case MessageCode.OrderModifyMessage:
                        OnOrderModifyMessage(framePtr, frameLength, transportOffset, message.Payload, message.PayloadLength, n);
                        break;
                    case MessageCode.OrderDeleteMessage:
                        OnOrderDeleteMessage(framePtr, frameLength, transportOffset, message.Payload, message.PayloadLength, n);
                        break;
                    case MessageCode.OrderExecutedMessage:
                        OnOrderExecutedMessage(framePtr, frameLength, transportOffset, message.Payload, message.PayloadLength, n);
                        break;
                    case MessageCode.TradeMessage:
                        OnTradeMessage(framePtr, frameLength, transportOffset, message.Payload, message.PayloadLength, n);
                        break;
                    case MessageCode.TradeBreakMessage:
                        OnTradeBreakMessage(framePtr, frameLength, transportOffset, message.Payload, message.PayloadLength, n);
                        break;
                    case MessageCode.ClearBookMessage:
                        OnClearBookMessage(framePtr, frameLength, transportOffset, message.Payload, message.PayloadLength, n);
                        break;
                    default:
                        Console.WriteLine($"[seq={n}] Unhandled message type '{type}' (0x{(byte)type:X2})");
                        break;
                }
            }
        }
    }

    /// <summary>
    ///  Handles each <c>SystemEventMessage</c> message.
    ///  Pointers target the pinned frame and are valid only during the call.
    /// </summary>
    unsafe partial void OnSystemEventMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq);

    /// <summary>
    ///  Handles each <c>SecurityDirectoryMessage</c> message.
    ///  Pointers target the pinned frame and are valid only during the call.
    /// </summary>
    unsafe partial void OnSecurityDirectoryMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq);

    /// <summary>
    ///  Handles each <c>TradingStatusMessage</c> message.
    ///  Pointers target the pinned frame and are valid only during the call.
    /// </summary>
    unsafe partial void OnTradingStatusMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq);

    /// <summary>
    ///  Handles each <c>RetailLiquidityIndicatorMessage</c> message.
    ///  Pointers target the pinned frame and are valid only during the call.
    /// </summary>
    unsafe partial void OnRetailLiquidityIndicatorMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq);

    /// <summary>
    ///  Handles each <c>OperationalHaltStatusMessage</c> message.
    ///  Pointers target the pinned frame and are valid only during the call.
    /// </summary>
    unsafe partial void OnOperationalHaltStatusMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq);

    /// <summary>
    ///  Handles each <c>ShortSalePriceTestStatusMessage</c> message.
    ///  Pointers target the pinned frame and are valid only during the call.
    /// </summary>
    unsafe partial void OnShortSalePriceTestStatusMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq);

    /// <summary>
    ///  Handles each <c>SecurityEventMessage</c> message.
    ///  Pointers target the pinned frame and are valid only during the call.
    /// </summary>
    unsafe partial void OnSecurityEventMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq);

    /// <summary>
    ///  Handles each <c>AddOrderMessage</c> message.
    ///  Pointers target the pinned frame and are valid only during the call.
    /// </summary>
    unsafe partial void OnAddOrderMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq);

    /// <summary>
    ///  Handles each <c>OrderModifyMessage</c> message.
    ///  Pointers target the pinned frame and are valid only during the call.
    /// </summary>
    unsafe partial void OnOrderModifyMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq);

    /// <summary>
    ///  Handles each <c>OrderDeleteMessage</c> message.
    ///  Pointers target the pinned frame and are valid only during the call.
    /// </summary>
    unsafe partial void OnOrderDeleteMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq);

    /// <summary>
    ///  Handles each <c>OrderExecutedMessage</c> message.
    ///  Pointers target the pinned frame and are valid only during the call.
    /// </summary>
    unsafe partial void OnOrderExecutedMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq);

    /// <summary>
    ///  Handles each <c>TradeMessage</c> message.
    ///  Pointers target the pinned frame and are valid only during the call.
    /// </summary>
    unsafe partial void OnTradeMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq);

    /// <summary>
    ///  Handles each <c>TradeBreakMessage</c> message.
    ///  Pointers target the pinned frame and are valid only during the call.
    /// </summary>
    unsafe partial void OnTradeBreakMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq);

    /// <summary>
    ///  Handles each <c>ClearBookMessage</c> message.
    ///  Pointers target the pinned frame and are valid only during the call.
    /// </summary>
    unsafe partial void OnClearBookMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq);

}
