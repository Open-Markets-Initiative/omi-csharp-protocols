using System;
using System.Buffers.Binary;
namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0.Iterator;
/// <summary>
///  Reads the Server Packet of each TCP flow in a capture.
/// </summary>
public sealed class ServerPacketCapture
{
    private const global::Omi.Pcap.TcpSide RequiredSide = global::Omi.Pcap.TcpSide.Server;
    private readonly global::Omi.Pcap.TcpReassembler reassembler = new();
    private readonly global::System.Collections.Generic.Dictionary<global::Omi.Pcap.TcpFlow, Flow?> flows = new();
    private readonly global::System.Collections.Generic.Dictionary<global::Omi.Pcap.TcpFlow, Held> heldFlows = new();
    private readonly global::Omi.Pcap.TcpSide capturedSide;
    private Flow? last;
    private int incompleteFlows;
    private int invalidFlows;
    private int unreadFlows;
    private int heldFramesRead;
    /// <summary>
    ///  Gets the flow of the last frame.
    /// </summary>
    public global::Omi.Pcap.TcpFlow? CurrentFlow { get; private set; }
    /// <summary>
    ///  Gets a value that indicates whether the last frame belongs to a flow that this capture reads.
    /// </summary>
    public bool FrameRead { get; private set; }
    private const long NothingConsumed = 0;
    /// <summary>
    ///  Gets the parser offset in the flow of the last frame, or -1 after invalid message bytes.
    /// </summary>
    public long Consumed => this.last?.Consumed ?? NothingConsumed;
    /// <summary>
    ///  Gets the flows with invalid message bytes.
    /// </summary>
    public int InvalidFlows
    {
        get
        {
            int count = this.invalidFlows;
            foreach (var flow in this.flows.Values) if (flow is { Invalid: true }) count++;
            return count;
        }
    }
    /// <summary>
    ///  Gets the flows excluded by the selected connection role, or whose connection role their bytes do not show.
    /// </summary>
    public int UnreadFlows => this.unreadFlows + this.heldFlows.Count;
    /// <summary>
    ///  Gets the number of frames that waited for the side of their flow and that this capture then read.
    /// </summary>
    public int HeldFramesRead => this.heldFramesRead;
    /// <summary>
    ///  Gets the flows with missing captured bytes or incomplete messages.
    /// </summary>
    public int IncompleteFlows
    {
        get
        {
            int count = this.incompleteFlows;
            foreach (var item in this.flows) if (item.Value is { Invalid: false } flow && (item.Key.Waiting || item.Key.Lost || flow.Incomplete)) count++;
            return count;
        }
    }
    /// <summary>
    ///  Gets the bytes retained for incomplete messages.
    /// </summary>
    public long PendingBytes
    {
        get
        {
            long count = 0;
            foreach (var flow in this.flows.Values) if (flow is not null) count += flow.PendingBytes;
            foreach (var held in this.heldFlows.Values) count += held.Bytes.WrittenCount;
            return count;
        }
    }
    /// <summary>
    ///  Selects the sender side for flows whose capture has no connection handshake.
    /// </summary>
    /// <remarks>
    ///  With Unknown, the sender side is the side from which exactly one packet parser opens an entry without failure.
    /// </remarks>
    /// <param name="capturedSide">The sender side of flows with no captured handshake, or Unknown to detect it from the bytes.</param>
    public ServerPacketCapture(global::Omi.Pcap.TcpSide capturedSide = global::Omi.Pcap.TcpSide.Unknown)
    {
        if (!global::System.Enum.IsDefined(capturedSide)) throw new global::System.ArgumentOutOfRangeException(nameof(capturedSide));
        this.capturedSide = capturedSide;
    }
    /// <summary>
    ///  Gives the newly available bytes of the flow of one captured TCP packet.
    /// </summary>
    /// <remarks>
    ///  The bytes stay valid until the next frame. A frame that waits for the side of its flow gives none. HeldFramesRead counts it once this capture reads it. FrameRead tells whether the frame belongs to a flow that this capture reads.
    /// </remarks>
    /// <returns>
    ///  <see langword="true"/> when the frame gives new, valid bytes of a flow that this capture reads; otherwise, <see langword="false"/>.
    /// </returns>
    public bool TryGetSegment(ReadOnlySpan<byte> frame, global::Omi.Pcap.LinkType linkType, out ReadOnlySpan<byte> bytes)
    {
        this.FrameRead = false;
        ReleaseEnded();
        var tcp = this.reassembler.Add(frame, linkType, out bytes);
        if (tcp is null) return false;
        if (!this.flows.TryGetValue(tcp, out var flow))
        {
            var side = tcp.Side == global::Omi.Pcap.TcpSide.Unknown ? this.capturedSide : tcp.Side;
            if (RequiredSide != global::Omi.Pcap.TcpSide.Unknown && side == global::Omi.Pcap.TcpSide.Unknown)
            {
                if (bytes.IsEmpty) return false;
                if (!this.heldFlows.TryGetValue(tcp, out var held)) this.heldFlows.Add(tcp, held = new());
                global::System.Buffers.BuffersExtensions.Write(held.Bytes, bytes);
                if (!FlowSide.Infer(held.Bytes.WrittenSpan, out side))
                {
                    held.Frames++;
                    return false;
                }
                this.heldFlows.Remove(tcp);
                bytes = held.Bytes.WrittenSpan;
                if (side == RequiredSide) this.heldFramesRead += held.Frames;
            }
            flow = RequiredSide == global::Omi.Pcap.TcpSide.Unknown || side == RequiredSide ? new Flow() : null;
            if (flow is null) this.unreadFlows++;
            this.flows.Add(tcp, flow);
        }
        this.CurrentFlow = tcp;
        if (flow is null) return false;
        this.last = flow;
        this.FrameRead = true;
        return !flow.Invalid && !bytes.IsEmpty;
    }
    /// <summary>
    ///  Reads the messages in the newly available bytes of one captured TCP packet.
    /// </summary>
    /// <remarks>
    ///  The packets borrow the bytes of the capture until its next frame. FrameRead tells whether the frame belongs to a flow that this capture reads.
    /// </remarks>
    /// <returns>
    ///  <see langword="true"/> when the capture read new bytes of a flow and the flow is valid; otherwise, <see langword="false"/>.
    /// </returns>
    public bool TryRead(ReadOnlySpan<byte> frame, global::Omi.Pcap.LinkType linkType, out global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Iterator.ServerPacketStream.Frames reading)
    {
        reading = default;
        if (!TryGetSegment(frame, linkType, out var bytes)) return false;
        var flow = this.last!;
        reading = flow.Stream.Receive(bytes);
        return !flow.Invalid;
    }
    /// <summary>
    ///  Gives bytes of the flow of the last frame to the stream of that flow.
    /// </summary>
    /// <param name="segment">
    ///  Bytes of the flow, in the order that the flow gives them.
    /// </param>
    /// <returns>
    ///  The packets of the whole frames that the bytes complete.
    /// </returns>
    /// <exception cref="global::System.InvalidOperationException">
    ///  No frame of a flow that this capture reads came before.
    /// </exception>
    public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Iterator.ServerPacketStream.Frames Receive(ReadOnlySpan<byte> segment) => (this.last ?? throw new global::System.InvalidOperationException("No frame of a flow that this capture reads came before.")).Stream.Receive(segment);
    internal static FlowSide.Outcome Attempt(ReadOnlySpan<byte> bytes)
    {
        var flow = new Flow();
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Iterator.ServerPacketStream.Frames reading;
        var recognized = false;
        try
        {
            reading = flow.Stream.Receive(bytes);
            foreach (var root in reading)
            {
                if (Recognizes(root))
                {
                    recognized = true;
                    break;
                }
            }
        }
        catch (global::System.ArgumentOutOfRangeException)
        {
            return FlowSide.Outcome.Failed;
        }
        return flow.Invalid ? FlowSide.Outcome.Failed : recognized ? FlowSide.Outcome.Recognized : FlowSide.Outcome.Waiting;
    }
    private static bool Recognizes(ServerPacket.Reader reader)
    {
        foreach (var entry in reader.ServerSoupBinTcpPacket)
        {
            if (Recognizes(entry)) return true;
        }
        return false;
    }
    private static bool Recognizes(ServerSoupBinTcpPacket.Reader reader)
    {
        if (Recognizes(reader.ServerPayload)) return true;
        return false;
    }
    private static bool Recognizes(ServerPayload.Reader reader)
    {
        if (reader.Case != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Case.None) return true;
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Case.SequencedDataPacket && Recognizes(reader.SequencedDataPacket)) return true;
        return false;
    }
    private static bool Recognizes(SequencedDataPacket.Reader reader)
    {
        if (Recognizes(reader.SequencedMessage)) return true;
        return false;
    }
    private static bool Recognizes(SequencedMessage.Reader reader)
    {
        if (reader.Case != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.None) return true;
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderAcceptedMessage && Recognizes(reader.OrderAcceptedMessage)) return true;
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.ReplacedMessage && Recognizes(reader.ReplacedMessage)) return true;
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.CanceledMessage && Recognizes(reader.CanceledMessage)) return true;
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.AiqCanceledMessage && Recognizes(reader.AiqCanceledMessage)) return true;
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderExecutedMessage && Recognizes(reader.OrderExecutedMessage)) return true;
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.BrokenTradeMessage && Recognizes(reader.BrokenTradeMessage)) return true;
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.RejectedOrderMessage && Recognizes(reader.RejectedOrderMessage)) return true;
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.CancelPendingMessage && Recognizes(reader.CancelPendingMessage)) return true;
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.CancelRejectMessage && Recognizes(reader.CancelRejectMessage)) return true;
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderPriorityUpdateMessage && Recognizes(reader.OrderPriorityUpdateMessage)) return true;
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderModifiedMessage && Recognizes(reader.OrderModifiedMessage)) return true;
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderRestatedMessage && Recognizes(reader.OrderRestatedMessage)) return true;
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.AccountQueryResponseMessage && Recognizes(reader.AccountQueryResponseMessage)) return true;
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.MassCancelResponseMessage && Recognizes(reader.MassCancelResponseMessage)) return true;
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.DisableOrderEntryResponseMessage && Recognizes(reader.DisableOrderEntryResponseMessage)) return true;
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.EnableOrderEntryResponseMessage && Recognizes(reader.EnableOrderEntryResponseMessage)) return true;
        return false;
    }
    private static bool Recognizes(OrderAcceptedMessage.Reader reader)
    {
        foreach (var entry in reader.OrderAcceptedAppendage)
        {
            if (Recognizes(entry)) return true;
        }
        return false;
    }
    private static bool Recognizes(OrderAcceptedAppendage.Reader reader)
    {
        if (Recognizes(reader.OrderAcceptedOptionalValue)) return true;
        return false;
    }
    private static bool Recognizes(OrderAcceptedOptionalValue.Reader reader)
    {
        if (reader.Case != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.None) return true;
        return false;
    }
    private static bool Recognizes(ReplacedMessage.Reader reader)
    {
        foreach (var entry in reader.ReplacedMessageAppendage)
        {
            if (Recognizes(entry)) return true;
        }
        return false;
    }
    private static bool Recognizes(ReplacedMessageAppendage.Reader reader)
    {
        if (Recognizes(reader.ReplacedMessageOptionalValue)) return true;
        return false;
    }
    private static bool Recognizes(ReplacedMessageOptionalValue.Reader reader)
    {
        if (reader.Case != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.None) return true;
        return false;
    }
    private static bool Recognizes(CanceledMessage.Reader reader)
    {
        foreach (var entry in reader.CanceledMessageAppendage)
        {
            if (Recognizes(entry)) return true;
        }
        return false;
    }
    private static bool Recognizes(CanceledMessageAppendage.Reader reader)
    {
        if (Recognizes(reader.CanceledMessageOptionalValue)) return true;
        return false;
    }
    private static bool Recognizes(CanceledMessageOptionalValue.Reader reader)
    {
        if (reader.Case != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CanceledMessageOptionalValue.Case.None) return true;
        return false;
    }
    private static bool Recognizes(AiqCanceledMessage.Reader reader)
    {
        foreach (var entry in reader.AiqCanceledAppendage)
        {
            if (Recognizes(entry)) return true;
        }
        return false;
    }
    private static bool Recognizes(AiqCanceledAppendage.Reader reader)
    {
        if (Recognizes(reader.AiqCanceledOptionalValue)) return true;
        return false;
    }
    private static bool Recognizes(AiqCanceledOptionalValue.Reader reader)
    {
        if (reader.Case != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledOptionalValue.Case.None) return true;
        return false;
    }
    private static bool Recognizes(OrderExecutedMessage.Reader reader)
    {
        foreach (var entry in reader.OrderExecutedAppendage)
        {
            if (Recognizes(entry)) return true;
        }
        return false;
    }
    private static bool Recognizes(OrderExecutedAppendage.Reader reader)
    {
        if (Recognizes(reader.OrderExecutedOptionalValue)) return true;
        return false;
    }
    private static bool Recognizes(OrderExecutedOptionalValue.Reader reader)
    {
        if (reader.Case != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedOptionalValue.Case.None) return true;
        return false;
    }
    private static bool Recognizes(BrokenTradeMessage.Reader reader)
    {
        foreach (var entry in reader.BrokenTradeAppendage)
        {
            if (Recognizes(entry)) return true;
        }
        return false;
    }
    private static bool Recognizes(BrokenTradeAppendage.Reader reader)
    {
        if (Recognizes(reader.BrokenTradeOptionalValue)) return true;
        return false;
    }
    private static bool Recognizes(BrokenTradeOptionalValue.Reader reader)
    {
        if (reader.Case != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeOptionalValue.Case.None) return true;
        return false;
    }
    private static bool Recognizes(RejectedOrderMessage.Reader reader)
    {
        foreach (var entry in reader.RejectedOrderAppendage)
        {
            if (Recognizes(entry)) return true;
        }
        return false;
    }
    private static bool Recognizes(RejectedOrderAppendage.Reader reader)
    {
        if (Recognizes(reader.RejectedOrderOptionalValue)) return true;
        return false;
    }
    private static bool Recognizes(RejectedOrderOptionalValue.Reader reader)
    {
        if (reader.Case != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RejectedOrderOptionalValue.Case.None) return true;
        return false;
    }
    private static bool Recognizes(CancelPendingMessage.Reader reader)
    {
        foreach (var entry in reader.CancelPendingAppendage)
        {
            if (Recognizes(entry)) return true;
        }
        return false;
    }
    private static bool Recognizes(CancelPendingAppendage.Reader reader)
    {
        if (Recognizes(reader.CancelPendingOptionalValue)) return true;
        return false;
    }
    private static bool Recognizes(CancelPendingOptionalValue.Reader reader)
    {
        if (reader.Case != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingOptionalValue.Case.None) return true;
        return false;
    }
    private static bool Recognizes(CancelRejectMessage.Reader reader)
    {
        foreach (var entry in reader.CancelRejectAppendage)
        {
            if (Recognizes(entry)) return true;
        }
        return false;
    }
    private static bool Recognizes(CancelRejectAppendage.Reader reader)
    {
        if (Recognizes(reader.CancelRejectOptionalValue)) return true;
        return false;
    }
    private static bool Recognizes(CancelRejectOptionalValue.Reader reader)
    {
        if (reader.Case != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelRejectOptionalValue.Case.None) return true;
        return false;
    }
    private static bool Recognizes(OrderPriorityUpdateMessage.Reader reader)
    {
        foreach (var entry in reader.OrderPriorityUpdateAppendage)
        {
            if (Recognizes(entry)) return true;
        }
        return false;
    }
    private static bool Recognizes(OrderPriorityUpdateAppendage.Reader reader)
    {
        if (Recognizes(reader.OrderPriorityUpdateOptionalValue)) return true;
        return false;
    }
    private static bool Recognizes(OrderPriorityUpdateOptionalValue.Reader reader)
    {
        if (reader.Case != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateOptionalValue.Case.None) return true;
        return false;
    }
    private static bool Recognizes(OrderModifiedMessage.Reader reader)
    {
        foreach (var entry in reader.OrderModifiedAppendage)
        {
            if (Recognizes(entry)) return true;
        }
        return false;
    }
    private static bool Recognizes(OrderModifiedAppendage.Reader reader)
    {
        if (Recognizes(reader.OrderModifiedOptionalValue)) return true;
        return false;
    }
    private static bool Recognizes(OrderModifiedOptionalValue.Reader reader)
    {
        if (reader.Case != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalValue.Case.None) return true;
        return false;
    }
    private static bool Recognizes(OrderRestatedMessage.Reader reader)
    {
        foreach (var entry in reader.OrderRestatedAppendage)
        {
            if (Recognizes(entry)) return true;
        }
        return false;
    }
    private static bool Recognizes(OrderRestatedAppendage.Reader reader)
    {
        if (Recognizes(reader.OrderRestatedOptionalValue)) return true;
        return false;
    }
    private static bool Recognizes(OrderRestatedOptionalValue.Reader reader)
    {
        if (reader.Case != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Case.None) return true;
        return false;
    }
    private static bool Recognizes(AccountQueryResponseMessage.Reader reader)
    {
        foreach (var entry in reader.AccountQueryResponseAppendage)
        {
            if (Recognizes(entry)) return true;
        }
        return false;
    }
    private static bool Recognizes(AccountQueryResponseAppendage.Reader reader)
    {
        if (Recognizes(reader.AccountQueryResponseOptionalValue)) return true;
        return false;
    }
    private static bool Recognizes(AccountQueryResponseOptionalValue.Reader reader)
    {
        if (reader.Case != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseOptionalValue.Case.None) return true;
        return false;
    }
    private static bool Recognizes(MassCancelResponseMessage.Reader reader)
    {
        foreach (var entry in reader.MassCancelResponseAppendage)
        {
            if (Recognizes(entry)) return true;
        }
        return false;
    }
    private static bool Recognizes(MassCancelResponseAppendage.Reader reader)
    {
        if (Recognizes(reader.MassCancelResponseOptionalValue)) return true;
        return false;
    }
    private static bool Recognizes(MassCancelResponseOptionalValue.Reader reader)
    {
        if (reader.Case != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseOptionalValue.Case.None) return true;
        return false;
    }
    private static bool Recognizes(DisableOrderEntryResponseMessage.Reader reader)
    {
        foreach (var entry in reader.DisableOrderEntryResponseAppendage)
        {
            if (Recognizes(entry)) return true;
        }
        return false;
    }
    private static bool Recognizes(DisableOrderEntryResponseAppendage.Reader reader)
    {
        if (Recognizes(reader.DisableOrderEntryResponseOptionalValue)) return true;
        return false;
    }
    private static bool Recognizes(DisableOrderEntryResponseOptionalValue.Reader reader)
    {
        if (reader.Case != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryResponseOptionalValue.Case.None) return true;
        return false;
    }
    private static bool Recognizes(EnableOrderEntryResponseMessage.Reader reader)
    {
        foreach (var entry in reader.EnableOrderEntryResponseAppendage)
        {
            if (Recognizes(entry)) return true;
        }
        return false;
    }
    private static bool Recognizes(EnableOrderEntryResponseAppendage.Reader reader)
    {
        if (Recognizes(reader.EnableOrderEntryResponseOptionalValue)) return true;
        return false;
    }
    private static bool Recognizes(EnableOrderEntryResponseOptionalValue.Reader reader)
    {
        if (reader.Case != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryResponseOptionalValue.Case.None) return true;
        return false;
    }
    private void ReleaseEnded()
    {
        foreach (var item in this.flows)
        {
            if (!item.Key.Ended) continue;
            if (item.Value is { Invalid: true }) this.invalidFlows++;
            else if (item.Value is { } flow && (item.Key.Lost || flow.Incomplete)) this.incompleteFlows++;
            this.flows.Remove(item.Key);
        }
        foreach (var item in this.heldFlows)
        {
            if (!item.Key.Ended) continue;
            this.unreadFlows++;
            this.heldFlows.Remove(item.Key);
        }
    }
    private sealed class Flow
    {
        internal readonly ServerPacketStream Stream = new();
        internal bool Invalid => this.Stream.Invalid;
        internal bool Incomplete => this.Stream.NeededBytes > 0;
        internal int PendingBytes => this.Stream.PendingBytes;
        internal long Consumed => this.Stream.Consumed;
    }
    private sealed class Held
    {
        internal readonly global::System.Buffers.ArrayBufferWriter<byte> Bytes = new();
        internal int Frames;
    }
}
