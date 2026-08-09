using Iex.IexEquities.Deep.IexTp;
using System.Runtime.InteropServices;
using Pcap.CSharp;
using Iex.IexEquities.Deep.IexTp.Testing;
using static Iex.IexEquities.Deep.IexTp.Testing.TestHarness;

return Run<MessageCode>(args, "Iex.IexEquities.Deep.IexTp.v1.08.Test", ReadMessages, PrintMessage);

static void PrintMessage(MessageCode type, ReadOnlySpan<byte> payload, int seq)
{
    switch (type)
    {
        case MessageCode.SystemEventMessage:
        {
            var msg = MemoryMarshal.Read<SystemEventMessage.Layout>(payload);
            Console.WriteLine($"[{seq}] SystemEvent: systemEvent={msg.SystemEvent.Value,-8} timestamp={FormatTs(msg.Timestamp.Value)}");
            break;
        }
        case MessageCode.SecurityDirectoryMessage:
        {
            var msg = MemoryMarshal.Read<SecurityDirectoryMessage.Layout>(payload);
            Console.WriteLine($"[{seq}] SecurityDirectory: timestamp={FormatTs(msg.Timestamp.Value)} symbol={msg.Symbol.Value,-8} roundLotSize={msg.RoundLotSize.Value} adjustedPocPrice={msg.AdjustedPocPrice.Value,10}");
            break;
        }
        case MessageCode.TradingStatusMessage:
        {
            var msg = MemoryMarshal.Read<TradingStatusMessage.Layout>(payload);
            Console.WriteLine($"[{seq}] TradingStatus: tradingStatus={msg.TradingStatus.Value,-8} timestamp={FormatTs(msg.Timestamp.Value)} symbol={msg.Symbol.Value,-8} reason={msg.Reason.Value,-8}");
            break;
        }
        case MessageCode.RetailLiquidityIndicatorMessage:
        {
            var msg = MemoryMarshal.Read<RetailLiquidityIndicatorMessage.Layout>(payload);
            Console.WriteLine($"[{seq}] RetailLiquidityIndicator: retailLiquidityIndicator={msg.RetailLiquidityIndicator.Value,-8} timestamp={FormatTs(msg.Timestamp.Value)} symbol={msg.Symbol.Value,-8}");
            break;
        }
        case MessageCode.OperationalHaltStatusMessage:
        {
            var msg = MemoryMarshal.Read<OperationalHaltStatusMessage.Layout>(payload);
            Console.WriteLine($"[{seq}] OperationalHaltStatus: operationalHaltStatus={msg.OperationalHaltStatus.Value,-8} timestamp={FormatTs(msg.Timestamp.Value)} symbol={msg.Symbol.Value,-8}");
            break;
        }
        case MessageCode.ShortSalePriceTestStatusMessage:
        {
            var msg = MemoryMarshal.Read<ShortSalePriceTestStatusMessage.Layout>(payload);
            Console.WriteLine($"[{seq}] ShortSalePriceTestStatus: shortSalePriceTestStatus={msg.ShortSalePriceTestStatus} timestamp={FormatTs(msg.Timestamp.Value)} symbol={msg.Symbol.Value,-8} detail={msg.Detail.Value,-8}");
            break;
        }
        case MessageCode.SecurityEventMessage:
        {
            var msg = MemoryMarshal.Read<SecurityEventMessage.Layout>(payload);
            Console.WriteLine($"[{seq}] SecurityEvent: securityEvent={msg.SecurityEvent.Value,-8} timestamp={FormatTs(msg.Timestamp.Value)} symbol={msg.Symbol.Value,-8}");
            break;
        }
        case MessageCode.PriceLevelBuyUpdateMessage:
        {
            var msg = MemoryMarshal.Read<PriceLevelBuyUpdateMessage.Layout>(payload);
            Console.WriteLine($"[{seq}] PriceLevelBuyUpdate: eventFlags={msg.EventFlags} timestamp={FormatTs(msg.Timestamp.Value)} symbol={msg.Symbol.Value,-8} price={msg.Price.Value,10}");
            break;
        }
        case MessageCode.PriceLevelSellUpdateMessage:
        {
            var msg = MemoryMarshal.Read<PriceLevelSellUpdateMessage.Layout>(payload);
            Console.WriteLine($"[{seq}] PriceLevelSellUpdate: eventFlags={msg.EventFlags} timestamp={FormatTs(msg.Timestamp.Value)} symbol={msg.Symbol.Value,-8} price={msg.Price.Value,10}");
            break;
        }
        case MessageCode.TradeReportMessage:
        {
            var msg = MemoryMarshal.Read<TradeReportMessage.Layout>(payload);
            Console.WriteLine($"[{seq}] TradeReport: timestamp={FormatTs(msg.Timestamp.Value)} symbol={msg.Symbol.Value,-8} size={msg.Size.Value} price={msg.Price.Value,10}");
            break;
        }
        case MessageCode.OfficialPriceMessage:
        {
            var msg = MemoryMarshal.Read<OfficialPriceMessage.Layout>(payload);
            Console.WriteLine($"[{seq}] OfficialPrice: priceType={msg.PriceType.Value,-8} timestamp={FormatTs(msg.Timestamp.Value)} symbol={msg.Symbol.Value,-8} officialPrice={msg.OfficialPrice.Value,10}");
            break;
        }
        case MessageCode.TradeBreakMessage:
        {
            var msg = MemoryMarshal.Read<TradeBreakMessage.Layout>(payload);
            Console.WriteLine($"[{seq}] TradeBreak: timestamp={FormatTs(msg.Timestamp.Value)} symbol={msg.Symbol.Value,-8} size={msg.Size.Value} price={msg.Price.Value,10}");
            break;
        }
        case MessageCode.AuctionInformationMessage:
        {
            var msg = MemoryMarshal.Read<AuctionInformationMessage.Layout>(payload);
            Console.WriteLine($"[{seq}] AuctionInformation: auctionType={msg.AuctionType.Value,-8} timestamp={FormatTs(msg.Timestamp.Value)} symbol={msg.Symbol.Value,-8} scheduledAuctionTime={FormatTs(msg.ScheduledAuctionTime.Value)}");
            break;
        }
        default:
            Console.WriteLine($"[{seq}] Unknown: type={type} (0x{(ulong)type:X})");
            break;
    }
}

static IEnumerable<ProtocolMessage<MessageCode>> ReadMessages(string path)
{
    foreach (var frame in PcapReader.ReadPackets(path))
    {
        if (!NetworkHeaders.TryGetUdpPayloadOffset(frame.Span, out var transportOffset))
            continue;

        foreach (var message in WalkFrame(frame, transportOffset))
            yield return message;
    }
}

static unsafe List<ProtocolMessage<MessageCode>> WalkFrame(ReadOnlyMemory<byte> frame, int transportOffset)
{
    var messages = new List<ProtocolMessage<MessageCode>>();
    var span = frame.Span;
    fixed (byte* framePtr = span)
    {
        foreach (var message in new MessageWalker(framePtr + transportOffset, span.Length - transportOffset))
        {
            var payloadOffset = (int)(message.Payload - framePtr);
            messages.Add(new ProtocolMessage<MessageCode>(message.Type, frame.Slice(payloadOffset, message.PayloadLength)));
        }
    }
    return messages;
}
