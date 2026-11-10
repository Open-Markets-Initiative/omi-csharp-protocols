using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Databento.MarketData.Normalized.Dbn.v2;

/// <summary>
///  The gateway sends this message immediately following the greeting message to authenticate the client (cram: a randomly-generated challenge string)
/// </summary>
public static class ChallengeRequest
{
    /// <summary>
    ///  Reads the Challenge Request in place.
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
        ///  Gets the bytes of the Challenge Request.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureChallengeRequest(this.bytes, this.start, this.end, this.start))];

        /// <summary>
        ///  ASCII key=value pairs separated by the | character, terminated by a newline character
        /// </summary>
        public ReadOnlySpan<byte> ControlFields
        {
            get
            {
                int challengeRequestControlFieldsReaderPosition = Parser.Valid(Parser.ChallengeRequestControlFieldsStart(this.bytes, this.start, this.end, this.start));
                int challengeRequestControlFieldsReaderEnd = Parser.Valid(Parser.ChallengeRequestControlFieldsEnd(this.bytes, challengeRequestControlFieldsReaderPosition, this.end, this.start));
                return this.bytes[challengeRequestControlFieldsReaderPosition..challengeRequestControlFieldsReaderEnd];
            }
        }
    }

    /// <summary>
    ///  The Challenge Request fields to write.
    /// </summary>
    public readonly ref struct Value
    {
        /// <summary>
        ///  ASCII key=value pairs separated by the | character, terminated by a newline character
        /// </summary>
        public required global::System.ReadOnlySpan<byte> ControlFields { get; init; }

        /// <summary>
        ///  Creates the value of the Challenge Request, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    private const byte ControlFieldsSizeRule0Terminator0 = 10;

    /// <summary>
    ///  Writes the Challenge Request to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Challenge Request.
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
        int challengeRequestControlFieldsWriterLength = value.ControlFields.Length + sizeof(byte);
        if (challengeRequestControlFieldsWriterLength > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v2.Parser.WriterNoRoom, global::Databento.MarketData.Normalized.Dbn.v2.Parser.WriterDestination);
        int challengeRequestControlFieldsWriterPosition = cursor.Index;
        value.ControlFields.CopyTo(bytes[challengeRequestControlFieldsWriterPosition..]);
        bytes[challengeRequestControlFieldsWriterPosition + value.ControlFields.Length] = ControlFieldsSizeRule0Terminator0;
        cursor.Index += challengeRequestControlFieldsWriterLength;
        global::Databento.MarketData.Normalized.Dbn.v2.Parser.ChallengeRequestWriter.ChallengeRequestControlFieldsSizeRule0(bytes, ref cursor, challengeRequestControlFieldsWriterPosition, challengeRequestControlFieldsWriterPosition + challengeRequestControlFieldsWriterLength);
        return bytes[start..cursor.Index];
    }
}
