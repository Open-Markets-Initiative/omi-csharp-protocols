using Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;
using Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Iterator;
using Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Test;
using static Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Test.Dump;

const int PcapArgument = 0;
const int FirstOption = 1;
const int None = 0;
const int OneFeed = 1;
const int Success = 0;
const int Failure = 1;
const int UsageFailure = 2;
const string FeedOption = "--feed";
const string StrictOption = "--strict";
const string Usage = $"usage: Nasdaq.NsmEquities.TotalView.Itch.v5.0.2023.Test <pcap> [{FeedOption} <name>] [{StrictOption}]";
const string ClientPacketTree = "ClientPacket";
const string ClientPacketAlias = "Client";
const string ServerPacketTree = "ServerPacket";
const string ServerPacketAlias = "Server";
const string PacketTree = "Packet";
const string PacketAlias = "Packet";
const string FileTree = "File";
const string FileAlias = "File";

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
var clientPacketCapture = Selects(ClientPacketTree, ClientPacketAlias) ? new ClientPacketCapture(global::Omi.Pcap.TcpSide.Unknown) : null;
var serverPacketCapture = Selects(ServerPacketTree, ServerPacketAlias) ? new ServerPacketCapture(global::Omi.Pcap.TcpSide.Unknown) : null;
var packetCapture = Selects(PacketTree, PacketAlias) ? new PacketCapture() : null;
var clientPacketCaptureSplit = clientPacketCapture is null ? null : new ClientPacketCapture(global::Omi.Pcap.TcpSide.Unknown);
var serverPacketCaptureSplit = serverPacketCapture is null ? null : new ServerPacketCapture(global::Omi.Pcap.TcpSide.Unknown);
var fileSelected = feed is not null && Selects(FileTree, FileAlias);
if (feed is not null && feeds != OneFeed)
{
    global::System.Console.Error.WriteLine($"Unknown or ambiguous feed '{feed}'.");
    global::System.Console.Error.WriteLine(Usage);
    return UsageFailure;
}

