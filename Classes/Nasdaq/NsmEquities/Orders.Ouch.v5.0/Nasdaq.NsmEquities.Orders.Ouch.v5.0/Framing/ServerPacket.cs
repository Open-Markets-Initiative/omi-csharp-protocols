using System.Text;
using System.Buffers.Binary;
using static Nasdaq.NsmEquities.Orders.Ouch.v5_0.Rules;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  SoupBinTCP Packet sent by the server
/// </summary>
public sealed class ServerPacket : IPacket
{
    IHeader? IPacket.Header => null;

    /// <summary>
    ///  Gets the Server Soup Bin Tcp Packet entries, in wire order.
    /// </summary>
    public List<ServerSoupBinTcpPacketFrame> ServerSoupBinTcpPacketFrames { get; } = [];

    /// <summary>
    ///  Adds a Server Soup Bin Tcp Packet entry that carries the message.
    /// </summary>
    /// <param name="message">The message that the entry carries.</param>
    /// <returns>The new entry, whose header fields the caller can set.</returns>
    /// <exception cref="ArgumentNullException">The value is <see langword="null"/>.</exception>
    public ServerSoupBinTcpPacketFrame Add(IServerMessage message)
    {
        ArgumentNullException.ThrowIfNull(message);
        var entry = new ServerSoupBinTcpPacketFrame { ServerPayload = message };
        ServerSoupBinTcpPacketFrames.Add(entry);
        return entry;
    }

    /// <summary>
    ///  Adds a Server Soup Bin Tcp Packet entry of the Server Heartbeat kind, which carries no message.
    /// </summary>
    /// <returns>The new entry, whose header fields the caller can set.</returns>
    public ServerSoupBinTcpPacketFrame AddServerHeartbeat()
    {
        var entry = new ServerSoupBinTcpPacketFrame();
        entry.ServerPayloadKind = ServerPayloadKind.ServerHeartbeat;
        ServerSoupBinTcpPacketFrames.Add(entry);
        return entry;
    }

    /// <summary>
    ///  Adds a Server Soup Bin Tcp Packet entry of the End Of Session kind, which carries no message.
    /// </summary>
    /// <returns>The new entry, whose header fields the caller can set.</returns>
    public ServerSoupBinTcpPacketFrame AddEndOfSession()
    {
        var entry = new ServerSoupBinTcpPacketFrame();
        entry.ServerPayloadKind = ServerPayloadKind.EndOfSession;
        ServerSoupBinTcpPacketFrames.Add(entry);
        return entry;
    }

