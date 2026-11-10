using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Qbbo.Itch.v2_1;

/// <summary>
///  Nasdaq introduced the Reg SHO Short Sale Price Test Restricted Indicator message format.
/// </summary>
public static class RegShoRestrictionMessage
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Reg Sho Restriction Message in bytes.
        /// </summary>
        public const int Size = global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeTrackingNumber + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeTimeStamp + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeStock + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeRegShoAction;

        /// <summary>
        ///  Gets the Reg Sho Restriction Message laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Reg Sho Restriction Message.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Reg Sho Restriction Message at the start of the buffer, writes its fixed values, and gets the Reg Sho Restriction Message laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Reg Sho Restriction Message.
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
        ///  Denotes the security symbol for the issue in the execution system.
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Stock Stock;
        /// <summary>
        ///  Denotes the Reg SHO Short Sale Price Test Restriction status for the issue at the time of the message dissemination.
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.RegShoActionField RegShoAction;
    }

    /// <summary>
    ///  The Reg Sho Restriction Message fields to write.
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
        ///  Denotes the security symbol for the issue in the execution system.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Stock Stock { get; init; }

        /// <summary>
        ///  Denotes the Reg SHO Short Sale Price Test Restriction status for the issue at the time of the message dissemination.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.RegShoActionField RegShoAction { get; init; }

        /// <summary>
        ///  Creates the value of the Reg Sho Restriction Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeTrackingNumber + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeTimeStamp + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeStock + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeRegShoAction;

    /// <summary>
    ///  Writes the Reg Sho Restriction Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Reg Sho Restriction Message.
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
        ref Layout regShoRestrictionMessageTrackingNumberWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        regShoRestrictionMessageTrackingNumberWriterLayout.TrackingNumber = value.TrackingNumber;
        regShoRestrictionMessageTrackingNumberWriterLayout.TimeStamp = value.TimeStamp;
        regShoRestrictionMessageTrackingNumberWriterLayout.Stock = value.Stock;
        regShoRestrictionMessageTrackingNumberWriterLayout.RegShoAction = value.RegShoAction;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
