namespace Iex.IexEquities.Deep.IexTp.ConsoleDumpManager;

/// <summary>
///  Generated example consumer: iterates over parsed packet messages and dumps a full formatted representation to the console.
/// </summary>
public sealed class ConsoleDumpManager
{
    /// <summary>
    ///  Per-packet sequence counter. Increments once per processed frame.
    /// </summary>
    private int packetSeq;

    /// <summary>
    ///  Per-message sequence counter. Increments once per dispatched message.
    /// </summary>
    private int messageSeq;

    /// <summary>
    ///  Strips the transport header, parses the packet, and dumps it to the console.
    ///  Silently returns if the frame does not carry a recognized transport payload.
    /// </summary>
    public void Process(ReadOnlyMemory<byte> frame)
    {
        if (!NetworkHeaders.TryGetUdpPayload(frame.Span, out var payload)) return;
        var packet = Packet.Parse(payload);
        Dump(packet, ++packetSeq, ref messageSeq);
    }

    /// <summary>
    ///  Dumps a parsed packet and all its decoded messages to the console.
    /// </summary>
    private void Dump(Packet packet, int packetSeq, ref int messageSeq)
    {
        if (!packet.IsValid)
        {
            Console.WriteLine($"[packet={packetSeq}] invalid packet");
            return;
        }
        Console.WriteLine($"[packet={packetSeq}] valid={packet.IsValid} modelCount={packet.MessageCount} decoded={packet.Messages.Count}");
        Console.Write(packet.Header.ToFormattedString(new PrintOptions { IndentDepth = 1 }));
        if (packet.Messages.UnknownMessageCount > 0)
            Console.WriteLine($"[packet={packetSeq}] WARNING: dropped {packet.Messages.UnknownMessageCount} message(s) with an unknown type code");

        foreach (var message in packet.Messages)
            Dump(message, ++messageSeq);
    }

    /// <summary>
    ///  Routes a message to its typed dump method.
    /// </summary>
    public void Dump(IMessage message, int seq)
    {
        switch (message)
        {
            case AuctionInformationMessage msg:
                DumpAuctionInformationMessage(msg, seq);
                break;
            case OfficialPriceMessage msg:
                DumpOfficialPriceMessage(msg, seq);
                break;
            case OperationalHaltStatusMessage msg:
                DumpOperationalHaltStatusMessage(msg, seq);
                break;
            case PriceLevelBuyUpdateMessage msg:
                DumpPriceLevelBuyUpdateMessage(msg, seq);
                break;
            case PriceLevelSellUpdateMessage msg:
                DumpPriceLevelSellUpdateMessage(msg, seq);
                break;
            case RetailLiquidityIndicatorMessage msg:
                DumpRetailLiquidityIndicatorMessage(msg, seq);
                break;
            case SecurityDirectoryMessage msg:
                DumpSecurityDirectoryMessage(msg, seq);
                break;
            case SecurityEventMessage msg:
                DumpSecurityEventMessage(msg, seq);
                break;
            case ShortSalePriceTestStatusMessage msg:
                DumpShortSalePriceTestStatusMessage(msg, seq);
                break;
            case SystemEventMessage msg:
                DumpSystemEventMessage(msg, seq);
                break;
            case TradeBreakMessage msg:
                DumpTradeBreakMessage(msg, seq);
                break;
            case TradeReportMessage msg:
                DumpTradeReportMessage(msg, seq);
                break;
            case TradingStatusMessage msg:
                DumpTradingStatusMessage(msg, seq);
                break;
            default:
                DumpUnhandled(message, seq);
                break;
        }
    }

    /// <summary>
    ///  Dumps a AuctionInformationMessage to the console.
    /// </summary>
    static void DumpAuctionInformationMessage(AuctionInformationMessage msg, int seq)
    {
        Console.WriteLine($"[seq={seq}] AuctionInformationMessage");
        Console.Write(msg.ToFormattedString(new PrintOptions { IndentDepth = 1 }));
    }

    /// <summary>
    ///  Dumps a OfficialPriceMessage to the console.
    /// </summary>
    static void DumpOfficialPriceMessage(OfficialPriceMessage msg, int seq)
    {
        Console.WriteLine($"[seq={seq}] OfficialPriceMessage");
        Console.Write(msg.ToFormattedString(new PrintOptions { IndentDepth = 1 }));
    }

    /// <summary>
    ///  Dumps a OperationalHaltStatusMessage to the console.
    /// </summary>
    static void DumpOperationalHaltStatusMessage(OperationalHaltStatusMessage msg, int seq)
    {
        Console.WriteLine($"[seq={seq}] OperationalHaltStatusMessage");
        Console.Write(msg.ToFormattedString(new PrintOptions { IndentDepth = 1 }));
    }

