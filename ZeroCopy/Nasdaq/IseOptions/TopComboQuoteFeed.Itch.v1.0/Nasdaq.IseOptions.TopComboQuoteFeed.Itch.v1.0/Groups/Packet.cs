using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0;

/// <summary>
///  Itch Mold Udp 64 Packet
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
            if (global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Packet.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Itch Mold Udp 64 Packet Header
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.PacketHeader.Layout PacketHeader;
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
        ///  Itch Mold Udp 64 Packet Header
        /// </summary>
        public ref readonly PacketHeader.Layout PacketHeader => ref this.layout.PacketHeader;

        /// <summary>
        ///  Branch
        /// </summary>
        public Messages.Reader Messages
        {
            get
            {
                int messagesReaderPosition = Parser.Valid(Parser.PacketMessagesStart(this.bytes, this.start, this.end, this.start, out var messageCountValue));
                return new(this.bytes, messagesReaderPosition, this.end, messageCountValue, (ulong)this.PacketHeader.SequenceNumber.Decode());
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
        ///  Itch Mold Udp 64 Packet Header
        /// </summary>
        public required global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.PacketHeader.Value PacketHeader { get; init; }

        /// <summary>
        ///  Branch
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Messages.Value Messages { get; init; } = new();

        /// <summary>
        ///  Creates the value of the Packet, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizePacketHeader;
    private const int messageCountDependencyWriteDistanceFromPacketHeader = 18;
    private const int HeartbeatCase = 1;
    private const int EndOfSessionCase = 2;
    private const int MessageCase = 3;

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
        var cursor = new global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        int packetHeaderWriterStart = global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.Position.Absent;
        int messagesWriterStart = global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.Position.Absent;
        int messagesWriterCase = global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.Position.Absent;
        bool messageCountDependencyFirstUsed = false;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterNoRoom, global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterDestination);
        if (packetHeaderWriterStart == global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.Position.Absent) packetHeaderWriterStart = cursor.Index;
        int packetHeaderWriting = cursor.Index;
        var packetHeaderWriteValue = value.PacketHeader;
        global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.PacketHeader.Write(bytes, ref cursor, packetHeaderWriting, ref packetHeaderWriterStart == packetHeaderWriting ? ref messageCountDependencyFirstUsed : ref global::System.Runtime.CompilerServices.Unsafe.NullRef<bool>(), in packetHeaderWriteValue);
        if (packetHeaderWriterStart == global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.Position.Absent) packetHeaderWriterStart = cursor.Index;
        if (messagesWriterStart == global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.Position.Absent) messagesWriterStart = cursor.Index;
        int messagesWriting = cursor.Index;
        var messagesWriteValue = value.Messages;
        global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Messages.Write(bytes, ref cursor, messagesWriting, ref messageCountDependencyFirstUsed, ref messagesWriterCase, in messagesWriteValue);
        if (packetHeaderWriterStart == global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.Position.Absent) packetHeaderWriterStart = cursor.Index;
        if (messagesWriterStart == global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.Position.Absent) messagesWriterStart = cursor.Index;
        if (messagesWriterCase == HeartbeatCase) global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.PacketWriter.MessagesBranchRule0(bytes, ref cursor, packetHeaderWriterStart < global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.Position.Minimum ? global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.Position.Absent : packetHeaderWriterStart + messageCountDependencyWriteDistanceFromPacketHeader, ref messageCountDependencyFirstUsed, messagesWriterStart);
        if (messagesWriterCase == EndOfSessionCase) global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.PacketWriter.MessagesBranchRule1(bytes, ref cursor, packetHeaderWriterStart < global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.Position.Minimum ? global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.Position.Absent : packetHeaderWriterStart + messageCountDependencyWriteDistanceFromPacketHeader, ref messageCountDependencyFirstUsed, messagesWriterStart);
        global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.PacketWriter.MessagesCases(bytes, ref cursor, packetHeaderWriterStart < global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.Position.Minimum ? global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.Position.Absent : packetHeaderWriterStart + messageCountDependencyWriteDistanceFromPacketHeader, ref messageCountDependencyFirstUsed, messagesWriterStart, messagesWriterCase == global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.Position.NoCase ? global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.Position.Absent : messagesWriterCase);
        return bytes[start..cursor.Index];
    }
}
