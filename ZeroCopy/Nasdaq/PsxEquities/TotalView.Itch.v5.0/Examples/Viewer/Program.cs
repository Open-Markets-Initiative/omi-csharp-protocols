using Nasdaq.PsxEquities.TotalView.Itch.v5_0;
using Nasdaq.PsxEquities.TotalView.Itch.v5_0.Iterator;
using Nasdaq.PsxEquities.TotalView.Itch.v5_0.Viewer;

const int PcapArgument = 0;
const int FirstTreeArgument = 1;
const int Success = 0;
const int UsageError = 1;
const int ReadError = 1;
const string PacketTree = "Packet";
const string Usage = $"usage: Nasdaq.PsxEquities.TotalView.Itch.v5.0.Viewer <pcap-file> [{PacketTree}]";
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
var invalidReads = 0;
using var reader = global::Omi.Pcap.PcapReader.Open(args[PcapArgument]);
while (reader.Advance())
{
    if (packetCapture?.TryRead(reader.Data, reader.LinkType, out var packetReading) == true)
    {
        try
        {
            Dump.Line(string.Empty, nameof(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Packet));
            Dump.Print(packetReading, Dump.Indent);
        }
        catch (ArgumentOutOfRangeException)
        {
            invalidReads++;
        }
    }
}
Console.Error.WriteLine($"{invalidReads} invalid reads.");
return invalidReads > 0 ? ReadError : Success;
