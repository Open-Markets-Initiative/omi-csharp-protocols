using System.Text;
using System.Buffers.Binary;
using static Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Rules;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

/// <summary>
///  SoupBinTCP Packet sent by the client
/// </summary>
public sealed class ClientPacket : IPacket
{
    IHeader? IPacket.Header => null;

    /// <summary>
    ///  Gets the Client Soup Bin Tcp Packet entries, in wire order.
    /// </summary>
    public List<ClientSoupBinTcpPacketFrame> ClientSoupBinTcpPacketFrames { get; } = [];

    /// <summary>
    ///  Adds a Client Soup Bin Tcp Packet entry that carries the message.
    /// </summary>
    /// <param name="message">The message that the entry carries.</param>
    /// <returns>The new entry, whose header fields the caller can set.</returns>
    /// <exception cref="ArgumentNullException">The value is <see langword="null"/>.</exception>
    public ClientSoupBinTcpPacketFrame Add(IClientMessage message)
    {
        ArgumentNullException.ThrowIfNull(message);
        var entry = new ClientSoupBinTcpPacketFrame { ClientPayload = message };
        ClientSoupBinTcpPacketFrames.Add(entry);
        return entry;
    }

    /// <summary>
    ///  Adds a Client Soup Bin Tcp Packet entry of the Client Heartbeat Packet kind, which carries no message.
    /// </summary>
    /// <returns>The new entry, whose header fields the caller can set.</returns>
    public ClientSoupBinTcpPacketFrame AddClientHeartbeatPacket()
    {
        var entry = new ClientSoupBinTcpPacketFrame();
        entry.ClientPayloadKind = ClientPayloadKind.ClientHeartbeatPacket;
        ClientSoupBinTcpPacketFrames.Add(entry);
        return entry;
    }

    /// <summary>
    ///  Adds a Client Soup Bin Tcp Packet entry of the Logout Request Packet kind, which carries no message.
    /// </summary>
    /// <returns>The new entry, whose header fields the caller can set.</returns>
    public ClientSoupBinTcpPacketFrame AddLogoutRequestPacket()
    {
        var entry = new ClientSoupBinTcpPacketFrame();
        entry.ClientPayloadKind = ClientPayloadKind.LogoutRequestPacket;
        ClientSoupBinTcpPacketFrames.Add(entry);
        return entry;
    }

    /// <summary>
    ///  Gets the messages of the packet, in wire order.
    /// </summary>
    /// <remarks>
    ///  The list reads the packet entries each time that it is used, so it shows each change to them. Add or remove messages through the entries.
    /// </remarks>
    public ClientFramedMessages Messages { get; }

    IReadOnlyList<IMessage> IPacket.Messages => Messages;

    /// <summary>
    ///  Gets a value indicating whether the header and every message of the packet parsed, with no message missing.
    /// </summary>
    /// <remarks>
    ///  Unlike <see cref="IsValid"/>, this property keeps the result of the parse when a value changes. It does not test whether the protocol defines each value. A packet with an unknown message type or an undefined value can be parsed. The value is <see langword="true"/> for a packet that is created in code rather than parsed.
    /// </remarks>
    public bool IsParsed { get; private set; } = true;

    /// <summary>
    ///  Gets a value indicating whether the protocol specification allows every value of the header and of each message.
    /// </summary>
    /// <remarks>
    ///  The value is computed from the current values each time that it is read. It is <see langword="true"/> when <see cref="Validate"/> reports no failures. It is <see langword="false"/> for a message of a type that the protocol does not define. It does not show whether the parse succeeded. A field that the parse did not reach holds its cleared value, so check <see cref="IsParsed"/> first.
    /// </remarks>
    public bool IsValid => Validate().Count == 0;

    ReadOnlyMemory<byte> IPacket.Trailer => ReadOnlyMemory<byte>.Empty;