const byte Filler = 0xA5;
const int Following = 8;
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout>() != global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DirectListingWithCapitalRaisePriceDiscoveryMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DirectListingWithCapitalRaisePriceDiscoveryMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RetailPriceImprovementIndicatorMessage.Layout>() != global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RetailPriceImprovementIndicatorMessage.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RetailPriceImprovementIndicatorMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RetailPriceImprovementIndicatorMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RetailPriceImprovementIndicatorMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RetailPriceImprovementIndicatorMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RetailPriceImprovementIndicatorMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RetailPriceImprovementIndicatorMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RetailPriceImprovementIndicatorMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RetailPriceImprovementIndicatorMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RetailPriceImprovementIndicatorMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RetailPriceImprovementIndicatorMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RetailPriceImprovementIndicatorMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RetailPriceImprovementIndicatorMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RetailPriceImprovementIndicatorMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RetailPriceImprovementIndicatorMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RetailPriceImprovementIndicatorMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RetailPriceImprovementIndicatorMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NetOrderImbalanceIndicatorMessage.Layout>() != global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NetOrderImbalanceIndicatorMessage.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NetOrderImbalanceIndicatorMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NetOrderImbalanceIndicatorMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NetOrderImbalanceIndicatorMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NetOrderImbalanceIndicatorMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NetOrderImbalanceIndicatorMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NetOrderImbalanceIndicatorMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NetOrderImbalanceIndicatorMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NetOrderImbalanceIndicatorMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NetOrderImbalanceIndicatorMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NetOrderImbalanceIndicatorMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NetOrderImbalanceIndicatorMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NetOrderImbalanceIndicatorMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NetOrderImbalanceIndicatorMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NetOrderImbalanceIndicatorMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NetOrderImbalanceIndicatorMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NetOrderImbalanceIndicatorMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.BrokenTradeMessage.Layout>() != global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.BrokenTradeMessage.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.BrokenTradeMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.BrokenTradeMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.BrokenTradeMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.BrokenTradeMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.BrokenTradeMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.BrokenTradeMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.BrokenTradeMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.BrokenTradeMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.BrokenTradeMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.BrokenTradeMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.BrokenTradeMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.BrokenTradeMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.BrokenTradeMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.BrokenTradeMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.BrokenTradeMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.BrokenTradeMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossTradeMessage.Layout>() != global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossTradeMessage.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossTradeMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossTradeMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossTradeMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossTradeMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossTradeMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossTradeMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossTradeMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossTradeMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossTradeMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossTradeMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossTradeMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossTradeMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossTradeMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossTradeMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossTradeMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossTradeMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NonCrossTradeMessage.Layout>() != global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NonCrossTradeMessage.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NonCrossTradeMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NonCrossTradeMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NonCrossTradeMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NonCrossTradeMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NonCrossTradeMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NonCrossTradeMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NonCrossTradeMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NonCrossTradeMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NonCrossTradeMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NonCrossTradeMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NonCrossTradeMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NonCrossTradeMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NonCrossTradeMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NonCrossTradeMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NonCrossTradeMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NonCrossTradeMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderReplaceMessage.Layout>() != global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderReplaceMessage.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderReplaceMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderReplaceMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderReplaceMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderReplaceMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderReplaceMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderReplaceMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderReplaceMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderReplaceMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderReplaceMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderReplaceMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderReplaceMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderReplaceMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderReplaceMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderReplaceMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderReplaceMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderReplaceMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderDeleteMessage.Layout>() != global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderDeleteMessage.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderDeleteMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderDeleteMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderDeleteMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderDeleteMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderDeleteMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderDeleteMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderDeleteMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderDeleteMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderDeleteMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderDeleteMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderDeleteMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderDeleteMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderDeleteMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderDeleteMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderDeleteMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderDeleteMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderCancelMessage.Layout>() != global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderCancelMessage.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderCancelMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderCancelMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderCancelMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderCancelMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderCancelMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderCancelMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderCancelMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderCancelMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderCancelMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderCancelMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderCancelMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderCancelMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderCancelMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderCancelMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderCancelMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderCancelMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedWithPriceMessage.Layout>() != global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedWithPriceMessage.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedWithPriceMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedWithPriceMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedWithPriceMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedWithPriceMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedWithPriceMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedWithPriceMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedWithPriceMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedWithPriceMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedWithPriceMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedWithPriceMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedWithPriceMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedWithPriceMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedWithPriceMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedWithPriceMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedWithPriceMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedWithPriceMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedMessage.Layout>() != global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedMessage.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderWithMpidAttributionMessage.Layout>() != global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderWithMpidAttributionMessage.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderWithMpidAttributionMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderWithMpidAttributionMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderWithMpidAttributionMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderWithMpidAttributionMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderWithMpidAttributionMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderWithMpidAttributionMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderWithMpidAttributionMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderWithMpidAttributionMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderWithMpidAttributionMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderWithMpidAttributionMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderWithMpidAttributionMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderWithMpidAttributionMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderWithMpidAttributionMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderWithMpidAttributionMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderWithMpidAttributionMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderWithMpidAttributionMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderNoMpidAttributionMessage.Layout>() != global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderNoMpidAttributionMessage.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderNoMpidAttributionMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderNoMpidAttributionMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderNoMpidAttributionMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderNoMpidAttributionMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderNoMpidAttributionMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderNoMpidAttributionMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderNoMpidAttributionMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderNoMpidAttributionMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderNoMpidAttributionMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderNoMpidAttributionMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderNoMpidAttributionMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderNoMpidAttributionMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderNoMpidAttributionMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderNoMpidAttributionMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderNoMpidAttributionMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderNoMpidAttributionMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OperationalHaltMessage.Layout>() != global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OperationalHaltMessage.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OperationalHaltMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OperationalHaltMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OperationalHaltMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OperationalHaltMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OperationalHaltMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OperationalHaltMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OperationalHaltMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OperationalHaltMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OperationalHaltMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OperationalHaltMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OperationalHaltMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OperationalHaltMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OperationalHaltMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OperationalHaltMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OperationalHaltMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OperationalHaltMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LuldAuctionCollarMessage.Layout>() != global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LuldAuctionCollarMessage.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LuldAuctionCollarMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LuldAuctionCollarMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LuldAuctionCollarMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LuldAuctionCollarMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LuldAuctionCollarMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LuldAuctionCollarMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LuldAuctionCollarMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LuldAuctionCollarMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LuldAuctionCollarMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LuldAuctionCollarMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LuldAuctionCollarMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LuldAuctionCollarMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LuldAuctionCollarMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LuldAuctionCollarMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LuldAuctionCollarMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LuldAuctionCollarMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.IpoQuotingPeriodUpdate.Layout>() != global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.IpoQuotingPeriodUpdate.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.IpoQuotingPeriodUpdate.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.IpoQuotingPeriodUpdate.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.IpoQuotingPeriodUpdate.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.IpoQuotingPeriodUpdate.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.IpoQuotingPeriodUpdate.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.IpoQuotingPeriodUpdate.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.IpoQuotingPeriodUpdate.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.IpoQuotingPeriodUpdate.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.IpoQuotingPeriodUpdate)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.IpoQuotingPeriodUpdate.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.IpoQuotingPeriodUpdate.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.IpoQuotingPeriodUpdate.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.IpoQuotingPeriodUpdate.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.IpoQuotingPeriodUpdate.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.IpoQuotingPeriodUpdate.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.IpoQuotingPeriodUpdate)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbStatusLevelMessage.Layout>() != global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbStatusLevelMessage.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbStatusLevelMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbStatusLevelMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbStatusLevelMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbStatusLevelMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbStatusLevelMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbStatusLevelMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbStatusLevelMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbStatusLevelMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbStatusLevelMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbStatusLevelMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbStatusLevelMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbStatusLevelMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbStatusLevelMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbStatusLevelMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbStatusLevelMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbStatusLevelMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbDeclineLevelMessage.Layout>() != global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbDeclineLevelMessage.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbDeclineLevelMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbDeclineLevelMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbDeclineLevelMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbDeclineLevelMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbDeclineLevelMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbDeclineLevelMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbDeclineLevelMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbDeclineLevelMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbDeclineLevelMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbDeclineLevelMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbDeclineLevelMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbDeclineLevelMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbDeclineLevelMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbDeclineLevelMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbDeclineLevelMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbDeclineLevelMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MarketParticipantPositionMessage.Layout>() != global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MarketParticipantPositionMessage.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MarketParticipantPositionMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MarketParticipantPositionMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MarketParticipantPositionMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MarketParticipantPositionMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MarketParticipantPositionMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MarketParticipantPositionMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MarketParticipantPositionMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MarketParticipantPositionMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MarketParticipantPositionMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MarketParticipantPositionMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MarketParticipantPositionMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MarketParticipantPositionMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MarketParticipantPositionMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MarketParticipantPositionMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MarketParticipantPositionMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MarketParticipantPositionMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout>() != global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RegShoShortSalePriceTestRestrictedIndicatorMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RegShoShortSalePriceTestRestrictedIndicatorMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockTradingActionMessage.Layout>() != global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockTradingActionMessage.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockTradingActionMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockTradingActionMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockTradingActionMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockTradingActionMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockTradingActionMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockTradingActionMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockTradingActionMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockTradingActionMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockTradingActionMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockTradingActionMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockTradingActionMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockTradingActionMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockTradingActionMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockTradingActionMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockTradingActionMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockTradingActionMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockDirectoryMessage.Layout>() != global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockDirectoryMessage.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockDirectoryMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockDirectoryMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockDirectoryMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockDirectoryMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockDirectoryMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockDirectoryMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockDirectoryMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockDirectoryMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockDirectoryMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockDirectoryMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockDirectoryMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockDirectoryMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockDirectoryMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockDirectoryMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockDirectoryMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockDirectoryMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SystemEventMessage.Layout>() != global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SystemEventMessage.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SystemEventMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SystemEventMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SystemEventMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SystemEventMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SystemEventMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SystemEventMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SystemEventMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SystemEventMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SystemEventMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SystemEventMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SystemEventMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SystemEventMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SystemEventMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SystemEventMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SystemEventMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SystemEventMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout>() != global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.PacketHeader.Layout>() != global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.PacketHeader.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.PacketHeader.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.PacketHeader.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.PacketHeader.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.PacketHeader.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.PacketHeader.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.PacketHeader.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.PacketHeader.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.PacketHeader.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.PacketHeader)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.PacketHeader.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.PacketHeader.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.PacketHeader.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.PacketHeader.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.PacketHeader.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.PacketHeader.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.PacketHeader)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRejectedPacket.Layout>() != global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRejectedPacket.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRejectedPacket.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRejectedPacket.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRejectedPacket.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRejectedPacket.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRejectedPacket.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRejectedPacket.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRejectedPacket.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRejectedPacket.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRejectedPacket)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRejectedPacket.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRejectedPacket.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRejectedPacket.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRejectedPacket.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRejectedPacket.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRejectedPacket.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRejectedPacket)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginAcceptedPacket.Layout>() != global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginAcceptedPacket.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginAcceptedPacket.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginAcceptedPacket.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginAcceptedPacket.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginAcceptedPacket.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginAcceptedPacket.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginAcceptedPacket.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginAcceptedPacket.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginAcceptedPacket.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginAcceptedPacket)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginAcceptedPacket.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginAcceptedPacket.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginAcceptedPacket.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginAcceptedPacket.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginAcceptedPacket.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginAcceptedPacket.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginAcceptedPacket)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPacketHeader.Layout>() != global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPacketHeader.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPacketHeader.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPacketHeader.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPacketHeader.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPacketHeader.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPacketHeader.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPacketHeader.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPacketHeader.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPacketHeader.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPacketHeader)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPacketHeader.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPacketHeader.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPacketHeader.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPacketHeader.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPacketHeader.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPacketHeader.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPacketHeader)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRequestPacket.Layout>() != global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRequestPacket.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRequestPacket.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRequestPacket.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRequestPacket.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRequestPacket.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRequestPacket.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRequestPacket.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRequestPacket.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRequestPacket.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRequestPacket)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRequestPacket.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRequestPacket.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRequestPacket.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRequestPacket.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRequestPacket.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRequestPacket.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRequestPacket)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPacketHeader.Layout>() != global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPacketHeader.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPacketHeader.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPacketHeader.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPacketHeader.Layout.Read(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPacketHeader.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPacketHeader.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPacketHeader.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPacketHeader.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPacketHeader.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPacketHeader)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPacketHeader.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPacketHeader.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPacketHeader.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPacketHeader.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPacketHeader.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPacketHeader.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPacketHeader)} Layout does not clear exactly its bytes.");
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
        var fileStream = new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Iterator.FileStream();
        var chunk = new byte[ChunkBytes];
        int read;
        while (!fileStream.Invalid && (read = input.Read(chunk)) > 0)
        {
            var reading = fileStream.Receive(chunk.AsSpan(..read));
            if (fileStream.Invalid) break;
            try
            {
                foreach (var root in reading)
                {
                    walker.VisitFileMessage(root);
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
    invalid += clientPacketCapture?.InvalidFlows ?? None;
    incomplete += clientPacketCapture?.IncompleteFlows ?? None;
    unread -= clientPacketCapture?.HeldFramesRead ?? None;
    invalid += serverPacketCapture?.InvalidFlows ?? None;
    incomplete += serverPacketCapture?.IncompleteFlows ?? None;
    unread -= serverPacketCapture?.HeldFramesRead ?? None;
    global::System.Console.WriteLine($"Parsed: {walker.Parsed}; invalid: {invalid}; incomplete: {incomplete}; unread frames: {unread}; skipped frames: {skipped}");
    if (walker.OnDirectListingWithCapitalRaisePriceDiscoveryMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnDirectListingWithCapitalRaisePriceDiscoveryMessageCalls)}: {walker.OnDirectListingWithCapitalRaisePriceDiscoveryMessageCalls}");
    if (walker.OnRetailPriceImprovementIndicatorMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnRetailPriceImprovementIndicatorMessageCalls)}: {walker.OnRetailPriceImprovementIndicatorMessageCalls}");
    if (walker.OnNetOrderImbalanceIndicatorMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnNetOrderImbalanceIndicatorMessageCalls)}: {walker.OnNetOrderImbalanceIndicatorMessageCalls}");
    if (walker.OnBrokenTradeMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnBrokenTradeMessageCalls)}: {walker.OnBrokenTradeMessageCalls}");
    if (walker.OnCrossTradeMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnCrossTradeMessageCalls)}: {walker.OnCrossTradeMessageCalls}");
    if (walker.OnNonCrossTradeMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnNonCrossTradeMessageCalls)}: {walker.OnNonCrossTradeMessageCalls}");
    if (walker.OnOrderReplaceMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnOrderReplaceMessageCalls)}: {walker.OnOrderReplaceMessageCalls}");
    if (walker.OnOrderDeleteMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnOrderDeleteMessageCalls)}: {walker.OnOrderDeleteMessageCalls}");
    if (walker.OnOrderCancelMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnOrderCancelMessageCalls)}: {walker.OnOrderCancelMessageCalls}");
    if (walker.OnOrderExecutedWithPriceMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnOrderExecutedWithPriceMessageCalls)}: {walker.OnOrderExecutedWithPriceMessageCalls}");
    if (walker.OnOrderExecutedMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnOrderExecutedMessageCalls)}: {walker.OnOrderExecutedMessageCalls}");
    if (walker.OnAddOrderWithMpidAttributionMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnAddOrderWithMpidAttributionMessageCalls)}: {walker.OnAddOrderWithMpidAttributionMessageCalls}");
    if (walker.OnAddOrderNoMpidAttributionMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnAddOrderNoMpidAttributionMessageCalls)}: {walker.OnAddOrderNoMpidAttributionMessageCalls}");
    if (walker.OnOperationalHaltMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnOperationalHaltMessageCalls)}: {walker.OnOperationalHaltMessageCalls}");
    if (walker.OnLuldAuctionCollarMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnLuldAuctionCollarMessageCalls)}: {walker.OnLuldAuctionCollarMessageCalls}");
    if (walker.OnIpoQuotingPeriodUpdateCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnIpoQuotingPeriodUpdateCalls)}: {walker.OnIpoQuotingPeriodUpdateCalls}");
    if (walker.OnMwcbStatusLevelMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnMwcbStatusLevelMessageCalls)}: {walker.OnMwcbStatusLevelMessageCalls}");
    if (walker.OnMwcbDeclineLevelMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnMwcbDeclineLevelMessageCalls)}: {walker.OnMwcbDeclineLevelMessageCalls}");
    if (walker.OnMarketParticipantPositionMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnMarketParticipantPositionMessageCalls)}: {walker.OnMarketParticipantPositionMessageCalls}");
    if (walker.OnRegShoShortSalePriceTestRestrictedIndicatorMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnRegShoShortSalePriceTestRestrictedIndicatorMessageCalls)}: {walker.OnRegShoShortSalePriceTestRestrictedIndicatorMessageCalls}");
    if (walker.OnStockTradingActionMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnStockTradingActionMessageCalls)}: {walker.OnStockTradingActionMessageCalls}");
    if (walker.OnStockDirectoryMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnStockDirectoryMessageCalls)}: {walker.OnStockDirectoryMessageCalls}");
    if (walker.OnSystemEventMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnSystemEventMessageCalls)}: {walker.OnSystemEventMessageCalls}");
    if (walker.OnEndOfSessionCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnEndOfSessionCalls)}: {walker.OnEndOfSessionCalls}");
    if (walker.OnHeartbeatCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnHeartbeatCalls)}: {walker.OnHeartbeatCalls}");
    if (walker.OnServerHeartbeatCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnServerHeartbeatCalls)}: {walker.OnServerHeartbeatCalls}");
    if (walker.OnSequencedDataPacketCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnSequencedDataPacketCalls)}: {walker.OnSequencedDataPacketCalls}");
    if (walker.OnLoginRejectedPacketCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnLoginRejectedPacketCalls)}: {walker.OnLoginRejectedPacketCalls}");
    if (walker.OnLoginAcceptedPacketCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnLoginAcceptedPacketCalls)}: {walker.OnLoginAcceptedPacketCalls}");
    if (walker.OnDebugPacketCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnDebugPacketCalls)}: {walker.OnDebugPacketCalls}");
    if (walker.OnLogoutRequestCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnLogoutRequestCalls)}: {walker.OnLogoutRequestCalls}");
    if (walker.OnClientHeartbeatCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnClientHeartbeatCalls)}: {walker.OnClientHeartbeatCalls}");
    if (walker.OnUnsequencedDataPacketCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnUnsequencedDataPacketCalls)}: {walker.OnUnsequencedDataPacketCalls}");
    if (walker.OnLoginRequestPacketCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnLoginRequestPacketCalls)}: {walker.OnLoginRequestPacketCalls}");
    if (walker.OnFileCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnFileCalls)}: {walker.OnFileCalls}");
    if (walker.OnPacketHeaderCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnPacketHeaderCalls)}: {walker.OnPacketHeaderCalls}");
    if (walker.OnPacketCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnPacketCalls)}: {walker.OnPacketCalls}");
    if (walker.OnServerPacketHeaderCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnServerPacketHeaderCalls)}: {walker.OnServerPacketHeaderCalls}");
    if (walker.OnServerPacketCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnServerPacketCalls)}: {walker.OnServerPacketCalls}");
    if (walker.OnClientPacketHeaderCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnClientPacketHeaderCalls)}: {walker.OnClientPacketHeaderCalls}");
    if (walker.OnClientPacketCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnClientPacketCalls)}: {walker.OnClientPacketCalls}");
    global::System.Console.WriteLine($"Observed: {walker.Observed:X16}");
    global::System.Console.WriteLine($"Checked: {nameof(walker.StableEntries)} {walker.StableEntries}; {nameof(walker.EndedEntries)} {walker.EndedEntries}; {nameof(walker.ActionChecks)} {walker.ActionChecks}; {nameof(walker.SequenceValued)} {walker.SequenceValued}; {nameof(walker.SequenceAbsent)} {walker.SequenceAbsent}; {nameof(walker.Rewritten)} {walker.Rewritten}; {nameof(walker.Splits)} {walker.Splits}");
    if (walker.Mismatched > None) global::System.Console.Error.WriteLine($"FAIL: {walker.Mismatched} writes differ from their captured bytes");
    if (splitter.Framed != walker.Framed)
    {
        global::System.Console.Error.WriteLine("FAIL: the reads of the segments split at their midpoint observe the frames differently from the unsplit reads.");
        return Failure;
    }
    return invalid > None || incomplete > None || walker.Mismatched > None || strict && (walker.Parsed == None || unread > None || skipped > None) ? Failure : Success;
}
try
{
    using var reader = global::Omi.Pcap.PcapReader.Open(args[PcapArgument]);
    while (reader.Advance())
    {
        var read = false;
        walker.Frame = reader.Data.ToArray();
        if (clientPacketCapture is not null)
        {
            var opened = clientPacketCapture.TryRead(walker.Frame, reader.LinkType, out var reading);
            if (clientPacketCapture.FrameRead)
            {
                read = true;
                if (opened)
                {
                    try
                    {
                        foreach (var root in reading)
                        {
                            walker.VisitClientPacket(root);
                        }
                    }
                    catch (global::System.ArgumentOutOfRangeException)
                    {
                        invalid++;
                    }
                }
                walker.Observe(clientPacketCapture.Consumed);
            }
            if (clientPacketCaptureSplit is not null && clientPacketCaptureSplit.TryGetSegment(walker.Frame, reader.LinkType, out var segment))
            {
                var middle = segment.Length / 2;
                try
                {
                    foreach (var root in clientPacketCaptureSplit.Receive(segment[..middle]))
                    {
                        splitter.VisitClientPacket(root);
                    }
                    foreach (var root in clientPacketCaptureSplit.Receive(segment[middle..]))
                    {
                        splitter.VisitClientPacket(root);
                    }
                }
                catch (global::System.ArgumentOutOfRangeException)
                {
                }
                walker.Splits++;
            }
        }
        if (serverPacketCapture is not null)
        {
            var opened = serverPacketCapture.TryRead(walker.Frame, reader.LinkType, out var reading);
            if (serverPacketCapture.FrameRead)
            {
                read = true;
                if (opened)
                {
                    try
                    {
                        foreach (var root in reading)
                        {
                            walker.VisitServerPacket(root);
                        }
                    }
                    catch (global::System.ArgumentOutOfRangeException)
                    {
                        invalid++;
                    }
                }
                walker.Observe(serverPacketCapture.Consumed);
            }
            if (serverPacketCaptureSplit is not null && serverPacketCaptureSplit.TryGetSegment(walker.Frame, reader.LinkType, out var segment))
            {
                var middle = segment.Length / 2;
                try
                {
                    foreach (var root in serverPacketCaptureSplit.Receive(segment[..middle]))
                    {
                        splitter.VisitServerPacket(root);
                    }
                    foreach (var root in serverPacketCaptureSplit.Receive(segment[middle..]))
                    {
                        splitter.VisitServerPacket(root);
                    }
                }
                catch (global::System.ArgumentOutOfRangeException)
                {
                }
                walker.Splits++;
            }
        }
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

invalid += clientPacketCapture?.InvalidFlows ?? None;
incomplete += clientPacketCapture?.IncompleteFlows ?? None;
unread -= clientPacketCapture?.HeldFramesRead ?? None;
invalid += serverPacketCapture?.InvalidFlows ?? None;
incomplete += serverPacketCapture?.IncompleteFlows ?? None;
unread -= serverPacketCapture?.HeldFramesRead ?? None;
global::System.Console.WriteLine($"Parsed: {walker.Parsed}; invalid: {invalid}; incomplete: {incomplete}; unread frames: {unread}; skipped frames: {skipped}");
if (walker.OnDirectListingWithCapitalRaisePriceDiscoveryMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnDirectListingWithCapitalRaisePriceDiscoveryMessageCalls)}: {walker.OnDirectListingWithCapitalRaisePriceDiscoveryMessageCalls}");
if (walker.OnRetailPriceImprovementIndicatorMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnRetailPriceImprovementIndicatorMessageCalls)}: {walker.OnRetailPriceImprovementIndicatorMessageCalls}");
if (walker.OnNetOrderImbalanceIndicatorMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnNetOrderImbalanceIndicatorMessageCalls)}: {walker.OnNetOrderImbalanceIndicatorMessageCalls}");
if (walker.OnBrokenTradeMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnBrokenTradeMessageCalls)}: {walker.OnBrokenTradeMessageCalls}");
if (walker.OnCrossTradeMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnCrossTradeMessageCalls)}: {walker.OnCrossTradeMessageCalls}");
if (walker.OnNonCrossTradeMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnNonCrossTradeMessageCalls)}: {walker.OnNonCrossTradeMessageCalls}");
if (walker.OnOrderReplaceMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnOrderReplaceMessageCalls)}: {walker.OnOrderReplaceMessageCalls}");
if (walker.OnOrderDeleteMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnOrderDeleteMessageCalls)}: {walker.OnOrderDeleteMessageCalls}");
if (walker.OnOrderCancelMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnOrderCancelMessageCalls)}: {walker.OnOrderCancelMessageCalls}");
if (walker.OnOrderExecutedWithPriceMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnOrderExecutedWithPriceMessageCalls)}: {walker.OnOrderExecutedWithPriceMessageCalls}");
if (walker.OnOrderExecutedMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnOrderExecutedMessageCalls)}: {walker.OnOrderExecutedMessageCalls}");
if (walker.OnAddOrderWithMpidAttributionMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnAddOrderWithMpidAttributionMessageCalls)}: {walker.OnAddOrderWithMpidAttributionMessageCalls}");
if (walker.OnAddOrderNoMpidAttributionMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnAddOrderNoMpidAttributionMessageCalls)}: {walker.OnAddOrderNoMpidAttributionMessageCalls}");
if (walker.OnOperationalHaltMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnOperationalHaltMessageCalls)}: {walker.OnOperationalHaltMessageCalls}");
if (walker.OnLuldAuctionCollarMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnLuldAuctionCollarMessageCalls)}: {walker.OnLuldAuctionCollarMessageCalls}");
if (walker.OnIpoQuotingPeriodUpdateCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnIpoQuotingPeriodUpdateCalls)}: {walker.OnIpoQuotingPeriodUpdateCalls}");
if (walker.OnMwcbStatusLevelMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnMwcbStatusLevelMessageCalls)}: {walker.OnMwcbStatusLevelMessageCalls}");
if (walker.OnMwcbDeclineLevelMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnMwcbDeclineLevelMessageCalls)}: {walker.OnMwcbDeclineLevelMessageCalls}");
if (walker.OnMarketParticipantPositionMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnMarketParticipantPositionMessageCalls)}: {walker.OnMarketParticipantPositionMessageCalls}");
if (walker.OnRegShoShortSalePriceTestRestrictedIndicatorMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnRegShoShortSalePriceTestRestrictedIndicatorMessageCalls)}: {walker.OnRegShoShortSalePriceTestRestrictedIndicatorMessageCalls}");
if (walker.OnStockTradingActionMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnStockTradingActionMessageCalls)}: {walker.OnStockTradingActionMessageCalls}");
if (walker.OnStockDirectoryMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnStockDirectoryMessageCalls)}: {walker.OnStockDirectoryMessageCalls}");
if (walker.OnSystemEventMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnSystemEventMessageCalls)}: {walker.OnSystemEventMessageCalls}");
if (walker.OnEndOfSessionCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnEndOfSessionCalls)}: {walker.OnEndOfSessionCalls}");
if (walker.OnHeartbeatCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnHeartbeatCalls)}: {walker.OnHeartbeatCalls}");
if (walker.OnServerHeartbeatCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnServerHeartbeatCalls)}: {walker.OnServerHeartbeatCalls}");
if (walker.OnSequencedDataPacketCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnSequencedDataPacketCalls)}: {walker.OnSequencedDataPacketCalls}");
if (walker.OnLoginRejectedPacketCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnLoginRejectedPacketCalls)}: {walker.OnLoginRejectedPacketCalls}");
if (walker.OnLoginAcceptedPacketCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnLoginAcceptedPacketCalls)}: {walker.OnLoginAcceptedPacketCalls}");
if (walker.OnDebugPacketCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnDebugPacketCalls)}: {walker.OnDebugPacketCalls}");
if (walker.OnLogoutRequestCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnLogoutRequestCalls)}: {walker.OnLogoutRequestCalls}");
if (walker.OnClientHeartbeatCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnClientHeartbeatCalls)}: {walker.OnClientHeartbeatCalls}");
if (walker.OnUnsequencedDataPacketCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnUnsequencedDataPacketCalls)}: {walker.OnUnsequencedDataPacketCalls}");
if (walker.OnLoginRequestPacketCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnLoginRequestPacketCalls)}: {walker.OnLoginRequestPacketCalls}");
if (walker.OnFileCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnFileCalls)}: {walker.OnFileCalls}");
if (walker.OnPacketHeaderCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnPacketHeaderCalls)}: {walker.OnPacketHeaderCalls}");
if (walker.OnPacketCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnPacketCalls)}: {walker.OnPacketCalls}");
if (walker.OnServerPacketHeaderCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnServerPacketHeaderCalls)}: {walker.OnServerPacketHeaderCalls}");
if (walker.OnServerPacketCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnServerPacketCalls)}: {walker.OnServerPacketCalls}");
if (walker.OnClientPacketHeaderCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnClientPacketHeaderCalls)}: {walker.OnClientPacketHeaderCalls}");
if (walker.OnClientPacketCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnClientPacketCalls)}: {walker.OnClientPacketCalls}");
global::System.Console.WriteLine($"Observed: {walker.Observed:X16}");
global::System.Console.WriteLine($"Checked: {nameof(walker.StableEntries)} {walker.StableEntries}; {nameof(walker.EndedEntries)} {walker.EndedEntries}; {nameof(walker.ActionChecks)} {walker.ActionChecks}; {nameof(walker.SequenceValued)} {walker.SequenceValued}; {nameof(walker.SequenceAbsent)} {walker.SequenceAbsent}; {nameof(walker.Rewritten)} {walker.Rewritten}; {nameof(walker.Splits)} {walker.Splits}");
if (walker.Mismatched > None) global::System.Console.Error.WriteLine($"FAIL: {walker.Mismatched} writes differ from their captured bytes");
if (splitter.Framed != walker.Framed)
{
    global::System.Console.Error.WriteLine("FAIL: the reads of the segments split at their midpoint observe the frames differently from the unsplit reads.");
    return Failure;
}
return invalid > None || incomplete > None || walker.Mismatched > None || strict && (walker.Parsed == None || unread > None || skipped > None) ? Failure : Success;

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
    ///  The number of captured messages whose write differs from their bytes.
    /// </summary>
    public long Mismatched;
    /// <summary>
    ///  The number of calls of the OnClientPacketHeader method.
    /// </summary>
    public long OnClientPacketHeaderCalls;
    /// <summary>
    ///  The number of calls of the OnDebugPacket method.
    /// </summary>
    public long OnDebugPacketCalls;
    /// <summary>
    ///  The number of calls of the OnLoginRequestPacket method.
    /// </summary>
    public long OnLoginRequestPacketCalls;
    /// <summary>
    ///  The number of calls of the OnUnsequencedDataPacket method.
    /// </summary>
    public long OnUnsequencedDataPacketCalls;
    /// <summary>
    ///  The number of calls of the OnClientHeartbeat method.
    /// </summary>
    public long OnClientHeartbeatCalls;
    /// <summary>
    ///  The number of calls of the OnLogoutRequest method.
    /// </summary>
    public long OnLogoutRequestCalls;
    /// <summary>
    ///  The number of calls of the OnServerPacketHeader method.
    /// </summary>
    public long OnServerPacketHeaderCalls;
    /// <summary>
    ///  The number of calls of the OnLoginAcceptedPacket method.
    /// </summary>
    public long OnLoginAcceptedPacketCalls;
    /// <summary>
    ///  The number of calls of the OnLoginRejectedPacket method.
    /// </summary>
    public long OnLoginRejectedPacketCalls;
    /// <summary>
    ///  The number of calls of the OnSequencedDataPacket method.
    /// </summary>
    public long OnSequencedDataPacketCalls;
    /// <summary>
    ///  The number of calls of the OnServerHeartbeat method.
    /// </summary>
    public long OnServerHeartbeatCalls;
    /// <summary>
    ///  The number of calls of the OnEndOfSession method.
    /// </summary>
    public long OnEndOfSessionCalls;
    /// <summary>
    ///  The number of calls of the OnSystemEventMessage method.
    /// </summary>
    public long OnSystemEventMessageCalls;
    /// <summary>
    ///  The number of calls of the OnStockDirectoryMessage method.
    /// </summary>
    public long OnStockDirectoryMessageCalls;
    /// <summary>
    ///  The number of calls of the OnStockTradingActionMessage method.
    /// </summary>
    public long OnStockTradingActionMessageCalls;
    /// <summary>
    ///  The number of calls of the OnRegShoShortSalePriceTestRestrictedIndicatorMessage method.
    /// </summary>
    public long OnRegShoShortSalePriceTestRestrictedIndicatorMessageCalls;
    /// <summary>
    ///  The number of calls of the OnMarketParticipantPositionMessage method.
    /// </summary>
    public long OnMarketParticipantPositionMessageCalls;
    /// <summary>
    ///  The number of calls of the OnMwcbDeclineLevelMessage method.
    /// </summary>
    public long OnMwcbDeclineLevelMessageCalls;
    /// <summary>
    ///  The number of calls of the OnMwcbStatusLevelMessage method.
    /// </summary>
    public long OnMwcbStatusLevelMessageCalls;
    /// <summary>
    ///  The number of calls of the OnIpoQuotingPeriodUpdate method.
    /// </summary>
    public long OnIpoQuotingPeriodUpdateCalls;
    /// <summary>
    ///  The number of calls of the OnLuldAuctionCollarMessage method.
    /// </summary>
    public long OnLuldAuctionCollarMessageCalls;
    /// <summary>
    ///  The number of calls of the OnOperationalHaltMessage method.
    /// </summary>
    public long OnOperationalHaltMessageCalls;
    /// <summary>
    ///  The number of calls of the OnAddOrderNoMpidAttributionMessage method.
    /// </summary>
    public long OnAddOrderNoMpidAttributionMessageCalls;
    /// <summary>
    ///  The number of calls of the OnAddOrderWithMpidAttributionMessage method.
    /// </summary>
    public long OnAddOrderWithMpidAttributionMessageCalls;
    /// <summary>
    ///  The number of calls of the OnOrderExecutedMessage method.
    /// </summary>
    public long OnOrderExecutedMessageCalls;
    /// <summary>
    ///  The number of calls of the OnOrderExecutedWithPriceMessage method.
    /// </summary>
    public long OnOrderExecutedWithPriceMessageCalls;
    /// <summary>
    ///  The number of calls of the OnOrderCancelMessage method.
    /// </summary>
    public long OnOrderCancelMessageCalls;
    /// <summary>
    ///  The number of calls of the OnOrderDeleteMessage method.
    /// </summary>
    public long OnOrderDeleteMessageCalls;
    /// <summary>
    ///  The number of calls of the OnOrderReplaceMessage method.
    /// </summary>
    public long OnOrderReplaceMessageCalls;
    /// <summary>
    ///  The number of calls of the OnNonCrossTradeMessage method.
    /// </summary>
    public long OnNonCrossTradeMessageCalls;
    /// <summary>
    ///  The number of calls of the OnCrossTradeMessage method.
    /// </summary>
    public long OnCrossTradeMessageCalls;
    /// <summary>
    ///  The number of calls of the OnBrokenTradeMessage method.
    /// </summary>
    public long OnBrokenTradeMessageCalls;
    /// <summary>
    ///  The number of calls of the OnNetOrderImbalanceIndicatorMessage method.
    /// </summary>
    public long OnNetOrderImbalanceIndicatorMessageCalls;
    /// <summary>
    ///  The number of calls of the OnRetailPriceImprovementIndicatorMessage method.
    /// </summary>
    public long OnRetailPriceImprovementIndicatorMessageCalls;
    /// <summary>
    ///  The number of calls of the OnDirectListingWithCapitalRaisePriceDiscoveryMessage method.
    /// </summary>
    public long OnDirectListingWithCapitalRaisePriceDiscoveryMessageCalls;
    /// <summary>
    ///  The number of calls of the OnPacketHeader method.
    /// </summary>
    public long OnPacketHeaderCalls;
    /// <summary>
    ///  The number of calls of the OnHeartbeat method.
    /// </summary>
    public long OnHeartbeatCalls;
    /// <summary>
    ///  The number of calls of the OnClientPacket method.
    /// </summary>
    public long OnClientPacketCalls;
    /// <summary>
    ///  The number of calls of the OnServerPacket method.
    /// </summary>
    public long OnServerPacketCalls;
    /// <summary>
    ///  The number of calls of the OnPacket method.
    /// </summary>
    public long OnPacketCalls;
    /// <summary>
    ///  The number of calls of the OnFile method.
    /// </summary>
    public long OnFileCalls;
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
    ///  The captured messages whose write gives their bytes again.
    /// </summary>
    public long Rewritten;
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
    public void VisitClientPacket(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPacket.Reader clientPacket)
    {
        this.BeginPacketSequence();
        var clientSoupBinTcpPacketEntries = clientPacket.ClientSoupBinTcpPacket;
        var clientSoupBinTcpPacketRead = 0L;
        while (clientSoupBinTcpPacketEntries.MoveNext())
        {
            var clientSoupBinTcpPacket = clientSoupBinTcpPacketEntries.Current;
            this.Inside++;
            this.OnClientPacketHeader(in clientSoupBinTcpPacket.ClientPacketHeader);
            var clientPayload = clientSoupBinTcpPacket.ClientPayload;
            switch (clientPayload.Case)
            {
                case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.DebugPacket:
                {
                    this.VisitDebugPacket(clientPayload.DebugPacket);
                    break;
                }
                case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.LoginRequestPacket:
                {
                    this.OnLoginRequestPacket(in clientPayload.LoginRequestPacket);
                    break;
                }
                case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.UnsequencedDataPacket:
                {
                    this.OnUnsequencedDataPacket(clientPayload.UnsequencedDataPacket);
                    break;
                }
                case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.ClientHeartbeat:
                {
                    this.OnClientHeartbeat();
                    break;
                }
                case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.LogoutRequest:
                {
                    this.OnLogoutRequest();
                    break;
                }
            }
            this.Inside--;
            if (clientSoupBinTcpPacketEntries.Index != ++clientSoupBinTcpPacketRead) throw new global::System.InvalidOperationException("The ClientPacket.ClientSoupBinTcpPacket entries do not number their entries in order.");
            _ = clientSoupBinTcpPacketEntries.Current;
            if (clientSoupBinTcpPacketEntries.Index != clientSoupBinTcpPacketRead) throw new global::System.InvalidOperationException("A second read of the current ClientPacket.ClientSoupBinTcpPacket entry moves the entries.");
            this.StableEntries++;
        }
        if (clientSoupBinTcpPacketEntries.MoveNext() || clientSoupBinTcpPacketEntries.Index != clientSoupBinTcpPacketRead) throw new global::System.InvalidOperationException("The ClientPacket.ClientSoupBinTcpPacket entries step after their end, or number their entries differently from the entries read.");
        this.EndedEntries++;
        this.OnClientPacket(clientPacket);
    }

    /// <summary>
    ///  Visits the packet and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitServerPacket(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPacket.Reader serverPacket)
    {
        this.BeginPacketSequence();
        var serverSoupBinTcpPacketEntries = serverPacket.ServerSoupBinTcpPacket;
        var serverSoupBinTcpPacketRead = 0L;
        while (serverSoupBinTcpPacketEntries.MoveNext())
        {
            var serverSoupBinTcpPacket = serverSoupBinTcpPacketEntries.Current;
            this.Inside++;
            this.OnServerPacketHeader(in serverSoupBinTcpPacket.ServerPacketHeader);
            var serverPayload = serverSoupBinTcpPacket.ServerPayload;
            switch (serverPayload.Case)
            {
                case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.DebugPacket:
                {
                    this.VisitDebugPacket(serverPayload.DebugPacket);
                    break;
                }
                case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.LoginAcceptedPacket:
                {
                    this.OnLoginAcceptedPacket(in serverPayload.LoginAcceptedPacket);
                    break;
                }
                case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.LoginRejectedPacket:
                {
                    this.OnLoginRejectedPacket(in serverPayload.LoginRejectedPacket);
                    break;
                }
                case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.SequencedDataPacket:
                {
                    var sequencedDataPacket = serverPayload.SequencedDataPacket;
                    var sequencedMessage = sequencedDataPacket.SequencedMessage;
                    switch (sequencedMessage.Case)
                    {
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.SystemEventMessage:
                        {
                            this.VisitSystemEventMessage(in sequencedMessage.SystemEventMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.StockDirectoryMessage:
                        {
                            this.VisitStockDirectoryMessage(in sequencedMessage.StockDirectoryMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.StockTradingActionMessage:
                        {
                            this.VisitStockTradingActionMessage(in sequencedMessage.StockTradingActionMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.RegShoShortSalePriceTestRestrictedIndicatorMessage:
                        {
                            this.VisitRegShoShortSalePriceTestRestrictedIndicatorMessage(in sequencedMessage.RegShoShortSalePriceTestRestrictedIndicatorMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.MarketParticipantPositionMessage:
                        {
                            this.VisitMarketParticipantPositionMessage(in sequencedMessage.MarketParticipantPositionMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.MwcbDeclineLevelMessage:
                        {
                            this.VisitMwcbDeclineLevelMessage(in sequencedMessage.MwcbDeclineLevelMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.MwcbStatusLevelMessage:
                        {
                            this.VisitMwcbStatusLevelMessage(in sequencedMessage.MwcbStatusLevelMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.IpoQuotingPeriodUpdate:
                        {
                            this.VisitIpoQuotingPeriodUpdate(in sequencedMessage.IpoQuotingPeriodUpdate, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.LuldAuctionCollarMessage:
                        {
                            this.VisitLuldAuctionCollarMessage(in sequencedMessage.LuldAuctionCollarMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OperationalHaltMessage:
                        {
                            this.VisitOperationalHaltMessage(in sequencedMessage.OperationalHaltMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.AddOrderNoMpidAttributionMessage:
                        {
                            this.VisitAddOrderNoMpidAttributionMessage(in sequencedMessage.AddOrderNoMpidAttributionMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.AddOrderWithMpidAttributionMessage:
                        {
                            this.VisitAddOrderWithMpidAttributionMessage(in sequencedMessage.AddOrderWithMpidAttributionMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderExecutedMessage:
                        {
                            this.VisitOrderExecutedMessage(in sequencedMessage.OrderExecutedMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderExecutedWithPriceMessage:
                        {
                            this.VisitOrderExecutedWithPriceMessage(in sequencedMessage.OrderExecutedWithPriceMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderCancelMessage:
                        {
                            this.VisitOrderCancelMessage(in sequencedMessage.OrderCancelMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderDeleteMessage:
                        {
                            this.VisitOrderDeleteMessage(in sequencedMessage.OrderDeleteMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderReplaceMessage:
                        {
                            this.VisitOrderReplaceMessage(in sequencedMessage.OrderReplaceMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.NonCrossTradeMessage:
                        {
                            this.VisitNonCrossTradeMessage(in sequencedMessage.NonCrossTradeMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.CrossTradeMessage:
                        {
                            this.VisitCrossTradeMessage(in sequencedMessage.CrossTradeMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.BrokenTradeMessage:
                        {
                            this.VisitBrokenTradeMessage(in sequencedMessage.BrokenTradeMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.NetOrderImbalanceIndicatorMessage:
                        {
                            this.VisitNetOrderImbalanceIndicatorMessage(in sequencedMessage.NetOrderImbalanceIndicatorMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.RetailPriceImprovementIndicatorMessage:
                        {
                            this.VisitRetailPriceImprovementIndicatorMessage(in sequencedMessage.RetailPriceImprovementIndicatorMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.DirectListingWithCapitalRaisePriceDiscoveryMessage:
                        {
                            this.VisitDirectListingWithCapitalRaisePriceDiscoveryMessage(in sequencedMessage.DirectListingWithCapitalRaisePriceDiscoveryMessage, null);
                            break;
                        }
                    }
                    this.OnSequencedDataPacket(sequencedDataPacket, null);
                    break;
                }
                case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.ServerHeartbeat:
                {
                    this.OnServerHeartbeat();
                    break;
                }
                case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.EndOfSession:
                {
                    this.VisitEndOfSession();
                    break;
                }
            }
            this.Inside--;
            if (serverSoupBinTcpPacketEntries.Index != ++serverSoupBinTcpPacketRead) throw new global::System.InvalidOperationException("The ServerPacket.ServerSoupBinTcpPacket entries do not number their entries in order.");
            _ = serverSoupBinTcpPacketEntries.Current;
            if (serverSoupBinTcpPacketEntries.Index != serverSoupBinTcpPacketRead) throw new global::System.InvalidOperationException("A second read of the current ServerPacket.ServerSoupBinTcpPacket entry moves the entries.");
            this.StableEntries++;
        }
        if (serverSoupBinTcpPacketEntries.MoveNext() || serverSoupBinTcpPacketEntries.Index != serverSoupBinTcpPacketRead) throw new global::System.InvalidOperationException("The ServerPacket.ServerSoupBinTcpPacket entries step after their end, or number their entries differently from the entries read.");
        this.EndedEntries++;
        this.OnServerPacket(serverPacket);
    }

    /// <summary>
    ///  Visits the packet and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitPacket(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Packet.Reader packet)
    {
        this.BeginPacketSequence();
        this.SeedPacketSequence(ulong.CreateSaturating(packet.PacketHeader.SequenceNumber.Value));
        this.OnPacketHeader(in packet.PacketHeader);
        var messages = packet.Messages;
        switch (messages.Case)
        {
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Messages.Case.Heartbeat:
            {
                this.OnHeartbeat();
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Messages.Case.EndOfSession:
            {
                this.VisitEndOfSession();
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Messages.Case.Message:
            {
                var messageEntries = messages.Message;
                var messageRead = 0L;
                while (messageEntries.MoveNext())
                {
                    var message = messageEntries.Current;
                    {
                        ulong? expected = null;
                        expected = this.NextPacketSequence();
                        this.CheckSequence((ulong?)message.SequenceNumber, expected, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Message));
                    }
                    this.numberedDepth++;
                    var payload = message.Payload;
                    switch (payload.Case)
                    {
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.SystemEventMessage:
                        {
                            this.VisitSystemEventMessage(in payload.SystemEventMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.StockDirectoryMessage:
                        {
                            this.VisitStockDirectoryMessage(in payload.StockDirectoryMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.StockTradingActionMessage:
                        {
                            this.VisitStockTradingActionMessage(in payload.StockTradingActionMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.RegShoShortSalePriceTestRestrictedIndicatorMessage:
                        {
                            this.VisitRegShoShortSalePriceTestRestrictedIndicatorMessage(in payload.RegShoShortSalePriceTestRestrictedIndicatorMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.MarketParticipantPositionMessage:
                        {
                            this.VisitMarketParticipantPositionMessage(in payload.MarketParticipantPositionMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.MwcbDeclineLevelMessage:
                        {
                            this.VisitMwcbDeclineLevelMessage(in payload.MwcbDeclineLevelMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.MwcbStatusLevelMessage:
                        {
                            this.VisitMwcbStatusLevelMessage(in payload.MwcbStatusLevelMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.IpoQuotingPeriodUpdate:
                        {
                            this.VisitIpoQuotingPeriodUpdate(in payload.IpoQuotingPeriodUpdate, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.LuldAuctionCollarMessage:
                        {
                            this.VisitLuldAuctionCollarMessage(in payload.LuldAuctionCollarMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.OperationalHaltMessage:
                        {
                            this.VisitOperationalHaltMessage(in payload.OperationalHaltMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.AddOrderNoMpidAttributionMessage:
                        {
                            this.VisitAddOrderNoMpidAttributionMessage(in payload.AddOrderNoMpidAttributionMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.AddOrderWithMpidAttributionMessage:
                        {
                            this.VisitAddOrderWithMpidAttributionMessage(in payload.AddOrderWithMpidAttributionMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.OrderExecutedMessage:
                        {
                            this.VisitOrderExecutedMessage(in payload.OrderExecutedMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.OrderExecutedWithPriceMessage:
                        {
                            this.VisitOrderExecutedWithPriceMessage(in payload.OrderExecutedWithPriceMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.OrderCancelMessage:
                        {
                            this.VisitOrderCancelMessage(in payload.OrderCancelMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.OrderDeleteMessage:
                        {
                            this.VisitOrderDeleteMessage(in payload.OrderDeleteMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.OrderReplaceMessage:
                        {
                            this.VisitOrderReplaceMessage(in payload.OrderReplaceMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.NonCrossTradeMessage:
                        {
                            this.VisitNonCrossTradeMessage(in payload.NonCrossTradeMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.CrossTradeMessage:
                        {
                            this.VisitCrossTradeMessage(in payload.CrossTradeMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.BrokenTradeMessage:
                        {
                            this.VisitBrokenTradeMessage(in payload.BrokenTradeMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.NetOrderImbalanceIndicatorMessage:
                        {
                            this.VisitNetOrderImbalanceIndicatorMessage(in payload.NetOrderImbalanceIndicatorMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.RetailPriceImprovementIndicatorMessage:
                        {
                            this.VisitRetailPriceImprovementIndicatorMessage(in payload.RetailPriceImprovementIndicatorMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.DirectListingWithCapitalRaisePriceDiscoveryMessage:
                        {
                            this.VisitDirectListingWithCapitalRaisePriceDiscoveryMessage(in payload.DirectListingWithCapitalRaisePriceDiscoveryMessage, in message.MessageHeader, message.SequenceNumber);
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

    /// <summary>
    ///  Visits the packet and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitFile(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.File.Reader file)
    {
        this.BeginPacketSequence();
        var fileMessageEntries = file.FileMessage;
        var fileMessageRead = 0L;
        while (fileMessageEntries.MoveNext())
        {
            var fileMessage = fileMessageEntries.Current;
            this.Inside++;
            this.VisitFileMessage(fileMessage);
            this.Inside--;
            if (fileMessageEntries.Index != ++fileMessageRead) throw new global::System.InvalidOperationException("The File.FileMessage entries do not number their entries in order.");
            _ = fileMessageEntries.Current;
            if (fileMessageEntries.Index != fileMessageRead) throw new global::System.InvalidOperationException("A second read of the current File.FileMessage entry moves the entries.");
            this.StableEntries++;
        }
        if (fileMessageEntries.MoveNext() || fileMessageEntries.Index != fileMessageRead) throw new global::System.InvalidOperationException("The File.FileMessage entries step after their end, or number their entries differently from the entries read.");
        this.EndedEntries++;
        this.OnFile(file);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitDebugPacket(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DebugPacket.Reader debugPacket)
    {
        this.OnDebugPacket(debugPacket);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitEndOfSession()
    {
        this.OnEndOfSession();
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitSystemEventMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SystemEventMessage.Layout systemEventMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnSystemEventMessage(in systemEventMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitStockDirectoryMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockDirectoryMessage.Layout stockDirectoryMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnStockDirectoryMessage(in stockDirectoryMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitStockTradingActionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockTradingActionMessage.Layout stockTradingActionMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnStockTradingActionMessage(in stockTradingActionMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitRegShoShortSalePriceTestRestrictedIndicatorMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout regShoShortSalePriceTestRestrictedIndicatorMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnRegShoShortSalePriceTestRestrictedIndicatorMessage(in regShoShortSalePriceTestRestrictedIndicatorMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitMarketParticipantPositionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MarketParticipantPositionMessage.Layout marketParticipantPositionMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnMarketParticipantPositionMessage(in marketParticipantPositionMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitMwcbDeclineLevelMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbDeclineLevelMessage.Layout mwcbDeclineLevelMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnMwcbDeclineLevelMessage(in mwcbDeclineLevelMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitMwcbStatusLevelMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbStatusLevelMessage.Layout mwcbStatusLevelMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnMwcbStatusLevelMessage(in mwcbStatusLevelMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitIpoQuotingPeriodUpdate(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.IpoQuotingPeriodUpdate.Layout ipoQuotingPeriodUpdate, global::System.UInt128? sequenceNumber)
    {
        this.OnIpoQuotingPeriodUpdate(in ipoQuotingPeriodUpdate, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitLuldAuctionCollarMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LuldAuctionCollarMessage.Layout luldAuctionCollarMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnLuldAuctionCollarMessage(in luldAuctionCollarMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitOperationalHaltMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OperationalHaltMessage.Layout operationalHaltMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnOperationalHaltMessage(in operationalHaltMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitAddOrderNoMpidAttributionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderNoMpidAttributionMessage.Layout addOrderNoMpidAttributionMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnAddOrderNoMpidAttributionMessage(in addOrderNoMpidAttributionMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitAddOrderWithMpidAttributionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderWithMpidAttributionMessage.Layout addOrderWithMpidAttributionMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnAddOrderWithMpidAttributionMessage(in addOrderWithMpidAttributionMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitOrderExecutedMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedMessage.Layout orderExecutedMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnOrderExecutedMessage(in orderExecutedMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitOrderExecutedWithPriceMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedWithPriceMessage.Layout orderExecutedWithPriceMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnOrderExecutedWithPriceMessage(in orderExecutedWithPriceMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitOrderCancelMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderCancelMessage.Layout orderCancelMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnOrderCancelMessage(in orderCancelMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitOrderDeleteMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderDeleteMessage.Layout orderDeleteMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnOrderDeleteMessage(in orderDeleteMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitOrderReplaceMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderReplaceMessage.Layout orderReplaceMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnOrderReplaceMessage(in orderReplaceMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitNonCrossTradeMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NonCrossTradeMessage.Layout nonCrossTradeMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnNonCrossTradeMessage(in nonCrossTradeMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitCrossTradeMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossTradeMessage.Layout crossTradeMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnCrossTradeMessage(in crossTradeMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitBrokenTradeMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.BrokenTradeMessage.Layout brokenTradeMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnBrokenTradeMessage(in brokenTradeMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitNetOrderImbalanceIndicatorMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NetOrderImbalanceIndicatorMessage.Layout netOrderImbalanceIndicatorMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnNetOrderImbalanceIndicatorMessage(in netOrderImbalanceIndicatorMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitRetailPriceImprovementIndicatorMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RetailPriceImprovementIndicatorMessage.Layout retailPriceImprovementIndicatorMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnRetailPriceImprovementIndicatorMessage(in retailPriceImprovementIndicatorMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitDirectListingWithCapitalRaisePriceDiscoveryMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout directListingWithCapitalRaisePriceDiscoveryMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnDirectListingWithCapitalRaisePriceDiscoveryMessage(in directListingWithCapitalRaisePriceDiscoveryMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitSystemEventMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SystemEventMessage.Layout systemEventMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnSystemEventMessage(in systemEventMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitStockDirectoryMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockDirectoryMessage.Layout stockDirectoryMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnStockDirectoryMessage(in stockDirectoryMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitStockTradingActionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockTradingActionMessage.Layout stockTradingActionMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnStockTradingActionMessage(in stockTradingActionMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitRegShoShortSalePriceTestRestrictedIndicatorMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout regShoShortSalePriceTestRestrictedIndicatorMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnRegShoShortSalePriceTestRestrictedIndicatorMessage(in regShoShortSalePriceTestRestrictedIndicatorMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitMarketParticipantPositionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MarketParticipantPositionMessage.Layout marketParticipantPositionMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnMarketParticipantPositionMessage(in marketParticipantPositionMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitMwcbDeclineLevelMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbDeclineLevelMessage.Layout mwcbDeclineLevelMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnMwcbDeclineLevelMessage(in mwcbDeclineLevelMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitMwcbStatusLevelMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbStatusLevelMessage.Layout mwcbStatusLevelMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnMwcbStatusLevelMessage(in mwcbStatusLevelMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitIpoQuotingPeriodUpdate(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.IpoQuotingPeriodUpdate.Layout ipoQuotingPeriodUpdate, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnIpoQuotingPeriodUpdate(in ipoQuotingPeriodUpdate, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitLuldAuctionCollarMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LuldAuctionCollarMessage.Layout luldAuctionCollarMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnLuldAuctionCollarMessage(in luldAuctionCollarMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitOperationalHaltMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OperationalHaltMessage.Layout operationalHaltMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnOperationalHaltMessage(in operationalHaltMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitAddOrderNoMpidAttributionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderNoMpidAttributionMessage.Layout addOrderNoMpidAttributionMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnAddOrderNoMpidAttributionMessage(in addOrderNoMpidAttributionMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitAddOrderWithMpidAttributionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderWithMpidAttributionMessage.Layout addOrderWithMpidAttributionMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnAddOrderWithMpidAttributionMessage(in addOrderWithMpidAttributionMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitOrderExecutedMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedMessage.Layout orderExecutedMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnOrderExecutedMessage(in orderExecutedMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitOrderExecutedWithPriceMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedWithPriceMessage.Layout orderExecutedWithPriceMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnOrderExecutedWithPriceMessage(in orderExecutedWithPriceMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitOrderCancelMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderCancelMessage.Layout orderCancelMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnOrderCancelMessage(in orderCancelMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitOrderDeleteMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderDeleteMessage.Layout orderDeleteMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnOrderDeleteMessage(in orderDeleteMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitOrderReplaceMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderReplaceMessage.Layout orderReplaceMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnOrderReplaceMessage(in orderReplaceMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitNonCrossTradeMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NonCrossTradeMessage.Layout nonCrossTradeMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnNonCrossTradeMessage(in nonCrossTradeMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitCrossTradeMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossTradeMessage.Layout crossTradeMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnCrossTradeMessage(in crossTradeMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitBrokenTradeMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.BrokenTradeMessage.Layout brokenTradeMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnBrokenTradeMessage(in brokenTradeMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitNetOrderImbalanceIndicatorMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NetOrderImbalanceIndicatorMessage.Layout netOrderImbalanceIndicatorMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnNetOrderImbalanceIndicatorMessage(in netOrderImbalanceIndicatorMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitRetailPriceImprovementIndicatorMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RetailPriceImprovementIndicatorMessage.Layout retailPriceImprovementIndicatorMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnRetailPriceImprovementIndicatorMessage(in retailPriceImprovementIndicatorMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitDirectListingWithCapitalRaisePriceDiscoveryMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout directListingWithCapitalRaisePriceDiscoveryMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnDirectListingWithCapitalRaisePriceDiscoveryMessage(in directListingWithCapitalRaisePriceDiscoveryMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitFileMessage(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FileMessage.Reader fileMessage)
    {
        var filePayload = fileMessage.FilePayload;
        switch (filePayload.Case)
        {
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.SystemEventMessage:
            {
                this.VisitSystemEventMessage(in filePayload.SystemEventMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.StockDirectoryMessage:
            {
                this.VisitStockDirectoryMessage(in filePayload.StockDirectoryMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.StockTradingActionMessage:
            {
                this.VisitStockTradingActionMessage(in filePayload.StockTradingActionMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.RegShoShortSalePriceTestRestrictedIndicatorMessage:
            {
                this.VisitRegShoShortSalePriceTestRestrictedIndicatorMessage(in filePayload.RegShoShortSalePriceTestRestrictedIndicatorMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.MarketParticipantPositionMessage:
            {
                this.VisitMarketParticipantPositionMessage(in filePayload.MarketParticipantPositionMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.MwcbDeclineLevelMessage:
            {
                this.VisitMwcbDeclineLevelMessage(in filePayload.MwcbDeclineLevelMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.MwcbStatusLevelMessage:
            {
                this.VisitMwcbStatusLevelMessage(in filePayload.MwcbStatusLevelMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.IpoQuotingPeriodUpdate:
            {
                this.VisitIpoQuotingPeriodUpdate(in filePayload.IpoQuotingPeriodUpdate, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.LuldAuctionCollarMessage:
            {
                this.VisitLuldAuctionCollarMessage(in filePayload.LuldAuctionCollarMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.OperationalHaltMessage:
            {
                this.VisitOperationalHaltMessage(in filePayload.OperationalHaltMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.AddOrderNoMpidAttributionMessage:
            {
                this.VisitAddOrderNoMpidAttributionMessage(in filePayload.AddOrderNoMpidAttributionMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.AddOrderWithMpidAttributionMessage:
            {
                this.VisitAddOrderWithMpidAttributionMessage(in filePayload.AddOrderWithMpidAttributionMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.OrderExecutedMessage:
            {
                this.VisitOrderExecutedMessage(in filePayload.OrderExecutedMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.OrderExecutedWithPriceMessage:
            {
                this.VisitOrderExecutedWithPriceMessage(in filePayload.OrderExecutedWithPriceMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.OrderCancelMessage:
            {
                this.VisitOrderCancelMessage(in filePayload.OrderCancelMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.OrderDeleteMessage:
            {
                this.VisitOrderDeleteMessage(in filePayload.OrderDeleteMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.OrderReplaceMessage:
            {
                this.VisitOrderReplaceMessage(in filePayload.OrderReplaceMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.NonCrossTradeMessage:
            {
                this.VisitNonCrossTradeMessage(in filePayload.NonCrossTradeMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.CrossTradeMessage:
            {
                this.VisitCrossTradeMessage(in filePayload.CrossTradeMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.BrokenTradeMessage:
            {
                this.VisitBrokenTradeMessage(in filePayload.BrokenTradeMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.NetOrderImbalanceIndicatorMessage:
            {
                this.VisitNetOrderImbalanceIndicatorMessage(in filePayload.NetOrderImbalanceIndicatorMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.RetailPriceImprovementIndicatorMessage:
            {
                this.VisitRetailPriceImprovementIndicatorMessage(in filePayload.RetailPriceImprovementIndicatorMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.DirectListingWithCapitalRaisePriceDiscoveryMessage:
            {
                this.VisitDirectListingWithCapitalRaisePriceDiscoveryMessage(in filePayload.DirectListingWithCapitalRaisePriceDiscoveryMessage, in fileMessage.MessageHeader, null);
                break;
            }
        }
    }
    void OnClientPacketHeader(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPacketHeader.Layout value)
    {
        this.Parsed++;
        this.OnClientPacketHeaderCalls++;
        this.Observe(nameof(OnClientPacketHeaderCalls));
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
    void OnDebugPacket(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DebugPacket.Reader value)
    {
        this.Parsed++;
        this.OnDebugPacketCalls++;
        this.Observe(nameof(OnDebugPacketCalls));
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                var reader = value;
                var indent = Dump.Indent;
                Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DebugPacket.Reader.DebugText), Convert.ToHexString(reader.DebugText));
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
    }
    void OnLoginRequestPacket(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRequestPacket.Layout value)
    {
        this.Parsed++;
        this.OnLoginRequestPacketCalls++;
        this.Observe(nameof(OnLoginRequestPacketCalls));
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
    void OnUnsequencedDataPacket(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.UnsequencedDataPacket.Reader value)
    {
        this.Parsed++;
        this.OnUnsequencedDataPacketCalls++;
        this.Observe(nameof(OnUnsequencedDataPacketCalls));
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                var reader = value;
                var indent = Dump.Indent;
                Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.UnsequencedDataPacket.Reader.UnsequencedMessageType), reader.UnsequencedMessageType);
                Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.UnsequencedDataPacket.Reader.UnsequencedMessage), Convert.ToHexString(reader.UnsequencedMessage));
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
    }
    void OnClientHeartbeat()
    {
        this.Parsed++;
        this.OnClientHeartbeatCalls++;
        this.Observe(nameof(OnClientHeartbeatCalls));
    }
    void OnLogoutRequest()
    {
        this.Parsed++;
        this.OnLogoutRequestCalls++;
        this.Observe(nameof(OnLogoutRequestCalls));
    }
    void OnServerPacketHeader(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPacketHeader.Layout value)
    {
        this.Parsed++;
        this.OnServerPacketHeaderCalls++;
        this.Observe(nameof(OnServerPacketHeaderCalls));
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
    void OnLoginAcceptedPacket(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginAcceptedPacket.Layout value)
    {
        this.Parsed++;
        this.OnLoginAcceptedPacketCalls++;
        this.Observe(nameof(OnLoginAcceptedPacketCalls));
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
    void OnLoginRejectedPacket(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRejectedPacket.Layout value)
    {
        this.Parsed++;
        this.OnLoginRejectedPacketCalls++;
        this.Observe(nameof(OnLoginRejectedPacketCalls));
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
    void OnSequencedDataPacket(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedDataPacket.Reader value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnSequencedDataPacketCalls++;
        this.Observe(nameof(OnSequencedDataPacketCalls));
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                var reader = value;
                var indent = Dump.Indent;
                Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedDataPacket.Reader.SequencedMessageType), reader.SequencedMessageType);
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
        {
            var captured = value.Bytes;
            var rewrite = ValueOf(value);
            var written = new byte[captured.Length];
            var padding = new bool[captured.Length];
            try
            {
                this.Compare(nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedDataPacket), global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedDataPacket.Write(written, in rewrite), captured, padding);
            }
            catch (global::System.Exception exception) when (exception is global::System.ArgumentException or global::System.OverflowException)
            {
                this.Differ(nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedDataPacket), exception.Message);
            }
        }
    }
    void OnServerHeartbeat()
    {
        this.Parsed++;
        this.OnServerHeartbeatCalls++;
        this.Observe(nameof(OnServerHeartbeatCalls));
    }
    void OnEndOfSession()
    {
        this.Parsed++;
        this.OnEndOfSessionCalls++;
        this.Observe(nameof(OnEndOfSessionCalls));
    }
    void OnSystemEventMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SystemEventMessage.Layout value, global::System.UInt128? sequenceNumber)
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
    void OnStockDirectoryMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockDirectoryMessage.Layout value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnStockDirectoryMessageCalls++;
        this.Observe(nameof(OnStockDirectoryMessageCalls));
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
    void OnStockTradingActionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockTradingActionMessage.Layout value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnStockTradingActionMessageCalls++;
        this.Observe(nameof(OnStockTradingActionMessageCalls));
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
    void OnRegShoShortSalePriceTestRestrictedIndicatorMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnRegShoShortSalePriceTestRestrictedIndicatorMessageCalls++;
        this.Observe(nameof(OnRegShoShortSalePriceTestRestrictedIndicatorMessageCalls));
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
    void OnMarketParticipantPositionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MarketParticipantPositionMessage.Layout value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnMarketParticipantPositionMessageCalls++;
        this.Observe(nameof(OnMarketParticipantPositionMessageCalls));
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
    void OnMwcbDeclineLevelMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbDeclineLevelMessage.Layout value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnMwcbDeclineLevelMessageCalls++;
        this.Observe(nameof(OnMwcbDeclineLevelMessageCalls));
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
    void OnMwcbStatusLevelMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbStatusLevelMessage.Layout value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnMwcbStatusLevelMessageCalls++;
        this.Observe(nameof(OnMwcbStatusLevelMessageCalls));
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
    void OnIpoQuotingPeriodUpdate(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.IpoQuotingPeriodUpdate.Layout value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnIpoQuotingPeriodUpdateCalls++;
        this.Observe(nameof(OnIpoQuotingPeriodUpdateCalls));
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
    void OnLuldAuctionCollarMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LuldAuctionCollarMessage.Layout value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnLuldAuctionCollarMessageCalls++;
        this.Observe(nameof(OnLuldAuctionCollarMessageCalls));
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
    void OnOperationalHaltMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OperationalHaltMessage.Layout value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnOperationalHaltMessageCalls++;
        this.Observe(nameof(OnOperationalHaltMessageCalls));
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
    void OnAddOrderNoMpidAttributionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderNoMpidAttributionMessage.Layout value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnAddOrderNoMpidAttributionMessageCalls++;
        this.Observe(nameof(OnAddOrderNoMpidAttributionMessageCalls));
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
    void OnAddOrderWithMpidAttributionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderWithMpidAttributionMessage.Layout value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnAddOrderWithMpidAttributionMessageCalls++;
        this.Observe(nameof(OnAddOrderWithMpidAttributionMessageCalls));
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
    void OnOrderExecutedMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedMessage.Layout value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnOrderExecutedMessageCalls++;
        this.Observe(nameof(OnOrderExecutedMessageCalls));
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
    void OnOrderExecutedWithPriceMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedWithPriceMessage.Layout value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnOrderExecutedWithPriceMessageCalls++;
        this.Observe(nameof(OnOrderExecutedWithPriceMessageCalls));
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
    void OnOrderCancelMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderCancelMessage.Layout value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnOrderCancelMessageCalls++;
        this.Observe(nameof(OnOrderCancelMessageCalls));
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
    void OnOrderDeleteMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderDeleteMessage.Layout value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnOrderDeleteMessageCalls++;
        this.Observe(nameof(OnOrderDeleteMessageCalls));
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
    void OnOrderReplaceMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderReplaceMessage.Layout value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnOrderReplaceMessageCalls++;
        this.Observe(nameof(OnOrderReplaceMessageCalls));
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
    void OnNonCrossTradeMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NonCrossTradeMessage.Layout value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnNonCrossTradeMessageCalls++;
        this.Observe(nameof(OnNonCrossTradeMessageCalls));
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
    void OnCrossTradeMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossTradeMessage.Layout value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnCrossTradeMessageCalls++;
        this.Observe(nameof(OnCrossTradeMessageCalls));
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
    void OnBrokenTradeMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.BrokenTradeMessage.Layout value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnBrokenTradeMessageCalls++;
        this.Observe(nameof(OnBrokenTradeMessageCalls));
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
    void OnNetOrderImbalanceIndicatorMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NetOrderImbalanceIndicatorMessage.Layout value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnNetOrderImbalanceIndicatorMessageCalls++;
        this.Observe(nameof(OnNetOrderImbalanceIndicatorMessageCalls));
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
    void OnRetailPriceImprovementIndicatorMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RetailPriceImprovementIndicatorMessage.Layout value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnRetailPriceImprovementIndicatorMessageCalls++;
        this.Observe(nameof(OnRetailPriceImprovementIndicatorMessageCalls));
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
    void OnDirectListingWithCapitalRaisePriceDiscoveryMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnDirectListingWithCapitalRaisePriceDiscoveryMessageCalls++;
        this.Observe(nameof(OnDirectListingWithCapitalRaisePriceDiscoveryMessageCalls));
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
    void OnPacketHeader(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.PacketHeader.Layout value)
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
    void OnSystemEventMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SystemEventMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber)
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
    void OnStockDirectoryMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockDirectoryMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber)
    {
        this.Parsed++;
        this.OnStockDirectoryMessageCalls++;
        this.Observe(nameof(OnStockDirectoryMessageCalls));
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
    void OnStockTradingActionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockTradingActionMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber)
    {
        this.Parsed++;
        this.OnStockTradingActionMessageCalls++;
        this.Observe(nameof(OnStockTradingActionMessageCalls));
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
    void OnRegShoShortSalePriceTestRestrictedIndicatorMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber)
    {
        this.Parsed++;
        this.OnRegShoShortSalePriceTestRestrictedIndicatorMessageCalls++;
        this.Observe(nameof(OnRegShoShortSalePriceTestRestrictedIndicatorMessageCalls));
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
    void OnMarketParticipantPositionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MarketParticipantPositionMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber)
    {
        this.Parsed++;
        this.OnMarketParticipantPositionMessageCalls++;
        this.Observe(nameof(OnMarketParticipantPositionMessageCalls));
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
    void OnMwcbDeclineLevelMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbDeclineLevelMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber)
    {
        this.Parsed++;
        this.OnMwcbDeclineLevelMessageCalls++;
        this.Observe(nameof(OnMwcbDeclineLevelMessageCalls));
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
    void OnMwcbStatusLevelMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbStatusLevelMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber)
    {
        this.Parsed++;
        this.OnMwcbStatusLevelMessageCalls++;
        this.Observe(nameof(OnMwcbStatusLevelMessageCalls));
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
    void OnIpoQuotingPeriodUpdate(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.IpoQuotingPeriodUpdate.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber)
    {
        this.Parsed++;
        this.OnIpoQuotingPeriodUpdateCalls++;
        this.Observe(nameof(OnIpoQuotingPeriodUpdateCalls));
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
    void OnLuldAuctionCollarMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LuldAuctionCollarMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber)
    {
        this.Parsed++;
        this.OnLuldAuctionCollarMessageCalls++;
        this.Observe(nameof(OnLuldAuctionCollarMessageCalls));
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
    void OnOperationalHaltMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OperationalHaltMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber)
    {
        this.Parsed++;
        this.OnOperationalHaltMessageCalls++;
        this.Observe(nameof(OnOperationalHaltMessageCalls));
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
    void OnAddOrderNoMpidAttributionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderNoMpidAttributionMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber)
    {
        this.Parsed++;
        this.OnAddOrderNoMpidAttributionMessageCalls++;
        this.Observe(nameof(OnAddOrderNoMpidAttributionMessageCalls));
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
    void OnAddOrderWithMpidAttributionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderWithMpidAttributionMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber)
    {
        this.Parsed++;
        this.OnAddOrderWithMpidAttributionMessageCalls++;
        this.Observe(nameof(OnAddOrderWithMpidAttributionMessageCalls));
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
    void OnOrderExecutedMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber)
    {
        this.Parsed++;
        this.OnOrderExecutedMessageCalls++;
        this.Observe(nameof(OnOrderExecutedMessageCalls));
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
    void OnOrderExecutedWithPriceMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedWithPriceMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber)
    {
        this.Parsed++;
        this.OnOrderExecutedWithPriceMessageCalls++;
        this.Observe(nameof(OnOrderExecutedWithPriceMessageCalls));
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
    void OnOrderCancelMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderCancelMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber)
    {
        this.Parsed++;
        this.OnOrderCancelMessageCalls++;
        this.Observe(nameof(OnOrderCancelMessageCalls));
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
    void OnOrderDeleteMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderDeleteMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber)
    {
        this.Parsed++;
        this.OnOrderDeleteMessageCalls++;
        this.Observe(nameof(OnOrderDeleteMessageCalls));
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
    void OnOrderReplaceMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderReplaceMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber)
    {
        this.Parsed++;
        this.OnOrderReplaceMessageCalls++;
        this.Observe(nameof(OnOrderReplaceMessageCalls));
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
    void OnNonCrossTradeMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NonCrossTradeMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber)
    {
        this.Parsed++;
        this.OnNonCrossTradeMessageCalls++;
        this.Observe(nameof(OnNonCrossTradeMessageCalls));
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
    void OnCrossTradeMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossTradeMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber)
    {
        this.Parsed++;
        this.OnCrossTradeMessageCalls++;
        this.Observe(nameof(OnCrossTradeMessageCalls));
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
    void OnBrokenTradeMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.BrokenTradeMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber)
    {
        this.Parsed++;
        this.OnBrokenTradeMessageCalls++;
        this.Observe(nameof(OnBrokenTradeMessageCalls));
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
    void OnNetOrderImbalanceIndicatorMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NetOrderImbalanceIndicatorMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber)
    {
        this.Parsed++;
        this.OnNetOrderImbalanceIndicatorMessageCalls++;
        this.Observe(nameof(OnNetOrderImbalanceIndicatorMessageCalls));
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
    void OnRetailPriceImprovementIndicatorMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RetailPriceImprovementIndicatorMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber)
    {
        this.Parsed++;
        this.OnRetailPriceImprovementIndicatorMessageCalls++;
        this.Observe(nameof(OnRetailPriceImprovementIndicatorMessageCalls));
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
    void OnDirectListingWithCapitalRaisePriceDiscoveryMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber)
    {
        this.Parsed++;
        this.OnDirectListingWithCapitalRaisePriceDiscoveryMessageCalls++;
        this.Observe(nameof(OnDirectListingWithCapitalRaisePriceDiscoveryMessageCalls));
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
    void OnClientPacket(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPacket.Reader value)
    {
        this.Parsed++;
        this.OnClientPacketCalls++;
        this.Observe(nameof(OnClientPacketCalls));
    }
    void OnServerPacket(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPacket.Reader value)
    {
        this.Parsed++;
        this.OnServerPacketCalls++;
        this.Observe(nameof(OnServerPacketCalls));
    }
    void OnPacket(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Packet.Reader value)
    {
        this.Parsed++;
        this.OnPacketCalls++;
        this.Observe(nameof(OnPacketCalls));
    }
    void OnFile(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.File.Reader value)
    {
        this.Parsed++;
        this.OnFileCalls++;
        this.Observe(nameof(OnFileCalls));
    }
    readonly global::System.Collections.Generic.HashSet<string> differing = new();

    static void Skip<T>(global::System.Span<bool> padding, global::System.ReadOnlySpan<byte> bytes, in T field)
    {
        var offset = (int)global::System.Runtime.CompilerServices.Unsafe.ByteOffset(ref global::System.Runtime.InteropServices.MemoryMarshal.GetReference(bytes), ref global::System.Runtime.CompilerServices.Unsafe.As<T, byte>(ref global::System.Runtime.CompilerServices.Unsafe.AsRef(in field)));
        padding.Slice(offset, global::System.Runtime.CompilerServices.Unsafe.SizeOf<T>()).Fill(true);
    }

    void Compare(string message, global::System.ReadOnlySpan<byte> written, global::System.ReadOnlySpan<byte> captured, global::System.ReadOnlySpan<bool> padding)
    {
        var same = 0;
        while (same < written.Length && same < captured.Length && (written[same] == captured[same] || padding[same])) same++;
        if (same == written.Length && same == captured.Length) this.Rewritten++;
        else this.Differ(message, $"{written.Length} bytes written for {captured.Length} captured; the first difference is at byte {same}");
    }

    void Differ(string message, string difference)
    {
        this.Mismatched++;
        if (this.differing.Add(message)) global::System.Console.Error.WriteLine($"FAIL: the write of a captured {message} differs from its bytes: {difference}");
    }

    static global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedDataPacket.Value ValueOf(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedDataPacket.Reader reader) => new() { SequencedMessage = ValueOf(reader.SequencedMessage) };
    static global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value ValueOf(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Reader reader) => reader.Case switch { global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.SystemEventMessage => new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value { SystemEventMessage = new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SystemEventMessage.Value { StockLocate = reader.SystemEventMessage.StockLocate, TrackingNumber = reader.SystemEventMessage.TrackingNumber, Timestamp = reader.SystemEventMessage.Timestamp, EventCode = reader.SystemEventMessage.EventCode } }, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.StockDirectoryMessage => new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value { StockDirectoryMessage = new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockDirectoryMessage.Value { StockLocate = reader.StockDirectoryMessage.StockLocate, TrackingNumber = reader.StockDirectoryMessage.TrackingNumber, Timestamp = reader.StockDirectoryMessage.Timestamp, Stock = reader.StockDirectoryMessage.Stock, MarketCategory = reader.StockDirectoryMessage.MarketCategory, FinancialStatusIndicator = reader.StockDirectoryMessage.FinancialStatusIndicator, RoundLotSize = reader.StockDirectoryMessage.RoundLotSize, RoundLotsOnly = reader.StockDirectoryMessage.RoundLotsOnly, IssueClassification = reader.StockDirectoryMessage.IssueClassification, IssueSubType = reader.StockDirectoryMessage.IssueSubType, Authenticity = reader.StockDirectoryMessage.Authenticity, ShortSaleThresholdIndicator = reader.StockDirectoryMessage.ShortSaleThresholdIndicator, IpoFlag = reader.StockDirectoryMessage.IpoFlag, LuldReferencePriceTier = reader.StockDirectoryMessage.LuldReferencePriceTier, EtpFlag = reader.StockDirectoryMessage.EtpFlag, EtpLeverageFactor = reader.StockDirectoryMessage.EtpLeverageFactor, InverseIndicator = reader.StockDirectoryMessage.InverseIndicator } }, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.StockTradingActionMessage => new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value { StockTradingActionMessage = new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockTradingActionMessage.Value { StockLocate = reader.StockTradingActionMessage.StockLocate, TrackingNumber = reader.StockTradingActionMessage.TrackingNumber, Timestamp = reader.StockTradingActionMessage.Timestamp, Stock = reader.StockTradingActionMessage.Stock, TradingState = reader.StockTradingActionMessage.TradingState, Reserved = reader.StockTradingActionMessage.Reserved, ReasonCode = reader.StockTradingActionMessage.ReasonCode } }, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.RegShoShortSalePriceTestRestrictedIndicatorMessage => new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value { RegShoShortSalePriceTestRestrictedIndicatorMessage = new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RegShoShortSalePriceTestRestrictedIndicatorMessage.Value { LocateCode = reader.RegShoShortSalePriceTestRestrictedIndicatorMessage.LocateCode, TrackingNumber = reader.RegShoShortSalePriceTestRestrictedIndicatorMessage.TrackingNumber, Timestamp = reader.RegShoShortSalePriceTestRestrictedIndicatorMessage.Timestamp, Stock = reader.RegShoShortSalePriceTestRestrictedIndicatorMessage.Stock, RegShoAction = reader.RegShoShortSalePriceTestRestrictedIndicatorMessage.RegShoAction } }, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.MarketParticipantPositionMessage => new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value { MarketParticipantPositionMessage = new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MarketParticipantPositionMessage.Value { StockLocate = reader.MarketParticipantPositionMessage.StockLocate, TrackingNumber = reader.MarketParticipantPositionMessage.TrackingNumber, Timestamp = reader.MarketParticipantPositionMessage.Timestamp, Mpid = reader.MarketParticipantPositionMessage.Mpid, Stock = reader.MarketParticipantPositionMessage.Stock, PrimaryMarketMaker = reader.MarketParticipantPositionMessage.PrimaryMarketMaker, MarketMakerMode = reader.MarketParticipantPositionMessage.MarketMakerMode, MarketParticipantState = reader.MarketParticipantPositionMessage.MarketParticipantState } }, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.MwcbDeclineLevelMessage => new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value { MwcbDeclineLevelMessage = new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbDeclineLevelMessage.Value { StockLocate = reader.MwcbDeclineLevelMessage.StockLocate, TrackingNumber = reader.MwcbDeclineLevelMessage.TrackingNumber, Timestamp = reader.MwcbDeclineLevelMessage.Timestamp, Level1 = reader.MwcbDeclineLevelMessage.Level1, Level2 = reader.MwcbDeclineLevelMessage.Level2, Level3 = reader.MwcbDeclineLevelMessage.Level3 } }, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.MwcbStatusLevelMessage => new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value { MwcbStatusLevelMessage = new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbStatusLevelMessage.Value { StockLocate = reader.MwcbStatusLevelMessage.StockLocate, TrackingNumber = reader.MwcbStatusLevelMessage.TrackingNumber, Timestamp = reader.MwcbStatusLevelMessage.Timestamp, BreachedLevel = reader.MwcbStatusLevelMessage.BreachedLevel } }, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.IpoQuotingPeriodUpdate => new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value { IpoQuotingPeriodUpdate = new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.IpoQuotingPeriodUpdate.Value { StockLocate = reader.IpoQuotingPeriodUpdate.StockLocate, TrackingNumber = reader.IpoQuotingPeriodUpdate.TrackingNumber, Timestamp = reader.IpoQuotingPeriodUpdate.Timestamp, Stock = reader.IpoQuotingPeriodUpdate.Stock, IpoQuotationReleaseTime = reader.IpoQuotingPeriodUpdate.IpoQuotationReleaseTime, IpoQuotationReleaseQualifier = reader.IpoQuotingPeriodUpdate.IpoQuotationReleaseQualifier, IpoPrice = reader.IpoQuotingPeriodUpdate.IpoPrice } }, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.LuldAuctionCollarMessage => new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value { LuldAuctionCollarMessage = new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LuldAuctionCollarMessage.Value { StockLocate = reader.LuldAuctionCollarMessage.StockLocate, TrackingNumber = reader.LuldAuctionCollarMessage.TrackingNumber, Timestamp = reader.LuldAuctionCollarMessage.Timestamp, Stock = reader.LuldAuctionCollarMessage.Stock, AuctionCollarReferencePrice = reader.LuldAuctionCollarMessage.AuctionCollarReferencePrice, UpperAuctionCollarPrice = reader.LuldAuctionCollarMessage.UpperAuctionCollarPrice, LowerAuctionCollarPrice = reader.LuldAuctionCollarMessage.LowerAuctionCollarPrice, AuctionCollarExtension = reader.LuldAuctionCollarMessage.AuctionCollarExtension } }, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OperationalHaltMessage => new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value { OperationalHaltMessage = new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OperationalHaltMessage.Value { StockLocate = reader.OperationalHaltMessage.StockLocate, TrackingNumber = reader.OperationalHaltMessage.TrackingNumber, Timestamp = reader.OperationalHaltMessage.Timestamp, Stock = reader.OperationalHaltMessage.Stock, MarketCode = reader.OperationalHaltMessage.MarketCode, OperationalHaltAction = reader.OperationalHaltMessage.OperationalHaltAction } }, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.AddOrderNoMpidAttributionMessage => new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value { AddOrderNoMpidAttributionMessage = new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderNoMpidAttributionMessage.Value { StockLocate = reader.AddOrderNoMpidAttributionMessage.StockLocate, TrackingNumber = reader.AddOrderNoMpidAttributionMessage.TrackingNumber, Timestamp = reader.AddOrderNoMpidAttributionMessage.Timestamp, OrderReferenceNumber = reader.AddOrderNoMpidAttributionMessage.OrderReferenceNumber, BuySellIndicator = reader.AddOrderNoMpidAttributionMessage.BuySellIndicator, Shares = reader.AddOrderNoMpidAttributionMessage.Shares, Stock = reader.AddOrderNoMpidAttributionMessage.Stock, Price = reader.AddOrderNoMpidAttributionMessage.Price } }, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.AddOrderWithMpidAttributionMessage => new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value { AddOrderWithMpidAttributionMessage = new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderWithMpidAttributionMessage.Value { StockLocate = reader.AddOrderWithMpidAttributionMessage.StockLocate, TrackingNumber = reader.AddOrderWithMpidAttributionMessage.TrackingNumber, Timestamp = reader.AddOrderWithMpidAttributionMessage.Timestamp, OrderReferenceNumber = reader.AddOrderWithMpidAttributionMessage.OrderReferenceNumber, BuySellIndicator = reader.AddOrderWithMpidAttributionMessage.BuySellIndicator, Shares = reader.AddOrderWithMpidAttributionMessage.Shares, Stock = reader.AddOrderWithMpidAttributionMessage.Stock, Price = reader.AddOrderWithMpidAttributionMessage.Price, Attribution = reader.AddOrderWithMpidAttributionMessage.Attribution } }, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderExecutedMessage => new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value { OrderExecutedMessage = new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedMessage.Value { StockLocate = reader.OrderExecutedMessage.StockLocate, TrackingNumber = reader.OrderExecutedMessage.TrackingNumber, Timestamp = reader.OrderExecutedMessage.Timestamp, OrderReferenceNumber = reader.OrderExecutedMessage.OrderReferenceNumber, ExecutedShares = reader.OrderExecutedMessage.ExecutedShares, MatchNumber = reader.OrderExecutedMessage.MatchNumber } }, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderExecutedWithPriceMessage => new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value { OrderExecutedWithPriceMessage = new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedWithPriceMessage.Value { StockLocate = reader.OrderExecutedWithPriceMessage.StockLocate, TrackingNumber = reader.OrderExecutedWithPriceMessage.TrackingNumber, Timestamp = reader.OrderExecutedWithPriceMessage.Timestamp, OrderReferenceNumber = reader.OrderExecutedWithPriceMessage.OrderReferenceNumber, ExecutedShares = reader.OrderExecutedWithPriceMessage.ExecutedShares, MatchNumber = reader.OrderExecutedWithPriceMessage.MatchNumber, Printable = reader.OrderExecutedWithPriceMessage.Printable, ExecutionPrice = reader.OrderExecutedWithPriceMessage.ExecutionPrice } }, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderCancelMessage => new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value { OrderCancelMessage = new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderCancelMessage.Value { StockLocate = reader.OrderCancelMessage.StockLocate, TrackingNumber = reader.OrderCancelMessage.TrackingNumber, Timestamp = reader.OrderCancelMessage.Timestamp, OrderReferenceNumber = reader.OrderCancelMessage.OrderReferenceNumber, CanceledShares = reader.OrderCancelMessage.CanceledShares } }, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderDeleteMessage => new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value { OrderDeleteMessage = new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderDeleteMessage.Value { StockLocate = reader.OrderDeleteMessage.StockLocate, TrackingNumber = reader.OrderDeleteMessage.TrackingNumber, Timestamp = reader.OrderDeleteMessage.Timestamp, OrderReferenceNumber = reader.OrderDeleteMessage.OrderReferenceNumber } }, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderReplaceMessage => new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value { OrderReplaceMessage = new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderReplaceMessage.Value { StockLocate = reader.OrderReplaceMessage.StockLocate, TrackingNumber = reader.OrderReplaceMessage.TrackingNumber, Timestamp = reader.OrderReplaceMessage.Timestamp, OriginalOrderReferenceNumber = reader.OrderReplaceMessage.OriginalOrderReferenceNumber, NewOrderReferenceNumber = reader.OrderReplaceMessage.NewOrderReferenceNumber, Shares = reader.OrderReplaceMessage.Shares, Price = reader.OrderReplaceMessage.Price } }, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.NonCrossTradeMessage => new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value { NonCrossTradeMessage = new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NonCrossTradeMessage.Value { StockLocate = reader.NonCrossTradeMessage.StockLocate, TrackingNumber = reader.NonCrossTradeMessage.TrackingNumber, Timestamp = reader.NonCrossTradeMessage.Timestamp, OrderReferenceNumber = reader.NonCrossTradeMessage.OrderReferenceNumber, BuySellIndicator = reader.NonCrossTradeMessage.BuySellIndicator, Shares = reader.NonCrossTradeMessage.Shares, Stock = reader.NonCrossTradeMessage.Stock, Price = reader.NonCrossTradeMessage.Price, MatchNumber = reader.NonCrossTradeMessage.MatchNumber } }, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.CrossTradeMessage => new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value { CrossTradeMessage = new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossTradeMessage.Value { StockLocate = reader.CrossTradeMessage.StockLocate, TrackingNumber = reader.CrossTradeMessage.TrackingNumber, Timestamp = reader.CrossTradeMessage.Timestamp, CrossShares = reader.CrossTradeMessage.CrossShares, Stock = reader.CrossTradeMessage.Stock, CrossPrice = reader.CrossTradeMessage.CrossPrice, MatchNumber = reader.CrossTradeMessage.MatchNumber, CrossType = reader.CrossTradeMessage.CrossType } }, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.BrokenTradeMessage => new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value { BrokenTradeMessage = new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.BrokenTradeMessage.Value { StockLocate = reader.BrokenTradeMessage.StockLocate, TrackingNumber = reader.BrokenTradeMessage.TrackingNumber, Timestamp = reader.BrokenTradeMessage.Timestamp, MatchNumber = reader.BrokenTradeMessage.MatchNumber } }, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.NetOrderImbalanceIndicatorMessage => new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value { NetOrderImbalanceIndicatorMessage = new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NetOrderImbalanceIndicatorMessage.Value { StockLocate = reader.NetOrderImbalanceIndicatorMessage.StockLocate, TrackingNumber = reader.NetOrderImbalanceIndicatorMessage.TrackingNumber, Timestamp = reader.NetOrderImbalanceIndicatorMessage.Timestamp, PairedShares = reader.NetOrderImbalanceIndicatorMessage.PairedShares, ImbalanceShares = reader.NetOrderImbalanceIndicatorMessage.ImbalanceShares, ImbalanceDirection = reader.NetOrderImbalanceIndicatorMessage.ImbalanceDirection, Stock = reader.NetOrderImbalanceIndicatorMessage.Stock, FarPrice = reader.NetOrderImbalanceIndicatorMessage.FarPrice, NearPrice = reader.NetOrderImbalanceIndicatorMessage.NearPrice, CurrentReferencePrice = reader.NetOrderImbalanceIndicatorMessage.CurrentReferencePrice, CrossType = reader.NetOrderImbalanceIndicatorMessage.CrossType, PriceVariationIndicator = reader.NetOrderImbalanceIndicatorMessage.PriceVariationIndicator } }, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.RetailPriceImprovementIndicatorMessage => new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value { RetailPriceImprovementIndicatorMessage = new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RetailPriceImprovementIndicatorMessage.Value { StockLocate = reader.RetailPriceImprovementIndicatorMessage.StockLocate, TrackingNumber = reader.RetailPriceImprovementIndicatorMessage.TrackingNumber, Timestamp = reader.RetailPriceImprovementIndicatorMessage.Timestamp, Stock = reader.RetailPriceImprovementIndicatorMessage.Stock, InterestFlag = reader.RetailPriceImprovementIndicatorMessage.InterestFlag } }, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.DirectListingWithCapitalRaisePriceDiscoveryMessage => new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value { DirectListingWithCapitalRaisePriceDiscoveryMessage = new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DirectListingWithCapitalRaisePriceDiscoveryMessage.Value { StockLocate = reader.DirectListingWithCapitalRaisePriceDiscoveryMessage.StockLocate, TrackingNumber = reader.DirectListingWithCapitalRaisePriceDiscoveryMessage.TrackingNumber, Timestamp = reader.DirectListingWithCapitalRaisePriceDiscoveryMessage.Timestamp, Stock = reader.DirectListingWithCapitalRaisePriceDiscoveryMessage.Stock, OpenEligibilityStatus = reader.DirectListingWithCapitalRaisePriceDiscoveryMessage.OpenEligibilityStatus, MinimumAllowablePrice = reader.DirectListingWithCapitalRaisePriceDiscoveryMessage.MinimumAllowablePrice, MaximumAllowablePrice = reader.DirectListingWithCapitalRaisePriceDiscoveryMessage.MaximumAllowablePrice, NearExecutionPrice = reader.DirectListingWithCapitalRaisePriceDiscoveryMessage.NearExecutionPrice, NearExecutionTime = reader.DirectListingWithCapitalRaisePriceDiscoveryMessage.NearExecutionTime, LowerPriceRangeCollar = reader.DirectListingWithCapitalRaisePriceDiscoveryMessage.LowerPriceRangeCollar, UpperPriceRangeCollar = reader.DirectListingWithCapitalRaisePriceDiscoveryMessage.UpperPriceRangeCollar } }, _ => default };
}
