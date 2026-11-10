using Iex.IexEquities.Deep.IexTp.v1_06;
using Omi.Pcap;

const string Usage = "Usage: Iex.IexEquities.Deep.IexTp.v1.06.Test <pcap-file> [--strict]";

if (args.Length < 1)
{
    Console.Error.WriteLine(Usage);
    return 1;
}

var path = args[0];
var strict = false;
for (var index = 1; index < args.Length; index++)
{
    if (args[index] == "--strict")
    {
        strict = true;
        continue;
    }
    Console.Error.WriteLine(Usage);
    return 1;
}

if (!File.Exists(path))
{
    Console.Error.WriteLine($"File not found: {path}");
    return 1;
}

Console.WriteLine($"Reading: {path}");

CaptureFeed[] captureFeeds =
[
    new("Packet", "Udp", null, NetworkHeaders.TryGetUdpPayload, TryPacket),
];

var flows = new TcpFlowReader([.. captureFeeds.Where(entry => entry.Feed is not null).Select(entry => entry.Feed!)]);
var datagramFeeds = captureFeeds.Where(entry => entry.Feed is null).GroupBy(entry => entry.Transport).ToList();
var flowTrials = new Dictionary<TcpFlow, List<(string Name, Trial Trial)>>();
var flowOrder = new List<TcpFlow>();
var readFlows = new HashSet<TcpFlow>();
var capturePacket = 0L;
var errors = 0;
var otherPackets = 0;
var otherFlows = 0;
var packets = 0;
var messages = 0;
var packetTrailers = 0;
var messageTrailers = 0;
var reEncoded = 0;
var counts = new Dictionary<string, int>(StringComparer.Ordinal);

try
{
    using var reader = PcapReader.Open(path);
    while (reader.Advance())
    {
        var frame = reader.Data;
        capturePacket = reader.PacketNumber;
        foreach (var transport in datagramFeeds)
        {
            if (!transport.First().Payload(frame, reader.LinkType, out var payload) || payload.IsEmpty)
                continue;
            var trials = transport.Select(entry => (entry.Name, Trial: entry.Start())).ToList();
            foreach (var (_, trial) in trials)
                trial.Add(capturePacket, payload, trials.Count > 1);
            if (IsOtherTraffic(trials))
                otherPackets++;
            else
                Report($"Capture packet {capturePacket}", trials, false);
        }
        if (flows.TryAdd(frame, reader.LinkType, out var flow, out var data))
        {
            var owner = flows.Read(flow, data, out _);
            if (!flowTrials.TryGetValue(flow, out var trials))
            {
                var offered = flows.Candidates(flow);
                trials = [.. captureFeeds.Where(entry => offered.Any(feed => feed.Name == entry.Name)).Select(entry => (entry.Name, Trial: entry.Start()))];
                flowTrials.Add(flow, trials);
                flowOrder.Add(flow);
            }
            if (owner is { } reading)
            {
                readFlows.Add(flow);
                trials.RemoveAll(trial => trial.Name != reading.Name);
            }
            foreach (var (_, trial) in trials)
                trial.Add(capturePacket, data, trials.Count > 1);
        }
    }
    if (reader.SkippedPackets > 0)
        Console.WriteLine($"Packets skipped: {reader.SkippedPackets}. They are on a pcap-ng interface with an unsupported link type, or are IPv4 fragments.");
}
catch (Exception exception) when (exception is InvalidDataException or UnauthorizedAccessException)
{
    Console.Error.WriteLine($"Cannot read {path}: {exception.Message}");
    return 1;
}

foreach (var flow in flowOrder)
{
    var trials = flowTrials[flow];
    if (trials.Count == 0)
        continue;
    if (IsOtherTraffic(trials))
        otherFlows++;
    else if (flow.Lost || flow.Waiting)
    {
        errors++;
        Console.WriteLine($"TCP {flow.Source} -> {flow.Destination}: the capture misses bytes of the flow");
    }
    else
        Report($"TCP {flow.Source} -> {flow.Destination}", trials, !readFlows.Contains(flow));
}

