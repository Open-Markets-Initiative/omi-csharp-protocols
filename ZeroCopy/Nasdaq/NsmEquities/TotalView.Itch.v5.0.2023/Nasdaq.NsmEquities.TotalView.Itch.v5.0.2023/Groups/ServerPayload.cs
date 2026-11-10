using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

/// <summary>
///  Branch
/// </summary>
public static class ServerPayload
{
    /// <summary>
    ///  Identifies the case of the Server Payload that the bytes select.
    /// </summary>
    public enum Case
    {
        /// <summary>
        ///  The bytes select no case of the Server Payload.
        /// </summary>
        None = 0,
        /// <summary>
        ///  Debug packets are intended to provide human readable text that may aid in debugging problems
        /// </summary>
        DebugPacket = 1,
        /// <summary>
        ///  The SoupBinTCP server sends a Login Accepted Packet in response to receiving a valid Login Request from the client
        /// </summary>
        LoginAcceptedPacket = 2,
        /// <summary>
        ///  The SoupBinTCP server sends this packet in response to an invalid Login Request Packet from the client
        /// </summary>
        LoginRejectedPacket = 3,
        /// <summary>
        ///  The Sequenced Data Packets act as an envelope to carry the actual sequenced data messages that are transferred from the server to the client. Each Sequenced Data Packet carries one message from the higher-lever protocol
        /// </summary>
        SequencedDataPacket = 4,
        /// <summary>
        ///  SoupBinTCP server sends a heartbeat to indicate the connection is alive; carries no payload
        /// </summary>
        ServerHeartbeat = 5,
        /// <summary>
        ///  Mold Udp64 end of session
        /// </summary>
        EndOfSession = 6,
    }

    /// <summary>
    ///  Reads the Server Payload in place.
    /// </summary>
    public readonly ref struct Reader
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int start;
        private readonly int end;
        private readonly ReadOnlySpan<byte> serverPacketTypeValue;
        private readonly ushort packetLengthValue;
        private readonly global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case selectedCase;

        internal Reader(ReadOnlySpan<byte> bytes, int start, int end, ReadOnlySpan<byte> serverPacketTypeValue, ushort packetLengthValue)
        {
            this.bytes = bytes;
            this.start = start;
            this.end = end;
            this.serverPacketTypeValue = serverPacketTypeValue;
            this.packetLengthValue = packetLengthValue;
            this.selectedCase = (global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case)Parser.Valid(Parser.SelectServerPayload(this.bytes, this.start, this.end, this.start, this.serverPacketTypeValue, this.packetLengthValue));
        }

        /// <summary>
        ///  Gets the case of the Server Payload that the bytes select.
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case Case => this.selectedCase;

