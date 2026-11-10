using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0;

/// <summary>
///  Branch
/// </summary>
public static class Messages
{
    /// <summary>
    ///  Identifies the case of the Messages that the bytes select.
    /// </summary>
    public enum Case
    {
        /// <summary>
        ///  The bytes select no case of the Messages.
        /// </summary>
        None = 0,
        /// <summary>
        ///  Mold Udp64 heartbeat
        /// </summary>
        Heartbeat = 1,
        /// <summary>
        ///  Mold Udp64 end of session
        /// </summary>
        EndOfSession = 2,
        /// <summary>
        ///  Mold Udp64 Message
        /// </summary>
        Message = 3,
    }

    /// <summary>
    ///  Reads the Messages in place.
    /// </summary>
    public readonly ref struct Reader
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int start;
        private readonly int end;
        private readonly ushort messageCountValue;
        private readonly ulong sequenceNumberSequenceSeed;
        private readonly global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Messages.Case selectedCase;

        internal Reader(ReadOnlySpan<byte> bytes, int start, int end, ushort messageCountValue, ulong sequenceNumberSequenceSeed)
        {
            this.bytes = bytes;
            this.start = start;
            this.end = end;
            this.messageCountValue = messageCountValue;
            this.sequenceNumberSequenceSeed = sequenceNumberSequenceSeed;
            this.selectedCase = (global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Messages.Case)Parser.Valid(Parser.SelectMessages(this.bytes, this.start, this.end, this.start, this.messageCountValue));
        }

        /// <summary>
        ///  Gets the case of the Messages that the bytes select.
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Messages.Case Case => this.selectedCase;

        /// <summary>
        ///  Gets the bytes of the Messages.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureMessages(this.bytes, this.start, this.end, this.start, this.messageCountValue))];

