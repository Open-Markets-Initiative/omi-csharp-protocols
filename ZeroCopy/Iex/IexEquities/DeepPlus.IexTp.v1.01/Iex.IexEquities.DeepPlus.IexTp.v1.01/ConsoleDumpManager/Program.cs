using Iex.IexEquities.DeepPlus.IexTp;
using Iex.IexEquities.DeepPlus.IexTp.ConsoleDumpManager;
using Pcap.CSharp;

if (args.Length < 1)
{
    Console.Error.WriteLine("usage: Iex.IexEquities.DeepPlus.IexTp.v1.01.ConsoleDumpManager <pcap-file>");
    return 1;
}

var path = args[0];
var manager = new ConsoleDumpManager();

using var reader = PcapCursor.Open(path);
while (reader.Advance())
{
    var frame = reader.Data;
    if (!NetworkHeaders.TryGetUdpPayloadOffset(frame, out var transportOffset))
        continue;

    manager.Handle(frame, transportOffset);
}

return 0;
