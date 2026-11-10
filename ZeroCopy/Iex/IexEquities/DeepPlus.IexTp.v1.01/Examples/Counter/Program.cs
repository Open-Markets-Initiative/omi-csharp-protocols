using Iex.IexEquities.DeepPlus.IexTp.v1_01;
using Iex.IexEquities.DeepPlus.IexTp.v1_01.Iterator;
using Iex.IexEquities.DeepPlus.IexTp.v1_01.Counter;

const int PcapArgument = 0;
const int FirstTreeArgument = 1;
const int Success = 0;
const int UsageError = 1;
const int ReadError = 1;
const string PacketTree = "Packet";
const string Usage = $"usage: Iex.IexEquities.DeepPlus.IexTp.v1.01.Counter <pcap-file> [{PacketTree}]";
const string TreeNote = "Reads the packet types named on the command line, or every packet type when none is named.";

if (args.Length <= PcapArgument)
{
    Console.Error.WriteLine(Usage);
    Console.Error.WriteLine(TreeNote);
    return UsageError;
}

var trees = args[FirstTreeArgument..];

if (trees.Except([PacketTree]).Any())
{
    Console.Error.WriteLine(Usage);
    Console.Error.WriteLine(TreeNote);
    return UsageError;
}

var packetCapture = trees is [] || trees.Contains(PacketTree) ? new PacketCapture() : null;
var counter = new MessageCounter();
var invalidReads = 0;
using var reader = global::Omi.Pcap.PcapReader.Open(args[PcapArgument]);
while (reader.Advance())
{
    if (packetCapture?.TryRead(reader.Data, reader.LinkType, out var packetReading) == true)
    {
        try
        {
            counter.VisitPacket(packetReading);
        }
        catch (ArgumentOutOfRangeException)
        {
            invalidReads++;
        }
    }
}
counter.WriteReport(new FileInfo(args[PcapArgument]).Length);
Console.Error.WriteLine($"{invalidReads} invalid reads.");
return invalidReads > 0 ? ReadError : Success;
