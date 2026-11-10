using Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;
using Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Iterator;
using Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Test;
using static Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Test.Dump;

const int PcapArgument = 0;
const int FirstOption = 1;
const int None = 0;
const int OneFeed = 1;
const int Success = 0;
const int Failure = 1;
const int UsageFailure = 2;
const string FeedOption = "--feed";
const string StrictOption = "--strict";
const string Usage = $"usage: Nasdaq.IseOptions.OrderComboFeed.Itch.v1.1.Test <pcap> [{FeedOption} <name>] [{StrictOption}]";
const string PacketTree = "Packet";
const string PacketAlias = "Packet";

if (args.Length <= PcapArgument)
{
    global::System.Console.Error.WriteLine(Usage);
    return UsageFailure;
}

string? feed = null;
var strict = false;
for (var index = FirstOption; index < args.Length; index++)
{
    if (args[index] == StrictOption && !strict) strict = true;
    else if (args[index] == FeedOption && feed is null && index + 1 < args.Length) feed = args[++index];
    else
    {
        global::System.Console.Error.WriteLine(Usage);
        return UsageFailure;
    }
}

var feeds = None;
var packetCapture = Selects(PacketTree, PacketAlias) ? new PacketCapture() : null;
if (feed is not null && feeds != OneFeed)
{
    global::System.Console.Error.WriteLine($"Unknown or ambiguous feed '{feed}'.");
    global::System.Console.Error.WriteLine(Usage);
    return UsageFailure;
}

