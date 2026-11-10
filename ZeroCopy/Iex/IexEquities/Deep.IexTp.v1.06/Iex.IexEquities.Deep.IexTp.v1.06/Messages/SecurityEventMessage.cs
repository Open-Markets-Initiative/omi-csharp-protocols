using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Iex.IexEquities.Deep.IexTp.v1_06;

/// <summary>
///  The Security Event Message is used to indicate events that apply to a security
/// </summary>
public static class SecurityEventMessage
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Security Event Message in bytes.
        /// </summary>
        public const int Size = global::Iex.IexEquities.Deep.IexTp.v1_06.Parser.SizeSecurityEvent + global::Iex.IexEquities.Deep.IexTp.v1_06.Parser.SizeTimestamp + global::Iex.IexEquities.Deep.IexTp.v1_06.Parser.SizeSymbol;

        /// <summary>
        ///  Gets the Security Event Message laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Security Event Message.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Iex.IexEquities.Deep.IexTp.v1_06.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Security Event Message at the start of the buffer, writes its fixed values, and gets the Security Event Message laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Security Event Message.
        /// </exception>
        public static ref Layout Initialize(global::System.Span<byte> destination)
        {
            if (Size > destination.Length) throw new global::System.ArgumentException(global::Iex.IexEquities.Deep.IexTp.v1_06.Parser.WriterNoRoom, nameof(destination));
            destination[..Size].Clear();
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(destination);
        }

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
            if (Size > bytes.Length) throw new global::System.ArgumentException(global::Iex.IexEquities.Deep.IexTp.v1_06.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Security event identifier
        /// </summary>
        public global::Iex.IexEquities.Deep.IexTp.v1_06.SecurityEventField SecurityEvent;
        /// <summary>
        ///  Time stamp of the system event
        /// </summary>
        public global::Iex.IexEquities.Deep.IexTp.v1_06.Timestamp Timestamp;
        /// <summary>
        ///  Security identifier
        /// </summary>
        public global::Iex.IexEquities.Deep.IexTp.v1_06.Symbol Symbol;
    }

    /// <summary>
    ///  The Security Event Message fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Security event identifier
        /// </summary>
        public required global::Iex.IexEquities.Deep.IexTp.v1_06.SecurityEventField SecurityEvent { get; init; }

        /// <summary>
        ///  Time stamp of the system event
        /// </summary>
        public required global::Iex.IexEquities.Deep.IexTp.v1_06.Timestamp Timestamp { get; init; }

        /// <summary>
        ///  Security identifier
        /// </summary>
        public required global::Iex.IexEquities.Deep.IexTp.v1_06.Symbol Symbol { get; init; }

        /// <summary>
        ///  Creates the value of the Security Event Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Iex.IexEquities.Deep.IexTp.v1_06.Parser.SizeSecurityEvent + global::Iex.IexEquities.Deep.IexTp.v1_06.Parser.SizeTimestamp + global::Iex.IexEquities.Deep.IexTp.v1_06.Parser.SizeSymbol;

    /// <summary>
    ///  Writes the Security Event Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Security Event Message.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Iex.IexEquities.Deep.IexTp.v1_06.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Iex.IexEquities.Deep.IexTp.v1_06.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Iex.IexEquities.Deep.IexTp.v1_06.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Iex.IexEquities.Deep.IexTp.v1_06.Parser.WriterNoRoom, global::Iex.IexEquities.Deep.IexTp.v1_06.Parser.WriterDestination);
        ref Layout securityEventWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        securityEventWriterLayout.SecurityEvent = value.SecurityEvent;
        securityEventWriterLayout.Timestamp = value.Timestamp;
        securityEventWriterLayout.Symbol = value.Symbol;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
