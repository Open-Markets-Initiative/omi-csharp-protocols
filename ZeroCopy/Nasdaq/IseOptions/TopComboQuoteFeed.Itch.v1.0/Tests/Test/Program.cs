using Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0;
using Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Iterator;
using Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Test;
using static Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Test.Dump;

const int PcapArgument = 0;
const int FirstOption = 1;
const int None = 0;
const int OneFeed = 1;
const int Success = 0;
const int Failure = 1;
const int UsageFailure = 2;
const string FeedOption = "--feed";
const string StrictOption = "--strict";
const string Usage = $"usage: Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1.0.Test <pcap> [{FeedOption} <name>] [{StrictOption}]";
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
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyTickerMessage.Layout>() != global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyTickerMessage.Layout.LayoutSize || Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyTickerMessage.Layout.Read(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyTickerMessage.Layout.LayoutSize]); }) || !Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyTickerMessage.Layout.Read(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyTickerMessage.Layout.LayoutSize - 1]); }) || Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyTickerMessage.Layout.Initialize(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyTickerMessage.Layout.LayoutSize]); }) || !Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyTickerMessage.Layout.Initialize(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyTickerMessage.Layout.LayoutSize - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyTickerMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyTickerMessage.Layout.LayoutSize];
    var filled = new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyTickerMessage.Layout.LayoutSize + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyTickerMessage.Layout.Initialize(zeroed);
    global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyTickerMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyTickerMessage.Layout.LayoutSize).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyTickerMessage.Layout.LayoutSize).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyTickerMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestAskUpdate.Layout>() != global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestAskUpdate.Layout.LayoutSize || Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestAskUpdate.Layout.Read(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestAskUpdate.Layout.LayoutSize]); }) || !Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestAskUpdate.Layout.Read(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestAskUpdate.Layout.LayoutSize - 1]); }) || Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestAskUpdate.Layout.Initialize(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestAskUpdate.Layout.LayoutSize]); }) || !Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestAskUpdate.Layout.Initialize(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestAskUpdate.Layout.LayoutSize - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestAskUpdate)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestAskUpdate.Layout.LayoutSize];
    var filled = new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestAskUpdate.Layout.LayoutSize + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestAskUpdate.Layout.Initialize(zeroed);
    global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestAskUpdate.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestAskUpdate.Layout.LayoutSize).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestAskUpdate.Layout.LayoutSize).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestAskUpdate)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidUpdate.Layout>() != global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidUpdate.Layout.LayoutSize || Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidUpdate.Layout.Read(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidUpdate.Layout.LayoutSize]); }) || !Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidUpdate.Layout.Read(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidUpdate.Layout.LayoutSize - 1]); }) || Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidUpdate.Layout.Initialize(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidUpdate.Layout.LayoutSize]); }) || !Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidUpdate.Layout.Initialize(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidUpdate.Layout.LayoutSize - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidUpdate)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidUpdate.Layout.LayoutSize];
    var filled = new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidUpdate.Layout.LayoutSize + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidUpdate.Layout.Initialize(zeroed);
    global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidUpdate.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidUpdate.Layout.LayoutSize).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidUpdate.Layout.LayoutSize).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidUpdate)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidAndAskUpdate.Layout>() != global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidAndAskUpdate.Layout.Size || Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidAndAskUpdate.Layout.Read(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidAndAskUpdate.Layout.Size]); }) || !Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidAndAskUpdate.Layout.Read(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidAndAskUpdate.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidAndAskUpdate.Layout.Initialize(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidAndAskUpdate.Layout.Size]); }) || !Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidAndAskUpdate.Layout.Initialize(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidAndAskUpdate.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidAndAskUpdate)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidAndAskUpdate.Layout.Size];
    var filled = new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidAndAskUpdate.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidAndAskUpdate.Layout.Initialize(zeroed);
    global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidAndAskUpdate.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidAndAskUpdate.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidAndAskUpdate.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidAndAskUpdate)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyTradingActionMessage.Layout>() != global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyTradingActionMessage.Layout.Size || Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyTradingActionMessage.Layout.Read(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyTradingActionMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyTradingActionMessage.Layout.Read(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyTradingActionMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyTradingActionMessage.Layout.Initialize(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyTradingActionMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyTradingActionMessage.Layout.Initialize(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyTradingActionMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyTradingActionMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyTradingActionMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyTradingActionMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyTradingActionMessage.Layout.Initialize(zeroed);
    global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyTradingActionMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyTradingActionMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyTradingActionMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyTradingActionMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyOpenClosedMessage.Layout>() != global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyOpenClosedMessage.Layout.Size || Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyOpenClosedMessage.Layout.Read(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyOpenClosedMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyOpenClosedMessage.Layout.Read(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyOpenClosedMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyOpenClosedMessage.Layout.Initialize(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyOpenClosedMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyOpenClosedMessage.Layout.Initialize(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyOpenClosedMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyOpenClosedMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyOpenClosedMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyOpenClosedMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyOpenClosedMessage.Layout.Initialize(zeroed);
    global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyOpenClosedMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyOpenClosedMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyOpenClosedMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyOpenClosedMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.LegInformation.Layout>() != global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.LegInformation.Layout.Size || Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.LegInformation.Layout.Read(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.LegInformation.Layout.Size]); }) || !Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.LegInformation.Layout.Read(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.LegInformation.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.LegInformation.Layout.Initialize(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.LegInformation.Layout.Size]); }) || !Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.LegInformation.Layout.Initialize(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.LegInformation.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.LegInformation)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.LegInformation.Layout.Size];
    var filled = new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.LegInformation.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.LegInformation.Layout.Initialize(zeroed);
    global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.LegInformation.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.LegInformation.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.LegInformation.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.LegInformation)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.SystemEventMessage.Layout>() != global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.SystemEventMessage.Layout.Size || Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.SystemEventMessage.Layout.Read(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.SystemEventMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.SystemEventMessage.Layout.Read(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.SystemEventMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.SystemEventMessage.Layout.Initialize(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.SystemEventMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.SystemEventMessage.Layout.Initialize(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.SystemEventMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.SystemEventMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.SystemEventMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.SystemEventMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.SystemEventMessage.Layout.Initialize(zeroed);
    global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.SystemEventMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.SystemEventMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.SystemEventMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.SystemEventMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageHeader.Layout>() != global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageHeader.Layout.Size || Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageHeader.Layout.Read(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageHeader.Layout.Size]); }) || !Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageHeader.Layout.Read(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageHeader.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageHeader.Layout.Initialize(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageHeader.Layout.Size]); }) || !Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageHeader.Layout.Initialize(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageHeader.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageHeader)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageHeader.Layout.Size];
    var filled = new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageHeader.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageHeader.Layout.Initialize(zeroed);
    global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageHeader.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageHeader.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageHeader.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageHeader)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.PacketHeader.Layout>() != global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.PacketHeader.Layout.Size || Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.PacketHeader.Layout.Read(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.PacketHeader.Layout.Size]); }) || !Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.PacketHeader.Layout.Read(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.PacketHeader.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.PacketHeader.Layout.Initialize(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.PacketHeader.Layout.Size]); }) || !Throws(() => { global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.PacketHeader.Layout.Initialize(new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.PacketHeader.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.PacketHeader)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.PacketHeader.Layout.Size];
    var filled = new byte[global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.PacketHeader.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.PacketHeader.Layout.Initialize(zeroed);
    global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.PacketHeader.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.PacketHeader.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.PacketHeader.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.PacketHeader)} Layout does not clear exactly its bytes.");
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
if (walker.OnComplexStrategyTickerMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnComplexStrategyTickerMessageCalls)}: {walker.OnComplexStrategyTickerMessageCalls}");
if (walker.OnStrategyBestAskUpdateCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnStrategyBestAskUpdateCalls)}: {walker.OnStrategyBestAskUpdateCalls}");
if (walker.OnStrategyBestBidUpdateCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnStrategyBestBidUpdateCalls)}: {walker.OnStrategyBestBidUpdateCalls}");
if (walker.OnStrategyBestBidAndAskUpdateCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnStrategyBestBidAndAskUpdateCalls)}: {walker.OnStrategyBestBidAndAskUpdateCalls}");
if (walker.OnStrategyTradingActionMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnStrategyTradingActionMessageCalls)}: {walker.OnStrategyTradingActionMessageCalls}");
if (walker.OnStrategyOpenClosedMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnStrategyOpenClosedMessageCalls)}: {walker.OnStrategyOpenClosedMessageCalls}");
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
    ///  The number of calls of the OnStrategyOpenClosedMessage method.
    /// </summary>
    public long OnStrategyOpenClosedMessageCalls;
    /// <summary>
    ///  The number of calls of the OnStrategyTradingActionMessage method.
    /// </summary>
    public long OnStrategyTradingActionMessageCalls;
    /// <summary>
    ///  The number of calls of the OnStrategyBestBidAndAskUpdate method.
    /// </summary>
    public long OnStrategyBestBidAndAskUpdateCalls;
    /// <summary>
    ///  The number of calls of the OnStrategyBestBidUpdate method.
    /// </summary>
    public long OnStrategyBestBidUpdateCalls;
    /// <summary>
    ///  The number of calls of the OnStrategyBestAskUpdate method.
    /// </summary>
    public long OnStrategyBestAskUpdateCalls;
    /// <summary>
    ///  The number of calls of the OnComplexStrategyTickerMessage method.
    /// </summary>
    public long OnComplexStrategyTickerMessageCalls;
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
    public void VisitPacket(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Packet.Reader packet)
    {
        this.BeginPacketSequence();
        this.SeedPacketSequence(ulong.CreateSaturating(packet.PacketHeader.SequenceNumber.Value));
        this.OnPacketHeader(in packet.PacketHeader);
        var messages = packet.Messages;
        switch (messages.Case)
        {
            case global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Messages.Case.Heartbeat:
            {
                this.OnHeartbeat();
                break;
            }
            case global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Messages.Case.EndOfSession:
            {
                this.OnEndOfSession();
                break;
            }
            case global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Messages.Case.Message:
            {
                var messageEntries = messages.Message;
                var messageRead = 0L;
                while (messageEntries.MoveNext())
                {
                    var message = messageEntries.Current;
                    {
                        ulong? expected = null;
                        expected = this.NextPacketSequence();
                        this.CheckSequence((ulong?)message.SequenceNumber, expected, nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Message));
                    }
                    this.numberedDepth++;
                    var payload = message.Payload;
                    switch (payload.Case)
                    {
                        case global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.SystemEventMessage:
                        {
                            this.OnSystemEventMessage(in payload.SystemEventMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.ComplexStrategyDirectoryMessage:
                        {
                            this.OnComplexStrategyDirectoryMessage(payload.ComplexStrategyDirectoryMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyOpenClosedMessage:
                        {
                            this.OnStrategyOpenClosedMessage(in payload.StrategyOpenClosedMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyTradingActionMessage:
                        {
                            this.OnStrategyTradingActionMessage(in payload.StrategyTradingActionMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyBestBidAndAskUpdate:
                        {
                            this.OnStrategyBestBidAndAskUpdate(in payload.StrategyBestBidAndAskUpdate, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyBestBidUpdate:
                        {
                            this.OnStrategyBestBidUpdate(in payload.StrategyBestBidUpdate, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyBestAskUpdate:
                        {
                            this.OnStrategyBestAskUpdate(in payload.StrategyBestAskUpdate, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.ComplexStrategyTickerMessage:
                        {
                            this.OnComplexStrategyTickerMessage(in payload.ComplexStrategyTickerMessage, in message.MessageHeader, message.SequenceNumber);
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
    void OnPacketHeader(in global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.PacketHeader.Layout value)
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
    void OnSystemEventMessage(in global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.SystemEventMessage.Layout value, in global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnComplexStrategyDirectoryMessage(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyDirectoryMessage.Reader value, in global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageHeader.Layout header, ulong sequenceNumber)
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
                Line(indent, nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyDirectoryMessage.Reader.Timestamp), reader.Timestamp);
                Line(indent, nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyDirectoryMessage.Reader.StrategyId), reader.StrategyId);
                Line(indent, nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyDirectoryMessage.Reader.StrategyType), reader.StrategyType);
                Line(indent, nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyDirectoryMessage.Reader.Source), reader.Source);
                Line(indent, nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyDirectoryMessage.Reader.UnderlyingSymbol), reader.UnderlyingSymbol);
                Line(indent, nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyDirectoryMessage.Reader.NumberOfLegs), reader.NumberOfLegs);
                foreach (var entry in reader.LegInformation)
                {
                    Line(indent, nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyDirectoryMessage.Reader.LegInformation));
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
    void OnStrategyOpenClosedMessage(in global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyOpenClosedMessage.Layout value, in global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnStrategyTradingActionMessage(in global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyTradingActionMessage.Layout value, in global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnStrategyBestBidAndAskUpdate(in global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidAndAskUpdate.Layout value, in global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageHeader.Layout header, ulong sequenceNumber)
    {
        this.Parsed++;
        this.OnStrategyBestBidAndAskUpdateCalls++;
        this.Observe(nameof(OnStrategyBestBidAndAskUpdateCalls));
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
    void OnStrategyBestBidUpdate(in global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidUpdate.Layout value, in global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageHeader.Layout header, ulong sequenceNumber)
    {
        this.Parsed++;
        this.OnStrategyBestBidUpdateCalls++;
        this.Observe(nameof(OnStrategyBestBidUpdateCalls));
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
    void OnStrategyBestAskUpdate(in global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestAskUpdate.Layout value, in global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageHeader.Layout header, ulong sequenceNumber)
    {
        this.Parsed++;
        this.OnStrategyBestAskUpdateCalls++;
        this.Observe(nameof(OnStrategyBestAskUpdateCalls));
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
    void OnComplexStrategyTickerMessage(in global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyTickerMessage.Layout value, in global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageHeader.Layout header, ulong sequenceNumber)
    {
        this.Parsed++;
        this.OnComplexStrategyTickerMessageCalls++;
        this.Observe(nameof(OnComplexStrategyTickerMessageCalls));
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
    void OnPacket(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Packet.Reader value)
    {
        this.Parsed++;
        this.OnPacketCalls++;
        this.Observe(nameof(OnPacketCalls));
    }
}
