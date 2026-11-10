using Nasdaq.NsmEquities.Orders.Ouch.v5_0;
using Nasdaq.NsmEquities.Orders.Ouch.v5_0.Iterator;
using Nasdaq.NsmEquities.Orders.Ouch.v5_0.Test;
using static Nasdaq.NsmEquities.Orders.Ouch.v5_0.Test.Dump;

const int PcapArgument = 0;
const int FirstOption = 1;
const int None = 0;
const int OneFeed = 1;
const int Success = 0;
const int Failure = 1;
const int UsageFailure = 2;
const string FeedOption = "--feed";
const string StrictOption = "--strict";
const string Usage = $"usage: Nasdaq.NsmEquities.Orders.Ouch.v5.0.Test <pcap> [{FeedOption} <name>] [{StrictOption}]";
const string ClientPacketTree = "ClientPacket";
const string ClientPacketAlias = "Client";
const string ServerPacketTree = "ServerPacket";
const string ServerPacketAlias = "Server";

if (args.Length <= PcapArgument)
{
    global::System.Console.Error.WriteLine(Usage);
    return UsageFailure;
}

string? feed = null;
var strict = false;
for (var index = FirstOption; index < args.Length; index++)
{
    if (args[index] == StrictOption && !strict) strict = true;
    else if (args[index] == FeedOption && feed is null && index + 1 < args.Length) feed = args[++index];
    else
    {
        global::System.Console.Error.WriteLine(Usage);
        return UsageFailure;
    }
}

var feeds = None;
var clientPacketCapture = Selects(ClientPacketTree, ClientPacketAlias) ? new ClientPacketCapture(global::Omi.Pcap.TcpSide.Unknown) : null;
var serverPacketCapture = Selects(ServerPacketTree, ServerPacketAlias) ? new ServerPacketCapture(global::Omi.Pcap.TcpSide.Unknown) : null;
var clientPacketCaptureSplit = clientPacketCapture is null ? null : new ClientPacketCapture(global::Omi.Pcap.TcpSide.Unknown);
var serverPacketCaptureSplit = serverPacketCapture is null ? null : new ServerPacketCapture(global::Omi.Pcap.TcpSide.Unknown);
if (feed is not null && feeds != OneFeed)
{
    global::System.Console.Error.WriteLine($"Unknown or ambiguous feed '{feed}'.");
    global::System.Console.Error.WriteLine(Usage);
    return UsageFailure;
}