const byte Filler = 0xA5;
const int Following = 8;
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.AuctionResponse.Layout>() != global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.AuctionResponse.Layout.Size || Throws(() => { global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.AuctionResponse.Layout.Read(new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.AuctionResponse.Layout.Size]); }) || !Throws(() => { global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.AuctionResponse.Layout.Read(new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.AuctionResponse.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.AuctionResponse.Layout.Initialize(new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.AuctionResponse.Layout.Size]); }) || !Throws(() => { global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.AuctionResponse.Layout.Initialize(new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.AuctionResponse.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.AuctionResponse)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.AuctionResponse.Layout.Size];
    var filled = new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.AuctionResponse.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.AuctionResponse.Layout.Initialize(zeroed);
    global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.AuctionResponse.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.AuctionResponse.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.AuctionResponse.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.AuctionResponse)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyOrderOnBookMessage.Layout>() != global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyOrderOnBookMessage.Layout.LayoutSize || Throws(() => { global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyOrderOnBookMessage.Layout.Read(new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyOrderOnBookMessage.Layout.LayoutSize]); }) || !Throws(() => { global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyOrderOnBookMessage.Layout.Read(new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyOrderOnBookMessage.Layout.LayoutSize - 1]); }) || Throws(() => { global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyOrderOnBookMessage.Layout.Initialize(new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyOrderOnBookMessage.Layout.LayoutSize]); }) || !Throws(() => { global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyOrderOnBookMessage.Layout.Initialize(new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyOrderOnBookMessage.Layout.LayoutSize - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyOrderOnBookMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyOrderOnBookMessage.Layout.LayoutSize];
    var filled = new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyOrderOnBookMessage.Layout.LayoutSize + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyOrderOnBookMessage.Layout.Initialize(zeroed);
    global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyOrderOnBookMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyOrderOnBookMessage.Layout.LayoutSize).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyOrderOnBookMessage.Layout.LayoutSize).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyOrderOnBookMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyOpenClosedMessage.Layout>() != global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyOpenClosedMessage.Layout.Size || Throws(() => { global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyOpenClosedMessage.Layout.Read(new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyOpenClosedMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyOpenClosedMessage.Layout.Read(new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyOpenClosedMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyOpenClosedMessage.Layout.Initialize(new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyOpenClosedMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyOpenClosedMessage.Layout.Initialize(new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyOpenClosedMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyOpenClosedMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyOpenClosedMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyOpenClosedMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyOpenClosedMessage.Layout.Initialize(zeroed);
    global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyOpenClosedMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyOpenClosedMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyOpenClosedMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyOpenClosedMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyTradingActionMessage.Layout>() != global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyTradingActionMessage.Layout.Size || Throws(() => { global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyTradingActionMessage.Layout.Read(new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyTradingActionMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyTradingActionMessage.Layout.Read(new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyTradingActionMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyTradingActionMessage.Layout.Initialize(new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyTradingActionMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyTradingActionMessage.Layout.Initialize(new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyTradingActionMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyTradingActionMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyTradingActionMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyTradingActionMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyTradingActionMessage.Layout.Initialize(zeroed);
    global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyTradingActionMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyTradingActionMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyTradingActionMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyTradingActionMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.LegInformation.Layout>() != global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.LegInformation.Layout.Size || Throws(() => { global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.LegInformation.Layout.Read(new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.LegInformation.Layout.Size]); }) || !Throws(() => { global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.LegInformation.Layout.Read(new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.LegInformation.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.LegInformation.Layout.Initialize(new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.LegInformation.Layout.Size]); }) || !Throws(() => { global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.LegInformation.Layout.Initialize(new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.LegInformation.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.LegInformation)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.LegInformation.Layout.Size];
    var filled = new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.LegInformation.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.LegInformation.Layout.Initialize(zeroed);
    global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.LegInformation.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.LegInformation.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.LegInformation.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.LegInformation)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.SystemEventMessage.Layout>() != global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.SystemEventMessage.Layout.Size || Throws(() => { global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.SystemEventMessage.Layout.Read(new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.SystemEventMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.SystemEventMessage.Layout.Read(new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.SystemEventMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.SystemEventMessage.Layout.Initialize(new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.SystemEventMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.SystemEventMessage.Layout.Initialize(new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.SystemEventMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.SystemEventMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.SystemEventMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.SystemEventMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.SystemEventMessage.Layout.Initialize(zeroed);
    global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.SystemEventMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.SystemEventMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.SystemEventMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.SystemEventMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageHeader.Layout>() != global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageHeader.Layout.Size || Throws(() => { global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageHeader.Layout.Read(new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageHeader.Layout.Size]); }) || !Throws(() => { global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageHeader.Layout.Read(new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageHeader.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageHeader.Layout.Initialize(new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageHeader.Layout.Size]); }) || !Throws(() => { global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageHeader.Layout.Initialize(new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageHeader.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageHeader)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageHeader.Layout.Size];
    var filled = new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageHeader.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageHeader.Layout.Initialize(zeroed);
    global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageHeader.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageHeader.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageHeader.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageHeader)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.PacketHeader.Layout>() != global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.PacketHeader.Layout.Size || Throws(() => { global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.PacketHeader.Layout.Read(new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.PacketHeader.Layout.Size]); }) || !Throws(() => { global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.PacketHeader.Layout.Read(new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.PacketHeader.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.PacketHeader.Layout.Initialize(new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.PacketHeader.Layout.Size]); }) || !Throws(() => { global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.PacketHeader.Layout.Initialize(new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.PacketHeader.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.PacketHeader)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.PacketHeader.Layout.Size];
    var filled = new byte[global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.PacketHeader.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.PacketHeader.Layout.Initialize(zeroed);
    global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.PacketHeader.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.PacketHeader.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.PacketHeader.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.PacketHeader)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
var walker = new Walker();
var unread = None;
var invalid = None;
var incomplete = None;
long skipped;
try
{
    using var reader = global::Omi.Pcap.PcapReader.Open(args[PcapArgument]);
    while (reader.Advance())
    {
        var read = false;
        walker.Frame = reader.Data.ToArray();
        if (packetCapture is not null)
        {
            var opened = packetCapture.TryRead(walker.Frame, reader.LinkType, out var reading);
            if (packetCapture.FrameRead)
            {
                read = true;
                if (opened)
                {
                    try
                    {
                        walker.VisitPacket(reading);
                    }
                    catch (global::System.ArgumentOutOfRangeException)
                    {
                        invalid++;
                    }
                }
                if (!opened) invalid++;
            }
        }
        if (!read) unread++;
    }

    skipped = reader.SkippedPackets;
}
catch (global::System.Exception exception)
{
    global::System.Console.Error.WriteLine($"FAIL: {exception.GetType().Name}: {exception.Message}");
    return Failure;
}

global::System.Console.WriteLine($"Parsed: {walker.Parsed}; invalid: {invalid}; incomplete: {incomplete}; unread frames: {unread}; skipped frames: {skipped}");
if (walker.OnComplexStrategyAuctionMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnComplexStrategyAuctionMessageCalls)}: {walker.OnComplexStrategyAuctionMessageCalls}");
if (walker.OnComplexStrategyOrderOnBookMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnComplexStrategyOrderOnBookMessageCalls)}: {walker.OnComplexStrategyOrderOnBookMessageCalls}");
if (walker.OnStrategyOpenClosedMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnStrategyOpenClosedMessageCalls)}: {walker.OnStrategyOpenClosedMessageCalls}");
if (walker.OnStrategyTradingActionMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnStrategyTradingActionMessageCalls)}: {walker.OnStrategyTradingActionMessageCalls}");
if (walker.OnComplexStrategyDirectoryMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnComplexStrategyDirectoryMessageCalls)}: {walker.OnComplexStrategyDirectoryMessageCalls}");
if (walker.OnSystemEventMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnSystemEventMessageCalls)}: {walker.OnSystemEventMessageCalls}");
if (walker.OnEndOfSessionCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnEndOfSessionCalls)}: {walker.OnEndOfSessionCalls}");
if (walker.OnHeartbeatCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnHeartbeatCalls)}: {walker.OnHeartbeatCalls}");
if (walker.OnPacketHeaderCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnPacketHeaderCalls)}: {walker.OnPacketHeaderCalls}");
if (walker.OnPacketCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnPacketCalls)}: {walker.OnPacketCalls}");
global::System.Console.WriteLine($"Observed: {walker.Observed:X16}");
global::System.Console.WriteLine($"Checked: {nameof(walker.StableEntries)} {walker.StableEntries}; {nameof(walker.EndedEntries)} {walker.EndedEntries}; {nameof(walker.ActionChecks)} {walker.ActionChecks}; {nameof(walker.SequenceValued)} {walker.SequenceValued}; {nameof(walker.SequenceAbsent)} {walker.SequenceAbsent}");
return invalid > None || incomplete > None || strict && (walker.Parsed == None || unread > None || skipped > None) ? Failure : Success;

bool Selects(string tree, string alias)
{
    var selected = feed is null || string.Equals(feed, tree, global::System.StringComparison.OrdinalIgnoreCase) || string.Equals(feed, alias, global::System.StringComparison.OrdinalIgnoreCase);
    if (selected) feeds++;
    return selected;
}

bool Throws(global::System.Action action)
{
    try
    {
        action();
        return false;
    }
    catch (global::System.ArgumentException)
    {
        return true;
    }
}

struct Walker
{
    /// <summary>
    ///  The number of messages parsed.
    /// </summary>
    public long Parsed;
    /// <summary>
    ///  The number of calls of the OnPacketHeader method.
    /// </summary>
    public long OnPacketHeaderCalls;
    /// <summary>
    ///  The number of calls of the OnHeartbeat method.
    /// </summary>
    public long OnHeartbeatCalls;
    /// <summary>
    ///  The number of calls of the OnEndOfSession method.
    /// </summary>
    public long OnEndOfSessionCalls;
    /// <summary>
    ///  The number of calls of the OnSystemEventMessage method.
    /// </summary>
    public long OnSystemEventMessageCalls;
    /// <summary>
    ///  The number of calls of the OnComplexStrategyDirectoryMessage method.
    /// </summary>
    public long OnComplexStrategyDirectoryMessageCalls;
    /// <summary>
    ///  The number of calls of the OnStrategyTradingActionMessage method.
    /// </summary>
    public long OnStrategyTradingActionMessageCalls;
    /// <summary>
    ///  The number of calls of the OnStrategyOpenClosedMessage method.
    /// </summary>
    public long OnStrategyOpenClosedMessageCalls;
    /// <summary>
    ///  The number of calls of the OnComplexStrategyOrderOnBookMessage method.
    /// </summary>
    public long OnComplexStrategyOrderOnBookMessageCalls;
    /// <summary>
    ///  The number of calls of the OnComplexStrategyAuctionMessage method.
    /// </summary>
    public long OnComplexStrategyAuctionMessageCalls;
    /// <summary>
    ///  The number of calls of the OnPacket method.
    /// </summary>
    public long OnPacketCalls;
    /// <summary>
    ///  The current entries that a second read gave again.
    /// </summary>
    public long StableEntries;
    /// <summary>
    ///  The entries that ended with the number of entries read.
    /// </summary>
    public long EndedEntries;
    /// <summary>
    ///  The action values that a Context gives as the Reader of the field gives them.
    /// </summary>
    public long ActionChecks;
    /// <summary>
    ///  The numbered occurrences that delivered the number that their seeds give.
    /// </summary>
    public long SequenceValued;
    /// <summary>
    ///  The numbered occurrences that delivered no number, as their sequences have none.
    /// </summary>
    public long SequenceAbsent;
    const ulong ObservationBasis = 14695981039346656037;
    const ulong ObservationPrime = 1099511628211;
    const int Unplaced = -1;
    /// <summary>
    ///  The hash of the observations so far.
    /// </summary>
    public ulong Observed;
    /// <summary>
    ///  The bytes of the current frame, which the captures read.
    /// </summary>
    public byte[] Frame;
    /// <summary>
    ///  The text that the Dump print of a visited value writes.
    /// </summary>
    public global::System.IO.StringWriter Printer;

    public Walker()
    {
        this.Observed = ObservationBasis;
        this.Frame = [];
        this.Printer = new();
    }

    void Observe(global::System.ReadOnlySpan<byte> bytes)
    {
        foreach (var value in bytes) this.Observed = unchecked((this.Observed ^ value) * ObservationPrime);
    }

    /// <summary>
    ///  Adds a number, such as where the parser stopped in a frame, to the hash of the observations.
    /// </summary>
    public void Observe(long value) => this.Observe(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.ReadOnlySpan<long>(in value)));
    void Observe(string text) => this.Observe(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(text.AsSpan()));
    int Place(global::System.ReadOnlySpan<byte> bytes) => global::System.MemoryExtensions.Overlaps((global::System.ReadOnlySpan<byte>)this.Frame, bytes, out var offset) ? offset : Unplaced;
    void Locate(global::System.ReadOnlySpan<byte> bytes)
    {
        long place = this.Place(bytes);
        foreach (var value in global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.ReadOnlySpan<long>(in place))) this.Observed = unchecked((this.Observed ^ value) * ObservationPrime);
    }

    void CheckAction<T>(T fromReader, T fromContext)
    {
        if (!global::System.Collections.Generic.EqualityComparer<T>.Default.Equals(fromReader, fromContext)) throw new global::System.InvalidOperationException($"A Context gives the action value {fromContext}; the Reader of the field gives {fromReader}.");
        this.ActionChecks++;
    }

    const ulong SequenceStep = 1UL;
    ulong? PacketSequenceNext;
    bool PacketSequenceShared;
    int numberedDepth;
    ulong? numberTaken;

    /// <summary>
    ///  Clears the packet sequence where a root occurrence starts.
    /// </summary>
    public void BeginPacketSequence()
    {
        this.PacketSequenceNext = null;
        this.numberedDepth = 0;
    }
    /// <summary>
    ///  Starts the packet sequence with the value of a seed field.
    /// </summary>
    public void SeedPacketSequence(ulong? value)
    {
        this.PacketSequenceNext = value;
        this.PacketSequenceShared = false;
    }
    /// <summary>
    ///  Starts a packet sequence that gives the value of a seed field to each message.
    /// </summary>
    public void SeedSharedPacketSequence(ulong? value)
    {
        this.PacketSequenceNext = value;
        this.PacketSequenceShared = true;
    }
    ulong? NextPacketSequence()
    {
        if (this.numberedDepth > 0) return this.numberTaken;
        var number = this.PacketSequenceNext;
        if (number is { } value && !this.PacketSequenceShared) this.PacketSequenceNext = unchecked(value + SequenceStep);
        this.numberTaken = number;
        return number;
    }
    void CheckSequence(ulong? delivered, ulong? expected, string name)
    {
        if (delivered != expected) throw new global::System.InvalidOperationException($"The {name} occurrence has the sequence number {delivered}; its sequences give {expected}.");
        if (expected is null) this.SequenceAbsent++;
        else this.SequenceValued++;
    }
    /// <summary>
    ///  Gets the number of numbered occurrences that the packet traversal checked.
    /// </summary>
    public readonly long SequenceChecks => this.SequenceValued + this.SequenceAbsent;

    /// <summary>
    ///  Visits the packet and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitPacket(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Packet.Reader packet)
    {
        this.BeginPacketSequence();
        this.SeedPacketSequence(ulong.CreateSaturating(packet.PacketHeader.SequenceNumber.Value));
        this.OnPacketHeader(in packet.PacketHeader);
        var messages = packet.Messages;
        switch (messages.Case)
        {
            case global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Messages.Case.Heartbeat:
            {
                this.OnHeartbeat();
                break;
            }
            case global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Messages.Case.EndOfSession:
            {
                this.OnEndOfSession();
                break;
            }
            case global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Messages.Case.Message:
            {
                var messageEntries = messages.Message;
                var messageRead = 0L;
                while (messageEntries.MoveNext())
                {
                    var message = messageEntries.Current;
                    {
                        ulong? expected = null;
                        expected = this.NextPacketSequence();
                        this.CheckSequence((ulong?)message.SequenceNumber, expected, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Message));
                    }
                    this.numberedDepth++;
                    var payload = message.Payload;
                    switch (payload.Case)
                    {
                        case global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.SystemEventMessage:
                        {
                            this.OnSystemEventMessage(in payload.SystemEventMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.ComplexStrategyDirectoryMessage:
                        {
                            this.OnComplexStrategyDirectoryMessage(payload.ComplexStrategyDirectoryMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.StrategyTradingActionMessage:
                        {
                            this.OnStrategyTradingActionMessage(in payload.StrategyTradingActionMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.StrategyOpenClosedMessage:
                        {
                            this.OnStrategyOpenClosedMessage(in payload.StrategyOpenClosedMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.ComplexStrategyOrderOnBookMessage:
                        {
                            this.OnComplexStrategyOrderOnBookMessage(in payload.ComplexStrategyOrderOnBookMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.ComplexStrategyAuctionMessage:
                        {
                            this.OnComplexStrategyAuctionMessage(payload.ComplexStrategyAuctionMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                    }
                    this.numberedDepth--;
                    if (messageEntries.Index != ++messageRead) throw new global::System.InvalidOperationException("The Messages.Message entries do not number their entries in order.");
                    _ = messageEntries.Current;
                    if (messageEntries.Index != messageRead) throw new global::System.InvalidOperationException("A second read of the current Messages.Message entry moves the entries.");
                    this.StableEntries++;
                }
                if (messageEntries.MoveNext() || messageEntries.Index != messageRead) throw new global::System.InvalidOperationException("The Messages.Message entries step after their end, or number their entries differently from the entries read.");
                this.EndedEntries++;
                if (messageEntries.Count != messageRead) throw new global::System.InvalidOperationException("The Messages.Message entries differ from their count.");
                break;
            }
        }
        this.OnPacket(packet);
    }
    void OnPacketHeader(in global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.PacketHeader.Layout value)
    {
        this.Parsed++;
        this.OnPacketHeaderCalls++;
        this.Observe(nameof(OnPacketHeaderCalls));
        var bytes = global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(global::System.Runtime.InteropServices.MemoryMarshal.CreateReadOnlySpan(in value, 1));
        this.Observe(bytes);
        this.Locate(bytes);
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                Dump.Print(value, Dump.Indent);
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
    }
    void OnHeartbeat()
    {
        this.Parsed++;
        this.OnHeartbeatCalls++;
        this.Observe(nameof(OnHeartbeatCalls));
    }
    void OnEndOfSession()
    {
        this.Parsed++;
        this.OnEndOfSessionCalls++;
        this.Observe(nameof(OnEndOfSessionCalls));
    }
    void OnSystemEventMessage(in global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.SystemEventMessage.Layout value, in global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageHeader.Layout header, ulong sequenceNumber)
    {
        this.Parsed++;
        this.OnSystemEventMessageCalls++;
        this.Observe(nameof(OnSystemEventMessageCalls));
        var bytes = global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(global::System.Runtime.InteropServices.MemoryMarshal.CreateReadOnlySpan(in value, 1));
        this.Observe(bytes);
        this.Locate(bytes);
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                Dump.Print(value, Dump.Indent);
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
    }
    void OnComplexStrategyDirectoryMessage(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyDirectoryMessage.Reader value, in global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageHeader.Layout header, ulong sequenceNumber)
    {
        this.Parsed++;
        this.OnComplexStrategyDirectoryMessageCalls++;
        this.Observe(nameof(OnComplexStrategyDirectoryMessageCalls));
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                var reader = value;
                var indent = Dump.Indent;
                Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyDirectoryMessage.Reader.Timestamp), reader.Timestamp);
                Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyDirectoryMessage.Reader.StrategyId), reader.StrategyId);
                Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyDirectoryMessage.Reader.StrategyType), reader.StrategyType);
                Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyDirectoryMessage.Reader.Source), reader.Source);
                Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyDirectoryMessage.Reader.UnderlyingSymbol), reader.UnderlyingSymbol);
                Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyDirectoryMessage.Reader.NumberOfLegs), reader.NumberOfLegs);
                foreach (var entry in reader.LegInformation)
                {
                    Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyDirectoryMessage.Reader.LegInformation));
                    Print(entry, indent + Indent);
                }
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
        {
            var entries = value.LegInformation;
            var read = entries.Index;
            var ordered = true;
            while (entries.MoveNext()) ordered &= entries.Index == ++read;
            this.Observe(read);
            if (!ordered || read != entries.Count || entries.MoveNext()) throw new global::System.InvalidOperationException("The ComplexStrategyDirectoryMessage.Reader.LegInformation entries differ from their index or count.");
        }
    }
    void OnStrategyTradingActionMessage(in global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyTradingActionMessage.Layout value, in global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageHeader.Layout header, ulong sequenceNumber)
    {
        this.Parsed++;
        this.OnStrategyTradingActionMessageCalls++;
        this.Observe(nameof(OnStrategyTradingActionMessageCalls));
        var bytes = global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(global::System.Runtime.InteropServices.MemoryMarshal.CreateReadOnlySpan(in value, 1));
        this.Observe(bytes);
        this.Locate(bytes);
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                Dump.Print(value, Dump.Indent);
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
    }
    void OnStrategyOpenClosedMessage(in global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyOpenClosedMessage.Layout value, in global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageHeader.Layout header, ulong sequenceNumber)
    {
        this.Parsed++;
        this.OnStrategyOpenClosedMessageCalls++;
        this.Observe(nameof(OnStrategyOpenClosedMessageCalls));
        var bytes = global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(global::System.Runtime.InteropServices.MemoryMarshal.CreateReadOnlySpan(in value, 1));
        this.Observe(bytes);
        this.Locate(bytes);
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                Dump.Print(value, Dump.Indent);
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
    }
    void OnComplexStrategyOrderOnBookMessage(in global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyOrderOnBookMessage.Layout value, in global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageHeader.Layout header, ulong sequenceNumber)
    {
        this.Parsed++;
        this.OnComplexStrategyOrderOnBookMessageCalls++;
        this.Observe(nameof(OnComplexStrategyOrderOnBookMessageCalls));
        var bytes = global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(global::System.Runtime.InteropServices.MemoryMarshal.CreateReadOnlySpan(in value, 1));
        this.Observe(bytes);
        this.Locate(bytes);
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                Dump.Print(value, Dump.Indent);
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
    }
    void OnComplexStrategyAuctionMessage(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Reader value, in global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageHeader.Layout header, ulong sequenceNumber)
    {
        this.Parsed++;
        this.OnComplexStrategyAuctionMessageCalls++;
        this.Observe(nameof(OnComplexStrategyAuctionMessageCalls));
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                var reader = value;
                var indent = Dump.Indent;
                Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Reader.Timestamp), reader.Timestamp);
                Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Reader.StrategyId), reader.StrategyId);
                Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Reader.AuctionId), reader.AuctionId);
                Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Reader.OrderType), reader.OrderType);
                Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Reader.Side), reader.Side);
                Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Reader.Price), reader.Price);
                Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Reader.Size), reader.Size);
                Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Reader.ExecFlag), reader.ExecFlag);
                Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Reader.OrderCapacity), reader.OrderCapacity);
                Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Reader.Scope), reader.Scope);
                Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Reader.OwnerId), reader.OwnerId);
                Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Reader.Giveup), reader.Giveup);
                Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Reader.Cmta), reader.Cmta);
                Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Reader.AuctionEvent), reader.AuctionEvent);
                Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Reader.AuctionType), reader.AuctionType);
                Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Reader.NumberOfResponses), reader.NumberOfResponses);
                foreach (var entry in reader.AuctionResponse)
                {
                    Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Reader.AuctionResponse));
                    Print(entry, indent + Indent);
                }
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
        {
            var entries = value.AuctionResponse;
            var read = entries.Index;
            var ordered = true;
            while (entries.MoveNext()) ordered &= entries.Index == ++read;
            this.Observe(read);
            if (!ordered || read != entries.Count || entries.MoveNext()) throw new global::System.InvalidOperationException("The ComplexStrategyAuctionMessage.Reader.AuctionResponse entries differ from their index or count.");
        }
    }
    void OnPacket(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Packet.Reader value)
    {
        this.Parsed++;
        this.OnPacketCalls++;
        this.Observe(nameof(OnPacketCalls));
    }
}
