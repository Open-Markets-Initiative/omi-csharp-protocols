using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

/// <summary>
///  The SoupBinTCP server sends a Login Accepted Packet in response to receiving a valid Login Request from the client
/// </summary>
public static class LoginAcceptedPacket
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Login Accepted Packet in bytes.
        /// </summary>
        public const int Size = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeAcceptedSession + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeAcceptedSequenceNumber;

        /// <summary>
        ///  Gets the Login Accepted Packet laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Login Accepted Packet.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Login Accepted Packet at the start of the buffer, writes its fixed values, and gets the Login Accepted Packet laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Login Accepted Packet.
        /// </exception>
        public static ref Layout Initialize(global::System.Span<byte> destination)
        {
            if (Size > destination.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterNoRoom, nameof(destination));
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
            if (Size > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  The session ID of the session that is now logged into. Left padded with spaces.
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AcceptedSession AcceptedSession;
        /// <summary>
        ///  The sequence number in ASCII of the next Sequenced Message to be sent. Left padded with spaces.
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AcceptedSequenceNumber AcceptedSequenceNumber;
    }

    /// <summary>
    ///  The Login Accepted Packet fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  The session ID of the session that is now logged into. Left padded with spaces.
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AcceptedSession AcceptedSession { get; init; }

        /// <summary>
        ///  The sequence number in ASCII of the next Sequenced Message to be sent. Left padded with spaces.
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AcceptedSequenceNumber AcceptedSequenceNumber { get; init; }

        /// <summary>
        ///  Creates the value of the Login Accepted Packet, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeAcceptedSession + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeAcceptedSequenceNumber;

    /// <summary>
    ///  Writes the Login Accepted Packet to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Login Accepted Packet.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterDestination);
        ref Layout acceptedSessionWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        acceptedSessionWriterLayout.AcceptedSession = value.AcceptedSession;
        acceptedSessionWriterLayout.AcceptedSequenceNumber = value.AcceptedSequenceNumber;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