if (flows.StoppedFlows > 0)
    Console.WriteLine($"TCP flows stopped: {flows.StoppedFlows}. The capture misses bytes of the flow, or a frame states a length that is not valid.");

Console.WriteLine();
Console.WriteLine("=== Summary ===");
Console.WriteLine($"Packets:  {packets}");
Console.WriteLine($"Messages: {messages}");
Console.WriteLine($"Errors:   {errors}");
Console.WriteLine($"Other traffic: {otherPackets} capture packets, {otherFlows} TCP flows");
Console.WriteLine($"Packets with a trailer:  {packetTrailers}");
Console.WriteLine($"Messages with a trailer: {messageTrailers}");
Console.WriteLine($"Packets re-encoded:      {reEncoded}");
Console.WriteLine();
Console.WriteLine("Messages by type:");
foreach (var (type, count) in counts.OrderByDescending(x => x.Value))
    Console.WriteLine($"  {type}: {count}");

if (packets == 0)
{
    Console.Error.WriteLine("No packet was read from the capture.");
    return 1;
}

return errors > 0 ? 1 : 0;

bool IsOtherTraffic(List<(string Name, Trial Trial)> trials)
    => !strict && trials.TrueForAll(result => !result.Trial.Result.ReadFirst);

void Report(string label, List<(string Name, Trial Trial)> trials, bool unread)
{
    var accepted = trials.Where(result => result.Trial.Rejection is null).ToList();
    if (accepted.Count == 0)
    {
        errors++;
        Console.WriteLine($"{label}: no feed reads it");
        foreach (var (name, trial) in trials)
            Console.WriteLine($"  {name}: {trial.Rejection}");
        return;
    }

    var names = string.Join(", ", accepted.Select(result => result.Name));
    if (unread)
    {
        errors++;
        Console.WriteLine($"{label}: feeds {names} all read the first packets, so no feed is chosen");
        return;
    }
    if (Difference(accepted) is { } difference)
    {
        errors++;
        Console.WriteLine($"{label}: feeds {names} read it differently: {difference}");
        return;
    }

    var encodeFailed = accepted.Find(result => result.Trial.Result.EncodeFailure is not null);
    if (encodeFailed.Trial is not null)
    {
        errors++;
        Console.WriteLine($"{label}: {encodeFailed.Name}: {encodeFailed.Trial.Result.EncodeFailure}");
        return;
    }

    var accepting = accepted[0].Trial.Result;
    packets += accepting.Packets;
    messages += accepting.Messages;
    packetTrailers += accepting.PacketTrailers;
    messageTrailers += accepting.MessageTrailers;
    reEncoded += accepting.ReEncoded;
    foreach (var (type, count) in accepting.Counts)
        counts[type] = counts.GetValueOrDefault(type) + count;
}

