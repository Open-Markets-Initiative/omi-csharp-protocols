namespace Iex.IexEquities.Deep.IexTp.Manager;

using global::Iex.IexEquities.Deep.IexTp;

/// <summary>
///  Generated manager dispatch: routes framed messages to typed partial handlers.
/// </summary>
public sealed partial class IexIexEquitiesDeepIexTpManager
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
                    case MessageCode.OperationalHaltStatusMessage:
                        OnOperationalHaltStatusMessage(framePtr, frameLength, transportOffset, message.Payload, message.PayloadLength, n);
                        break;
                    case MessageCode.ShortSalePriceTestStatusMessage:
                        OnShortSalePriceTestStatusMessage(framePtr, frameLength, transportOffset, message.Payload, message.PayloadLength, n);
                        break;
                    case MessageCode.SecurityEventMessage:
                        OnSecurityEventMessage(framePtr, frameLength, transportOffset, message.Payload, message.PayloadLength, n);
                        break;
                    case MessageCode.PriceLevelBuyUpdateMessage:
                        OnPriceLevelBuyUpdateMessage(framePtr, frameLength, transportOffset, message.Payload, message.PayloadLength, n);
                        break;
                    case MessageCode.PriceLevelSellUpdateMessage:
                        OnPriceLevelSellUpdateMessage(framePtr, frameLength, transportOffset, message.Payload, message.PayloadLength, n);
                        break;
                    case MessageCode.TradeReportMessage:
                        OnTradeReportMessage(framePtr, frameLength, transportOffset, message.Payload, message.PayloadLength, n);
                        break;
                    case MessageCode.OfficialPriceMessage:
                        OnOfficialPriceMessage(framePtr, frameLength, transportOffset, message.Payload, message.PayloadLength, n);
                        break;
                    case MessageCode.TradeBreakMessage:
                        OnTradeBreakMessage(framePtr, frameLength, transportOffset, message.Payload, message.PayloadLength, n);
                        break;
                    case MessageCode.AuctionInformationMessage:
                        OnAuctionInformationMessage(framePtr, frameLength, transportOffset, message.Payload, message.PayloadLength, n);
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
    ///  Handles a <c>PriceLevelBuyUpdateMessage</c> message.
    ///  The pointer is valid only during this call.
    /// </summary>
    unsafe partial void OnPriceLevelBuyUpdateMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq);

    /// <summary>
    ///  Handles a <c>PriceLevelSellUpdateMessage</c> message.
    ///  The pointer is valid only during this call.
    /// </summary>
    unsafe partial void OnPriceLevelSellUpdateMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq);

    /// <summary>
    ///  Handles a <c>TradeReportMessage</c> message.
    ///  The pointer is valid only during this call.
    /// </summary>
    unsafe partial void OnTradeReportMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq);

    /// <summary>
    ///  Handles a <c>OfficialPriceMessage</c> message.
    ///  The pointer is valid only during this call.
    /// </summary>
    unsafe partial void OnOfficialPriceMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq);

    /// <summary>
    ///  Handles a <c>TradeBreakMessage</c> message.
    ///  The pointer is valid only during this call.
    /// </summary>
    unsafe partial void OnTradeBreakMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq);

    /// <summary>
    ///  Handles a <c>AuctionInformationMessage</c> message.
    ///  The pointer is valid only during this call.
    /// </summary>
    unsafe partial void OnAuctionInformationMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq);

}
