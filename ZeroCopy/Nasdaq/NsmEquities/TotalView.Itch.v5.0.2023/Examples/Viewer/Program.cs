using Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;
using Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Iterator;
using Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Viewer;

const int PcapArgument = 0;
const int FirstTreeArgument = 1;
const int SenderArguments = 2;
const int Success = 0;
const int UsageError = 1;
const int ReadError = 1;
const string SenderOption = "--sender";
const string ClientPacketTree = "ClientPacket";
const string ServerPacketTree = "ServerPacket";
const string PacketTree = "Packet";
const string FileTree = "File";
const string Usage = $"usage: Nasdaq.NsmEquities.TotalView.Itch.v5.0.2023.Viewer <pcap-file> [{SenderOption} {nameof(global::Omi.Pcap.TcpSide.Server)}|{nameof(global::Omi.Pcap.TcpSide.Client)}] [{ClientPacketTree}] [{ServerPacketTree}] [{PacketTree}] [{FileTree}]";
const string TreeNote = "Reads the packet types named on the command line, or every packet type when none is named.";
const string SenderNote = "The sender option names the side that sent a TCP flow whose capture has no connection handshake.";

if (args.Length <= PcapArgument)
{
    Console.Error.WriteLine(Usage);
    Console.Error.WriteLine(TreeNote);
    Console.Error.WriteLine(SenderNote);
    return UsageError;
}

var trees = args[FirstTreeArgument..];
var sender = global::Omi.Pcap.TcpSide.Unknown;
if (trees is [SenderOption, ..])
{
    if (trees is not [_, var side, ..] || !Enum.TryParse(side, out sender) || sender is not (global::Omi.Pcap.TcpSide.Server or global::Omi.Pcap.TcpSide.Client))
    {
        Console.Error.WriteLine(Usage);
        Console.Error.WriteLine(SenderNote);
        return UsageError;
    }

    trees = trees[SenderArguments..];
}

if (trees.Except([ClientPacketTree, ServerPacketTree, PacketTree, FileTree]).Any())
{
    Console.Error.WriteLine(Usage);
    Console.Error.WriteLine(TreeNote);
    return UsageError;
}

var clientPacketCapture = trees is [] || trees.Contains(ClientPacketTree) ? new ClientPacketCapture(sender) : null;
var serverPacketCapture = trees is [] || trees.Contains(ServerPacketTree) ? new ServerPacketCapture(sender) : null;
var packetCapture = trees is [] || trees.Contains(PacketTree) ? new PacketCapture() : null;
var invalidReads = 0;
if (trees is [FileTree])
{
    const int ChunkBytes = 65536;
    using var input = global::Omi.Pcap.DataFile.Open(args[PcapArgument]);
    var fileStream = new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Iterator.FileStream();
    var chunk = new byte[ChunkBytes];
    int read;
    while (!fileStream.Invalid && (read = input.Read(chunk)) > 0)
    {
        var fileReading = fileStream.Receive(chunk.AsSpan(..read));
        if (fileStream.Invalid) break;
        try
        {
            foreach (var root in fileReading)
            {
                Dump.Line(string.Empty, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FileMessage));
                Dump.Print(root, Dump.Indent);
            }
        }
        catch (global::System.ArgumentOutOfRangeException)
        {
            invalidReads++;
        }
    }
    Console.Error.WriteLine($"{invalidReads} invalid reads.");
    return fileStream.Invalid || fileStream.NeededBytes > 0 || invalidReads > 0 ? ReadError : Success;
}

using var reader = global::Omi.Pcap.PcapReader.Open(args[PcapArgument]);
while (reader.Advance())
{
    if (clientPacketCapture?.TryRead(reader.Data, reader.LinkType, out var clientPacketReading) == true)
    {
        try
        {
            foreach (var root in clientPacketReading)
            {
                Dump.Line(string.Empty, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPacket));
                Dump.Print(root, Dump.Indent);
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            invalidReads++;
        }
    }
    if (serverPacketCapture?.TryRead(reader.Data, reader.LinkType, out var serverPacketReading) == true)
    {
        try
        {
            foreach (var root in serverPacketReading)
            {
                Dump.Line(string.Empty, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPacket));
                Dump.Print(root, Dump.Indent);
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            invalidReads++;
        }
    }
    if (packetCapture?.TryRead(reader.Data, reader.LinkType, out var packetReading) == true)
    {
        try
        {
            Dump.Line(string.Empty, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Packet));
            Dump.Print(packetReading, Dump.Indent);
        }
        catch (ArgumentOutOfRangeException)
        {
            invalidReads++;
        }
    }
}
if (clientPacketCapture is not null)
{
    Console.Error.WriteLine($"{ClientPacketTree}: {clientPacketCapture.InvalidFlows} invalid, {clientPacketCapture.IncompleteFlows} incomplete and {clientPacketCapture.UnreadFlows} unread TCP flows.");
}
if (serverPacketCapture is not null)
{
    Console.Error.WriteLine($"{ServerPacketTree}: {serverPacketCapture.InvalidFlows} invalid, {serverPacketCapture.IncompleteFlows} incomplete and {serverPacketCapture.UnreadFlows} unread TCP flows.");
}
Console.Error.WriteLine($"{invalidReads} invalid reads.");
return invalidReads > 0 || clientPacketCapture is { InvalidFlows: > 0 } or { IncompleteFlows: > 0 } || serverPacketCapture is { InvalidFlows: > 0 } or { IncompleteFlows: > 0 } ? ReadError : Success;