static Trial TryPacket()
{
    var output = new StringWriter();
    var packets = 0;
    var messages = 0;
    var errors = 0;
    var readFirst = false;
    var packetTrailers = 0;
    var messageTrailers = 0;
    var reEncoded = 0;
    string? encodeFailure = null;
    var counts = new Dictionary<string, int>(StringComparer.Ordinal);
    var formatted = new List<string>();
    return new Trial(Decode, End, output);

    void Decode(long capturePacket, ReadOnlySpan<byte> payload, bool compare)
    {
        if (errors > 0)
        {
            return;
        }
        try
        {
            var packet = Packet.Parse(payload);
            if (packets == 0)
                readFirst = packet.IsParsed && packet.IsValid;
            packets++;
            if (!(packet.IsParsed && packet.IsValid))
            {
                errors++;
                output.Write($"Capture packet {capturePacket}: packet not valid");
                IHeader header = packet.Header;
                if (header.Status != ParseStatus.Ok)
                    output.Write($"; header {header.Status} at {header.FailedField?.Name}");
                else if (!header.IsRecognized)
                    output.Write($"; header value not recognized in {header.Fields.Where(field => !field.IsRecognized).Select(field => field.Name).FirstOrDefault()}");
                if (!packet.Messages.IsComplete && packet.Messages.StopReason is { } reason)
                    output.Write($"; messages end at byte {packet.Messages.FailedOffset}: {reason}");
                if (packet.Messages.FailedMessage is { } failedMessage)
                    output.Write($"; message {packet.Messages.Count} {failedMessage.GetType().Name} {failedMessage.Status} at {failedMessage.FailedField?.Name}");
                foreach (var (index, message) in packet.Messages.Index())
                {
                    if (message is UnknownPacketMessage unknownPacketMessage)
                    {
                        output.Write($"; message {index} has the unknown message type 0x{unknownPacketMessage.DataValue:X}");
                        continue;
                    }
                    if (!message.IsRecognized)
                        output.Write($"; message {index} {message.GetType().Name} value not recognized in {message.Fields.Where(field => !field.IsRecognized).Select(field => field.Name).FirstOrDefault()}");
                }
                foreach (var failure in packet.Validate())
                    output.Write($"; {failure.Field.Name} {failure.Reason}");
                output.WriteLine();
            }
            if (!packet.Trailer.IsEmpty)
                packetTrailers++;
            if (packet.IsParsed && packet.IsValid)
            {
                try
                {
                    var encoded = new byte[packet.EncodedByteLength];
                    if (!packet.TryEncode(encoded, out var written, out var failure))
                        encodeFailure ??= $"Capture packet {capturePacket}: {EncodeRefusal(failure)}";
                    else if (EncodeDifference(encoded.AsSpan(0, written), payload) is { } difference)
                        encodeFailure ??= $"Capture packet {capturePacket}: {difference}";
                    else
                        reEncoded++;
                }
                catch (Exception exception)
                {
                    encodeFailure ??= $"Capture packet {capturePacket}: the parsed packet cannot be re-encoded: {exception.Message}";
                }
            }

            foreach (var message in packet.Messages)
            {
                messages++;
                var type = message.GetType().Name;
                counts[type] = counts.GetValueOrDefault(type) + 1;
                if (!message.Trailer.IsEmpty)
                    messageTrailers++;
            }
            if (compare)
            {
                foreach (var message in packet.Messages)
                    formatted.Add(Detail(message));
            }
        }
        catch (Exception exception)
        {
            errors++;
            output.WriteLine($"Capture packet {capturePacket}: {exception.Message}");
        }
    }

    TrialResult End()
    {
        return new TrialResult(packets, messages, packetTrailers, messageTrailers, reEncoded, counts, formatted, errors, readFirst, encodeFailure);
    }
}

static string? Difference(List<(string Name, Trial Trial)> accepted)
{
    var (firstName, firstTrial) = accepted[0];
    var first = firstTrial.Result;
    foreach (var (name, other) in accepted.Skip(1))
    {
        var trial = other.Result;
        var shared = Math.Min(first.Formatted.Count, trial.Formatted.Count);
        for (var index = 0; index < shared; index++)
        {
            if (first.Formatted[index] != trial.Formatted[index])
                return $"{firstName} and {name} differ at message {index}";
        }
        if (first.Formatted.Count != trial.Formatted.Count)
            return $"{firstName} and {name} differ at message {shared}: {first.Formatted.Count} and {trial.Formatted.Count} messages";
    }
    return null;
}

static string Detail(IMessage message) => message switch
{
    UnknownPacketMessage unknown => $"0x{unknown.DataValue:X} {Convert.ToHexString(unknown.Bytes.Span)}",
    _ => $"{ModelMessage(message)} {Convert.ToHexString(message.Raw.Span)} {FieldDetail(message.Fields)}",
};

