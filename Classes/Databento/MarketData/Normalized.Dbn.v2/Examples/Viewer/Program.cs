const string Usage = "Usage: Databento.MarketData.Normalized.Dbn.v2.Viewer <pcap-file> [max-packets] [--feed Gateway | Client]";

if (args.Length < 1)
{
    Console.Error.WriteLine(Usage);
    return 1;
}

var path = args[0];
var maxPackets = int.MaxValue;
string? selectedFeed = null;
for (var index = 1; index < args.Length; index++)
{
    if (args[index] == "--feed" && index + 1 < args.Length)
    {
        selectedFeed = args[++index];
        continue;
    }
    if (int.TryParse(args[index], out var limit) && limit >= 0)
    {
        maxPackets = limit;
        continue;
    }
    Console.Error.WriteLine(Usage);
    return 1;
}

string[] feedNames = ["Gateway", "Client"];
if (selectedFeed is not null && !feedNames.Contains(selectedFeed))
{
    Console.Error.WriteLine($"Unknown feed: {selectedFeed}. Valid feeds: {string.Join(", ", feedNames)}");
    return 1;
}

if (!File.Exists(path))
{
    Console.Error.WriteLine($"File not found: {path}");
    return 1;
}

var gatewayViewer = new GatewayViewer();
var clientViewer = new ClientViewer();
TcpFlowReader.Feed[] feeds =
[
    new TcpFlowReader.Feed("Gateway", TcpSide.Server, () => new GatewayStreamStream().Append),
    new TcpFlowReader.Feed("Client", TcpSide.Client, () => new ClientStreamStream().Append),
];
var flows = new TcpFlowReader([.. feeds.Where(feed => selectedFeed is null || feed.Name == selectedFeed)]);
var count = 0;

try
{
    using var reader = PcapReader.Open(path);
    while (count < maxPackets && reader.Advance())
    {
        var frame = reader.Data;
        if (flows.TryAdd(frame, reader.LinkType, out var flow, out var data))
        {
            flows.Read(flow, data, out var packets);
            foreach (var received in packets)
            {
                if (count >= maxPackets)
                {
                    break;
                }
                switch (received)
                {
                    case GatewayStream gatewayStream:
                        gatewayViewer.Receive(gatewayStream);
                        break;
                    case ClientStream clientStream:
                        clientViewer.Receive(clientStream);
                        break;
                }
                count++;
            }
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

if (flows.UnreadFlows > 0)
    Console.WriteLine($"TCP flows not read: {flows.UnreadFlows}. No feed, or more than one feed, parses their first packets.");

if (flows.StoppedFlows > 0)
    Console.WriteLine($"TCP flows stopped: {flows.StoppedFlows}. The capture misses bytes of the flow, or a frame states a length that is not valid.");

if (gatewayViewer.Packets + clientViewer.Packets == 0)
{
    Console.Error.WriteLine("No packet was read from the capture.");
    return 1;
}

return 0;
