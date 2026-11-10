using Databento.MarketData.Normalized.Dbn.v1;
using Databento.MarketData.Normalized.Dbn.v1.Iterator;
using Databento.MarketData.Normalized.Dbn.v1.Test;
using static Databento.MarketData.Normalized.Dbn.v1.Test.Dump;

const int PcapArgument = 0;
const int FirstOption = 1;
const int None = 0;
const int OneFeed = 1;
const int Success = 0;
const int Failure = 1;
const int UsageFailure = 2;
const string FeedOption = "--feed";
const string StrictOption = "--strict";
const string Usage = $"usage: Databento.MarketData.Normalized.Dbn.v1.Test <pcap> [{FeedOption} <name>] [{StrictOption}]";
const string FileTree = "File";
const string FileAlias = "File";
const string GatewayStreamTree = "GatewayStream";
const string GatewayStreamAlias = "Gateway";
const string ClientStreamTree = "ClientStream";
const string ClientStreamAlias = "Client";

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
var gatewayStreamCapture = Selects(GatewayStreamTree, GatewayStreamAlias) ? new GatewayStreamCapture(global::Omi.Pcap.TcpSide.Unknown) : null;
var clientStreamCapture = Selects(ClientStreamTree, ClientStreamAlias) ? new ClientStreamCapture(global::Omi.Pcap.TcpSide.Unknown) : null;
var gatewayStreamCaptureSplit = gatewayStreamCapture is null ? null : new GatewayStreamCapture(global::Omi.Pcap.TcpSide.Unknown);
var clientStreamCaptureSplit = clientStreamCapture is null ? null : new ClientStreamCapture(global::Omi.Pcap.TcpSide.Unknown);
var fileSelected = feed is not null && Selects(FileTree, FileAlias);
if (feed is not null && feeds != OneFeed)
{
    global::System.Console.Error.WriteLine($"Unknown or ambiguous feed '{feed}'.");
    global::System.Console.Error.WriteLine(Usage);
    return UsageFailure;
}

