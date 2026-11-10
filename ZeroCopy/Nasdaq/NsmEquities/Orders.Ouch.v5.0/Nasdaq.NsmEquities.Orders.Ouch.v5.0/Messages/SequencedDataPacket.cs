using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  The Sequenced Data Packets act as an envelope to carry the actual sequenced data messages that are transferred from the server to the client. Each Sequenced Data Packet carries one message from the higher-lever protocol
/// </summary>
public static class SequencedDataPacket
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  Gets the fixed fields over the start of the bytes without clearing or writing them, for changes in place.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the fixed fields.
        /// </exception>
        public static ref Layout Over(global::System.Span<byte> bytes)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedDataPacket.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Value identifying sequenced message type
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessageTypeField SequencedMessageType;
    }

    /// <summary>
    ///  Reads the Sequenced Data Packet in place.
    /// </summary>
    public readonly ref struct Reader
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int start;
        private readonly int end;
        private readonly ushort packetLengthValue;
        private ref readonly Layout layout => ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(this.bytes[this.start..this.end]);

        internal Reader(ReadOnlySpan<byte> bytes, int start, int end, ushort packetLengthValue)
        {
            this.bytes = bytes;
            this.start = start;
            this.end = end;
            this.packetLengthValue = packetLengthValue;
        }

        /// <summary>
        ///  Gets the bytes of the Sequenced Data Packet.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..this.end];

        /// <summary>
        ///  Gets the fixed fields at the start of the Sequenced Data Packet.
        /// </summary>
        public ref readonly Layout Layout => ref this.layout;

        /// <summary>
        ///  Value identifying sequenced message type
        /// </summary>
        public ref readonly SequencedMessageTypeField SequencedMessageType => ref this.layout.SequencedMessageType;

        /// <summary>
        ///  The sequenced (server to client) message carried by the packet, opaque bytes unless an application source dispatches it
        /// </summary>
        public SequencedMessage.Reader SequencedMessage
        {
            get
            {
                int sequencedMessageReaderPosition = Parser.Valid(Parser.SequencedDataPacketSequencedMessageStart(this.bytes, this.start, this.end, this.start, this.packetLengthValue, out var sequencedMessageTypeValue, out var packetLengthValue));
                int sequencedMessageReaderEnd = Parser.Valid(Parser.SequencedDataPacketSequencedMessageEnd(this.bytes, sequencedMessageReaderPosition, this.end, this.start, packetLengthValue));
                return new(this.bytes, sequencedMessageReaderPosition, sequencedMessageReaderEnd, sequencedMessageTypeValue);
            }
        }
    }

    /// <summary>
    ///  The Sequenced Data Packet fields to write.
    /// </summary>
    public readonly ref struct Value
    {
        /// <summary>
        ///  The sequenced (server to client) message carried by the packet, opaque bytes unless an application source dispatches it
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Value SequencedMessage { get; init; } = new();

        /// <summary>
        ///  Creates the value of the Sequenced Data Packet, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeSequencedMessageType;
    private const int packetLengthDependencyWriteDistanceFromSequencedDataPacket = -3;
    private const int sequencedMessageTypeDependencyWriteDistanceFromSequencedDataPacket = 0;
    private const int SystemEventMessageCase = 1;
    private const int OrderAcceptedMessageCase = 2;
    private const int ReplacedMessageCase = 3;
    private const int CanceledMessageCase = 4;
    private const int AiqCanceledMessageCase = 5;
    private const int OrderExecutedMessageCase = 6;
    private const int BrokenTradeMessageCase = 7;
    private const int RejectedOrderMessageCase = 8;
    private const int CancelPendingMessageCase = 9;
    private const int CancelRejectMessageCase = 10;
    private const int OrderPriorityUpdateMessageCase = 11;
    private const int OrderModifiedMessageCase = 12;
    private const int OrderRestatedMessageCase = 13;
    private const int AccountQueryResponseMessageCase = 14;
    private const int MassCancelResponseMessageCase = 15;
    private const int DisableOrderEntryResponseMessageCase = 16;
    private const int EnableOrderEntryResponseMessageCase = 17;

    /// <summary>
    ///  Writes the Sequenced Data Packet to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Sequenced Data Packet.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum, ref global::System.Runtime.CompilerServices.Unsafe.NullRef<bool>(), in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterCursor cursor, int start, scoped ref bool packetLengthDependencyWriteUsed, scoped in Value value)
    {
        int sequencedMessageWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int sequencedMessageWriterEnd = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int sequencedMessageWriterCase = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        cursor.SequencedDataPacket_SequencedMessage_EndFrozen = false;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
        cursor.SequencedDataPacket_SequencedMessageType_Used = false;
        cursor.Index += LayoutSize;
        if (sequencedMessageWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) sequencedMessageWriterStart = cursor.Index;
        int sequencedMessageWriting = cursor.Index;
        var sequencedMessageWriteValue = value.SequencedMessage;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Write(bytes, ref cursor, sequencedMessageWriting, ref packetLengthDependencyWriteUsed, ref sequencedMessageWriterCase, in sequencedMessageWriteValue);
        if (sequencedMessageWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) sequencedMessageWriterStart = cursor.Index;
        if (sequencedMessageWriterEnd == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) sequencedMessageWriterEnd = cursor.Index;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SequencedDataPacketWriter.SequencedDataPacketSizeRule0(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + packetLengthDependencyWriteDistanceFromSequencedDataPacket, ref packetLengthDependencyWriteUsed, start, cursor.Index);
        if (sequencedMessageWriterCase == SystemEventMessageCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SequencedDataPacketWriter.SequencedMessageBranchRule1(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + sequencedMessageTypeDependencyWriteDistanceFromSequencedDataPacket, sequencedMessageWriterStart);
        if (sequencedMessageWriterCase == OrderAcceptedMessageCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SequencedDataPacketWriter.SequencedMessageBranchRule2(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + sequencedMessageTypeDependencyWriteDistanceFromSequencedDataPacket, sequencedMessageWriterStart);
        if (sequencedMessageWriterCase == ReplacedMessageCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SequencedDataPacketWriter.SequencedMessageBranchRule3(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + sequencedMessageTypeDependencyWriteDistanceFromSequencedDataPacket, sequencedMessageWriterStart);
        if (sequencedMessageWriterCase == CanceledMessageCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SequencedDataPacketWriter.SequencedMessageBranchRule4(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + sequencedMessageTypeDependencyWriteDistanceFromSequencedDataPacket, sequencedMessageWriterStart);
        if (sequencedMessageWriterCase == AiqCanceledMessageCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SequencedDataPacketWriter.SequencedMessageBranchRule5(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + sequencedMessageTypeDependencyWriteDistanceFromSequencedDataPacket, sequencedMessageWriterStart);
        if (sequencedMessageWriterCase == OrderExecutedMessageCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SequencedDataPacketWriter.SequencedMessageBranchRule6(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + sequencedMessageTypeDependencyWriteDistanceFromSequencedDataPacket, sequencedMessageWriterStart);
        if (sequencedMessageWriterCase == BrokenTradeMessageCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SequencedDataPacketWriter.SequencedMessageBranchRule7(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + sequencedMessageTypeDependencyWriteDistanceFromSequencedDataPacket, sequencedMessageWriterStart);
        if (sequencedMessageWriterCase == RejectedOrderMessageCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SequencedDataPacketWriter.SequencedMessageBranchRule8(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + sequencedMessageTypeDependencyWriteDistanceFromSequencedDataPacket, sequencedMessageWriterStart);
        if (sequencedMessageWriterCase == CancelPendingMessageCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SequencedDataPacketWriter.SequencedMessageBranchRule9(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + sequencedMessageTypeDependencyWriteDistanceFromSequencedDataPacket, sequencedMessageWriterStart);
        if (sequencedMessageWriterCase == CancelRejectMessageCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SequencedDataPacketWriter.SequencedMessageBranchRule10(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + sequencedMessageTypeDependencyWriteDistanceFromSequencedDataPacket, sequencedMessageWriterStart);
        if (sequencedMessageWriterCase == OrderPriorityUpdateMessageCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SequencedDataPacketWriter.SequencedMessageBranchRule11(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + sequencedMessageTypeDependencyWriteDistanceFromSequencedDataPacket, sequencedMessageWriterStart);
        if (sequencedMessageWriterCase == OrderModifiedMessageCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SequencedDataPacketWriter.SequencedMessageBranchRule12(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + sequencedMessageTypeDependencyWriteDistanceFromSequencedDataPacket, sequencedMessageWriterStart);
        if (sequencedMessageWriterCase == OrderRestatedMessageCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SequencedDataPacketWriter.SequencedMessageBranchRule13(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + sequencedMessageTypeDependencyWriteDistanceFromSequencedDataPacket, sequencedMessageWriterStart);
        if (sequencedMessageWriterCase == AccountQueryResponseMessageCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SequencedDataPacketWriter.SequencedMessageBranchRule14(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + sequencedMessageTypeDependencyWriteDistanceFromSequencedDataPacket, sequencedMessageWriterStart);
        if (sequencedMessageWriterCase == MassCancelResponseMessageCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SequencedDataPacketWriter.SequencedMessageBranchRule15(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + sequencedMessageTypeDependencyWriteDistanceFromSequencedDataPacket, sequencedMessageWriterStart);
        if (sequencedMessageWriterCase == DisableOrderEntryResponseMessageCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SequencedDataPacketWriter.SequencedMessageBranchRule16(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + sequencedMessageTypeDependencyWriteDistanceFromSequencedDataPacket, sequencedMessageWriterStart);
        if (sequencedMessageWriterCase == EnableOrderEntryResponseMessageCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SequencedDataPacketWriter.SequencedMessageBranchRule17(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + sequencedMessageTypeDependencyWriteDistanceFromSequencedDataPacket, sequencedMessageWriterStart);
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SequencedDataPacketWriter.SequencedMessageCases(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + sequencedMessageTypeDependencyWriteDistanceFromSequencedDataPacket, sequencedMessageWriterStart, sequencedMessageWriterCase == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoCase ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : sequencedMessageWriterCase);
        if (cursor.SequencedDataPacket_SequencedMessage_EndFrozen && sequencedMessageWriterEnd != cursor.SequencedDataPacket_SequencedMessage_End) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterRuleDisagrees, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
        return bytes[start..cursor.Index];
    }
}
