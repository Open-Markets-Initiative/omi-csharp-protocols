using Iex.IexEquities.DeepPlus.IexTp;
using Pcap.CSharp;

if (args.Length < 1) { Console.Error.WriteLine("usage: viewer <pcap-file> [max-packets]"); return 1; }

var path = args[0];
var max = args.Length > 1 && int.TryParse(args[1], out var n) ? n : int.MaxValue;
var count = 0;

foreach (var frame in PcapReader.ReadPackets(path))
{
    if (!NetworkHeaders.TryGetUdpPayload(frame.Span, out var payload)) continue;

    var packet = Packet.Parse(payload);
    if (!packet.IsValid) continue;

    Console.WriteLine(packet);
    if (++count >= max) break;
}

return 0;