        /// <summary>
        ///  Gets the bytes of the Server Payload.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureServerPayload(this.bytes, this.start, this.end, this.start, this.serverPacketTypeValue, this.packetLengthValue))];

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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.DebugPacket) throw new global::System.InvalidOperationException(Parser.OtherCase);
                int debugPacketReaderPosition = Parser.Valid(Parser.ServerPayloadDebugPacketStart(this.bytes, this.start, this.end, this.start, this.serverPacketTypeValue, this.packetLengthValue, out var packetLengthValue));
                return new(this.bytes, debugPacketReaderPosition, this.end, packetLengthValue);
            }
        }

        /// <summary>
        ///  The SoupBinTCP server sends a Login Accepted Packet in response to receiving a valid Login Request from the client
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly LoginAcceptedPacket.Layout LoginAcceptedPacket
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.LoginAcceptedPacket) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<LoginAcceptedPacket.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  The SoupBinTCP server sends this packet in response to an invalid Login Request Packet from the client
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly LoginRejectedPacket.Layout LoginRejectedPacket
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.LoginRejectedPacket) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<LoginRejectedPacket.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  The Sequenced Data Packets act as an envelope to carry the actual sequenced data messages that are transferred from the server to the client. Each Sequenced Data Packet carries one message from the higher-lever protocol
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public SequencedDataPacket.Reader SequencedDataPacket
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.SequencedDataPacket) throw new global::System.InvalidOperationException(Parser.OtherCase);
                int sequencedDataPacketReaderPosition = Parser.Valid(Parser.ServerPayloadSequencedDataPacketStart(this.bytes, this.start, this.end, this.start, this.serverPacketTypeValue, this.packetLengthValue, out var packetLengthValue));
                int sequencedDataPacketReaderEnd = Parser.Valid(Parser.ServerPayloadSequencedDataPacketEnd(this.bytes, sequencedDataPacketReaderPosition, this.end, this.start, packetLengthValue));
                return new(this.bytes, sequencedDataPacketReaderPosition, sequencedDataPacketReaderEnd, packetLengthValue);
            }
        }
    }

    /// <summary>
    ///  The Server Payload fields to write.
    /// </summary>
    public readonly ref struct Value
    {
        internal readonly global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case @case;
        internal readonly global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DebugPacket.Value debugPacket;
        internal readonly CaseStorage caseStorage;

        [StructLayout(LayoutKind.Explicit)]
        internal struct CaseStorage
        {
            const int CaseStart = 0;

            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginAcceptedPacket.Value loginAcceptedPacket;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRejectedPacket.Value loginRejectedPacket;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedDataPacket.Value sequencedDataPacket;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerHeartbeat.Value serverHeartbeat;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.EndOfSession.Value endOfSession;
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
                this.@case = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.DebugPacket;
            }
        }

        /// <summary>
        ///  The SoupBinTCP server sends a Login Accepted Packet in response to receiving a valid Login Request from the client
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginAcceptedPacket.Value LoginAcceptedPacket
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterValue);
                this.caseStorage.loginAcceptedPacket = value;
                this.@case = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.LoginAcceptedPacket;
            }
        }

        /// <summary>
        ///  The SoupBinTCP server sends this packet in response to an invalid Login Request Packet from the client
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRejectedPacket.Value LoginRejectedPacket
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterValue);
                this.caseStorage.loginRejectedPacket = value;
                this.@case = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.LoginRejectedPacket;
            }
        }

        /// <summary>
        ///  The Sequenced Data Packets act as an envelope to carry the actual sequenced data messages that are transferred from the server to the client. Each Sequenced Data Packet carries one message from the higher-lever protocol
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedDataPacket.Value SequencedDataPacket
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterValue);
                this.caseStorage.sequencedDataPacket = value;
                this.@case = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.SequencedDataPacket;
            }
        }

        /// <summary>
        ///  SoupBinTCP server sends a heartbeat to indicate the connection is alive; carries no payload
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerHeartbeat.Value ServerHeartbeat
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterValue);
                this.caseStorage.serverHeartbeat = value;
                this.@case = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.ServerHeartbeat;
            }
        }

        /// <summary>
        ///  Mold Udp64 end of session
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.EndOfSession.Value EndOfSession
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterValue);
                this.caseStorage.endOfSession = value;
                this.@case = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.EndOfSession;
            }
        }

        /// <summary>
        ///  The value in the bytes that selects the Debug Packet.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> DebugPacketServerPacketType => [(byte)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPacketType.DebugPacket];

        /// <summary>
        ///  The value in the bytes that selects the Login Accepted Packet.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> LoginAcceptedPacketServerPacketType => [(byte)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPacketType.LoginAcceptedPacket];

        /// <summary>
        ///  The value in the bytes that selects the Login Rejected Packet.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> LoginRejectedPacketServerPacketType => [(byte)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPacketType.LoginRejectedPacket];

        /// <summary>
        ///  The value in the bytes that selects the Sequenced Data Packet.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> SequencedDataPacketServerPacketType => [(byte)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPacketType.SequencedDataPacket];

        /// <summary>
        ///  The value in the bytes that selects the Server Heartbeat.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> ServerHeartbeatServerPacketType => [(byte)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPacketType.ServerHeartbeatPacket];

        /// <summary>
        ///  The value in the bytes that selects the End Of Session.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> EndOfSessionServerPacketType => [(byte)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPacketType.EndOfSessionPacket];

        /// <summary>
        ///  Creates the value of the Server Payload, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    private const int DebugPacketCase = 1;
    private const int LoginAcceptedPacketCase = 2;
    private const int LoginRejectedPacketCase = 3;
    private const int SequencedDataPacketCase = 4;
    private const int ServerHeartbeatCase = 5;
    private const int EndOfSessionCase = 6;

    /// <summary>
    ///  Writes the Server Payload to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Server Payload.
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

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterCursor cursor, int start, scoped ref bool packetLengthDependencyWriteUsed, scoped ref int serverPayloadWriterCaseRecord, scoped in Value value)
    {
        int serverPayloadWriterCase = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent;
        if (value.@case is not (global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.DebugPacket or global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.LoginAcceptedPacket or global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.LoginRejectedPacket or global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.SequencedDataPacket or global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.ServerHeartbeat or global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.EndOfSession)) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterNoCase, nameof(value));
        if (value.@case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.DebugPacket)
        {
            serverPayloadWriterCase = DebugPacketCase;
            int debugPacketWriting = cursor.Index;
            var debugPacketWriteValue = value.debugPacket;
            global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DebugPacket.Write(bytes, ref cursor, debugPacketWriting, ref packetLengthDependencyWriteUsed, in debugPacketWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.LoginAcceptedPacket)
        {
            serverPayloadWriterCase = LoginAcceptedPacketCase;
            int loginAcceptedPacketWriting = cursor.Index;
            var loginAcceptedPacketWriteValue = value.caseStorage.loginAcceptedPacket;
            global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginAcceptedPacket.Write(bytes, ref cursor, loginAcceptedPacketWriting, in loginAcceptedPacketWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.LoginRejectedPacket)
        {
            serverPayloadWriterCase = LoginRejectedPacketCase;
            int loginRejectedPacketWriting = cursor.Index;
            var loginRejectedPacketWriteValue = value.caseStorage.loginRejectedPacket;
            global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRejectedPacket.Write(bytes, ref cursor, loginRejectedPacketWriting, in loginRejectedPacketWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.SequencedDataPacket)
        {
            serverPayloadWriterCase = SequencedDataPacketCase;
            int sequencedDataPacketWriting = cursor.Index;
            var sequencedDataPacketWriteValue = value.caseStorage.sequencedDataPacket;
            global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedDataPacket.Write(bytes, ref cursor, sequencedDataPacketWriting, ref packetLengthDependencyWriteUsed, in sequencedDataPacketWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.ServerHeartbeat)
        {
            serverPayloadWriterCase = ServerHeartbeatCase;
            int serverHeartbeatWriting = cursor.Index;
            var serverHeartbeatWriteValue = value.caseStorage.serverHeartbeat;
            global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerHeartbeat.Write(bytes, ref cursor, serverHeartbeatWriting, in serverHeartbeatWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.EndOfSession)
        {
            serverPayloadWriterCase = EndOfSessionCase;
            int endOfSessionWriting = cursor.Index;
            var endOfSessionWriteValue = value.caseStorage.endOfSession;
            global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.EndOfSession.Write(bytes, ref cursor, endOfSessionWriting, in endOfSessionWriteValue);
        }
        if (!global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref serverPayloadWriterCaseRecord))
        {
            int recorded = serverPayloadWriterCase == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.NoCase : serverPayloadWriterCase;
            if (serverPayloadWriterCaseRecord != global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent && serverPayloadWriterCaseRecord != recorded) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterRuleDisagrees, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterValue);
            serverPayloadWriterCaseRecord = recorded;
        }
        return bytes[start..cursor.Index];
    }
}
