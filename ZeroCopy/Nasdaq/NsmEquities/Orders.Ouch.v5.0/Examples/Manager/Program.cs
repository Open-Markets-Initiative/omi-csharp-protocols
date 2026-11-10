using Nasdaq.NsmEquities.Orders.Ouch.v5_0;
using Nasdaq.NsmEquities.Orders.Ouch.v5_0.Iterator;
using Nasdaq.NsmEquities.Orders.Ouch.v5_0.Manager;

const int PcapArgument = 0;
const int FirstTreeArgument = 1;
const int SenderArguments = 2;
const int Success = 0;
const int UsageError = 1;
const int ReadError = 1;
const string SenderOption = "--sender";
const string ClientPacketTree = "ClientPacket";
const string ServerPacketTree = "ServerPacket";
const string Usage = $"usage: Nasdaq.NsmEquities.Orders.Ouch.v5.0.Manager <pcap-file> [{SenderOption} {nameof(global::Omi.Pcap.TcpSide.Server)}|{nameof(global::Omi.Pcap.TcpSide.Client)}] [{ClientPacketTree}] [{ServerPacketTree}]";
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

if (trees.Except([ClientPacketTree, ServerPacketTree]).Any())
{
    Console.Error.WriteLine(Usage);
    Console.Error.WriteLine(TreeNote);
    return UsageError;
}

var clientPacketCapture = trees is [] || trees.Contains(ClientPacketTree) ? new ClientPacketCapture(sender) : null;
var serverPacketCapture = trees is [] || trees.Contains(ServerPacketTree) ? new ServerPacketCapture(sender) : null;
var manager = new MessageHandler();
var serverPacketConversations = new global::System.Collections.Generic.Dictionary<(global::System.Net.IPEndPoint?, global::System.Net.IPEndPoint?), ConversationState>();
var invalidReads = 0;
using var reader = global::Omi.Pcap.PcapReader.Open(args[PcapArgument]);
while (reader.Advance())
{
    if (clientPacketCapture?.TryRead(reader.Data, reader.LinkType, out var clientPacketReading) == true)
    {
        try
        {
            foreach (var root in clientPacketReading)
            {
                manager.VisitClientPacket(root);
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
            var conversation = (serverPacketCapture.CurrentFlow!.Source, serverPacketCapture.CurrentFlow!.Destination);
            if (!serverPacketConversations.TryGetValue(conversation, out var conversationState)) serverPacketConversations.Add(conversation, conversationState = new());
            manager.ConversationState = conversationState;
            foreach (var root in serverPacketReading)
            {
                manager.VisitServerPacket(root);
            }
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
