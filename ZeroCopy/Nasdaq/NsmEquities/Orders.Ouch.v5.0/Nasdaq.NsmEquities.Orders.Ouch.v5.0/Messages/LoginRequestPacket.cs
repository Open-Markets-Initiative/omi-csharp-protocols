using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  The SoupBinTCP client must send a Login Request Packet immediately upon establishing a new TCP/IP socket connection to the server
/// </summary>
public static class LoginRequestPacket
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Login Request Packet in bytes.
        /// </summary>
        public const int Size = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeUsername + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizePassword + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeRequestedSession + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeRequestedSequenceNumber;

        /// <summary>
        ///  Gets the Login Request Packet laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Login Request Packet.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Login Request Packet at the start of the buffer, writes its fixed values, and gets the Login Request Packet laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Login Request Packet.
        /// </exception>
        public static ref Layout Initialize(global::System.Span<byte> destination)
        {
            if (Size > destination.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, nameof(destination));
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
            if (Size > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Session username
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Username Username;
        /// <summary>
        ///  Login password
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Password Password;
        /// <summary>
        ///  Specifies the session the client would like to log into, or all blanks to log into the currently active session.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RequestedSession RequestedSession;
        /// <summary>
        ///  Specifies the next sequence number in ASCII the client wants to receive upon connection, or 0 to start receiving the most recently generated message.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RequestedSequenceNumber RequestedSequenceNumber;
    }

    /// <summary>
    ///  The Login Request Packet fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Session username
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Username Username { get; init; }

        /// <summary>
        ///  Login password
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Password Password { get; init; }

        /// <summary>
        ///  Specifies the session the client would like to log into, or all blanks to log into the currently active session.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RequestedSession RequestedSession { get; init; }

        /// <summary>
        ///  Specifies the next sequence number in ASCII the client wants to receive upon connection, or 0 to start receiving the most recently generated message.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RequestedSequenceNumber RequestedSequenceNumber { get; init; }

        /// <summary>
        ///  Creates the value of the Login Request Packet, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeUsername + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizePassword + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeRequestedSession + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeRequestedSequenceNumber;

    /// <summary>
    ///  Writes the Login Request Packet to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Login Request Packet.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
        ref Layout usernameWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        usernameWriterLayout.Username = value.Username;
        usernameWriterLayout.Password = value.Password;
        usernameWriterLayout.RequestedSession = value.RequestedSession;
        usernameWriterLayout.RequestedSequenceNumber = value.RequestedSequenceNumber;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
