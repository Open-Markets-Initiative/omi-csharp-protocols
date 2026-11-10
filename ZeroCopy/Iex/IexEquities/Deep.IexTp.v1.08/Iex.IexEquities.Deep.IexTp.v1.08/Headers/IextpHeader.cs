using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Iex.IexEquities.Deep.IexTp.v1_08;

/// <summary>
///  IexTp packet header
/// </summary>
public static class IextpHeader
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Iextp Header in bytes.
        /// </summary>
        public const int Size = global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeVersion + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeReserved + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeMessageProtocolId + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeChannelId + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeSessionId + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizePayloadLength + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeMessageCount + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeStreamOffset + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeFirstMessageSequenceNumber + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeSendTime;

        /// <summary>
        ///  Gets the Iextp Header laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Iextp Header.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Iextp Header at the start of the buffer, writes its fixed values, and gets the Iextp Header laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Iextp Header.
        /// </exception>
        public static ref Layout Initialize(global::System.Span<byte> destination)
        {
            if (Size > destination.Length) throw new global::System.ArgumentException(global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterNoRoom, nameof(destination));
            destination[..Size].Clear();
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(destination[global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.Position.Minimum..]).Version.Encode(global::Iex.IexEquities.Deep.IexTp.v1_08.Version.StaticValue);
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(destination[global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.Position.Minimum..]).MessageProtocolId.Encode(global::Iex.IexEquities.Deep.IexTp.v1_08.MessageProtocolId.StaticValue);
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(destination[global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.Position.Minimum..]).ChannelId.Encode(global::Iex.IexEquities.Deep.IexTp.v1_08.ChannelId.StaticValue);
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
            if (Size > bytes.Length) throw new global::System.ArgumentException(global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Version of transport specification
        /// </summary>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.Version Version;
        /// <summary>
        ///  Reserved byte
        /// </summary>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.Reserved Reserved;
        /// <summary>
        ///  Unique identifier of the higher layer protocol
        /// </summary>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.MessageProtocolId MessageProtocolId;
        /// <summary>
        ///  Identifies the stream of bytes sequenced messages
        /// </summary>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.ChannelId ChannelId;
        /// <summary>
        ///  Identifies the session
        /// </summary>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.SessionId SessionId;
        /// <summary>
        ///  Byte length of the payload
        /// </summary>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.PayloadLength PayloadLength;
        /// <summary>
        ///  Number of messages in the payload
        /// </summary>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.MessageCount MessageCount;
        /// <summary>
        ///  Byte offset of the data stream
        /// </summary>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.StreamOffset StreamOffset;
        /// <summary>
        ///  Sequence of the first message in the segment
        /// </summary>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.FirstMessageSequenceNumber FirstMessageSequenceNumber;
        /// <summary>
        ///  Send time of segment
        /// </summary>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.SendTime SendTime;
    }

    /// <summary>
    ///  The Iextp Header fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Identifies the session
        /// </summary>
        public required global::Iex.IexEquities.Deep.IexTp.v1_08.SessionId SessionId { get; init; }

        /// <summary>
        ///  Byte offset of the data stream
        /// </summary>
        public required global::Iex.IexEquities.Deep.IexTp.v1_08.StreamOffset StreamOffset { get; init; }

        /// <summary>
        ///  Sequence of the first message in the segment
        /// </summary>
        public required global::Iex.IexEquities.Deep.IexTp.v1_08.FirstMessageSequenceNumber FirstMessageSequenceNumber { get; init; }

        /// <summary>
        ///  Send time of segment
        /// </summary>
        public required global::Iex.IexEquities.Deep.IexTp.v1_08.SendTime SendTime { get; init; }

        /// <summary>
        ///  Creates the value of the Iextp Header, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeVersion + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeReserved + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeMessageProtocolId + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeChannelId + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeSessionId + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizePayloadLength + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeMessageCount + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeStreamOffset + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeFirstMessageSequenceNumber + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeSendTime;

    /// <summary>
    ///  Writes the Iextp Header to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Iextp Header.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.Position.Minimum, ref global::System.Runtime.CompilerServices.Unsafe.NullRef<bool>(), ref global::System.Runtime.CompilerServices.Unsafe.NullRef<bool>(), in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterCursor cursor, int start, scoped ref bool payloadLengthDependencyWriteUsed, scoped ref bool messageCountDependencyWriteUsed, scoped in Value value)
    {
        ref bool payloadLengthDependencyParentUsed = ref global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref payloadLengthDependencyWriteUsed) ? ref cursor.IextpHeader_PayloadLength_Used : ref payloadLengthDependencyWriteUsed;
        ref bool messageCountDependencyParentUsed = ref global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref messageCountDependencyWriteUsed) ? ref cursor.IextpHeader_MessageCount_Used : ref messageCountDependencyWriteUsed;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterNoRoom, global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterDestination);
        ref Layout versionWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        versionWriterLayout.Reserved = default;
        versionWriterLayout.SessionId = value.SessionId;
        versionWriterLayout.StreamOffset = value.StreamOffset;
        versionWriterLayout.FirstMessageSequenceNumber = value.FirstMessageSequenceNumber;
        versionWriterLayout.SendTime = value.SendTime;
        global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]).Version.Encode(global::Iex.IexEquities.Deep.IexTp.v1_08.Version.StaticValue);
        global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]).MessageProtocolId.Encode(global::Iex.IexEquities.Deep.IexTp.v1_08.MessageProtocolId.StaticValue);
        global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]).ChannelId.Encode(global::Iex.IexEquities.Deep.IexTp.v1_08.ChannelId.StaticValue);
        cursor.IextpHeader_PayloadLength_Used = false;
        cursor.IextpHeader_MessageCount_Used = false;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
