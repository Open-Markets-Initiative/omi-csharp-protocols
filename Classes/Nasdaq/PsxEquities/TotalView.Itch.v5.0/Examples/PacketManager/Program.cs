const string Usage = "Usage: Nasdaq.PsxEquities.TotalView.Itch.v5.0.PacketManager <pcap-file>";

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

var manager = new NasdaqPsxEquitiesTotalViewItchPacketManager();

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
Console.WriteLine($"Gaps:     {manager.SequenceGaps}");

if (manager.Packets == 0)
{
    Console.Error.WriteLine("No packet was read from the capture.");
    return 1;
}

return 0;