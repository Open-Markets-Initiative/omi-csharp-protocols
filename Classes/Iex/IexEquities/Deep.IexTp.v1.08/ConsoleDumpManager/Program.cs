if (args.Length < 1)
{
    Console.Error.WriteLine("usage: Iex.IexEquities.Deep.IexTp.v1.08.ConsoleDumpManager <pcap-file>");
    return 1;
}

var path = args[0];
var manager = new ConsoleDumpManager();

foreach (var frame in PcapReader.ReadPackets(path))
    manager.Process(frame);

return 0;