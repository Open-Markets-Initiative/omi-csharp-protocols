using Iex.IexEquities.DeepPlus.IexTp;
using Pcap.CSharp;

if (args.Length < 1)
{
    Console.WriteLine("Usage: Iex.IexEquities.DeepPlus.IexTp.v1.01.Test <pcap-file>");
    return 1;
}

var path = args[0];

if (!File.Exists(path))
{
    Console.WriteLine($"File not found: {path}");
    return 1;
}

Console.WriteLine($"Reading: {path}");
Console.WriteLine();

var packets = 0;
var messages = 0;
var errors = 0;
var counts = new Dictionary<string, int>(StringComparer.Ordinal);

foreach (var frame in PcapReader.ReadPackets(path))
{
    if (!NetworkHeaders.TryGetUdpPayload(frame.Span, out var payload)) continue;

    var packet = Packet.Parse(payload);
    packets++;
    if (!packet.IsValid) errors++;

    foreach (var message in packet.Messages)
    {
        messages++;
        var type = message.GetType().Name;
        counts[type] = counts.GetValueOrDefault(type) + 1;
    }
}

Console.WriteLine("=== Summary ===");
Console.WriteLine($"Packets:  {packets}");
Console.WriteLine($"Messages: {messages}");
Console.WriteLine($"Errors:   {errors}");
Console.WriteLine();
Console.WriteLine("Messages by type:");
foreach (var (type, count) in counts.OrderByDescending(x => x.Value))
    Console.WriteLine($"  {type}: {count}");

return errors > 0 ? 1 : 0;