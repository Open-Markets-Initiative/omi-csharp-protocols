const string Usage = "Usage: Iex.IexEquities.DeepPlus.IexTp.v1.01.Viewer <pcap-file> [max-packets]";

if (args.Length < 1)
{
    Console.Error.WriteLine(Usage);
    return 1;
}

var path = args[0];
var maxPackets = int.MaxValue;
for (var index = 1; index < args.Length; index++)
{
    if (int.TryParse(args[index], out var limit) && limit >= 0)
    {
        maxPackets = limit;
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

var packetViewer = new PacketViewer();
var count = 0;

try
{
    using var reader = PcapReader.Open(path);
    while (count < maxPackets && reader.Advance())
    {
        var frame = reader.Data;
        if (NetworkHeaders.TryGetUdpPayload(frame, reader.LinkType, out var packetPayload))
        {
            packetViewer.Receive(Packet.Parse(packetPayload));
            count++;
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

if (packetViewer.Packets == 0)
{
    Console.Error.WriteLine("No packet was read from the capture.");
    return 1;
}

return 0;
