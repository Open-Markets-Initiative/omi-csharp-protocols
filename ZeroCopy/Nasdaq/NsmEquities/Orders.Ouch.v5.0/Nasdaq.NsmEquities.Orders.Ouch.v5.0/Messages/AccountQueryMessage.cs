using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  The Account Query Request message can be used when recovering state to request the next available UserRefNum that can be used for identifying new transactions.
/// </summary>
public static class AccountQueryMessage
{
    /// <summary>
    ///  Reads the Account Query Message in place.
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
        ///  Gets the bytes of the Account Query Message.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureAccountQueryMessage(this.bytes, this.start, this.end, this.start))];

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
                int accountQueryMessageAppendageLengthReaderPosition = Parser.Valid(Parser.AccountQueryMessageAppendageLengthStart(this.bytes, this.start, this.end, this.start));
                return accountQueryMessageAppendageLengthReaderPosition == Parser.Position.Absent ? null : global::System.Runtime.InteropServices.MemoryMarshal.AsRef<AppendageLength>(this.bytes[accountQueryMessageAppendageLengthReaderPosition..this.end]);
            }
        }

        /// <summary>
        ///  Account Query Appendage
        /// </summary>
        public AccountQueryMessage.AccountQueryAppendageEntries AccountQueryAppendage
        {
            get
            {
                int accountQueryAppendageReaderPosition = Parser.Valid(Parser.AccountQueryMessageAccountQueryAppendageStart(this.bytes, this.start, this.end, this.start, out var accountQueryAppendageCursorLimit, out var accountQueryAppendageCursorAccountQueryAppendageCountRule0End));
                return new(this.bytes, accountQueryAppendageReaderPosition, accountQueryAppendageCursorLimit, accountQueryAppendageCursorAccountQueryAppendageCountRule0End);
            }
        }
    }

    /// <summary>
    ///  Steps through the Account Query Appendage entries in wire order.
    /// </summary>
    /// <remarks>
    ///  A copy of the entries steps on its own. Each step measures its entry from the bytes.
    /// </remarks>
    public ref struct AccountQueryAppendageEntries
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int limit;
        private int position;
        private long index;
        private int currentStart;
        private int currentEnd;
        private readonly int accountQueryAppendageCursorAccountQueryAppendageCountRule0End;

        internal AccountQueryAppendageEntries(ReadOnlySpan<byte> bytes, int start, int limit, int accountQueryAppendageCursorAccountQueryAppendageCountRule0End)
        {
            this.bytes = bytes;
            this.limit = limit;
            this.position = start;
            this.accountQueryAppendageCursorAccountQueryAppendageCountRule0End = accountQueryAppendageCursorAccountQueryAppendageCountRule0End;
        }
        private readonly void Entry()
        {
            if (this.index == Parser.Position.NoEntries) throw new global::System.InvalidOperationException(Parser.NoEntry);
        }
        /// <summary>
        ///  Gets the enumerator over the entries.
        /// </summary>
        public readonly AccountQueryAppendageEntries GetEnumerator() => this;

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
            int end = Parser.StepAccountQueryMessageAccountQueryAppendage(this.bytes, this.position, this.limit, this.accountQueryAppendageCursorAccountQueryAppendageCountRule0End);
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
        public readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryAppendage.Reader Current
        {
            get
            {
                Entry();
                return new(this.bytes, this.currentStart, this.currentEnd);
            }
        }
    }

    /// <summary>
    ///  The Account Query Message fields to write.
    /// </summary>
    public readonly ref struct Value
    {
        /// <summary>
        ///  The length of the remaining Optional Appendage field.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AppendageLength? AppendageLength { get; init; }

        /// <summary>
        ///  Account Query Appendage
        /// </summary>
        public global::System.ReadOnlySpan<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryAppendage.Value> AccountQueryAppendage { get; init; }

        /// <summary>
        ///  Creates the value of the Account Query Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    private const int accountQueryMessageAppendageLengthDependencyWriteDistanceFromAccountQueryMessageAppendageLength = 0;

    /// <summary>
    ///  Writes the Account Query Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <param name="unsequencedMessageEnd">
    ///  The offset in destination where the enclosing Unsequenced Message ends.
    /// </param>
    /// <returns>
    ///  The bytes of the Account Query Message.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value, int unsequencedMessageEnd)
    {
        var cursor = new global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterCursor();
        cursor.UnsequencedDataPacket_UnsequencedMessage_End = unsequencedMessageEnd;
        cursor.UnsequencedDataPacket_UnsequencedMessage_EndFrozen = true;
        return Write(destination, ref cursor, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        int accountQueryMessageAppendageLengthWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int accountQueryMessageAppendageLengthWriterCount = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries;
        int accountQueryAppendageWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int accountQueryAppendageWriterEnd = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int accountQueryAppendageWriterCount = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries;
        if (value.AppendageLength is { } accountQueryMessageAppendageLengthCurrent)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAppendageLength > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            if (accountQueryMessageAppendageLengthWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) accountQueryMessageAppendageLengthWriterStart = cursor.Index;
            accountQueryMessageAppendageLengthWriterCount++;
            int accountQueryMessageAppendageLengthWriterPosition = cursor.Index;
            cursor.AccountQueryMessage_AppendageLength_Used = false;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAppendageLength;
        }
        for (int accountQueryAppendageIndex = 0; accountQueryAppendageIndex < value.AccountQueryAppendage.Length; accountQueryAppendageIndex++)
        {
            ref readonly var accountQueryAppendageCurrent = ref value.AccountQueryAppendage[accountQueryAppendageIndex];
            if (accountQueryMessageAppendageLengthWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) accountQueryMessageAppendageLengthWriterStart = cursor.Index;
            if (accountQueryAppendageWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) accountQueryAppendageWriterStart = cursor.Index;
            accountQueryAppendageWriterCount++;
            int accountQueryAppendageWriting = cursor.Index;
            var accountQueryAppendageWriteValue = accountQueryAppendageCurrent;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryAppendage.Write(bytes, ref cursor, accountQueryAppendageWriting, in accountQueryAppendageWriteValue);
        }
        if (accountQueryMessageAppendageLengthWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) accountQueryMessageAppendageLengthWriterStart = cursor.Index;
        if (accountQueryAppendageWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) accountQueryAppendageWriterStart = cursor.Index;
        if (accountQueryAppendageWriterEnd == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) accountQueryAppendageWriterEnd = cursor.Index;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.AccountQueryMessageWriter.AccountQueryAppendageCountRule0(bytes, ref cursor, accountQueryMessageAppendageLengthWriterStart < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : accountQueryMessageAppendageLengthWriterStart + accountQueryMessageAppendageLengthDependencyWriteDistanceFromAccountQueryMessageAppendageLength, accountQueryMessageAppendageLengthWriterCount != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries, accountQueryAppendageWriterStart, accountQueryAppendageWriterEnd);
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.AccountQueryMessageWriter.AccountQueryMessageAppendageLengthOptionalRule0(bytes, ref cursor, accountQueryMessageAppendageLengthWriterStart, cursor.UnsequencedDataPacket_UnsequencedMessage_End, accountQueryMessageAppendageLengthWriterCount != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries);
        return bytes[start..cursor.Index];
    }
}
