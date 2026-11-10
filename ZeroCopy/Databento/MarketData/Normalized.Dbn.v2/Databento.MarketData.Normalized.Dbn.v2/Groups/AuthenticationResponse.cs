using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Databento.MarketData.Normalized.Dbn.v2;

/// <summary>
///  The gateway sends this response following the client's authentication request (success, and error or session_id)
/// </summary>
public static class AuthenticationResponse
{
    /// <summary>
    ///  Reads the Authentication Response in place.
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
        ///  Gets the bytes of the Authentication Response.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureAuthenticationResponse(this.bytes, this.start, this.end, this.start))];

        /// <summary>
        ///  ASCII key=value pairs separated by the | character, terminated by a newline character
        /// </summary>
        public ReadOnlySpan<byte> ControlFields
        {
            get
            {
                int authenticationResponseControlFieldsReaderPosition = Parser.Valid(Parser.AuthenticationResponseControlFieldsStart(this.bytes, this.start, this.end, this.start));
                int authenticationResponseControlFieldsReaderEnd = Parser.Valid(Parser.AuthenticationResponseControlFieldsEnd(this.bytes, authenticationResponseControlFieldsReaderPosition, this.end, this.start));
                return this.bytes[authenticationResponseControlFieldsReaderPosition..authenticationResponseControlFieldsReaderEnd];
            }
        }
    }

    /// <summary>
    ///  The Authentication Response fields to write.
    /// </summary>
    public readonly ref struct Value
    {
        /// <summary>
        ///  ASCII key=value pairs separated by the | character, terminated by a newline character
        /// </summary>
        public required global::System.ReadOnlySpan<byte> ControlFields { get; init; }

        /// <summary>
        ///  Creates the value of the Authentication Response, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    private const byte ControlFieldsSizeRule0Terminator0 = 10;

    /// <summary>
    ///  Writes the Authentication Response to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Authentication Response.
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
        int authenticationResponseControlFieldsWriterLength = value.ControlFields.Length + sizeof(byte);
        if (authenticationResponseControlFieldsWriterLength > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v2.Parser.WriterNoRoom, global::Databento.MarketData.Normalized.Dbn.v2.Parser.WriterDestination);
        int authenticationResponseControlFieldsWriterPosition = cursor.Index;
        value.ControlFields.CopyTo(bytes[authenticationResponseControlFieldsWriterPosition..]);
        bytes[authenticationResponseControlFieldsWriterPosition + value.ControlFields.Length] = ControlFieldsSizeRule0Terminator0;
        cursor.Index += authenticationResponseControlFieldsWriterLength;
        global::Databento.MarketData.Normalized.Dbn.v2.Parser.AuthenticationResponseWriter.AuthenticationResponseControlFieldsSizeRule0(bytes, ref cursor, authenticationResponseControlFieldsWriterPosition, authenticationResponseControlFieldsWriterPosition + authenticationResponseControlFieldsWriterLength);
        return bytes[start..cursor.Index];
    }
}