    /// <summary>
    ///  Gets the messages of the packet, in wire order.
    /// </summary>
    /// <remarks>
    ///  The list reads the packet entries each time that it is used, so it shows each change to them. Add or remove messages through the entries.
    /// </remarks>
    public ServerFramedMessages Messages { get; }

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
    ///  Initializes a new instance of the <see cref="ServerPacket"/> class with default values and no messages.
    /// </summary>
    public ServerPacket()
    {
        Messages = new ServerFramedMessages(this);
        Clear();
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(ServerPacket other)
    {
        ArgumentNullException.ThrowIfNull(other);
        if (ReferenceEquals(this, other))
            return;
        this.ServerSoupBinTcpPacketFrames.Clear();
        this.ServerSoupBinTcpPacketFrames.AddRange(other.ServerSoupBinTcpPacketFrames.Select(static entry => entry.Copy()));
        Messages.KeepCopy(other.Messages);
        this.IsParsed = other.IsParsed;
    }

    /// <summary>
    ///  Resets all values, the bytes, and the parse result to their defaults.
    /// </summary>
    public void Clear()
    {
        this.ServerSoupBinTcpPacketFrames.Clear();
        Messages.Keep(null);
        this.IsParsed = true;
    }

    /// <summary>
    ///  Parses a packet from the bytes.
    /// </summary>
    /// <returns>The packet. Check <see cref="IsParsed"/> for the result of the parse, and <see cref="IsValid"/> for whether the protocol allows every value.</returns>
    public static ServerPacket Parse(ReadOnlySpan<byte> data)
    {
        var packet = new ServerPacket();
        packet.ParseFrom(data);
        return packet;
    }

    /// <summary>
    ///  Parses the bytes into this instance, starting at the first byte.
    /// </summary>
    public void ParseFrom(ReadOnlySpan<byte> data)
    {
        Clear();
        var messages = new ServerFramedMessages(data, this);
        this.IsParsed = messages.Status == ParseStatus.Ok && messages.IsComplete;
        Messages.Keep(messages);
    }

    /// <summary>
    ///  Gets the length of the Server Packet frame at the start of the bytes.
    /// </summary>
    /// <param name="source">The bytes, from the start of the frame.</param>
    /// <param name="length">The number of bytes of the frame, which can be more than the bytes hold.</param>
    /// <returns><see langword="true"/> when the bytes hold the fields that state the length; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">The bytes state a frame length that is not valid.</exception>
    public static bool TryGetFrameLength(ReadOnlySpan<byte> source, out int length)
    {
        const string FrameLengthInvalid = "The bytes state a frame length that is not valid.";
        const int packetLengthDependencyDistanceFromServerSoupBinTcpPacket = 0;
        const int packetLengthDependencyReadWidth = 2;
        global::System.Int128 ServerSoupBinTcpPacketReassembleRule1Width;
        ushort packetLengthValue;
        length = 0;
        const long ServerSoupBinTcpPacketReassembleRule1Data2 = 2;
        {
            if (source.Length < packetLengthDependencyDistanceFromServerSoupBinTcpPacket + packetLengthDependencyReadWidth) return false;
            packetLengthValue = BinaryPrimitives.ReadUInt16BigEndian(source.Slice(packetLengthDependencyDistanceFromServerSoupBinTcpPacket, packetLengthDependencyReadWidth));
            ServerSoupBinTcpPacketReassembleRule1Width = checked((checked((global::System.Int128)(packetLengthValue)) + ((global::System.Int128)(ServerSoupBinTcpPacketReassembleRule1Data2))));
        }
        if (ServerSoupBinTcpPacketReassembleRule1Width <= RuleEmptyWidth || ServerSoupBinTcpPacketReassembleRule1Width > int.MaxValue) throw new global::System.ArgumentOutOfRangeException(nameof(source), FrameLengthInvalid);
        length = (int)ServerSoupBinTcpPacketReassembleRule1Width;
        return true;
    }

    internal IEnumerable<IServerMessage> Collect()
    {
        foreach (var entry in this.ServerSoupBinTcpPacketFrames)
        {
            foreach (var message in entry.Collect())
            {
                yield return message;
            }
        }
        yield break;
    }

    private static global::System.ReadOnlySpan<byte> ServerHeartbeatServerPacketType => "H"u8;
    private static global::System.ReadOnlySpan<byte> EndOfSessionServerPacketType => "Z"u8;
    private const int PacketLengthLength = 2;
    private static readonly FieldDescriptor PacketLengthField = new("PacketLength", FieldKind.UnsignedInteger, PacketLengthLength) { ByteOrder = "Big", IsSigned = false };
    private const int ServerPacketTypeLength = 1;
    private static readonly FieldDescriptor ServerPacketTypeField = new("ServerPacketType", FieldKind.Enum, ServerPacketTypeLength);

    /// <summary>
    ///  Gets the number of bytes that Encode writes, including variable-length content and each omittable field that is present.
    /// </summary>
    public int EncodedByteLength
    {
        get
        {
            var length = 0;
            var serverSoupBinTcpPacketMeasuredEntries = this.ServerSoupBinTcpPacketFrames;
            var serverSoupBinTcpPacketMeasuredFirst = 0;
            while (serverSoupBinTcpPacketMeasuredFirst < serverSoupBinTcpPacketMeasuredEntries.Count)
            {
                var serverSoupBinTcpPacketMeasuredFrame = serverSoupBinTcpPacketMeasuredEntries[serverSoupBinTcpPacketMeasuredFirst];
                length += ServerPacketHeader.HeaderByteLength;
                if (serverSoupBinTcpPacketMeasuredFrame.ServerPayloadKind == ServerPayloadKind.Messages && serverSoupBinTcpPacketMeasuredFrame.ServerPayload is DebugPacket or LoginAcceptedPacket or LoginRejectedPacket or SequencedDataPacket)
                {
                    length += ((IMessage)serverSoupBinTcpPacketMeasuredFrame.ServerPayload!).EncodedByteLength + ((IMessage)serverSoupBinTcpPacketMeasuredFrame.ServerPayload!).Trailer.Length;
                }
                else if (serverSoupBinTcpPacketMeasuredFrame.ServerPayloadKind == ServerPayloadKind.ServerHeartbeat)
                {
                }
                else if (serverSoupBinTcpPacketMeasuredFrame.ServerPayloadKind == ServerPayloadKind.EndOfSession)
                {
                }
                else if (serverSoupBinTcpPacketMeasuredFrame.ServerPayload is UnknownServerMessage)
                {
                    length += ((IMessage)serverSoupBinTcpPacketMeasuredFrame.ServerPayload!).EncodedByteLength + ((IMessage)serverSoupBinTcpPacketMeasuredFrame.ServerPayload!).Trailer.Length;
                }
                serverSoupBinTcpPacketMeasuredFirst++;
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
        const int serverPacketTypeDependencyDistanceFromServerPacketHeader = 2;
        const int serverPacketTypeDependencyReadWidth = 1;
        const int packetLengthDependencyDistanceFromServerPacketHeader = 0;
        const int packetLengthDependencyReadWidth = 2;
        var serverPacketWriterStart = cursor;
        var serverSoupBinTcpPacketWriterPosition = cursor;
        var serverSoupBinTcpPacketMeasuredBytes = 0;
        {
            var serverSoupBinTcpPacketMeasuredEntries = this.ServerSoupBinTcpPacketFrames;
            var serverSoupBinTcpPacketMeasuredFirst = 0;
            while (serverSoupBinTcpPacketMeasuredFirst < serverSoupBinTcpPacketMeasuredEntries.Count)
            {
                var serverSoupBinTcpPacketMeasuredFrame = serverSoupBinTcpPacketMeasuredEntries[serverSoupBinTcpPacketMeasuredFirst];
                serverSoupBinTcpPacketMeasuredBytes += ServerPacketHeader.HeaderByteLength;
                if (serverSoupBinTcpPacketMeasuredFrame.ServerPayloadKind == ServerPayloadKind.Messages && serverSoupBinTcpPacketMeasuredFrame.ServerPayload is DebugPacket or LoginAcceptedPacket or LoginRejectedPacket or SequencedDataPacket)
                {
                    serverSoupBinTcpPacketMeasuredBytes += ((IMessage)serverSoupBinTcpPacketMeasuredFrame.ServerPayload!).EncodedByteLength + ((IMessage)serverSoupBinTcpPacketMeasuredFrame.ServerPayload!).Trailer.Length;
                }
                else if (serverSoupBinTcpPacketMeasuredFrame.ServerPayloadKind == ServerPayloadKind.ServerHeartbeat)
                {
                }
                else if (serverSoupBinTcpPacketMeasuredFrame.ServerPayloadKind == ServerPayloadKind.EndOfSession)
                {
                }
                else if (serverSoupBinTcpPacketMeasuredFrame.ServerPayload is UnknownServerMessage)
                {
                    serverSoupBinTcpPacketMeasuredBytes += ((IMessage)serverSoupBinTcpPacketMeasuredFrame.ServerPayload!).EncodedByteLength + ((IMessage)serverSoupBinTcpPacketMeasuredFrame.ServerPayload!).Trailer.Length;
                }
                serverSoupBinTcpPacketMeasuredFirst++;
            }
        }
        var serverSoupBinTcpPacketWriterEntries = this.ServerSoupBinTcpPacketFrames;
        var serverSoupBinTcpPacketIndex = 0;
        while (serverSoupBinTcpPacketIndex < serverSoupBinTcpPacketWriterEntries.Count)
        {
            var serverSoupBinTcpPacketWriterFrame = serverSoupBinTcpPacketWriterEntries[serverSoupBinTcpPacketIndex];
            var serverSoupBinTcpPacketWriterStart = cursor;
            var serverSoupBinTcpPacketOccurrenceBytes = 0;
            {
                serverSoupBinTcpPacketOccurrenceBytes += ServerPacketHeader.HeaderByteLength;
                if (serverSoupBinTcpPacketWriterFrame.ServerPayloadKind == ServerPayloadKind.Messages && serverSoupBinTcpPacketWriterFrame.ServerPayload is DebugPacket or LoginAcceptedPacket or LoginRejectedPacket or SequencedDataPacket)
                {
                    serverSoupBinTcpPacketOccurrenceBytes += ((IMessage)serverSoupBinTcpPacketWriterFrame.ServerPayload!).EncodedByteLength + ((IMessage)serverSoupBinTcpPacketWriterFrame.ServerPayload!).Trailer.Length;
                }
                else if (serverSoupBinTcpPacketWriterFrame.ServerPayloadKind == ServerPayloadKind.ServerHeartbeat)
                {
                }
                else if (serverSoupBinTcpPacketWriterFrame.ServerPayloadKind == ServerPayloadKind.EndOfSession)
                {
                }
                else if (serverSoupBinTcpPacketWriterFrame.ServerPayload is UnknownServerMessage)
                {
                    serverSoupBinTcpPacketOccurrenceBytes += ((IMessage)serverSoupBinTcpPacketWriterFrame.ServerPayload!).EncodedByteLength + ((IMessage)serverSoupBinTcpPacketWriterFrame.ServerPayload!).Trailer.Length;
                }
            }
            global::System.Int128 packetLengthStampedValue;
            {
                const long ServerSoupBinTcpPacketReassembleRule1Data2 = 2;
                global::System.Int128 measure = serverSoupBinTcpPacketOccurrenceBytes;
                try
                {
                    packetLengthStampedValue = checked((measure - checked((((global::System.Int128)(RuleNumberZero)) + ((global::System.Int128)(ServerSoupBinTcpPacketReassembleRule1Data2))))));
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
            var serverPacketHeaderWriterStart = cursor;
            var serverPacketHeaderWriting = serverSoupBinTcpPacketWriterFrame.ServerPacketHeader;
            if (!((IHeader)serverPacketHeaderWriting).Write(data, cursor, out cursor, out failure))
            {
                failure = failure.InMessage(index);
                return false;
            }
            var serverPayloadWriterStart = cursor;
            {
                const int ServerPayloadNoCase = 0;
                const int ServerPayloadBranchRule0Case = 1;
                const int ServerPayloadBranchRule1Case = 2;
                const int ServerPayloadBranchRule2Case = 3;
                const int ServerPayloadBranchRule3Case = 4;
                const int ServerPayloadBranchRule4Case = 5;
                const int ServerPayloadBranchRule5Case = 6;
                const int ServerPayloadNothingWritten = -1;
                var serverPayloadWrittenCase = ServerPayloadNothingWritten;
                if (serverSoupBinTcpPacketWriterFrame.ServerPayloadKind == ServerPayloadKind.Messages && serverSoupBinTcpPacketWriterFrame.ServerPayload is DebugPacket)
                {
                    var debugPacketWriterStart = cursor;
                    var debugPacketWriting = (DebugPacket)serverSoupBinTcpPacketWriterFrame.ServerPayload!;
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
                    data.Slice(serverPacketHeaderWriterStart + serverPacketTypeDependencyDistanceFromServerPacketHeader, serverPacketTypeDependencyReadWidth)[0] = unchecked((byte)(((IServerMessage)debugPacketWriting).DataValue));
                    {
                        var serverPacketTypeStamped = (ServerPacketType)data.Slice(serverPacketHeaderWriterStart + serverPacketTypeDependencyDistanceFromServerPacketHeader, serverPacketTypeDependencyReadWidth)[0];
                        if (serverSoupBinTcpPacketWriterFrame.ServerPacketHeader.serverPacketType != serverPacketTypeStamped)
                        {
                            serverSoupBinTcpPacketWriterFrame.ServerPacketHeader.serverPacketType = serverPacketTypeStamped;
                            ((IFieldOwner)serverSoupBinTcpPacketWriterFrame.ServerPacketHeader).Touch();
                        }
                    }
                }
                else if (serverSoupBinTcpPacketWriterFrame.ServerPayloadKind == ServerPayloadKind.Messages && serverSoupBinTcpPacketWriterFrame.ServerPayload is LoginAcceptedPacket)
                {
                    var loginAcceptedPacketWriterStart = cursor;
                    var loginAcceptedPacketWriting = (LoginAcceptedPacket)serverSoupBinTcpPacketWriterFrame.ServerPayload!;
                    if (!((IMessage)loginAcceptedPacketWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    loginAcceptedPacketWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += loginAcceptedPacketWriting.Trailer.Length;
                    index++;
                    data.Slice(serverPacketHeaderWriterStart + serverPacketTypeDependencyDistanceFromServerPacketHeader, serverPacketTypeDependencyReadWidth)[0] = unchecked((byte)(((IServerMessage)loginAcceptedPacketWriting).DataValue));
                    {
                        var serverPacketTypeStamped = (ServerPacketType)data.Slice(serverPacketHeaderWriterStart + serverPacketTypeDependencyDistanceFromServerPacketHeader, serverPacketTypeDependencyReadWidth)[0];
                        if (serverSoupBinTcpPacketWriterFrame.ServerPacketHeader.serverPacketType != serverPacketTypeStamped)
                        {
                            serverSoupBinTcpPacketWriterFrame.ServerPacketHeader.serverPacketType = serverPacketTypeStamped;
                            ((IFieldOwner)serverSoupBinTcpPacketWriterFrame.ServerPacketHeader).Touch();
                        }
                    }
                }
                else if (serverSoupBinTcpPacketWriterFrame.ServerPayloadKind == ServerPayloadKind.Messages && serverSoupBinTcpPacketWriterFrame.ServerPayload is LoginRejectedPacket)
                {
                    var loginRejectedPacketWriterStart = cursor;
                    var loginRejectedPacketWriting = (LoginRejectedPacket)serverSoupBinTcpPacketWriterFrame.ServerPayload!;
                    if (!((IMessage)loginRejectedPacketWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    loginRejectedPacketWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += loginRejectedPacketWriting.Trailer.Length;
                    index++;
                    data.Slice(serverPacketHeaderWriterStart + serverPacketTypeDependencyDistanceFromServerPacketHeader, serverPacketTypeDependencyReadWidth)[0] = unchecked((byte)(((IServerMessage)loginRejectedPacketWriting).DataValue));
                    {
                        var serverPacketTypeStamped = (ServerPacketType)data.Slice(serverPacketHeaderWriterStart + serverPacketTypeDependencyDistanceFromServerPacketHeader, serverPacketTypeDependencyReadWidth)[0];
                        if (serverSoupBinTcpPacketWriterFrame.ServerPacketHeader.serverPacketType != serverPacketTypeStamped)
                        {
                            serverSoupBinTcpPacketWriterFrame.ServerPacketHeader.serverPacketType = serverPacketTypeStamped;
                            ((IFieldOwner)serverSoupBinTcpPacketWriterFrame.ServerPacketHeader).Touch();
                        }
                    }
                }
                else if (serverSoupBinTcpPacketWriterFrame.ServerPayloadKind == ServerPayloadKind.Messages && serverSoupBinTcpPacketWriterFrame.ServerPayload is SequencedDataPacket)
                {
                    var sequencedDataPacketWriterStart = cursor;
                    var sequencedDataPacketWriting = (SequencedDataPacket)serverSoupBinTcpPacketWriterFrame.ServerPayload!;
                    {
                        ushort packetLengthValue;
                        packetLengthValue = checked((ushort)(packetLengthStampedValue));
                        if (!sequencedDataPacketWriting.Write(data, cursor, trailerFollows: true, checked((global::System.Int128?)(packetLengthValue)), checked((global::System.Int128?)(packetLengthValue)), out cursor, out failure))
                        {
                            failure = failure.InMessage(index);
                            return false;
                        }
                    }
                    sequencedDataPacketWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += sequencedDataPacketWriting.Trailer.Length;
                    index++;
                    data.Slice(serverPacketHeaderWriterStart + serverPacketTypeDependencyDistanceFromServerPacketHeader, serverPacketTypeDependencyReadWidth)[0] = unchecked((byte)(((IServerMessage)sequencedDataPacketWriting).DataValue));
                    {
                        var serverPacketTypeStamped = (ServerPacketType)data.Slice(serverPacketHeaderWriterStart + serverPacketTypeDependencyDistanceFromServerPacketHeader, serverPacketTypeDependencyReadWidth)[0];
                        if (serverSoupBinTcpPacketWriterFrame.ServerPacketHeader.serverPacketType != serverPacketTypeStamped)
                        {
                            serverSoupBinTcpPacketWriterFrame.ServerPacketHeader.serverPacketType = serverPacketTypeStamped;
                            ((IFieldOwner)serverSoupBinTcpPacketWriterFrame.ServerPacketHeader).Touch();
                        }
                    }
                }
                else if (serverSoupBinTcpPacketWriterFrame.ServerPayloadKind == ServerPayloadKind.ServerHeartbeat)
                {
                    ReadOnlySpan<byte> serverPacketTypeStampedValue;
                    {
                        global::System.ReadOnlySpan<byte> ServerPayloadBranchRule4Data2 = ServerHeartbeatServerPacketType;
                        serverPacketTypeStampedValue = ServerPayloadBranchRule4Data2;
                    }
                    if (serverPacketTypeStampedValue.Length > ServerPacketTypeLength)
                    {
                        failure = new EncodeFailure(ServerPacketTypeField, EncodeStatus.TooLong, index);
                        return false;
                    }
                    if (serverPacketTypeStampedValue.Length != ServerPacketTypeLength)
                    {
                        failure = new EncodeFailure(ServerPacketTypeField, EncodeStatus.DoesNotFit, index);
                        return false;
                    }
                    var serverHeartbeatWriterStart = cursor;
                    serverPacketTypeStampedValue.CopyTo(data.Slice(serverPacketHeaderWriterStart + serverPacketTypeDependencyDistanceFromServerPacketHeader, serverPacketTypeDependencyReadWidth));
                    {
                        var serverPacketTypeStamped = (ServerPacketType)data.Slice(serverPacketHeaderWriterStart + serverPacketTypeDependencyDistanceFromServerPacketHeader, serverPacketTypeDependencyReadWidth)[0];
                        if (serverSoupBinTcpPacketWriterFrame.ServerPacketHeader.serverPacketType != serverPacketTypeStamped)
                        {
                            serverSoupBinTcpPacketWriterFrame.ServerPacketHeader.serverPacketType = serverPacketTypeStamped;
                            ((IFieldOwner)serverSoupBinTcpPacketWriterFrame.ServerPacketHeader).Touch();
                        }
                    }
                }
                else if (serverSoupBinTcpPacketWriterFrame.ServerPayloadKind == ServerPayloadKind.EndOfSession)
                {
                    ReadOnlySpan<byte> serverPacketTypeStampedValue;
                    {
                        global::System.ReadOnlySpan<byte> ServerPayloadBranchRule5Data2 = EndOfSessionServerPacketType;
                        serverPacketTypeStampedValue = ServerPayloadBranchRule5Data2;
                    }
                    if (serverPacketTypeStampedValue.Length > ServerPacketTypeLength)
                    {
                        failure = new EncodeFailure(ServerPacketTypeField, EncodeStatus.TooLong, index);
                        return false;
                    }
                    if (serverPacketTypeStampedValue.Length != ServerPacketTypeLength)
                    {
                        failure = new EncodeFailure(ServerPacketTypeField, EncodeStatus.DoesNotFit, index);
                        return false;
                    }
                    var endOfSessionWriterStart = cursor;
                    serverPacketTypeStampedValue.CopyTo(data.Slice(serverPacketHeaderWriterStart + serverPacketTypeDependencyDistanceFromServerPacketHeader, serverPacketTypeDependencyReadWidth));
                    {
                        var serverPacketTypeStamped = (ServerPacketType)data.Slice(serverPacketHeaderWriterStart + serverPacketTypeDependencyDistanceFromServerPacketHeader, serverPacketTypeDependencyReadWidth)[0];
                        if (serverSoupBinTcpPacketWriterFrame.ServerPacketHeader.serverPacketType != serverPacketTypeStamped)
                        {
                            serverSoupBinTcpPacketWriterFrame.ServerPacketHeader.serverPacketType = serverPacketTypeStamped;
                            ((IFieldOwner)serverSoupBinTcpPacketWriterFrame.ServerPacketHeader).Touch();
                        }
                    }
                }
                else if (serverSoupBinTcpPacketWriterFrame.ServerPayload is UnknownServerMessage)
                {
                    var serverPayloadWriting = (UnknownServerMessage)serverSoupBinTcpPacketWriterFrame.ServerPayload!;
                    if (!((IMessage)serverPayloadWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    serverPayloadWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += serverPayloadWriting.Trailer.Length;
                    index++;
                    data.Slice(serverPacketHeaderWriterStart + serverPacketTypeDependencyDistanceFromServerPacketHeader, serverPacketTypeDependencyReadWidth)[0] = unchecked((byte)(((IServerMessage)serverPayloadWriting).DataValue));
                    {
                        var serverPacketTypeStamped = (ServerPacketType)data.Slice(serverPacketHeaderWriterStart + serverPacketTypeDependencyDistanceFromServerPacketHeader, serverPacketTypeDependencyReadWidth)[0];
                        if (serverSoupBinTcpPacketWriterFrame.ServerPacketHeader.serverPacketType != serverPacketTypeStamped)
                        {
                            serverSoupBinTcpPacketWriterFrame.ServerPacketHeader.serverPacketType = serverPacketTypeStamped;
                            ((IFieldOwner)serverSoupBinTcpPacketWriterFrame.ServerPacketHeader).Touch();
                        }
                    }
                    serverPayloadWrittenCase = ServerPayloadNoCase;
                }
                if (serverPayloadWrittenCase != ServerPayloadNothingWritten)
                {
                    int serverPayloadCase;
                    ReadOnlySpan<byte> serverPacketTypeValue;
                    serverPayloadCase = ServerPayloadNoCase;
                    {
                        if (length < serverPacketHeaderWriterStart + serverPacketTypeDependencyDistanceFromServerPacketHeader + serverPacketTypeDependencyReadWidth) { failure = new EncodeFailure(EncodeStatus.RuleMismatch).InMessage(index); return false; }
                        serverPacketTypeValue = data.Slice(serverPacketHeaderWriterStart + serverPacketTypeDependencyDistanceFromServerPacketHeader, serverPacketTypeDependencyReadWidth);
                        global::System.ReadOnlySpan<byte> ServerPayloadBranchRule0Data2 = "\u002B"u8;
                        global::System.ReadOnlySpan<byte> ServerPayloadBranchRule1Data2 = "A"u8;
                        global::System.ReadOnlySpan<byte> ServerPayloadBranchRule2Data2 = "J"u8;
                        global::System.ReadOnlySpan<byte> ServerPayloadBranchRule3Data2 = "S"u8;
                        global::System.ReadOnlySpan<byte> ServerPayloadBranchRule4Data2 = "H"u8;
                        global::System.ReadOnlySpan<byte> ServerPayloadBranchRule5Data2 = "Z"u8;
                        if (serverPacketTypeValue.SequenceEqual(ServerPayloadBranchRule0Data2)) { serverPayloadCase = ServerPayloadBranchRule0Case; }
                        else if (serverPacketTypeValue.SequenceEqual(ServerPayloadBranchRule1Data2)) { serverPayloadCase = ServerPayloadBranchRule1Case; }
                        else if (serverPacketTypeValue.SequenceEqual(ServerPayloadBranchRule2Data2)) { serverPayloadCase = ServerPayloadBranchRule2Case; }
                        else if (serverPacketTypeValue.SequenceEqual(ServerPayloadBranchRule3Data2)) { serverPayloadCase = ServerPayloadBranchRule3Case; }
                        else if (serverPacketTypeValue.SequenceEqual(ServerPayloadBranchRule4Data2)) { serverPayloadCase = ServerPayloadBranchRule4Case; }
                        else if (serverPacketTypeValue.SequenceEqual(ServerPayloadBranchRule5Data2)) { serverPayloadCase = ServerPayloadBranchRule5Case; }
                    }
                    if (serverPayloadCase != serverPayloadWrittenCase)
                    {
                        failure = new EncodeFailure(ServerPacketTypeField, EncodeStatus.RuleMismatch, index);
                        return false;
                    }
                }
            }
            BinaryPrimitives.WriteUInt16BigEndian(data.Slice(serverPacketHeaderWriterStart + packetLengthDependencyDistanceFromServerPacketHeader, packetLengthDependencyReadWidth), unchecked((ushort)(packetLengthStampedValue)));
            {
                var packetLengthStamped = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(serverPacketHeaderWriterStart + packetLengthDependencyDistanceFromServerPacketHeader, packetLengthDependencyReadWidth));
                if (serverSoupBinTcpPacketWriterFrame.ServerPacketHeader.packetLength != packetLengthStamped)
                {
                    serverSoupBinTcpPacketWriterFrame.ServerPacketHeader.packetLength = packetLengthStamped;
                    ((IFieldOwner)serverSoupBinTcpPacketWriterFrame.ServerPacketHeader).Touch();
                }
            }
            {
                ushort packetLengthValue;
                const long ServerSoupBinTcpPacketSizeRule2Data2 = 2;
                global::System.Int128 measure = serverSoupBinTcpPacketOccurrenceBytes;
                bool agrees;
                try
                {
                    packetLengthValue = checked((ushort)(packetLengthStampedValue));
                    agrees = checked((checked((global::System.Int128)(packetLengthValue)) + ((global::System.Int128)(ServerSoupBinTcpPacketSizeRule2Data2)))) == measure;
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
            serverSoupBinTcpPacketIndex++;
        }
        {
            global::System.Int128 measure = serverSoupBinTcpPacketWriterPosition + serverSoupBinTcpPacketMeasuredBytes;
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
        foreach (var entry in this.ServerSoupBinTcpPacketFrames)
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
        builder.Append("  ServerSoupBinTcpPacketFrames: ").Append(this.ServerSoupBinTcpPacketFrames.Count).AppendLine();
        foreach (var entry in this.ServerSoupBinTcpPacketFrames)
        {
            builder.Append("  ").AppendLine(entry.ToString());
        }
        return builder.ToString();
    }
}
