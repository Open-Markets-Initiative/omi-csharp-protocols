namespace Iex.IexEquities.DeepPlus.IexTp.Manager;

using global::Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
///  Generated manager dispatch: routes framed messages to typed partial handlers.
/// </summary>
public sealed partial class IexIexEquitiesDeepPlusIexTpManager
{
    /// <summary>
    ///  Sequence number for dispatched messages.
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
    ///  Handles a <c>SystemEventMessage</c> message.
    ///  The pointer is valid only during this call.
    /// </summary>
    unsafe partial void OnSystemEventMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq);

    /// <summary>
    ///  Handles a <c>SecurityDirectoryMessage</c> message.
    ///  The pointer is valid only during this call.
    /// </summary>
    unsafe partial void OnSecurityDirectoryMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq);

    /// <summary>
    ///  Handles a <c>TradingStatusMessage</c> message.
    ///  The pointer is valid only during this call.
    /// </summary>
    unsafe partial void OnTradingStatusMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq);

    /// <summary>
    ///  Handles a <c>RetailLiquidityIndicatorMessage</c> message.
    ///  The pointer is valid only during this call.
    /// </summary>
    unsafe partial void OnRetailLiquidityIndicatorMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq);

    /// <summary>
    ///  Handles a <c>OperationalHaltStatusMessage</c> message.
    ///  The pointer is valid only during this call.
    /// </summary>
    unsafe partial void OnOperationalHaltStatusMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq);

    /// <summary>
    ///  Handles a <c>ShortSalePriceTestStatusMessage</c> message.
    ///  The pointer is valid only during this call.
    /// </summary>
    unsafe partial void OnShortSalePriceTestStatusMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq);

    /// <summary>
    ///  Handles a <c>SecurityEventMessage</c> message.
    ///  The pointer is valid only during this call.
    /// </summary>
    unsafe partial void OnSecurityEventMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq);

    /// <summary>
    ///  Handles a <c>AddOrderMessage</c> message.
    ///  The pointer is valid only during this call.
    /// </summary>
    unsafe partial void OnAddOrderMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq);

    /// <summary>
    ///  Handles a <c>OrderModifyMessage</c> message.
    ///  The pointer is valid only during this call.
    /// </summary>
    unsafe partial void OnOrderModifyMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq);

    /// <summary>
    ///  Handles a <c>OrderDeleteMessage</c> message.
    ///  The pointer is valid only during this call.
    /// </summary>
    unsafe partial void OnOrderDeleteMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq);

    /// <summary>
    ///  Handles a <c>OrderExecutedMessage</c> message.
    ///  The pointer is valid only during this call.
    /// </summary>
    unsafe partial void OnOrderExecutedMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq);

    /// <summary>
    ///  Handles a <c>TradeMessage</c> message.
    ///  The pointer is valid only during this call.
    /// </summary>
    unsafe partial void OnTradeMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq);

    /// <summary>
    ///  Handles a <c>TradeBreakMessage</c> message.
    ///  The pointer is valid only during this call.
    /// </summary>
    unsafe partial void OnTradeBreakMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq);

    /// <summary>
    ///  Handles a <c>ClearBookMessage</c> message.
    ///  The pointer is valid only during this call.
    /// </summary>
    unsafe partial void OnClearBookMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq);

}
