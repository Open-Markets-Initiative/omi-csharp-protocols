using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0;

/// <summary>
///  Itch Mold Udp 64 Packet Header
/// </summary>
public static class PacketHeader
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Packet Header in bytes.
        /// </summary>
        public const int Size = global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeSession + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeSequenceNumber + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeMessageCount;

        /// <summary>
        ///  Gets the Packet Header laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Packet Header.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Packet Header at the start of the buffer, writes its fixed values, and gets the Packet Header laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Packet Header.
        /// </exception>
        public static ref Layout Initialize(global::System.Span<byte> destination)
        {
            if (Size > destination.Length) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterNoRoom, nameof(destination));
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
            if (Size > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Identity of the multicast session
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Session Session;
        /// <summary>
        ///  Sequence number of the first message to follow this header
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.SequenceNumber SequenceNumber;
        /// <summary>
        ///  Number of messages to follow this header
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageCount MessageCount;
    }

    /// <summary>
    ///  The Packet Header fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Identity of the multicast session
        /// </summary>
        public required global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Session Session { get; init; }

        /// <summary>
        ///  Sequence number of the first message to follow this header
        /// </summary>
        public required global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.SequenceNumber SequenceNumber { get; init; }

        /// <summary>
        ///  Creates the value of the Packet Header, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeSession + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeSequenceNumber + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeMessageCount;

    /// <summary>
    ///  Writes the Packet Header to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Packet Header.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.Position.Minimum, ref global::System.Runtime.CompilerServices.Unsafe.NullRef<bool>(), in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterCursor cursor, int start, scoped ref bool messageCountDependencyWriteUsed, scoped in Value value)
    {
        ref bool messageCountDependencyParentUsed = ref global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref messageCountDependencyWriteUsed) ? ref cursor.PacketHeader_MessageCount_Used : ref messageCountDependencyWriteUsed;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterNoRoom, global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterDestination);
        ref Layout sessionWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        sessionWriterLayout.Session = value.Session;
        sessionWriterLayout.SequenceNumber = value.SequenceNumber;
        cursor.PacketHeader_MessageCount_Used = false;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
