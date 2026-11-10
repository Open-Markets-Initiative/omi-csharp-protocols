using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Databento.MarketData.Normalized.Dbn.v2;

/// <summary>
///  The client's control messages: authentication, subscriptions and the session start
/// </summary>
public static class ClientStream
{
    /// <summary>
    ///  Reads the Client Stream in place.
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
        ///  Gets the bytes of the Client Stream.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..this.end];

        /// <summary>
        ///  A control message: key=value fields separated by |, ending on a newline
        /// </summary>
        public ClientStream.ControlMessageEntries ControlMessage
        {
            get
            {
                int controlMessageReaderPosition = Parser.Valid(Parser.ClientStreamControlMessageStart(this.bytes, this.start, this.end, this.start, out var controlMessageCursorLimit, out var controlMessageCursorControlMessagePayloadRule0End));
                return new(this.bytes, controlMessageReaderPosition, controlMessageCursorLimit, controlMessageCursorControlMessagePayloadRule0End);
            }
        }
    }

    /// <summary>
    ///  Reads one Client Stream from the bytes.
    /// </summary>
    /// <param name="source">
    ///  The bytes of the Client Stream.
    /// </param>
    /// <param name="reader">
    ///  The Reader of the Client Stream, which borrows the bytes.
    /// </param>
    /// <returns>
    ///  <see langword="true"/> when the bytes hold the fixed fields at the start of the Client Stream; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool TryRead(ReadOnlySpan<byte> source, out Reader reader)
    {
        reader = new Reader(source, Parser.Position.Minimum, source.Length);
        return true;
    }

    /// <summary>
    ///  Reads one Control Message from the bytes of its frame.
    /// </summary>
    /// <param name="source">
    ///  The bytes of the frame of the Control Message.
    /// </param>
    /// <param name="reader">
    ///  The Reader of the Control Message, which borrows the bytes.
    /// </param>
    /// <returns>
    ///  <see langword="true"/> when the bytes hold the fixed fields at the start of the Control Message; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool TryReadRecord(ReadOnlySpan<byte> source, out global::Databento.MarketData.Normalized.Dbn.v2.ControlMessage.Reader reader)
    {
        reader = new global::Databento.MarketData.Normalized.Dbn.v2.ControlMessage.Reader(source, Parser.Position.Minimum, source.Length);
        return true;
    }

    /// <summary>
    ///  Gets the length of the Client Stream frame at the start of the bytes.
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
        int available = Parser.ControlMessageReassembleRule1BytesRemaining(source, Parser.Position.Minimum, source.Length, out length);
        if (length < Parser.Position.Minimum || available >= Parser.Position.Minimum && length == Parser.Position.Minimum) throw new global::System.ArgumentOutOfRangeException(nameof(source), Parser.FrameLength);
        return length > Parser.Position.Minimum;
    }

    /// <summary>
    ///  Steps through the Control Message entries in wire order.
    /// </summary>
    /// <remarks>
    ///  A copy of the entries steps on its own. Each step measures its entry from the bytes.
    /// </remarks>
    public ref struct ControlMessageEntries
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int limit;
        private int position;
        private long index;
        private int currentStart;
        private int currentEnd;
        private readonly int controlMessageCursorControlMessagePayloadRule0End;

        internal ControlMessageEntries(ReadOnlySpan<byte> bytes, int start, int limit, int controlMessageCursorControlMessagePayloadRule0End)
        {
            this.bytes = bytes;
            this.limit = limit;
            this.position = start;
            this.controlMessageCursorControlMessagePayloadRule0End = controlMessageCursorControlMessagePayloadRule0End;
        }
        private readonly void Entry()
        {
            if (this.index == Parser.Position.NoEntries) throw new global::System.InvalidOperationException(Parser.NoEntry);
        }
        /// <summary>
        ///  Gets the enumerator over the entries.
        /// </summary>
        public readonly ControlMessageEntries GetEnumerator() => this;

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
            int end = Parser.StepClientStreamControlMessage(this.bytes, this.position, this.limit, this.controlMessageCursorControlMessagePayloadRule0End);
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
        public readonly global::Databento.MarketData.Normalized.Dbn.v2.ControlMessage.Reader Current
        {
            get
            {
                Entry();
                return new(this.bytes, this.currentStart, this.currentEnd);
            }
        }
    }

    /// <summary>
    ///  The Client Stream fields to write.
    /// </summary>
    public readonly ref struct Value
    {
        /// <summary>
        ///  A control message: key=value fields separated by |, ending on a newline
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v2.Entries<global::Databento.MarketData.Normalized.Dbn.v2.ControlMessage.Value> ControlMessage { get; init; }

        /// <summary>
        ///  Creates the value of the Client Stream, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    /// <summary>
    ///  Writes the Client Stream to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Client Stream.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Databento.MarketData.Normalized.Dbn.v2.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Databento.MarketData.Normalized.Dbn.v2.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Databento.MarketData.Normalized.Dbn.v2.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        int controlMessageWriterStart = global::Databento.MarketData.Normalized.Dbn.v2.Parser.Position.Absent;
        int controlMessageWriterEnd = global::Databento.MarketData.Normalized.Dbn.v2.Parser.Position.Absent;
        int controlMessageWriterCount = global::Databento.MarketData.Normalized.Dbn.v2.Parser.Position.NoEntries;
        for (int controlMessageIndex = 0; controlMessageIndex < value.ControlMessage.Count; controlMessageIndex++)
        {
            var controlMessageCurrent = value.ControlMessage[controlMessageIndex];
            if (controlMessageWriterStart == global::Databento.MarketData.Normalized.Dbn.v2.Parser.Position.Absent) controlMessageWriterStart = cursor.Index;
            controlMessageWriterCount++;
            int controlMessageWriting = cursor.Index;
            var controlMessageWriteValue = controlMessageCurrent;
            global::Databento.MarketData.Normalized.Dbn.v2.ControlMessage.Write(bytes, ref cursor, controlMessageWriting, in controlMessageWriteValue);
        }
        if (controlMessageWriterStart == global::Databento.MarketData.Normalized.Dbn.v2.Parser.Position.Absent) controlMessageWriterStart = cursor.Index;
        if (controlMessageWriterEnd == global::Databento.MarketData.Normalized.Dbn.v2.Parser.Position.Absent) controlMessageWriterEnd = cursor.Index;
        global::Databento.MarketData.Normalized.Dbn.v2.Parser.ClientStreamWriter.ControlMessagePayloadRule0(bytes, ref cursor, controlMessageWriterStart, cursor.Index, controlMessageWriterEnd);
        return bytes[start..cursor.Index];
    }
}
