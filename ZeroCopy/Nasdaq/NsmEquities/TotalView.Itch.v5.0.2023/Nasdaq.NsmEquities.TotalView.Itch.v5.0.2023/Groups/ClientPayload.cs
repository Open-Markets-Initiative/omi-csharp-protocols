using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

/// <summary>
///  Branch
/// </summary>
public static class ClientPayload
{
    /// <summary>
    ///  Identifies the case of the Client Payload that the bytes select.
    /// </summary>
    public enum Case
    {
        /// <summary>
        ///  The bytes select no case of the Client Payload.
        /// </summary>
        None = 0,
        /// <summary>
        ///  Debug packets are intended to provide human readable text that may aid in debugging problems
        /// </summary>
        DebugPacket = 1,
        /// <summary>
        ///  The SoupBinTCP client must send a Login Request Packet immediately upon establishing a new TCP/IP socket connection to the server
        /// </summary>
        LoginRequestPacket = 2,
        /// <summary>
        ///  The Unsequenced Data Packets act as an envelope to carry the actual data messages that are transferred from the client to the server
        /// </summary>
        UnsequencedDataPacket = 3,
        /// <summary>
        ///  SoupBinTCP client sends a heartbeat to indicate the connection is alive; carries no payload
        /// </summary>
        ClientHeartbeat = 4,
        /// <summary>
        ///  SoupBinTCP client requests session logout; server completes any in-flight messages then closes. Carries no payload
        /// </summary>
        LogoutRequest = 5,
    }

    /// <summary>
    ///  Reads the Client Payload in place.
    /// </summary>
    public readonly ref struct Reader
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int start;
        private readonly int end;
        private readonly ReadOnlySpan<byte> clientPacketTypeValue;
        private readonly ushort packetLengthValue;
        private readonly global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case selectedCase;

        internal Reader(ReadOnlySpan<byte> bytes, int start, int end, ReadOnlySpan<byte> clientPacketTypeValue, ushort packetLengthValue)
        {
            this.bytes = bytes;
            this.start = start;
            this.end = end;
            this.clientPacketTypeValue = clientPacketTypeValue;
            this.packetLengthValue = packetLengthValue;
            this.selectedCase = (global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case)Parser.Valid(Parser.SelectClientPayload(this.bytes, this.start, this.end, this.start, this.clientPacketTypeValue, this.packetLengthValue));
        }

        /// <summary>
        ///  Gets the case of the Client Payload that the bytes select.
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case Case => this.selectedCase;

        /// <summary>
        ///  Gets the bytes of the Client Payload.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureClientPayload(this.bytes, this.start, this.end, this.start, this.clientPacketTypeValue, this.packetLengthValue))];

