using Nasdaq.NsmEquities.Qbbo.Itch.v2_1;
using Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Iterator;
using Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Test;
using static Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Test.Dump;

const int PcapArgument = 0;
const int FirstOption = 1;
const int None = 0;
const int OneFeed = 1;
const int Success = 0;
const int Failure = 1;
const int UsageFailure = 2;
const string FeedOption = "--feed";
const string StrictOption = "--strict";
const string Usage = $"usage: Nasdaq.NsmEquities.Qbbo.Itch.v2.1.Test <pcap> [{FeedOption} <name>] [{StrictOption}]";
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
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.IpoQuotingPeriodUpdateMessage.Layout>() != global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.IpoQuotingPeriodUpdateMessage.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.IpoQuotingPeriodUpdateMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.IpoQuotingPeriodUpdateMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.IpoQuotingPeriodUpdateMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.IpoQuotingPeriodUpdateMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.IpoQuotingPeriodUpdateMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.IpoQuotingPeriodUpdateMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.IpoQuotingPeriodUpdateMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.IpoQuotingPeriodUpdateMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.IpoQuotingPeriodUpdateMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.IpoQuotingPeriodUpdateMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.IpoQuotingPeriodUpdateMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.IpoQuotingPeriodUpdateMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.IpoQuotingPeriodUpdateMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.IpoQuotingPeriodUpdateMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.IpoQuotingPeriodUpdateMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.IpoQuotingPeriodUpdateMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PriceImprovementMessage.Layout>() != global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PriceImprovementMessage.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PriceImprovementMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PriceImprovementMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PriceImprovementMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PriceImprovementMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PriceImprovementMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PriceImprovementMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PriceImprovementMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PriceImprovementMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PriceImprovementMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PriceImprovementMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PriceImprovementMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PriceImprovementMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PriceImprovementMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PriceImprovementMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PriceImprovementMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PriceImprovementMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.BboQuotationMessage.Layout>() != global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.BboQuotationMessage.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.BboQuotationMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.BboQuotationMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.BboQuotationMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.BboQuotationMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.BboQuotationMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.BboQuotationMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.BboQuotationMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.BboQuotationMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.BboQuotationMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.BboQuotationMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.BboQuotationMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.BboQuotationMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.BboQuotationMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.BboQuotationMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.BboQuotationMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.BboQuotationMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.OperationalHaltMessage.Layout>() != global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.OperationalHaltMessage.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.OperationalHaltMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.OperationalHaltMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.OperationalHaltMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.OperationalHaltMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.OperationalHaltMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.OperationalHaltMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.OperationalHaltMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.OperationalHaltMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.OperationalHaltMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.OperationalHaltMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.OperationalHaltMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.OperationalHaltMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.OperationalHaltMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.OperationalHaltMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.OperationalHaltMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.OperationalHaltMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbBreachMessage.Layout>() != global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbBreachMessage.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbBreachMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbBreachMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbBreachMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbBreachMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbBreachMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbBreachMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbBreachMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbBreachMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbBreachMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbBreachMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbBreachMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbBreachMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbBreachMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbBreachMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbBreachMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbBreachMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbDeclineLevelMessage.Layout>() != global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbDeclineLevelMessage.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbDeclineLevelMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbDeclineLevelMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbDeclineLevelMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbDeclineLevelMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbDeclineLevelMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbDeclineLevelMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbDeclineLevelMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbDeclineLevelMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbDeclineLevelMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbDeclineLevelMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbDeclineLevelMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbDeclineLevelMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbDeclineLevelMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbDeclineLevelMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbDeclineLevelMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbDeclineLevelMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.RegShoRestrictionMessage.Layout>() != global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.RegShoRestrictionMessage.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.RegShoRestrictionMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.RegShoRestrictionMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.RegShoRestrictionMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.RegShoRestrictionMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.RegShoRestrictionMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.RegShoRestrictionMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.RegShoRestrictionMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.RegShoRestrictionMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.RegShoRestrictionMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.RegShoRestrictionMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.RegShoRestrictionMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.RegShoRestrictionMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.RegShoRestrictionMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.RegShoRestrictionMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.RegShoRestrictionMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.RegShoRestrictionMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockTradingActionMessage.Layout>() != global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockTradingActionMessage.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockTradingActionMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockTradingActionMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockTradingActionMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockTradingActionMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockTradingActionMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockTradingActionMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockTradingActionMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockTradingActionMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockTradingActionMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockTradingActionMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockTradingActionMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockTradingActionMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockTradingActionMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockTradingActionMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockTradingActionMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockTradingActionMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockDirectoryMessage.Layout>() != global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockDirectoryMessage.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockDirectoryMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockDirectoryMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockDirectoryMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockDirectoryMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockDirectoryMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockDirectoryMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockDirectoryMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockDirectoryMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockDirectoryMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockDirectoryMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockDirectoryMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockDirectoryMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockDirectoryMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockDirectoryMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockDirectoryMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockDirectoryMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.SystemEventMessage.Layout>() != global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.SystemEventMessage.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.SystemEventMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.SystemEventMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.SystemEventMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.SystemEventMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.SystemEventMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.SystemEventMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.SystemEventMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.SystemEventMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.SystemEventMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.SystemEventMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.SystemEventMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.SystemEventMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.SystemEventMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.SystemEventMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.SystemEventMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.SystemEventMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout>() != global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout.Read(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout.Read(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PacketHeader.Layout>() != global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PacketHeader.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PacketHeader.Layout.Read(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PacketHeader.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PacketHeader.Layout.Read(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PacketHeader.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PacketHeader.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PacketHeader.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PacketHeader.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PacketHeader.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PacketHeader)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PacketHeader.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PacketHeader.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PacketHeader.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PacketHeader.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PacketHeader.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PacketHeader.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PacketHeader)} Layout does not clear exactly its bytes.");
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
if (walker.OnIpoQuotingPeriodUpdateMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnIpoQuotingPeriodUpdateMessageCalls)}: {walker.OnIpoQuotingPeriodUpdateMessageCalls}");
if (walker.OnPriceImprovementMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnPriceImprovementMessageCalls)}: {walker.OnPriceImprovementMessageCalls}");
if (walker.OnBboQuotationMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnBboQuotationMessageCalls)}: {walker.OnBboQuotationMessageCalls}");
if (walker.OnOperationalHaltMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnOperationalHaltMessageCalls)}: {walker.OnOperationalHaltMessageCalls}");
if (walker.OnMwcbBreachMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnMwcbBreachMessageCalls)}: {walker.OnMwcbBreachMessageCalls}");
if (walker.OnMwcbDeclineLevelMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnMwcbDeclineLevelMessageCalls)}: {walker.OnMwcbDeclineLevelMessageCalls}");
if (walker.OnRegShoRestrictionMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnRegShoRestrictionMessageCalls)}: {walker.OnRegShoRestrictionMessageCalls}");
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
    ///  The number of calls of the OnRegShoRestrictionMessage method.
    /// </summary>
    public long OnRegShoRestrictionMessageCalls;
    /// <summary>
    ///  The number of calls of the OnMwcbDeclineLevelMessage method.
    /// </summary>
    public long OnMwcbDeclineLevelMessageCalls;
    /// <summary>
    ///  The number of calls of the OnMwcbBreachMessage method.
    /// </summary>
    public long OnMwcbBreachMessageCalls;
    /// <summary>
    ///  The number of calls of the OnOperationalHaltMessage method.
    /// </summary>
    public long OnOperationalHaltMessageCalls;
    /// <summary>
    ///  The number of calls of the OnBboQuotationMessage method.
    /// </summary>
    public long OnBboQuotationMessageCalls;
    /// <summary>
    ///  The number of calls of the OnPriceImprovementMessage method.
    /// </summary>
    public long OnPriceImprovementMessageCalls;
    /// <summary>
    ///  The number of calls of the OnIpoQuotingPeriodUpdateMessage method.
    /// </summary>
    public long OnIpoQuotingPeriodUpdateMessageCalls;
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
    public void VisitPacket(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Packet.Reader packet)
    {
        this.BeginPacketSequence();
        this.SeedPacketSequence(ulong.CreateSaturating(packet.PacketHeader.SequenceNumber.Value));
        this.OnPacketHeader(in packet.PacketHeader);
        var messages = packet.Messages;
        switch (messages.Case)
        {
            case global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Messages.Case.Heartbeat:
            {
                this.OnHeartbeat();
                break;
            }
            case global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Messages.Case.EndOfSession:
            {
                this.OnEndOfSession();
                break;
            }
            case global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Messages.Case.Message:
            {
                var messageEntries = messages.Message;
                var messageRead = 0L;
                while (messageEntries.MoveNext())
                {
                    var message = messageEntries.Current;
                    {
                        ulong? expected = null;
                        expected = this.NextPacketSequence();
                        this.CheckSequence((ulong?)message.SequenceNumber, expected, nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Message));
                    }
                    this.numberedDepth++;
                    var payload = message.Payload;
                    switch (payload.Case)
                    {
                        case global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.SystemEventMessage:
                        {
                            this.OnSystemEventMessage(in payload.SystemEventMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.StockDirectoryMessage:
                        {
                            this.OnStockDirectoryMessage(in payload.StockDirectoryMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.StockTradingActionMessage:
                        {
                            this.OnStockTradingActionMessage(in payload.StockTradingActionMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.RegShoRestrictionMessage:
                        {
                            this.OnRegShoRestrictionMessage(in payload.RegShoRestrictionMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.MwcbDeclineLevelMessage:
                        {
                            this.OnMwcbDeclineLevelMessage(in payload.MwcbDeclineLevelMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.MwcbBreachMessage:
                        {
                            this.OnMwcbBreachMessage(in payload.MwcbBreachMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.OperationalHaltMessage:
                        {
                            this.OnOperationalHaltMessage(in payload.OperationalHaltMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.BboQuotationMessage:
                        {
                            this.OnBboQuotationMessage(in payload.BboQuotationMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.PriceImprovementMessage:
                        {
                            this.OnPriceImprovementMessage(in payload.PriceImprovementMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.IpoQuotingPeriodUpdateMessage:
                        {
                            this.OnIpoQuotingPeriodUpdateMessage(in payload.IpoQuotingPeriodUpdateMessage, in message.MessageHeader, message.SequenceNumber);
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
    void OnPacketHeader(in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PacketHeader.Layout value)
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
    void OnSystemEventMessage(in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.SystemEventMessage.Layout value, in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnStockDirectoryMessage(in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockDirectoryMessage.Layout value, in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnStockTradingActionMessage(in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockTradingActionMessage.Layout value, in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnRegShoRestrictionMessage(in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.RegShoRestrictionMessage.Layout value, in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout header, ulong sequenceNumber)
    {
        this.Parsed++;
        this.OnRegShoRestrictionMessageCalls++;
        this.Observe(nameof(OnRegShoRestrictionMessageCalls));
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
    void OnMwcbDeclineLevelMessage(in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbDeclineLevelMessage.Layout value, in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnMwcbBreachMessage(in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbBreachMessage.Layout value, in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout header, ulong sequenceNumber)
    {
        this.Parsed++;
        this.OnMwcbBreachMessageCalls++;
        this.Observe(nameof(OnMwcbBreachMessageCalls));
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
    void OnOperationalHaltMessage(in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.OperationalHaltMessage.Layout value, in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnBboQuotationMessage(in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.BboQuotationMessage.Layout value, in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout header, ulong sequenceNumber)
    {
        this.Parsed++;
        this.OnBboQuotationMessageCalls++;
        this.Observe(nameof(OnBboQuotationMessageCalls));
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
    void OnPriceImprovementMessage(in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PriceImprovementMessage.Layout value, in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout header, ulong sequenceNumber)
    {
        this.Parsed++;
        this.OnPriceImprovementMessageCalls++;
        this.Observe(nameof(OnPriceImprovementMessageCalls));
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
    void OnIpoQuotingPeriodUpdateMessage(in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.IpoQuotingPeriodUpdateMessage.Layout value, in global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout header, ulong sequenceNumber)
    {
        this.Parsed++;
        this.OnIpoQuotingPeriodUpdateMessageCalls++;
        this.Observe(nameof(OnIpoQuotingPeriodUpdateMessageCalls));
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
    void OnPacket(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Packet.Reader value)
    {
        this.Parsed++;
        this.OnPacketCalls++;
        this.Observe(nameof(OnPacketCalls));
    }
}
