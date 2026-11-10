using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  SoupBinTCP Packet sent by the client
/// </summary>
public static class ClientPacket
{
    /// <summary>
    ///  Reads the Client Packet in place.
    /// </summary>
    public readonly ref struct Reader
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int start;
        private readonly int end;

        internal Reader(ReadOnlySpan<byte> bytes, int start, int end)
        {
            this.bytes = bytes;
            this.start = start;
            this.end = end;
        }

        /// <summary>
        ///  Gets the bytes of the Client Packet.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..this.end];

        /// <summary>
        ///  Soup Bin Tcp Packet sent by the client
        /// </summary>
        public ClientPacket.ClientSoupBinTcpPacketEntries ClientSoupBinTcpPacket
        {
            get
            {
                int clientSoupBinTcpPacketReaderPosition = Parser.Valid(Parser.ClientPacketClientSoupBinTcpPacketStart(this.bytes, this.start, this.end, this.start, out var clientSoupBinTcpPacketCursorLimit, out var clientSoupBinTcpPacketCursorClientSoupBinTcpPacketPayloadRule0End));
                return new(this.bytes, clientSoupBinTcpPacketReaderPosition, clientSoupBinTcpPacketCursorLimit, clientSoupBinTcpPacketCursorClientSoupBinTcpPacketPayloadRule0End);
            }
        }
    }

    /// <summary>
    ///  Reads one Client Packet from the bytes.
    /// </summary>
    /// <param name="source">
    ///  The bytes of the Client Packet.
    /// </param>
    /// <param name="reader">
    ///  The Reader of the Client Packet, which borrows the bytes.
    /// </param>
    /// <returns>
    ///  <see langword="true"/> when the bytes hold the fixed fields at the start of the Client Packet; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool TryRead(ReadOnlySpan<byte> source, out Reader reader)
    {
        reader = new Reader(source, Parser.Position.Minimum, source.Length);
        return true;
    }

    /// <summary>
    ///  Gets the length of the Client Packet frame at the start of the bytes.
    /// </summary>
    /// <param name="source">
    ///  The bytes, from the start of the frame.
    /// </param>
    /// <param name="length">
    ///  The number of bytes of the frame, which can be more than the bytes hold.
    /// </param>
    /// <returns>
    ///  <see langword="true"/> when the bytes hold the fields that state the length; otherwise, <see langword="false"/>.
    /// </returns>
    /// <exception cref="global::System.ArgumentOutOfRangeException">
    ///  The bytes state a frame length that is not valid.
    /// </exception>
    public static bool TryGetFrameLength(ReadOnlySpan<byte> source, out int length)
    {
        int available = Parser.ClientSoupBinTcpPacketReassembleRule1BytesRemaining(source, Parser.Position.Minimum, source.Length, out length);
        if (length < Parser.Position.Minimum || available >= Parser.Position.Minimum && length == Parser.Position.Minimum) throw new global::System.ArgumentOutOfRangeException(nameof(source), Parser.FrameLength);
        return length > Parser.Position.Minimum;
    }

    /// <summary>
    ///  Steps through the Client Soup Bin Tcp Packet entries in wire order.
    /// </summary>
    /// <remarks>
    ///  A copy of the entries steps on its own. Each step measures its entry from the bytes.
    /// </remarks>
    public ref struct ClientSoupBinTcpPacketEntries
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int limit;
        private int position;
        private long index;
        private int currentStart;
        private int currentEnd;
        private readonly int clientSoupBinTcpPacketCursorClientSoupBinTcpPacketPayloadRule0End;

        internal ClientSoupBinTcpPacketEntries(ReadOnlySpan<byte> bytes, int start, int limit, int clientSoupBinTcpPacketCursorClientSoupBinTcpPacketPayloadRule0End)
        {
            this.bytes = bytes;
            this.limit = limit;
            this.position = start;
            this.clientSoupBinTcpPacketCursorClientSoupBinTcpPacketPayloadRule0End = clientSoupBinTcpPacketCursorClientSoupBinTcpPacketPayloadRule0End;
        }
        private readonly void Entry()
        {
            if (this.index == Parser.Position.NoEntries) throw new global::System.InvalidOperationException(Parser.NoEntry);
        }
        /// <summary>
        ///  Gets the enumerator over the entries.
        /// </summary>
        public readonly ClientSoupBinTcpPacketEntries GetEnumerator() => this;

        /// <summary>
        ///  Moves to the next entry.
        /// </summary>
        /// <returns>
        ///  <see langword="true"/> when there is another entry; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="global::System.ArgumentOutOfRangeException">
        ///  The bytes end before the length that they state.
        /// </exception>
        public bool MoveNext()
        {
            int end = Parser.StepClientPacketClientSoupBinTcpPacket(this.bytes, this.position, this.limit, this.clientSoupBinTcpPacketCursorClientSoupBinTcpPacketPayloadRule0End);
            if (end == Parser.Position.Ended) return false;
            this.currentStart = this.position;
            this.currentEnd = Parser.Valid(end);
            this.position = this.currentEnd;
            this.index++;
            return true;
        }

        /// <summary>
        ///  Gets the one-based number of the current entry, or zero before the first entry.
        /// </summary>
        public readonly long Index => this.index;

        /// <summary>
        ///  Gets the Reader of the current entry.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The entries have no current entry.
        /// </exception>
        public readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientSoupBinTcpPacket.Reader Current
        {
            get
            {
                Entry();
                return new(this.bytes, this.currentStart, this.currentEnd);
            }
        }
    }

    /// <summary>
    ///  The Client Packet fields to write.
    /// </summary>
    public readonly ref struct Value
    {
        /// <summary>
        ///  Soup Bin Tcp Packet sent by the client
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Entries<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientSoupBinTcpPacket.Value> ClientSoupBinTcpPacket { get; init; }

        /// <summary>
        ///  Creates the value of the Client Packet, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    /// <summary>
    ///  Writes the Client Packet to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Client Packet.
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
        return Write(destination, ref cursor, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        int clientSoupBinTcpPacketWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int clientSoupBinTcpPacketWriterEnd = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int clientSoupBinTcpPacketWriterCount = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries;
        for (int clientSoupBinTcpPacketIndex = 0; clientSoupBinTcpPacketIndex < value.ClientSoupBinTcpPacket.Count; clientSoupBinTcpPacketIndex++)
        {
            var clientSoupBinTcpPacketCurrent = value.ClientSoupBinTcpPacket[clientSoupBinTcpPacketIndex];
            if (clientSoupBinTcpPacketWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) clientSoupBinTcpPacketWriterStart = cursor.Index;
            clientSoupBinTcpPacketWriterCount++;
            int clientSoupBinTcpPacketWriting = cursor.Index;
            var clientSoupBinTcpPacketWriteValue = clientSoupBinTcpPacketCurrent;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientSoupBinTcpPacket.Write(bytes, ref cursor, clientSoupBinTcpPacketWriting, in clientSoupBinTcpPacketWriteValue);
        }
        if (clientSoupBinTcpPacketWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) clientSoupBinTcpPacketWriterStart = cursor.Index;
        if (clientSoupBinTcpPacketWriterEnd == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) clientSoupBinTcpPacketWriterEnd = cursor.Index;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ClientPacketWriter.ClientSoupBinTcpPacketPayloadRule0(bytes, ref cursor, clientSoupBinTcpPacketWriterStart, cursor.Index, clientSoupBinTcpPacketWriterEnd);
        return bytes[start..cursor.Index];
    }
}
