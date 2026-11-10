using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Iex.IexEquities.DeepPlus.IexTp.v1_01;

/// <summary>
///  IexTp Udp Packet
/// </summary>
public static class Packet
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
            if (global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Packet.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  IexTp packet header
        /// </summary>
        public global::Iex.IexEquities.DeepPlus.IexTp.v1_01.IextpHeader.Layout IextpHeader;
    }

    /// <summary>
    ///  Reads the Packet in place.
    /// </summary>
    public readonly ref struct Reader
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int start;
        private readonly int end;
        private ref readonly Layout layout => ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(this.bytes[this.start..this.end]);

        internal Reader(ReadOnlySpan<byte> bytes, int start, int end)
        {
            this.bytes = bytes;
            this.start = start;
            this.end = end;
        }

        /// <summary>
        ///  Gets the bytes of the Packet.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..this.end];

        /// <summary>
        ///  Gets the fixed fields at the start of the Packet.
        /// </summary>
        public ref readonly Layout Layout => ref this.layout;

        /// <summary>
        ///  IexTp packet header
        /// </summary>
        public ref readonly IextpHeader.Layout IextpHeader => ref this.layout.IextpHeader;

        /// <summary>
        ///  Branch
        /// </summary>
        public Messages.Reader Messages
        {
            get
            {
                int messagesReaderPosition = Parser.Valid(Parser.PacketMessagesStart(this.bytes, this.start, this.end, this.start, out var messageCountValue, out var payloadLengthValue));
                int messagesReaderEnd = Parser.Valid(Parser.PacketMessagesEnd(this.bytes, messagesReaderPosition, this.end, this.start, payloadLengthValue));
                return new(this.bytes, messagesReaderPosition, messagesReaderEnd, messageCountValue, (ulong)this.IextpHeader.FirstMessageSequenceNumber.Decode());
            }
        }
    }

    /// <summary>
    ///  Reads one Packet from the bytes.
    /// </summary>
    /// <param name="source">
    ///  The bytes of the Packet.
    /// </param>
    /// <param name="reader">
    ///  The Reader of the Packet, which borrows the bytes.
    /// </param>
    /// <returns>
    ///  <see langword="true"/> when the bytes hold the fixed fields at the start of the Packet; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool TryRead(ReadOnlySpan<byte> source, out Reader reader)
    {
        if (LayoutSize > source.Length)
        {
            reader = default;
            return false;
        }
        reader = new Reader(source, Parser.Position.Minimum, source.Length);
        return true;
    }

    /// <summary>
    ///  The Packet fields to write.
    /// </summary>
    public readonly ref struct Value
    {
        /// <summary>
        ///  IexTp packet header
        /// </summary>
        public required global::Iex.IexEquities.DeepPlus.IexTp.v1_01.IextpHeader.Value IextpHeader { get; init; }

        /// <summary>
        ///  Branch
        /// </summary>
        public global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Messages.Value Messages { get; init; } = new();

        /// <summary>
        ///  Creates the value of the Packet, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.SizeIextpHeader;
    private const int messageCountDependencyWriteDistanceFromIextpHeader = 14;
    private const int HeartbeatCase = 1;
    private const int MessageCase = 2;

    /// <summary>
    ///  Writes the Packet to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Packet.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        int iextpHeaderWriterStart = global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.Position.Absent;
        int messagesWriterStart = global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.Position.Absent;
        int messagesWriterCase = global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.Position.Absent;
        bool messageCountDependencyFirstUsed = false;
        bool payloadLengthDependencyFirstUsed = false;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterNoRoom, global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterDestination);
        if (iextpHeaderWriterStart == global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.Position.Absent) iextpHeaderWriterStart = cursor.Index;
        int iextpHeaderWriting = cursor.Index;
        var iextpHeaderWriteValue = value.IextpHeader;
        global::Iex.IexEquities.DeepPlus.IexTp.v1_01.IextpHeader.Write(bytes, ref cursor, iextpHeaderWriting, ref iextpHeaderWriterStart == iextpHeaderWriting ? ref payloadLengthDependencyFirstUsed : ref global::System.Runtime.CompilerServices.Unsafe.NullRef<bool>(), ref iextpHeaderWriterStart == iextpHeaderWriting ? ref messageCountDependencyFirstUsed : ref global::System.Runtime.CompilerServices.Unsafe.NullRef<bool>(), in iextpHeaderWriteValue);
        if (iextpHeaderWriterStart == global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.Position.Absent) iextpHeaderWriterStart = cursor.Index;
        if (messagesWriterStart == global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.Position.Absent) messagesWriterStart = cursor.Index;
        int messagesWriting = cursor.Index;
        var messagesWriteValue = value.Messages;
        global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Messages.Write(bytes, ref cursor, messagesWriting, ref messageCountDependencyFirstUsed, ref payloadLengthDependencyFirstUsed, ref messagesWriterCase, in messagesWriteValue);
        if (iextpHeaderWriterStart == global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.Position.Absent) iextpHeaderWriterStart = cursor.Index;
        if (messagesWriterStart == global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.Position.Absent) messagesWriterStart = cursor.Index;
        if (messagesWriterCase == HeartbeatCase) global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.PacketWriter.MessagesBranchRule1(bytes, ref cursor, iextpHeaderWriterStart < global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.Position.Minimum ? global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.Position.Absent : iextpHeaderWriterStart + messageCountDependencyWriteDistanceFromIextpHeader, ref messageCountDependencyFirstUsed, messagesWriterStart);
        global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.PacketWriter.MessagesCases(bytes, ref cursor, iextpHeaderWriterStart < global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.Position.Minimum ? global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.Position.Absent : iextpHeaderWriterStart + messageCountDependencyWriteDistanceFromIextpHeader, ref messageCountDependencyFirstUsed, messagesWriterStart, messagesWriterCase == global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.Position.NoCase ? global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.Position.Absent : messagesWriterCase);
        return bytes[start..cursor.Index];
    }
}
