using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  A control message: key=value fields separated by |, ending on a newline
/// </summary>
public static class ControlMessage
{
    /// <summary>
    ///  Reads the Control Message in place.
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
        ///  Gets the bytes of the Control Message.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..this.end];

        /// <summary>
        ///  ASCII key=value pairs separated by the | character, terminated by a newline character
        /// </summary>
        public ReadOnlySpan<byte> ControlFields
        {
            get
            {
                int controlFieldsReaderPosition = Parser.Valid(Parser.ControlMessageControlFieldsStart(this.bytes, this.start, this.end, this.start));
                int controlFieldsReaderEnd = Parser.Valid(Parser.ControlMessageControlFieldsEnd(this.bytes, controlFieldsReaderPosition, this.end, this.start));
                return this.bytes[controlFieldsReaderPosition..controlFieldsReaderEnd];
            }
        }
    }

    /// <summary>
    ///  The Control Message fields to write.
    /// </summary>
    public readonly ref struct Value
    {
        /// <summary>
        ///  ASCII key=value pairs separated by the | character, terminated by a newline character
        /// </summary>
        public required global::System.ReadOnlySpan<byte> ControlFields { get; init; }

        /// <summary>
        ///  Creates the value of the Control Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    private const byte ControlMessageReassembleRule1Terminator0 = 10;
    private const byte ControlFieldsSizeRule0Terminator0 = 10;

    /// <summary>
    ///  Writes the Control Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Control Message.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        int controlFieldsWriterLength = value.ControlFields.Length + sizeof(byte);
        if (controlFieldsWriterLength > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterNoRoom, global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterDestination);
        int controlFieldsWriterPosition = cursor.Index;
        value.ControlFields.CopyTo(bytes[controlFieldsWriterPosition..]);
        bytes[controlFieldsWriterPosition + value.ControlFields.Length] = ControlFieldsSizeRule0Terminator0;
        cursor.Index += controlFieldsWriterLength;
        global::Databento.MarketData.Normalized.Dbn.v3.Parser.ControlMessageWriter.ControlFieldsSizeRule0(bytes, ref cursor, controlFieldsWriterPosition, controlFieldsWriterPosition + controlFieldsWriterLength);
        if (global::System.MemoryExtensions.IndexOf(bytes[start..cursor.Index], ControlMessageReassembleRule1Terminator0) < 0)
        {
            if (sizeof(byte) > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterNoRoom, global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterDestination);
            bytes[cursor.Index] = ControlMessageReassembleRule1Terminator0;
            cursor.Index += sizeof(byte);
        }
        global::Databento.MarketData.Normalized.Dbn.v3.Parser.ControlMessageWriter.ControlMessageReassembleRule1(bytes, ref cursor, start, cursor.Index);
        return bytes[start..cursor.Index];
    }
}
