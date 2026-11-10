using Nasdaq.NtxEquities.TotalView.Itch.v5_0;
using Nasdaq.NtxEquities.TotalView.Itch.v5_0.Iterator;
using Nasdaq.NtxEquities.TotalView.Itch.v5_0.Test;
using static Nasdaq.NtxEquities.TotalView.Itch.v5_0.Test.Dump;

const int PcapArgument = 0;
const int FirstOption = 1;
const int None = 0;
const int OneFeed = 1;
const int Success = 0;
const int Failure = 1;
const int UsageFailure = 2;
const string FeedOption = "--feed";
const string StrictOption = "--strict";
const string Usage = $"usage: Nasdaq.NtxEquities.TotalView.Itch.v5.0.Test <pcap> [{FeedOption} <name>] [{StrictOption}]";
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
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RetailInterestMessage.Layout>() != global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RetailInterestMessage.Layout.Size || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RetailInterestMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RetailInterestMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RetailInterestMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RetailInterestMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RetailInterestMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RetailInterestMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RetailInterestMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RetailInterestMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RetailInterestMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RetailInterestMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RetailInterestMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RetailInterestMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RetailInterestMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RetailInterestMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RetailInterestMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RetailInterestMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NetOrderImbalanceIndicatorMessage.Layout>() != global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NetOrderImbalanceIndicatorMessage.Layout.Size || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NetOrderImbalanceIndicatorMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NetOrderImbalanceIndicatorMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NetOrderImbalanceIndicatorMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NetOrderImbalanceIndicatorMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NetOrderImbalanceIndicatorMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NetOrderImbalanceIndicatorMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NetOrderImbalanceIndicatorMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NetOrderImbalanceIndicatorMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NetOrderImbalanceIndicatorMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NetOrderImbalanceIndicatorMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NetOrderImbalanceIndicatorMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NetOrderImbalanceIndicatorMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NetOrderImbalanceIndicatorMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NetOrderImbalanceIndicatorMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NetOrderImbalanceIndicatorMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NetOrderImbalanceIndicatorMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.BrokenTradeMessage.Layout>() != global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.BrokenTradeMessage.Layout.Size || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.BrokenTradeMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.BrokenTradeMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.BrokenTradeMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.BrokenTradeMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.BrokenTradeMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.BrokenTradeMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.BrokenTradeMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.BrokenTradeMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.BrokenTradeMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.BrokenTradeMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.BrokenTradeMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.BrokenTradeMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.BrokenTradeMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.BrokenTradeMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.BrokenTradeMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.BrokenTradeMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.CrossTradeMessage.Layout>() != global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.CrossTradeMessage.Layout.Size || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.CrossTradeMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.CrossTradeMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.CrossTradeMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.CrossTradeMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.CrossTradeMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.CrossTradeMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.CrossTradeMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.CrossTradeMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.CrossTradeMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.CrossTradeMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.CrossTradeMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.CrossTradeMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.CrossTradeMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.CrossTradeMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.CrossTradeMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.CrossTradeMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NonCrossTradeMessage.Layout>() != global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NonCrossTradeMessage.Layout.Size || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NonCrossTradeMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NonCrossTradeMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NonCrossTradeMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NonCrossTradeMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NonCrossTradeMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NonCrossTradeMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NonCrossTradeMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NonCrossTradeMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NonCrossTradeMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NonCrossTradeMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NonCrossTradeMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NonCrossTradeMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NonCrossTradeMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NonCrossTradeMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NonCrossTradeMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NonCrossTradeMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderReplaceMessage.Layout>() != global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderReplaceMessage.Layout.Size || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderReplaceMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderReplaceMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderReplaceMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderReplaceMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderReplaceMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderReplaceMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderReplaceMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderReplaceMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderReplaceMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderReplaceMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderReplaceMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderReplaceMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderReplaceMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderReplaceMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderReplaceMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderReplaceMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderDeleteMessage.Layout>() != global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderDeleteMessage.Layout.Size || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderDeleteMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderDeleteMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderDeleteMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderDeleteMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderDeleteMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderDeleteMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderDeleteMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderDeleteMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderDeleteMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderDeleteMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderDeleteMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderDeleteMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderDeleteMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderDeleteMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderDeleteMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderDeleteMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderCancelMessage.Layout>() != global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderCancelMessage.Layout.Size || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderCancelMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderCancelMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderCancelMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderCancelMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderCancelMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderCancelMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderCancelMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderCancelMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderCancelMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderCancelMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderCancelMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderCancelMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderCancelMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderCancelMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderCancelMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderCancelMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedWithPriceMessage.Layout>() != global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedWithPriceMessage.Layout.Size || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedWithPriceMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedWithPriceMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedWithPriceMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedWithPriceMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedWithPriceMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedWithPriceMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedWithPriceMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedWithPriceMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedWithPriceMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedWithPriceMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedWithPriceMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedWithPriceMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedWithPriceMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedWithPriceMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedWithPriceMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedWithPriceMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedMessage.Layout>() != global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedMessage.Layout.Size || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderMpidAttributionMessage.Layout>() != global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderMpidAttributionMessage.Layout.Size || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderMpidAttributionMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderMpidAttributionMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderMpidAttributionMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderMpidAttributionMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderMpidAttributionMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderMpidAttributionMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderMpidAttributionMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderMpidAttributionMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderMpidAttributionMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderMpidAttributionMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderMpidAttributionMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderMpidAttributionMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderMpidAttributionMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderMpidAttributionMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderMpidAttributionMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderMpidAttributionMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderNoMpidAttributionMessage.Layout>() != global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderNoMpidAttributionMessage.Layout.Size || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderNoMpidAttributionMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderNoMpidAttributionMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderNoMpidAttributionMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderNoMpidAttributionMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderNoMpidAttributionMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderNoMpidAttributionMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderNoMpidAttributionMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderNoMpidAttributionMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderNoMpidAttributionMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderNoMpidAttributionMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderNoMpidAttributionMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderNoMpidAttributionMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderNoMpidAttributionMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderNoMpidAttributionMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderNoMpidAttributionMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderNoMpidAttributionMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OperationalHaltMessage.Layout>() != global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OperationalHaltMessage.Layout.Size || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OperationalHaltMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OperationalHaltMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OperationalHaltMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OperationalHaltMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OperationalHaltMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OperationalHaltMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OperationalHaltMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OperationalHaltMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OperationalHaltMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OperationalHaltMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OperationalHaltMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OperationalHaltMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OperationalHaltMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OperationalHaltMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OperationalHaltMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OperationalHaltMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.LuldAuctionCollarMessage.Layout>() != global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.LuldAuctionCollarMessage.Layout.Size || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.LuldAuctionCollarMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.LuldAuctionCollarMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.LuldAuctionCollarMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.LuldAuctionCollarMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.LuldAuctionCollarMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.LuldAuctionCollarMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.LuldAuctionCollarMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.LuldAuctionCollarMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.LuldAuctionCollarMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.LuldAuctionCollarMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.LuldAuctionCollarMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.LuldAuctionCollarMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.LuldAuctionCollarMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.LuldAuctionCollarMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.LuldAuctionCollarMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.LuldAuctionCollarMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbStatusMessage.Layout>() != global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbStatusMessage.Layout.Size || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbStatusMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbStatusMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbStatusMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbStatusMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbStatusMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbStatusMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbStatusMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbStatusMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbStatusMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbStatusMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbStatusMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbStatusMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbStatusMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbStatusMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbStatusMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbStatusMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbDeclineLevelMessage.Layout>() != global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbDeclineLevelMessage.Layout.Size || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbDeclineLevelMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbDeclineLevelMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbDeclineLevelMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbDeclineLevelMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbDeclineLevelMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbDeclineLevelMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbDeclineLevelMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbDeclineLevelMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbDeclineLevelMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbDeclineLevelMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbDeclineLevelMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbDeclineLevelMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbDeclineLevelMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbDeclineLevelMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbDeclineLevelMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbDeclineLevelMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MarketParticipantPositionMessage.Layout>() != global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MarketParticipantPositionMessage.Layout.Size || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MarketParticipantPositionMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MarketParticipantPositionMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MarketParticipantPositionMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MarketParticipantPositionMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MarketParticipantPositionMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MarketParticipantPositionMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MarketParticipantPositionMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MarketParticipantPositionMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MarketParticipantPositionMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MarketParticipantPositionMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MarketParticipantPositionMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MarketParticipantPositionMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MarketParticipantPositionMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MarketParticipantPositionMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MarketParticipantPositionMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MarketParticipantPositionMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout>() != global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout.Size || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RegShoShortSalePriceTestRestrictedIndicatorMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RegShoShortSalePriceTestRestrictedIndicatorMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockTradingActionMessage.Layout>() != global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockTradingActionMessage.Layout.Size || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockTradingActionMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockTradingActionMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockTradingActionMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockTradingActionMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockTradingActionMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockTradingActionMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockTradingActionMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockTradingActionMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockTradingActionMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockTradingActionMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockTradingActionMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockTradingActionMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockTradingActionMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockTradingActionMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockTradingActionMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockTradingActionMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockDirectoryMessage.Layout>() != global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockDirectoryMessage.Layout.Size || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockDirectoryMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockDirectoryMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockDirectoryMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockDirectoryMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockDirectoryMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockDirectoryMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockDirectoryMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockDirectoryMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockDirectoryMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockDirectoryMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockDirectoryMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockDirectoryMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockDirectoryMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockDirectoryMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockDirectoryMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockDirectoryMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.SystemEventMessage.Layout>() != global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.SystemEventMessage.Layout.Size || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.SystemEventMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.SystemEventMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.SystemEventMessage.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.SystemEventMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.SystemEventMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.SystemEventMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.SystemEventMessage.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.SystemEventMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.SystemEventMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.SystemEventMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.SystemEventMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.SystemEventMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.SystemEventMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.SystemEventMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.SystemEventMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.SystemEventMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout>() != global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout.Size || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout.Size];
    var filled = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout.Initialize(zeroed);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.PacketHeader.Layout>() != global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.PacketHeader.Layout.Size || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.PacketHeader.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.PacketHeader.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.PacketHeader.Layout.Read(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.PacketHeader.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.PacketHeader.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.PacketHeader.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.PacketHeader.Layout.Initialize(new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.PacketHeader.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.PacketHeader)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.PacketHeader.Layout.Size];
    var filled = new byte[global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.PacketHeader.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.PacketHeader.Layout.Initialize(zeroed);
    global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.PacketHeader.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.PacketHeader.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.PacketHeader.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.PacketHeader)} Layout does not clear exactly its bytes.");
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
if (walker.OnRetailInterestMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnRetailInterestMessageCalls)}: {walker.OnRetailInterestMessageCalls}");
if (walker.OnNetOrderImbalanceIndicatorMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnNetOrderImbalanceIndicatorMessageCalls)}: {walker.OnNetOrderImbalanceIndicatorMessageCalls}");
if (walker.OnBrokenTradeMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnBrokenTradeMessageCalls)}: {walker.OnBrokenTradeMessageCalls}");
if (walker.OnCrossTradeMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnCrossTradeMessageCalls)}: {walker.OnCrossTradeMessageCalls}");
if (walker.OnNonCrossTradeMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnNonCrossTradeMessageCalls)}: {walker.OnNonCrossTradeMessageCalls}");
if (walker.OnOrderReplaceMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnOrderReplaceMessageCalls)}: {walker.OnOrderReplaceMessageCalls}");
if (walker.OnOrderDeleteMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnOrderDeleteMessageCalls)}: {walker.OnOrderDeleteMessageCalls}");
if (walker.OnOrderCancelMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnOrderCancelMessageCalls)}: {walker.OnOrderCancelMessageCalls}");
if (walker.OnOrderExecutedWithPriceMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnOrderExecutedWithPriceMessageCalls)}: {walker.OnOrderExecutedWithPriceMessageCalls}");
if (walker.OnOrderExecutedMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnOrderExecutedMessageCalls)}: {walker.OnOrderExecutedMessageCalls}");
if (walker.OnAddOrderMpidAttributionMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnAddOrderMpidAttributionMessageCalls)}: {walker.OnAddOrderMpidAttributionMessageCalls}");
if (walker.OnAddOrderNoMpidAttributionMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnAddOrderNoMpidAttributionMessageCalls)}: {walker.OnAddOrderNoMpidAttributionMessageCalls}");
if (walker.OnOperationalHaltMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnOperationalHaltMessageCalls)}: {walker.OnOperationalHaltMessageCalls}");
if (walker.OnLuldAuctionCollarMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnLuldAuctionCollarMessageCalls)}: {walker.OnLuldAuctionCollarMessageCalls}");
if (walker.OnMwcbStatusMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnMwcbStatusMessageCalls)}: {walker.OnMwcbStatusMessageCalls}");
if (walker.OnMwcbDeclineLevelMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnMwcbDeclineLevelMessageCalls)}: {walker.OnMwcbDeclineLevelMessageCalls}");
if (walker.OnMarketParticipantPositionMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnMarketParticipantPositionMessageCalls)}: {walker.OnMarketParticipantPositionMessageCalls}");
if (walker.OnRegShoShortSalePriceTestRestrictedIndicatorMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnRegShoShortSalePriceTestRestrictedIndicatorMessageCalls)}: {walker.OnRegShoShortSalePriceTestRestrictedIndicatorMessageCalls}");
if (walker.OnStockTradingActionMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnStockTradingActionMessageCalls)}: {walker.OnStockTradingActionMessageCalls}");
if (walker.OnStockDirectoryMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnStockDirectoryMessageCalls)}: {walker.OnStockDirectoryMessageCalls}");
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
    ///  The number of calls of the OnMwcbStatusMessage method.
    /// </summary>
    public long OnMwcbStatusMessageCalls;
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
    ///  The number of calls of the OnAddOrderMpidAttributionMessage method.
    /// </summary>
    public long OnAddOrderMpidAttributionMessageCalls;
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
    ///  The number of calls of the OnRetailInterestMessage method.
    /// </summary>
    public long OnRetailInterestMessageCalls;
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
    public void VisitPacket(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Packet.Reader packet)
    {
        this.BeginPacketSequence();
        this.SeedPacketSequence(ulong.CreateSaturating(packet.PacketHeader.SequenceNumber.Value));
        this.OnPacketHeader(in packet.PacketHeader);
        var messages = packet.Messages;
        switch (messages.Case)
        {
            case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Messages.Case.Heartbeat:
            {
                this.OnHeartbeat();
                break;
            }
            case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Messages.Case.EndOfSession:
            {
                this.OnEndOfSession();
                break;
            }
            case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Messages.Case.Message:
            {
                var messageEntries = messages.Message;
                var messageRead = 0L;
                while (messageEntries.MoveNext())
                {
                    var message = messageEntries.Current;
                    {
                        ulong? expected = null;
                        expected = this.NextPacketSequence();
                        this.CheckSequence((ulong?)message.SequenceNumber, expected, nameof(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Message));
                    }
                    this.numberedDepth++;
                    var payload = message.Payload;
                    switch (payload.Case)
                    {
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.SystemEventMessage:
                        {
                            this.OnSystemEventMessage(in payload.SystemEventMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.StockDirectoryMessage:
                        {
                            this.OnStockDirectoryMessage(in payload.StockDirectoryMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.StockTradingActionMessage:
                        {
                            this.OnStockTradingActionMessage(in payload.StockTradingActionMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.RegShoShortSalePriceTestRestrictedIndicatorMessage:
                        {
                            this.OnRegShoShortSalePriceTestRestrictedIndicatorMessage(in payload.RegShoShortSalePriceTestRestrictedIndicatorMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.MarketParticipantPositionMessage:
                        {
                            this.OnMarketParticipantPositionMessage(in payload.MarketParticipantPositionMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.MwcbDeclineLevelMessage:
                        {
                            this.OnMwcbDeclineLevelMessage(in payload.MwcbDeclineLevelMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.MwcbStatusMessage:
                        {
                            this.OnMwcbStatusMessage(in payload.MwcbStatusMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.LuldAuctionCollarMessage:
                        {
                            this.OnLuldAuctionCollarMessage(in payload.LuldAuctionCollarMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.OperationalHaltMessage:
                        {
                            this.OnOperationalHaltMessage(in payload.OperationalHaltMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.AddOrderNoMpidAttributionMessage:
                        {
                            this.OnAddOrderNoMpidAttributionMessage(in payload.AddOrderNoMpidAttributionMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.AddOrderMpidAttributionMessage:
                        {
                            this.OnAddOrderMpidAttributionMessage(in payload.AddOrderMpidAttributionMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.OrderExecutedMessage:
                        {
                            this.OnOrderExecutedMessage(in payload.OrderExecutedMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.OrderExecutedWithPriceMessage:
                        {
                            this.OnOrderExecutedWithPriceMessage(in payload.OrderExecutedWithPriceMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.OrderCancelMessage:
                        {
                            this.OnOrderCancelMessage(in payload.OrderCancelMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.OrderDeleteMessage:
                        {
                            this.OnOrderDeleteMessage(in payload.OrderDeleteMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.OrderReplaceMessage:
                        {
                            this.OnOrderReplaceMessage(in payload.OrderReplaceMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.NonCrossTradeMessage:
                        {
                            this.OnNonCrossTradeMessage(in payload.NonCrossTradeMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.CrossTradeMessage:
                        {
                            this.OnCrossTradeMessage(in payload.CrossTradeMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.BrokenTradeMessage:
                        {
                            this.OnBrokenTradeMessage(in payload.BrokenTradeMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.NetOrderImbalanceIndicatorMessage:
                        {
                            this.OnNetOrderImbalanceIndicatorMessage(in payload.NetOrderImbalanceIndicatorMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.RetailInterestMessage:
                        {
                            this.OnRetailInterestMessage(in payload.RetailInterestMessage, in message.MessageHeader, message.SequenceNumber);
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
    void OnPacketHeader(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.PacketHeader.Layout value)
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
    void OnSystemEventMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.SystemEventMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnStockDirectoryMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockDirectoryMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnStockTradingActionMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockTradingActionMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnRegShoShortSalePriceTestRestrictedIndicatorMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnMarketParticipantPositionMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MarketParticipantPositionMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnMwcbDeclineLevelMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbDeclineLevelMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnMwcbStatusMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbStatusMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber)
    {
        this.Parsed++;
        this.OnMwcbStatusMessageCalls++;
        this.Observe(nameof(OnMwcbStatusMessageCalls));
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
    void OnLuldAuctionCollarMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.LuldAuctionCollarMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnOperationalHaltMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OperationalHaltMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnAddOrderNoMpidAttributionMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderNoMpidAttributionMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnAddOrderMpidAttributionMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderMpidAttributionMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber)
    {
        this.Parsed++;
        this.OnAddOrderMpidAttributionMessageCalls++;
        this.Observe(nameof(OnAddOrderMpidAttributionMessageCalls));
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
    void OnOrderExecutedMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnOrderExecutedWithPriceMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedWithPriceMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnOrderCancelMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderCancelMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnOrderDeleteMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderDeleteMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnOrderReplaceMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderReplaceMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnNonCrossTradeMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NonCrossTradeMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnCrossTradeMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.CrossTradeMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnBrokenTradeMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.BrokenTradeMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnNetOrderImbalanceIndicatorMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NetOrderImbalanceIndicatorMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnRetailInterestMessage(in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RetailInterestMessage.Layout value, in global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageHeader.Layout header, ulong sequenceNumber)
    {
        this.Parsed++;
        this.OnRetailInterestMessageCalls++;
        this.Observe(nameof(OnRetailInterestMessageCalls));
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
    void OnPacket(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Packet.Reader value)
    {
        this.Parsed++;
        this.OnPacketCalls++;
        this.Observe(nameof(OnPacketCalls));
    }
}
