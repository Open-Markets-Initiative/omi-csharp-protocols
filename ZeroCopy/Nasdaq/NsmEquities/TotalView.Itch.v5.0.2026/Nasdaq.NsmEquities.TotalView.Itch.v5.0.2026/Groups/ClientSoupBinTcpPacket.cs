using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

/// <summary>
///  Soup Bin Tcp Packet sent by the client
/// </summary>
public static class ClientSoupBinTcpPacket
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
            if (global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.ClientSoupBinTcpPacket.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Packet header of a packet sent by the client
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.ClientPacketHeader.Layout ClientPacketHeader;
    }

    /// <summary>
    ///  Reads the Client Soup Bin Tcp Packet in place.
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
        ///  Gets the bytes of the Client Soup Bin Tcp Packet.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..this.end];

        /// <summary>
        ///  Gets the fixed fields at the start of the Client Soup Bin Tcp Packet.
        /// </summary>
        public ref readonly Layout Layout => ref this.layout;

        /// <summary>
        ///  Packet header of a packet sent by the client
        /// </summary>
        public ref readonly ClientPacketHeader.Layout ClientPacketHeader => ref this.layout.ClientPacketHeader;

        /// <summary>
        ///  Branch
        /// </summary>
        public ClientPayload.Reader ClientPayload
        {
            get
            {
                int clientPayloadReaderPosition = Parser.Valid(Parser.ClientSoupBinTcpPacketClientPayloadStart(this.bytes, this.start, this.end, this.start, out var clientPacketTypeValue, out var packetLengthValue));
                return new(this.bytes, clientPayloadReaderPosition, this.end, clientPacketTypeValue, packetLengthValue);
            }
        }
    }

    /// <summary>
    ///  The Client Soup Bin Tcp Packet fields to write.
    /// </summary>
    public readonly ref struct Value
    {
        /// <summary>
        ///  Packet header of a packet sent by the client
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.ClientPacketHeader.Value ClientPacketHeader { get; init; } = new();

        /// <summary>
        ///  Branch
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.ClientPayload.Value ClientPayload { get; init; } = new();

        /// <summary>
        ///  Creates the value of the Client Soup Bin Tcp Packet, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeClientPacketHeader;
    private const int packetLengthDependencyWriteDistanceFromClientPacketHeader = 0;
    private const int clientPacketTypeDependencyWriteDistanceFromClientPacketHeader = 2;
    private const int DebugPacketCase = 1;
    private const int LoginRequestPacketCase = 2;
    private const int UnsequencedDataPacketCase = 3;
    private const int ClientHeartbeatPacketCase = 4;
    private const int LogoutRequestPacketCase = 5;

    /// <summary>
    ///  Writes the Client Soup Bin Tcp Packet to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Client Soup Bin Tcp Packet.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        int clientPacketHeaderWriterStart = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.Absent;
        int clientPayloadWriterStart = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.Absent;
        int clientPayloadWriterCase = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.Absent;
        bool packetLengthDependencyFirstUsed = false;
        bool clientPacketTypeDependencyFirstUsed = false;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.WriterDestination);
        if (clientPacketHeaderWriterStart == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.Absent) clientPacketHeaderWriterStart = cursor.Index;
        int clientPacketHeaderWriting = cursor.Index;
        var clientPacketHeaderWriteValue = value.ClientPacketHeader;
        global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.ClientPacketHeader.Write(bytes, ref cursor, clientPacketHeaderWriting, ref clientPacketHeaderWriterStart == clientPacketHeaderWriting ? ref packetLengthDependencyFirstUsed : ref global::System.Runtime.CompilerServices.Unsafe.NullRef<bool>(), ref clientPacketHeaderWriterStart == clientPacketHeaderWriting ? ref clientPacketTypeDependencyFirstUsed : ref global::System.Runtime.CompilerServices.Unsafe.NullRef<bool>(), in clientPacketHeaderWriteValue);
        if (clientPacketHeaderWriterStart == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.Absent) clientPacketHeaderWriterStart = cursor.Index;
        if (clientPayloadWriterStart == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.Absent) clientPayloadWriterStart = cursor.Index;
        int clientPayloadWriting = cursor.Index;
        var clientPayloadWriteValue = value.ClientPayload;
        global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.ClientPayload.Write(bytes, ref cursor, clientPayloadWriting, ref packetLengthDependencyFirstUsed, ref clientPayloadWriterCase, in clientPayloadWriteValue);
        if (clientPacketHeaderWriterStart == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.Absent) clientPacketHeaderWriterStart = cursor.Index;
        if (clientPayloadWriterStart == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.Absent) clientPayloadWriterStart = cursor.Index;
        global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.ClientSoupBinTcpPacketWriter.ClientSoupBinTcpPacketReassembleRule1(bytes, ref cursor, clientPacketHeaderWriterStart < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.Absent : clientPacketHeaderWriterStart + packetLengthDependencyWriteDistanceFromClientPacketHeader, ref packetLengthDependencyFirstUsed, start, cursor.Index);
        global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.ClientSoupBinTcpPacketWriter.ClientSoupBinTcpPacketSizeRule2(bytes, ref cursor, clientPacketHeaderWriterStart < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.Absent : clientPacketHeaderWriterStart + packetLengthDependencyWriteDistanceFromClientPacketHeader, ref packetLengthDependencyFirstUsed, start, cursor.Index);
        if (clientPayloadWriterCase == DebugPacketCase) global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.ClientSoupBinTcpPacketWriter.ClientPayloadBranchRule0(bytes, ref cursor, clientPacketHeaderWriterStart < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.Absent : clientPacketHeaderWriterStart + clientPacketTypeDependencyWriteDistanceFromClientPacketHeader, ref clientPacketTypeDependencyFirstUsed, clientPayloadWriterStart);
        if (clientPayloadWriterCase == LoginRequestPacketCase) global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.ClientSoupBinTcpPacketWriter.ClientPayloadBranchRule1(bytes, ref cursor, clientPacketHeaderWriterStart < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.Absent : clientPacketHeaderWriterStart + clientPacketTypeDependencyWriteDistanceFromClientPacketHeader, ref clientPacketTypeDependencyFirstUsed, clientPayloadWriterStart);
        if (clientPayloadWriterCase == UnsequencedDataPacketCase) global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.ClientSoupBinTcpPacketWriter.ClientPayloadBranchRule2(bytes, ref cursor, clientPacketHeaderWriterStart < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.Absent : clientPacketHeaderWriterStart + clientPacketTypeDependencyWriteDistanceFromClientPacketHeader, ref clientPacketTypeDependencyFirstUsed, clientPayloadWriterStart);
        if (clientPayloadWriterCase == ClientHeartbeatPacketCase) global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.ClientSoupBinTcpPacketWriter.ClientPayloadBranchRule3(bytes, ref cursor, clientPacketHeaderWriterStart < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.Absent : clientPacketHeaderWriterStart + clientPacketTypeDependencyWriteDistanceFromClientPacketHeader, ref clientPacketTypeDependencyFirstUsed, clientPayloadWriterStart);
        if (clientPayloadWriterCase == LogoutRequestPacketCase) global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.ClientSoupBinTcpPacketWriter.ClientPayloadBranchRule4(bytes, ref cursor, clientPacketHeaderWriterStart < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.Absent : clientPacketHeaderWriterStart + clientPacketTypeDependencyWriteDistanceFromClientPacketHeader, ref clientPacketTypeDependencyFirstUsed, clientPayloadWriterStart);
        global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.ClientSoupBinTcpPacketWriter.ClientPayloadCases(bytes, ref cursor, clientPacketHeaderWriterStart < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.Absent : clientPacketHeaderWriterStart + clientPacketTypeDependencyWriteDistanceFromClientPacketHeader, ref clientPacketTypeDependencyFirstUsed, clientPayloadWriterStart, clientPayloadWriterCase == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.NoCase ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.Absent : clientPayloadWriterCase);
        return bytes[start..cursor.Index];
    }
}