    /// <summary>
    ///  Dumps a PriceLevelBuyUpdateMessage to the console.
    /// </summary>
    static void DumpPriceLevelBuyUpdateMessage(PriceLevelBuyUpdateMessage msg, int seq)
    {
        Console.WriteLine($"[seq={seq}] PriceLevelBuyUpdateMessage");
        Console.Write(msg.ToFormattedString(new PrintOptions { IndentDepth = 1 }));
    }

    /// <summary>
    ///  Dumps a PriceLevelSellUpdateMessage to the console.
    /// </summary>
    static void DumpPriceLevelSellUpdateMessage(PriceLevelSellUpdateMessage msg, int seq)
    {
        Console.WriteLine($"[seq={seq}] PriceLevelSellUpdateMessage");
        Console.Write(msg.ToFormattedString(new PrintOptions { IndentDepth = 1 }));
    }

    /// <summary>
    ///  Dumps a RetailLiquidityIndicatorMessage to the console.
    /// </summary>
    static void DumpRetailLiquidityIndicatorMessage(RetailLiquidityIndicatorMessage msg, int seq)
    {
        Console.WriteLine($"[seq={seq}] RetailLiquidityIndicatorMessage");
        Console.Write(msg.ToFormattedString(new PrintOptions { IndentDepth = 1 }));
    }

    /// <summary>
    ///  Dumps a SecurityDirectoryMessage to the console.
    /// </summary>
    static void DumpSecurityDirectoryMessage(SecurityDirectoryMessage msg, int seq)
    {
        Console.WriteLine($"[seq={seq}] SecurityDirectoryMessage");
        Console.Write(msg.ToFormattedString(new PrintOptions { IndentDepth = 1 }));
    }

    /// <summary>
    ///  Dumps a SecurityEventMessage to the console.
    /// </summary>
    static void DumpSecurityEventMessage(SecurityEventMessage msg, int seq)
    {
        Console.WriteLine($"[seq={seq}] SecurityEventMessage");
        Console.Write(msg.ToFormattedString(new PrintOptions { IndentDepth = 1 }));
    }

    /// <summary>
    ///  Dumps a ShortSalePriceTestStatusMessage to the console.
    /// </summary>
    static void DumpShortSalePriceTestStatusMessage(ShortSalePriceTestStatusMessage msg, int seq)
    {
        Console.WriteLine($"[seq={seq}] ShortSalePriceTestStatusMessage");
        Console.Write(msg.ToFormattedString(new PrintOptions { IndentDepth = 1 }));
    }

    /// <summary>
    ///  Dumps a SystemEventMessage to the console.
    /// </summary>
    static void DumpSystemEventMessage(SystemEventMessage msg, int seq)
    {
        Console.WriteLine($"[seq={seq}] SystemEventMessage");
        Console.Write(msg.ToFormattedString(new PrintOptions { IndentDepth = 1 }));
    }

    /// <summary>
    ///  Dumps a TradeBreakMessage to the console.
    /// </summary>
    static void DumpTradeBreakMessage(TradeBreakMessage msg, int seq)
    {
        Console.WriteLine($"[seq={seq}] TradeBreakMessage");
        Console.Write(msg.ToFormattedString(new PrintOptions { IndentDepth = 1 }));
    }

    /// <summary>
    ///  Dumps a TradeReportMessage to the console.
    /// </summary>
    static void DumpTradeReportMessage(TradeReportMessage msg, int seq)
    {
        Console.WriteLine($"[seq={seq}] TradeReportMessage");
        Console.Write(msg.ToFormattedString(new PrintOptions { IndentDepth = 1 }));
    }

    /// <summary>
    ///  Dumps a TradingStatusMessage to the console.
    /// </summary>
    static void DumpTradingStatusMessage(TradingStatusMessage msg, int seq)
    {
        Console.WriteLine($"[seq={seq}] TradingStatusMessage");
        Console.Write(msg.ToFormattedString(new PrintOptions { IndentDepth = 1 }));
    }

    /// <summary>
    ///  Dumps an unhandled message type to the console.
    ///  Should not fire in practice — the type-switch covers every generated IMessage implementation.
    /// </summary>
    static void DumpUnhandled(IMessage message, int seq)
    {
        Console.WriteLine($"[seq={seq}] Unhandled message type {message.GetType().Name}");
        Console.Write(message.ToFormattedString(new PrintOptions { IndentDepth = 1 }));
    }
}