const byte Filler = 0xA5;
const int Following = 8;
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SystemEventMessage.Layout>() != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SystemEventMessage.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SystemEventMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SystemEventMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SystemEventMessage.Layout.Read(new byte[global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SystemEventMessage.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SystemEventMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SystemEventMessage.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SystemEventMessage.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SystemEventMessage.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SystemEventMessage)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SystemEventMessage.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SystemEventMessage.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SystemEventMessage.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SystemEventMessage.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SystemEventMessage.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SystemEventMessage.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SystemEventMessage)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRejectedPacket.Layout>() != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRejectedPacket.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRejectedPacket.Layout.Read(new byte[global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRejectedPacket.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRejectedPacket.Layout.Read(new byte[global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRejectedPacket.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRejectedPacket.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRejectedPacket.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRejectedPacket.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRejectedPacket.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRejectedPacket)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRejectedPacket.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRejectedPacket.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRejectedPacket.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRejectedPacket.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRejectedPacket.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRejectedPacket.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRejectedPacket)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginAcceptedPacket.Layout>() != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginAcceptedPacket.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginAcceptedPacket.Layout.Read(new byte[global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginAcceptedPacket.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginAcceptedPacket.Layout.Read(new byte[global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginAcceptedPacket.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginAcceptedPacket.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginAcceptedPacket.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginAcceptedPacket.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginAcceptedPacket.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginAcceptedPacket)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginAcceptedPacket.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginAcceptedPacket.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginAcceptedPacket.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginAcceptedPacket.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginAcceptedPacket.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginAcceptedPacket.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginAcceptedPacket)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPacketHeader.Layout>() != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPacketHeader.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPacketHeader.Layout.Read(new byte[global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPacketHeader.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPacketHeader.Layout.Read(new byte[global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPacketHeader.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPacketHeader.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPacketHeader.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPacketHeader.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPacketHeader.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPacketHeader)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPacketHeader.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPacketHeader.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPacketHeader.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPacketHeader.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPacketHeader.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPacketHeader.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPacketHeader)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRequestPacket.Layout>() != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRequestPacket.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRequestPacket.Layout.Read(new byte[global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRequestPacket.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRequestPacket.Layout.Read(new byte[global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRequestPacket.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRequestPacket.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRequestPacket.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRequestPacket.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRequestPacket.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRequestPacket)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRequestPacket.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRequestPacket.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRequestPacket.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRequestPacket.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRequestPacket.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRequestPacket.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRequestPacket)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
if (global::System.Runtime.CompilerServices.Unsafe.SizeOf<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPacketHeader.Layout>() != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPacketHeader.Layout.Size || Throws(() => { global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPacketHeader.Layout.Read(new byte[global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPacketHeader.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPacketHeader.Layout.Read(new byte[global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPacketHeader.Layout.Size - 1]); }) || Throws(() => { global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPacketHeader.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPacketHeader.Layout.Size]); }) || !Throws(() => { global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPacketHeader.Layout.Initialize(new byte[global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPacketHeader.Layout.Size - 1]); }))
{
    global::System.Console.Error.WriteLine($"FAIL: the size, the read or the write of the {nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPacketHeader)} Layout does not match the model.");
    return Failure;
}
{
    var zeroed = new byte[global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPacketHeader.Layout.Size];
    var filled = new byte[global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPacketHeader.Layout.Size + Following];
    global::System.Array.Fill(filled, Filler);
    global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPacketHeader.Layout.Initialize(zeroed);
    global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPacketHeader.Layout.Initialize(filled);
    if (!filled.AsSpan(0, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPacketHeader.Layout.Size).SequenceEqual(zeroed) || filled.AsSpan(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPacketHeader.Layout.Size).ContainsAnyExcept(Filler))
    {
        global::System.Console.Error.WriteLine($"FAIL: the write of the {nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPacketHeader)} Layout does not clear exactly its bytes.");
        return Failure;
    }
}
var walker = new Walker();
var splitter = new Walker();
var unread = None;
var invalid = None;
var incomplete = None;
long skipped;
try
{
    using var reader = global::Omi.Pcap.PcapReader.Open(args[PcapArgument]);
    while (reader.Advance())
    {
        var read = false;
        walker.Frame = reader.Data.ToArray();
        if (clientPacketCapture is not null)
        {
            var opened = clientPacketCapture.TryRead(walker.Frame, reader.LinkType, out var reading);
            if (clientPacketCapture.FrameRead)
            {
                read = true;
                if (opened)
                {
                    try
                    {
                        foreach (var root in reading)
                        {
                            walker.VisitClientPacket(root);
                        }
                    }
                    catch (global::System.ArgumentOutOfRangeException)
                    {
                        invalid++;
                    }
                }
                walker.Observe(clientPacketCapture.Consumed);
            }
            if (clientPacketCaptureSplit is not null && clientPacketCaptureSplit.TryGetSegment(walker.Frame, reader.LinkType, out var segment))
            {
                var middle = segment.Length / 2;
                try
                {
                    foreach (var root in clientPacketCaptureSplit.Receive(segment[..middle]))
                    {
                        splitter.VisitClientPacket(root);
                    }
                    foreach (var root in clientPacketCaptureSplit.Receive(segment[middle..]))
                    {
                        splitter.VisitClientPacket(root);
                    }
                }
                catch (global::System.ArgumentOutOfRangeException)
                {
                }
                walker.Splits++;
            }
        }
        if (serverPacketCapture is not null)
        {
            var opened = serverPacketCapture.TryRead(walker.Frame, reader.LinkType, out var reading);
            if (serverPacketCapture.FrameRead)
            {
                read = true;
                if (opened)
                {
                    try
                    {
                        foreach (var root in reading)
                        {
                            walker.VisitServerPacket(root);
                        }
                    }
                    catch (global::System.ArgumentOutOfRangeException)
                    {
                        invalid++;
                    }
                }
                walker.Observe(serverPacketCapture.Consumed);
            }
            if (serverPacketCaptureSplit is not null && serverPacketCaptureSplit.TryGetSegment(walker.Frame, reader.LinkType, out var segment))
            {
                var middle = segment.Length / 2;
                try
                {
                    foreach (var root in serverPacketCaptureSplit.Receive(segment[..middle]))
                    {
                        splitter.VisitServerPacket(root);
                    }
                    foreach (var root in serverPacketCaptureSplit.Receive(segment[middle..]))
                    {
                        splitter.VisitServerPacket(root);
                    }
                }
                catch (global::System.ArgumentOutOfRangeException)
                {
                }
                walker.Splits++;
            }
        }
        if (!read) unread++;
    }

    skipped = reader.SkippedPackets;
}
catch (global::System.Exception exception)
{
    global::System.Console.Error.WriteLine($"FAIL: {exception.GetType().Name}: {exception.Message}");
    return Failure;
}

invalid += clientPacketCapture?.InvalidFlows ?? None;
incomplete += clientPacketCapture?.IncompleteFlows ?? None;
unread -= clientPacketCapture?.HeldFramesRead ?? None;
invalid += serverPacketCapture?.InvalidFlows ?? None;
incomplete += serverPacketCapture?.IncompleteFlows ?? None;
unread -= serverPacketCapture?.HeldFramesRead ?? None;
global::System.Console.WriteLine($"Parsed: {walker.Parsed}; invalid: {invalid}; incomplete: {incomplete}; unread frames: {unread}; skipped frames: {skipped}");
if (walker.OnEndOfSessionCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnEndOfSessionCalls)}: {walker.OnEndOfSessionCalls}");
if (walker.OnServerHeartbeatCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnServerHeartbeatCalls)}: {walker.OnServerHeartbeatCalls}");
if (walker.OnEnableOrderEntryResponseMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnEnableOrderEntryResponseMessageCalls)}: {walker.OnEnableOrderEntryResponseMessageCalls}");
if (walker.OnDisableOrderEntryResponseMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnDisableOrderEntryResponseMessageCalls)}: {walker.OnDisableOrderEntryResponseMessageCalls}");
if (walker.OnMassCancelResponseMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnMassCancelResponseMessageCalls)}: {walker.OnMassCancelResponseMessageCalls}");
if (walker.OnAccountQueryResponseMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnAccountQueryResponseMessageCalls)}: {walker.OnAccountQueryResponseMessageCalls}");
if (walker.OnOrderRestatedMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnOrderRestatedMessageCalls)}: {walker.OnOrderRestatedMessageCalls}");
if (walker.OnOrderModifiedMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnOrderModifiedMessageCalls)}: {walker.OnOrderModifiedMessageCalls}");
if (walker.OnOrderPriorityUpdateMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnOrderPriorityUpdateMessageCalls)}: {walker.OnOrderPriorityUpdateMessageCalls}");
if (walker.OnCancelRejectMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnCancelRejectMessageCalls)}: {walker.OnCancelRejectMessageCalls}");
if (walker.OnCancelPendingMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnCancelPendingMessageCalls)}: {walker.OnCancelPendingMessageCalls}");
if (walker.OnRejectedOrderMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnRejectedOrderMessageCalls)}: {walker.OnRejectedOrderMessageCalls}");
if (walker.OnBrokenTradeMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnBrokenTradeMessageCalls)}: {walker.OnBrokenTradeMessageCalls}");
if (walker.OnOrderExecutedMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnOrderExecutedMessageCalls)}: {walker.OnOrderExecutedMessageCalls}");
if (walker.OnAiqCanceledMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnAiqCanceledMessageCalls)}: {walker.OnAiqCanceledMessageCalls}");
if (walker.OnCanceledMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnCanceledMessageCalls)}: {walker.OnCanceledMessageCalls}");
if (walker.OnReplacedMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnReplacedMessageCalls)}: {walker.OnReplacedMessageCalls}");
if (walker.OnOrderAcceptedMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnOrderAcceptedMessageCalls)}: {walker.OnOrderAcceptedMessageCalls}");
if (walker.OnSystemEventMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnSystemEventMessageCalls)}: {walker.OnSystemEventMessageCalls}");
if (walker.OnSequencedDataPacketCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnSequencedDataPacketCalls)}: {walker.OnSequencedDataPacketCalls}");
if (walker.OnLoginRejectedPacketCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnLoginRejectedPacketCalls)}: {walker.OnLoginRejectedPacketCalls}");
if (walker.OnLoginAcceptedPacketCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnLoginAcceptedPacketCalls)}: {walker.OnLoginAcceptedPacketCalls}");
if (walker.OnDebugPacketCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnDebugPacketCalls)}: {walker.OnDebugPacketCalls}");
if (walker.OnLogoutRequestCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnLogoutRequestCalls)}: {walker.OnLogoutRequestCalls}");
if (walker.OnClientHeartbeatCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnClientHeartbeatCalls)}: {walker.OnClientHeartbeatCalls}");
if (walker.OnEnableOrderEntryRequestMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnEnableOrderEntryRequestMessageCalls)}: {walker.OnEnableOrderEntryRequestMessageCalls}");
if (walker.OnDisableOrderEntryRequestMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnDisableOrderEntryRequestMessageCalls)}: {walker.OnDisableOrderEntryRequestMessageCalls}");
if (walker.OnMassCancelRequestMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnMassCancelRequestMessageCalls)}: {walker.OnMassCancelRequestMessageCalls}");
if (walker.OnAccountQueryMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnAccountQueryMessageCalls)}: {walker.OnAccountQueryMessageCalls}");
if (walker.OnModifyOrderMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnModifyOrderMessageCalls)}: {walker.OnModifyOrderMessageCalls}");
if (walker.OnCancelOrderMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnCancelOrderMessageCalls)}: {walker.OnCancelOrderMessageCalls}");
if (walker.OnReplaceOrderMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnReplaceOrderMessageCalls)}: {walker.OnReplaceOrderMessageCalls}");
if (walker.OnEnterOrderMessageCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnEnterOrderMessageCalls)}: {walker.OnEnterOrderMessageCalls}");
if (walker.OnUnsequencedDataPacketCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnUnsequencedDataPacketCalls)}: {walker.OnUnsequencedDataPacketCalls}");
if (walker.OnLoginRequestPacketCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnLoginRequestPacketCalls)}: {walker.OnLoginRequestPacketCalls}");
if (walker.OnServerPacketHeaderCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnServerPacketHeaderCalls)}: {walker.OnServerPacketHeaderCalls}");
if (walker.OnServerPacketCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnServerPacketCalls)}: {walker.OnServerPacketCalls}");
if (walker.OnClientPacketHeaderCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnClientPacketHeaderCalls)}: {walker.OnClientPacketHeaderCalls}");
if (walker.OnClientPacketCalls > None) global::System.Console.WriteLine($"{nameof(walker.OnClientPacketCalls)}: {walker.OnClientPacketCalls}");
global::System.Console.WriteLine($"Observed: {walker.Observed:X16}");
global::System.Console.WriteLine($"Checked: {nameof(walker.StableEntries)} {walker.StableEntries}; {nameof(walker.EndedEntries)} {walker.EndedEntries}; {nameof(walker.ActionChecks)} {walker.ActionChecks}; {nameof(walker.Splits)} {walker.Splits}");
if (splitter.Framed != walker.Framed)
{
    global::System.Console.Error.WriteLine("FAIL: the reads of the segments split at their midpoint observe the frames differently from the unsplit reads.");
    return Failure;
}
return invalid > None || incomplete > None || strict && (walker.Parsed == None || unread > None || skipped > None) ? Failure : Success;

bool Selects(string tree, string alias)
{
    var selected = feed is null || string.Equals(feed, tree, global::System.StringComparison.OrdinalIgnoreCase) || string.Equals(feed, alias, global::System.StringComparison.OrdinalIgnoreCase);
    if (selected) feeds++;
    return selected;
}

bool Throws(global::System.Action action)
{
    try
    {
        action();
        return false;
    }
    catch (global::System.ArgumentException)
    {
        return true;
    }
}

