const string Usage = "Usage: Databento.MarketData.Normalized.Dbn.v1.ClientManager <pcap-file>";

if (args.Length != 1)
{
    Console.Error.WriteLine(Usage);
    return 1;
}

var path = args[0];

if (!File.Exists(path))
{
    Console.Error.WriteLine($"File not found: {path}");
    return 1;
}

var manager = new DatabentoMarketDataNormalizedDbnClientManager();

try
{
    using var reader = PcapReader.Open(path);
    while (reader.Advance())
    {
        var frame = reader.Data;
        manager.Process(frame, reader.LinkType);
    }
    if (reader.SkippedPackets > 0)
        Console.WriteLine($"Packets skipped: {reader.SkippedPackets}. They are on a pcap-ng interface with an unsupported link type, or are IPv4 fragments.");
}
catch (Exception exception) when (exception is InvalidDataException or UnauthorizedAccessException)
{
    Console.Error.WriteLine($"Cannot read {path}: {exception.Message}");
    return 1;
}

Console.WriteLine($"Packets:  {manager.Packets}");
Console.WriteLine($"Messages: {manager.Messages}");

if (manager.UnreadFlows > 0)
    Console.WriteLine($"TCP flows not read: {manager.UnreadFlows}. No feed, or more than one feed, parses their first packets.");

if (manager.StoppedFlows > 0)
    Console.WriteLine($"TCP flows stopped: {manager.StoppedFlows}. The capture misses bytes of the flow, or a frame states a length that is not valid.");

if (manager.Packets == 0)
{
    Console.Error.WriteLine("No packet was read from the capture.");
    return 1;
}

return 0;