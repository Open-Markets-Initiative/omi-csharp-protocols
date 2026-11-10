using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

/// <summary>
///  Nasdaq historical file: every message of the day, each preceded by its length
/// </summary>
public static class File
{
    /// <summary>
    ///  Reads the File in place.
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
        ///  Gets the bytes of the File.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..this.end];

        /// <summary>
        ///  Nasdaq historical file message: its length, then the message
        /// </summary>
        public File.FileMessageEntries FileMessage
        {
            get
            {
                int fileMessageReaderPosition = Parser.Valid(Parser.FileFileMessageStart(this.bytes, this.start, this.end, this.start, out var fileMessageCursorLimit, out var fileMessageCursorFileMessagePayloadRule0End));
                return new(this.bytes, fileMessageReaderPosition, fileMessageCursorLimit, fileMessageCursorFileMessagePayloadRule0End);
            }
        }
    }

    /// <summary>
    ///  Reads one File from the bytes.
    /// </summary>
    /// <param name="source">
    ///  The bytes of the File.
    /// </param>
    /// <param name="reader">
    ///  The Reader of the File, which borrows the bytes.
    /// </param>
    /// <returns>
    ///  <see langword="true"/> when the bytes hold the fixed fields at the start of the File; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool TryRead(ReadOnlySpan<byte> source, out Reader reader)
    {
        reader = new Reader(source, Parser.Position.Minimum, source.Length);
        return true;
    }

    /// <summary>
    ///  Reads one File Message from the bytes of its frame.
    /// </summary>
    /// <param name="source">
    ///  The bytes of the frame of the File Message.
    /// </param>
    /// <param name="reader">
    ///  The Reader of the File Message, which borrows the bytes.
    /// </param>
    /// <returns>
    ///  <see langword="true"/> when the bytes hold the fixed fields at the start of the File Message; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool TryReadRecord(ReadOnlySpan<byte> source, out global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.FileMessage.Reader reader)
    {
        if (global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.FileMessage.LayoutSize > source.Length)
        {
            reader = default;
            return false;
        }
        reader = new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.FileMessage.Reader(source, Parser.Position.Minimum, source.Length);
        return true;
    }

    /// <summary>
    ///  Gets the length of the File frame at the start of the bytes.
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
        int available = Parser.FileMessageReassembleRule1BytesRemaining(source, Parser.Position.Minimum, source.Length, out length);
        if (length < Parser.Position.Minimum || available >= Parser.Position.Minimum && length == Parser.Position.Minimum) throw new global::System.ArgumentOutOfRangeException(nameof(source), Parser.FrameLength);
        return length > Parser.Position.Minimum;
    }

    /// <summary>
    ///  Steps through the File Message entries in wire order.
    /// </summary>
    /// <remarks>
    ///  A copy of the entries steps on its own. Each step measures its entry from the bytes.
    /// </remarks>
    public ref struct FileMessageEntries
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int limit;
        private int position;
        private long index;
        private int currentStart;
        private int currentEnd;
        private readonly int fileMessageCursorFileMessagePayloadRule0End;

        internal FileMessageEntries(ReadOnlySpan<byte> bytes, int start, int limit, int fileMessageCursorFileMessagePayloadRule0End)
        {
            this.bytes = bytes;
            this.limit = limit;
            this.position = start;
            this.fileMessageCursorFileMessagePayloadRule0End = fileMessageCursorFileMessagePayloadRule0End;
        }
        private readonly void Entry()
        {
            if (this.index == Parser.Position.NoEntries) throw new global::System.InvalidOperationException(Parser.NoEntry);
        }
        /// <summary>
        ///  Gets the enumerator over the entries.
        /// </summary>
        public readonly FileMessageEntries GetEnumerator() => this;

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
            int end = Parser.StepFileFileMessage(this.bytes, this.position, this.limit, this.fileMessageCursorFileMessagePayloadRule0End);
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
        public readonly global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.FileMessage.Reader Current
        {
            get
            {
                Entry();
                return new(this.bytes, this.currentStart, this.currentEnd);
            }
        }
    }

    /// <summary>
    ///  The File fields to write.
    /// </summary>
    public readonly ref struct Value
    {
        /// <summary>
        ///  Nasdaq historical file message: its length, then the message
        /// </summary>
        public global::System.ReadOnlySpan<global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.FileMessage.Value> FileMessage { get; init; }

        /// <summary>
        ///  Creates the value of the File, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    /// <summary>
    ///  Writes the File to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the File.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        int fileMessageWriterStart = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.Absent;
        int fileMessageWriterEnd = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.Absent;
        int fileMessageWriterCount = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.NoEntries;
        for (int fileMessageIndex = 0; fileMessageIndex < value.FileMessage.Length; fileMessageIndex++)
        {
            ref readonly var fileMessageCurrent = ref value.FileMessage[fileMessageIndex];
            if (fileMessageWriterStart == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.Absent) fileMessageWriterStart = cursor.Index;
            fileMessageWriterCount++;
            int fileMessageWriting = cursor.Index;
            var fileMessageWriteValue = fileMessageCurrent;
            global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.FileMessage.Write(bytes, ref cursor, fileMessageWriting, in fileMessageWriteValue);
        }
        if (fileMessageWriterStart == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.Absent) fileMessageWriterStart = cursor.Index;
        if (fileMessageWriterEnd == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.Absent) fileMessageWriterEnd = cursor.Index;
        global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.FileWriter.FileMessagePayloadRule0(bytes, ref cursor, fileMessageWriterStart, cursor.Index, fileMessageWriterEnd);
        return bytes[start..cursor.Index];
    }
}