struct Walker
{
    /// <summary>
    ///  The number of messages parsed.
    /// </summary>
    public long Parsed;
    /// <summary>
    ///  The number of calls of the OnClientPacketHeader method.
    /// </summary>
    public long OnClientPacketHeaderCalls;
    /// <summary>
    ///  The number of calls of the OnDebugPacket method.
    /// </summary>
    public long OnDebugPacketCalls;
    /// <summary>
    ///  The number of calls of the OnLoginRequestPacket method.
    /// </summary>
    public long OnLoginRequestPacketCalls;
    /// <summary>
    ///  The number of calls of the OnUnsequencedDataPacket method.
    /// </summary>
    public long OnUnsequencedDataPacketCalls;
    /// <summary>
    ///  The number of calls of the OnClientHeartbeat method.
    /// </summary>
    public long OnClientHeartbeatCalls;
    /// <summary>
    ///  The number of calls of the OnLogoutRequest method.
    /// </summary>
    public long OnLogoutRequestCalls;
    /// <summary>
    ///  The number of calls of the OnEnterOrderMessage method.
    /// </summary>
    public long OnEnterOrderMessageCalls;
    /// <summary>
    ///  The number of calls of the OnReplaceOrderMessage method.
    /// </summary>
    public long OnReplaceOrderMessageCalls;
    /// <summary>
    ///  The number of calls of the OnCancelOrderMessage method.
    /// </summary>
    public long OnCancelOrderMessageCalls;
    /// <summary>
    ///  The number of calls of the OnModifyOrderMessage method.
    /// </summary>
    public long OnModifyOrderMessageCalls;
    /// <summary>
    ///  The number of calls of the OnAccountQueryMessage method.
    /// </summary>
    public long OnAccountQueryMessageCalls;
    /// <summary>
    ///  The number of calls of the OnMassCancelRequestMessage method.
    /// </summary>
    public long OnMassCancelRequestMessageCalls;
    /// <summary>
    ///  The number of calls of the OnDisableOrderEntryRequestMessage method.
    /// </summary>
    public long OnDisableOrderEntryRequestMessageCalls;
    /// <summary>
    ///  The number of calls of the OnEnableOrderEntryRequestMessage method.
    /// </summary>
    public long OnEnableOrderEntryRequestMessageCalls;
    /// <summary>
    ///  The number of calls of the OnServerPacketHeader method.
    /// </summary>
    public long OnServerPacketHeaderCalls;
    /// <summary>
    ///  The number of calls of the OnLoginAcceptedPacket method.
    /// </summary>
    public long OnLoginAcceptedPacketCalls;
    /// <summary>
    ///  The number of calls of the OnLoginRejectedPacket method.
    /// </summary>
    public long OnLoginRejectedPacketCalls;
    /// <summary>
    ///  The number of calls of the OnSequencedDataPacket method.
    /// </summary>
    public long OnSequencedDataPacketCalls;
    /// <summary>
    ///  The number of calls of the OnServerHeartbeat method.
    /// </summary>
    public long OnServerHeartbeatCalls;
    /// <summary>
    ///  The number of calls of the OnEndOfSession method.
    /// </summary>
    public long OnEndOfSessionCalls;
    /// <summary>
    ///  The number of calls of the OnSystemEventMessage method.
    /// </summary>
    public long OnSystemEventMessageCalls;
    /// <summary>
    ///  The number of calls of the OnOrderAcceptedMessage method.
    /// </summary>
    public long OnOrderAcceptedMessageCalls;
    /// <summary>
    ///  The number of calls of the OnReplacedMessage method.
    /// </summary>
    public long OnReplacedMessageCalls;
    /// <summary>
    ///  The number of calls of the OnCanceledMessage method.
    /// </summary>
    public long OnCanceledMessageCalls;
    /// <summary>
    ///  The number of calls of the OnAiqCanceledMessage method.
    /// </summary>
    public long OnAiqCanceledMessageCalls;
    /// <summary>
    ///  The number of calls of the OnOrderExecutedMessage method.
    /// </summary>
    public long OnOrderExecutedMessageCalls;
    /// <summary>
    ///  The number of calls of the OnBrokenTradeMessage method.
    /// </summary>
    public long OnBrokenTradeMessageCalls;
    /// <summary>
    ///  The number of calls of the OnRejectedOrderMessage method.
    /// </summary>
    public long OnRejectedOrderMessageCalls;
    /// <summary>
    ///  The number of calls of the OnCancelPendingMessage method.
    /// </summary>
    public long OnCancelPendingMessageCalls;
    /// <summary>
    ///  The number of calls of the OnCancelRejectMessage method.
    /// </summary>
    public long OnCancelRejectMessageCalls;
    /// <summary>
    ///  The number of calls of the OnOrderPriorityUpdateMessage method.
    /// </summary>
    public long OnOrderPriorityUpdateMessageCalls;
    /// <summary>
    ///  The number of calls of the OnOrderModifiedMessage method.
    /// </summary>
    public long OnOrderModifiedMessageCalls;
    /// <summary>
    ///  The number of calls of the OnOrderRestatedMessage method.
    /// </summary>
    public long OnOrderRestatedMessageCalls;
    /// <summary>
    ///  The number of calls of the OnAccountQueryResponseMessage method.
    /// </summary>
    public long OnAccountQueryResponseMessageCalls;
    /// <summary>
    ///  The number of calls of the OnMassCancelResponseMessage method.
    /// </summary>
    public long OnMassCancelResponseMessageCalls;
    /// <summary>
    ///  The number of calls of the OnDisableOrderEntryResponseMessage method.
    /// </summary>
    public long OnDisableOrderEntryResponseMessageCalls;
    /// <summary>
    ///  The number of calls of the OnEnableOrderEntryResponseMessage method.
    /// </summary>
    public long OnEnableOrderEntryResponseMessageCalls;
    /// <summary>
    ///  The number of calls of the OnClientPacket method.
    /// </summary>
    public long OnClientPacketCalls;
    /// <summary>
    ///  The number of calls of the OnServerPacket method.
    /// </summary>
    public long OnServerPacketCalls;
    /// <summary>
    ///  The current entries that a second read gave again.
    /// </summary>
    public long StableEntries;
    /// <summary>
    ///  The entries that ended with the number of entries read.
    /// </summary>
    public long EndedEntries;
    /// <summary>
    ///  The action values that a Context gives as the Reader of the field gives them.
    /// </summary>
    public long ActionChecks;
    /// <summary>
    ///  The root occurrences of segments split at their midpoint whose observations equal those of the unsplit read.
    /// </summary>
    public long Splits;
    const ulong ObservationBasis = 14695981039346656037;
    const ulong ObservationPrime = 1099511628211;
    const int Unplaced = -1;
    /// <summary>
    ///  The hash of the observations so far.
    /// </summary>
    public ulong Observed;
    /// <summary>
    ///  The bytes of the current frame, which the captures read.
    /// </summary>
    public byte[] Frame;
    /// <summary>
    ///  The text that the Dump print of a visited value writes.
    /// </summary>
    public global::System.IO.StringWriter Printer;
    /// <summary>
    ///  The hash of the observations inside the frames of a stream, apart from the Layout positions.
    /// </summary>
    public ulong Framed;
    /// <summary>
    ///  The number of frames of a stream that contain the current visit.
    /// </summary>
    public int Inside;

    public Walker()
    {
        this.Observed = ObservationBasis;
        this.Frame = [];
        this.Printer = new();
    }

    void Observe(global::System.ReadOnlySpan<byte> bytes)
    {
        foreach (var value in bytes) this.Observed = unchecked((this.Observed ^ value) * ObservationPrime);
        if (this.Inside > 0) foreach (var value in bytes) this.Framed = unchecked((this.Framed ^ value) * ObservationPrime);
    }