const byte Filler = 0xA5;
const int Following = 8;
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Databento.MarketData.Normalized.Dbn.v1.BidAskPair.Layout>() != global::Databento.MarketData.Normalized.Dbn.v1.BidAskPair.Layout.Size || Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.BidAskPair.Layout.Read(new byte[global::Databento.MarketData.Normalized.Dbn.v1.BidAskPair.Layout.Size]); }) || !Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.BidAskPair.Layout.Read(new byte[global::Databento.MarketData.Normalized.Dbn.v1.BidAskPair.Layout.Size - 1]); }) || Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.BidAskPair.Layout.Initialize(new byte[global::Databento.MarketData.Normalized.Dbn.v1.BidAskPair.Layout.Size]); }) || !Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.BidAskPair.Layout.Initialize(new byte[global::Databento.MarketData.Normalized.Dbn.v1.BidAskPair.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Databento.MarketData.Normalized.Dbn.v1.BidAskPair)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Databento.MarketData.Normalized.Dbn.v1.BidAskPair.Layout.Size];
    var filled = new byte[global::Databento.MarketData.Normalized.Dbn.v1.BidAskPair.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Databento.MarketData.Normalized.Dbn.v1.BidAskPair.Layout.Initialize(zeroed);
    global::Databento.MarketData.Normalized.Dbn.v1.BidAskPair.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Databento.MarketData.Normalized.Dbn.v1.BidAskPair.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Databento.MarketData.Normalized.Dbn.v1.BidAskPair.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Databento.MarketData.Normalized.Dbn.v1.BidAskPair)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Databento.MarketData.Normalized.Dbn.v1.BboMsg.Layout>() != global::Databento.MarketData.Normalized.Dbn.v1.BboMsg.Layout.LayoutSize || Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.BboMsg.Layout.Read(new byte[global::Databento.MarketData.Normalized.Dbn.v1.BboMsg.Layout.LayoutSize]); }) || !Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.BboMsg.Layout.Read(new byte[global::Databento.MarketData.Normalized.Dbn.v1.BboMsg.Layout.LayoutSize - 1]); }) || Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.BboMsg.Layout.Initialize(new byte[global::Databento.MarketData.Normalized.Dbn.v1.BboMsg.Layout.LayoutSize]); }) || !Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.BboMsg.Layout.Initialize(new byte[global::Databento.MarketData.Normalized.Dbn.v1.BboMsg.Layout.LayoutSize - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Databento.MarketData.Normalized.Dbn.v1.BboMsg)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Databento.MarketData.Normalized.Dbn.v1.BboMsg.Layout.LayoutSize];
    var filled = new byte[global::Databento.MarketData.Normalized.Dbn.v1.BboMsg.Layout.LayoutSize + Following];
    global::System.Array.Fill(filled, Filler);
    global::Databento.MarketData.Normalized.Dbn.v1.BboMsg.Layout.Initialize(zeroed);
    global::Databento.MarketData.Normalized.Dbn.v1.BboMsg.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Databento.MarketData.Normalized.Dbn.v1.BboMsg.Layout.LayoutSize).SequenceEqual(zeroed) || filled.AsSpan(global::Databento.MarketData.Normalized.Dbn.v1.BboMsg.Layout.LayoutSize).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Databento.MarketData.Normalized.Dbn.v1.BboMsg)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Databento.MarketData.Normalized.Dbn.v1.ConsolidatedBidAskPair.Layout>() != global::Databento.MarketData.Normalized.Dbn.v1.ConsolidatedBidAskPair.Layout.Size || Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.ConsolidatedBidAskPair.Layout.Read(new byte[global::Databento.MarketData.Normalized.Dbn.v1.ConsolidatedBidAskPair.Layout.Size]); }) || !Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.ConsolidatedBidAskPair.Layout.Read(new byte[global::Databento.MarketData.Normalized.Dbn.v1.ConsolidatedBidAskPair.Layout.Size - 1]); }) || Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.ConsolidatedBidAskPair.Layout.Initialize(new byte[global::Databento.MarketData.Normalized.Dbn.v1.ConsolidatedBidAskPair.Layout.Size]); }) || !Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.ConsolidatedBidAskPair.Layout.Initialize(new byte[global::Databento.MarketData.Normalized.Dbn.v1.ConsolidatedBidAskPair.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Databento.MarketData.Normalized.Dbn.v1.ConsolidatedBidAskPair)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Databento.MarketData.Normalized.Dbn.v1.ConsolidatedBidAskPair.Layout.Size];
    var filled = new byte[global::Databento.MarketData.Normalized.Dbn.v1.ConsolidatedBidAskPair.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Databento.MarketData.Normalized.Dbn.v1.ConsolidatedBidAskPair.Layout.Initialize(zeroed);
    global::Databento.MarketData.Normalized.Dbn.v1.ConsolidatedBidAskPair.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Databento.MarketData.Normalized.Dbn.v1.ConsolidatedBidAskPair.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Databento.MarketData.Normalized.Dbn.v1.ConsolidatedBidAskPair.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Databento.MarketData.Normalized.Dbn.v1.ConsolidatedBidAskPair)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Databento.MarketData.Normalized.Dbn.v1.CbboMsg.Layout>() != global::Databento.MarketData.Normalized.Dbn.v1.CbboMsg.Layout.LayoutSize || Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.CbboMsg.Layout.Read(new byte[global::Databento.MarketData.Normalized.Dbn.v1.CbboMsg.Layout.LayoutSize]); }) || !Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.CbboMsg.Layout.Read(new byte[global::Databento.MarketData.Normalized.Dbn.v1.CbboMsg.Layout.LayoutSize - 1]); }) || Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.CbboMsg.Layout.Initialize(new byte[global::Databento.MarketData.Normalized.Dbn.v1.CbboMsg.Layout.LayoutSize]); }) || !Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.CbboMsg.Layout.Initialize(new byte[global::Databento.MarketData.Normalized.Dbn.v1.CbboMsg.Layout.LayoutSize - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Databento.MarketData.Normalized.Dbn.v1.CbboMsg)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Databento.MarketData.Normalized.Dbn.v1.CbboMsg.Layout.LayoutSize];
    var filled = new byte[global::Databento.MarketData.Normalized.Dbn.v1.CbboMsg.Layout.LayoutSize + Following];
    global::System.Array.Fill(filled, Filler);
    global::Databento.MarketData.Normalized.Dbn.v1.CbboMsg.Layout.Initialize(zeroed);
    global::Databento.MarketData.Normalized.Dbn.v1.CbboMsg.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Databento.MarketData.Normalized.Dbn.v1.CbboMsg.Layout.LayoutSize).SequenceEqual(zeroed) || filled.AsSpan(global::Databento.MarketData.Normalized.Dbn.v1.CbboMsg.Layout.LayoutSize).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Databento.MarketData.Normalized.Dbn.v1.CbboMsg)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Databento.MarketData.Normalized.Dbn.v1.Cmbp1Msg.Layout>() != global::Databento.MarketData.Normalized.Dbn.v1.Cmbp1Msg.Layout.LayoutSize || Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.Cmbp1Msg.Layout.Read(new byte[global::Databento.MarketData.Normalized.Dbn.v1.Cmbp1Msg.Layout.LayoutSize]); }) || !Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.Cmbp1Msg.Layout.Read(new byte[global::Databento.MarketData.Normalized.Dbn.v1.Cmbp1Msg.Layout.LayoutSize - 1]); }) || Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.Cmbp1Msg.Layout.Initialize(new byte[global::Databento.MarketData.Normalized.Dbn.v1.Cmbp1Msg.Layout.LayoutSize]); }) || !Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.Cmbp1Msg.Layout.Initialize(new byte[global::Databento.MarketData.Normalized.Dbn.v1.Cmbp1Msg.Layout.LayoutSize - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Databento.MarketData.Normalized.Dbn.v1.Cmbp1Msg)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Databento.MarketData.Normalized.Dbn.v1.Cmbp1Msg.Layout.LayoutSize];
    var filled = new byte[global::Databento.MarketData.Normalized.Dbn.v1.Cmbp1Msg.Layout.LayoutSize + Following];
    global::System.Array.Fill(filled, Filler);
    global::Databento.MarketData.Normalized.Dbn.v1.Cmbp1Msg.Layout.Initialize(zeroed);
    global::Databento.MarketData.Normalized.Dbn.v1.Cmbp1Msg.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Databento.MarketData.Normalized.Dbn.v1.Cmbp1Msg.Layout.LayoutSize).SequenceEqual(zeroed) || filled.AsSpan(global::Databento.MarketData.Normalized.Dbn.v1.Cmbp1Msg.Layout.LayoutSize).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Databento.MarketData.Normalized.Dbn.v1.Cmbp1Msg)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Databento.MarketData.Normalized.Dbn.v1.MboMsg.Layout>() != global::Databento.MarketData.Normalized.Dbn.v1.MboMsg.Layout.LayoutSize || Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.MboMsg.Layout.Read(new byte[global::Databento.MarketData.Normalized.Dbn.v1.MboMsg.Layout.LayoutSize]); }) || !Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.MboMsg.Layout.Read(new byte[global::Databento.MarketData.Normalized.Dbn.v1.MboMsg.Layout.LayoutSize - 1]); }) || Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.MboMsg.Layout.Initialize(new byte[global::Databento.MarketData.Normalized.Dbn.v1.MboMsg.Layout.LayoutSize]); }) || !Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.MboMsg.Layout.Initialize(new byte[global::Databento.MarketData.Normalized.Dbn.v1.MboMsg.Layout.LayoutSize - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Databento.MarketData.Normalized.Dbn.v1.MboMsg)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Databento.MarketData.Normalized.Dbn.v1.MboMsg.Layout.LayoutSize];
    var filled = new byte[global::Databento.MarketData.Normalized.Dbn.v1.MboMsg.Layout.LayoutSize + Following];
    global::System.Array.Fill(filled, Filler);
    global::Databento.MarketData.Normalized.Dbn.v1.MboMsg.Layout.Initialize(zeroed);
    global::Databento.MarketData.Normalized.Dbn.v1.MboMsg.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Databento.MarketData.Normalized.Dbn.v1.MboMsg.Layout.LayoutSize).SequenceEqual(zeroed) || filled.AsSpan(global::Databento.MarketData.Normalized.Dbn.v1.MboMsg.Layout.LayoutSize).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Databento.MarketData.Normalized.Dbn.v1.MboMsg)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Databento.MarketData.Normalized.Dbn.v1.StatMsg.Layout>() != global::Databento.MarketData.Normalized.Dbn.v1.StatMsg.Layout.Size || Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.StatMsg.Layout.Read(new byte[global::Databento.MarketData.Normalized.Dbn.v1.StatMsg.Layout.Size]); }) || !Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.StatMsg.Layout.Read(new byte[global::Databento.MarketData.Normalized.Dbn.v1.StatMsg.Layout.Size - 1]); }) || Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.StatMsg.Layout.Initialize(new byte[global::Databento.MarketData.Normalized.Dbn.v1.StatMsg.Layout.Size]); }) || !Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.StatMsg.Layout.Initialize(new byte[global::Databento.MarketData.Normalized.Dbn.v1.StatMsg.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Databento.MarketData.Normalized.Dbn.v1.StatMsg)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Databento.MarketData.Normalized.Dbn.v1.StatMsg.Layout.Size];
    var filled = new byte[global::Databento.MarketData.Normalized.Dbn.v1.StatMsg.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Databento.MarketData.Normalized.Dbn.v1.StatMsg.Layout.Initialize(zeroed);
    global::Databento.MarketData.Normalized.Dbn.v1.StatMsg.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Databento.MarketData.Normalized.Dbn.v1.StatMsg.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Databento.MarketData.Normalized.Dbn.v1.StatMsg.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Databento.MarketData.Normalized.Dbn.v1.StatMsg)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Databento.MarketData.Normalized.Dbn.v1.SystemMsg.Layout>() != global::Databento.MarketData.Normalized.Dbn.v1.SystemMsg.Layout.Size || Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.SystemMsg.Layout.Read(new byte[global::Databento.MarketData.Normalized.Dbn.v1.SystemMsg.Layout.Size]); }) || !Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.SystemMsg.Layout.Read(new byte[global::Databento.MarketData.Normalized.Dbn.v1.SystemMsg.Layout.Size - 1]); }) || Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.SystemMsg.Layout.Initialize(new byte[global::Databento.MarketData.Normalized.Dbn.v1.SystemMsg.Layout.Size]); }) || !Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.SystemMsg.Layout.Initialize(new byte[global::Databento.MarketData.Normalized.Dbn.v1.SystemMsg.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Databento.MarketData.Normalized.Dbn.v1.SystemMsg)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Databento.MarketData.Normalized.Dbn.v1.SystemMsg.Layout.Size];
    var filled = new byte[global::Databento.MarketData.Normalized.Dbn.v1.SystemMsg.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Databento.MarketData.Normalized.Dbn.v1.SystemMsg.Layout.Initialize(zeroed);
    global::Databento.MarketData.Normalized.Dbn.v1.SystemMsg.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Databento.MarketData.Normalized.Dbn.v1.SystemMsg.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Databento.MarketData.Normalized.Dbn.v1.SystemMsg.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Databento.MarketData.Normalized.Dbn.v1.SystemMsg)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Databento.MarketData.Normalized.Dbn.v1.SymbolMappingMsg.Layout>() != global::Databento.MarketData.Normalized.Dbn.v1.SymbolMappingMsg.Layout.Size || Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.SymbolMappingMsg.Layout.Read(new byte[global::Databento.MarketData.Normalized.Dbn.v1.SymbolMappingMsg.Layout.Size]); }) || !Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.SymbolMappingMsg.Layout.Read(new byte[global::Databento.MarketData.Normalized.Dbn.v1.SymbolMappingMsg.Layout.Size - 1]); }) || Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.SymbolMappingMsg.Layout.Initialize(new byte[global::Databento.MarketData.Normalized.Dbn.v1.SymbolMappingMsg.Layout.Size]); }) || !Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.SymbolMappingMsg.Layout.Initialize(new byte[global::Databento.MarketData.Normalized.Dbn.v1.SymbolMappingMsg.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Databento.MarketData.Normalized.Dbn.v1.SymbolMappingMsg)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Databento.MarketData.Normalized.Dbn.v1.SymbolMappingMsg.Layout.Size];
    var filled = new byte[global::Databento.MarketData.Normalized.Dbn.v1.SymbolMappingMsg.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Databento.MarketData.Normalized.Dbn.v1.SymbolMappingMsg.Layout.Initialize(zeroed);
    global::Databento.MarketData.Normalized.Dbn.v1.SymbolMappingMsg.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Databento.MarketData.Normalized.Dbn.v1.SymbolMappingMsg.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Databento.MarketData.Normalized.Dbn.v1.SymbolMappingMsg.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Databento.MarketData.Normalized.Dbn.v1.SymbolMappingMsg)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Databento.MarketData.Normalized.Dbn.v1.ErrorMsg.Layout>() != global::Databento.MarketData.Normalized.Dbn.v1.ErrorMsg.Layout.Size || Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.ErrorMsg.Layout.Read(new byte[global::Databento.MarketData.Normalized.Dbn.v1.ErrorMsg.Layout.Size]); }) || !Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.ErrorMsg.Layout.Read(new byte[global::Databento.MarketData.Normalized.Dbn.v1.ErrorMsg.Layout.Size - 1]); }) || Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.ErrorMsg.Layout.Initialize(new byte[global::Databento.MarketData.Normalized.Dbn.v1.ErrorMsg.Layout.Size]); }) || !Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.ErrorMsg.Layout.Initialize(new byte[global::Databento.MarketData.Normalized.Dbn.v1.ErrorMsg.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Databento.MarketData.Normalized.Dbn.v1.ErrorMsg)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Databento.MarketData.Normalized.Dbn.v1.ErrorMsg.Layout.Size];
    var filled = new byte[global::Databento.MarketData.Normalized.Dbn.v1.ErrorMsg.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Databento.MarketData.Normalized.Dbn.v1.ErrorMsg.Layout.Initialize(zeroed);
    global::Databento.MarketData.Normalized.Dbn.v1.ErrorMsg.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Databento.MarketData.Normalized.Dbn.v1.ErrorMsg.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Databento.MarketData.Normalized.Dbn.v1.ErrorMsg.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Databento.MarketData.Normalized.Dbn.v1.ErrorMsg)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Databento.MarketData.Normalized.Dbn.v1.ImbalanceMsg.Layout>() != global::Databento.MarketData.Normalized.Dbn.v1.ImbalanceMsg.Layout.Size || Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.ImbalanceMsg.Layout.Read(new byte[global::Databento.MarketData.Normalized.Dbn.v1.ImbalanceMsg.Layout.Size]); }) || !Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.ImbalanceMsg.Layout.Read(new byte[global::Databento.MarketData.Normalized.Dbn.v1.ImbalanceMsg.Layout.Size - 1]); }) || Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.ImbalanceMsg.Layout.Initialize(new byte[global::Databento.MarketData.Normalized.Dbn.v1.ImbalanceMsg.Layout.Size]); }) || !Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.ImbalanceMsg.Layout.Initialize(new byte[global::Databento.MarketData.Normalized.Dbn.v1.ImbalanceMsg.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Databento.MarketData.Normalized.Dbn.v1.ImbalanceMsg)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Databento.MarketData.Normalized.Dbn.v1.ImbalanceMsg.Layout.Size];
    var filled = new byte[global::Databento.MarketData.Normalized.Dbn.v1.ImbalanceMsg.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Databento.MarketData.Normalized.Dbn.v1.ImbalanceMsg.Layout.Initialize(zeroed);
    global::Databento.MarketData.Normalized.Dbn.v1.ImbalanceMsg.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Databento.MarketData.Normalized.Dbn.v1.ImbalanceMsg.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Databento.MarketData.Normalized.Dbn.v1.ImbalanceMsg.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Databento.MarketData.Normalized.Dbn.v1.ImbalanceMsg)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Databento.MarketData.Normalized.Dbn.v1.InstrumentDefMsg.Layout>() != global::Databento.MarketData.Normalized.Dbn.v1.InstrumentDefMsg.Layout.Size || Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.InstrumentDefMsg.Layout.Read(new byte[global::Databento.MarketData.Normalized.Dbn.v1.InstrumentDefMsg.Layout.Size]); }) || !Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.InstrumentDefMsg.Layout.Read(new byte[global::Databento.MarketData.Normalized.Dbn.v1.InstrumentDefMsg.Layout.Size - 1]); }) || Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.InstrumentDefMsg.Layout.Initialize(new byte[global::Databento.MarketData.Normalized.Dbn.v1.InstrumentDefMsg.Layout.Size]); }) || !Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.InstrumentDefMsg.Layout.Initialize(new byte[global::Databento.MarketData.Normalized.Dbn.v1.InstrumentDefMsg.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Databento.MarketData.Normalized.Dbn.v1.InstrumentDefMsg)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Databento.MarketData.Normalized.Dbn.v1.InstrumentDefMsg.Layout.Size];
    var filled = new byte[global::Databento.MarketData.Normalized.Dbn.v1.InstrumentDefMsg.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Databento.MarketData.Normalized.Dbn.v1.InstrumentDefMsg.Layout.Initialize(zeroed);
    global::Databento.MarketData.Normalized.Dbn.v1.InstrumentDefMsg.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Databento.MarketData.Normalized.Dbn.v1.InstrumentDefMsg.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Databento.MarketData.Normalized.Dbn.v1.InstrumentDefMsg.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Databento.MarketData.Normalized.Dbn.v1.InstrumentDefMsg)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Databento.MarketData.Normalized.Dbn.v1.StatusMsg.Layout>() != global::Databento.MarketData.Normalized.Dbn.v1.StatusMsg.Layout.Size || Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.StatusMsg.Layout.Read(new byte[global::Databento.MarketData.Normalized.Dbn.v1.StatusMsg.Layout.Size]); }) || !Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.StatusMsg.Layout.Read(new byte[global::Databento.MarketData.Normalized.Dbn.v1.StatusMsg.Layout.Size - 1]); }) || Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.StatusMsg.Layout.Initialize(new byte[global::Databento.MarketData.Normalized.Dbn.v1.StatusMsg.Layout.Size]); }) || !Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.StatusMsg.Layout.Initialize(new byte[global::Databento.MarketData.Normalized.Dbn.v1.StatusMsg.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Databento.MarketData.Normalized.Dbn.v1.StatusMsg)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Databento.MarketData.Normalized.Dbn.v1.StatusMsg.Layout.Size];
    var filled = new byte[global::Databento.MarketData.Normalized.Dbn.v1.StatusMsg.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Databento.MarketData.Normalized.Dbn.v1.StatusMsg.Layout.Initialize(zeroed);
    global::Databento.MarketData.Normalized.Dbn.v1.StatusMsg.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Databento.MarketData.Normalized.Dbn.v1.StatusMsg.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Databento.MarketData.Normalized.Dbn.v1.StatusMsg.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Databento.MarketData.Normalized.Dbn.v1.StatusMsg)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Databento.MarketData.Normalized.Dbn.v1.OhlcvMsg.Layout>() != global::Databento.MarketData.Normalized.Dbn.v1.OhlcvMsg.Layout.Size || Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.OhlcvMsg.Layout.Read(new byte[global::Databento.MarketData.Normalized.Dbn.v1.OhlcvMsg.Layout.Size]); }) || !Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.OhlcvMsg.Layout.Read(new byte[global::Databento.MarketData.Normalized.Dbn.v1.OhlcvMsg.Layout.Size - 1]); }) || Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.OhlcvMsg.Layout.Initialize(new byte[global::Databento.MarketData.Normalized.Dbn.v1.OhlcvMsg.Layout.Size]); }) || !Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.OhlcvMsg.Layout.Initialize(new byte[global::Databento.MarketData.Normalized.Dbn.v1.OhlcvMsg.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Databento.MarketData.Normalized.Dbn.v1.OhlcvMsg)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Databento.MarketData.Normalized.Dbn.v1.OhlcvMsg.Layout.Size];
    var filled = new byte[global::Databento.MarketData.Normalized.Dbn.v1.OhlcvMsg.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Databento.MarketData.Normalized.Dbn.v1.OhlcvMsg.Layout.Initialize(zeroed);
    global::Databento.MarketData.Normalized.Dbn.v1.OhlcvMsg.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Databento.MarketData.Normalized.Dbn.v1.OhlcvMsg.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Databento.MarketData.Normalized.Dbn.v1.OhlcvMsg.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Databento.MarketData.Normalized.Dbn.v1.OhlcvMsg)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Databento.MarketData.Normalized.Dbn.v1.Mbp1Msg.Layout>() != global::Databento.MarketData.Normalized.Dbn.v1.Mbp1Msg.Layout.LayoutSize || Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.Mbp1Msg.Layout.Read(new byte[global::Databento.MarketData.Normalized.Dbn.v1.Mbp1Msg.Layout.LayoutSize]); }) || !Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.Mbp1Msg.Layout.Read(new byte[global::Databento.MarketData.Normalized.Dbn.v1.Mbp1Msg.Layout.LayoutSize - 1]); }) || Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.Mbp1Msg.Layout.Initialize(new byte[global::Databento.MarketData.Normalized.Dbn.v1.Mbp1Msg.Layout.LayoutSize]); }) || !Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.Mbp1Msg.Layout.Initialize(new byte[global::Databento.MarketData.Normalized.Dbn.v1.Mbp1Msg.Layout.LayoutSize - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Databento.MarketData.Normalized.Dbn.v1.Mbp1Msg)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Databento.MarketData.Normalized.Dbn.v1.Mbp1Msg.Layout.LayoutSize];
    var filled = new byte[global::Databento.MarketData.Normalized.Dbn.v1.Mbp1Msg.Layout.LayoutSize + Following];
    global::System.Array.Fill(filled, Filler);
    global::Databento.MarketData.Normalized.Dbn.v1.Mbp1Msg.Layout.Initialize(zeroed);
    global::Databento.MarketData.Normalized.Dbn.v1.Mbp1Msg.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Databento.MarketData.Normalized.Dbn.v1.Mbp1Msg.Layout.LayoutSize).SequenceEqual(zeroed) || filled.AsSpan(global::Databento.MarketData.Normalized.Dbn.v1.Mbp1Msg.Layout.LayoutSize).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Databento.MarketData.Normalized.Dbn.v1.Mbp1Msg)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Databento.MarketData.Normalized.Dbn.v1.TradeMsg.Layout>() != global::Databento.MarketData.Normalized.Dbn.v1.TradeMsg.Layout.LayoutSize || Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.TradeMsg.Layout.Read(new byte[global::Databento.MarketData.Normalized.Dbn.v1.TradeMsg.Layout.LayoutSize]); }) || !Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.TradeMsg.Layout.Read(new byte[global::Databento.MarketData.Normalized.Dbn.v1.TradeMsg.Layout.LayoutSize - 1]); }) || Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.TradeMsg.Layout.Initialize(new byte[global::Databento.MarketData.Normalized.Dbn.v1.TradeMsg.Layout.LayoutSize]); }) || !Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.TradeMsg.Layout.Initialize(new byte[global::Databento.MarketData.Normalized.Dbn.v1.TradeMsg.Layout.LayoutSize - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Databento.MarketData.Normalized.Dbn.v1.TradeMsg)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Databento.MarketData.Normalized.Dbn.v1.TradeMsg.Layout.LayoutSize];
    var filled = new byte[global::Databento.MarketData.Normalized.Dbn.v1.TradeMsg.Layout.LayoutSize + Following];
    global::System.Array.Fill(filled, Filler);
    global::Databento.MarketData.Normalized.Dbn.v1.TradeMsg.Layout.Initialize(zeroed);
    global::Databento.MarketData.Normalized.Dbn.v1.TradeMsg.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Databento.MarketData.Normalized.Dbn.v1.TradeMsg.Layout.LayoutSize).SequenceEqual(zeroed) || filled.AsSpan(global::Databento.MarketData.Normalized.Dbn.v1.TradeMsg.Layout.LayoutSize).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Databento.MarketData.Normalized.Dbn.v1.TradeMsg)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Databento.MarketData.Normalized.Dbn.v1.RecordHeader.Layout>() != global::Databento.MarketData.Normalized.Dbn.v1.RecordHeader.Layout.Size || Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.RecordHeader.Layout.Read(new byte[global::Databento.MarketData.Normalized.Dbn.v1.RecordHeader.Layout.Size]); }) || !Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.RecordHeader.Layout.Read(new byte[global::Databento.MarketData.Normalized.Dbn.v1.RecordHeader.Layout.Size - 1]); }) || Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.RecordHeader.Layout.Initialize(new byte[global::Databento.MarketData.Normalized.Dbn.v1.RecordHeader.Layout.Size]); }) || !Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.RecordHeader.Layout.Initialize(new byte[global::Databento.MarketData.Normalized.Dbn.v1.RecordHeader.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Databento.MarketData.Normalized.Dbn.v1.RecordHeader)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Databento.MarketData.Normalized.Dbn.v1.RecordHeader.Layout.Size];
    var filled = new byte[global::Databento.MarketData.Normalized.Dbn.v1.RecordHeader.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Databento.MarketData.Normalized.Dbn.v1.RecordHeader.Layout.Initialize(zeroed);
    global::Databento.MarketData.Normalized.Dbn.v1.RecordHeader.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Databento.MarketData.Normalized.Dbn.v1.RecordHeader.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Databento.MarketData.Normalized.Dbn.v1.RecordHeader.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Databento.MarketData.Normalized.Dbn.v1.RecordHeader)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Databento.MarketData.Normalized.Dbn.v1.MappingInterval.Layout>() != global::Databento.MarketData.Normalized.Dbn.v1.MappingInterval.Layout.Size || Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.MappingInterval.Layout.Read(new byte[global::Databento.MarketData.Normalized.Dbn.v1.MappingInterval.Layout.Size]); }) || !Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.MappingInterval.Layout.Read(new byte[global::Databento.MarketData.Normalized.Dbn.v1.MappingInterval.Layout.Size - 1]); }) || Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.MappingInterval.Layout.Initialize(new byte[global::Databento.MarketData.Normalized.Dbn.v1.MappingInterval.Layout.Size]); }) || !Throws(() => { global::Databento.MarketData.Normalized.Dbn.v1.MappingInterval.Layout.Initialize(new byte[global::Databento.MarketData.Normalized.Dbn.v1.MappingInterval.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Databento.MarketData.Normalized.Dbn.v1.MappingInterval)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Databento.MarketData.Normalized.Dbn.v1.MappingInterval.Layout.Size];
    var filled = new byte[global::Databento.MarketData.Normalized.Dbn.v1.MappingInterval.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Databento.MarketData.Normalized.Dbn.v1.MappingInterval.Layout.Initialize(zeroed);
    global::Databento.MarketData.Normalized.Dbn.v1.MappingInterval.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Databento.MarketData.Normalized.Dbn.v1.MappingInterval.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Databento.MarketData.Normalized.Dbn.v1.MappingInterval.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Databento.MarketData.Normalized.Dbn.v1.MappingInterval)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
var walker = new Walker();
var splitter = new Walker();
var unread = None;
var invalid = None;
var incomplete = None;
long skipped;
if (fileSelected)
{
    try
    {
        const int ChunkBytes = 65536;
        using var input = global::Omi.Pcap.DataFile.Open(args[PcapArgument]);
        var fileStream = new global::Databento.MarketData.Normalized.Dbn.v1.Iterator.FileStream();
        var chunk = new byte[ChunkBytes];
        int read;
        while (!fileStream.Invalid && (read = input.Read(chunk)) > 0)
        {
            var reading = fileStream.Receive(chunk.AsSpan(..read));
            if (fileStream.Invalid) break;
            try
            {
                {
                    var frames = reading;
                    if (frames.TryGetOpening(out var opening))
                    {
                        walker.VisitFile(opening);
                    }
                    foreach (var record in frames)
                    {
                        walker.VisitRecord(record);
                    }
                }
            }
            catch (global::System.ArgumentOutOfRangeException)
            {
                invalid++;
            }
        }
        if (fileStream.Invalid) invalid++;
        if (fileStream.NeededBytes > 0) incomplete++;
    }
    catch (global::System.Exception exception)
    {
        global::System.Console.Error.WriteLine($"FAIL: {exception.GetType().Name}: {exception.Message}");
        return Failure;
    }

    skipped = None;
    invalid += gatewayStreamCapture?.InvalidFlows ?? None;
    incomplete += gatewayStreamCapture?.IncompleteFlows ?? None;
    unread -= gatewayStreamCapture?.HeldFramesRead ?? None;
    invalid += clientStreamCapture?.InvalidFlows ?? None;
    incomplete += clientStreamCapture?.IncompleteFlows ?? None;
    unread -= clientStreamCapture?.HeldFramesRead ?? None;
    global::System.Console.WriteLine($"Parsed: {walker.Parsed}; invalid: {invalid}; incomplete: {incomplete}; unread frames: {unread}; skipped frames: {skipped}");
    if (walker.OnBboMsgCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnBboMsgCalls)}: {walker.OnBboMsgCalls}");
    if (walker.OnCbboMsgCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnCbboMsgCalls)}: {walker.OnCbboMsgCalls}");
    if (walker.OnCmbp1MsgCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnCmbp1MsgCalls)}: {walker.OnCmbp1MsgCalls}");
    if (walker.OnMboMsgCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnMboMsgCalls)}: {walker.OnMboMsgCalls}");
    if (walker.OnStatMsgCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnStatMsgCalls)}: {walker.OnStatMsgCalls}");
    if (walker.OnSystemMsgCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnSystemMsgCalls)}: {walker.OnSystemMsgCalls}");
    if (walker.OnSymbolMappingMsgCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnSymbolMappingMsgCalls)}: {walker.OnSymbolMappingMsgCalls}");
    if (walker.OnErrorMsgCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnErrorMsgCalls)}: {walker.OnErrorMsgCalls}");
    if (walker.OnImbalanceMsgCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnImbalanceMsgCalls)}: {walker.OnImbalanceMsgCalls}");
    if (walker.OnInstrumentDefMsgCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnInstrumentDefMsgCalls)}: {walker.OnInstrumentDefMsgCalls}");
    if (walker.OnStatusMsgCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnStatusMsgCalls)}: {walker.OnStatusMsgCalls}");
    if (walker.OnOhlcvMsgCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnOhlcvMsgCalls)}: {walker.OnOhlcvMsgCalls}");
    if (walker.OnMbp10MsgCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnMbp10MsgCalls)}: {walker.OnMbp10MsgCalls}");
    if (walker.OnMbp1MsgCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnMbp1MsgCalls)}: {walker.OnMbp1MsgCalls}");
    if (walker.OnTradeMsgCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnTradeMsgCalls)}: {walker.OnTradeMsgCalls}");
    if (walker.OnClientStreamCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnClientStreamCalls)}: {walker.OnClientStreamCalls}");
    if (walker.OnGatewayStreamCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnGatewayStreamCalls)}: {walker.OnGatewayStreamCalls}");
    if (walker.OnFileCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnFileCalls)}: {walker.OnFileCalls}");
    global::System.Console.WriteLine($"Observed: {walker.Observed:X16}");
    global::System.Console.WriteLine($"Checked: {nameof(walker.StableEntries)} {walker.StableEntries}; {nameof(walker.EndedEntries)} {walker.EndedEntries}; {nameof(walker.ActionChecks)} {walker.ActionChecks}; {nameof(walker.SequenceValued)} {walker.SequenceValued}; {nameof(walker.SequenceAbsent)} {walker.SequenceAbsent}; {nameof(walker.Splits)} {walker.Splits}");
    if (splitter.Framed != walker.Framed)
    {
        global::System.Console.Error.WriteLine("FAIL: the reads of the segments split at their midpoint observe the frames differently from the unsplit reads.");
        return Failure;
    }
    return invalid > None || incomplete > None || strict && (walker.Parsed == None || unread > None || skipped > None) ? Failure : Success;
}
try
{
    using var reader = global::Omi.Pcap.PcapReader.Open(args[PcapArgument]);
    while (reader.Advance())
    {
        var read = false;
        walker.Frame = reader.Data.ToArray();
        if (gatewayStreamCapture is not null)
        {
            var opened = gatewayStreamCapture.TryRead(walker.Frame, reader.LinkType, out var reading);
            if (gatewayStreamCapture.FrameRead)
            {
                read = true;
                if (opened)
                {
                    try
                    {
                        {
                            var frames = reading;
                            if (frames.TryGetOpening(out var opening))
                            {
                                walker.VisitGatewayStream(opening);
                            }
                            foreach (var record in frames)
                            {
                                walker.VisitRecord(record);
                            }
                        }
                    }
                    catch (global::System.ArgumentOutOfRangeException)
                    {
                        invalid++;
                    }
                }
                walker.Observe(gatewayStreamCapture.Consumed);
            }
            if (gatewayStreamCaptureSplit is not null && gatewayStreamCaptureSplit.TryGetSegment(walker.Frame, reader.LinkType, out var segment))
            {
                var middle = segment.Length / 2;
                try
                {
                    {
                        var frames = gatewayStreamCaptureSplit.Receive(segment[..middle]);
                        if (frames.TryGetOpening(out var opening))
                        {
                            splitter.VisitGatewayStream(opening);
                        }
                        foreach (var record in frames)
                        {
                            splitter.VisitRecord(record);
                        }
                    }
                    {
                        var frames = gatewayStreamCaptureSplit.Receive(segment[middle..]);
                        if (frames.TryGetOpening(out var opening))
                        {
                            splitter.VisitGatewayStream(opening);
                        }
                        foreach (var record in frames)
                        {
                            splitter.VisitRecord(record);
                        }
                    }
                }
                catch (global::System.ArgumentOutOfRangeException)
                {
                }
                walker.Splits++;
            }
        }
        if (clientStreamCapture is not null)
        {
            var opened = clientStreamCapture.TryRead(walker.Frame, reader.LinkType, out var reading);
            if (clientStreamCapture.FrameRead)
            {
                read = true;
                if (opened)
                {
                    try
                    {
                        foreach (var root in reading)
                        {
                            walker.VisitControlMessage(root);
                        }
                    }
                    catch (global::System.ArgumentOutOfRangeException)
                    {
                        invalid++;
                    }
                }
                walker.Observe(clientStreamCapture.Consumed);
            }
            if (clientStreamCaptureSplit is not null && clientStreamCaptureSplit.TryGetSegment(walker.Frame, reader.LinkType, out var segment))
            {
                var middle = segment.Length / 2;
                try
                {
                    foreach (var root in clientStreamCaptureSplit.Receive(segment[..middle]))
                    {
                        splitter.VisitControlMessage(root);
                    }
                    foreach (var root in clientStreamCaptureSplit.Receive(segment[middle..]))
                    {
                        splitter.VisitControlMessage(root);
                    }
                }
                catch (global::System.ArgumentOutOfRangeException)
                {
                }
                walker.Splits++;
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

invalid += gatewayStreamCapture?.InvalidFlows ?? None;
incomplete += gatewayStreamCapture?.IncompleteFlows ?? None;
unread -= gatewayStreamCapture?.HeldFramesRead ?? None;
invalid += clientStreamCapture?.InvalidFlows ?? None;
incomplete += clientStreamCapture?.IncompleteFlows ?? None;
unread -= clientStreamCapture?.HeldFramesRead ?? None;
global::System.Console.WriteLine($"Parsed: {walker.Parsed}; invalid: {invalid}; incomplete: {incomplete}; unread frames: {unread}; skipped frames: {skipped}");
if (walker.OnBboMsgCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnBboMsgCalls)}: {walker.OnBboMsgCalls}");
if (walker.OnCbboMsgCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnCbboMsgCalls)}: {walker.OnCbboMsgCalls}");
if (walker.OnCmbp1MsgCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnCmbp1MsgCalls)}: {walker.OnCmbp1MsgCalls}");
if (walker.OnMboMsgCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnMboMsgCalls)}: {walker.OnMboMsgCalls}");
if (walker.OnStatMsgCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnStatMsgCalls)}: {walker.OnStatMsgCalls}");
if (walker.OnSystemMsgCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnSystemMsgCalls)}: {walker.OnSystemMsgCalls}");
if (walker.OnSymbolMappingMsgCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnSymbolMappingMsgCalls)}: {walker.OnSymbolMappingMsgCalls}");
if (walker.OnErrorMsgCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnErrorMsgCalls)}: {walker.OnErrorMsgCalls}");
if (walker.OnImbalanceMsgCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnImbalanceMsgCalls)}: {walker.OnImbalanceMsgCalls}");
if (walker.OnInstrumentDefMsgCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnInstrumentDefMsgCalls)}: {walker.OnInstrumentDefMsgCalls}");
if (walker.OnStatusMsgCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnStatusMsgCalls)}: {walker.OnStatusMsgCalls}");
if (walker.OnOhlcvMsgCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnOhlcvMsgCalls)}: {walker.OnOhlcvMsgCalls}");
if (walker.OnMbp10MsgCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnMbp10MsgCalls)}: {walker.OnMbp10MsgCalls}");
if (walker.OnMbp1MsgCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnMbp1MsgCalls)}: {walker.OnMbp1MsgCalls}");
if (walker.OnTradeMsgCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnTradeMsgCalls)}: {walker.OnTradeMsgCalls}");
if (walker.OnClientStreamCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnClientStreamCalls)}: {walker.OnClientStreamCalls}");
if (walker.OnGatewayStreamCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnGatewayStreamCalls)}: {walker.OnGatewayStreamCalls}");
if (walker.OnFileCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnFileCalls)}: {walker.OnFileCalls}");
global::System.Console.WriteLine($"Observed: {walker.Observed:X16}");
global::System.Console.WriteLine($"Checked: {nameof(walker.StableEntries)} {walker.StableEntries}; {nameof(walker.EndedEntries)} {walker.EndedEntries}; {nameof(walker.ActionChecks)} {walker.ActionChecks}; {nameof(walker.SequenceValued)} {walker.SequenceValued}; {nameof(walker.SequenceAbsent)} {walker.SequenceAbsent}; {nameof(walker.Splits)} {walker.Splits}");
if (splitter.Framed != walker.Framed)
{
    global::System.Console.Error.WriteLine("FAIL: the reads of the segments split at their midpoint observe the frames differently from the unsplit reads.");
    return Failure;
}
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
    ///  The number of calls of the OnTradeMsg method.
    /// </summary>
    public long OnTradeMsgCalls;
    /// <summary>
    ///  The number of calls of the OnMbp1Msg method.
    /// </summary>
    public long OnMbp1MsgCalls;
    /// <summary>
    ///  The number of calls of the OnMbp10Msg method.
    /// </summary>
    public long OnMbp10MsgCalls;
    /// <summary>
    ///  The number of calls of the OnOhlcvMsg method.
    /// </summary>
    public long OnOhlcvMsgCalls;
    /// <summary>
    ///  The number of calls of the OnStatusMsg method.
    /// </summary>
    public long OnStatusMsgCalls;
    /// <summary>
    ///  The number of calls of the OnInstrumentDefMsg method.
    /// </summary>
    public long OnInstrumentDefMsgCalls;
    /// <summary>
    ///  The number of calls of the OnImbalanceMsg method.
    /// </summary>
    public long OnImbalanceMsgCalls;
    /// <summary>
    ///  The number of calls of the OnErrorMsg method.
    /// </summary>
    public long OnErrorMsgCalls;
    /// <summary>
    ///  The number of calls of the OnSymbolMappingMsg method.
    /// </summary>
    public long OnSymbolMappingMsgCalls;
    /// <summary>
    ///  The number of calls of the OnSystemMsg method.
    /// </summary>
    public long OnSystemMsgCalls;
    /// <summary>
    ///  The number of calls of the OnStatMsg method.
    /// </summary>
    public long OnStatMsgCalls;
    /// <summary>
    ///  The number of calls of the OnMboMsg method.
    /// </summary>
    public long OnMboMsgCalls;
    /// <summary>
    ///  The number of calls of the OnCmbp1Msg method.
    /// </summary>
    public long OnCmbp1MsgCalls;
    /// <summary>
    ///  The number of calls of the OnCbboMsg method.
    /// </summary>
    public long OnCbboMsgCalls;
    /// <summary>
    ///  The number of calls of the OnBboMsg method.
    /// </summary>
    public long OnBboMsgCalls;
    /// <summary>
    ///  The number of calls of the OnFile method.
    /// </summary>
    public long OnFileCalls;
    /// <summary>
    ///  The number of calls of the OnGatewayStream method.
    /// </summary>
    public long OnGatewayStreamCalls;
    /// <summary>
    ///  The number of calls of the OnClientStream method.
    /// </summary>
    public long OnClientStreamCalls;
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
    /// <summary>
    ///  The root occurrences of segments split at their midpoint whose observations equal those of the unsplit read.
    /// </summary>
    public long Splits;
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
    /// <summary>
    ///  The hash of the observations inside the frames of a stream, apart from the Layout positions.
    /// </summary>
    public ulong Framed;
    /// <summary>
    ///  The number of frames of a stream that contain the current visit.
    /// </summary>
    public int Inside;

    public Walker()
    {
        this.Observed = ObservationBasis;
        this.Frame = [];
        this.Printer = new();
    }

    void Observe(global::System.ReadOnlySpan<byte> bytes)
    {
        foreach (var value in bytes) this.Observed = unchecked((this.Observed ^ value) * ObservationPrime);
        if (this.Inside > 0) foreach (var value in bytes) this.Framed = unchecked((this.Framed ^ value) * ObservationPrime);
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
    public void VisitFile(global::Databento.MarketData.Normalized.Dbn.v1.File.Reader file)
    {
        var recordEntries = file.Record;
        var recordRead = 0L;
        while (recordEntries.MoveNext())
        {
            var record = recordEntries.Current;
            this.Inside++;
            this.VisitRecord(record);
            this.Inside--;
            if (recordEntries.Index != ++recordRead) throw new global::System.InvalidOperationException("The File.Record entries do not number their entries in order.");
            _ = recordEntries.Current;
            if (recordEntries.Index != recordRead) throw new global::System.InvalidOperationException("A second read of the current File.Record entry moves the entries.");
            this.StableEntries++;
        }
        if (recordEntries.MoveNext() || recordEntries.Index != recordRead) throw new global::System.InvalidOperationException("The File.Record entries step after their end, or number their entries differently from the entries read.");
        this.EndedEntries++;
        this.OnFile(file);
    }

    /// <summary>
    ///  Visits the packet and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitGatewayStream(global::Databento.MarketData.Normalized.Dbn.v1.GatewayStream.Reader gatewayStream)
    {
        var recordEntries = gatewayStream.Record;
        var recordRead = 0L;
        while (recordEntries.MoveNext())
        {
            var record = recordEntries.Current;
            this.Inside++;
            this.VisitRecord(record);
            this.Inside--;
            if (recordEntries.Index != ++recordRead) throw new global::System.InvalidOperationException("The GatewayStream.Record entries do not number their entries in order.");
            _ = recordEntries.Current;
            if (recordEntries.Index != recordRead) throw new global::System.InvalidOperationException("A second read of the current GatewayStream.Record entry moves the entries.");
            this.StableEntries++;
        }
        if (recordEntries.MoveNext() || recordEntries.Index != recordRead) throw new global::System.InvalidOperationException("The GatewayStream.Record entries step after their end, or number their entries differently from the entries read.");
        this.EndedEntries++;
        this.OnGatewayStream(gatewayStream);
    }

    /// <summary>
    ///  Visits the packet and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitClientStream(global::Databento.MarketData.Normalized.Dbn.v1.ClientStream.Reader clientStream)
    {
        var controlMessageEntries = clientStream.ControlMessage;
        var controlMessageRead = 0L;
        while (controlMessageEntries.MoveNext())
        {
            var controlMessage = controlMessageEntries.Current;
            this.Inside++;
            this.VisitControlMessage(controlMessage);
            this.Inside--;
            if (controlMessageEntries.Index != ++controlMessageRead) throw new global::System.InvalidOperationException("The ClientStream.ControlMessage entries do not number their entries in order.");
            _ = controlMessageEntries.Current;
            if (controlMessageEntries.Index != controlMessageRead) throw new global::System.InvalidOperationException("A second read of the current ClientStream.ControlMessage entry moves the entries.");
            this.StableEntries++;
        }
        if (controlMessageEntries.MoveNext() || controlMessageEntries.Index != controlMessageRead) throw new global::System.InvalidOperationException("The ClientStream.ControlMessage entries step after their end, or number their entries differently from the entries read.");
        this.EndedEntries++;
        this.OnClientStream(clientStream);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitRecord(global::Databento.MarketData.Normalized.Dbn.v1.Record.Reader record)
    {
        var recordBody = record.RecordBody;
        switch (recordBody.Case)
        {
            case global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.TradeMsg:
            {
                this.OnTradeMsg(in recordBody.TradeMsg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.Mbp1Msg:
            {
                this.OnMbp1Msg(in recordBody.Mbp1Msg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.Mbp10Msg:
            {
                this.OnMbp10Msg(recordBody.Mbp10Msg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.OhlcvMsg:
            {
                this.OnOhlcvMsg(in recordBody.OhlcvMsg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.StatusMsg:
            {
                this.OnStatusMsg(in recordBody.StatusMsg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.InstrumentDefMsg:
            {
                this.OnInstrumentDefMsg(in recordBody.InstrumentDefMsg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.ImbalanceMsg:
            {
                this.OnImbalanceMsg(in recordBody.ImbalanceMsg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.ErrorMsg:
            {
                this.OnErrorMsg(in recordBody.ErrorMsg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.SymbolMappingMsg:
            {
                this.OnSymbolMappingMsg(in recordBody.SymbolMappingMsg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.SystemMsg:
            {
                this.OnSystemMsg(in recordBody.SystemMsg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.StatMsg:
            {
                this.OnStatMsg(in recordBody.StatMsg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.MboMsg:
            {
                this.OnMboMsg(in recordBody.MboMsg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.Cmbp1Msg:
            {
                this.OnCmbp1Msg(in recordBody.Cmbp1Msg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.CbboMsg:
            {
                this.OnCbboMsg(in recordBody.CbboMsg);
                break;
            }
            case global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.BboMsg:
            {
                this.OnBboMsg(in recordBody.BboMsg);
                break;
            }
        }
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitControlMessage(global::Databento.MarketData.Normalized.Dbn.v1.ControlMessage.Reader controlMessage)
    {
    }
    void OnTradeMsg(in global::Databento.MarketData.Normalized.Dbn.v1.TradeMsg.Layout value)
    {
        this.Parsed++;
        this.OnTradeMsgCalls++;
        this.Observe(nameof(OnTradeMsgCalls));
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
    void OnMbp1Msg(in global::Databento.MarketData.Normalized.Dbn.v1.Mbp1Msg.Layout value)
    {
        this.Parsed++;
        this.OnMbp1MsgCalls++;
        this.Observe(nameof(OnMbp1MsgCalls));
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
    void OnMbp10Msg(global::Databento.MarketData.Normalized.Dbn.v1.Mbp10Msg.Reader value)
    {
        this.Parsed++;
        this.OnMbp10MsgCalls++;
        this.Observe(nameof(OnMbp10MsgCalls));
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                var reader = value;
                var indent = Dump.Indent;
                Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Mbp10Msg.Reader.Price), reader.Price);
                Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Mbp10Msg.Reader.Size), reader.Size);
                Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Mbp10Msg.Reader.Action), reader.Action);
                Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Mbp10Msg.Reader.Side), reader.Side);
                Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Mbp10Msg.Reader.Flags), reader.Flags);
                Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Mbp10Msg.Reader.Depth), reader.Depth);
                Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Mbp10Msg.Reader.TsRecv), reader.TsRecv);
                Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Mbp10Msg.Reader.TsInDelta), reader.TsInDelta);
                Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Mbp10Msg.Reader.Sequence), reader.Sequence);
                foreach (var entry in reader.BidAskPair)
                {
                    Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Mbp10Msg.Reader.BidAskPair));
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
            var entries = value.BidAskPair;
            var read = entries.Index;
            var ordered = true;
            while (entries.MoveNext()) ordered &= entries.Index == ++read;
            this.Observe(read);
            if (!ordered || read != entries.Count || entries.MoveNext()) throw new global::System.InvalidOperationException("The Mbp10Msg.Reader.BidAskPair entries differ from their index or count.");
        }
    }
    void OnOhlcvMsg(in global::Databento.MarketData.Normalized.Dbn.v1.OhlcvMsg.Layout value)
    {
        this.Parsed++;
        this.OnOhlcvMsgCalls++;
        this.Observe(nameof(OnOhlcvMsgCalls));
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
    void OnStatusMsg(in global::Databento.MarketData.Normalized.Dbn.v1.StatusMsg.Layout value)
    {
        this.Parsed++;
        this.OnStatusMsgCalls++;
        this.Observe(nameof(OnStatusMsgCalls));
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
    void OnInstrumentDefMsg(in global::Databento.MarketData.Normalized.Dbn.v1.InstrumentDefMsg.Layout value)
    {
        this.Parsed++;
        this.OnInstrumentDefMsgCalls++;
        this.Observe(nameof(OnInstrumentDefMsgCalls));
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
    void OnImbalanceMsg(in global::Databento.MarketData.Normalized.Dbn.v1.ImbalanceMsg.Layout value)
    {
        this.Parsed++;
        this.OnImbalanceMsgCalls++;
        this.Observe(nameof(OnImbalanceMsgCalls));
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
    void OnErrorMsg(in global::Databento.MarketData.Normalized.Dbn.v1.ErrorMsg.Layout value)
    {
        this.Parsed++;
        this.OnErrorMsgCalls++;
        this.Observe(nameof(OnErrorMsgCalls));
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
    void OnSymbolMappingMsg(in global::Databento.MarketData.Normalized.Dbn.v1.SymbolMappingMsg.Layout value)
    {
        this.Parsed++;
        this.OnSymbolMappingMsgCalls++;
        this.Observe(nameof(OnSymbolMappingMsgCalls));
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
    void OnSystemMsg(in global::Databento.MarketData.Normalized.Dbn.v1.SystemMsg.Layout value)
    {
        this.Parsed++;
        this.OnSystemMsgCalls++;
        this.Observe(nameof(OnSystemMsgCalls));
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
    void OnStatMsg(in global::Databento.MarketData.Normalized.Dbn.v1.StatMsg.Layout value)
    {
        this.Parsed++;
        this.OnStatMsgCalls++;
        this.Observe(nameof(OnStatMsgCalls));
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
    void OnMboMsg(in global::Databento.MarketData.Normalized.Dbn.v1.MboMsg.Layout value)
    {
        this.Parsed++;
        this.OnMboMsgCalls++;
        this.Observe(nameof(OnMboMsgCalls));
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
    void OnCmbp1Msg(in global::Databento.MarketData.Normalized.Dbn.v1.Cmbp1Msg.Layout value)
    {
        this.Parsed++;
        this.OnCmbp1MsgCalls++;
        this.Observe(nameof(OnCmbp1MsgCalls));
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
    void OnCbboMsg(in global::Databento.MarketData.Normalized.Dbn.v1.CbboMsg.Layout value)
    {
        this.Parsed++;
        this.OnCbboMsgCalls++;
        this.Observe(nameof(OnCbboMsgCalls));
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
    void OnBboMsg(in global::Databento.MarketData.Normalized.Dbn.v1.BboMsg.Layout value)
    {
        this.Parsed++;
        this.OnBboMsgCalls++;
        this.Observe(nameof(OnBboMsgCalls));
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
    void OnFile(global::Databento.MarketData.Normalized.Dbn.v1.File.Reader value)
    {
        this.Parsed++;
        this.OnFileCalls++;
        this.Observe(nameof(OnFileCalls));
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                var reader = value;
                var indent = Dump.Indent;
                Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.File.Reader.Metadata));
                Print(reader.Metadata, indent + Indent);
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
    }
    void OnGatewayStream(global::Databento.MarketData.Normalized.Dbn.v1.GatewayStream.Reader value)
    {
        this.Parsed++;
        this.OnGatewayStreamCalls++;
        this.Observe(nameof(OnGatewayStreamCalls));
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                var reader = value;
                var indent = Dump.Indent;
                Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.GatewayStream.Reader.Greeting));
                Print(reader.Greeting, indent + Indent);
                Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.GatewayStream.Reader.ChallengeRequest));
                Print(reader.ChallengeRequest, indent + Indent);
                Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.GatewayStream.Reader.AuthenticationResponse));
                Print(reader.AuthenticationResponse, indent + Indent);
                Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.GatewayStream.Reader.Metadata));
                Print(reader.Metadata, indent + Indent);
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
    }
    void OnClientStream(global::Databento.MarketData.Normalized.Dbn.v1.ClientStream.Reader value)
    {
        this.Parsed++;
        this.OnClientStreamCalls++;
        this.Observe(nameof(OnClientStreamCalls));
    }
}
