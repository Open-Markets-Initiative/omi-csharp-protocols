if (args.Length < 1)
{
    Console.Error.WriteLine("usage: Iex.IexEquities.DeepPlus.IexTp.v1.01.Manager <pcap-file>");
    return 1;
}

var path = args[0];
var manager = new IexIexEquitiesDeepPlusIexTpManager();

foreach (var frame in PcapReader.ReadPackets(path))
    manager.Process(frame);

return 0;