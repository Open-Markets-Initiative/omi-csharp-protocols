using Iex.IexEquities.DeepPlus.IexTp.v1_05;
using Iex.IexEquities.DeepPlus.IexTp.v1_05.Iterator;
using Iex.IexEquities.DeepPlus.IexTp.v1_05.Test;
using static Iex.IexEquities.DeepPlus.IexTp.v1_05.Test.Dump;

const int PcapArgument = 0;
const int FirstOption = 1;
const int None = 0;
const int OneFeed = 1;
const int Success = 0;
const int Failure = 1;
const int UsageFailure = 2;
const string FeedOption = "--feed";
const string StrictOption = "--strict";
const string Usage = $"usage: Iex.IexEquities.DeepPlus.IexTp.v1.05.Test <pcap> [{FeedOption} <name>] [{StrictOption}]";
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
const ushort messageProtocolIdStaticValue = 32773;
const uint channelIdStaticValue = 1u;
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ClearBookMessage.Layout>() != global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ClearBookMessage.Layout.Size || Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ClearBookMessage.Layout.Read(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ClearBookMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ClearBookMessage.Layout.Read(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ClearBookMessage.Layout.Size - 1]); }) || Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ClearBookMessage.Layout.Initialize(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ClearBookMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ClearBookMessage.Layout.Initialize(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ClearBookMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ClearBookMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ClearBookMessage.Layout.Size];
    var filled = new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ClearBookMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ClearBookMessage.Layout.Initialize(zeroed);
    global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ClearBookMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ClearBookMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ClearBookMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ClearBookMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeBreakMessage.Layout>() != global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeBreakMessage.Layout.LayoutSize || Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeBreakMessage.Layout.Read(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeBreakMessage.Layout.LayoutSize]); }) || !Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeBreakMessage.Layout.Read(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeBreakMessage.Layout.LayoutSize - 1]); }) || Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeBreakMessage.Layout.Initialize(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeBreakMessage.Layout.LayoutSize]); }) || !Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeBreakMessage.Layout.Initialize(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeBreakMessage.Layout.LayoutSize - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeBreakMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeBreakMessage.Layout.LayoutSize];
    var filled = new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeBreakMessage.Layout.LayoutSize + Following];
    global::System.Array.Fill(filled, Filler);
    global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeBreakMessage.Layout.Initialize(zeroed);
    global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeBreakMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeBreakMessage.Layout.LayoutSize).SequenceEqual(zeroed) || filled.AsSpan(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeBreakMessage.Layout.LayoutSize).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeBreakMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeMessage.Layout>() != global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeMessage.Layout.LayoutSize || Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeMessage.Layout.Read(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeMessage.Layout.LayoutSize]); }) || !Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeMessage.Layout.Read(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeMessage.Layout.LayoutSize - 1]); }) || Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeMessage.Layout.Initialize(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeMessage.Layout.LayoutSize]); }) || !Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeMessage.Layout.Initialize(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeMessage.Layout.LayoutSize - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeMessage.Layout.LayoutSize];
    var filled = new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeMessage.Layout.LayoutSize + Following];
    global::System.Array.Fill(filled, Filler);
    global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeMessage.Layout.Initialize(zeroed);
    global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeMessage.Layout.LayoutSize).SequenceEqual(zeroed) || filled.AsSpan(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeMessage.Layout.LayoutSize).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderExecutedMessage.Layout>() != global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderExecutedMessage.Layout.LayoutSize || Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderExecutedMessage.Layout.Read(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderExecutedMessage.Layout.LayoutSize]); }) || !Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderExecutedMessage.Layout.Read(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderExecutedMessage.Layout.LayoutSize - 1]); }) || Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderExecutedMessage.Layout.Initialize(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderExecutedMessage.Layout.LayoutSize]); }) || !Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderExecutedMessage.Layout.Initialize(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderExecutedMessage.Layout.LayoutSize - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderExecutedMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderExecutedMessage.Layout.LayoutSize];
    var filled = new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderExecutedMessage.Layout.LayoutSize + Following];
    global::System.Array.Fill(filled, Filler);
    global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderExecutedMessage.Layout.Initialize(zeroed);
    global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderExecutedMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderExecutedMessage.Layout.LayoutSize).SequenceEqual(zeroed) || filled.AsSpan(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderExecutedMessage.Layout.LayoutSize).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderExecutedMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderDeleteMessage.Layout>() != global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderDeleteMessage.Layout.Size || Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderDeleteMessage.Layout.Read(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderDeleteMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderDeleteMessage.Layout.Read(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderDeleteMessage.Layout.Size - 1]); }) || Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderDeleteMessage.Layout.Initialize(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderDeleteMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderDeleteMessage.Layout.Initialize(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderDeleteMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderDeleteMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderDeleteMessage.Layout.Size];
    var filled = new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderDeleteMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderDeleteMessage.Layout.Initialize(zeroed);
    global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderDeleteMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderDeleteMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderDeleteMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderDeleteMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderModifyMessage.Layout>() != global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderModifyMessage.Layout.LayoutSize || Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderModifyMessage.Layout.Read(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderModifyMessage.Layout.LayoutSize]); }) || !Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderModifyMessage.Layout.Read(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderModifyMessage.Layout.LayoutSize - 1]); }) || Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderModifyMessage.Layout.Initialize(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderModifyMessage.Layout.LayoutSize]); }) || !Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderModifyMessage.Layout.Initialize(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderModifyMessage.Layout.LayoutSize - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderModifyMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderModifyMessage.Layout.LayoutSize];
    var filled = new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderModifyMessage.Layout.LayoutSize + Following];
    global::System.Array.Fill(filled, Filler);
    global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderModifyMessage.Layout.Initialize(zeroed);
    global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderModifyMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderModifyMessage.Layout.LayoutSize).SequenceEqual(zeroed) || filled.AsSpan(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderModifyMessage.Layout.LayoutSize).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderModifyMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Iex.IexEquities.DeepPlus.IexTp.v1_05.AddOrderMessage.Layout>() != global::Iex.IexEquities.DeepPlus.IexTp.v1_05.AddOrderMessage.Layout.LayoutSize || Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.AddOrderMessage.Layout.Read(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.AddOrderMessage.Layout.LayoutSize]); }) || !Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.AddOrderMessage.Layout.Read(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.AddOrderMessage.Layout.LayoutSize - 1]); }) || Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.AddOrderMessage.Layout.Initialize(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.AddOrderMessage.Layout.LayoutSize]); }) || !Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.AddOrderMessage.Layout.Initialize(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.AddOrderMessage.Layout.LayoutSize - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.AddOrderMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.AddOrderMessage.Layout.LayoutSize];
    var filled = new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.AddOrderMessage.Layout.LayoutSize + Following];
    global::System.Array.Fill(filled, Filler);
    global::Iex.IexEquities.DeepPlus.IexTp.v1_05.AddOrderMessage.Layout.Initialize(zeroed);
    global::Iex.IexEquities.DeepPlus.IexTp.v1_05.AddOrderMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Iex.IexEquities.DeepPlus.IexTp.v1_05.AddOrderMessage.Layout.LayoutSize).SequenceEqual(zeroed) || filled.AsSpan(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.AddOrderMessage.Layout.LayoutSize).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.AddOrderMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityEventMessage.Layout>() != global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityEventMessage.Layout.Size || Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityEventMessage.Layout.Read(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityEventMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityEventMessage.Layout.Read(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityEventMessage.Layout.Size - 1]); }) || Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityEventMessage.Layout.Initialize(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityEventMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityEventMessage.Layout.Initialize(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityEventMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityEventMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityEventMessage.Layout.Size];
    var filled = new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityEventMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityEventMessage.Layout.Initialize(zeroed);
    global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityEventMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityEventMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityEventMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityEventMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ShortSalePriceTestStatusMessage.Layout>() != global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ShortSalePriceTestStatusMessage.Layout.Size || Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ShortSalePriceTestStatusMessage.Layout.Read(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ShortSalePriceTestStatusMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ShortSalePriceTestStatusMessage.Layout.Read(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ShortSalePriceTestStatusMessage.Layout.Size - 1]); }) || Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ShortSalePriceTestStatusMessage.Layout.Initialize(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ShortSalePriceTestStatusMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ShortSalePriceTestStatusMessage.Layout.Initialize(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ShortSalePriceTestStatusMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ShortSalePriceTestStatusMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ShortSalePriceTestStatusMessage.Layout.Size];
    var filled = new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ShortSalePriceTestStatusMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ShortSalePriceTestStatusMessage.Layout.Initialize(zeroed);
    global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ShortSalePriceTestStatusMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ShortSalePriceTestStatusMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ShortSalePriceTestStatusMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ShortSalePriceTestStatusMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OperationalHaltStatusMessage.Layout>() != global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OperationalHaltStatusMessage.Layout.Size || Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OperationalHaltStatusMessage.Layout.Read(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OperationalHaltStatusMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OperationalHaltStatusMessage.Layout.Read(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OperationalHaltStatusMessage.Layout.Size - 1]); }) || Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OperationalHaltStatusMessage.Layout.Initialize(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OperationalHaltStatusMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OperationalHaltStatusMessage.Layout.Initialize(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OperationalHaltStatusMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OperationalHaltStatusMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OperationalHaltStatusMessage.Layout.Size];
    var filled = new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OperationalHaltStatusMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OperationalHaltStatusMessage.Layout.Initialize(zeroed);
    global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OperationalHaltStatusMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OperationalHaltStatusMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OperationalHaltStatusMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OperationalHaltStatusMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Iex.IexEquities.DeepPlus.IexTp.v1_05.RetailLiquidityIndicatorMessage.Layout>() != global::Iex.IexEquities.DeepPlus.IexTp.v1_05.RetailLiquidityIndicatorMessage.Layout.Size || Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.RetailLiquidityIndicatorMessage.Layout.Read(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.RetailLiquidityIndicatorMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.RetailLiquidityIndicatorMessage.Layout.Read(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.RetailLiquidityIndicatorMessage.Layout.Size - 1]); }) || Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.RetailLiquidityIndicatorMessage.Layout.Initialize(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.RetailLiquidityIndicatorMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.RetailLiquidityIndicatorMessage.Layout.Initialize(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.RetailLiquidityIndicatorMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.RetailLiquidityIndicatorMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.RetailLiquidityIndicatorMessage.Layout.Size];
    var filled = new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.RetailLiquidityIndicatorMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Iex.IexEquities.DeepPlus.IexTp.v1_05.RetailLiquidityIndicatorMessage.Layout.Initialize(zeroed);
    global::Iex.IexEquities.DeepPlus.IexTp.v1_05.RetailLiquidityIndicatorMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Iex.IexEquities.DeepPlus.IexTp.v1_05.RetailLiquidityIndicatorMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.RetailLiquidityIndicatorMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.RetailLiquidityIndicatorMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradingStatusMessage.Layout>() != global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradingStatusMessage.Layout.Size || Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradingStatusMessage.Layout.Read(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradingStatusMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradingStatusMessage.Layout.Read(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradingStatusMessage.Layout.Size - 1]); }) || Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradingStatusMessage.Layout.Initialize(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradingStatusMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradingStatusMessage.Layout.Initialize(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradingStatusMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradingStatusMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradingStatusMessage.Layout.Size];
    var filled = new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradingStatusMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradingStatusMessage.Layout.Initialize(zeroed);
    global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradingStatusMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradingStatusMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradingStatusMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradingStatusMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityDirectoryMessage.Layout>() != global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityDirectoryMessage.Layout.Size || Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityDirectoryMessage.Layout.Read(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityDirectoryMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityDirectoryMessage.Layout.Read(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityDirectoryMessage.Layout.Size - 1]); }) || Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityDirectoryMessage.Layout.Initialize(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityDirectoryMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityDirectoryMessage.Layout.Initialize(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityDirectoryMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityDirectoryMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityDirectoryMessage.Layout.Size];
    var filled = new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityDirectoryMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityDirectoryMessage.Layout.Initialize(zeroed);
    global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityDirectoryMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityDirectoryMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityDirectoryMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityDirectoryMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SystemEventMessage.Layout>() != global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SystemEventMessage.Layout.Size || Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SystemEventMessage.Layout.Read(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SystemEventMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SystemEventMessage.Layout.Read(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SystemEventMessage.Layout.Size - 1]); }) || Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SystemEventMessage.Layout.Initialize(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SystemEventMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SystemEventMessage.Layout.Initialize(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SystemEventMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SystemEventMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SystemEventMessage.Layout.Size];
    var filled = new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SystemEventMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SystemEventMessage.Layout.Initialize(zeroed);
    global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SystemEventMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SystemEventMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SystemEventMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SystemEventMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout>() != global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout.Size || Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout.Read(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout.Read(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout.Size - 1]); }) || Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout.Initialize(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout.Initialize(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout.Size];
    var filled = new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout.Initialize(zeroed);
    global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Iex.IexEquities.DeepPlus.IexTp.v1_05.IextpHeader.Layout>() != global::Iex.IexEquities.DeepPlus.IexTp.v1_05.IextpHeader.Layout.Size || Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.IextpHeader.Layout.Read(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.IextpHeader.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.IextpHeader.Layout.Read(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.IextpHeader.Layout.Size - 1]); }) || Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.IextpHeader.Layout.Initialize(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.IextpHeader.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.DeepPlus.IexTp.v1_05.IextpHeader.Layout.Initialize(new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.IextpHeader.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.IextpHeader)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.IextpHeader.Layout.Size];
    var filled = new byte[global::Iex.IexEquities.DeepPlus.IexTp.v1_05.IextpHeader.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Iex.IexEquities.DeepPlus.IexTp.v1_05.IextpHeader.Layout.Initialize(zeroed);
    global::Iex.IexEquities.DeepPlus.IexTp.v1_05.IextpHeader.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Iex.IexEquities.DeepPlus.IexTp.v1_05.IextpHeader.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.IextpHeader.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.IextpHeader)} Layout does not clear exactly its bytes.");
        return Failure;
    }
    if (global::Iex.IexEquities.DeepPlus.IexTp.v1_05.IextpHeader.Layout.Read(filled).Version.Value != versionStaticValue)
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.IextpHeader)} Layout does not give {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.IextpHeader.Layout.Version)} its static value.");
        return Failure;
    }
    if (global::Iex.IexEquities.DeepPlus.IexTp.v1_05.IextpHeader.Layout.Read(filled).MessageProtocolId.Value != messageProtocolIdStaticValue)
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.IextpHeader)} Layout does not give {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.IextpHeader.Layout.MessageProtocolId)} its static value.");
        return Failure;
    }
    if (global::Iex.IexEquities.DeepPlus.IexTp.v1_05.IextpHeader.Layout.Read(filled).ChannelId.Value != channelIdStaticValue)
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.IextpHeader)} Layout does not give {nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.IextpHeader.Layout.ChannelId)} its static value.");
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
if (walker.OnClearBookMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnClearBookMessageCalls)}: {walker.OnClearBookMessageCalls}");
if (walker.OnTradeBreakMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnTradeBreakMessageCalls)}: {walker.OnTradeBreakMessageCalls}");
if (walker.OnTradeMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnTradeMessageCalls)}: {walker.OnTradeMessageCalls}");
if (walker.OnOrderExecutedMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnOrderExecutedMessageCalls)}: {walker.OnOrderExecutedMessageCalls}");
if (walker.OnOrderDeleteMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnOrderDeleteMessageCalls)}: {walker.OnOrderDeleteMessageCalls}");
if (walker.OnOrderModifyMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnOrderModifyMessageCalls)}: {walker.OnOrderModifyMessageCalls}");
if (walker.OnAddOrderMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnAddOrderMessageCalls)}: {walker.OnAddOrderMessageCalls}");
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
    ///  The number of calls of the OnAddOrderMessage method.
    /// </summary>
    public long OnAddOrderMessageCalls;
    /// <summary>
    ///  The number of calls of the OnOrderModifyMessage method.
    /// </summary>
    public long OnOrderModifyMessageCalls;
    /// <summary>
    ///  The number of calls of the OnOrderDeleteMessage method.
    /// </summary>
    public long OnOrderDeleteMessageCalls;
    /// <summary>
    ///  The number of calls of the OnOrderExecutedMessage method.
    /// </summary>
    public long OnOrderExecutedMessageCalls;
    /// <summary>
    ///  The number of calls of the OnTradeMessage method.
    /// </summary>
    public long OnTradeMessageCalls;
    /// <summary>
    ///  The number of calls of the OnTradeBreakMessage method.
    /// </summary>
    public long OnTradeBreakMessageCalls;
    /// <summary>
    ///  The number of calls of the OnClearBookMessage method.
    /// </summary>
    public long OnClearBookMessageCalls;
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
    public void VisitPacket(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Packet.Reader packet)
    {
        this.BeginPacketSequence();
        this.SeedPacketSequence(ulong.CreateSaturating(packet.IextpHeader.FirstMessageSequenceNumber.Value));
        this.OnIextpHeader(in packet.IextpHeader);
        var messages = packet.Messages;
        switch (messages.Case)
        {
            case global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Messages.Case.Message:
            {
                var messageEntries = messages.Message;
                var messageRead = 0L;
                while (messageEntries.MoveNext())
                {
                    var message = messageEntries.Current;
                    {
                        ulong? expected = null;
                        expected = this.NextPacketSequence();
                        this.CheckSequence((ulong?)message.SequenceNumber, expected, nameof(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Message));
                    }
                    this.numberedDepth++;
                    var messageData = message.MessageData;
                    switch (messageData.Case)
                    {
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageData.Case.SystemEventMessage:
                        {
                            this.OnSystemEventMessage(in messageData.SystemEventMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageData.Case.SecurityDirectoryMessage:
                        {
                            this.OnSecurityDirectoryMessage(in messageData.SecurityDirectoryMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageData.Case.TradingStatusMessage:
                        {
                            this.OnTradingStatusMessage(in messageData.TradingStatusMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageData.Case.RetailLiquidityIndicatorMessage:
                        {
                            this.OnRetailLiquidityIndicatorMessage(in messageData.RetailLiquidityIndicatorMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageData.Case.OperationalHaltStatusMessage:
                        {
                            this.OnOperationalHaltStatusMessage(in messageData.OperationalHaltStatusMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageData.Case.ShortSalePriceTestStatusMessage:
                        {
                            this.OnShortSalePriceTestStatusMessage(in messageData.ShortSalePriceTestStatusMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageData.Case.SecurityEventMessage:
                        {
                            this.OnSecurityEventMessage(in messageData.SecurityEventMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageData.Case.AddOrderMessage:
                        {
                            this.OnAddOrderMessage(in messageData.AddOrderMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageData.Case.OrderModifyMessage:
                        {
                            this.OnOrderModifyMessage(in messageData.OrderModifyMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageData.Case.OrderDeleteMessage:
                        {
                            this.OnOrderDeleteMessage(in messageData.OrderDeleteMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageData.Case.OrderExecutedMessage:
                        {
                            this.OnOrderExecutedMessage(in messageData.OrderExecutedMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageData.Case.TradeMessage:
                        {
                            this.OnTradeMessage(in messageData.TradeMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageData.Case.TradeBreakMessage:
                        {
                            this.OnTradeBreakMessage(in messageData.TradeBreakMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageData.Case.ClearBookMessage:
                        {
                            this.OnClearBookMessage(in messageData.ClearBookMessage, in message.MessageHeader, message.SequenceNumber);
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
    void OnIextpHeader(in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.IextpHeader.Layout value)
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
    void OnSystemEventMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SystemEventMessage.Layout value, in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnSecurityDirectoryMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityDirectoryMessage.Layout value, in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnTradingStatusMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradingStatusMessage.Layout value, in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnRetailLiquidityIndicatorMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.RetailLiquidityIndicatorMessage.Layout value, in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnOperationalHaltStatusMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OperationalHaltStatusMessage.Layout value, in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnShortSalePriceTestStatusMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ShortSalePriceTestStatusMessage.Layout value, in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnSecurityEventMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SecurityEventMessage.Layout value, in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnAddOrderMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.AddOrderMessage.Layout value, in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout header, ulong sequenceNumber)
    {
        this.Parsed++;
        this.OnAddOrderMessageCalls++;
        this.Observe(nameof(OnAddOrderMessageCalls));
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
    void OnOrderModifyMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderModifyMessage.Layout value, in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout header, ulong sequenceNumber)
    {
        this.Parsed++;
        this.OnOrderModifyMessageCalls++;
        this.Observe(nameof(OnOrderModifyMessageCalls));
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
    void OnOrderDeleteMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderDeleteMessage.Layout value, in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnOrderExecutedMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderExecutedMessage.Layout value, in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnTradeMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeMessage.Layout value, in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout header, ulong sequenceNumber)
    {
        this.Parsed++;
        this.OnTradeMessageCalls++;
        this.Observe(nameof(OnTradeMessageCalls));
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
    void OnTradeBreakMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.TradeBreakMessage.Layout value, in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnClearBookMessage(in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.ClearBookMessage.Layout value, in global::Iex.IexEquities.DeepPlus.IexTp.v1_05.MessageHeader.Layout header, ulong sequenceNumber)
    {
        this.Parsed++;
        this.OnClearBookMessageCalls++;
        this.Observe(nameof(OnClearBookMessageCalls));
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
    void OnPacket(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Packet.Reader value)
    {
        this.Parsed++;
        this.OnPacketCalls++;
        this.Observe(nameof(OnPacketCalls));
    }
}