    /// <summary>
    ///  Adds a number, such as where the parser stopped in a frame, to the hash of the observations.
    /// </summary>
    public void Observe(long value) => this.Observe(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.ReadOnlySpan<long>(in value)));
    void Observe(string text) => this.Observe(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(text.AsSpan()));
    int Place(global::System.ReadOnlySpan<byte> bytes) => global::System.MemoryExtensions.Overlaps((global::System.ReadOnlySpan<byte>)this.Frame, bytes, out var offset) ? offset : Unplaced;
    void Locate(global::System.ReadOnlySpan<byte> bytes)
    {
        long place = this.Place(bytes);
        foreach (var value in global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.ReadOnlySpan<long>(in place))) this.Observed = unchecked((this.Observed ^ value) * ObservationPrime);
    }

    void CheckAction<T>(T fromReader, T fromContext)
    {
        if (!global::System.Collections.Generic.EqualityComparer<T>.Default.Equals(fromReader, fromContext)) throw new global::System.InvalidOperationException($"A Context gives the action value {fromContext}; the Reader of the field gives {fromReader}.");
        this.ActionChecks++;
    }

    /// <summary>
    ///  Visits the packet and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitClientPacket(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPacket.Reader clientPacket)
    {
        var clientSoupBinTcpPacketEntries = clientPacket.ClientSoupBinTcpPacket;
        var clientSoupBinTcpPacketRead = 0L;
        while (clientSoupBinTcpPacketEntries.MoveNext())
        {
            var clientSoupBinTcpPacket = clientSoupBinTcpPacketEntries.Current;
            this.Inside++;
            this.OnClientPacketHeader(in clientSoupBinTcpPacket.ClientPacketHeader);
            var clientPayload = clientSoupBinTcpPacket.ClientPayload;
            switch (clientPayload.Case)
            {
                case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Case.DebugPacket:
                {
                    this.VisitDebugPacket(clientPayload.DebugPacket);
                    break;
                }
                case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Case.LoginRequestPacket:
                {
                    this.OnLoginRequestPacket(in clientPayload.LoginRequestPacket);
                    break;
                }
                case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Case.UnsequencedDataPacket:
                {
                    var unsequencedDataPacket = clientPayload.UnsequencedDataPacket;
                    var unsequencedMessage = unsequencedDataPacket.UnsequencedMessage;
                    switch (unsequencedMessage.Case)
                    {
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.EnterOrderMessage:
                        {
                            this.OnEnterOrderMessage(unsequencedMessage.EnterOrderMessage);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.ReplaceOrderMessage:
                        {
                            this.OnReplaceOrderMessage(unsequencedMessage.ReplaceOrderMessage);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.CancelOrderMessage:
                        {
                            this.OnCancelOrderMessage(unsequencedMessage.CancelOrderMessage);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.ModifyOrderMessage:
                        {
                            this.OnModifyOrderMessage(unsequencedMessage.ModifyOrderMessage);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.AccountQueryMessage:
                        {
                            this.OnAccountQueryMessage(unsequencedMessage.AccountQueryMessage);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.MassCancelRequestMessage:
                        {
                            this.OnMassCancelRequestMessage(unsequencedMessage.MassCancelRequestMessage);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.DisableOrderEntryRequestMessage:
                        {
                            this.OnDisableOrderEntryRequestMessage(unsequencedMessage.DisableOrderEntryRequestMessage);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.EnableOrderEntryRequestMessage:
                        {
                            this.OnEnableOrderEntryRequestMessage(unsequencedMessage.EnableOrderEntryRequestMessage);
                            break;
                        }
                    }
                    this.OnUnsequencedDataPacket(unsequencedDataPacket);
                    break;
                }
                case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Case.ClientHeartbeat:
                {
                    this.OnClientHeartbeat();
                    break;
                }
                case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Case.LogoutRequest:
                {
                    this.OnLogoutRequest();
                    break;
                }
            }
            this.Inside--;
            if (clientSoupBinTcpPacketEntries.Index != ++clientSoupBinTcpPacketRead) throw new global::System.InvalidOperationException("The ClientPacket.ClientSoupBinTcpPacket entries do not number their entries in order.");
            _ = clientSoupBinTcpPacketEntries.Current;
            if (clientSoupBinTcpPacketEntries.Index != clientSoupBinTcpPacketRead) throw new global::System.InvalidOperationException("A second read of the current ClientPacket.ClientSoupBinTcpPacket entry moves the entries.");
            this.StableEntries++;
        }
        if (clientSoupBinTcpPacketEntries.MoveNext() || clientSoupBinTcpPacketEntries.Index != clientSoupBinTcpPacketRead) throw new global::System.InvalidOperationException("The ClientPacket.ClientSoupBinTcpPacket entries step after their end, or number their entries differently from the entries read.");
        this.EndedEntries++;
        this.OnClientPacket(clientPacket);
    }

    /// <summary>
    ///  Visits the packet and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitServerPacket(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPacket.Reader serverPacket)
    {
        var serverSoupBinTcpPacketEntries = serverPacket.ServerSoupBinTcpPacket;
        var serverSoupBinTcpPacketRead = 0L;
        while (serverSoupBinTcpPacketEntries.MoveNext())
        {
            var serverSoupBinTcpPacket = serverSoupBinTcpPacketEntries.Current;
            this.Inside++;
            this.OnServerPacketHeader(in serverSoupBinTcpPacket.ServerPacketHeader);
            var serverPayload = serverSoupBinTcpPacket.ServerPayload;
            switch (serverPayload.Case)
            {
                case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Case.DebugPacket:
                {
                    this.VisitDebugPacket(serverPayload.DebugPacket);
                    break;
                }
                case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Case.LoginAcceptedPacket:
                {
                    this.OnLoginAcceptedPacket(in serverPayload.LoginAcceptedPacket);
                    break;
                }
                case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Case.LoginRejectedPacket:
                {
                    this.OnLoginRejectedPacket(in serverPayload.LoginRejectedPacket);
                    break;
                }
                case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Case.SequencedDataPacket:
                {
                    var sequencedDataPacket = serverPayload.SequencedDataPacket;
                    var sequencedMessage = sequencedDataPacket.SequencedMessage;
                    switch (sequencedMessage.Case)
                    {
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.SystemEventMessage:
                        {
                            this.OnSystemEventMessage(in sequencedMessage.SystemEventMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderAcceptedMessage:
                        {
                            this.OnOrderAcceptedMessage(sequencedMessage.OrderAcceptedMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.ReplacedMessage:
                        {
                            this.OnReplacedMessage(sequencedMessage.ReplacedMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.CanceledMessage:
                        {
                            this.OnCanceledMessage(sequencedMessage.CanceledMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.AiqCanceledMessage:
                        {
                            this.OnAiqCanceledMessage(sequencedMessage.AiqCanceledMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderExecutedMessage:
                        {
                            this.OnOrderExecutedMessage(sequencedMessage.OrderExecutedMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.BrokenTradeMessage:
                        {
                            this.OnBrokenTradeMessage(sequencedMessage.BrokenTradeMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.RejectedOrderMessage:
                        {
                            this.OnRejectedOrderMessage(sequencedMessage.RejectedOrderMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.CancelPendingMessage:
                        {
                            this.OnCancelPendingMessage(sequencedMessage.CancelPendingMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.CancelRejectMessage:
                        {
                            this.OnCancelRejectMessage(sequencedMessage.CancelRejectMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderPriorityUpdateMessage:
                        {
                            this.OnOrderPriorityUpdateMessage(sequencedMessage.OrderPriorityUpdateMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderModifiedMessage:
                        {
                            this.OnOrderModifiedMessage(sequencedMessage.OrderModifiedMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderRestatedMessage:
                        {
                            this.OnOrderRestatedMessage(sequencedMessage.OrderRestatedMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.AccountQueryResponseMessage:
                        {
                            this.OnAccountQueryResponseMessage(sequencedMessage.AccountQueryResponseMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.MassCancelResponseMessage:
                        {
                            this.OnMassCancelResponseMessage(sequencedMessage.MassCancelResponseMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.DisableOrderEntryResponseMessage:
                        {
                            this.OnDisableOrderEntryResponseMessage(sequencedMessage.DisableOrderEntryResponseMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.EnableOrderEntryResponseMessage:
                        {
                            this.OnEnableOrderEntryResponseMessage(sequencedMessage.EnableOrderEntryResponseMessage, null);
                            break;
                        }
                    }
                    this.OnSequencedDataPacket(sequencedDataPacket, null);
                    break;
                }
                case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Case.ServerHeartbeat:
                {
                    this.OnServerHeartbeat();
                    break;
                }
                case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Case.EndOfSession:
                {
                    this.OnEndOfSession();
                    break;
                }
            }
            this.Inside--;
            if (serverSoupBinTcpPacketEntries.Index != ++serverSoupBinTcpPacketRead) throw new global::System.InvalidOperationException("The ServerPacket.ServerSoupBinTcpPacket entries do not number their entries in order.");
            _ = serverSoupBinTcpPacketEntries.Current;
            if (serverSoupBinTcpPacketEntries.Index != serverSoupBinTcpPacketRead) throw new global::System.InvalidOperationException("A second read of the current ServerPacket.ServerSoupBinTcpPacket entry moves the entries.");
            this.StableEntries++;
        }
        if (serverSoupBinTcpPacketEntries.MoveNext() || serverSoupBinTcpPacketEntries.Index != serverSoupBinTcpPacketRead) throw new global::System.InvalidOperationException("The ServerPacket.ServerSoupBinTcpPacket entries step after their end, or number their entries differently from the entries read.");
        this.EndedEntries++;
        this.OnServerPacket(serverPacket);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitDebugPacket(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DebugPacket.Reader debugPacket)
    {
        this.OnDebugPacket(debugPacket);
    }
    void OnClientPacketHeader(in global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPacketHeader.Layout value)
    {
        this.Parsed++;
        this.OnClientPacketHeaderCalls++;
        this.Observe(nameof(OnClientPacketHeaderCalls));
        var bytes = global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(global::System.Runtime.InteropServices.MemoryMarshal.CreateReadOnlySpan(in value, 1));
        this.Observe(bytes);
        this.Locate(bytes);
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                Dump.Print(value, Dump.Indent);
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
    }
    void OnDebugPacket(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DebugPacket.Reader value)
    {
        this.Parsed++;
        this.OnDebugPacketCalls++;
        this.Observe(nameof(OnDebugPacketCalls));
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                var reader = value;
                var indent = Dump.Indent;
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DebugPacket.Reader.DebugText), Convert.ToHexString(reader.DebugText));
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
    }
    void OnLoginRequestPacket(in global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRequestPacket.Layout value)
    {
        this.Parsed++;
        this.OnLoginRequestPacketCalls++;
        this.Observe(nameof(OnLoginRequestPacketCalls));
        var bytes = global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(global::System.Runtime.InteropServices.MemoryMarshal.CreateReadOnlySpan(in value, 1));
        this.Observe(bytes);
        this.Locate(bytes);
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                Dump.Print(value, Dump.Indent);
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
    }
    void OnUnsequencedDataPacket(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedDataPacket.Reader value)
    {
        this.Parsed++;
        this.OnUnsequencedDataPacketCalls++;
        this.Observe(nameof(OnUnsequencedDataPacketCalls));
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                var reader = value;
                var indent = Dump.Indent;
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedDataPacket.Reader.UnsequencedMessageType), reader.UnsequencedMessageType);
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
    }
    void OnClientHeartbeat()
    {
        this.Parsed++;
        this.OnClientHeartbeatCalls++;
        this.Observe(nameof(OnClientHeartbeatCalls));
    }
    void OnLogoutRequest()
    {
        this.Parsed++;
        this.OnLogoutRequestCalls++;
        this.Observe(nameof(OnLogoutRequestCalls));
    }
    void OnEnterOrderMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderMessage.Reader value)
    {
        this.Parsed++;
        this.OnEnterOrderMessageCalls++;
        this.Observe(nameof(OnEnterOrderMessageCalls));
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                var reader = value;
                var indent = Dump.Indent;
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderMessage.Reader.UserRefNum), reader.UserRefNum);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderMessage.Reader.Side), reader.Side);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderMessage.Reader.Quantity), reader.Quantity);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderMessage.Reader.Symbol), reader.Symbol);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderMessage.Reader.Price), reader.Price);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderMessage.Reader.TimeInForce), reader.TimeInForce);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderMessage.Reader.Display), reader.Display);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderMessage.Reader.Capacity), reader.Capacity);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderMessage.Reader.InterMarketSweepEligibility), reader.InterMarketSweepEligibility);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderMessage.Reader.CrossType), reader.CrossType);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderMessage.Reader.Clordid), reader.Clordid);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderMessage.Reader.AppendageLength), reader.AppendageLength);
                foreach (var entry in reader.EnterOrderAppendage)
                {
                    Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderMessage.Reader.EnterOrderAppendage));
                    Print(entry, indent + Indent);
                }
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
        {
            var entries = value.EnterOrderAppendage;
            var read = entries.Index;
            var ordered = true;
            while (entries.MoveNext()) ordered &= entries.Index == ++read;
            this.Observe(read);
            if (!ordered || entries.MoveNext()) throw new global::System.InvalidOperationException("The EnterOrderMessage.Reader.EnterOrderAppendage entries differ from their index or count.");
        }
    }
    void OnReplaceOrderMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderMessage.Reader value)
    {
        this.Parsed++;
        this.OnReplaceOrderMessageCalls++;
        this.Observe(nameof(OnReplaceOrderMessageCalls));
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                var reader = value;
                var indent = Dump.Indent;
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderMessage.Reader.OrigUserRefNum), reader.OrigUserRefNum);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderMessage.Reader.UserRefNum), reader.UserRefNum);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderMessage.Reader.Quantity), reader.Quantity);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderMessage.Reader.Price), reader.Price);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderMessage.Reader.TimeInForce), reader.TimeInForce);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderMessage.Reader.Display), reader.Display);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderMessage.Reader.InterMarketSweepEligibility), reader.InterMarketSweepEligibility);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderMessage.Reader.Clordid), reader.Clordid);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderMessage.Reader.AppendageLength), reader.AppendageLength);
                foreach (var entry in reader.ReplaceOrderAppendage)
                {
                    Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderMessage.Reader.ReplaceOrderAppendage));
                    Print(entry, indent + Indent);
                }
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
        {
            var entries = value.ReplaceOrderAppendage;
            var read = entries.Index;
            var ordered = true;
            while (entries.MoveNext()) ordered &= entries.Index == ++read;
            this.Observe(read);
            if (!ordered || entries.MoveNext()) throw new global::System.InvalidOperationException("The ReplaceOrderMessage.Reader.ReplaceOrderAppendage entries differ from their index or count.");
        }
    }
    void OnCancelOrderMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelOrderMessage.Reader value)
    {
        this.Parsed++;
        this.OnCancelOrderMessageCalls++;
        this.Observe(nameof(OnCancelOrderMessageCalls));
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                var reader = value;
                var indent = Dump.Indent;
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelOrderMessage.Reader.UserRefNum), reader.UserRefNum);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelOrderMessage.Reader.Quantity), reader.Quantity);
                if (reader.AppendageLength is { } appendageLengthValue)
                {
                    Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelOrderMessage.Reader.AppendageLength), appendageLengthValue);
                }
                foreach (var entry in reader.CancelOrderAppendage)
                {
                    Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelOrderMessage.Reader.CancelOrderAppendage));
                    Print(entry, indent + Indent);
                }
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
        {
            var entries = value.CancelOrderAppendage;
            var read = entries.Index;
            var ordered = true;
            while (entries.MoveNext()) ordered &= entries.Index == ++read;
            this.Observe(read);
            if (!ordered || entries.MoveNext()) throw new global::System.InvalidOperationException("The CancelOrderMessage.Reader.CancelOrderAppendage entries differ from their index or count.");
        }
    }
    void OnModifyOrderMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderMessage.Reader value)
    {
        this.Parsed++;
        this.OnModifyOrderMessageCalls++;
        this.Observe(nameof(OnModifyOrderMessageCalls));
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                var reader = value;
                var indent = Dump.Indent;
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderMessage.Reader.UserRefNum), reader.UserRefNum);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderMessage.Reader.Side), reader.Side);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderMessage.Reader.Quantity), reader.Quantity);
                if (reader.AppendageLength is { } appendageLengthValue)
                {
                    Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderMessage.Reader.AppendageLength), appendageLengthValue);
                }
                foreach (var entry in reader.ModifyOrderAppendage)
                {
                    Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderMessage.Reader.ModifyOrderAppendage));
                    Print(entry, indent + Indent);
                }
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
        {
            var entries = value.ModifyOrderAppendage;
            var read = entries.Index;
            var ordered = true;
            while (entries.MoveNext()) ordered &= entries.Index == ++read;
            this.Observe(read);
            if (!ordered || entries.MoveNext()) throw new global::System.InvalidOperationException("The ModifyOrderMessage.Reader.ModifyOrderAppendage entries differ from their index or count.");
        }
    }
    void OnAccountQueryMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryMessage.Reader value)
    {
        this.Parsed++;
        this.OnAccountQueryMessageCalls++;
        this.Observe(nameof(OnAccountQueryMessageCalls));
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                var reader = value;
                var indent = Dump.Indent;
                if (reader.AppendageLength is { } appendageLengthValue)
                {
                    Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryMessage.Reader.AppendageLength), appendageLengthValue);
                }
                foreach (var entry in reader.AccountQueryAppendage)
                {
                    Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryMessage.Reader.AccountQueryAppendage));
                    Print(entry, indent + Indent);
                }
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
        {
            var entries = value.AccountQueryAppendage;
            var read = entries.Index;
            var ordered = true;
            while (entries.MoveNext()) ordered &= entries.Index == ++read;
            this.Observe(read);
            if (!ordered || entries.MoveNext()) throw new global::System.InvalidOperationException("The AccountQueryMessage.Reader.AccountQueryAppendage entries differ from their index or count.");
        }
    }
    void OnMassCancelRequestMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestMessage.Reader value)
    {
        this.Parsed++;
        this.OnMassCancelRequestMessageCalls++;
        this.Observe(nameof(OnMassCancelRequestMessageCalls));
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                var reader = value;
                var indent = Dump.Indent;
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestMessage.Reader.UserRefNum), reader.UserRefNum);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestMessage.Reader.Firm), reader.Firm);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestMessage.Reader.Symbol), reader.Symbol);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestMessage.Reader.AppendageLength), reader.AppendageLength);
                foreach (var entry in reader.MassCancelRequestAppendage)
                {
                    Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestMessage.Reader.MassCancelRequestAppendage));
                    Print(entry, indent + Indent);
                }
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
        {
            var entries = value.MassCancelRequestAppendage;
            var read = entries.Index;
            var ordered = true;
            while (entries.MoveNext()) ordered &= entries.Index == ++read;
            this.Observe(read);
            if (!ordered || entries.MoveNext()) throw new global::System.InvalidOperationException("The MassCancelRequestMessage.Reader.MassCancelRequestAppendage entries differ from their index or count.");
        }
    }
    void OnDisableOrderEntryRequestMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryRequestMessage.Reader value)
    {
        this.Parsed++;
        this.OnDisableOrderEntryRequestMessageCalls++;
        this.Observe(nameof(OnDisableOrderEntryRequestMessageCalls));
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                var reader = value;
                var indent = Dump.Indent;
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryRequestMessage.Reader.UserRefNum), reader.UserRefNum);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryRequestMessage.Reader.Firm), reader.Firm);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryRequestMessage.Reader.AppendageLength), reader.AppendageLength);
                foreach (var entry in reader.DisableOrderEntryRequestAppendage)
                {
                    Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryRequestMessage.Reader.DisableOrderEntryRequestAppendage));
                    Print(entry, indent + Indent);
                }
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
        {
            var entries = value.DisableOrderEntryRequestAppendage;
            var read = entries.Index;
            var ordered = true;
            while (entries.MoveNext()) ordered &= entries.Index == ++read;
            this.Observe(read);
            if (!ordered || entries.MoveNext()) throw new global::System.InvalidOperationException("The DisableOrderEntryRequestMessage.Reader.DisableOrderEntryRequestAppendage entries differ from their index or count.");
        }
    }
    void OnEnableOrderEntryRequestMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryRequestMessage.Reader value)
    {
        this.Parsed++;
        this.OnEnableOrderEntryRequestMessageCalls++;
        this.Observe(nameof(OnEnableOrderEntryRequestMessageCalls));
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                var reader = value;
                var indent = Dump.Indent;
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryRequestMessage.Reader.UserRefNum), reader.UserRefNum);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryRequestMessage.Reader.Firm), reader.Firm);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryRequestMessage.Reader.AppendageLength), reader.AppendageLength);
                foreach (var entry in reader.EnableOrderEntryRequestAppendage)
                {
                    Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryRequestMessage.Reader.EnableOrderEntryRequestAppendage));
                    Print(entry, indent + Indent);
                }
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
        {
            var entries = value.EnableOrderEntryRequestAppendage;
            var read = entries.Index;
            var ordered = true;
            while (entries.MoveNext()) ordered &= entries.Index == ++read;
            this.Observe(read);
            if (!ordered || entries.MoveNext()) throw new global::System.InvalidOperationException("The EnableOrderEntryRequestMessage.Reader.EnableOrderEntryRequestAppendage entries differ from their index or count.");
        }
    }
    void OnServerPacketHeader(in global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPacketHeader.Layout value)
    {
        this.Parsed++;
        this.OnServerPacketHeaderCalls++;
        this.Observe(nameof(OnServerPacketHeaderCalls));
        var bytes = global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(global::System.Runtime.InteropServices.MemoryMarshal.CreateReadOnlySpan(in value, 1));
        this.Observe(bytes);
        this.Locate(bytes);
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                Dump.Print(value, Dump.Indent);
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
    }
    void OnLoginAcceptedPacket(in global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginAcceptedPacket.Layout value)
    {
        this.Parsed++;
        this.OnLoginAcceptedPacketCalls++;
        this.Observe(nameof(OnLoginAcceptedPacketCalls));
        var bytes = global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(global::System.Runtime.InteropServices.MemoryMarshal.CreateReadOnlySpan(in value, 1));
        this.Observe(bytes);
        this.Locate(bytes);
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                Dump.Print(value, Dump.Indent);
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
    }
    void OnLoginRejectedPacket(in global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRejectedPacket.Layout value)
    {
        this.Parsed++;
        this.OnLoginRejectedPacketCalls++;
        this.Observe(nameof(OnLoginRejectedPacketCalls));
        var bytes = global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(global::System.Runtime.InteropServices.MemoryMarshal.CreateReadOnlySpan(in value, 1));
        this.Observe(bytes);
        this.Locate(bytes);
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                Dump.Print(value, Dump.Indent);
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
    }
    void OnSequencedDataPacket(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedDataPacket.Reader value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnSequencedDataPacketCalls++;
        this.Observe(nameof(OnSequencedDataPacketCalls));
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                var reader = value;
                var indent = Dump.Indent;
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedDataPacket.Reader.SequencedMessageType), reader.SequencedMessageType);
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
    }
    void OnServerHeartbeat()
    {
        this.Parsed++;
        this.OnServerHeartbeatCalls++;
        this.Observe(nameof(OnServerHeartbeatCalls));
    }
    void OnEndOfSession()
    {
        this.Parsed++;
        this.OnEndOfSessionCalls++;
        this.Observe(nameof(OnEndOfSessionCalls));
    }
    void OnSystemEventMessage(in global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SystemEventMessage.Layout value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnSystemEventMessageCalls++;
        this.Observe(nameof(OnSystemEventMessageCalls));
        var bytes = global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(global::System.Runtime.InteropServices.MemoryMarshal.CreateReadOnlySpan(in value, 1));
        this.Observe(bytes);
        this.Locate(bytes);
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                Dump.Print(value, Dump.Indent);
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
    }
    void OnOrderAcceptedMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Reader value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnOrderAcceptedMessageCalls++;
        this.Observe(nameof(OnOrderAcceptedMessageCalls));
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                var reader = value;
                var indent = Dump.Indent;
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Reader.Timestamp), reader.Timestamp);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Reader.UserRefNum), reader.UserRefNum);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Reader.Side), reader.Side);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Reader.Quantity), reader.Quantity);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Reader.Symbol), reader.Symbol);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Reader.Price), reader.Price);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Reader.TimeInForce), reader.TimeInForce);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Reader.Display), reader.Display);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Reader.OrderReferenceNumber), reader.OrderReferenceNumber);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Reader.Capacity), reader.Capacity);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Reader.InterMarketSweepEligibility), reader.InterMarketSweepEligibility);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Reader.CrossType), reader.CrossType);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Reader.OrderState), reader.OrderState);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Reader.Clordid), reader.Clordid);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Reader.AppendageLength), reader.AppendageLength);
                foreach (var entry in reader.OrderAcceptedAppendage)
                {
                    Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Reader.OrderAcceptedAppendage));
                    Print(entry, indent + Indent);
                }
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
        {
            var entries = value.OrderAcceptedAppendage;
            var read = entries.Index;
            var ordered = true;
            while (entries.MoveNext()) ordered &= entries.Index == ++read;
            this.Observe(read);
            if (!ordered || entries.MoveNext()) throw new global::System.InvalidOperationException("The OrderAcceptedMessage.Reader.OrderAcceptedAppendage entries differ from their index or count.");
        }
    }
    void OnReplacedMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Reader value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnReplacedMessageCalls++;
        this.Observe(nameof(OnReplacedMessageCalls));
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                var reader = value;
                var indent = Dump.Indent;
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Reader.Timestamp), reader.Timestamp);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Reader.OrigUserRefNum), reader.OrigUserRefNum);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Reader.UserRefNum), reader.UserRefNum);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Reader.Side), reader.Side);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Reader.Quantity), reader.Quantity);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Reader.Symbol), reader.Symbol);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Reader.Price), reader.Price);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Reader.TimeInForce), reader.TimeInForce);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Reader.Display), reader.Display);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Reader.OrderReferenceNumber), reader.OrderReferenceNumber);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Reader.Capacity), reader.Capacity);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Reader.InterMarketSweepEligibility), reader.InterMarketSweepEligibility);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Reader.CrossType), reader.CrossType);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Reader.OrderState), reader.OrderState);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Reader.Clordid), reader.Clordid);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Reader.AppendageLength), reader.AppendageLength);
                foreach (var entry in reader.ReplacedMessageAppendage)
                {
                    Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Reader.ReplacedMessageAppendage));
                    Print(entry, indent + Indent);
                }
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
        {
            var entries = value.ReplacedMessageAppendage;
            var read = entries.Index;
            var ordered = true;
            while (entries.MoveNext()) ordered &= entries.Index == ++read;
            this.Observe(read);
            if (!ordered || entries.MoveNext()) throw new global::System.InvalidOperationException("The ReplacedMessage.Reader.ReplacedMessageAppendage entries differ from their index or count.");
        }
    }
    void OnCanceledMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CanceledMessage.Reader value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnCanceledMessageCalls++;
        this.Observe(nameof(OnCanceledMessageCalls));
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                var reader = value;
                var indent = Dump.Indent;
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CanceledMessage.Reader.Timestamp), reader.Timestamp);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CanceledMessage.Reader.UserRefNum), reader.UserRefNum);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CanceledMessage.Reader.Quantity), reader.Quantity);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CanceledMessage.Reader.CancelOrderReason), reader.CancelOrderReason);
                if (reader.AppendageLength is { } appendageLengthValue)
                {
                    Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CanceledMessage.Reader.AppendageLength), appendageLengthValue);
                }
                foreach (var entry in reader.CanceledMessageAppendage)
                {
                    Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CanceledMessage.Reader.CanceledMessageAppendage));
                    Print(entry, indent + Indent);
                }
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
        {
            var entries = value.CanceledMessageAppendage;
            var read = entries.Index;
            var ordered = true;
            while (entries.MoveNext()) ordered &= entries.Index == ++read;
            this.Observe(read);
            if (!ordered || entries.MoveNext()) throw new global::System.InvalidOperationException("The CanceledMessage.Reader.CanceledMessageAppendage entries differ from their index or count.");
        }
    }
    void OnAiqCanceledMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledMessage.Reader value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnAiqCanceledMessageCalls++;
        this.Observe(nameof(OnAiqCanceledMessageCalls));
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                var reader = value;
                var indent = Dump.Indent;
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledMessage.Reader.Timestamp), reader.Timestamp);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledMessage.Reader.UserRefNum), reader.UserRefNum);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledMessage.Reader.DecrementShares), reader.DecrementShares);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledMessage.Reader.OrderCancelReason), reader.OrderCancelReason);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledMessage.Reader.QuantityPreventedFromTrading), reader.QuantityPreventedFromTrading);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledMessage.Reader.ExecutionPrice), reader.ExecutionPrice);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledMessage.Reader.LiquidityFlag), reader.LiquidityFlag);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledMessage.Reader.AiqStrategy), reader.AiqStrategy);
                if (reader.AppendageLength is { } appendageLengthValue)
                {
                    Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledMessage.Reader.AppendageLength), appendageLengthValue);
                }
                foreach (var entry in reader.AiqCanceledAppendage)
                {
                    Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledMessage.Reader.AiqCanceledAppendage));
                    Print(entry, indent + Indent);
                }
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
        {
            var entries = value.AiqCanceledAppendage;
            var read = entries.Index;
            var ordered = true;
            while (entries.MoveNext()) ordered &= entries.Index == ++read;
            this.Observe(read);
            if (!ordered || entries.MoveNext()) throw new global::System.InvalidOperationException("The AiqCanceledMessage.Reader.AiqCanceledAppendage entries differ from their index or count.");
        }
    }
    void OnOrderExecutedMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedMessage.Reader value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnOrderExecutedMessageCalls++;
        this.Observe(nameof(OnOrderExecutedMessageCalls));
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                var reader = value;
                var indent = Dump.Indent;
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedMessage.Reader.Timestamp), reader.Timestamp);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedMessage.Reader.UserRefNum), reader.UserRefNum);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedMessage.Reader.Quantity), reader.Quantity);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedMessage.Reader.Price), reader.Price);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedMessage.Reader.LiquidityFlag), reader.LiquidityFlag);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedMessage.Reader.MatchNumber), reader.MatchNumber);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedMessage.Reader.AppendageLength), reader.AppendageLength);
                foreach (var entry in reader.OrderExecutedAppendage)
                {
                    Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedMessage.Reader.OrderExecutedAppendage));
                    Print(entry, indent + Indent);
                }
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
        {
            var entries = value.OrderExecutedAppendage;
            var read = entries.Index;
            var ordered = true;
            while (entries.MoveNext()) ordered &= entries.Index == ++read;
            this.Observe(read);
            if (!ordered || entries.MoveNext()) throw new global::System.InvalidOperationException("The OrderExecutedMessage.Reader.OrderExecutedAppendage entries differ from their index or count.");
        }
    }
    void OnBrokenTradeMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeMessage.Reader value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnBrokenTradeMessageCalls++;
        this.Observe(nameof(OnBrokenTradeMessageCalls));
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                var reader = value;
                var indent = Dump.Indent;
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeMessage.Reader.Timestamp), reader.Timestamp);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeMessage.Reader.UserRefNum), reader.UserRefNum);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeMessage.Reader.MatchNumber), reader.MatchNumber);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeMessage.Reader.BrokenTradeReason), reader.BrokenTradeReason);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeMessage.Reader.Clordid), reader.Clordid);
                if (reader.AppendageLength is { } appendageLengthValue)
                {
                    Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeMessage.Reader.AppendageLength), appendageLengthValue);
                }
                foreach (var entry in reader.BrokenTradeAppendage)
                {
                    Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeMessage.Reader.BrokenTradeAppendage));
                    Print(entry, indent + Indent);
                }
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
        {
            var entries = value.BrokenTradeAppendage;
            var read = entries.Index;
            var ordered = true;
            while (entries.MoveNext()) ordered &= entries.Index == ++read;
            this.Observe(read);
            if (!ordered || entries.MoveNext()) throw new global::System.InvalidOperationException("The BrokenTradeMessage.Reader.BrokenTradeAppendage entries differ from their index or count.");
        }
    }
    void OnRejectedOrderMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RejectedOrderMessage.Reader value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnRejectedOrderMessageCalls++;
        this.Observe(nameof(OnRejectedOrderMessageCalls));
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                var reader = value;
                var indent = Dump.Indent;
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RejectedOrderMessage.Reader.Timestamp), reader.Timestamp);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RejectedOrderMessage.Reader.UserRefNum), reader.UserRefNum);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RejectedOrderMessage.Reader.RejectedOrderReason), reader.RejectedOrderReason);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RejectedOrderMessage.Reader.Clordid), reader.Clordid);
                if (reader.AppendageLength is { } appendageLengthValue)
                {
                    Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RejectedOrderMessage.Reader.AppendageLength), appendageLengthValue);
                }
                foreach (var entry in reader.RejectedOrderAppendage)
                {
                    Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RejectedOrderMessage.Reader.RejectedOrderAppendage));
                    Print(entry, indent + Indent);
                }
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
        {
            var entries = value.RejectedOrderAppendage;
            var read = entries.Index;
            var ordered = true;
            while (entries.MoveNext()) ordered &= entries.Index == ++read;
            this.Observe(read);
            if (!ordered || entries.MoveNext()) throw new global::System.InvalidOperationException("The RejectedOrderMessage.Reader.RejectedOrderAppendage entries differ from their index or count.");
        }
    }
    void OnCancelPendingMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingMessage.Reader value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnCancelPendingMessageCalls++;
        this.Observe(nameof(OnCancelPendingMessageCalls));
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                var reader = value;
                var indent = Dump.Indent;
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingMessage.Reader.Timestamp), reader.Timestamp);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingMessage.Reader.UserRefNum), reader.UserRefNum);
                if (reader.AppendageLength is { } appendageLengthValue)
                {
                    Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingMessage.Reader.AppendageLength), appendageLengthValue);
                }
                foreach (var entry in reader.CancelPendingAppendage)
                {
                    Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingMessage.Reader.CancelPendingAppendage));
                    Print(entry, indent + Indent);
                }
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
        {
            var entries = value.CancelPendingAppendage;
            var read = entries.Index;
            var ordered = true;
            while (entries.MoveNext()) ordered &= entries.Index == ++read;
            this.Observe(read);
            if (!ordered || entries.MoveNext()) throw new global::System.InvalidOperationException("The CancelPendingMessage.Reader.CancelPendingAppendage entries differ from their index or count.");
        }
    }
    void OnCancelRejectMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelRejectMessage.Reader value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnCancelRejectMessageCalls++;
        this.Observe(nameof(OnCancelRejectMessageCalls));
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                var reader = value;
                var indent = Dump.Indent;
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelRejectMessage.Reader.Timestamp), reader.Timestamp);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelRejectMessage.Reader.UserRefNum), reader.UserRefNum);
                if (reader.AppendageLength is { } appendageLengthValue)
                {
                    Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelRejectMessage.Reader.AppendageLength), appendageLengthValue);
                }
                foreach (var entry in reader.CancelRejectAppendage)
                {
                    Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelRejectMessage.Reader.CancelRejectAppendage));
                    Print(entry, indent + Indent);
                }
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
        {
            var entries = value.CancelRejectAppendage;
            var read = entries.Index;
            var ordered = true;
            while (entries.MoveNext()) ordered &= entries.Index == ++read;
            this.Observe(read);
            if (!ordered || entries.MoveNext()) throw new global::System.InvalidOperationException("The CancelRejectMessage.Reader.CancelRejectAppendage entries differ from their index or count.");
        }
    }
    void OnOrderPriorityUpdateMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateMessage.Reader value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnOrderPriorityUpdateMessageCalls++;
        this.Observe(nameof(OnOrderPriorityUpdateMessageCalls));
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                var reader = value;
                var indent = Dump.Indent;
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateMessage.Reader.Timestamp), reader.Timestamp);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateMessage.Reader.UserRefNum), reader.UserRefNum);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateMessage.Reader.Price), reader.Price);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateMessage.Reader.Display), reader.Display);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateMessage.Reader.OrderReferenceNumber), reader.OrderReferenceNumber);
                if (reader.AppendageLength is { } appendageLengthValue)
                {
                    Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateMessage.Reader.AppendageLength), appendageLengthValue);
                }
                foreach (var entry in reader.OrderPriorityUpdateAppendage)
                {
                    Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateMessage.Reader.OrderPriorityUpdateAppendage));
                    Print(entry, indent + Indent);
                }
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
        {
            var entries = value.OrderPriorityUpdateAppendage;
            var read = entries.Index;
            var ordered = true;
            while (entries.MoveNext()) ordered &= entries.Index == ++read;
            this.Observe(read);
            if (!ordered || entries.MoveNext()) throw new global::System.InvalidOperationException("The OrderPriorityUpdateMessage.Reader.OrderPriorityUpdateAppendage entries differ from their index or count.");
        }
    }
    void OnOrderModifiedMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedMessage.Reader value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnOrderModifiedMessageCalls++;
        this.Observe(nameof(OnOrderModifiedMessageCalls));
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                var reader = value;
                var indent = Dump.Indent;
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedMessage.Reader.Timestamp), reader.Timestamp);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedMessage.Reader.UserRefNum), reader.UserRefNum);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedMessage.Reader.Side), reader.Side);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedMessage.Reader.Quantity), reader.Quantity);
                if (reader.AppendageLength is { } appendageLengthValue)
                {
                    Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedMessage.Reader.AppendageLength), appendageLengthValue);
                }
                foreach (var entry in reader.OrderModifiedAppendage)
                {
                    Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedMessage.Reader.OrderModifiedAppendage));
                    Print(entry, indent + Indent);
                }
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
        {
            var entries = value.OrderModifiedAppendage;
            var read = entries.Index;
            var ordered = true;
            while (entries.MoveNext()) ordered &= entries.Index == ++read;
            this.Observe(read);
            if (!ordered || entries.MoveNext()) throw new global::System.InvalidOperationException("The OrderModifiedMessage.Reader.OrderModifiedAppendage entries differ from their index or count.");
        }
    }
    void OnOrderRestatedMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedMessage.Reader value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnOrderRestatedMessageCalls++;
        this.Observe(nameof(OnOrderRestatedMessageCalls));
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                var reader = value;
                var indent = Dump.Indent;
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedMessage.Reader.Timestamp), reader.Timestamp);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedMessage.Reader.UserRefNum), reader.UserRefNum);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedMessage.Reader.OrderRestatedReason), reader.OrderRestatedReason);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedMessage.Reader.AppendageLength), reader.AppendageLength);
                foreach (var entry in reader.OrderRestatedAppendage)
                {
                    Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedMessage.Reader.OrderRestatedAppendage));
                    Print(entry, indent + Indent);
                }
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
        {
            var entries = value.OrderRestatedAppendage;
            var read = entries.Index;
            var ordered = true;
            while (entries.MoveNext()) ordered &= entries.Index == ++read;
            this.Observe(read);
            if (!ordered || entries.MoveNext()) throw new global::System.InvalidOperationException("The OrderRestatedMessage.Reader.OrderRestatedAppendage entries differ from their index or count.");
        }
    }
    void OnAccountQueryResponseMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseMessage.Reader value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnAccountQueryResponseMessageCalls++;
        this.Observe(nameof(OnAccountQueryResponseMessageCalls));
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                var reader = value;
                var indent = Dump.Indent;
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseMessage.Reader.Timestamp), reader.Timestamp);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseMessage.Reader.NextUserRefNum), reader.NextUserRefNum);
                if (reader.AppendageLength is { } appendageLengthValue)
                {
                    Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseMessage.Reader.AppendageLength), appendageLengthValue);
                }
                foreach (var entry in reader.AccountQueryResponseAppendage)
                {
                    Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseMessage.Reader.AccountQueryResponseAppendage));
                    Print(entry, indent + Indent);
                }
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
        {
            var entries = value.AccountQueryResponseAppendage;
            var read = entries.Index;
            var ordered = true;
            while (entries.MoveNext()) ordered &= entries.Index == ++read;
            this.Observe(read);
            if (!ordered || entries.MoveNext()) throw new global::System.InvalidOperationException("The AccountQueryResponseMessage.Reader.AccountQueryResponseAppendage entries differ from their index or count.");
        }
    }
    void OnMassCancelResponseMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseMessage.Reader value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnMassCancelResponseMessageCalls++;
        this.Observe(nameof(OnMassCancelResponseMessageCalls));
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                var reader = value;
                var indent = Dump.Indent;
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseMessage.Reader.Timestamp), reader.Timestamp);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseMessage.Reader.UserRefNum), reader.UserRefNum);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseMessage.Reader.Firm), reader.Firm);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseMessage.Reader.Symbol), reader.Symbol);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseMessage.Reader.AppendageLength), reader.AppendageLength);
                foreach (var entry in reader.MassCancelResponseAppendage)
                {
                    Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseMessage.Reader.MassCancelResponseAppendage));
                    Print(entry, indent + Indent);
                }
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
        {
            var entries = value.MassCancelResponseAppendage;
            var read = entries.Index;
            var ordered = true;
            while (entries.MoveNext()) ordered &= entries.Index == ++read;
            this.Observe(read);
            if (!ordered || entries.MoveNext()) throw new global::System.InvalidOperationException("The MassCancelResponseMessage.Reader.MassCancelResponseAppendage entries differ from their index or count.");
        }
    }
    void OnDisableOrderEntryResponseMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryResponseMessage.Reader value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnDisableOrderEntryResponseMessageCalls++;
        this.Observe(nameof(OnDisableOrderEntryResponseMessageCalls));
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                var reader = value;
                var indent = Dump.Indent;
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryResponseMessage.Reader.Timestamp), reader.Timestamp);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryResponseMessage.Reader.UserRefNum), reader.UserRefNum);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryResponseMessage.Reader.Firm), reader.Firm);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryResponseMessage.Reader.AppendageLength), reader.AppendageLength);
                foreach (var entry in reader.DisableOrderEntryResponseAppendage)
                {
                    Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryResponseMessage.Reader.DisableOrderEntryResponseAppendage));
                    Print(entry, indent + Indent);
                }
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
        {
            var entries = value.DisableOrderEntryResponseAppendage;
            var read = entries.Index;
            var ordered = true;
            while (entries.MoveNext()) ordered &= entries.Index == ++read;
            this.Observe(read);
            if (!ordered || entries.MoveNext()) throw new global::System.InvalidOperationException("The DisableOrderEntryResponseMessage.Reader.DisableOrderEntryResponseAppendage entries differ from their index or count.");
        }
    }
    void OnEnableOrderEntryResponseMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryResponseMessage.Reader value, global::System.UInt128? sequenceNumber)
    {
        this.Parsed++;
        this.OnEnableOrderEntryResponseMessageCalls++;
        this.Observe(nameof(OnEnableOrderEntryResponseMessageCalls));
        {
            var console = global::System.Console.Out;
            global::System.Console.SetOut(this.Printer);
            try
            {
                var reader = value;
                var indent = Dump.Indent;
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryResponseMessage.Reader.Timestamp), reader.Timestamp);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryResponseMessage.Reader.UserRefNum), reader.UserRefNum);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryResponseMessage.Reader.Firm), reader.Firm);
                Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryResponseMessage.Reader.AppendageLength), reader.AppendageLength);
                foreach (var entry in reader.EnableOrderEntryResponseAppendage)
                {
                    Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryResponseMessage.Reader.EnableOrderEntryResponseAppendage));
                    Print(entry, indent + Indent);
                }
            }
            finally
            {
                global::System.Console.SetOut(console);
            }
            this.Observe(this.Printer.ToString());
            this.Printer.GetStringBuilder().Clear();
        }
        {
            var entries = value.EnableOrderEntryResponseAppendage;
            var read = entries.Index;
            var ordered = true;
            while (entries.MoveNext()) ordered &= entries.Index == ++read;
            this.Observe(read);
            if (!ordered || entries.MoveNext()) throw new global::System.InvalidOperationException("The EnableOrderEntryResponseMessage.Reader.EnableOrderEntryResponseAppendage entries differ from their index or count.");
        }
    }
    void OnClientPacket(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPacket.Reader value)
    {
        this.Parsed++;
        this.OnClientPacketCalls++;
        this.Observe(nameof(OnClientPacketCalls));
    }
    void OnServerPacket(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPacket.Reader value)
    {
        this.Parsed++;
        this.OnServerPacketCalls++;
        this.Observe(nameof(OnServerPacketCalls));
    }
}
