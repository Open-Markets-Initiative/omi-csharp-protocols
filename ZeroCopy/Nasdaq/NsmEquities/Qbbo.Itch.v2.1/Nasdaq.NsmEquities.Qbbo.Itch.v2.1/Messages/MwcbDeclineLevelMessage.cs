using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Qbbo.Itch.v2_1;

/// <summary>
///  Informs data recipients what the daily MWCB breach points are set to for the current trading day.
/// </summary>
public static class MwcbDeclineLevelMessage
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Mwcb Decline Level Message in bytes.
        /// </summary>
        public const int Size = global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeTrackingNumber + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeTimeStamp + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeLevel1 + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeLevel2 + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeLevel3;

        /// <summary>
        ///  Gets the Mwcb Decline Level Message laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Mwcb Decline Level Message.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Mwcb Decline Level Message at the start of the buffer, writes its fixed values, and gets the Mwcb Decline Level Message laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Mwcb Decline Level Message.
        /// </exception>
        public static ref Layout Initialize(global::System.Span<byte> destination)
        {
            if (Size > destination.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterNoRoom, nameof(destination));
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
            if (Size > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Nasdaq internal tracking number
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.TrackingNumber TrackingNumber;
        /// <summary>
        ///  Timestamp
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.TimeStamp TimeStamp;
        /// <summary>
        ///  Denotes the MWCB Level 1 Value.
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Level1 Level1;
        /// <summary>
        ///  Denotes the MWCB Level 2 Value.
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Level2 Level2;
        /// <summary>
        ///  Denotes the MWCB Level 3 Value.
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Level3 Level3;
    }

    /// <summary>
    ///  The Mwcb Decline Level Message fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Nasdaq internal tracking number
        /// </summary>
        public required global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.TrackingNumber TrackingNumber { get; init; }

        /// <summary>
        ///  Timestamp
        /// </summary>
        public required global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.TimeStamp TimeStamp { get; init; }

        /// <summary>
        ///  Denotes the MWCB Level 1 Value.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Level1 Level1 { get; init; }

        /// <summary>
        ///  Denotes the MWCB Level 2 Value.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Level2 Level2 { get; init; }

        /// <summary>
        ///  Denotes the MWCB Level 3 Value.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Level3 Level3 { get; init; }

        /// <summary>
        ///  Creates the value of the Mwcb Decline Level Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeTrackingNumber + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeTimeStamp + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeLevel1 + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeLevel2 + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeLevel3;

    /// <summary>
    ///  Writes the Mwcb Decline Level Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Mwcb Decline Level Message.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterDestination);
        ref Layout mwcbDeclineLevelMessageTrackingNumberWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        mwcbDeclineLevelMessageTrackingNumberWriterLayout.TrackingNumber = value.TrackingNumber;
        mwcbDeclineLevelMessageTrackingNumberWriterLayout.TimeStamp = value.TimeStamp;
        mwcbDeclineLevelMessageTrackingNumberWriterLayout.Level1 = value.Level1;
        mwcbDeclineLevelMessageTrackingNumberWriterLayout.Level2 = value.Level2;
        mwcbDeclineLevelMessageTrackingNumberWriterLayout.Level3 = value.Level3;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
