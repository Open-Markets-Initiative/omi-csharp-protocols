using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Soup Bin Tcp Packet sent by the server
/// </summary>
public static class ServerSoupBinTcpPacket
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
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
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerSoupBinTcpPacket.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Packet header of a packet sent by the server
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPacketHeader.Layout ServerPacketHeader;
    }

    /// <summary>
    ///  Reads the Server Soup Bin Tcp Packet in place.
    /// </summary>
    public readonly ref struct Reader
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int start;
        private readonly int end;
        private ref readonly Layout layout => ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(this.bytes[this.start..this.end]);

        internal Reader(ReadOnlySpan<byte> bytes, int start, int end)
        {
            this.bytes = bytes;
            this.start = start;
            this.end = end;
        }

        /// <summary>
        ///  Gets the bytes of the Server Soup Bin Tcp Packet.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..this.end];

        /// <summary>
        ///  Gets the fixed fields at the start of the Server Soup Bin Tcp Packet.
        /// </summary>
        public ref readonly Layout Layout => ref this.layout;

        /// <summary>
        ///  Packet header of a packet sent by the server
        /// </summary>
        public ref readonly ServerPacketHeader.Layout ServerPacketHeader => ref this.layout.ServerPacketHeader;

        /// <summary>
        ///  Branch
        /// </summary>
        public ServerPayload.Reader ServerPayload
        {
            get
            {
                int serverPayloadReaderPosition = Parser.Valid(Parser.ServerSoupBinTcpPacketServerPayloadStart(this.bytes, this.start, this.end, this.start, out var serverPacketTypeValue, out var packetLengthValue));
                return new(this.bytes, serverPayloadReaderPosition, this.end, serverPacketTypeValue, packetLengthValue);
            }
        }
    }

    /// <summary>
    ///  The Server Soup Bin Tcp Packet fields to write.
    /// </summary>
    public readonly ref struct Value
    {
        /// <summary>
        ///  Packet header of a packet sent by the server
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPacketHeader.Value ServerPacketHeader { get; init; } = new();

        /// <summary>
        ///  Branch
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Value ServerPayload { get; init; } = new();

        /// <summary>
        ///  Creates the value of the Server Soup Bin Tcp Packet, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeServerPacketHeader;
    private const int packetLengthDependencyWriteDistanceFromServerPacketHeader = 0;
    private const int serverPacketTypeDependencyWriteDistanceFromServerPacketHeader = 2;
    private const int DebugPacketCase = 1;
    private const int LoginAcceptedPacketCase = 2;
    private const int LoginRejectedPacketCase = 3;
    private const int SequencedDataPacketCase = 4;
    private const int ServerHeartbeatCase = 5;
    private const int EndOfSessionCase = 6;

    /// <summary>
    ///  Writes the Server Soup Bin Tcp Packet to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Server Soup Bin Tcp Packet.
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
        int serverPacketHeaderWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int serverPayloadWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int serverPayloadWriterCase = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        bool packetLengthDependencyFirstUsed = false;
        bool serverPacketTypeDependencyFirstUsed = false;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
        if (serverPacketHeaderWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) serverPacketHeaderWriterStart = cursor.Index;
        int serverPacketHeaderWriting = cursor.Index;
        var serverPacketHeaderWriteValue = value.ServerPacketHeader;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPacketHeader.Write(bytes, ref cursor, serverPacketHeaderWriting, ref serverPacketHeaderWriterStart == serverPacketHeaderWriting ? ref packetLengthDependencyFirstUsed : ref global::System.Runtime.CompilerServices.Unsafe.NullRef<bool>(), ref serverPacketHeaderWriterStart == serverPacketHeaderWriting ? ref serverPacketTypeDependencyFirstUsed : ref global::System.Runtime.CompilerServices.Unsafe.NullRef<bool>(), in serverPacketHeaderWriteValue);
        if (serverPacketHeaderWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) serverPacketHeaderWriterStart = cursor.Index;
        if (serverPayloadWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) serverPayloadWriterStart = cursor.Index;
        int serverPayloadWriting = cursor.Index;
        var serverPayloadWriteValue = value.ServerPayload;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Write(bytes, ref cursor, serverPayloadWriting, ref packetLengthDependencyFirstUsed, ref serverPayloadWriterCase, in serverPayloadWriteValue);
        if (serverPacketHeaderWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) serverPacketHeaderWriterStart = cursor.Index;
        if (serverPayloadWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) serverPayloadWriterStart = cursor.Index;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ServerSoupBinTcpPacketWriter.ServerSoupBinTcpPacketReassembleRule1(bytes, ref cursor, serverPacketHeaderWriterStart < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : serverPacketHeaderWriterStart + packetLengthDependencyWriteDistanceFromServerPacketHeader, ref packetLengthDependencyFirstUsed, start, cursor.Index);
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ServerSoupBinTcpPacketWriter.ServerSoupBinTcpPacketSizeRule2(bytes, ref cursor, serverPacketHeaderWriterStart < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : serverPacketHeaderWriterStart + packetLengthDependencyWriteDistanceFromServerPacketHeader, ref packetLengthDependencyFirstUsed, start, cursor.Index);
        if (serverPayloadWriterCase == DebugPacketCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ServerSoupBinTcpPacketWriter.ServerPayloadBranchRule0(bytes, ref cursor, serverPacketHeaderWriterStart < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : serverPacketHeaderWriterStart + serverPacketTypeDependencyWriteDistanceFromServerPacketHeader, ref serverPacketTypeDependencyFirstUsed, serverPayloadWriterStart);
        if (serverPayloadWriterCase == LoginAcceptedPacketCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ServerSoupBinTcpPacketWriter.ServerPayloadBranchRule1(bytes, ref cursor, serverPacketHeaderWriterStart < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : serverPacketHeaderWriterStart + serverPacketTypeDependencyWriteDistanceFromServerPacketHeader, ref serverPacketTypeDependencyFirstUsed, serverPayloadWriterStart);
        if (serverPayloadWriterCase == LoginRejectedPacketCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ServerSoupBinTcpPacketWriter.ServerPayloadBranchRule2(bytes, ref cursor, serverPacketHeaderWriterStart < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : serverPacketHeaderWriterStart + serverPacketTypeDependencyWriteDistanceFromServerPacketHeader, ref serverPacketTypeDependencyFirstUsed, serverPayloadWriterStart);
        if (serverPayloadWriterCase == SequencedDataPacketCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ServerSoupBinTcpPacketWriter.ServerPayloadBranchRule3(bytes, ref cursor, serverPacketHeaderWriterStart < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : serverPacketHeaderWriterStart + serverPacketTypeDependencyWriteDistanceFromServerPacketHeader, ref serverPacketTypeDependencyFirstUsed, serverPayloadWriterStart);
        if (serverPayloadWriterCase == ServerHeartbeatCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ServerSoupBinTcpPacketWriter.ServerPayloadBranchRule4(bytes, ref cursor, serverPacketHeaderWriterStart < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : serverPacketHeaderWriterStart + serverPacketTypeDependencyWriteDistanceFromServerPacketHeader, ref serverPacketTypeDependencyFirstUsed, serverPayloadWriterStart);
        if (serverPayloadWriterCase == EndOfSessionCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ServerSoupBinTcpPacketWriter.ServerPayloadBranchRule5(bytes, ref cursor, serverPacketHeaderWriterStart < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : serverPacketHeaderWriterStart + serverPacketTypeDependencyWriteDistanceFromServerPacketHeader, ref serverPacketTypeDependencyFirstUsed, serverPayloadWriterStart);
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ServerSoupBinTcpPacketWriter.ServerPayloadCases(bytes, ref cursor, serverPacketHeaderWriterStart < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : serverPacketHeaderWriterStart + serverPacketTypeDependencyWriteDistanceFromServerPacketHeader, ref serverPacketTypeDependencyFirstUsed, serverPayloadWriterStart, serverPayloadWriterCase == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoCase ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : serverPayloadWriterCase);
        return bytes[start..cursor.Index];
    }
}
