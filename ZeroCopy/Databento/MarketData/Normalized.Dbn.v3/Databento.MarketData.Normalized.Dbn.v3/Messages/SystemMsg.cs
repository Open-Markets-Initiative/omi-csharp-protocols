using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  A non-error message from the Databento Live Subscription Gateway (LSG); also used for heartbeating
/// </summary>
public static class SystemMsg
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the System Msg in bytes.
        /// </summary>
        public const int Size = global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMsg + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeSystemCode;

        /// <summary>
        ///  Gets the System Msg laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the System Msg.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the System Msg at the start of the buffer, writes its fixed values, and gets the System Msg laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the System Msg.
        /// </exception>
        public static ref Layout Initialize(global::System.Span<byte> destination)
        {
            if (Size > destination.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterNoRoom, nameof(destination));
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
            if (Size > bytes.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  The message from the Databento Live Subscription Gateway (LSG)
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.Msg Msg;
        /// <summary>
        ///  Type of system message
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.SystemCodeField SystemCode;
    }

    /// <summary>
    ///  The System Msg fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  The message from the Databento Live Subscription Gateway (LSG)
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.Msg Msg { get; init; }

        /// <summary>
        ///  Type of system message
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.SystemCodeField SystemCode { get; init; }

        /// <summary>
        ///  Creates the value of the System Msg, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMsg + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeSystemCode;

    /// <summary>
    ///  Writes the System Msg to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the System Msg.
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
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterNoRoom, global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterDestination);
        ref Layout msgWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        msgWriterLayout.Msg = value.Msg;
        msgWriterLayout.SystemCode = value.SystemCode;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
