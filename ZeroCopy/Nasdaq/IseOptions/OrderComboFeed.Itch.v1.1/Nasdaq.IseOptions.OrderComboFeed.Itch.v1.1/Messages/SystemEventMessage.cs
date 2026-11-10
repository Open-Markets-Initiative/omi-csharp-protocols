using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

/// <summary>
///  The system event message type is used to signal a market or data feed handler event.
/// </summary>
public static class SystemEventMessage
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the System Event Message in bytes.
        /// </summary>
        public const int Size = global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeTimestamp + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeEventCode + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeCurrentYear + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeCurrentMonth + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeCurrentDay + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeVersion + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeSubVersion;

        /// <summary>
        ///  Gets the System Event Message laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the System Event Message.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the System Event Message at the start of the buffer, writes its fixed values, and gets the System Event Message laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the System Event Message.
        /// </exception>
        public static ref Layout Initialize(global::System.Span<byte> destination)
        {
            if (Size > destination.Length) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterNoRoom, nameof(destination));
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
            if (Size > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  The time, expressed as the number of nanoseconds after midnight.
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Timestamp Timestamp;
        /// <summary>
        ///  Refer to System Event Codes below
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.EventCodeField EventCode;
        /// <summary>
        ///  The current calendar year (example: 2016).
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.CurrentYear CurrentYear;
        /// <summary>
        ///  The current calendar month, with values 1 to 12 inclusive, January=1, etc.
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.CurrentMonth CurrentMonth;
        /// <summary>
        ///  The current calendar day, with values 1 to 31 inclusive.
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.CurrentDay CurrentDay;
        /// <summary>
        ///  Version of this interface. Currently set to 1.
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Version Version;
        /// <summary>
        ///  Sub-version of this interface. Currently set to 0.
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.SubVersion SubVersion;
    }

    /// <summary>
    ///  The System Event Message fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  The time, expressed as the number of nanoseconds after midnight.
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Timestamp Timestamp { get; init; }

        /// <summary>
        ///  Refer to System Event Codes below
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.EventCodeField EventCode { get; init; }

        /// <summary>
        ///  The current calendar year (example: 2016).
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.CurrentYear CurrentYear { get; init; }

        /// <summary>
        ///  The current calendar month, with values 1 to 12 inclusive, January=1, etc.
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.CurrentMonth CurrentMonth { get; init; }

        /// <summary>
        ///  The current calendar day, with values 1 to 31 inclusive.
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.CurrentDay CurrentDay { get; init; }

        /// <summary>
        ///  Version of this interface. Currently set to 1.
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Version Version { get; init; }

        /// <summary>
        ///  Sub-version of this interface. Currently set to 0.
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.SubVersion SubVersion { get; init; }

        /// <summary>
        ///  Creates the value of the System Event Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeTimestamp + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeEventCode + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeCurrentYear + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeCurrentMonth + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeCurrentDay + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeVersion + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeSubVersion;

    /// <summary>
    ///  Writes the System Event Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the System Event Message.
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
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterNoRoom, global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterDestination);
        ref Layout systemEventMessageTimestampWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        systemEventMessageTimestampWriterLayout.Timestamp = value.Timestamp;
        systemEventMessageTimestampWriterLayout.EventCode = value.EventCode;
        systemEventMessageTimestampWriterLayout.CurrentYear = value.CurrentYear;
        systemEventMessageTimestampWriterLayout.CurrentMonth = value.CurrentMonth;
        systemEventMessageTimestampWriterLayout.CurrentDay = value.CurrentDay;
        systemEventMessageTimestampWriterLayout.Version = value.Version;
        systemEventMessageTimestampWriterLayout.SubVersion = value.SubVersion;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