        /// <summary>
        ///  Mold Udp64 Message
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public Messages.MessageEntries Message
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Messages.Case.Message) throw new global::System.InvalidOperationException(Parser.OtherCase);
                int messageReaderPosition = Parser.Valid(Parser.MessagesMessageStart(this.bytes, this.start, this.end, this.start, this.messageCountValue, out var messageCursorLimit, out var messageCursorMessageCountRule0Entries));
                return new(this.bytes, messageReaderPosition, messageCursorLimit, messageCursorMessageCountRule0Entries, this.sequenceNumberSequenceSeed);
            }
        }
    }

    /// <summary>
    ///  Steps through the Message entries in wire order.
    /// </summary>
    /// <remarks>
    ///  A copy of the entries steps on its own. Each step measures its entry from the bytes.
    /// </remarks>
    public ref struct MessageEntries
    {
        private const long FirstEntry = 1;
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int limit;
        private int position;
        private long index;
        private int currentStart;
        private int currentEnd;
        private readonly Parser.RuleCount total;
        private Parser.RuleCount messageCursorMessageCountRule0Entries;
        private readonly ulong sequenceNumberSequenceSeed;

        internal MessageEntries(ReadOnlySpan<byte> bytes, int start, int limit, Parser.RuleCount messageCursorMessageCountRule0Entries, ulong sequenceNumberSequenceSeed)
        {
            this.bytes = bytes;
            this.limit = limit;
            this.position = start;
            this.total = messageCursorMessageCountRule0Entries;
            this.messageCursorMessageCountRule0Entries = messageCursorMessageCountRule0Entries;
            this.sequenceNumberSequenceSeed = sequenceNumberSequenceSeed;
        }
        private readonly void Entry()
        {
            if (this.index == Parser.Position.NoEntries) throw new global::System.InvalidOperationException(Parser.NoEntry);
        }
        /// <summary>
        ///  Gets the enumerator over the entries.
        /// </summary>
        public readonly MessageEntries GetEnumerator() => this;

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
            int end = Parser.StepMessagesMessage(this.bytes, this.position, this.limit, ref this.messageCursorMessageCountRule0Entries);
            if (end == Parser.Position.Ended) return false;
            this.currentStart = this.position;
            this.currentEnd = Parser.Valid(end);
            this.position = this.currentEnd;
            this.index++;
            return true;
        }

        /// <summary>
        ///  Gets the number of entries that the bytes state.
        /// </summary>
        /// <exception cref="global::System.OverflowException">
        ///  The number of entries is larger than an int can hold.
        /// </exception>
        public readonly long Count
        {
            get
            {
                if (!Parser.TryRuleCountValue(in this.total, out int count)) throw new global::System.OverflowException(Parser.CountTooLarge);
                return count;
            }
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
        public readonly global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Message.Reader Current
        {
            get
            {
                Entry();
                return new(this.bytes, this.currentStart, this.currentEnd, unchecked((ulong)(this.sequenceNumberSequenceSeed + (ulong)Parser.RuleSequenceStep * (ulong)(this.index - FirstEntry))));
            }
        }
    }

    /// <summary>
    ///  The Messages fields to write.
    /// </summary>
    public readonly ref struct Value
    {
        internal readonly global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Messages.Case @case;
        internal readonly global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Entries<global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Message.Value> message;
        internal readonly CaseStorage caseStorage;

        [StructLayout(LayoutKind.Explicit)]
        internal struct CaseStorage
        {
            const int CaseStart = 0;

            [FieldOffset(CaseStart)] internal global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Heartbeat.Value heartbeat;
            [FieldOffset(CaseStart)] internal global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.EndOfSession.Value endOfSession;
        }

        /// <summary>
        ///  Mold Udp64 heartbeat
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Heartbeat.Value Heartbeat
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterTwoCases, global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterValue);
                this.caseStorage.heartbeat = value;
                this.@case = global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Messages.Case.Heartbeat;
            }
        }

        /// <summary>
        ///  Mold Udp64 end of session
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.EndOfSession.Value EndOfSession
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterTwoCases, global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterValue);
                this.caseStorage.endOfSession = value;
                this.@case = global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Messages.Case.EndOfSession;
            }
        }

        /// <summary>
        ///  Mold Udp64 Message
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Entries<global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Message.Value> Message
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterTwoCases, global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterValue);
                this.message = value;
                this.@case = global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Messages.Case.Message;
            }
        }

        /// <summary>
        ///  The value in the bytes that selects the Heartbeat.
        /// </summary>
        public const ushort HeartbeatMessageCount = 0;

        /// <summary>
        ///  The value in the bytes that selects the End Of Session.
        /// </summary>
        public const ushort EndOfSessionMessageCount = 65535;

        /// <summary>
        ///  Creates the value of the Messages, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    private const int messageCountDependencyWriteDistanceFromMessage = -2;
    private const int HeartbeatCase = 1;
    private const int EndOfSessionCase = 2;
    private const int MessageCase = 3;

    /// <summary>
    ///  Writes the Messages to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Messages.
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
        return Write(destination, ref cursor, global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.Position.Minimum, ref global::System.Runtime.CompilerServices.Unsafe.NullRef<bool>(), ref global::System.Runtime.CompilerServices.Unsafe.NullRef<int>(), in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterCursor cursor, int start, scoped ref bool messageCountDependencyWriteUsed, scoped ref int messagesWriterCaseRecord, scoped in Value value)
    {
        int messagesWriterCase = global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.Position.Absent;
        int messageWriterStart = global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.Position.Absent;
        int messageWriterCount = global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.Position.NoEntries;
        if (value.@case is not (global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Messages.Case.Heartbeat or global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Messages.Case.EndOfSession or global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Messages.Case.Message)) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterNoCase, nameof(value));
        if (value.@case == global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Messages.Case.Heartbeat)
        {
            messagesWriterCase = HeartbeatCase;
            int heartbeatWriting = cursor.Index;
            var heartbeatWriteValue = value.caseStorage.heartbeat;
            global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Heartbeat.Write(bytes, ref cursor, heartbeatWriting, in heartbeatWriteValue);
        }
        if (value.@case == global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Messages.Case.EndOfSession)
        {
            messagesWriterCase = EndOfSessionCase;
            int endOfSessionWriting = cursor.Index;
            var endOfSessionWriteValue = value.caseStorage.endOfSession;
            global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.EndOfSession.Write(bytes, ref cursor, endOfSessionWriting, in endOfSessionWriteValue);
        }
        if (value.@case == global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Messages.Case.Message)
        {
            for (int messageIndex = 0; messageIndex < value.message.Count; messageIndex++)
            {
                var messageCurrent = value.message[messageIndex];
                messagesWriterCase = MessageCase;
                if (messageWriterStart == global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.Position.Absent) messageWriterStart = cursor.Index;
                messageWriterCount++;
                int messageWriting = cursor.Index;
                var messageWriteValue = messageCurrent;
                global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Message.Write(bytes, ref cursor, messageWriting, in messageWriteValue);
            }
        }
        if (messageWriterStart == global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.Position.Absent) messageWriterStart = cursor.Index;
        if (messagesWriterCase == MessageCase) global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.MessagesWriter.MessageCountRule0(bytes, ref cursor, messageWriterStart < global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.Position.Minimum ? global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.Position.Absent : messageWriterStart + messageCountDependencyWriteDistanceFromMessage, ref messageCountDependencyWriteUsed, messageWriterStart, messageWriterCount);
        if (!global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref messagesWriterCaseRecord))
        {
            int recorded = messagesWriterCase == global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.Position.Absent ? global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.Position.NoCase : messagesWriterCase;
            if (messagesWriterCaseRecord != global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.Position.Absent && messagesWriterCaseRecord != recorded) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterRuleDisagrees, global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterValue);
            messagesWriterCaseRecord = recorded;
        }
        return bytes[start..cursor.Index];
    }
}