    /// <summary>
    ///  Initializes a new instance of the <see cref="ClientPacket"/> class with default values and no messages.
    /// </summary>
    public ClientPacket()
    {
        Messages = new ClientFramedMessages(this);
        Clear();
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(ClientPacket other)
    {
        ArgumentNullException.ThrowIfNull(other);
        if (ReferenceEquals(this, other))
            return;
        this.ClientSoupBinTcpPacketFrames.Clear();
        this.ClientSoupBinTcpPacketFrames.AddRange(other.ClientSoupBinTcpPacketFrames.Select(static entry => entry.Copy()));
        Messages.KeepCopy(other.Messages);
        this.IsParsed = other.IsParsed;
    }

    /// <summary>
    ///  Resets all values, the bytes, and the parse result to their defaults.
    /// </summary>
    public void Clear()
    {
        this.ClientSoupBinTcpPacketFrames.Clear();
        Messages.Keep(null);
        this.IsParsed = true;
    }

    /// <summary>
    ///  Parses a packet from the bytes.
    /// </summary>
    /// <returns>The packet. Check <see cref="IsParsed"/> for the result of the parse, and <see cref="IsValid"/> for whether the protocol allows every value.</returns>
    public static ClientPacket Parse(ReadOnlySpan<byte> data)
    {
        var packet = new ClientPacket();
        packet.ParseFrom(data);
        return packet;
    }

    /// <summary>
    ///  Parses the bytes into this instance, starting at the first byte.
    /// </summary>
    public void ParseFrom(ReadOnlySpan<byte> data)
    {
        Clear();
        var messages = new ClientFramedMessages(data, this);
        this.IsParsed = messages.Status == ParseStatus.Ok && messages.IsComplete;
        Messages.Keep(messages);
    }

    /// <summary>
    ///  Gets the length of the Client Packet frame at the start of the bytes.
    /// </summary>
    /// <param name="source">The bytes, from the start of the frame.</param>
    /// <param name="length">The number of bytes of the frame, which can be more than the bytes hold.</param>
    /// <returns><see langword="true"/> when the bytes hold the fields that state the length; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">The bytes state a frame length that is not valid.</exception>
    public static bool TryGetFrameLength(ReadOnlySpan<byte> source, out int length)
    {
        const string FrameLengthInvalid = "The bytes state a frame length that is not valid.";
        const int packetLengthDependencyDistanceFromClientSoupBinTcpPacket = 0;
        const int packetLengthDependencyReadWidth = 2;
        global::System.Int128 ClientSoupBinTcpPacketReassembleRule1Width;
        ushort packetLengthValue;
        length = 0;
        const long ClientSoupBinTcpPacketReassembleRule1Data2 = 2;
        {
            if (source.Length < packetLengthDependencyDistanceFromClientSoupBinTcpPacket + packetLengthDependencyReadWidth) return false;
            packetLengthValue = BinaryPrimitives.ReadUInt16BigEndian(source.Slice(packetLengthDependencyDistanceFromClientSoupBinTcpPacket, packetLengthDependencyReadWidth));
            ClientSoupBinTcpPacketReassembleRule1Width = checked((checked((global::System.Int128)(packetLengthValue)) + ((global::System.Int128)(ClientSoupBinTcpPacketReassembleRule1Data2))));
        }
        if (ClientSoupBinTcpPacketReassembleRule1Width <= RuleEmptyWidth || ClientSoupBinTcpPacketReassembleRule1Width > int.MaxValue) throw new global::System.ArgumentOutOfRangeException(nameof(source), FrameLengthInvalid);
        length = (int)ClientSoupBinTcpPacketReassembleRule1Width;
        return true;
    }

    internal IEnumerable<IClientMessage> Collect()
    {
        foreach (var entry in this.ClientSoupBinTcpPacketFrames)
        {
            foreach (var message in entry.Collect())
            {
                yield return message;
            }
        }
        yield break;
    }

    private static global::System.ReadOnlySpan<byte> ClientHeartbeatPacketClientPacketType => "R"u8;
    private static global::System.ReadOnlySpan<byte> LogoutRequestPacketClientPacketType => "O"u8;
    private const int PacketLengthLength = 2;
    private static readonly FieldDescriptor PacketLengthField = new("PacketLength", FieldKind.UnsignedInteger, PacketLengthLength) { ByteOrder = "Big", IsSigned = false };
    private const int ClientPacketTypeLength = 1;
    private static readonly FieldDescriptor ClientPacketTypeField = new("ClientPacketType", FieldKind.Enum, ClientPacketTypeLength);

    /// <summary>
    ///  Gets the number of bytes that Encode writes, including variable-length content and each omittable field that is present.
    /// </summary>
    public int EncodedByteLength
    {
        get
        {
            var length = 0;
            var clientSoupBinTcpPacketMeasuredEntries = this.ClientSoupBinTcpPacketFrames;
            var clientSoupBinTcpPacketMeasuredFirst = 0;
            while (clientSoupBinTcpPacketMeasuredFirst < clientSoupBinTcpPacketMeasuredEntries.Count)
            {
                var clientSoupBinTcpPacketMeasuredFrame = clientSoupBinTcpPacketMeasuredEntries[clientSoupBinTcpPacketMeasuredFirst];
                length += ClientPacketHeader.HeaderByteLength;
                if (clientSoupBinTcpPacketMeasuredFrame.ClientPayloadKind == ClientPayloadKind.Messages && clientSoupBinTcpPacketMeasuredFrame.ClientPayload is DebugPacket or LoginRequestPacket or UnsequencedDataPacket)
                {
                    length += ((IMessage)clientSoupBinTcpPacketMeasuredFrame.ClientPayload!).EncodedByteLength + ((IMessage)clientSoupBinTcpPacketMeasuredFrame.ClientPayload!).Trailer.Length;
                }
                else if (clientSoupBinTcpPacketMeasuredFrame.ClientPayloadKind == ClientPayloadKind.ClientHeartbeatPacket)
                {
                }
                else if (clientSoupBinTcpPacketMeasuredFrame.ClientPayloadKind == ClientPayloadKind.LogoutRequestPacket)
                {
                }
                else if (clientSoupBinTcpPacketMeasuredFrame.ClientPayload is UnknownClientMessage)
                {
                    length += ((IMessage)clientSoupBinTcpPacketMeasuredFrame.ClientPayload!).EncodedByteLength + ((IMessage)clientSoupBinTcpPacketMeasuredFrame.ClientPayload!).Trailer.Length;
                }
                clientSoupBinTcpPacketMeasuredFirst++;
            }
            return length;
        }
    }

    /// <summary>
    ///  Encodes the packet into the start of the span.
    /// </summary>
    /// <returns>The number of bytes written.</returns>
    /// <exception cref="EncodeException">A value does not fit its field.</exception>
    /// <exception cref="ArgumentException">The span is too small for the encoded bytes.</exception>
    public int Encode(Span<byte> data)
    {
        var length = EncodedByteLength;
        if (data.Length < length)
        {
            throw new ArgumentException("The span is too small for the encoded bytes.", nameof(data));
        }
        if (!Write(data, length, out var cursor, out var failure))
        {
            throw new EncodeException(failure);
        }
        return cursor;
    }

    /// <summary>
    ///  Encodes this instance into a new array.
    /// </summary>
    /// <exception cref="EncodeException">A value does not fit its field.</exception>
    public byte[] ToBytes()
    {
        var buffer = new byte[EncodedByteLength];
        Encode(buffer);
        return buffer;
    }

    /// <summary>
    ///  Tries to encode the packet into the span.
    /// </summary>
    /// <param name="data">The span that receives the bytes from its first byte.</param>
    /// <param name="written">When this method returns, contains the number of bytes written, or 0 if encoding failed.</param>
    /// <param name="failure">When this method returns <see langword="false"/>, contains the reason and the first field whose value cannot be encoded, if any. It contains <see cref="EncodeStatus.BufferTooSmall"/> if the span is shorter than the encoded bytes.</param>
    /// <returns><see langword="true"/> if the packet was written, or <see langword="false"/> if a value cannot be encoded or the span is too small.</returns>
    /// <remarks>
    ///  The field is in the header or in a message, or is a length. For a field in a group entry or nested message, the failure also names the group or field holding it. If a nested message has a trailer, the reason is <see cref="EncodeStatus.NestedTrailer"/>, and the field is the one holding the message.
    /// </remarks>
    public bool TryEncode(Span<byte> data, out int written, out EncodeFailure failure)
    {
        written = 0;
        var length = EncodedByteLength;
        if (data.Length < length)
        {
            failure = new EncodeFailure(EncodeStatus.BufferTooSmall);
            return false;
        }
        if (!Write(data, length, out var cursor, out failure))
        {
            return false;
        }
        written = cursor;
        return true;
    }

    private bool Write(Span<byte> data, int length, out int cursor, out EncodeFailure failure)
    {
        cursor = 0;
        var index = 0;
        const int clientPacketTypeDependencyDistanceFromClientPacketHeader = 2;
        const int clientPacketTypeDependencyReadWidth = 1;
        const int packetLengthDependencyDistanceFromClientPacketHeader = 0;
        const int packetLengthDependencyReadWidth = 2;
        var clientPacketWriterStart = cursor;
        var clientSoupBinTcpPacketWriterPosition = cursor;
        var clientSoupBinTcpPacketMeasuredBytes = 0;
        {
            var clientSoupBinTcpPacketMeasuredEntries = this.ClientSoupBinTcpPacketFrames;
            var clientSoupBinTcpPacketMeasuredFirst = 0;
            while (clientSoupBinTcpPacketMeasuredFirst < clientSoupBinTcpPacketMeasuredEntries.Count)
            {
                var clientSoupBinTcpPacketMeasuredFrame = clientSoupBinTcpPacketMeasuredEntries[clientSoupBinTcpPacketMeasuredFirst];
                clientSoupBinTcpPacketMeasuredBytes += ClientPacketHeader.HeaderByteLength;
                if (clientSoupBinTcpPacketMeasuredFrame.ClientPayloadKind == ClientPayloadKind.Messages && clientSoupBinTcpPacketMeasuredFrame.ClientPayload is DebugPacket or LoginRequestPacket or UnsequencedDataPacket)
                {
                    clientSoupBinTcpPacketMeasuredBytes += ((IMessage)clientSoupBinTcpPacketMeasuredFrame.ClientPayload!).EncodedByteLength + ((IMessage)clientSoupBinTcpPacketMeasuredFrame.ClientPayload!).Trailer.Length;
                }
                else if (clientSoupBinTcpPacketMeasuredFrame.ClientPayloadKind == ClientPayloadKind.ClientHeartbeatPacket)
                {
                }
                else if (clientSoupBinTcpPacketMeasuredFrame.ClientPayloadKind == ClientPayloadKind.LogoutRequestPacket)
                {
                }
                else if (clientSoupBinTcpPacketMeasuredFrame.ClientPayload is UnknownClientMessage)
                {
                    clientSoupBinTcpPacketMeasuredBytes += ((IMessage)clientSoupBinTcpPacketMeasuredFrame.ClientPayload!).EncodedByteLength + ((IMessage)clientSoupBinTcpPacketMeasuredFrame.ClientPayload!).Trailer.Length;
                }
                clientSoupBinTcpPacketMeasuredFirst++;
            }
        }
        var clientSoupBinTcpPacketWriterEntries = this.ClientSoupBinTcpPacketFrames;
        var clientSoupBinTcpPacketIndex = 0;
        while (clientSoupBinTcpPacketIndex < clientSoupBinTcpPacketWriterEntries.Count)
        {
            var clientSoupBinTcpPacketWriterFrame = clientSoupBinTcpPacketWriterEntries[clientSoupBinTcpPacketIndex];
            var clientSoupBinTcpPacketWriterStart = cursor;
            var clientSoupBinTcpPacketOccurrenceBytes = 0;
            {
                clientSoupBinTcpPacketOccurrenceBytes += ClientPacketHeader.HeaderByteLength;
                if (clientSoupBinTcpPacketWriterFrame.ClientPayloadKind == ClientPayloadKind.Messages && clientSoupBinTcpPacketWriterFrame.ClientPayload is DebugPacket or LoginRequestPacket or UnsequencedDataPacket)
                {
                    clientSoupBinTcpPacketOccurrenceBytes += ((IMessage)clientSoupBinTcpPacketWriterFrame.ClientPayload!).EncodedByteLength + ((IMessage)clientSoupBinTcpPacketWriterFrame.ClientPayload!).Trailer.Length;
                }
                else if (clientSoupBinTcpPacketWriterFrame.ClientPayloadKind == ClientPayloadKind.ClientHeartbeatPacket)
                {
                }
                else if (clientSoupBinTcpPacketWriterFrame.ClientPayloadKind == ClientPayloadKind.LogoutRequestPacket)
                {
                }
                else if (clientSoupBinTcpPacketWriterFrame.ClientPayload is UnknownClientMessage)
                {
                    clientSoupBinTcpPacketOccurrenceBytes += ((IMessage)clientSoupBinTcpPacketWriterFrame.ClientPayload!).EncodedByteLength + ((IMessage)clientSoupBinTcpPacketWriterFrame.ClientPayload!).Trailer.Length;
                }
            }
            global::System.Int128 packetLengthStampedValue;
            {
                const long ClientSoupBinTcpPacketReassembleRule1Data2 = 2;
                global::System.Int128 measure = clientSoupBinTcpPacketOccurrenceBytes;
                try
                {
                    packetLengthStampedValue = checked((measure - checked((((global::System.Int128)(RuleNumberZero)) + ((global::System.Int128)(ClientSoupBinTcpPacketReassembleRule1Data2))))));
                }
                catch (global::System.OverflowException)
                {
                    failure = new EncodeFailure(PacketLengthField, EncodeStatus.RuleMismatch, index);
                    return false;
                }
            }
            if (packetLengthStampedValue > ushort.MaxValue)
            {
                failure = new EncodeFailure(PacketLengthField, EncodeStatus.TooLong, index);
                return false;
            }
            if (packetLengthStampedValue < ushort.MinValue)
            {
                failure = new EncodeFailure(PacketLengthField, EncodeStatus.DoesNotFit, index);
                return false;
            }
            var clientPacketHeaderWriterStart = cursor;
            var clientPacketHeaderWriting = clientSoupBinTcpPacketWriterFrame.ClientPacketHeader;
            if (!((IHeader)clientPacketHeaderWriting).Write(data, cursor, out cursor, out failure))
            {
                failure = failure.InMessage(index);
                return false;
            }
            var clientPayloadWriterStart = cursor;
            {
                const int ClientPayloadNoCase = 0;
                const int ClientPayloadBranchRule0Case = 1;
                const int ClientPayloadBranchRule1Case = 2;
                const int ClientPayloadBranchRule2Case = 3;
                const int ClientPayloadBranchRule3Case = 4;
                const int ClientPayloadBranchRule4Case = 5;
                const int ClientPayloadNothingWritten = -1;
                var clientPayloadWrittenCase = ClientPayloadNothingWritten;
                if (clientSoupBinTcpPacketWriterFrame.ClientPayloadKind == ClientPayloadKind.Messages && clientSoupBinTcpPacketWriterFrame.ClientPayload is DebugPacket)
                {
                    var debugPacketWriterStart = cursor;
                    var debugPacketWriting = (DebugPacket)clientSoupBinTcpPacketWriterFrame.ClientPayload!;
                    {
                        ushort packetLengthValue;
                        packetLengthValue = checked((ushort)(packetLengthStampedValue));
                        if (!debugPacketWriting.Write(data, cursor, trailerFollows: true, checked((global::System.Int128?)(packetLengthValue)), out cursor, out failure))
                        {
                            failure = failure.InMessage(index);
                            return false;
                        }
                    }
                    debugPacketWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += debugPacketWriting.Trailer.Length;
                    index++;
                    data.Slice(clientPacketHeaderWriterStart + clientPacketTypeDependencyDistanceFromClientPacketHeader, clientPacketTypeDependencyReadWidth)[0] = unchecked((byte)(((IClientMessage)debugPacketWriting).DataValue));
                    {
                        var clientPacketTypeStamped = (ClientPacketType)data.Slice(clientPacketHeaderWriterStart + clientPacketTypeDependencyDistanceFromClientPacketHeader, clientPacketTypeDependencyReadWidth)[0];
                        if (clientSoupBinTcpPacketWriterFrame.ClientPacketHeader.clientPacketType != clientPacketTypeStamped)
                        {
                            clientSoupBinTcpPacketWriterFrame.ClientPacketHeader.clientPacketType = clientPacketTypeStamped;
                            ((IFieldOwner)clientSoupBinTcpPacketWriterFrame.ClientPacketHeader).Touch();
                        }
                    }
                }
                else if (clientSoupBinTcpPacketWriterFrame.ClientPayloadKind == ClientPayloadKind.Messages && clientSoupBinTcpPacketWriterFrame.ClientPayload is LoginRequestPacket)
                {
                    var loginRequestPacketWriterStart = cursor;
                    var loginRequestPacketWriting = (LoginRequestPacket)clientSoupBinTcpPacketWriterFrame.ClientPayload!;
                    if (!((IMessage)loginRequestPacketWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    loginRequestPacketWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += loginRequestPacketWriting.Trailer.Length;
                    index++;
                    data.Slice(clientPacketHeaderWriterStart + clientPacketTypeDependencyDistanceFromClientPacketHeader, clientPacketTypeDependencyReadWidth)[0] = unchecked((byte)(((IClientMessage)loginRequestPacketWriting).DataValue));
                    {
                        var clientPacketTypeStamped = (ClientPacketType)data.Slice(clientPacketHeaderWriterStart + clientPacketTypeDependencyDistanceFromClientPacketHeader, clientPacketTypeDependencyReadWidth)[0];
                        if (clientSoupBinTcpPacketWriterFrame.ClientPacketHeader.clientPacketType != clientPacketTypeStamped)
                        {
                            clientSoupBinTcpPacketWriterFrame.ClientPacketHeader.clientPacketType = clientPacketTypeStamped;
                            ((IFieldOwner)clientSoupBinTcpPacketWriterFrame.ClientPacketHeader).Touch();
                        }
                    }
                }
                else if (clientSoupBinTcpPacketWriterFrame.ClientPayloadKind == ClientPayloadKind.Messages && clientSoupBinTcpPacketWriterFrame.ClientPayload is UnsequencedDataPacket)
                {
                    var unsequencedDataPacketWriterStart = cursor;
                    var unsequencedDataPacketWriting = (UnsequencedDataPacket)clientSoupBinTcpPacketWriterFrame.ClientPayload!;
                    {
                        ushort packetLengthValue;
                        packetLengthValue = checked((ushort)(packetLengthStampedValue));
                        if (!unsequencedDataPacketWriting.Write(data, cursor, trailerFollows: true, checked((global::System.Int128?)(packetLengthValue)), checked((global::System.Int128?)(packetLengthValue)), out cursor, out failure))
                        {
                            failure = failure.InMessage(index);
                            return false;
                        }
                    }
                    unsequencedDataPacketWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += unsequencedDataPacketWriting.Trailer.Length;
                    index++;
                    data.Slice(clientPacketHeaderWriterStart + clientPacketTypeDependencyDistanceFromClientPacketHeader, clientPacketTypeDependencyReadWidth)[0] = unchecked((byte)(((IClientMessage)unsequencedDataPacketWriting).DataValue));
                    {
                        var clientPacketTypeStamped = (ClientPacketType)data.Slice(clientPacketHeaderWriterStart + clientPacketTypeDependencyDistanceFromClientPacketHeader, clientPacketTypeDependencyReadWidth)[0];
                        if (clientSoupBinTcpPacketWriterFrame.ClientPacketHeader.clientPacketType != clientPacketTypeStamped)
                        {
                            clientSoupBinTcpPacketWriterFrame.ClientPacketHeader.clientPacketType = clientPacketTypeStamped;
                            ((IFieldOwner)clientSoupBinTcpPacketWriterFrame.ClientPacketHeader).Touch();
                        }
                    }
                }
                else if (clientSoupBinTcpPacketWriterFrame.ClientPayloadKind == ClientPayloadKind.ClientHeartbeatPacket)
                {
                    ReadOnlySpan<byte> clientPacketTypeStampedValue;
                    {
                        global::System.ReadOnlySpan<byte> ClientPayloadBranchRule3Data2 = ClientHeartbeatPacketClientPacketType;
                        clientPacketTypeStampedValue = ClientPayloadBranchRule3Data2;
                    }
                    if (clientPacketTypeStampedValue.Length > ClientPacketTypeLength)
                    {
                        failure = new EncodeFailure(ClientPacketTypeField, EncodeStatus.TooLong, index);
                        return false;
                    }
                    if (clientPacketTypeStampedValue.Length != ClientPacketTypeLength)
                    {
                        failure = new EncodeFailure(ClientPacketTypeField, EncodeStatus.DoesNotFit, index);
                        return false;
                    }
                    var clientHeartbeatPacketWriterStart = cursor;
                    clientPacketTypeStampedValue.CopyTo(data.Slice(clientPacketHeaderWriterStart + clientPacketTypeDependencyDistanceFromClientPacketHeader, clientPacketTypeDependencyReadWidth));
                    {
                        var clientPacketTypeStamped = (ClientPacketType)data.Slice(clientPacketHeaderWriterStart + clientPacketTypeDependencyDistanceFromClientPacketHeader, clientPacketTypeDependencyReadWidth)[0];
                        if (clientSoupBinTcpPacketWriterFrame.ClientPacketHeader.clientPacketType != clientPacketTypeStamped)
                        {
                            clientSoupBinTcpPacketWriterFrame.ClientPacketHeader.clientPacketType = clientPacketTypeStamped;
                            ((IFieldOwner)clientSoupBinTcpPacketWriterFrame.ClientPacketHeader).Touch();
                        }
                    }
                }
                else if (clientSoupBinTcpPacketWriterFrame.ClientPayloadKind == ClientPayloadKind.LogoutRequestPacket)
                {
                    ReadOnlySpan<byte> clientPacketTypeStampedValue;
                    {
                        global::System.ReadOnlySpan<byte> ClientPayloadBranchRule4Data2 = LogoutRequestPacketClientPacketType;
                        clientPacketTypeStampedValue = ClientPayloadBranchRule4Data2;
                    }
                    if (clientPacketTypeStampedValue.Length > ClientPacketTypeLength)
                    {
                        failure = new EncodeFailure(ClientPacketTypeField, EncodeStatus.TooLong, index);
                        return false;
                    }
                    if (clientPacketTypeStampedValue.Length != ClientPacketTypeLength)
                    {
                        failure = new EncodeFailure(ClientPacketTypeField, EncodeStatus.DoesNotFit, index);
                        return false;
                    }
                    var logoutRequestPacketWriterStart = cursor;
                    clientPacketTypeStampedValue.CopyTo(data.Slice(clientPacketHeaderWriterStart + clientPacketTypeDependencyDistanceFromClientPacketHeader, clientPacketTypeDependencyReadWidth));
                    {
                        var clientPacketTypeStamped = (ClientPacketType)data.Slice(clientPacketHeaderWriterStart + clientPacketTypeDependencyDistanceFromClientPacketHeader, clientPacketTypeDependencyReadWidth)[0];
                        if (clientSoupBinTcpPacketWriterFrame.ClientPacketHeader.clientPacketType != clientPacketTypeStamped)
                        {
                            clientSoupBinTcpPacketWriterFrame.ClientPacketHeader.clientPacketType = clientPacketTypeStamped;
                            ((IFieldOwner)clientSoupBinTcpPacketWriterFrame.ClientPacketHeader).Touch();
                        }
                    }
                }
                else if (clientSoupBinTcpPacketWriterFrame.ClientPayload is UnknownClientMessage)
                {
                    var clientPayloadWriting = (UnknownClientMessage)clientSoupBinTcpPacketWriterFrame.ClientPayload!;
                    if (!((IMessage)clientPayloadWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    clientPayloadWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += clientPayloadWriting.Trailer.Length;
                    index++;
                    data.Slice(clientPacketHeaderWriterStart + clientPacketTypeDependencyDistanceFromClientPacketHeader, clientPacketTypeDependencyReadWidth)[0] = unchecked((byte)(((IClientMessage)clientPayloadWriting).DataValue));
                    {
                        var clientPacketTypeStamped = (ClientPacketType)data.Slice(clientPacketHeaderWriterStart + clientPacketTypeDependencyDistanceFromClientPacketHeader, clientPacketTypeDependencyReadWidth)[0];
                        if (clientSoupBinTcpPacketWriterFrame.ClientPacketHeader.clientPacketType != clientPacketTypeStamped)
                        {
                            clientSoupBinTcpPacketWriterFrame.ClientPacketHeader.clientPacketType = clientPacketTypeStamped;
                            ((IFieldOwner)clientSoupBinTcpPacketWriterFrame.ClientPacketHeader).Touch();
                        }
                    }
                    clientPayloadWrittenCase = ClientPayloadNoCase;
                }
                if (clientPayloadWrittenCase != ClientPayloadNothingWritten)
                {
                    int clientPayloadCase;
                    ReadOnlySpan<byte> clientPacketTypeValue;
                    clientPayloadCase = ClientPayloadNoCase;
                    {
                        if (length < clientPacketHeaderWriterStart + clientPacketTypeDependencyDistanceFromClientPacketHeader + clientPacketTypeDependencyReadWidth) { failure = new EncodeFailure(EncodeStatus.RuleMismatch).InMessage(index); return false; }
                        clientPacketTypeValue = data.Slice(clientPacketHeaderWriterStart + clientPacketTypeDependencyDistanceFromClientPacketHeader, clientPacketTypeDependencyReadWidth);
                        global::System.ReadOnlySpan<byte> ClientPayloadBranchRule0Data2 = "\u002B"u8;
                        global::System.ReadOnlySpan<byte> ClientPayloadBranchRule1Data2 = "L"u8;
                        global::System.ReadOnlySpan<byte> ClientPayloadBranchRule2Data2 = "U"u8;
                        global::System.ReadOnlySpan<byte> ClientPayloadBranchRule3Data2 = "R"u8;
                        global::System.ReadOnlySpan<byte> ClientPayloadBranchRule4Data2 = "O"u8;
                        if (clientPacketTypeValue.SequenceEqual(ClientPayloadBranchRule0Data2)) { clientPayloadCase = ClientPayloadBranchRule0Case; }
                        else if (clientPacketTypeValue.SequenceEqual(ClientPayloadBranchRule1Data2)) { clientPayloadCase = ClientPayloadBranchRule1Case; }
                        else if (clientPacketTypeValue.SequenceEqual(ClientPayloadBranchRule2Data2)) { clientPayloadCase = ClientPayloadBranchRule2Case; }
                        else if (clientPacketTypeValue.SequenceEqual(ClientPayloadBranchRule3Data2)) { clientPayloadCase = ClientPayloadBranchRule3Case; }
                        else if (clientPacketTypeValue.SequenceEqual(ClientPayloadBranchRule4Data2)) { clientPayloadCase = ClientPayloadBranchRule4Case; }
                    }
                    if (clientPayloadCase != clientPayloadWrittenCase)
                    {
                        failure = new EncodeFailure(ClientPacketTypeField, EncodeStatus.RuleMismatch, index);
                        return false;
                    }
                }
            }
            BinaryPrimitives.WriteUInt16BigEndian(data.Slice(clientPacketHeaderWriterStart + packetLengthDependencyDistanceFromClientPacketHeader, packetLengthDependencyReadWidth), unchecked((ushort)(packetLengthStampedValue)));
            {
                var packetLengthStamped = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(clientPacketHeaderWriterStart + packetLengthDependencyDistanceFromClientPacketHeader, packetLengthDependencyReadWidth));
                if (clientSoupBinTcpPacketWriterFrame.ClientPacketHeader.packetLength != packetLengthStamped)
                {
                    clientSoupBinTcpPacketWriterFrame.ClientPacketHeader.packetLength = packetLengthStamped;
                    ((IFieldOwner)clientSoupBinTcpPacketWriterFrame.ClientPacketHeader).Touch();
                }
            }
            {
                ushort packetLengthValue;
                const long ClientSoupBinTcpPacketSizeRule2Data2 = 2;
                global::System.Int128 measure = clientSoupBinTcpPacketOccurrenceBytes;
                bool agrees;
                try
                {
                    packetLengthValue = checked((ushort)(packetLengthStampedValue));
                    agrees = checked((checked((global::System.Int128)(packetLengthValue)) + ((global::System.Int128)(ClientSoupBinTcpPacketSizeRule2Data2)))) == measure;
                }
                catch (global::System.OverflowException)
                {
                    agrees = false;
                }
                if (!agrees)
                {
                    failure = new EncodeFailure(PacketLengthField, EncodeStatus.RuleMismatch, index);
                    return false;
                }
            }
            clientSoupBinTcpPacketIndex++;
        }
        {
            global::System.Int128 measure = clientSoupBinTcpPacketWriterPosition + clientSoupBinTcpPacketMeasuredBytes;
            bool agrees;
            agrees = checked((global::System.Int128)(length)) == measure;
            if (!agrees)
            {
                failure = new EncodeFailure(EncodeStatus.RuleMismatch);
                return false;
            }
        }
        failure = default;
        return true;
    }

    /// <summary>
    ///  Checks every field of the header and of each message against the values that the protocol specification allows.
    /// </summary>
    /// <returns>The fields whose values are not allowed, each with the reason, or an empty list if every value is allowed.</returns>
    public List<ValidationFailure> Validate()
    {
        var failures = new List<ValidationFailure>();
        foreach (var (index, message) in Messages.Index())
        {
            foreach (var failure in message.Validate())
            {
                failures.Add(failure.InMessage(index));
            }
        }
        foreach (var entry in this.ClientSoupBinTcpPacketFrames)
        {
            failures.AddRange(entry.Validate());
        }
        return failures;
    }

    /// <summary>
    ///  Returns the packet formatted on multiple lines.
    /// </summary>
    /// <remarks>
    ///  The text holds each kind and message of the packet, and a line for each frame entry.
    /// </remarks>
    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.AppendLine("Packet");
        builder.Append("  ClientSoupBinTcpPacketFrames: ").Append(this.ClientSoupBinTcpPacketFrames.Count).AppendLine();
        foreach (var entry in this.ClientSoupBinTcpPacketFrames)
        {
            builder.Append("  ").AppendLine(entry.ToString());
        }
        return builder.ToString();
    }
}
