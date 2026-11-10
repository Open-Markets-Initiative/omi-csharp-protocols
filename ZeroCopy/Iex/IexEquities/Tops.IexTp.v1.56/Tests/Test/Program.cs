using Iex.IexEquities.Tops.IexTp.v1_56;
using Iex.IexEquities.Tops.IexTp.v1_56.Iterator;
using Iex.IexEquities.Tops.IexTp.v1_56.Test;
using static Iex.IexEquities.Tops.IexTp.v1_56.Test.Dump;

const int PcapArgument = 0;
const int FirstOption = 1;
const int None = 0;
const int OneFeed = 1;
const int Success = 0;
const int Failure = 1;
const int UsageFailure = 2;
const string FeedOption = "--feed";
const string StrictOption = "--strict";
const string Usage = $"usage: Iex.IexEquities.Tops.IexTp.v1.56.Test <pcap> [{FeedOption} <name>] [{StrictOption}]";
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
const ushort messageProtocolIdStaticValue = 32770;
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Iex.IexEquities.Tops.IexTp.v1_56.TradeBreakMessage.Layout>() != global::Iex.IexEquities.Tops.IexTp.v1_56.TradeBreakMessage.Layout.LayoutSize || Throws(() => { global::Iex.IexEquities.Tops.IexTp.v1_56.TradeBreakMessage.Layout.Read(new byte[global::Iex.IexEquities.Tops.IexTp.v1_56.TradeBreakMessage.Layout.LayoutSize]); }) || !Throws(() => { global::Iex.IexEquities.Tops.IexTp.v1_56.TradeBreakMessage.Layout.Read(new byte[global::Iex.IexEquities.Tops.IexTp.v1_56.TradeBreakMessage.Layout.LayoutSize - 1]); }) || Throws(() => { global::Iex.IexEquities.Tops.IexTp.v1_56.TradeBreakMessage.Layout.Initialize(new byte[global::Iex.IexEquities.Tops.IexTp.v1_56.TradeBreakMessage.Layout.LayoutSize]); }) || !Throws(() => { global::Iex.IexEquities.Tops.IexTp.v1_56.TradeBreakMessage.Layout.Initialize(new byte[global::Iex.IexEquities.Tops.IexTp.v1_56.TradeBreakMessage.Layout.LayoutSize - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Iex.IexEquities.Tops.IexTp.v1_56.TradeBreakMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Iex.IexEquities.Tops.IexTp.v1_56.TradeBreakMessage.Layout.LayoutSize];
    var filled = new byte[global::Iex.IexEquities.Tops.IexTp.v1_56.TradeBreakMessage.Layout.LayoutSize + Following];
    global::System.Array.Fill(filled, Filler);
    global::Iex.IexEquities.Tops.IexTp.v1_56.TradeBreakMessage.Layout.Initialize(zeroed);
    global::Iex.IexEquities.Tops.IexTp.v1_56.TradeBreakMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Iex.IexEquities.Tops.IexTp.v1_56.TradeBreakMessage.Layout.LayoutSize).SequenceEqual(zeroed) || filled.AsSpan(global::Iex.IexEquities.Tops.IexTp.v1_56.TradeBreakMessage.Layout.LayoutSize).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.Tops.IexTp.v1_56.TradeBreakMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Iex.IexEquities.Tops.IexTp.v1_56.TradeReportMessage.Layout>() != global::Iex.IexEquities.Tops.IexTp.v1_56.TradeReportMessage.Layout.LayoutSize || Throws(() => { global::Iex.IexEquities.Tops.IexTp.v1_56.TradeReportMessage.Layout.Read(new byte[global::Iex.IexEquities.Tops.IexTp.v1_56.TradeReportMessage.Layout.LayoutSize]); }) || !Throws(() => { global::Iex.IexEquities.Tops.IexTp.v1_56.TradeReportMessage.Layout.Read(new byte[global::Iex.IexEquities.Tops.IexTp.v1_56.TradeReportMessage.Layout.LayoutSize - 1]); }) || Throws(() => { global::Iex.IexEquities.Tops.IexTp.v1_56.TradeReportMessage.Layout.Initialize(new byte[global::Iex.IexEquities.Tops.IexTp.v1_56.TradeReportMessage.Layout.LayoutSize]); }) || !Throws(() => { global::Iex.IexEquities.Tops.IexTp.v1_56.TradeReportMessage.Layout.Initialize(new byte[global::Iex.IexEquities.Tops.IexTp.v1_56.TradeReportMessage.Layout.LayoutSize - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Iex.IexEquities.Tops.IexTp.v1_56.TradeReportMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Iex.IexEquities.Tops.IexTp.v1_56.TradeReportMessage.Layout.LayoutSize];
    var filled = new byte[global::Iex.IexEquities.Tops.IexTp.v1_56.TradeReportMessage.Layout.LayoutSize + Following];
    global::System.Array.Fill(filled, Filler);
    global::Iex.IexEquities.Tops.IexTp.v1_56.TradeReportMessage.Layout.Initialize(zeroed);
    global::Iex.IexEquities.Tops.IexTp.v1_56.TradeReportMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Iex.IexEquities.Tops.IexTp.v1_56.TradeReportMessage.Layout.LayoutSize).SequenceEqual(zeroed) || filled.AsSpan(global::Iex.IexEquities.Tops.IexTp.v1_56.TradeReportMessage.Layout.LayoutSize).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.Tops.IexTp.v1_56.TradeReportMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Iex.IexEquities.Tops.IexTp.v1_56.QuoteUpdateMessage.Layout>() != global::Iex.IexEquities.Tops.IexTp.v1_56.QuoteUpdateMessage.Layout.Size || Throws(() => { global::Iex.IexEquities.Tops.IexTp.v1_56.QuoteUpdateMessage.Layout.Read(new byte[global::Iex.IexEquities.Tops.IexTp.v1_56.QuoteUpdateMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.Tops.IexTp.v1_56.QuoteUpdateMessage.Layout.Read(new byte[global::Iex.IexEquities.Tops.IexTp.v1_56.QuoteUpdateMessage.Layout.Size - 1]); }) || Throws(() => { global::Iex.IexEquities.Tops.IexTp.v1_56.QuoteUpdateMessage.Layout.Initialize(new byte[global::Iex.IexEquities.Tops.IexTp.v1_56.QuoteUpdateMessage.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.Tops.IexTp.v1_56.QuoteUpdateMessage.Layout.Initialize(new byte[global::Iex.IexEquities.Tops.IexTp.v1_56.QuoteUpdateMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Iex.IexEquities.Tops.IexTp.v1_56.QuoteUpdateMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Iex.IexEquities.Tops.IexTp.v1_56.QuoteUpdateMessage.Layout.Size];
    var filled = new byte[global::Iex.IexEquities.Tops.IexTp.v1_56.QuoteUpdateMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Iex.IexEquities.Tops.IexTp.v1_56.QuoteUpdateMessage.Layout.Initialize(zeroed);
    global::Iex.IexEquities.Tops.IexTp.v1_56.QuoteUpdateMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Iex.IexEquities.Tops.IexTp.v1_56.QuoteUpdateMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Iex.IexEquities.Tops.IexTp.v1_56.QuoteUpdateMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.Tops.IexTp.v1_56.QuoteUpdateMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Iex.IexEquities.Tops.IexTp.v1_56.MessageHeader.Layout>() != global::Iex.IexEquities.Tops.IexTp.v1_56.MessageHeader.Layout.Size || Throws(() => { global::Iex.IexEquities.Tops.IexTp.v1_56.MessageHeader.Layout.Read(new byte[global::Iex.IexEquities.Tops.IexTp.v1_56.MessageHeader.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.Tops.IexTp.v1_56.MessageHeader.Layout.Read(new byte[global::Iex.IexEquities.Tops.IexTp.v1_56.MessageHeader.Layout.Size - 1]); }) || Throws(() => { global::Iex.IexEquities.Tops.IexTp.v1_56.MessageHeader.Layout.Initialize(new byte[global::Iex.IexEquities.Tops.IexTp.v1_56.MessageHeader.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.Tops.IexTp.v1_56.MessageHeader.Layout.Initialize(new byte[global::Iex.IexEquities.Tops.IexTp.v1_56.MessageHeader.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Iex.IexEquities.Tops.IexTp.v1_56.MessageHeader)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Iex.IexEquities.Tops.IexTp.v1_56.MessageHeader.Layout.Size];
    var filled = new byte[global::Iex.IexEquities.Tops.IexTp.v1_56.MessageHeader.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Iex.IexEquities.Tops.IexTp.v1_56.MessageHeader.Layout.Initialize(zeroed);
    global::Iex.IexEquities.Tops.IexTp.v1_56.MessageHeader.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Iex.IexEquities.Tops.IexTp.v1_56.MessageHeader.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Iex.IexEquities.Tops.IexTp.v1_56.MessageHeader.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.Tops.IexTp.v1_56.MessageHeader)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Iex.IexEquities.Tops.IexTp.v1_56.IextpHeader.Layout>() != global::Iex.IexEquities.Tops.IexTp.v1_56.IextpHeader.Layout.Size || Throws(() => { global::Iex.IexEquities.Tops.IexTp.v1_56.IextpHeader.Layout.Read(new byte[global::Iex.IexEquities.Tops.IexTp.v1_56.IextpHeader.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.Tops.IexTp.v1_56.IextpHeader.Layout.Read(new byte[global::Iex.IexEquities.Tops.IexTp.v1_56.IextpHeader.Layout.Size - 1]); }) || Throws(() => { global::Iex.IexEquities.Tops.IexTp.v1_56.IextpHeader.Layout.Initialize(new byte[global::Iex.IexEquities.Tops.IexTp.v1_56.IextpHeader.Layout.Size]); }) || !Throws(() => { global::Iex.IexEquities.Tops.IexTp.v1_56.IextpHeader.Layout.Initialize(new byte[global::Iex.IexEquities.Tops.IexTp.v1_56.IextpHeader.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Iex.IexEquities.Tops.IexTp.v1_56.IextpHeader)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Iex.IexEquities.Tops.IexTp.v1_56.IextpHeader.Layout.Size];
    var filled = new byte[global::Iex.IexEquities.Tops.IexTp.v1_56.IextpHeader.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Iex.IexEquities.Tops.IexTp.v1_56.IextpHeader.Layout.Initialize(zeroed);
    global::Iex.IexEquities.Tops.IexTp.v1_56.IextpHeader.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Iex.IexEquities.Tops.IexTp.v1_56.IextpHeader.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Iex.IexEquities.Tops.IexTp.v1_56.IextpHeader.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.Tops.IexTp.v1_56.IextpHeader)} Layout does not clear exactly its bytes.");
        return Failure;
    }
    if (global::Iex.IexEquities.Tops.IexTp.v1_56.IextpHeader.Layout.Read(filled).Version.Value != versionStaticValue)
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.Tops.IexTp.v1_56.IextpHeader)} Layout does not give {nameof(global::Iex.IexEquities.Tops.IexTp.v1_56.IextpHeader.Layout.Version)} its static value.");
        return Failure;
    }
    if (global::Iex.IexEquities.Tops.IexTp.v1_56.IextpHeader.Layout.Read(filled).MessageProtocolId.Value != messageProtocolIdStaticValue)
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Iex.IexEquities.Tops.IexTp.v1_56.IextpHeader)} Layout does not give {nameof(global::Iex.IexEquities.Tops.IexTp.v1_56.IextpHeader.Layout.MessageProtocolId)} its static value.");
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
if (walker.OnTradeBreakMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnTradeBreakMessageCalls)}: {walker.OnTradeBreakMessageCalls}");
if (walker.OnTradeReportMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnTradeReportMessageCalls)}: {walker.OnTradeReportMessageCalls}");
if (walker.OnQuoteUpdateMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnQuoteUpdateMessageCalls)}: {walker.OnQuoteUpdateMessageCalls}");
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
    ///  The number of calls of the OnQuoteUpdateMessage method.
    /// </summary>
    public long OnQuoteUpdateMessageCalls;
    /// <summary>
    ///  The number of calls of the OnTradeReportMessage method.
    /// </summary>
    public long OnTradeReportMessageCalls;
    /// <summary>
    ///  The number of calls of the OnTradeBreakMessage method.
    /// </summary>
    public long OnTradeBreakMessageCalls;
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
    public void VisitPacket(global::Iex.IexEquities.Tops.IexTp.v1_56.Packet.Reader packet)
    {
        this.BeginPacketSequence();
        this.SeedPacketSequence(ulong.CreateSaturating(packet.IextpHeader.FirstMessageSequenceNumber.Value));
        this.OnIextpHeader(in packet.IextpHeader);
        var messages = packet.Messages;
        switch (messages.Case)
        {
            case global::Iex.IexEquities.Tops.IexTp.v1_56.Messages.Case.Message:
            {
                var messageEntries = messages.Message;
                var messageRead = 0L;
                while (messageEntries.MoveNext())
                {
                    var message = messageEntries.Current;
                    {
                        ulong? expected = null;
                        expected = this.NextPacketSequence();
                        this.CheckSequence((ulong?)message.SequenceNumber, expected, nameof(global::Iex.IexEquities.Tops.IexTp.v1_56.Message));
                    }
                    this.numberedDepth++;
                    var messageData = message.MessageData;
                    switch (messageData.Case)
                    {
                        case global::Iex.IexEquities.Tops.IexTp.v1_56.MessageData.Case.QuoteUpdateMessage:
                        {
                            this.OnQuoteUpdateMessage(in messageData.QuoteUpdateMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Tops.IexTp.v1_56.MessageData.Case.TradeReportMessage:
                        {
                            this.OnTradeReportMessage(in messageData.TradeReportMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Iex.IexEquities.Tops.IexTp.v1_56.MessageData.Case.TradeBreakMessage:
                        {
                            this.OnTradeBreakMessage(in messageData.TradeBreakMessage, in message.MessageHeader, message.SequenceNumber);
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
    void OnIextpHeader(in global::Iex.IexEquities.Tops.IexTp.v1_56.IextpHeader.Layout value)
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
    void OnQuoteUpdateMessage(in global::Iex.IexEquities.Tops.IexTp.v1_56.QuoteUpdateMessage.Layout value, in global::Iex.IexEquities.Tops.IexTp.v1_56.MessageHeader.Layout header, ulong sequenceNumber)
    {
        this.Parsed++;
        this.OnQuoteUpdateMessageCalls++;
        this.Observe(nameof(OnQuoteUpdateMessageCalls));
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
    void OnTradeReportMessage(in global::Iex.IexEquities.Tops.IexTp.v1_56.TradeReportMessage.Layout value, in global::Iex.IexEquities.Tops.IexTp.v1_56.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnTradeBreakMessage(in global::Iex.IexEquities.Tops.IexTp.v1_56.TradeBreakMessage.Layout value, in global::Iex.IexEquities.Tops.IexTp.v1_56.MessageHeader.Layout header, ulong sequenceNumber)
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
    void OnPacket(global::Iex.IexEquities.Tops.IexTp.v1_56.Packet.Reader value)
    {
        this.Parsed++;
        this.OnPacketCalls++;
        this.Observe(nameof(OnPacketCalls));
    }
}
