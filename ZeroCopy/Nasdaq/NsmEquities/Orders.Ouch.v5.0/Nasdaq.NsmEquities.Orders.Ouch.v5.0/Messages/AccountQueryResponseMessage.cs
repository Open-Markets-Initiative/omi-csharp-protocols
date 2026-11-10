using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  The Account Query Response message in sent in response to an Account Query Request to indicate the next available UserRefNum that can be used to identify new transactions.
/// </summary>
public static class AccountQueryResponseMessage
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
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseMessage.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Expressed as nanoseconds since midnight
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Timestamp Timestamp;
        /// <summary>
        ///  The next available UserRefNum
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.NextUserRefNum NextUserRefNum;
    }

    /// <summary>
    ///  Reads the Account Query Response Message in place.
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
        ///  Gets the bytes of the Account Query Response Message.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureAccountQueryResponseMessage(this.bytes, this.start, this.end, this.start))];

        /// <summary>
        ///  Gets the fixed fields at the start of the Account Query Response Message.
        /// </summary>
        public ref readonly Layout Layout => ref this.layout;

        /// <summary>
        ///  Expressed as nanoseconds since midnight
        /// </summary>
        public ref readonly Timestamp Timestamp => ref this.layout.Timestamp;

        /// <summary>
        ///  The next available UserRefNum
        /// </summary>
        public ref readonly NextUserRefNum NextUserRefNum => ref this.layout.NextUserRefNum;

        /// <summary>
        ///  The length of the remaining Optional Appendage field.
        /// </summary>
        /// <remarks>
        ///  <see langword="null"/> when the bytes leave it out.
        /// </remarks>
        public AppendageLength? AppendageLength
        {
            get
            {
                int accountQueryResponseMessageAppendageLengthReaderPosition = Parser.Valid(Parser.AccountQueryResponseMessageAppendageLengthStart(this.bytes, this.start, this.end, this.start));
                return accountQueryResponseMessageAppendageLengthReaderPosition == Parser.Position.Absent ? null : global::System.Runtime.InteropServices.MemoryMarshal.AsRef<AppendageLength>(this.bytes[accountQueryResponseMessageAppendageLengthReaderPosition..this.end]);
            }
        }

        /// <summary>
        ///  Account Query Response Appendage
        /// </summary>
        public AccountQueryResponseMessage.AccountQueryResponseAppendageEntries AccountQueryResponseAppendage
        {
            get
            {
                int accountQueryResponseAppendageReaderPosition = Parser.Valid(Parser.AccountQueryResponseMessageAccountQueryResponseAppendageStart(this.bytes, this.start, this.end, this.start, out var accountQueryResponseAppendageCursorLimit, out var accountQueryResponseAppendageCursorAccountQueryResponseAppendageCountRule0End));
                return new(this.bytes, accountQueryResponseAppendageReaderPosition, accountQueryResponseAppendageCursorLimit, accountQueryResponseAppendageCursorAccountQueryResponseAppendageCountRule0End);
            }
        }
    }

    /// <summary>
    ///  Steps through the Account Query Response Appendage entries in wire order.
    /// </summary>
    /// <remarks>
    ///  A copy of the entries steps on its own. Each step measures its entry from the bytes.
    /// </remarks>
    public ref struct AccountQueryResponseAppendageEntries
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int limit;
        private int position;
        private long index;
        private int currentStart;
        private int currentEnd;
        private readonly int accountQueryResponseAppendageCursorAccountQueryResponseAppendageCountRule0End;

        internal AccountQueryResponseAppendageEntries(ReadOnlySpan<byte> bytes, int start, int limit, int accountQueryResponseAppendageCursorAccountQueryResponseAppendageCountRule0End)
        {
            this.bytes = bytes;
            this.limit = limit;
            this.position = start;
            this.accountQueryResponseAppendageCursorAccountQueryResponseAppendageCountRule0End = accountQueryResponseAppendageCursorAccountQueryResponseAppendageCountRule0End;
        }
        private readonly void Entry()
        {
            if (this.index == Parser.Position.NoEntries) throw new global::System.InvalidOperationException(Parser.NoEntry);
        }
        /// <summary>
        ///  Gets the enumerator over the entries.
        /// </summary>
        public readonly AccountQueryResponseAppendageEntries GetEnumerator() => this;

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
            int end = Parser.StepAccountQueryResponseMessageAccountQueryResponseAppendage(this.bytes, this.position, this.limit, this.accountQueryResponseAppendageCursorAccountQueryResponseAppendageCountRule0End);
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
        public readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseAppendage.Reader Current
        {
            get
            {
                Entry();
                return new(this.bytes, this.currentStart, this.currentEnd);
            }
        }
    }

    /// <summary>
    ///  The Account Query Response Message fields to write.
    /// </summary>
    public readonly ref struct Value
    {
        /// <summary>
        ///  Expressed as nanoseconds since midnight
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Timestamp Timestamp { get; init; }

        /// <summary>
        ///  The next available UserRefNum
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.NextUserRefNum NextUserRefNum { get; init; }

        /// <summary>
        ///  The length of the remaining Optional Appendage field.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AppendageLength? AppendageLength { get; init; }

        /// <summary>
        ///  Account Query Response Appendage
        /// </summary>
        public global::System.ReadOnlySpan<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseAppendage.Value> AccountQueryResponseAppendage { get; init; }

        /// <summary>
        ///  Creates the value of the Account Query Response Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeTimestamp + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeNextUserRefNum;
    private const int accountQueryResponseMessageAppendageLengthDependencyWriteDistanceFromAccountQueryResponseMessageAppendageLength = 0;

    /// <summary>
    ///  Writes the Account Query Response Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <param name="sequencedMessageEnd">
    ///  The offset in destination where the enclosing Sequenced Message ends.
    /// </param>
    /// <returns>
    ///  The bytes of the Account Query Response Message.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value, int sequencedMessageEnd)
    {
        var cursor = new global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterCursor();
        cursor.SequencedDataPacket_SequencedMessage_End = sequencedMessageEnd;
        cursor.SequencedDataPacket_SequencedMessage_EndFrozen = true;
        return Write(destination, ref cursor, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        int accountQueryResponseMessageAppendageLengthWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int accountQueryResponseMessageAppendageLengthWriterCount = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries;
        int accountQueryResponseAppendageWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int accountQueryResponseAppendageWriterEnd = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int accountQueryResponseAppendageWriterCount = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
        ref Layout accountQueryResponseMessageTimestampWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        accountQueryResponseMessageTimestampWriterLayout.Timestamp = value.Timestamp;
        accountQueryResponseMessageTimestampWriterLayout.NextUserRefNum = value.NextUserRefNum;
        cursor.Index += LayoutSize;
        if (value.AppendageLength is { } accountQueryResponseMessageAppendageLengthCurrent)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAppendageLength > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            if (accountQueryResponseMessageAppendageLengthWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) accountQueryResponseMessageAppendageLengthWriterStart = cursor.Index;
            accountQueryResponseMessageAppendageLengthWriterCount++;
            int accountQueryResponseMessageAppendageLengthWriterPosition = cursor.Index;
            cursor.AccountQueryResponseMessage_AppendageLength_Used = false;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAppendageLength;
        }
        for (int accountQueryResponseAppendageIndex = 0; accountQueryResponseAppendageIndex < value.AccountQueryResponseAppendage.Length; accountQueryResponseAppendageIndex++)
        {
            ref readonly var accountQueryResponseAppendageCurrent = ref value.AccountQueryResponseAppendage[accountQueryResponseAppendageIndex];
            if (accountQueryResponseMessageAppendageLengthWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) accountQueryResponseMessageAppendageLengthWriterStart = cursor.Index;
            if (accountQueryResponseAppendageWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) accountQueryResponseAppendageWriterStart = cursor.Index;
            accountQueryResponseAppendageWriterCount++;
            int accountQueryResponseAppendageWriting = cursor.Index;
            var accountQueryResponseAppendageWriteValue = accountQueryResponseAppendageCurrent;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseAppendage.Write(bytes, ref cursor, accountQueryResponseAppendageWriting, in accountQueryResponseAppendageWriteValue);
        }
        if (accountQueryResponseMessageAppendageLengthWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) accountQueryResponseMessageAppendageLengthWriterStart = cursor.Index;
        if (accountQueryResponseAppendageWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) accountQueryResponseAppendageWriterStart = cursor.Index;
        if (accountQueryResponseAppendageWriterEnd == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) accountQueryResponseAppendageWriterEnd = cursor.Index;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.AccountQueryResponseMessageWriter.AccountQueryResponseAppendageCountRule0(bytes, ref cursor, accountQueryResponseMessageAppendageLengthWriterStart < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : accountQueryResponseMessageAppendageLengthWriterStart + accountQueryResponseMessageAppendageLengthDependencyWriteDistanceFromAccountQueryResponseMessageAppendageLength, accountQueryResponseMessageAppendageLengthWriterCount != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries, accountQueryResponseAppendageWriterStart, accountQueryResponseAppendageWriterEnd);
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.AccountQueryResponseMessageWriter.AccountQueryResponseMessageAppendageLengthOptionalRule0(bytes, ref cursor, accountQueryResponseMessageAppendageLengthWriterStart, cursor.SequencedDataPacket_SequencedMessage_End, accountQueryResponseMessageAppendageLengthWriterCount != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries);
        return bytes[start..cursor.Index];
    }
}