        /// <summary>
        ///  Debug packets are intended to provide human readable text that may aid in debugging problems
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public DebugPacket.Reader DebugPacket
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.DebugPacket) throw new global::System.InvalidOperationException(Parser.OtherCase);
                int debugPacketReaderPosition = Parser.Valid(Parser.ClientPayloadDebugPacketStart(this.bytes, this.start, this.end, this.start, this.clientPacketTypeValue, this.packetLengthValue, out var packetLengthValue));
                return new(this.bytes, debugPacketReaderPosition, this.end, packetLengthValue);
            }
        }

        /// <summary>
        ///  The SoupBinTCP client must send a Login Request Packet immediately upon establishing a new TCP/IP socket connection to the server
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly LoginRequestPacket.Layout LoginRequestPacket
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.LoginRequestPacket) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<LoginRequestPacket.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  The Unsequenced Data Packets act as an envelope to carry the actual data messages that are transferred from the client to the server
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public UnsequencedDataPacket.Reader UnsequencedDataPacket
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.UnsequencedDataPacket) throw new global::System.InvalidOperationException(Parser.OtherCase);
                int unsequencedDataPacketReaderPosition = Parser.Valid(Parser.ClientPayloadUnsequencedDataPacketStart(this.bytes, this.start, this.end, this.start, this.clientPacketTypeValue, this.packetLengthValue, out var packetLengthValue));
                int unsequencedDataPacketReaderEnd = Parser.Valid(Parser.ClientPayloadUnsequencedDataPacketEnd(this.bytes, unsequencedDataPacketReaderPosition, this.end, this.start, packetLengthValue));
                return new(this.bytes, unsequencedDataPacketReaderPosition, unsequencedDataPacketReaderEnd, packetLengthValue);
            }
        }
    }

    /// <summary>
    ///  The Client Payload fields to write.
    /// </summary>
    public readonly ref struct Value
    {
        internal readonly global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case @case;
        internal readonly global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DebugPacket.Value debugPacket;
        internal readonly global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.UnsequencedDataPacket.Value unsequencedDataPacket;
        internal readonly CaseStorage caseStorage;

        [StructLayout(LayoutKind.Explicit)]
        internal struct CaseStorage
        {
            const int CaseStart = 0;

            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRequestPacket.Value loginRequestPacket;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientHeartbeat.Value clientHeartbeat;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LogoutRequest.Value logoutRequest;
        }

        /// <summary>
        ///  Debug packets are intended to provide human readable text that may aid in debugging problems
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DebugPacket.Value DebugPacket
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterValue);
                this.debugPacket = value;
                this.@case = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.DebugPacket;
            }
        }

        /// <summary>
        ///  The SoupBinTCP client must send a Login Request Packet immediately upon establishing a new TCP/IP socket connection to the server
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRequestPacket.Value LoginRequestPacket
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterValue);
                this.caseStorage.loginRequestPacket = value;
                this.@case = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.LoginRequestPacket;
            }
        }

        /// <summary>
        ///  The Unsequenced Data Packets act as an envelope to carry the actual data messages that are transferred from the client to the server
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.UnsequencedDataPacket.Value UnsequencedDataPacket
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterValue);
                this.unsequencedDataPacket = value;
                this.@case = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.UnsequencedDataPacket;
            }
        }

        /// <summary>
        ///  SoupBinTCP client sends a heartbeat to indicate the connection is alive; carries no payload
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientHeartbeat.Value ClientHeartbeat
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterValue);
                this.caseStorage.clientHeartbeat = value;
                this.@case = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.ClientHeartbeat;
            }
        }

        /// <summary>
        ///  SoupBinTCP client requests session logout; server completes any in-flight messages then closes. Carries no payload
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LogoutRequest.Value LogoutRequest
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterValue);
                this.caseStorage.logoutRequest = value;
                this.@case = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.LogoutRequest;
            }
        }

        /// <summary>
        ///  The value in the bytes that selects the Debug Packet.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> DebugPacketClientPacketType => [(byte)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPacketType.DebugPacket];

        /// <summary>
        ///  The value in the bytes that selects the Login Request Packet.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> LoginRequestPacketClientPacketType => [(byte)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPacketType.LoginRequestPacket];

        /// <summary>
        ///  The value in the bytes that selects the Unsequenced Data Packet.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> UnsequencedDataPacketClientPacketType => [(byte)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPacketType.UnsequencedDataPacket];

        /// <summary>
        ///  The value in the bytes that selects the Client Heartbeat.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> ClientHeartbeatClientPacketType => [(byte)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPacketType.ClientHeartbeatPacket];

        /// <summary>
        ///  The value in the bytes that selects the Logout Request.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> LogoutRequestClientPacketType => [(byte)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPacketType.LogoutRequestPacket];

        /// <summary>
        ///  Creates the value of the Client Payload, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    private const int DebugPacketCase = 1;
    private const int LoginRequestPacketCase = 2;
    private const int UnsequencedDataPacketCase = 3;
    private const int ClientHeartbeatCase = 4;
    private const int LogoutRequestCase = 5;

    /// <summary>
    ///  Writes the Client Payload to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Client Payload.
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
        return Write(destination, ref cursor, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Minimum, ref global::System.Runtime.CompilerServices.Unsafe.NullRef<bool>(), ref global::System.Runtime.CompilerServices.Unsafe.NullRef<int>(), in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterCursor cursor, int start, scoped ref bool packetLengthDependencyWriteUsed, scoped ref int clientPayloadWriterCaseRecord, scoped in Value value)
    {
        int clientPayloadWriterCase = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent;
        if (value.@case is not (global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.DebugPacket or global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.LoginRequestPacket or global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.UnsequencedDataPacket or global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.ClientHeartbeat or global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.LogoutRequest)) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterNoCase, nameof(value));
        if (value.@case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.DebugPacket)
        {
            clientPayloadWriterCase = DebugPacketCase;
            int debugPacketWriting = cursor.Index;
            var debugPacketWriteValue = value.debugPacket;
            global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DebugPacket.Write(bytes, ref cursor, debugPacketWriting, ref packetLengthDependencyWriteUsed, in debugPacketWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.LoginRequestPacket)
        {
            clientPayloadWriterCase = LoginRequestPacketCase;
            int loginRequestPacketWriting = cursor.Index;
            var loginRequestPacketWriteValue = value.caseStorage.loginRequestPacket;
            global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRequestPacket.Write(bytes, ref cursor, loginRequestPacketWriting, in loginRequestPacketWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.UnsequencedDataPacket)
        {
            clientPayloadWriterCase = UnsequencedDataPacketCase;
            int unsequencedDataPacketWriting = cursor.Index;
            var unsequencedDataPacketWriteValue = value.unsequencedDataPacket;
            global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.UnsequencedDataPacket.Write(bytes, ref cursor, unsequencedDataPacketWriting, ref packetLengthDependencyWriteUsed, in unsequencedDataPacketWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.ClientHeartbeat)
        {
            clientPayloadWriterCase = ClientHeartbeatCase;
            int clientHeartbeatWriting = cursor.Index;
            var clientHeartbeatWriteValue = value.caseStorage.clientHeartbeat;
            global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientHeartbeat.Write(bytes, ref cursor, clientHeartbeatWriting, in clientHeartbeatWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.LogoutRequest)
        {
            clientPayloadWriterCase = LogoutRequestCase;
            int logoutRequestWriting = cursor.Index;
            var logoutRequestWriteValue = value.caseStorage.logoutRequest;
            global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LogoutRequest.Write(bytes, ref cursor, logoutRequestWriting, in logoutRequestWriteValue);
        }
        if (!global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref clientPayloadWriterCaseRecord))
        {
            int recorded = clientPayloadWriterCase == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.NoCase : clientPayloadWriterCase;
            if (clientPayloadWriterCaseRecord != global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent && clientPayloadWriterCaseRecord != recorded) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterRuleDisagrees, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterValue);
            clientPayloadWriterCaseRecord = recorded;
        }
        return bytes[start..cursor.Index];
    }
}
