using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

/// <summary>
///  Whenever a complex order is added in the system for an underlying, the order is normalized and results in either the creation of a new complex strategy or is added to an existing strategy. A Complex Strategy Message containing the strategy definition will be sent.
/// </summary>
public static class ComplexStrategyDirectoryMessage
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
            if (global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyDirectoryMessage.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  The time, expressed as the number of nanoseconds after midnight.
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Timestamp Timestamp;
        /// <summary>
        ///  ISE’s Strategy ID assigned daily, valid while there are any open complex orders for the day
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyId StrategyId;
        /// <summary>
        ///  StrategyType value
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyTypeField StrategyType;
        /// <summary>
        ///  Identifies the source of the Strategy, valid for the trading day
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Source Source;
        /// <summary>
        ///  Underlying Symbol for the strategy. All legs in this strategy belong to this Underlying
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.UnderlyingSymbol UnderlyingSymbol;
        /// <summary>
        ///  Number of legs in the strategy NOTE: Leg field offsets below are an equation, where “n” is the zero based leg number (0, 1, …)
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.NumberOfLegs NumberOfLegs;
    }

    /// <summary>
    ///  Reads the Complex Strategy Directory Message in place.
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
        ///  Gets the bytes of the Complex Strategy Directory Message.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureComplexStrategyDirectoryMessage(this.bytes, this.start, this.end, this.start))];

        /// <summary>
        ///  Gets the fixed fields at the start of the Complex Strategy Directory Message.
        /// </summary>
        public ref readonly Layout Layout => ref this.layout;

        /// <summary>
        ///  The time, expressed as the number of nanoseconds after midnight.
        /// </summary>
        public ref readonly Timestamp Timestamp => ref this.layout.Timestamp;

        /// <summary>
        ///  ISE’s Strategy ID assigned daily, valid while there are any open complex orders for the day
        /// </summary>
        public ref readonly StrategyId StrategyId => ref this.layout.StrategyId;

        /// <summary>
        ///  StrategyType value
        /// </summary>
        public ref readonly StrategyTypeField StrategyType => ref this.layout.StrategyType;

        /// <summary>
        ///  Identifies the source of the Strategy, valid for the trading day
        /// </summary>
        public ref readonly Source Source => ref this.layout.Source;

        /// <summary>
        ///  Underlying Symbol for the strategy. All legs in this strategy belong to this Underlying
        /// </summary>
        public ref readonly UnderlyingSymbol UnderlyingSymbol => ref this.layout.UnderlyingSymbol;

        /// <summary>
        ///  Number of legs in the strategy NOTE: Leg field offsets below are an equation, where “n” is the zero based leg number (0, 1, …)
        /// </summary>
        public ref readonly NumberOfLegs NumberOfLegs => ref this.layout.NumberOfLegs;

        /// <summary>
        ///  Leg information
        /// </summary>
        public ComplexStrategyDirectoryMessage.LegInformationEntries LegInformation
        {
            get
            {
                int legInformationReaderPosition = Parser.Valid(Parser.ComplexStrategyDirectoryMessageLegInformationStart(this.bytes, this.start, this.end, this.start, out var legInformationCursorLimit, out var legInformationCursorLegInformationCountRule0Entries));
                return new(this.bytes, legInformationReaderPosition, legInformationCursorLimit, legInformationCursorLegInformationCountRule0Entries);
            }
        }
    }

    /// <summary>
    ///  Steps through the Leg Information entries in wire order.
    /// </summary>
    /// <remarks>
    ///  A copy of the entries steps on its own. Each step measures its entry from the bytes.
    /// </remarks>
    public ref struct LegInformationEntries
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int limit;
        private int position;
        private long index;
        private int currentStart;
        private int currentEnd;
        private readonly Parser.RuleCount total;
        private Parser.RuleCount legInformationCursorLegInformationCountRule0Entries;

        internal LegInformationEntries(ReadOnlySpan<byte> bytes, int start, int limit, Parser.RuleCount legInformationCursorLegInformationCountRule0Entries)
        {
            this.bytes = bytes;
            this.limit = limit;
            this.position = start;
            this.total = legInformationCursorLegInformationCountRule0Entries;
            this.legInformationCursorLegInformationCountRule0Entries = legInformationCursorLegInformationCountRule0Entries;
        }
        private readonly void Entry()
        {
            if (this.index == Parser.Position.NoEntries) throw new global::System.InvalidOperationException(Parser.NoEntry);
        }
        /// <summary>
        ///  Gets the enumerator over the entries.
        /// </summary>
        public readonly LegInformationEntries GetEnumerator() => this;

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
            int end = Parser.StepComplexStrategyDirectoryMessageLegInformation(this.bytes, this.position, this.limit, ref this.legInformationCursorLegInformationCountRule0Entries);
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
        ///  Gets the current entry.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The entries have no current entry.
        /// </exception>
        public readonly ref readonly LegInformation.Layout Current
        {
            get
            {
                Entry();
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<LegInformation.Layout>(this.bytes[this.currentStart..this.currentEnd]);
            }
        }
    }

    /// <summary>
    ///  The Complex Strategy Directory Message fields to write.
    /// </summary>
    public readonly ref struct Value
    {
        /// <summary>
        ///  The time, expressed as the number of nanoseconds after midnight.
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Timestamp Timestamp { get; init; }

        /// <summary>
        ///  ISE’s Strategy ID assigned daily, valid while there are any open complex orders for the day
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyId StrategyId { get; init; }

        /// <summary>
        ///  StrategyType value
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyTypeField StrategyType { get; init; }

        /// <summary>
        ///  Identifies the source of the Strategy, valid for the trading day
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Source Source { get; init; }

        /// <summary>
        ///  Underlying Symbol for the strategy. All legs in this strategy belong to this Underlying
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.UnderlyingSymbol UnderlyingSymbol { get; init; }

        /// <summary>
        ///  Leg information
        /// </summary>
        public global::System.ReadOnlySpan<global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.LegInformation.Value> LegInformation { get; init; }

        /// <summary>
        ///  Creates the value of the Complex Strategy Directory Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeTimestamp + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeStrategyId + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeStrategyType + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeSource + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeUnderlyingSymbol + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeNumberOfLegs;
    private const int numberOfLegsDependencyWriteDistanceFromComplexStrategyDirectoryMessage = 25;

    /// <summary>
    ///  Writes the Complex Strategy Directory Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Complex Strategy Directory Message.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        int legInformationWriterStart = global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.Position.Absent;
        int legInformationWriterCount = global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.Position.NoEntries;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterNoRoom, global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterDestination);
        ref Layout complexStrategyDirectoryMessageTimestampWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        complexStrategyDirectoryMessageTimestampWriterLayout.Timestamp = value.Timestamp;
        complexStrategyDirectoryMessageTimestampWriterLayout.StrategyId = value.StrategyId;
        complexStrategyDirectoryMessageTimestampWriterLayout.StrategyType = value.StrategyType;
        complexStrategyDirectoryMessageTimestampWriterLayout.Source = value.Source;
        complexStrategyDirectoryMessageTimestampWriterLayout.UnderlyingSymbol = value.UnderlyingSymbol;
        cursor.ComplexStrategyDirectoryMessage_NumberOfLegs_Used = false;
        cursor.Index += LayoutSize;
        for (int legInformationIndex = 0; legInformationIndex < value.LegInformation.Length; legInformationIndex++)
        {
            ref readonly var legInformationCurrent = ref value.LegInformation[legInformationIndex];
            if (legInformationWriterStart == global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.Position.Absent) legInformationWriterStart = cursor.Index;
            legInformationWriterCount++;
            int legInformationWriting = cursor.Index;
            var legInformationWriteValue = legInformationCurrent;
            global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.LegInformation.Write(bytes, ref cursor, legInformationWriting, in legInformationWriteValue);
        }
        if (legInformationWriterStart == global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.Position.Absent) legInformationWriterStart = cursor.Index;
        global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.ComplexStrategyDirectoryMessageWriter.LegInformationCountRule0(bytes, ref cursor, start < global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.Position.Minimum ? global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.Position.Absent : start + numberOfLegsDependencyWriteDistanceFromComplexStrategyDirectoryMessage, legInformationWriterStart, legInformationWriterCount);
        return bytes[start..cursor.Index];
    }
}