static string FieldDetail(IEnumerable<FieldView> fields)
    => $"{{ {string.Join(", ", fields.Select(field => $"{field.Name}: {ValueDetail(field)}"))} }}";

static string ValueDetail(FieldView field)
    => field.Message is { } message ? Detail(message)
    : field.Messages is { } messages ? $"[{string.Join(", ", messages.Select(Detail))}]"
    : field.Entries is { } entries ? $"[{string.Join(", ", entries.Select(entry => FieldDetail(entry.Fields)))}]"
    : field.Text;

static string ModelMessage(IMessage message) => message switch
{
    AuctionInformationMessage => "AuctionInformationMessage",
    OfficialPriceMessage => "OfficialPriceMessage",
    OperationalHaltStatusMessage => "OperationalHaltStatusMessage",
    PriceLevelBuyUpdateMessage => "PriceLevelBuyUpdateMessage",
    PriceLevelSellUpdateMessage => "PriceLevelSellUpdateMessage",
    SecurityDirectoryMessage => "SecurityDirectoryMessage",
    SecurityEventMessage => "SecurityEventMessage",
    ShortSalePriceTestStatusMessage => "ShortSalePriceTestStatusMessage",
    SystemEventMessage => "SystemEventMessage",
    TradeBreakMessage => "TradeBreakMessage",
    TradeReportMessage => "TradeReportMessage",
    TradingStatusMessage => "TradingStatusMessage",
    _ => message.GetType().Name,
};

static string EncodeRefusal(EncodeFailure failure)
{
    var location = failure.Parent is not { } parent ? string.Empty
        : failure.EntryIndex is { } entry ? $" in entry {entry} of {parent.Name}"
        : $" in {parent.Name}";
    var message = failure.MessageIndex is { } index ? $" in message {index}" : string.Empty;
    return $"the parsed packet cannot be re-encoded: {failure.Reason} at {failure.Descriptor?.Name}{location}{message}";
}

static string? EncodeDifference(ReadOnlySpan<byte> encoded, ReadOnlySpan<byte> captured)
{
    const int ShownBytes = 8;
    var offset = encoded.CommonPrefixLength(captured);
    if (offset == encoded.Length && offset == captured.Length)
        return null;
    var start = Math.Max(0, offset - ShownBytes);
    var encodedShown = Convert.ToHexString(encoded[start..Math.Min(encoded.Length, offset + ShownBytes)]);
    var capturedShown = Convert.ToHexString(captured[start..Math.Min(captured.Length, offset + ShownBytes)]);
    return $"the encoded packet differs from the captured bytes at byte {offset}: {encoded.Length} bytes encoded, {captured.Length} bytes captured; from byte {start}, encoded {encodedShown}, captured {capturedShown}";
}

delegate bool PayloadReader(ReadOnlySpan<byte> frame, LinkType linkType, out ReadOnlySpan<byte> payload);

delegate void PayloadDecoder(long capturePacket, ReadOnlySpan<byte> payload, bool compare);

record CaptureFeed(string Name, string Transport, TcpFlowReader.Feed? Feed, PayloadReader Payload, Func<Trial> Start);

record TrialResult(int Packets, int Messages, int PacketTrailers, int MessageTrailers, int ReEncoded, Dictionary<string, int> Counts, List<string> Formatted, int Errors, bool ReadFirst, string? EncodeFailure);

sealed class Trial(PayloadDecoder decode, Func<TrialResult> end, StringWriter output)
{
    private TrialResult? result;

    public void Add(long capturePacket, ReadOnlySpan<byte> payload, bool compare)
    {
        if (this.result is null)
            decode(capturePacket, payload, compare);
    }

    public TrialResult Result => this.result ??= end();

    public string? Rejection => this.Result.Errors > 0
        ? output.ToString().Split(Environment.NewLine)[0]
        : this.Result.Packets == 0 ? "no packet" : null;
}
