using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

/// <summary>
///  Debug packets are intended to provide human readable text that may aid in debugging problems
/// </summary>
public static class DebugPacket
{
    /// <summary>
    ///  Reads the Debug Packet in place.
    /// </summary>
    public readonly ref struct Reader
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int start;
        private readonly int end;
        private readonly ushort packetLengthValue;

        internal Reader(ReadOnlySpan<byte> bytes, int start, int end, ushort packetLengthValue)
        {
            this.bytes = bytes;
            this.start = start;
            this.end = end;
            this.packetLengthValue = packetLengthValue;
        }

        /// <summary>
        ///  Gets the bytes of the Debug Packet.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureDebugPacket(this.bytes, this.start, this.end, this.start, this.packetLengthValue))];

        /// <summary>
        ///  Free form human readable text
        /// </summary>
        public ReadOnlySpan<byte> DebugText
        {
            get
            {
                int debugTextReaderPosition = Parser.Valid(Parser.DebugPacketDebugTextStart(this.bytes, this.start, this.end, this.start, this.packetLengthValue, out var packetLengthValue));
                int debugTextReaderEnd = Parser.Valid(Parser.DebugPacketDebugTextEnd(this.bytes, debugTextReaderPosition, this.end, this.start, packetLengthValue));
                return this.bytes[debugTextReaderPosition..debugTextReaderEnd];
            }
        }
    }

    /// <summary>
    ///  The Debug Packet fields to write.
    /// </summary>
    public readonly ref struct Value
    {
        /// <summary>
        ///  Free form human readable text
        /// </summary>
        public required global::System.ReadOnlySpan<byte> DebugText { get; init; }

        /// <summary>
        ///  Creates the value of the Debug Packet, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    private const int packetLengthDependencyWriteDistanceFromDebugText = -3;

    /// <summary>
    ///  Writes the Debug Packet to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Debug Packet.
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
        return Write(destination, ref cursor, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.Minimum, ref global::System.Runtime.CompilerServices.Unsafe.NullRef<bool>(), in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.WriterCursor cursor, int start, scoped ref bool packetLengthDependencyWriteUsed, scoped in Value value)
    {
        int debugTextWriterLength = value.DebugText.Length;
        if (debugTextWriterLength > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.WriterDestination);
        int debugTextWriterPosition = cursor.Index;
        value.DebugText.CopyTo(bytes[debugTextWriterPosition..]);
        cursor.Index += debugTextWriterLength;
        global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.DebugPacketWriter.DebugTextSizeRule0(bytes, ref cursor, debugTextWriterPosition < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.Absent : debugTextWriterPosition + packetLengthDependencyWriteDistanceFromDebugText, ref packetLengthDependencyWriteUsed, debugTextWriterPosition, debugTextWriterPosition + debugTextWriterLength);
        return bytes[start..cursor.Index];
    }
}
