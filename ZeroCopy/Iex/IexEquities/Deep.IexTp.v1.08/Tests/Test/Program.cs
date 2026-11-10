using Iex.IexEquities.Deep.IexTp.v1_08;
using Iex.IexEquities.Deep.IexTp.v1_08.Iterator;
using Iex.IexEquities.Deep.IexTp.v1_08.Test;
using static Iex.IexEquities.Deep.IexTp.v1_08.Test.Dump;

const int PcapArgument = 0;
const int FirstOption = 1;
const int None = 0;
const int OneFeed = 1;
const int Success = 0;
const int Failure = 1;
const int UsageFailure = 2;
const string FeedOption = "--feed";
const string StrictOption = "--strict";
const string Usage = $"usage: Iex.IexEquities.Deep.IexTp.v1.08.Test <pcap> [{FeedOption} <name>] [{StrictOption}]";
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
const byte versionStaticValue = 1;
const ushort messageProtocolIdStaticValue = 32772;
const uint channelIdStaticValue = 1u;
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Iex.IexEquities.Deep.IexTp.v1_08.AuctionInformationMessage.Layout>() != global::Iex.IexEquities.Deep.IexTp.v1_08.AuctionInformationMessage.Layout.Size || Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.AuctionInformationMessage.Layout.Read(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.AuctionInformationMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.AuctionInformationMessage.Layout.Read(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.AuctionInformationMessage.Layout.Size - 1]); }) || Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.AuctionInformationMessage.Layout.Initialize(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.AuctionInformationMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.AuctionInformationMessage.Layout.Initialize(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.AuctionInformationMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.AuctionInformationMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.AuctionInformationMessage.Layout.Size];
    var filled = new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.AuctionInformationMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Iex.IexEquities.Deep.IexTp.v1_08.AuctionInformationMessage.Layout.Initialize(zeroed);
    global::Iex.IexEquities.Deep.IexTp.v1_08.AuctionInformationMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Iex.IexEquities.Deep.IexTp.v1_08.AuctionInformationMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Iex.IexEquities.Deep.IexTp.v1_08.AuctionInformationMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.AuctionInformationMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Iex.IexEquities.Deep.IexTp.v1_08.TradeBreakMessage.Layout>() != global::Iex.IexEquities.Deep.IexTp.v1_08.TradeBreakMessage.Layout.LayoutSize || Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.TradeBreakMessage.Layout.Read(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.TradeBreakMessage.Layout.LayoutSize]); }) || !Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.TradeBreakMessage.Layout.Read(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.TradeBreakMessage.Layout.LayoutSize - 1]); }) || Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.TradeBreakMessage.Layout.Initialize(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.TradeBreakMessage.Layout.LayoutSize]); }) || !Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.TradeBreakMessage.Layout.Initialize(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.TradeBreakMessage.Layout.LayoutSize - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.TradeBreakMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.TradeBreakMessage.Layout.LayoutSize];
    var filled = new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.TradeBreakMessage.Layout.LayoutSize + Following];
    global::System.Array.Fill(filled, Filler);
    global::Iex.IexEquities.Deep.IexTp.v1_08.TradeBreakMessage.Layout.Initialize(zeroed);
    global::Iex.IexEquities.Deep.IexTp.v1_08.TradeBreakMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Iex.IexEquities.Deep.IexTp.v1_08.TradeBreakMessage.Layout.LayoutSize).SequenceEqual(zeroed) || filled.AsSpan(global::Iex.IexEquities.Deep.IexTp.v1_08.TradeBreakMessage.Layout.LayoutSize).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.TradeBreakMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Iex.IexEquities.Deep.IexTp.v1_08.OfficialPriceMessage.Layout>() != global::Iex.IexEquities.Deep.IexTp.v1_08.OfficialPriceMessage.Layout.Size || Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.OfficialPriceMessage.Layout.Read(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.OfficialPriceMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.OfficialPriceMessage.Layout.Read(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.OfficialPriceMessage.Layout.Size - 1]); }) || Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.OfficialPriceMessage.Layout.Initialize(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.OfficialPriceMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.OfficialPriceMessage.Layout.Initialize(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.OfficialPriceMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.OfficialPriceMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.OfficialPriceMessage.Layout.Size];
    var filled = new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.OfficialPriceMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Iex.IexEquities.Deep.IexTp.v1_08.OfficialPriceMessage.Layout.Initialize(zeroed);
    global::Iex.IexEquities.Deep.IexTp.v1_08.OfficialPriceMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Iex.IexEquities.Deep.IexTp.v1_08.OfficialPriceMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Iex.IexEquities.Deep.IexTp.v1_08.OfficialPriceMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.OfficialPriceMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Iex.IexEquities.Deep.IexTp.v1_08.TradeReportMessage.Layout>() != global::Iex.IexEquities.Deep.IexTp.v1_08.TradeReportMessage.Layout.LayoutSize || Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.TradeReportMessage.Layout.Read(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.TradeReportMessage.Layout.LayoutSize]); }) || !Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.TradeReportMessage.Layout.Read(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.TradeReportMessage.Layout.LayoutSize - 1]); }) || Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.TradeReportMessage.Layout.Initialize(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.TradeReportMessage.Layout.LayoutSize]); }) || !Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.TradeReportMessage.Layout.Initialize(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.TradeReportMessage.Layout.LayoutSize - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.TradeReportMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.TradeReportMessage.Layout.LayoutSize];
    var filled = new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.TradeReportMessage.Layout.LayoutSize + Following];
    global::System.Array.Fill(filled, Filler);
    global::Iex.IexEquities.Deep.IexTp.v1_08.TradeReportMessage.Layout.Initialize(zeroed);
    global::Iex.IexEquities.Deep.IexTp.v1_08.TradeReportMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Iex.IexEquities.Deep.IexTp.v1_08.TradeReportMessage.Layout.LayoutSize).SequenceEqual(zeroed) || filled.AsSpan(global::Iex.IexEquities.Deep.IexTp.v1_08.TradeReportMessage.Layout.LayoutSize).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.TradeReportMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelSellUpdateMessage.Layout>() != global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelSellUpdateMessage.Layout.LayoutSize || Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelSellUpdateMessage.Layout.Read(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelSellUpdateMessage.Layout.LayoutSize]); }) || !Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelSellUpdateMessage.Layout.Read(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelSellUpdateMessage.Layout.LayoutSize - 1]); }) || Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelSellUpdateMessage.Layout.Initialize(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelSellUpdateMessage.Layout.LayoutSize]); }) || !Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelSellUpdateMessage.Layout.Initialize(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelSellUpdateMessage.Layout.LayoutSize - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelSellUpdateMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelSellUpdateMessage.Layout.LayoutSize];
    var filled = new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelSellUpdateMessage.Layout.LayoutSize + Following];
    global::System.Array.Fill(filled, Filler);
    global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelSellUpdateMessage.Layout.Initialize(zeroed);
    global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelSellUpdateMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelSellUpdateMessage.Layout.LayoutSize).SequenceEqual(zeroed) || filled.AsSpan(global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelSellUpdateMessage.Layout.LayoutSize).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelSellUpdateMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelBuyUpdateMessage.Layout>() != global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelBuyUpdateMessage.Layout.LayoutSize || Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelBuyUpdateMessage.Layout.Read(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelBuyUpdateMessage.Layout.LayoutSize]); }) || !Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelBuyUpdateMessage.Layout.Read(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelBuyUpdateMessage.Layout.LayoutSize - 1]); }) || Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelBuyUpdateMessage.Layout.Initialize(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelBuyUpdateMessage.Layout.LayoutSize]); }) || !Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelBuyUpdateMessage.Layout.Initialize(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelBuyUpdateMessage.Layout.LayoutSize - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelBuyUpdateMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelBuyUpdateMessage.Layout.LayoutSize];
    var filled = new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelBuyUpdateMessage.Layout.LayoutSize + Following];
    global::System.Array.Fill(filled, Filler);
    global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelBuyUpdateMessage.Layout.Initialize(zeroed);
    global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelBuyUpdateMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelBuyUpdateMessage.Layout.LayoutSize).SequenceEqual(zeroed) || filled.AsSpan(global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelBuyUpdateMessage.Layout.LayoutSize).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelBuyUpdateMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityEventMessage.Layout>() != global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityEventMessage.Layout.Size || Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityEventMessage.Layout.Read(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityEventMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityEventMessage.Layout.Read(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityEventMessage.Layout.Size - 1]); }) || Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityEventMessage.Layout.Initialize(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityEventMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityEventMessage.Layout.Initialize(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityEventMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityEventMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityEventMessage.Layout.Size];
    var filled = new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityEventMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityEventMessage.Layout.Initialize(zeroed);
    global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityEventMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityEventMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityEventMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityEventMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Iex.IexEquities.Deep.IexTp.v1_08.ShortSalePriceTestStatusMessage.Layout>() != global::Iex.IexEquities.Deep.IexTp.v1_08.ShortSalePriceTestStatusMessage.Layout.Size || Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.ShortSalePriceTestStatusMessage.Layout.Read(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.ShortSalePriceTestStatusMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.ShortSalePriceTestStatusMessage.Layout.Read(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.ShortSalePriceTestStatusMessage.Layout.Size - 1]); }) || Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.ShortSalePriceTestStatusMessage.Layout.Initialize(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.ShortSalePriceTestStatusMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.ShortSalePriceTestStatusMessage.Layout.Initialize(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.ShortSalePriceTestStatusMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.ShortSalePriceTestStatusMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.ShortSalePriceTestStatusMessage.Layout.Size];
    var filled = new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.ShortSalePriceTestStatusMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Iex.IexEquities.Deep.IexTp.v1_08.ShortSalePriceTestStatusMessage.Layout.Initialize(zeroed);
    global::Iex.IexEquities.Deep.IexTp.v1_08.ShortSalePriceTestStatusMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Iex.IexEquities.Deep.IexTp.v1_08.ShortSalePriceTestStatusMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Iex.IexEquities.Deep.IexTp.v1_08.ShortSalePriceTestStatusMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.ShortSalePriceTestStatusMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Iex.IexEquities.Deep.IexTp.v1_08.OperationalHaltStatusMessage.Layout>() != global::Iex.IexEquities.Deep.IexTp.v1_08.OperationalHaltStatusMessage.Layout.Size || Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.OperationalHaltStatusMessage.Layout.Read(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.OperationalHaltStatusMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.OperationalHaltStatusMessage.Layout.Read(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.OperationalHaltStatusMessage.Layout.Size - 1]); }) || Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.OperationalHaltStatusMessage.Layout.Initialize(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.OperationalHaltStatusMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.OperationalHaltStatusMessage.Layout.Initialize(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.OperationalHaltStatusMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.OperationalHaltStatusMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.OperationalHaltStatusMessage.Layout.Size];
    var filled = new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.OperationalHaltStatusMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Iex.IexEquities.Deep.IexTp.v1_08.OperationalHaltStatusMessage.Layout.Initialize(zeroed);
    global::Iex.IexEquities.Deep.IexTp.v1_08.OperationalHaltStatusMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Iex.IexEquities.Deep.IexTp.v1_08.OperationalHaltStatusMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Iex.IexEquities.Deep.IexTp.v1_08.OperationalHaltStatusMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.OperationalHaltStatusMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Iex.IexEquities.Deep.IexTp.v1_08.RetailLiquidityIndicatorMessage.Layout>() != global::Iex.IexEquities.Deep.IexTp.v1_08.RetailLiquidityIndicatorMessage.Layout.Size || Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.RetailLiquidityIndicatorMessage.Layout.Read(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.RetailLiquidityIndicatorMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.RetailLiquidityIndicatorMessage.Layout.Read(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.RetailLiquidityIndicatorMessage.Layout.Size - 1]); }) || Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.RetailLiquidityIndicatorMessage.Layout.Initialize(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.RetailLiquidityIndicatorMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.RetailLiquidityIndicatorMessage.Layout.Initialize(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.RetailLiquidityIndicatorMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.RetailLiquidityIndicatorMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.RetailLiquidityIndicatorMessage.Layout.Size];
    var filled = new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.RetailLiquidityIndicatorMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Iex.IexEquities.Deep.IexTp.v1_08.RetailLiquidityIndicatorMessage.Layout.Initialize(zeroed);
    global::Iex.IexEquities.Deep.IexTp.v1_08.RetailLiquidityIndicatorMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Iex.IexEquities.Deep.IexTp.v1_08.RetailLiquidityIndicatorMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Iex.IexEquities.Deep.IexTp.v1_08.RetailLiquidityIndicatorMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.RetailLiquidityIndicatorMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Iex.IexEquities.Deep.IexTp.v1_08.TradingStatusMessage.Layout>() != global::Iex.IexEquities.Deep.IexTp.v1_08.TradingStatusMessage.Layout.Size || Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.TradingStatusMessage.Layout.Read(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.TradingStatusMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.TradingStatusMessage.Layout.Read(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.TradingStatusMessage.Layout.Size - 1]); }) || Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.TradingStatusMessage.Layout.Initialize(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.TradingStatusMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.TradingStatusMessage.Layout.Initialize(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.TradingStatusMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.TradingStatusMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.TradingStatusMessage.Layout.Size];
    var filled = new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.TradingStatusMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Iex.IexEquities.Deep.IexTp.v1_08.TradingStatusMessage.Layout.Initialize(zeroed);
    global::Iex.IexEquities.Deep.IexTp.v1_08.TradingStatusMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Iex.IexEquities.Deep.IexTp.v1_08.TradingStatusMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Iex.IexEquities.Deep.IexTp.v1_08.TradingStatusMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.TradingStatusMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityDirectoryMessage.Layout>() != global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityDirectoryMessage.Layout.Size || Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityDirectoryMessage.Layout.Read(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityDirectoryMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityDirectoryMessage.Layout.Read(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityDirectoryMessage.Layout.Size - 1]); }) || Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityDirectoryMessage.Layout.Initialize(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityDirectoryMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityDirectoryMessage.Layout.Initialize(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityDirectoryMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityDirectoryMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityDirectoryMessage.Layout.Size];
    var filled = new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityDirectoryMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityDirectoryMessage.Layout.Initialize(zeroed);
    global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityDirectoryMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityDirectoryMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityDirectoryMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityDirectoryMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Iex.IexEquities.Deep.IexTp.v1_08.SystemEventMessage.Layout>() != global::Iex.IexEquities.Deep.IexTp.v1_08.SystemEventMessage.Layout.Size || Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.SystemEventMessage.Layout.Read(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.SystemEventMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.SystemEventMessage.Layout.Read(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.SystemEventMessage.Layout.Size - 1]); }) || Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.SystemEventMessage.Layout.Initialize(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.SystemEventMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.SystemEventMessage.Layout.Initialize(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.SystemEventMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.SystemEventMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.SystemEventMessage.Layout.Size];
    var filled = new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.SystemEventMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Iex.IexEquities.Deep.IexTp.v1_08.SystemEventMessage.Layout.Initialize(zeroed);
    global::Iex.IexEquities.Deep.IexTp.v1_08.SystemEventMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Iex.IexEquities.Deep.IexTp.v1_08.SystemEventMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Iex.IexEquities.Deep.IexTp.v1_08.SystemEventMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.SystemEventMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout>() != global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout.Size || Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout.Read(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout.Read(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout.Size - 1]); }) || Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout.Initialize(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout.Initialize(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout.Size];
    var filled = new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout.Initialize(zeroed);
    global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Iex.IexEquities.Deep.IexTp.v1_08.IextpHeader.Layout>() != global::Iex.IexEquities.Deep.IexTp.v1_08.IextpHeader.Layout.Size || Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.IextpHeader.Layout.Read(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.IextpHeader.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.IextpHeader.Layout.Read(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.IextpHeader.Layout.Size - 1]); }) || Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.IextpHeader.Layout.Initialize(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.IextpHeader.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.Deep.IexTp.v1_08.IextpHeader.Layout.Initialize(new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.IextpHeader.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.IextpHeader)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.IextpHeader.Layout.Size];
    var filled = new byte[global::Iex.IexEquities.Deep.IexTp.v1_08.IextpHeader.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Iex.IexEquities.Deep.IexTp.v1_08.IextpHeader.Layout.Initialize(zeroed);
    global::Iex.IexEquities.Deep.IexTp.v1_08.IextpHeader.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Iex.IexEquities.Deep.IexTp.v1_08.IextpHeader.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Iex.IexEquities.Deep.IexTp.v1_08.IextpHeader.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.IextpHeader)} Layout does not clear exactly its bytes.");
        return Failure;
    }
    if (global::Iex.IexEquities.Deep.IexTp.v1_08.IextpHeader.Layout.Read(filled).Version.Value != versionStaticValue)
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.IextpHeader)} Layout does not give {nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.IextpHeader.Layout.Version)} its static value.");
        return Failure;
    }
    if (global::Iex.IexEquities.Deep.IexTp.v1_08.IextpHeader.Layout.Read(filled).MessageProtocolId.Value != messageProtocolIdStaticValue)
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.IextpHeader)} Layout does not give {nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.IextpHeader.Layout.MessageProtocolId)} its static value.");
        return Failure;
    }
    if (global::Iex.IexEquities.Deep.IexTp.v1_08.IextpHeader.Layout.Read(filled).ChannelId.Value != channelIdStaticValue)
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.IextpHeader)} Layout does not give {nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.IextpHeader.Layout.ChannelId)} its static value.");
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
if (walker.OnAuctionInformationMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnAuctionInformationMessageCalls)}: {walker.OnAuctionInformationMessageCalls}");
if (walker.OnTradeBreakMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnTradeBreakMessageCalls)}: {walker.OnTradeBreakMessageCalls}");
if (walker.OnOfficialPriceMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnOfficialPriceMessageCalls)}: {walker.OnOfficialPriceMessageCalls}");
if (walker.OnTradeReportMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnTradeReportMessageCalls)}: {walker.OnTradeReportMessageCalls}");
if (walker.OnPriceLevelSellUpdateMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnPriceLevelSellUpdateMessageCalls)}: {walker.OnPriceLevelSellUpdateMessageCalls}");
if (walker.OnPriceLevelBuyUpdateMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnPriceLevelBuyUpdateMessageCalls)}: {walker.OnPriceLevelBuyUpdateMessageCalls}");
if (walker.OnSecurityEventMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnSecurityEventMessageCalls)}: {walker.OnSecurityEventMessageCalls}");
if (walker.OnShortSalePriceTestStatusMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnShortSalePriceTestStatusMessageCalls)}: {walker.OnShortSalePriceTestStatusMessageCalls}");
if (walker.OnOperationalHaltStatusMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnOperationalHaltStatusMessageCalls)}: {walker.OnOperationalHaltStatusMessageCalls}");
if (walker.OnRetailLiquidityIndicatorMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnRetailLiquidityIndicatorMessageCalls)}: {walker.OnRetailLiquidityIndicatorMessageCalls}");
if (walker.OnTradingStatusMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnTradingStatusMessageCalls)}: {walker.OnTradingStatusMessageCalls}");
if (walker.OnSecurityDirectoryMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnSecurityDirectoryMessageCalls)}: {walker.OnSecurityDirectoryMessageCalls}");
if (walker.OnSystemEventMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnSystemEventMessageCalls)}: {walker.OnSystemEventMessageCalls}");
if (walker.OnIextpHeaderCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnIextpHeaderCalls)}: {walker.OnIextpHeaderCalls}");
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
    ///  The number of calls of the OnIextpHeader method.
    /// </summary>
    public long OnIextpHeaderCalls;
    /// <summary>
    ///  The number of calls of the OnSystemEventMessage method.
    /// </summary>
    public long OnSystemEventMessageCalls;
    /// <summary>
    ///  The number of calls of the OnSecurityDirectoryMessage method.
    /// </summary>
    public long OnSecurityDirectoryMessageCalls;
    /// <summary>
    ///  The number of calls of the OnTradingStatusMessage method.
    /// </summary>
    public long OnTradingStatusMessageCalls;
    /// <summary>
    ///  The number of calls of the OnRetailLiquidityIndicatorMessage method.
    /// </summary>
    public long OnRetailLiquidityIndicatorMessageCalls;
    /// <summary>
    ///  The number of calls of the OnOperationalHaltStatusMessage method.
    /// </summary>
    public long OnOperationalHaltStatusMessageCalls;
    /// <summary>
    ///  The number of calls of the OnShortSalePriceTestStatusMessage method.
    /// </summary>
    public long OnShortSalePriceTestStatusMessageCalls;
    /// <summary>
    ///  The number of calls of the OnSecurityEventMessage method.
    /// </summary>
    public long OnSecurityEventMessageCalls;
    /// <summary>
    ///  The number of calls of the OnPriceLevelBuyUpdateMessage method.
    /// </summary>
    public long OnPriceLevelBuyUpdateMessageCalls;
    /// <summary>
    ///  The number of calls of the OnPriceLevelSellUpdateMessage method.
    /// </summary>
    public long OnPriceLevelSellUpdateMessageCalls;
    /// <summary>
    ///  The number of calls of the OnTradeReportMessage method.
    /// </summary>
    public long OnTradeReportMessageCalls;
    /// <summary>
    ///  The number of calls of the OnOfficialPriceMessage method.
    /// </summary>
    public long OnOfficialPriceMessageCalls;
    /// <summary>
    ///  The number of calls of the OnTradeBreakMessage method.
    /// </summary>
    public long OnTradeBreakMessageCalls;
    /// <summary>
    ///  The number of calls of the OnAuctionInformationMessage method.
    /// </summary>
    public long OnAuctionInformationMessageCalls;
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
    public void VisitPacket(global::Iex.IexEquities.Deep.IexTp.v1_08.Packet.Reader packet)
    {
        this.BeginPacketSequence();
        this.SeedPacketSequence(ulong.CreateSaturating(packet.IextpHeader.FirstMessageSequenceNumber.Value));
        this.OnIextpHeader(in packet.IextpHeader);
        var messages = packet.Messages;
        switch (messages.Case)
        {
            case global::Iex.IexEquities.Deep.IexTp.v1_08.Messages.Case.Message:
            {
                var messageEntries = messages.Message;
                var messageRead = 0L;
                while (messageEntries.MoveNext())
                {
                    var message = messageEntries.Current;
                    {
                        ulong? expected = null;
                        expected = this.NextPacketSequence();
                        this.CheckSequence((ulong?)message.SequenceNumber, expected, nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.Message));
                    }
                    this.numberedDepth++;
                    var messageData = message.MessageData;
                    switch (messageData.Case)
                    {
                        case global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.SystemEventMessage:
                        {
                            this.OnSystemEventMessage(in messageData.SystemEventMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.SecurityDirectoryMessage:
                        {
                            this.OnSecurityDirectoryMessage(in messageData.SecurityDirectoryMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.TradingStatusMessage:
                        {
                            this.OnTradingStatusMessage(in messageData.TradingStatusMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.RetailLiquidityIndicatorMessage:
                        {
                            this.OnRetailLiquidityIndicatorMessage(in messageData.RetailLiquidityIndicatorMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.OperationalHaltStatusMessage:
                        {
                            this.OnOperationalHaltStatusMessage(in messageData.OperationalHaltStatusMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.ShortSalePriceTestStatusMessage:
                        {
                            this.OnShortSalePriceTestStatusMessage(in messageData.ShortSalePriceTestStatusMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.SecurityEventMessage:
                        {
                            this.OnSecurityEventMessage(in messageData.SecurityEventMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.PriceLevelBuyUpdateMessage:
                        {
                            this.OnPriceLevelBuyUpdateMessage(in messageData.PriceLevelBuyUpdateMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.PriceLevelSellUpdateMessage:
                        {
                            this.OnPriceLevelSellUpdateMessage(in messageData.PriceLevelSellUpdateMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.TradeReportMessage:
                        {
                            this.OnTradeReportMessage(in messageData.TradeReportMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.OfficialPriceMessage:
                        {
                            this.OnOfficialPriceMessage(in messageData.OfficialPriceMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.TradeBreakMessage:
                        {
                            this.OnTradeBreakMessage(in messageData.TradeBreakMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.AuctionInformationMessage:
                        {
                            this.OnAuctionInformationMessage(in messageData.AuctionInformationMessage, in message.MessageHeader, message.SequenceNumber);
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
    void OnIextpHeader(in global::Iex.IexEquities.Deep.IexTp.v1_08.IextpHeader.Layout value)
    {
        this.Parsed++;
        this.OnIextpHeaderCalls++;
        this.Observe(nameof(OnIextpHeaderCalls));
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
    void OnSystemEventMessage(in global::Iex.IexEquities.Deep.IexTp.v1_08.SystemEventMessage.Layout value, in global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnSecurityDirectoryMessage(in global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityDirectoryMessage.Layout value, in global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout header, ulong sequenceNumber)
    {
        this.Parsed++;
        this.OnSecurityDirectoryMessageCalls++;
        this.Observe(nameof(OnSecurityDirectoryMessageCalls));
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
    void OnTradingStatusMessage(in global::Iex.IexEquities.Deep.IexTp.v1_08.TradingStatusMessage.Layout value, in global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout header, ulong sequenceNumber)
    {
        this.Parsed++;
        this.OnTradingStatusMessageCalls++;
        this.Observe(nameof(OnTradingStatusMessageCalls));
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
    void OnRetailLiquidityIndicatorMessage(in global::Iex.IexEquities.Deep.IexTp.v1_08.RetailLiquidityIndicatorMessage.Layout value, in global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout header, ulong sequenceNumber)
    {
        this.Parsed++;
        this.OnRetailLiquidityIndicatorMessageCalls++;
        this.Observe(nameof(OnRetailLiquidityIndicatorMessageCalls));
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
    void OnOperationalHaltStatusMessage(in global::Iex.IexEquities.Deep.IexTp.v1_08.OperationalHaltStatusMessage.Layout value, in global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout header, ulong sequenceNumber)
    {
        this.Parsed++;
        this.OnOperationalHaltStatusMessageCalls++;
        this.Observe(nameof(OnOperationalHaltStatusMessageCalls));
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
    void OnShortSalePriceTestStatusMessage(in global::Iex.IexEquities.Deep.IexTp.v1_08.ShortSalePriceTestStatusMessage.Layout value, in global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout header, ulong sequenceNumber)
    {
        this.Parsed++;
        this.OnShortSalePriceTestStatusMessageCalls++;
        this.Observe(nameof(OnShortSalePriceTestStatusMessageCalls));
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
    void OnSecurityEventMessage(in global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityEventMessage.Layout value, in global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout header, ulong sequenceNumber)
    {
        this.Parsed++;
        this.OnSecurityEventMessageCalls++;
        this.Observe(nameof(OnSecurityEventMessageCalls));
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
    void OnPriceLevelBuyUpdateMessage(in global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelBuyUpdateMessage.Layout value, in global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout header, ulong sequenceNumber)
    {
        this.Parsed++;
        this.OnPriceLevelBuyUpdateMessageCalls++;
        this.Observe(nameof(OnPriceLevelBuyUpdateMessageCalls));
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
    void OnPriceLevelSellUpdateMessage(in global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelSellUpdateMessage.Layout value, in global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout header, ulong sequenceNumber)
    {
        this.Parsed++;
        this.OnPriceLevelSellUpdateMessageCalls++;
        this.Observe(nameof(OnPriceLevelSellUpdateMessageCalls));
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
    void OnTradeReportMessage(in global::Iex.IexEquities.Deep.IexTp.v1_08.TradeReportMessage.Layout value, in global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout header, ulong sequenceNumber)
    {
        this.Parsed++;
        this.OnTradeReportMessageCalls++;
        this.Observe(nameof(OnTradeReportMessageCalls));
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
    void OnOfficialPriceMessage(in global::Iex.IexEquities.Deep.IexTp.v1_08.OfficialPriceMessage.Layout value, in global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout header, ulong sequenceNumber)
    {
        this.Parsed++;
        this.OnOfficialPriceMessageCalls++;
        this.Observe(nameof(OnOfficialPriceMessageCalls));
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
    void OnTradeBreakMessage(in global::Iex.IexEquities.Deep.IexTp.v1_08.TradeBreakMessage.Layout value, in global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout header, ulong sequenceNumber)
    {
        this.Parsed++;
        this.OnTradeBreakMessageCalls++;
        this.Observe(nameof(OnTradeBreakMessageCalls));
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
    void OnAuctionInformationMessage(in global::Iex.IexEquities.Deep.IexTp.v1_08.AuctionInformationMessage.Layout value, in global::Iex.IexEquities.Deep.IexTp.v1_08.MessageHeader.Layout header, ulong sequenceNumber)
    {
        this.Parsed++;
        this.OnAuctionInformationMessageCalls++;
        this.Observe(nameof(OnAuctionInformationMessageCalls));
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
    void OnPacket(global::Iex.IexEquities.Deep.IexTp.v1_08.Packet.Reader value)
    {
        this.Parsed++;
        this.OnPacketCalls++;
        this.Observe(nameof(OnPacketCalls));
    }
}
