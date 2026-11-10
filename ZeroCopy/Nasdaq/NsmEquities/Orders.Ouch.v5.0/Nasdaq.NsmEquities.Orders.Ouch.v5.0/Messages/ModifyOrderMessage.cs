using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  The Modify Order Message is used to request modifications that will not affect order priority on the book. Since priority of the order does not change, allowed modifications are restricted. Increasing share amount is not allowed and requests to do so will be ignored.
/// </summary>
public static class ModifyOrderMessage
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
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderMessage.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  UserRefNum must be day-unique and strictly increasing for each OUCH account.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UserRefNum UserRefNum;
        /// <summary>
        ///  Side
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SideField Side;
        /// <summary>
        ///  Total number of shares. Must be greater than zero and less than 1,000,000.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Quantity Quantity;
    }

    /// <summary>
    ///  Reads the Modify Order Message in place.
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
        ///  Gets the bytes of the Modify Order Message.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureModifyOrderMessage(this.bytes, this.start, this.end, this.start))];

        /// <summary>
        ///  Gets the fixed fields at the start of the Modify Order Message.
        /// </summary>
        public ref readonly Layout Layout => ref this.layout;

        /// <summary>
        ///  UserRefNum must be day-unique and strictly increasing for each OUCH account.
        /// </summary>
        public ref readonly UserRefNum UserRefNum => ref this.layout.UserRefNum;

        /// <summary>
        ///  Side
        /// </summary>
        public ref readonly SideField Side => ref this.layout.Side;

        /// <summary>
        ///  Total number of shares. Must be greater than zero and less than 1,000,000.
        /// </summary>
        public ref readonly Quantity Quantity => ref this.layout.Quantity;

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
                int modifyOrderMessageAppendageLengthReaderPosition = Parser.Valid(Parser.ModifyOrderMessageAppendageLengthStart(this.bytes, this.start, this.end, this.start));
                return modifyOrderMessageAppendageLengthReaderPosition == Parser.Position.Absent ? null : global::System.Runtime.InteropServices.MemoryMarshal.AsRef<AppendageLength>(this.bytes[modifyOrderMessageAppendageLengthReaderPosition..this.end]);
            }
        }

        /// <summary>
        ///  Modify Order Appendage
        /// </summary>
        public ModifyOrderMessage.ModifyOrderAppendageEntries ModifyOrderAppendage
        {
            get
            {
                int modifyOrderAppendageReaderPosition = Parser.Valid(Parser.ModifyOrderMessageModifyOrderAppendageStart(this.bytes, this.start, this.end, this.start, out var modifyOrderAppendageCursorLimit, out var modifyOrderAppendageCursorModifyOrderAppendageCountRule0End));
                return new(this.bytes, modifyOrderAppendageReaderPosition, modifyOrderAppendageCursorLimit, modifyOrderAppendageCursorModifyOrderAppendageCountRule0End);
            }
        }
    }

    /// <summary>
    ///  Steps through the Modify Order Appendage entries in wire order.
    /// </summary>
    /// <remarks>
    ///  A copy of the entries steps on its own. Each step measures its entry from the bytes.
    /// </remarks>
    public ref struct ModifyOrderAppendageEntries
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int limit;
        private int position;
        private long index;
        private int currentStart;
        private int currentEnd;
        private readonly int modifyOrderAppendageCursorModifyOrderAppendageCountRule0End;

        internal ModifyOrderAppendageEntries(ReadOnlySpan<byte> bytes, int start, int limit, int modifyOrderAppendageCursorModifyOrderAppendageCountRule0End)
        {
            this.bytes = bytes;
            this.limit = limit;
            this.position = start;
            this.modifyOrderAppendageCursorModifyOrderAppendageCountRule0End = modifyOrderAppendageCursorModifyOrderAppendageCountRule0End;
        }
        private readonly void Entry()
        {
            if (this.index == Parser.Position.NoEntries) throw new global::System.InvalidOperationException(Parser.NoEntry);
        }
        /// <summary>
        ///  Gets the enumerator over the entries.
        /// </summary>
        public readonly ModifyOrderAppendageEntries GetEnumerator() => this;

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
            int end = Parser.StepModifyOrderMessageModifyOrderAppendage(this.bytes, this.position, this.limit, this.modifyOrderAppendageCursorModifyOrderAppendageCountRule0End);
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
        public readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderAppendage.Reader Current
        {
            get
            {
                Entry();
                return new(this.bytes, this.currentStart, this.currentEnd);
            }
        }
    }

    /// <summary>
    ///  The Modify Order Message fields to write.
    /// </summary>
    public readonly ref struct Value
    {
        /// <summary>
        ///  UserRefNum must be day-unique and strictly increasing for each OUCH account.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UserRefNum UserRefNum { get; init; }

        /// <summary>
        ///  Side
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SideField Side { get; init; }

        /// <summary>
        ///  Total number of shares. Must be greater than zero and less than 1,000,000.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Quantity Quantity { get; init; }

        /// <summary>
        ///  The length of the remaining Optional Appendage field.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AppendageLength? AppendageLength { get; init; }

        /// <summary>
        ///  Modify Order Appendage
        /// </summary>
        public global::System.ReadOnlySpan<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderAppendage.Value> ModifyOrderAppendage { get; init; }

        /// <summary>
        ///  Creates the value of the Modify Order Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeUserRefNum + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeSide + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeQuantity;
    private const int modifyOrderMessageAppendageLengthDependencyWriteDistanceFromModifyOrderMessageAppendageLength = 0;

    /// <summary>
    ///  Writes the Modify Order Message to the start of destination and fills in each length, count and message type in it.
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
    ///  The bytes of the Modify Order Message.
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
        int modifyOrderMessageAppendageLengthWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int modifyOrderMessageAppendageLengthWriterCount = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries;
        int modifyOrderAppendageWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int modifyOrderAppendageWriterEnd = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int modifyOrderAppendageWriterCount = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
        ref Layout modifyOrderMessageUserRefNumWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        modifyOrderMessageUserRefNumWriterLayout.UserRefNum = value.UserRefNum;
        modifyOrderMessageUserRefNumWriterLayout.Side = value.Side;
        modifyOrderMessageUserRefNumWriterLayout.Quantity = value.Quantity;
        cursor.Index += LayoutSize;
        if (value.AppendageLength is { } modifyOrderMessageAppendageLengthCurrent)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAppendageLength > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            if (modifyOrderMessageAppendageLengthWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) modifyOrderMessageAppendageLengthWriterStart = cursor.Index;
            modifyOrderMessageAppendageLengthWriterCount++;
            int modifyOrderMessageAppendageLengthWriterPosition = cursor.Index;
            cursor.ModifyOrderMessage_AppendageLength_Used = false;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAppendageLength;
        }
        for (int modifyOrderAppendageIndex = 0; modifyOrderAppendageIndex < value.ModifyOrderAppendage.Length; modifyOrderAppendageIndex++)
        {
            ref readonly var modifyOrderAppendageCurrent = ref value.ModifyOrderAppendage[modifyOrderAppendageIndex];
            if (modifyOrderMessageAppendageLengthWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) modifyOrderMessageAppendageLengthWriterStart = cursor.Index;
            if (modifyOrderAppendageWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) modifyOrderAppendageWriterStart = cursor.Index;
            modifyOrderAppendageWriterCount++;
            int modifyOrderAppendageWriting = cursor.Index;
            var modifyOrderAppendageWriteValue = modifyOrderAppendageCurrent;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderAppendage.Write(bytes, ref cursor, modifyOrderAppendageWriting, in modifyOrderAppendageWriteValue);
        }
        if (modifyOrderMessageAppendageLengthWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) modifyOrderMessageAppendageLengthWriterStart = cursor.Index;
        if (modifyOrderAppendageWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) modifyOrderAppendageWriterStart = cursor.Index;
        if (modifyOrderAppendageWriterEnd == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) modifyOrderAppendageWriterEnd = cursor.Index;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ModifyOrderMessageWriter.ModifyOrderAppendageCountRule0(bytes, ref cursor, modifyOrderMessageAppendageLengthWriterStart < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : modifyOrderMessageAppendageLengthWriterStart + modifyOrderMessageAppendageLengthDependencyWriteDistanceFromModifyOrderMessageAppendageLength, modifyOrderMessageAppendageLengthWriterCount != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries, modifyOrderAppendageWriterStart, modifyOrderAppendageWriterEnd);
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ModifyOrderMessageWriter.ModifyOrderMessageAppendageLengthOptionalRule0(bytes, ref cursor, modifyOrderMessageAppendageLengthWriterStart, cursor.UnsequencedDataPacket_UnsequencedMessage_End, modifyOrderMessageAppendageLengthWriterCount != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries);
        return bytes[start..cursor.Index];
    }
}
