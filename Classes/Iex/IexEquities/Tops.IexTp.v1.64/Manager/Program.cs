if (args.Length < 1)
{
    Console.Error.WriteLine("usage: Iex.IexEquities.Tops.IexTp.v1.64.Manager <pcap-file>");
    return 1;
}

var path = args[0];
var manager = new IexIexEquitiesTopsIexTpManager();

foreach (var frame in PcapReader.ReadPackets(path))
    manager.Process(frame);

return 0;