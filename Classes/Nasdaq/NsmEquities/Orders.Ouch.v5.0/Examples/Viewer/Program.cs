const string Usage = "Usage: Nasdaq.NsmEquities.Orders.Ouch.v5.0.Viewer <pcap-file> [max-packets] [--feed Client | Server]";

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

string[] feedNames = ["Client", "Server"];
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

var clientViewer = new ClientViewer();
var serverViewer = new ServerViewer();
TcpFlowReader.Feed[] feeds =
[
    new TcpFlowReader.Feed("Client", TcpSide.Client, () => new ClientPacketStream().Append),
    new TcpFlowReader.Feed("Server", TcpSide.Server, () => new ServerPacketStream().Append),
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
                    case ClientPacket clientPacket:
                        clientViewer.Receive(clientPacket);
                        break;
                    case ServerPacket serverPacket:
                        serverViewer.Receive(flow, serverPacket);
                        break;
                }
                count++;
            }
        }
        if (flow is { Ended: true })
        {
            serverViewer.Release(flow);
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

if (clientViewer.Packets + serverViewer.Packets == 0)
{
    Console.Error.WriteLine("No packet was read from the capture.");
    return 1;
}

return 0;
