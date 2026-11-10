using Databento.MarketData.Normalized.Dbn.v3;
using Databento.MarketData.Normalized.Dbn.v3.Iterator;
using Databento.MarketData.Normalized.Dbn.v3.Viewer;

const int PcapArgument = 0;
const int FirstTreeArgument = 1;
const int SenderArguments = 2;
const int Success = 0;
const int UsageError = 1;
const int ReadError = 1;
const string SenderOption = "--sender";
const string FileTree = "File";
const string GatewayStreamTree = "GatewayStream";
const string ClientStreamTree = "ClientStream";
const string Usage = $"usage: Databento.MarketData.Normalized.Dbn.v3.Viewer <pcap-file> [{SenderOption} {nameof(global::Omi.Pcap.TcpSide.Server)}|{nameof(global::Omi.Pcap.TcpSide.Client)}] [{FileTree}] [{GatewayStreamTree}] [{ClientStreamTree}]";
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

if (trees.Except([FileTree, GatewayStreamTree, ClientStreamTree]).Any())
{
    Console.Error.WriteLine(Usage);
    Console.Error.WriteLine(TreeNote);
    return UsageError;
}

var gatewayStreamCapture = trees is [] || trees.Contains(GatewayStreamTree) ? new GatewayStreamCapture(sender) : null;
var clientStreamCapture = trees is [] || trees.Contains(ClientStreamTree) ? new ClientStreamCapture(sender) : null;
var invalidReads = 0;
if (trees is [FileTree])
{
    const int ChunkBytes = 65536;
    using var input = global::Omi.Pcap.DataFile.Open(args[PcapArgument]);
    var fileStream = new global::Databento.MarketData.Normalized.Dbn.v3.Iterator.FileStream();
    var chunk = new byte[ChunkBytes];
    int read;
    while (!fileStream.Invalid && (read = input.Read(chunk)) > 0)
    {
        var fileReading = fileStream.Receive(chunk.AsSpan(..read));
        if (fileStream.Invalid) break;
        try
        {
            if (fileReading.TryGetOpening(out var opening))
            {
                Dump.Line(string.Empty, nameof(global::Databento.MarketData.Normalized.Dbn.v3.File));
                Dump.Print(opening, Dump.Indent);
            }
            foreach (var root in fileReading)
            {
                Dump.Line(string.Empty, nameof(global::Databento.MarketData.Normalized.Dbn.v3.Record));
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
    if (gatewayStreamCapture?.TryRead(reader.Data, reader.LinkType, out var gatewayStreamReading) == true)
    {
        try
        {
            if (gatewayStreamReading.TryGetOpening(out var opening))
            {
                Dump.Line(string.Empty, nameof(global::Databento.MarketData.Normalized.Dbn.v3.GatewayStream));
                Dump.Print(opening, Dump.Indent);
            }
            foreach (var root in gatewayStreamReading)
            {
                Dump.Line(string.Empty, nameof(global::Databento.MarketData.Normalized.Dbn.v3.Record));
                Dump.Print(root, Dump.Indent);
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            invalidReads++;
        }
    }
    if (clientStreamCapture?.TryRead(reader.Data, reader.LinkType, out var clientStreamReading) == true)
    {
        try
        {
            foreach (var root in clientStreamReading)
            {
                Dump.Line(string.Empty, nameof(global::Databento.MarketData.Normalized.Dbn.v3.ControlMessage));
                Dump.Print(root, Dump.Indent);
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            invalidReads++;
        }
    }
}
if (gatewayStreamCapture is not null)
{
    Console.Error.WriteLine($"{GatewayStreamTree}: {gatewayStreamCapture.InvalidFlows} invalid, {gatewayStreamCapture.IncompleteFlows} incomplete and {gatewayStreamCapture.UnreadFlows} unread TCP flows.");
}
if (clientStreamCapture is not null)
{
    Console.Error.WriteLine($"{ClientStreamTree}: {clientStreamCapture.InvalidFlows} invalid, {clientStreamCapture.IncompleteFlows} incomplete and {clientStreamCapture.UnreadFlows} unread TCP flows.");
}
Console.Error.WriteLine($"{invalidReads} invalid reads.");
return invalidReads > 0 || gatewayStreamCapture is { InvalidFlows: > 0 } or { IncompleteFlows: > 0 } || clientStreamCapture is { InvalidFlows: > 0 } or { IncompleteFlows: > 0 } ? ReadError : Success;
