using System.Text;
using System.Buffers.Binary;
using static Databento.MarketData.Normalized.Dbn.v3.Rules;

namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  The gateway's control messages, then the DBN stream: its metadata, then records
/// </summary>
public sealed class GatewayStream : IPacket
{
    IHeader? IPacket.Header => null;

    private Greeting greeting = new Greeting();

    /// <summary>
    ///  The message sent by the gateway immediately after a client connects (lsg_version: the live gateway version)
    /// </summary>
    public Greeting Greeting => this.greeting;

    private ChallengeRequest challengeRequest = new ChallengeRequest();

    /// <summary>
    ///  The gateway sends this message immediately following the greeting message to authenticate the client (cram: a randomly-generated challenge string)
    /// </summary>
    public ChallengeRequest ChallengeRequest => this.challengeRequest;

    private AuthenticationResponse authenticationResponse = new AuthenticationResponse();

    /// <summary>
    ///  The gateway sends this response following the client's authentication request (success, and error or session_id)
    /// </summary>
    public AuthenticationResponse AuthenticationResponse => this.authenticationResponse;

    private Metadata metadata = new Metadata();

    /// <summary>
    ///  DBN metadata header, once at the start of a file or stream
    /// </summary>
    public Metadata Metadata => this.metadata;

    /// <summary>
    ///  Gets the Record entries, in wire order.
    /// </summary>
    public List<GatewayRecordFrame> RecordFrames { get; } = [];

    /// <summary>
    ///  Adds a Record entry that carries the message.
    /// </summary>
    /// <param name="message">The message that the entry carries.</param>
    /// <returns>The new entry, whose header fields the caller can set.</returns>
    /// <exception cref="ArgumentNullException">The value is <see langword="null"/>.</exception>
    public GatewayRecordFrame Add(IGatewayMessage message)
    {
        ArgumentNullException.ThrowIfNull(message);
        var entry = new GatewayRecordFrame { RecordBody = message };
        RecordFrames.Add(entry);
        return entry;
    }

    /// <summary>
    ///  Gets the messages of the packet, in wire order.
    /// </summary>
    /// <remarks>
    ///  The list reads the packet entries each time that it is used, so it shows each change to them. Add or remove messages through the entries.
    /// </remarks>
    public GatewayFramedMessages Messages { get; }

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

    /// <summary>
    ///  Gets the number of messages that the packet declares.
    /// </summary>
    /// <remarks>
    ///  The value is 0 for a packet that was not parsed or that ended before its message count. It is also 0 when the count field holds a value that marks a packet with no messages.
    /// </remarks>
    public ulong MessageCount { get; private set; }

    private byte[] trailer = [];

    /// <summary>
    ///  Gets or sets the bytes in the packet message area after the last message, or in place of messages.
    /// </summary>
    public ReadOnlyMemory<byte> Trailer
    {
        get => this.trailer;
        set => this.trailer = value.ToArray();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="GatewayStream"/> class with default values and no messages.
    /// </summary>
    public GatewayStream()
    {
        Messages = new GatewayFramedMessages(this);
        Clear();
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(GatewayStream other)
    {
        ArgumentNullException.ThrowIfNull(other);
        if (ReferenceEquals(this, other))
            return;
        this.greeting = new Greeting(other.greeting);
        this.challengeRequest = new ChallengeRequest(other.challengeRequest);
        this.authenticationResponse = new AuthenticationResponse(other.authenticationResponse);
        this.metadata = new Metadata(other.metadata);
        this.RecordFrames.Clear();
        this.RecordFrames.AddRange(other.RecordFrames.Select(static entry => entry.Copy()));
        Messages.KeepCopy(other.Messages);
        this.IsParsed = other.IsParsed;
        this.MessageCount = other.MessageCount;
        this.trailer = other.trailer;
    }

    /// <summary>
    ///  Resets all values, the bytes, and the parse result to their defaults.
    /// </summary>
    public void Clear()
    {
        this.greeting = new Greeting();
        this.challengeRequest = new ChallengeRequest();
        this.authenticationResponse = new AuthenticationResponse();
        this.metadata = new Metadata();
        this.RecordFrames.Clear();
        Messages.Keep(null);
        this.IsParsed = true;
        this.MessageCount = default;
        this.trailer = [];
    }

    /// <summary>
    ///  Parses a packet from the bytes.
    /// </summary>
    /// <returns>The packet. Check <see cref="IsParsed"/> for the result of the parse, and <see cref="IsValid"/> for whether the protocol allows every value.</returns>
    public static GatewayStream Parse(ReadOnlySpan<byte> data)
    {
        var packet = new GatewayStream();
        packet.ParseFrom(data);
        return packet;
    }

    /// <summary>
    ///  Parses a packet from the bytes.
    /// </summary>
    /// <returns>The packet. Check <see cref="IsParsed"/> for the result of the parse, and <see cref="IsValid"/> for whether the protocol allows every value.</returns>
    /// <remarks>
    ///  Each stored value holds what earlier packets of the conversation stored, or null before any store. The bytes that this call reads update it.
    /// </remarks>
    public static GatewayStream Parse(ReadOnlySpan<byte> data, ref byte? tsOutStoredValue)
    {
        var packet = new GatewayStream();
        packet.ParseFrom(data, ref tsOutStoredValue);
        return packet;
    }

    /// <summary>
    ///  Parses the bytes into this instance, starting at the first byte.
    /// </summary>
    public void ParseFrom(ReadOnlySpan<byte> data)
    {
        byte? tsOutStoredValue = null;
        ParseFrom(data, ref tsOutStoredValue);
    }

    /// <summary>
    ///  Parses the bytes into this instance, starting at the first byte.
    /// </summary>
    /// <remarks>
    ///  Each stored value holds what earlier packets of the conversation stored, or null before any store. The bytes that this call reads update it.
    /// </remarks>
    public void ParseFrom(ReadOnlySpan<byte> data, ref byte? tsOutStoredValue)
    {
        Clear();
        var messages = new GatewayFramedMessages(data, this, ref tsOutStoredValue);
        this.IsParsed = messages.Status == ParseStatus.Ok && messages.IsComplete;
        this.Trailer = messages.Trailer;
        this.MessageCount = messages.MessageCount;
        Messages.Keep(messages);
    }

    /// <summary>
    ///  Parses one Record frame of a stream into a new packet that holds the opening of the stream.
    /// </summary>
    /// <remarks>
    ///  Each stored value holds what earlier packets of the conversation stored, or null before any store. The bytes that this call reads update it.
    /// </remarks>
    public static GatewayStream ParseRecord(ReadOnlySpan<byte> data, GatewayStream opening, ref byte? tsOutStoredValue)
    {
        var packet = new GatewayStream();
        packet.ParseRecordFrom(data, opening, ref tsOutStoredValue);
        return packet;
    }

    /// <summary>
    ///  Parses one Record frame of a stream into this instance, which then holds the opening of the stream.
    /// </summary>
    /// <remarks>
    ///  The packet holds the part objects of the opening, not copies. The opening can be this instance. Each stored value holds what earlier packets of the conversation stored, or null before any store. The bytes that this call reads update it.
    /// </remarks>
    public void ParseRecordFrom(ReadOnlySpan<byte> data, GatewayStream opening, ref byte? tsOutStoredValue)
    {
        ArgumentNullException.ThrowIfNull(opening);
        var greeting = opening.greeting;
        var challengeRequest = opening.challengeRequest;
        var authenticationResponse = opening.authenticationResponse;
        var metadata = opening.metadata;
        Clear();
        this.greeting = greeting;
        this.challengeRequest = challengeRequest;
        this.authenticationResponse = authenticationResponse;
        this.metadata = metadata;
        var messages = new GatewayFramedMessages(data, this, ref tsOutStoredValue, records: true);
        this.IsParsed = messages.Status == ParseStatus.Ok && messages.IsComplete;
        this.Trailer = messages.Trailer;
        this.MessageCount = messages.MessageCount;
        Messages.Keep(messages);
    }

    /// <summary>
    ///  Gets the length of the Record frame at the start of the bytes.
    /// </summary>
    /// <param name="source">The bytes, from the start of the frame.</param>
    /// <param name="length">The number of bytes of the frame, which can be more than the bytes hold.</param>
    /// <returns><see langword="true"/> when the bytes hold the fields that state the length; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">The bytes state a frame length that is not valid.</exception>
    public static bool TryGetFrameLength(ReadOnlySpan<byte> source, out int length)
    {
        const string FrameLengthInvalid = "The bytes state a frame length that is not valid.";
        const int recordLengthDependencyDistanceFromRecord = 0;
        const int recordLengthDependencyReadWidth = 1;
        global::System.Int128 RecordReassembleRule1Width;
        byte recordLengthValue;
        length = 0;
        const long RecordReassembleRule1Data2 = 4;
        try
        {
            if (source.Length < recordLengthDependencyDistanceFromRecord + recordLengthDependencyReadWidth) return false;
            recordLengthValue = source.Slice(recordLengthDependencyDistanceFromRecord, recordLengthDependencyReadWidth)[0];
            RecordReassembleRule1Width = checked((checked((global::System.Int128)(recordLengthValue)) * ((global::System.Int128)(RecordReassembleRule1Data2))));
        }
        catch (global::System.OverflowException)
        {
            throw new global::System.ArgumentOutOfRangeException(nameof(source), FrameLengthInvalid);
        }
        if (RecordReassembleRule1Width <= RuleEmptyWidth || RecordReassembleRule1Width > int.MaxValue) throw new global::System.ArgumentOutOfRangeException(nameof(source), FrameLengthInvalid);
        length = (int)RecordReassembleRule1Width;
        return true;
    }

    /// <summary>
    ///  Gets the number of bytes before the first Record at the start of the bytes.
    /// </summary>
    /// <param name="source">The bytes, from the start of the stream.</param>
    /// <param name="length">The number of bytes before the first Record, which can be more than the bytes hold.</param>
    /// <returns><see langword="true"/> when the bytes hold the fields that state the length; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">The bytes state an opening length that is not valid.</exception>
    public static bool TryGetOpeningLength(ReadOnlySpan<byte> source, out int length)
    {
        const string FrameLengthInvalid = "The bytes state an opening length that is not valid.";
        const int magicOccurrenceBytes = 3;
        const int versionOccurrenceBytes = 1;
        const int metadataLengthOccurrenceBytes = 4;
        const int metadataLengthDependencyDistanceFromMetadata = 4;
        const int metadataLengthDependencyReadWidth = 4;
        const int datasetOccurrenceBytes = 16;
        const int schemaOccurrenceBytes = 2;
        const int startOccurrenceBytes = 8;
        const int endOccurrenceBytes = 8;
        const int limitOccurrenceBytes = 8;
        const int metadataStypeInOccurrenceBytes = 1;
        const int metadataStypeOutOccurrenceBytes = 1;
        const int tsOutOccurrenceBytes = 1;
        const int symbolCstrLenOccurrenceBytes = 2;
        const int metadataReservedOccurrenceBytes = 53;
        const int schemaDefinitionLengthOccurrenceBytes = 4;
        const int schemaDefinitionLengthDependencyDistanceFromSchemaDefinition = -4;
        const int schemaDefinitionLengthDependencyReadWidth = 4;
        const int symbolsCountOccurrenceBytes = 4;
        const int symbolsCountDependencyDistanceFromSymbol = -4;
        const int symbolsCountDependencyReadWidth = 4;
        const int symbolOccurrenceBytes = 71;
        const int partialCountOccurrenceBytes = 4;
        const int partialCountDependencyDistanceFromPartialSymbol = -4;
        const int partialCountDependencyReadWidth = 4;
        const int partialSymbolOccurrenceBytes = 71;
        const int notFoundCountOccurrenceBytes = 4;
        const int notFoundCountDependencyDistanceFromNotFoundSymbol = -4;
        const int notFoundCountDependencyReadWidth = 4;
        const int notFoundSymbolOccurrenceBytes = 71;
        const int mappingsCountOccurrenceBytes = 4;
        const int mappingsCountDependencyDistanceFromSymbolMapping = -4;
        const int mappingsCountDependencyReadWidth = 4;
        const int rawSymbolInOccurrenceBytes = 71;
        const int intervalCountOccurrenceBytes = 4;
        const int intervalCountDependencyDistanceFromMappingInterval = -4;
        const int intervalCountDependencyReadWidth = 4;
        const int mappingIntervalOccurrenceBytes = 79;
        int recordOccurrenceStart;
        int greetingMeasuredStart;
        int GreetingControlFieldsSizeRule0End;
        global::System.Int128 GreetingControlFieldsSizeRule0Width;
        int GreetingControlFieldsSizeRule0Terminator0Bytes;
        int GreetingControlFieldsSizeRule0Terminator0Offset;
        int challengeRequestMeasuredStart;
        int ChallengeRequestControlFieldsSizeRule0End;
        global::System.Int128 ChallengeRequestControlFieldsSizeRule0Width;
        int ChallengeRequestControlFieldsSizeRule0Terminator0Bytes;
        int ChallengeRequestControlFieldsSizeRule0Terminator0Offset;
        int authenticationResponseMeasuredStart;
        int AuthenticationResponseControlFieldsSizeRule0End;
        global::System.Int128 AuthenticationResponseControlFieldsSizeRule0Width;
        int AuthenticationResponseControlFieldsSizeRule0Terminator0Bytes;
        int AuthenticationResponseControlFieldsSizeRule0Terminator0Offset;
        int metadataMeasuredStart;
        int metadataMeasuredLimit;
        uint metadataLengthValue;
        int MetadataSizeRule0End;
        global::System.Int128 MetadataSizeRule0Width;
        uint schemaDefinitionLengthValue;
        int SchemaDefinitionSizeRule0End;
        global::System.Int128 SchemaDefinitionSizeRule0Width;
        uint symbolsCountValue;
        global::System.UInt128 SymbolCountRule0Entries;
        uint partialCountValue;
        global::System.UInt128 PartialSymbolCountRule0Entries;
        uint notFoundCountValue;
        global::System.UInt128 NotFoundSymbolCountRule0Entries;
        uint mappingsCountValue;
        global::System.UInt128 SymbolMappingCountRule0Entries;
        int symbolMappingMeasuredStart;
        uint intervalCountValue;
        global::System.UInt128 MappingIntervalCountRule0Entries;
        int MetadataPaddingSizeRule0End;
        global::System.Int128 MetadataPaddingSizeRule0Width;
        length = 0;
        recordOccurrenceStart = 0;
        greetingMeasuredStart = recordOccurrenceStart;
        int? greetingControlFieldsEnd = null;
        const byte GreetingControlFieldsSizeRule0Terminator0 = 10;
        {
            GreetingControlFieldsSizeRule0Terminator0Offset = source.Slice(recordOccurrenceStart, source.Length - recordOccurrenceStart).IndexOf(GreetingControlFieldsSizeRule0Terminator0);
            if (GreetingControlFieldsSizeRule0Terminator0Offset < 0) return false;
            GreetingControlFieldsSizeRule0Terminator0Bytes = GreetingControlFieldsSizeRule0Terminator0Offset + sizeof(byte);
        }
        GreetingControlFieldsSizeRule0Width = checked((global::System.Int128)(GreetingControlFieldsSizeRule0Terminator0Bytes));
        if (GreetingControlFieldsSizeRule0Width < RuleEmptyWidth || GreetingControlFieldsSizeRule0Width > (long)source.Length - recordOccurrenceStart) return false;
        GreetingControlFieldsSizeRule0End = recordOccurrenceStart + (int)GreetingControlFieldsSizeRule0Width;
        greetingControlFieldsEnd = GreetingControlFieldsSizeRule0End;
        if (greetingControlFieldsEnd is int greetingControlFieldsDeclaredEnd)
        {
            recordOccurrenceStart = greetingControlFieldsDeclaredEnd;
        }
        else
        {
            recordOccurrenceStart = source.Length;
        }
        challengeRequestMeasuredStart = recordOccurrenceStart;
        int? challengeRequestControlFieldsEnd = null;
        const byte ChallengeRequestControlFieldsSizeRule0Terminator0 = 10;
        {
            ChallengeRequestControlFieldsSizeRule0Terminator0Offset = source.Slice(recordOccurrenceStart, source.Length - recordOccurrenceStart).IndexOf(ChallengeRequestControlFieldsSizeRule0Terminator0);
            if (ChallengeRequestControlFieldsSizeRule0Terminator0Offset < 0) return false;
            ChallengeRequestControlFieldsSizeRule0Terminator0Bytes = ChallengeRequestControlFieldsSizeRule0Terminator0Offset + sizeof(byte);
        }
        ChallengeRequestControlFieldsSizeRule0Width = checked((global::System.Int128)(ChallengeRequestControlFieldsSizeRule0Terminator0Bytes));
        if (ChallengeRequestControlFieldsSizeRule0Width < RuleEmptyWidth || ChallengeRequestControlFieldsSizeRule0Width > (long)source.Length - recordOccurrenceStart) return false;
        ChallengeRequestControlFieldsSizeRule0End = recordOccurrenceStart + (int)ChallengeRequestControlFieldsSizeRule0Width;
        challengeRequestControlFieldsEnd = ChallengeRequestControlFieldsSizeRule0End;
        if (challengeRequestControlFieldsEnd is int challengeRequestControlFieldsDeclaredEnd)
        {
            recordOccurrenceStart = challengeRequestControlFieldsDeclaredEnd;
        }
        else
        {
            recordOccurrenceStart = source.Length;
        }
        authenticationResponseMeasuredStart = recordOccurrenceStart;
        int? authenticationResponseControlFieldsEnd = null;
        const byte AuthenticationResponseControlFieldsSizeRule0Terminator0 = 10;
        {
            AuthenticationResponseControlFieldsSizeRule0Terminator0Offset = source.Slice(recordOccurrenceStart, source.Length - recordOccurrenceStart).IndexOf(AuthenticationResponseControlFieldsSizeRule0Terminator0);
            if (AuthenticationResponseControlFieldsSizeRule0Terminator0Offset < 0) return false;
            AuthenticationResponseControlFieldsSizeRule0Terminator0Bytes = AuthenticationResponseControlFieldsSizeRule0Terminator0Offset + sizeof(byte);
        }
        AuthenticationResponseControlFieldsSizeRule0Width = checked((global::System.Int128)(AuthenticationResponseControlFieldsSizeRule0Terminator0Bytes));
        if (AuthenticationResponseControlFieldsSizeRule0Width < RuleEmptyWidth || AuthenticationResponseControlFieldsSizeRule0Width > (long)source.Length - recordOccurrenceStart) return false;
        AuthenticationResponseControlFieldsSizeRule0End = recordOccurrenceStart + (int)AuthenticationResponseControlFieldsSizeRule0Width;
        authenticationResponseControlFieldsEnd = AuthenticationResponseControlFieldsSizeRule0End;
        if (authenticationResponseControlFieldsEnd is int authenticationResponseControlFieldsDeclaredEnd)
        {
            recordOccurrenceStart = authenticationResponseControlFieldsDeclaredEnd;
        }
        else
        {
            recordOccurrenceStart = source.Length;
        }
        metadataMeasuredStart = recordOccurrenceStart;
        metadataMeasuredLimit = source.Length;
        recordOccurrenceStart += magicOccurrenceBytes;
        recordOccurrenceStart += versionOccurrenceBytes;
        recordOccurrenceStart += metadataLengthOccurrenceBytes;
        if (metadataMeasuredLimit < metadataMeasuredStart + metadataLengthDependencyDistanceFromMetadata + metadataLengthDependencyReadWidth) return false;
        metadataLengthValue = BinaryPrimitives.ReadUInt32LittleEndian(source.Slice(metadataMeasuredStart + metadataLengthDependencyDistanceFromMetadata, metadataLengthDependencyReadWidth));
        int? metadataEnd = null;
        const long MetadataSizeRule0Data2 = 8;
        MetadataSizeRule0Width = checked((checked((global::System.Int128)(metadataLengthValue)) + ((global::System.Int128)(MetadataSizeRule0Data2))));
        if (MetadataSizeRule0Width < RuleEmptyWidth || MetadataSizeRule0Width > (long)metadataMeasuredLimit - metadataMeasuredStart) return false;
        MetadataSizeRule0End = metadataMeasuredStart + (int)MetadataSizeRule0Width;
        metadataEnd = MetadataSizeRule0End;
        if (MetadataSizeRule0End < recordOccurrenceStart) return false;
        if (metadataEnd is int metadataDeclaredEnd) metadataMeasuredLimit = metadataDeclaredEnd;
        recordOccurrenceStart += datasetOccurrenceBytes;
        recordOccurrenceStart += schemaOccurrenceBytes;
        recordOccurrenceStart += startOccurrenceBytes;
        recordOccurrenceStart += endOccurrenceBytes;
        recordOccurrenceStart += limitOccurrenceBytes;
        recordOccurrenceStart += metadataStypeInOccurrenceBytes;
        recordOccurrenceStart += metadataStypeOutOccurrenceBytes;
        recordOccurrenceStart += tsOutOccurrenceBytes;
        recordOccurrenceStart += symbolCstrLenOccurrenceBytes;
        recordOccurrenceStart += metadataReservedOccurrenceBytes;
        recordOccurrenceStart += schemaDefinitionLengthOccurrenceBytes;
        if (metadataMeasuredLimit < recordOccurrenceStart + schemaDefinitionLengthDependencyDistanceFromSchemaDefinition + schemaDefinitionLengthDependencyReadWidth) return false;
        schemaDefinitionLengthValue = BinaryPrimitives.ReadUInt32LittleEndian(source.Slice(recordOccurrenceStart + schemaDefinitionLengthDependencyDistanceFromSchemaDefinition, schemaDefinitionLengthDependencyReadWidth));
        int? schemaDefinitionEnd = null;
        SchemaDefinitionSizeRule0Width = checked((global::System.Int128)(schemaDefinitionLengthValue));
        if (SchemaDefinitionSizeRule0Width < RuleEmptyWidth || SchemaDefinitionSizeRule0Width > (long)metadataMeasuredLimit - recordOccurrenceStart) return false;
        SchemaDefinitionSizeRule0End = recordOccurrenceStart + (int)SchemaDefinitionSizeRule0Width;
        schemaDefinitionEnd = SchemaDefinitionSizeRule0End;
        if (schemaDefinitionEnd is int schemaDefinitionDeclaredEnd)
        {
            recordOccurrenceStart = schemaDefinitionDeclaredEnd;
        }
        else
        {
            recordOccurrenceStart = metadataMeasuredLimit;
        }
        recordOccurrenceStart += symbolsCountOccurrenceBytes;
        if (metadataMeasuredLimit < recordOccurrenceStart + symbolsCountDependencyDistanceFromSymbol + symbolsCountDependencyReadWidth) return false;
        symbolsCountValue = BinaryPrimitives.ReadUInt32LittleEndian(source.Slice(recordOccurrenceStart + symbolsCountDependencyDistanceFromSymbol, symbolsCountDependencyReadWidth));
        SymbolCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(symbolsCountValue)));
        while (SymbolCountRule0Entries > RuleCountNone)
        {
            recordOccurrenceStart += symbolOccurrenceBytes;
            SymbolCountRule0Entries--;
        }
        if (SymbolCountRule0Entries > RuleCountNone) throw new global::System.ArgumentOutOfRangeException(nameof(source), FrameLengthInvalid);
        recordOccurrenceStart += partialCountOccurrenceBytes;
        if (metadataMeasuredLimit < recordOccurrenceStart + partialCountDependencyDistanceFromPartialSymbol + partialCountDependencyReadWidth) return false;
        partialCountValue = BinaryPrimitives.ReadUInt32LittleEndian(source.Slice(recordOccurrenceStart + partialCountDependencyDistanceFromPartialSymbol, partialCountDependencyReadWidth));
        PartialSymbolCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(partialCountValue)));
        while (PartialSymbolCountRule0Entries > RuleCountNone)
        {
            recordOccurrenceStart += partialSymbolOccurrenceBytes;
            PartialSymbolCountRule0Entries--;
        }
        if (PartialSymbolCountRule0Entries > RuleCountNone) throw new global::System.ArgumentOutOfRangeException(nameof(source), FrameLengthInvalid);
        recordOccurrenceStart += notFoundCountOccurrenceBytes;
        if (metadataMeasuredLimit < recordOccurrenceStart + notFoundCountDependencyDistanceFromNotFoundSymbol + notFoundCountDependencyReadWidth) return false;
        notFoundCountValue = BinaryPrimitives.ReadUInt32LittleEndian(source.Slice(recordOccurrenceStart + notFoundCountDependencyDistanceFromNotFoundSymbol, notFoundCountDependencyReadWidth));
        NotFoundSymbolCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(notFoundCountValue)));
        while (NotFoundSymbolCountRule0Entries > RuleCountNone)
        {
            recordOccurrenceStart += notFoundSymbolOccurrenceBytes;
            NotFoundSymbolCountRule0Entries--;
        }
        if (NotFoundSymbolCountRule0Entries > RuleCountNone) throw new global::System.ArgumentOutOfRangeException(nameof(source), FrameLengthInvalid);
        recordOccurrenceStart += mappingsCountOccurrenceBytes;
        if (metadataMeasuredLimit < recordOccurrenceStart + mappingsCountDependencyDistanceFromSymbolMapping + mappingsCountDependencyReadWidth) return false;
        mappingsCountValue = BinaryPrimitives.ReadUInt32LittleEndian(source.Slice(recordOccurrenceStart + mappingsCountDependencyDistanceFromSymbolMapping, mappingsCountDependencyReadWidth));
        SymbolMappingCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(mappingsCountValue)));
        while (SymbolMappingCountRule0Entries > RuleCountNone)
        {
            symbolMappingMeasuredStart = recordOccurrenceStart;
            recordOccurrenceStart += rawSymbolInOccurrenceBytes;
            recordOccurrenceStart += intervalCountOccurrenceBytes;
            if (metadataMeasuredLimit < recordOccurrenceStart + intervalCountDependencyDistanceFromMappingInterval + intervalCountDependencyReadWidth) return false;
            intervalCountValue = BinaryPrimitives.ReadUInt32LittleEndian(source.Slice(recordOccurrenceStart + intervalCountDependencyDistanceFromMappingInterval, intervalCountDependencyReadWidth));
            MappingIntervalCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(intervalCountValue)));
            while (MappingIntervalCountRule0Entries > RuleCountNone)
            {
                recordOccurrenceStart += mappingIntervalOccurrenceBytes;
                MappingIntervalCountRule0Entries--;
            }
            if (MappingIntervalCountRule0Entries > RuleCountNone) throw new global::System.ArgumentOutOfRangeException(nameof(source), FrameLengthInvalid);
            SymbolMappingCountRule0Entries--;
        }
        if (SymbolMappingCountRule0Entries > RuleCountNone) throw new global::System.ArgumentOutOfRangeException(nameof(source), FrameLengthInvalid);
        int? metadataPaddingEnd = null;
        MetadataPaddingSizeRule0Width = checked((global::System.Int128)(((long)metadataMeasuredLimit - recordOccurrenceStart)));
        if (MetadataPaddingSizeRule0Width < RuleEmptyWidth || MetadataPaddingSizeRule0Width > (long)metadataMeasuredLimit - recordOccurrenceStart) return false;
        MetadataPaddingSizeRule0End = recordOccurrenceStart + (int)MetadataPaddingSizeRule0Width;
        metadataPaddingEnd = MetadataPaddingSizeRule0End;
        if (metadataPaddingEnd is int metadataPaddingDeclaredEnd)
        {
            recordOccurrenceStart = metadataPaddingDeclaredEnd;
        }
        else
        {
            recordOccurrenceStart = metadataMeasuredLimit;
        }
        recordOccurrenceStart = metadataMeasuredLimit;
        length = recordOccurrenceStart;
        return true;
    }

    internal IEnumerable<IGatewayMessage> Collect()
    {
        foreach (var entry in this.RecordFrames)
        {
            foreach (var message in entry.Collect())
            {
                yield return message;
            }
        }
        yield break;
    }

    internal void SetGreeting(Greeting value)
    {
        this.greeting = value;
    }

    internal void SetChallengeRequest(ChallengeRequest value)
    {
        this.challengeRequest = value;
    }

    internal void SetAuthenticationResponse(AuthenticationResponse value)
    {
        this.authenticationResponse = value;
    }

    internal void SetMetadata(Metadata value)
    {
        this.metadata = value;
    }

    private const int MetadataLengthLength = 4;
    private static readonly FieldDescriptor MetadataLengthField = new("MetadataLength", FieldKind.UnsignedInteger, MetadataLengthLength) { ByteOrder = "Little", IsSigned = false };
    private const int RecordLengthLength = 1;
    private static readonly FieldDescriptor RecordLengthField = new("RecordLength", FieldKind.UnsignedInteger, RecordLengthLength) { IsSigned = false };
    private const int RtypeLength = 1;
    private static readonly FieldDescriptor RtypeField = new("Rtype", FieldKind.Enum, RtypeLength) { IsSigned = false };

    /// <summary>
    ///  Gets the number of bytes that Encode writes, including variable-length content and each omittable field that is present.
    /// </summary>
    public int EncodedByteLength
    {
        get
        {
            var length = 0;
            byte? tsOutStoredValue = null;
            length += (this.Greeting).EncodedByteLength;
            length += (this.ChallengeRequest).EncodedByteLength;
            length += (this.AuthenticationResponse).EncodedByteLength;
            length += (this.Metadata).EncodedByteLength;
            tsOutStoredValue = (byte)this.Metadata.tsOut;
            var recordMeasuredEntries = this.RecordFrames;
            var recordMeasuredFirst = 0;
            while (recordMeasuredFirst < recordMeasuredEntries.Count)
            {
                var recordMeasuredFrame = recordMeasuredEntries[recordMeasuredFirst];
                length += RecordHeader.MessageByteLength;
                if (recordMeasuredFrame.RecordBody is TradeMsg or Mbp1Msg or Mbp10Msg)
                {
                    length += ((IMessage)recordMeasuredFrame.RecordBody!).EncodedByteLength + ((IMessage)recordMeasuredFrame.RecordBody!).Trailer.Length;
                }
                else if (recordMeasuredFrame.RecordBody is OhlcvMsg and IGatewayMessage { DataValue: RecordBodyCode.OhlcvMsg })
                {
                    length += ((IMessage)recordMeasuredFrame.RecordBody!).EncodedByteLength + ((IMessage)recordMeasuredFrame.RecordBody!).Trailer.Length;
                }
                else if (recordMeasuredFrame.RecordBody is StatusMsg or InstrumentDefMsg or ImbalanceMsg or ErrorMsg or SymbolMappingMsg or SystemMsg or StatMsg)
                {
                    length += ((IMessage)recordMeasuredFrame.RecordBody!).EncodedByteLength + ((IMessage)recordMeasuredFrame.RecordBody!).Trailer.Length;
                }
                else if (recordMeasuredFrame.RecordBody is OhlcvMsg and IGatewayMessage { DataValue: RecordBodyCode.OhlcvMsg2 })
                {
                    length += ((IMessage)recordMeasuredFrame.RecordBody!).EncodedByteLength + ((IMessage)recordMeasuredFrame.RecordBody!).Trailer.Length;
                }
                else if (recordMeasuredFrame.RecordBody is OhlcvMsg and IGatewayMessage { DataValue: RecordBodyCode.OhlcvMsg3 })
                {
                    length += ((IMessage)recordMeasuredFrame.RecordBody!).EncodedByteLength + ((IMessage)recordMeasuredFrame.RecordBody!).Trailer.Length;
                }
                else if (recordMeasuredFrame.RecordBody is OhlcvMsg and IGatewayMessage { DataValue: RecordBodyCode.OhlcvMsg4 })
                {
                    length += ((IMessage)recordMeasuredFrame.RecordBody!).EncodedByteLength + ((IMessage)recordMeasuredFrame.RecordBody!).Trailer.Length;
                }
                else if (recordMeasuredFrame.RecordBody is OhlcvMsg and IGatewayMessage { DataValue: RecordBodyCode.OhlcvMsg5 })
                {
                    length += ((IMessage)recordMeasuredFrame.RecordBody!).EncodedByteLength + ((IMessage)recordMeasuredFrame.RecordBody!).Trailer.Length;
                }
                else if (recordMeasuredFrame.RecordBody is OhlcvMsg and IGatewayMessage { DataValue: RecordBodyCode.OhlcvMsg6 })
                {
                    length += ((IMessage)recordMeasuredFrame.RecordBody!).EncodedByteLength + ((IMessage)recordMeasuredFrame.RecordBody!).Trailer.Length;
                }
                else if (recordMeasuredFrame.RecordBody is MboMsg)
                {
                    length += ((IMessage)recordMeasuredFrame.RecordBody!).EncodedByteLength + ((IMessage)recordMeasuredFrame.RecordBody!).Trailer.Length;
                }
                else if (recordMeasuredFrame.RecordBody is Cmbp1Msg and IGatewayMessage { DataValue: RecordBodyCode.Cmbp1Msg })
                {
                    length += ((IMessage)recordMeasuredFrame.RecordBody!).EncodedByteLength + ((IMessage)recordMeasuredFrame.RecordBody!).Trailer.Length;
                }
                else if (recordMeasuredFrame.RecordBody is CbboMsg and IGatewayMessage { DataValue: RecordBodyCode.CbboMsg })
                {
                    length += ((IMessage)recordMeasuredFrame.RecordBody!).EncodedByteLength + ((IMessage)recordMeasuredFrame.RecordBody!).Trailer.Length;
                }
                else if (recordMeasuredFrame.RecordBody is CbboMsg and IGatewayMessage { DataValue: RecordBodyCode.CbboMsg2 })
                {
                    length += ((IMessage)recordMeasuredFrame.RecordBody!).EncodedByteLength + ((IMessage)recordMeasuredFrame.RecordBody!).Trailer.Length;
                }
                else if (recordMeasuredFrame.RecordBody is Cmbp1Msg and IGatewayMessage { DataValue: RecordBodyCode.Cmbp1Msg2 })
                {
                    length += ((IMessage)recordMeasuredFrame.RecordBody!).EncodedByteLength + ((IMessage)recordMeasuredFrame.RecordBody!).Trailer.Length;
                }
                else if (recordMeasuredFrame.RecordBody is BboMsg and IGatewayMessage { DataValue: RecordBodyCode.BboMsg })
                {
                    length += ((IMessage)recordMeasuredFrame.RecordBody!).EncodedByteLength + ((IMessage)recordMeasuredFrame.RecordBody!).Trailer.Length;
                }
                else if (recordMeasuredFrame.RecordBody is BboMsg and IGatewayMessage { DataValue: RecordBodyCode.BboMsg2 })
                {
                    length += ((IMessage)recordMeasuredFrame.RecordBody!).EncodedByteLength + ((IMessage)recordMeasuredFrame.RecordBody!).Trailer.Length;
                }
                else if (recordMeasuredFrame.RecordBody is UnknownGatewayMessage)
                {
                    length += ((IMessage)recordMeasuredFrame.RecordBody!).EncodedByteLength + ((IMessage)recordMeasuredFrame.RecordBody!).Trailer.Length;
                }
                bool tsOutTimeStoredPresence;
                {
                    bool TsOutTimeOptionalRule0Present;
                    const byte TsOutStoreAction0Default0 = 0;
                    const byte TsOutTimeOptionalRule0Data2 = 1;
                    TsOutTimeOptionalRule0Present = ((tsOutStoredValue ?? TsOutStoreAction0Default0) == TsOutTimeOptionalRule0Data2);
                    tsOutTimeStoredPresence = TsOutTimeOptionalRule0Present;
                }
                if (tsOutTimeStoredPresence)
                {
                    length += recordMeasuredFrame.TsOutTimeEncodedLength;
                }
                recordMeasuredFirst++;
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
        byte? tsOutStoredValue = null;
        const int metadataLengthDependencyDistanceFromMetadata = 4;
        const int metadataLengthDependencyReadWidth = 4;
        const int rtypeDependencyDistanceFromRecordHeader = 1;
        const int rtypeDependencyReadWidth = 1;
        const int recordLengthDependencyDistanceFromRecordHeader = 0;
        const int recordLengthDependencyReadWidth = 1;
        var gatewayStreamWriterStart = cursor;
        var greetingWriterStart = cursor;
        var greetingWriting = this.Greeting;
        if (!((IGroupEntry)greetingWriting).Write(data, cursor, out cursor, out failure)) return false;
        var challengeRequestWriterStart = cursor;
        var challengeRequestWriting = this.ChallengeRequest;
        if (!((IGroupEntry)challengeRequestWriting).Write(data, cursor, out cursor, out failure)) return false;
        var authenticationResponseWriterStart = cursor;
        var authenticationResponseWriting = this.AuthenticationResponse;
        if (!((IGroupEntry)authenticationResponseWriting).Write(data, cursor, out cursor, out failure)) return false;
        var metadataWriterStart = cursor;
        var metadataOccurrenceBytes = 0;
        {
            metadataOccurrenceBytes += (this.Metadata).EncodedByteLength;
            tsOutStoredValue = (byte)this.Metadata.tsOut;
        }
        global::System.Int128 metadataLengthStampedValue;
        {
            const long MetadataSizeRule0Data2 = 8;
            global::System.Int128 measure = metadataOccurrenceBytes;
            try
            {
                metadataLengthStampedValue = checked((measure - checked((((global::System.Int128)(RuleNumberZero)) + ((global::System.Int128)(MetadataSizeRule0Data2))))));
            }
            catch (global::System.OverflowException)
            {
                failure = new EncodeFailure(MetadataLengthField, EncodeStatus.RuleMismatch, null);
                return false;
            }
        }
        if (metadataLengthStampedValue > uint.MaxValue)
        {
            failure = new EncodeFailure(MetadataLengthField, EncodeStatus.TooLong, null);
            return false;
        }
        if (metadataLengthStampedValue < uint.MinValue)
        {
            failure = new EncodeFailure(MetadataLengthField, EncodeStatus.DoesNotFit, null);
            return false;
        }
        var metadataWriting = this.Metadata;
        if (!((IGroupEntry)metadataWriting).Write(data, cursor, out cursor, out failure)) return false;
        tsOutStoredValue = (byte)this.Metadata.tsOut;
        BinaryPrimitives.WriteUInt32LittleEndian(data.Slice(metadataWriterStart + metadataLengthDependencyDistanceFromMetadata, metadataLengthDependencyReadWidth), unchecked((uint)(metadataLengthStampedValue)));
        {
            var metadataLengthStamped = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(metadataWriterStart + metadataLengthDependencyDistanceFromMetadata, metadataLengthDependencyReadWidth));
            if (this.Metadata.metadataLength != metadataLengthStamped)
            {
                this.Metadata.metadataLength = metadataLengthStamped;
                ((IFieldOwner)this.Metadata).Touch();
            }
        }
        var recordWriterPosition = cursor;
        var recordMeasuredBytes = 0;
        {
            var recordMeasuredEntries = this.RecordFrames;
            var recordMeasuredFirst = 0;
            while (recordMeasuredFirst < recordMeasuredEntries.Count)
            {
                var recordMeasuredFrame = recordMeasuredEntries[recordMeasuredFirst];
                recordMeasuredBytes += RecordHeader.MessageByteLength;
                if (recordMeasuredFrame.RecordBody is TradeMsg or Mbp1Msg or Mbp10Msg)
                {
                    recordMeasuredBytes += ((IMessage)recordMeasuredFrame.RecordBody!).EncodedByteLength + ((IMessage)recordMeasuredFrame.RecordBody!).Trailer.Length;
                }
                else if (recordMeasuredFrame.RecordBody is OhlcvMsg and IGatewayMessage { DataValue: RecordBodyCode.OhlcvMsg })
                {
                    recordMeasuredBytes += ((IMessage)recordMeasuredFrame.RecordBody!).EncodedByteLength + ((IMessage)recordMeasuredFrame.RecordBody!).Trailer.Length;
                }
                else if (recordMeasuredFrame.RecordBody is StatusMsg or InstrumentDefMsg or ImbalanceMsg or ErrorMsg or SymbolMappingMsg or SystemMsg or StatMsg)
                {
                    recordMeasuredBytes += ((IMessage)recordMeasuredFrame.RecordBody!).EncodedByteLength + ((IMessage)recordMeasuredFrame.RecordBody!).Trailer.Length;
                }
                else if (recordMeasuredFrame.RecordBody is OhlcvMsg and IGatewayMessage { DataValue: RecordBodyCode.OhlcvMsg2 })
                {
                    recordMeasuredBytes += ((IMessage)recordMeasuredFrame.RecordBody!).EncodedByteLength + ((IMessage)recordMeasuredFrame.RecordBody!).Trailer.Length;
                }
                else if (recordMeasuredFrame.RecordBody is OhlcvMsg and IGatewayMessage { DataValue: RecordBodyCode.OhlcvMsg3 })
                {
                    recordMeasuredBytes += ((IMessage)recordMeasuredFrame.RecordBody!).EncodedByteLength + ((IMessage)recordMeasuredFrame.RecordBody!).Trailer.Length;
                }
                else if (recordMeasuredFrame.RecordBody is OhlcvMsg and IGatewayMessage { DataValue: RecordBodyCode.OhlcvMsg4 })
                {
                    recordMeasuredBytes += ((IMessage)recordMeasuredFrame.RecordBody!).EncodedByteLength + ((IMessage)recordMeasuredFrame.RecordBody!).Trailer.Length;
                }
                else if (recordMeasuredFrame.RecordBody is OhlcvMsg and IGatewayMessage { DataValue: RecordBodyCode.OhlcvMsg5 })
                {
                    recordMeasuredBytes += ((IMessage)recordMeasuredFrame.RecordBody!).EncodedByteLength + ((IMessage)recordMeasuredFrame.RecordBody!).Trailer.Length;
                }
                else if (recordMeasuredFrame.RecordBody is OhlcvMsg and IGatewayMessage { DataValue: RecordBodyCode.OhlcvMsg6 })
                {
                    recordMeasuredBytes += ((IMessage)recordMeasuredFrame.RecordBody!).EncodedByteLength + ((IMessage)recordMeasuredFrame.RecordBody!).Trailer.Length;
                }
                else if (recordMeasuredFrame.RecordBody is MboMsg)
                {
                    recordMeasuredBytes += ((IMessage)recordMeasuredFrame.RecordBody!).EncodedByteLength + ((IMessage)recordMeasuredFrame.RecordBody!).Trailer.Length;
                }
                else if (recordMeasuredFrame.RecordBody is Cmbp1Msg and IGatewayMessage { DataValue: RecordBodyCode.Cmbp1Msg })
                {
                    recordMeasuredBytes += ((IMessage)recordMeasuredFrame.RecordBody!).EncodedByteLength + ((IMessage)recordMeasuredFrame.RecordBody!).Trailer.Length;
                }
                else if (recordMeasuredFrame.RecordBody is CbboMsg and IGatewayMessage { DataValue: RecordBodyCode.CbboMsg })
                {
                    recordMeasuredBytes += ((IMessage)recordMeasuredFrame.RecordBody!).EncodedByteLength + ((IMessage)recordMeasuredFrame.RecordBody!).Trailer.Length;
                }
                else if (recordMeasuredFrame.RecordBody is CbboMsg and IGatewayMessage { DataValue: RecordBodyCode.CbboMsg2 })
                {
                    recordMeasuredBytes += ((IMessage)recordMeasuredFrame.RecordBody!).EncodedByteLength + ((IMessage)recordMeasuredFrame.RecordBody!).Trailer.Length;
                }
                else if (recordMeasuredFrame.RecordBody is Cmbp1Msg and IGatewayMessage { DataValue: RecordBodyCode.Cmbp1Msg2 })
                {
                    recordMeasuredBytes += ((IMessage)recordMeasuredFrame.RecordBody!).EncodedByteLength + ((IMessage)recordMeasuredFrame.RecordBody!).Trailer.Length;
                }
                else if (recordMeasuredFrame.RecordBody is BboMsg and IGatewayMessage { DataValue: RecordBodyCode.BboMsg })
                {
                    recordMeasuredBytes += ((IMessage)recordMeasuredFrame.RecordBody!).EncodedByteLength + ((IMessage)recordMeasuredFrame.RecordBody!).Trailer.Length;
                }
                else if (recordMeasuredFrame.RecordBody is BboMsg and IGatewayMessage { DataValue: RecordBodyCode.BboMsg2 })
                {
                    recordMeasuredBytes += ((IMessage)recordMeasuredFrame.RecordBody!).EncodedByteLength + ((IMessage)recordMeasuredFrame.RecordBody!).Trailer.Length;
                }
                else if (recordMeasuredFrame.RecordBody is UnknownGatewayMessage)
                {
                    recordMeasuredBytes += ((IMessage)recordMeasuredFrame.RecordBody!).EncodedByteLength + ((IMessage)recordMeasuredFrame.RecordBody!).Trailer.Length;
                }
                bool tsOutTimeStoredPresence;
                {
                    bool TsOutTimeOptionalRule0Present;
                    const byte TsOutStoreAction0Default0 = 0;
                    const byte TsOutTimeOptionalRule0Data2 = 1;
                    TsOutTimeOptionalRule0Present = ((tsOutStoredValue ?? TsOutStoreAction0Default0) == TsOutTimeOptionalRule0Data2);
                    tsOutTimeStoredPresence = TsOutTimeOptionalRule0Present;
                }
                if (tsOutTimeStoredPresence)
                {
                    recordMeasuredBytes += recordMeasuredFrame.TsOutTimeEncodedLength;
                }
                recordMeasuredFirst++;
            }
        }
        var recordWriterEntries = this.RecordFrames;
        var recordIndex = 0;
        while (recordIndex < recordWriterEntries.Count)
        {
            var recordWriterFrame = recordWriterEntries[recordIndex];
            var recordWriterStart = cursor;
            var recordOccurrenceBytes = 0;
            {
                recordOccurrenceBytes += RecordHeader.MessageByteLength;
                if (recordWriterFrame.RecordBody is TradeMsg or Mbp1Msg or Mbp10Msg)
                {
                    recordOccurrenceBytes += ((IMessage)recordWriterFrame.RecordBody!).EncodedByteLength + ((IMessage)recordWriterFrame.RecordBody!).Trailer.Length;
                }
                else if (recordWriterFrame.RecordBody is OhlcvMsg and IGatewayMessage { DataValue: RecordBodyCode.OhlcvMsg })
                {
                    recordOccurrenceBytes += ((IMessage)recordWriterFrame.RecordBody!).EncodedByteLength + ((IMessage)recordWriterFrame.RecordBody!).Trailer.Length;
                }
                else if (recordWriterFrame.RecordBody is StatusMsg or InstrumentDefMsg or ImbalanceMsg or ErrorMsg or SymbolMappingMsg or SystemMsg or StatMsg)
                {
                    recordOccurrenceBytes += ((IMessage)recordWriterFrame.RecordBody!).EncodedByteLength + ((IMessage)recordWriterFrame.RecordBody!).Trailer.Length;
                }
                else if (recordWriterFrame.RecordBody is OhlcvMsg and IGatewayMessage { DataValue: RecordBodyCode.OhlcvMsg2 })
                {
                    recordOccurrenceBytes += ((IMessage)recordWriterFrame.RecordBody!).EncodedByteLength + ((IMessage)recordWriterFrame.RecordBody!).Trailer.Length;
                }
                else if (recordWriterFrame.RecordBody is OhlcvMsg and IGatewayMessage { DataValue: RecordBodyCode.OhlcvMsg3 })
                {
                    recordOccurrenceBytes += ((IMessage)recordWriterFrame.RecordBody!).EncodedByteLength + ((IMessage)recordWriterFrame.RecordBody!).Trailer.Length;
                }
                else if (recordWriterFrame.RecordBody is OhlcvMsg and IGatewayMessage { DataValue: RecordBodyCode.OhlcvMsg4 })
                {
                    recordOccurrenceBytes += ((IMessage)recordWriterFrame.RecordBody!).EncodedByteLength + ((IMessage)recordWriterFrame.RecordBody!).Trailer.Length;
                }
                else if (recordWriterFrame.RecordBody is OhlcvMsg and IGatewayMessage { DataValue: RecordBodyCode.OhlcvMsg5 })
                {
                    recordOccurrenceBytes += ((IMessage)recordWriterFrame.RecordBody!).EncodedByteLength + ((IMessage)recordWriterFrame.RecordBody!).Trailer.Length;
                }
                else if (recordWriterFrame.RecordBody is OhlcvMsg and IGatewayMessage { DataValue: RecordBodyCode.OhlcvMsg6 })
                {
                    recordOccurrenceBytes += ((IMessage)recordWriterFrame.RecordBody!).EncodedByteLength + ((IMessage)recordWriterFrame.RecordBody!).Trailer.Length;
                }
                else if (recordWriterFrame.RecordBody is MboMsg)
                {
                    recordOccurrenceBytes += ((IMessage)recordWriterFrame.RecordBody!).EncodedByteLength + ((IMessage)recordWriterFrame.RecordBody!).Trailer.Length;
                }
                else if (recordWriterFrame.RecordBody is Cmbp1Msg and IGatewayMessage { DataValue: RecordBodyCode.Cmbp1Msg })
                {
                    recordOccurrenceBytes += ((IMessage)recordWriterFrame.RecordBody!).EncodedByteLength + ((IMessage)recordWriterFrame.RecordBody!).Trailer.Length;
                }
                else if (recordWriterFrame.RecordBody is CbboMsg and IGatewayMessage { DataValue: RecordBodyCode.CbboMsg })
                {
                    recordOccurrenceBytes += ((IMessage)recordWriterFrame.RecordBody!).EncodedByteLength + ((IMessage)recordWriterFrame.RecordBody!).Trailer.Length;
                }
                else if (recordWriterFrame.RecordBody is CbboMsg and IGatewayMessage { DataValue: RecordBodyCode.CbboMsg2 })
                {
                    recordOccurrenceBytes += ((IMessage)recordWriterFrame.RecordBody!).EncodedByteLength + ((IMessage)recordWriterFrame.RecordBody!).Trailer.Length;
                }
                else if (recordWriterFrame.RecordBody is Cmbp1Msg and IGatewayMessage { DataValue: RecordBodyCode.Cmbp1Msg2 })
                {
                    recordOccurrenceBytes += ((IMessage)recordWriterFrame.RecordBody!).EncodedByteLength + ((IMessage)recordWriterFrame.RecordBody!).Trailer.Length;
                }
                else if (recordWriterFrame.RecordBody is BboMsg and IGatewayMessage { DataValue: RecordBodyCode.BboMsg })
                {
                    recordOccurrenceBytes += ((IMessage)recordWriterFrame.RecordBody!).EncodedByteLength + ((IMessage)recordWriterFrame.RecordBody!).Trailer.Length;
                }
                else if (recordWriterFrame.RecordBody is BboMsg and IGatewayMessage { DataValue: RecordBodyCode.BboMsg2 })
                {
                    recordOccurrenceBytes += ((IMessage)recordWriterFrame.RecordBody!).EncodedByteLength + ((IMessage)recordWriterFrame.RecordBody!).Trailer.Length;
                }
                else if (recordWriterFrame.RecordBody is UnknownGatewayMessage)
                {
                    recordOccurrenceBytes += ((IMessage)recordWriterFrame.RecordBody!).EncodedByteLength + ((IMessage)recordWriterFrame.RecordBody!).Trailer.Length;
                }
                bool tsOutTimeStoredPresence;
                {
                    bool TsOutTimeOptionalRule0Present;
                    const byte TsOutStoreAction0Default0 = 0;
                    const byte TsOutTimeOptionalRule0Data2 = 1;
                    TsOutTimeOptionalRule0Present = ((tsOutStoredValue ?? TsOutStoreAction0Default0) == TsOutTimeOptionalRule0Data2);
                    tsOutTimeStoredPresence = TsOutTimeOptionalRule0Present;
                }
                if (tsOutTimeStoredPresence)
                {
                    recordOccurrenceBytes += recordWriterFrame.TsOutTimeEncodedLength;
                }
            }
            global::System.Int128 recordLengthStampedValue;
            {
                const long RecordReassembleRule1Data2 = 4;
                global::System.Int128 measure = recordOccurrenceBytes;
                try
                {
                    recordLengthStampedValue = ((measure) switch { var RecordReassembleRule1Inverse0 => DivideRuleIntegerExact(RecordReassembleRule1Inverse0, checked((((global::System.Int128)(RuleNumberOne)) * ((global::System.Int128)(RecordReassembleRule1Data2))))) });
                }
                catch (global::System.Exception exception) when (exception is global::System.OverflowException or global::System.DivideByZeroException or RuleValueException)
                {
                    failure = new EncodeFailure(RecordLengthField, EncodeStatus.RuleMismatch, index);
                    return false;
                }
            }
            if (recordLengthStampedValue > byte.MaxValue)
            {
                failure = new EncodeFailure(RecordLengthField, EncodeStatus.TooLong, index);
                return false;
            }
            if (recordLengthStampedValue < byte.MinValue)
            {
                failure = new EncodeFailure(RecordLengthField, EncodeStatus.DoesNotFit, index);
                return false;
            }
            var recordHeaderWriterStart = cursor;
            var recordHeaderWriting = recordWriterFrame.RecordHeader;
            if (!((IGroupEntry)recordHeaderWriting).Write(data, cursor, out cursor, out failure))
            {
                failure = failure.InMessage(index);
                return false;
            }
            var recordBodyWriterStart = cursor;
            {
                const int RecordBodyNoCase = 0;
                const int RecordBodyBranchRule0Case = 1;
                const int RecordBodyBranchRule1Case = 2;
                const int RecordBodyBranchRule2Case = 3;
                const int RecordBodyBranchRule3Case = 4;
                const int RecordBodyBranchRule4Case = 5;
                const int RecordBodyBranchRule5Case = 6;
                const int RecordBodyBranchRule6Case = 7;
                const int RecordBodyBranchRule7Case = 8;
                const int RecordBodyBranchRule8Case = 9;
                const int RecordBodyBranchRule9Case = 10;
                const int RecordBodyBranchRule10Case = 11;
                const int RecordBodyBranchRule11Case = 12;
                const int RecordBodyBranchRule12Case = 13;
                const int RecordBodyBranchRule13Case = 14;
                const int RecordBodyBranchRule14Case = 15;
                const int RecordBodyBranchRule15Case = 16;
                const int RecordBodyBranchRule16Case = 17;
                const int RecordBodyBranchRule17Case = 18;
                const int RecordBodyBranchRule18Case = 19;
                const int RecordBodyBranchRule19Case = 20;
                const int RecordBodyBranchRule20Case = 21;
                const int RecordBodyBranchRule21Case = 22;
                const int RecordBodyBranchRule22Case = 23;
                const int RecordBodyNothingWritten = -1;
                var recordBodyWrittenCase = RecordBodyNothingWritten;
                if (recordWriterFrame.RecordBody is TradeMsg)
                {
                    var tradeMsgWriterStart = cursor;
                    var tradeMsgWriting = (TradeMsg)recordWriterFrame.RecordBody!;
                    if (!((IMessage)tradeMsgWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    tradeMsgWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += tradeMsgWriting.Trailer.Length;
                    index++;
                    data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0] = unchecked((byte)(((IGatewayMessage)tradeMsgWriting).DataValue));
                    {
                        var rtypeStamped = (Rtype)data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0];
                        if (recordWriterFrame.RecordHeader.rtype != rtypeStamped)
                        {
                            recordWriterFrame.RecordHeader.rtype = rtypeStamped;
                            ((IFieldOwner)recordWriterFrame.RecordHeader).Touch();
                        }
                    }
                }
                else if (recordWriterFrame.RecordBody is Mbp1Msg)
                {
                    var mbp1MsgWriterStart = cursor;
                    var mbp1MsgWriting = (Mbp1Msg)recordWriterFrame.RecordBody!;
                    if (!((IMessage)mbp1MsgWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    mbp1MsgWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += mbp1MsgWriting.Trailer.Length;
                    index++;
                    data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0] = unchecked((byte)(((IGatewayMessage)mbp1MsgWriting).DataValue));
                    {
                        var rtypeStamped = (Rtype)data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0];
                        if (recordWriterFrame.RecordHeader.rtype != rtypeStamped)
                        {
                            recordWriterFrame.RecordHeader.rtype = rtypeStamped;
                            ((IFieldOwner)recordWriterFrame.RecordHeader).Touch();
                        }
                    }
                }
                else if (recordWriterFrame.RecordBody is Mbp10Msg)
                {
                    var mbp10MsgWriterStart = cursor;
                    var mbp10MsgWriting = (Mbp10Msg)recordWriterFrame.RecordBody!;
                    if (!((IMessage)mbp10MsgWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    mbp10MsgWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += mbp10MsgWriting.Trailer.Length;
                    index++;
                    data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0] = unchecked((byte)(((IGatewayMessage)mbp10MsgWriting).DataValue));
                    {
                        var rtypeStamped = (Rtype)data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0];
                        if (recordWriterFrame.RecordHeader.rtype != rtypeStamped)
                        {
                            recordWriterFrame.RecordHeader.rtype = rtypeStamped;
                            ((IFieldOwner)recordWriterFrame.RecordHeader).Touch();
                        }
                    }
                }
                else if (recordWriterFrame.RecordBody is OhlcvMsg and IGatewayMessage { DataValue: RecordBodyCode.OhlcvMsg })
                {
                    var ohlcvMsgWriterStart = cursor;
                    var ohlcvMsgWriting = (OhlcvMsg)recordWriterFrame.RecordBody!;
                    if (!((IMessage)ohlcvMsgWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    ohlcvMsgWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += ohlcvMsgWriting.Trailer.Length;
                    index++;
                    data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0] = unchecked((byte)(((IGatewayMessage)ohlcvMsgWriting).DataValue));
                    {
                        var rtypeStamped = (Rtype)data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0];
                        if (recordWriterFrame.RecordHeader.rtype != rtypeStamped)
                        {
                            recordWriterFrame.RecordHeader.rtype = rtypeStamped;
                            ((IFieldOwner)recordWriterFrame.RecordHeader).Touch();
                        }
                    }
                    recordBodyWrittenCase = RecordBodyBranchRule3Case;
                }
                else if (recordWriterFrame.RecordBody is StatusMsg)
                {
                    var statusMsgWriterStart = cursor;
                    var statusMsgWriting = (StatusMsg)recordWriterFrame.RecordBody!;
                    if (!((IMessage)statusMsgWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    statusMsgWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += statusMsgWriting.Trailer.Length;
                    index++;
                    data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0] = unchecked((byte)(((IGatewayMessage)statusMsgWriting).DataValue));
                    {
                        var rtypeStamped = (Rtype)data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0];
                        if (recordWriterFrame.RecordHeader.rtype != rtypeStamped)
                        {
                            recordWriterFrame.RecordHeader.rtype = rtypeStamped;
                            ((IFieldOwner)recordWriterFrame.RecordHeader).Touch();
                        }
                    }
                }
                else if (recordWriterFrame.RecordBody is InstrumentDefMsg)
                {
                    var instrumentDefMsgWriterStart = cursor;
                    var instrumentDefMsgWriting = (InstrumentDefMsg)recordWriterFrame.RecordBody!;
                    if (!((IMessage)instrumentDefMsgWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    instrumentDefMsgWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += instrumentDefMsgWriting.Trailer.Length;
                    index++;
                    data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0] = unchecked((byte)(((IGatewayMessage)instrumentDefMsgWriting).DataValue));
                    {
                        var rtypeStamped = (Rtype)data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0];
                        if (recordWriterFrame.RecordHeader.rtype != rtypeStamped)
                        {
                            recordWriterFrame.RecordHeader.rtype = rtypeStamped;
                            ((IFieldOwner)recordWriterFrame.RecordHeader).Touch();
                        }
                    }
                }
                else if (recordWriterFrame.RecordBody is ImbalanceMsg)
                {
                    var imbalanceMsgWriterStart = cursor;
                    var imbalanceMsgWriting = (ImbalanceMsg)recordWriterFrame.RecordBody!;
                    if (!((IMessage)imbalanceMsgWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    imbalanceMsgWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += imbalanceMsgWriting.Trailer.Length;
                    index++;
                    data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0] = unchecked((byte)(((IGatewayMessage)imbalanceMsgWriting).DataValue));
                    {
                        var rtypeStamped = (Rtype)data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0];
                        if (recordWriterFrame.RecordHeader.rtype != rtypeStamped)
                        {
                            recordWriterFrame.RecordHeader.rtype = rtypeStamped;
                            ((IFieldOwner)recordWriterFrame.RecordHeader).Touch();
                        }
                    }
                }
                else if (recordWriterFrame.RecordBody is ErrorMsg)
                {
                    var errorMsgWriterStart = cursor;
                    var errorMsgWriting = (ErrorMsg)recordWriterFrame.RecordBody!;
                    if (!((IMessage)errorMsgWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    errorMsgWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += errorMsgWriting.Trailer.Length;
                    index++;
                    data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0] = unchecked((byte)(((IGatewayMessage)errorMsgWriting).DataValue));
                    {
                        var rtypeStamped = (Rtype)data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0];
                        if (recordWriterFrame.RecordHeader.rtype != rtypeStamped)
                        {
                            recordWriterFrame.RecordHeader.rtype = rtypeStamped;
                            ((IFieldOwner)recordWriterFrame.RecordHeader).Touch();
                        }
                    }
                }
                else if (recordWriterFrame.RecordBody is SymbolMappingMsg)
                {
                    var symbolMappingMsgWriterStart = cursor;
                    var symbolMappingMsgWriting = (SymbolMappingMsg)recordWriterFrame.RecordBody!;
                    if (!((IMessage)symbolMappingMsgWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    symbolMappingMsgWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += symbolMappingMsgWriting.Trailer.Length;
                    index++;
                    data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0] = unchecked((byte)(((IGatewayMessage)symbolMappingMsgWriting).DataValue));
                    {
                        var rtypeStamped = (Rtype)data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0];
                        if (recordWriterFrame.RecordHeader.rtype != rtypeStamped)
                        {
                            recordWriterFrame.RecordHeader.rtype = rtypeStamped;
                            ((IFieldOwner)recordWriterFrame.RecordHeader).Touch();
                        }
                    }
                }
                else if (recordWriterFrame.RecordBody is SystemMsg)
                {
                    var systemMsgWriterStart = cursor;
                    var systemMsgWriting = (SystemMsg)recordWriterFrame.RecordBody!;
                    if (!((IMessage)systemMsgWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    systemMsgWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += systemMsgWriting.Trailer.Length;
                    index++;
                    data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0] = unchecked((byte)(((IGatewayMessage)systemMsgWriting).DataValue));
                    {
                        var rtypeStamped = (Rtype)data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0];
                        if (recordWriterFrame.RecordHeader.rtype != rtypeStamped)
                        {
                            recordWriterFrame.RecordHeader.rtype = rtypeStamped;
                            ((IFieldOwner)recordWriterFrame.RecordHeader).Touch();
                        }
                    }
                }
                else if (recordWriterFrame.RecordBody is StatMsg)
                {
                    var statMsgWriterStart = cursor;
                    var statMsgWriting = (StatMsg)recordWriterFrame.RecordBody!;
                    if (!((IMessage)statMsgWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    statMsgWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += statMsgWriting.Trailer.Length;
                    index++;
                    data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0] = unchecked((byte)(((IGatewayMessage)statMsgWriting).DataValue));
                    {
                        var rtypeStamped = (Rtype)data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0];
                        if (recordWriterFrame.RecordHeader.rtype != rtypeStamped)
                        {
                            recordWriterFrame.RecordHeader.rtype = rtypeStamped;
                            ((IFieldOwner)recordWriterFrame.RecordHeader).Touch();
                        }
                    }
                }
                else if (recordWriterFrame.RecordBody is OhlcvMsg and IGatewayMessage { DataValue: RecordBodyCode.OhlcvMsg2 })
                {
                    var ohlcvMsgWriterStart = cursor;
                    var ohlcvMsgWriting = (OhlcvMsg)recordWriterFrame.RecordBody!;
                    if (!((IMessage)ohlcvMsgWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    ohlcvMsgWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += ohlcvMsgWriting.Trailer.Length;
                    index++;
                    data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0] = unchecked((byte)(((IGatewayMessage)ohlcvMsgWriting).DataValue));
                    {
                        var rtypeStamped = (Rtype)data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0];
                        if (recordWriterFrame.RecordHeader.rtype != rtypeStamped)
                        {
                            recordWriterFrame.RecordHeader.rtype = rtypeStamped;
                            ((IFieldOwner)recordWriterFrame.RecordHeader).Touch();
                        }
                    }
                    recordBodyWrittenCase = RecordBodyBranchRule11Case;
                }
                else if (recordWriterFrame.RecordBody is OhlcvMsg and IGatewayMessage { DataValue: RecordBodyCode.OhlcvMsg3 })
                {
                    var ohlcvMsgWriterStart = cursor;
                    var ohlcvMsgWriting = (OhlcvMsg)recordWriterFrame.RecordBody!;
                    if (!((IMessage)ohlcvMsgWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    ohlcvMsgWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += ohlcvMsgWriting.Trailer.Length;
                    index++;
                    data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0] = unchecked((byte)(((IGatewayMessage)ohlcvMsgWriting).DataValue));
                    {
                        var rtypeStamped = (Rtype)data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0];
                        if (recordWriterFrame.RecordHeader.rtype != rtypeStamped)
                        {
                            recordWriterFrame.RecordHeader.rtype = rtypeStamped;
                            ((IFieldOwner)recordWriterFrame.RecordHeader).Touch();
                        }
                    }
                    recordBodyWrittenCase = RecordBodyBranchRule12Case;
                }
                else if (recordWriterFrame.RecordBody is OhlcvMsg and IGatewayMessage { DataValue: RecordBodyCode.OhlcvMsg4 })
                {
                    var ohlcvMsgWriterStart = cursor;
                    var ohlcvMsgWriting = (OhlcvMsg)recordWriterFrame.RecordBody!;
                    if (!((IMessage)ohlcvMsgWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    ohlcvMsgWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += ohlcvMsgWriting.Trailer.Length;
                    index++;
                    data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0] = unchecked((byte)(((IGatewayMessage)ohlcvMsgWriting).DataValue));
                    {
                        var rtypeStamped = (Rtype)data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0];
                        if (recordWriterFrame.RecordHeader.rtype != rtypeStamped)
                        {
                            recordWriterFrame.RecordHeader.rtype = rtypeStamped;
                            ((IFieldOwner)recordWriterFrame.RecordHeader).Touch();
                        }
                    }
                    recordBodyWrittenCase = RecordBodyBranchRule13Case;
                }
                else if (recordWriterFrame.RecordBody is OhlcvMsg and IGatewayMessage { DataValue: RecordBodyCode.OhlcvMsg5 })
                {
                    var ohlcvMsgWriterStart = cursor;
                    var ohlcvMsgWriting = (OhlcvMsg)recordWriterFrame.RecordBody!;
                    if (!((IMessage)ohlcvMsgWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    ohlcvMsgWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += ohlcvMsgWriting.Trailer.Length;
                    index++;
                    data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0] = unchecked((byte)(((IGatewayMessage)ohlcvMsgWriting).DataValue));
                    {
                        var rtypeStamped = (Rtype)data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0];
                        if (recordWriterFrame.RecordHeader.rtype != rtypeStamped)
                        {
                            recordWriterFrame.RecordHeader.rtype = rtypeStamped;
                            ((IFieldOwner)recordWriterFrame.RecordHeader).Touch();
                        }
                    }
                    recordBodyWrittenCase = RecordBodyBranchRule14Case;
                }
                else if (recordWriterFrame.RecordBody is OhlcvMsg and IGatewayMessage { DataValue: RecordBodyCode.OhlcvMsg6 })
                {
                    var ohlcvMsgWriterStart = cursor;
                    var ohlcvMsgWriting = (OhlcvMsg)recordWriterFrame.RecordBody!;
                    if (!((IMessage)ohlcvMsgWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    ohlcvMsgWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += ohlcvMsgWriting.Trailer.Length;
                    index++;
                    data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0] = unchecked((byte)(((IGatewayMessage)ohlcvMsgWriting).DataValue));
                    {
                        var rtypeStamped = (Rtype)data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0];
                        if (recordWriterFrame.RecordHeader.rtype != rtypeStamped)
                        {
                            recordWriterFrame.RecordHeader.rtype = rtypeStamped;
                            ((IFieldOwner)recordWriterFrame.RecordHeader).Touch();
                        }
                    }
                    recordBodyWrittenCase = RecordBodyBranchRule15Case;
                }
                else if (recordWriterFrame.RecordBody is MboMsg)
                {
                    var mboMsgWriterStart = cursor;
                    var mboMsgWriting = (MboMsg)recordWriterFrame.RecordBody!;
                    if (!((IMessage)mboMsgWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    mboMsgWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += mboMsgWriting.Trailer.Length;
                    index++;
                    data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0] = unchecked((byte)(((IGatewayMessage)mboMsgWriting).DataValue));
                    {
                        var rtypeStamped = (Rtype)data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0];
                        if (recordWriterFrame.RecordHeader.rtype != rtypeStamped)
                        {
                            recordWriterFrame.RecordHeader.rtype = rtypeStamped;
                            ((IFieldOwner)recordWriterFrame.RecordHeader).Touch();
                        }
                    }
                }
                else if (recordWriterFrame.RecordBody is Cmbp1Msg and IGatewayMessage { DataValue: RecordBodyCode.Cmbp1Msg })
                {
                    var cmbp1MsgWriterStart = cursor;
                    var cmbp1MsgWriting = (Cmbp1Msg)recordWriterFrame.RecordBody!;
                    if (!((IMessage)cmbp1MsgWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    cmbp1MsgWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += cmbp1MsgWriting.Trailer.Length;
                    index++;
                    data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0] = unchecked((byte)(((IGatewayMessage)cmbp1MsgWriting).DataValue));
                    {
                        var rtypeStamped = (Rtype)data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0];
                        if (recordWriterFrame.RecordHeader.rtype != rtypeStamped)
                        {
                            recordWriterFrame.RecordHeader.rtype = rtypeStamped;
                            ((IFieldOwner)recordWriterFrame.RecordHeader).Touch();
                        }
                    }
                    recordBodyWrittenCase = RecordBodyBranchRule17Case;
                }
                else if (recordWriterFrame.RecordBody is CbboMsg and IGatewayMessage { DataValue: RecordBodyCode.CbboMsg })
                {
                    var cbboMsgWriterStart = cursor;
                    var cbboMsgWriting = (CbboMsg)recordWriterFrame.RecordBody!;
                    if (!((IMessage)cbboMsgWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    cbboMsgWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += cbboMsgWriting.Trailer.Length;
                    index++;
                    data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0] = unchecked((byte)(((IGatewayMessage)cbboMsgWriting).DataValue));
                    {
                        var rtypeStamped = (Rtype)data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0];
                        if (recordWriterFrame.RecordHeader.rtype != rtypeStamped)
                        {
                            recordWriterFrame.RecordHeader.rtype = rtypeStamped;
                            ((IFieldOwner)recordWriterFrame.RecordHeader).Touch();
                        }
                    }
                    recordBodyWrittenCase = RecordBodyBranchRule18Case;
                }
                else if (recordWriterFrame.RecordBody is CbboMsg and IGatewayMessage { DataValue: RecordBodyCode.CbboMsg2 })
                {
                    var cbboMsgWriterStart = cursor;
                    var cbboMsgWriting = (CbboMsg)recordWriterFrame.RecordBody!;
                    if (!((IMessage)cbboMsgWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    cbboMsgWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += cbboMsgWriting.Trailer.Length;
                    index++;
                    data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0] = unchecked((byte)(((IGatewayMessage)cbboMsgWriting).DataValue));
                    {
                        var rtypeStamped = (Rtype)data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0];
                        if (recordWriterFrame.RecordHeader.rtype != rtypeStamped)
                        {
                            recordWriterFrame.RecordHeader.rtype = rtypeStamped;
                            ((IFieldOwner)recordWriterFrame.RecordHeader).Touch();
                        }
                    }
                    recordBodyWrittenCase = RecordBodyBranchRule19Case;
                }
                else if (recordWriterFrame.RecordBody is Cmbp1Msg and IGatewayMessage { DataValue: RecordBodyCode.Cmbp1Msg2 })
                {
                    var cmbp1MsgWriterStart = cursor;
                    var cmbp1MsgWriting = (Cmbp1Msg)recordWriterFrame.RecordBody!;
                    if (!((IMessage)cmbp1MsgWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    cmbp1MsgWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += cmbp1MsgWriting.Trailer.Length;
                    index++;
                    data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0] = unchecked((byte)(((IGatewayMessage)cmbp1MsgWriting).DataValue));
                    {
                        var rtypeStamped = (Rtype)data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0];
                        if (recordWriterFrame.RecordHeader.rtype != rtypeStamped)
                        {
                            recordWriterFrame.RecordHeader.rtype = rtypeStamped;
                            ((IFieldOwner)recordWriterFrame.RecordHeader).Touch();
                        }
                    }
                    recordBodyWrittenCase = RecordBodyBranchRule20Case;
                }
                else if (recordWriterFrame.RecordBody is BboMsg and IGatewayMessage { DataValue: RecordBodyCode.BboMsg })
                {
                    var bboMsgWriterStart = cursor;
                    var bboMsgWriting = (BboMsg)recordWriterFrame.RecordBody!;
                    if (!((IMessage)bboMsgWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    bboMsgWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += bboMsgWriting.Trailer.Length;
                    index++;
                    data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0] = unchecked((byte)(((IGatewayMessage)bboMsgWriting).DataValue));
                    {
                        var rtypeStamped = (Rtype)data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0];
                        if (recordWriterFrame.RecordHeader.rtype != rtypeStamped)
                        {
                            recordWriterFrame.RecordHeader.rtype = rtypeStamped;
                            ((IFieldOwner)recordWriterFrame.RecordHeader).Touch();
                        }
                    }
                    recordBodyWrittenCase = RecordBodyBranchRule21Case;
                }
                else if (recordWriterFrame.RecordBody is BboMsg and IGatewayMessage { DataValue: RecordBodyCode.BboMsg2 })
                {
                    var bboMsgWriterStart = cursor;
                    var bboMsgWriting = (BboMsg)recordWriterFrame.RecordBody!;
                    if (!((IMessage)bboMsgWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    bboMsgWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += bboMsgWriting.Trailer.Length;
                    index++;
                    data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0] = unchecked((byte)(((IGatewayMessage)bboMsgWriting).DataValue));
                    {
                        var rtypeStamped = (Rtype)data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0];
                        if (recordWriterFrame.RecordHeader.rtype != rtypeStamped)
                        {
                            recordWriterFrame.RecordHeader.rtype = rtypeStamped;
                            ((IFieldOwner)recordWriterFrame.RecordHeader).Touch();
                        }
                    }
                    recordBodyWrittenCase = RecordBodyBranchRule22Case;
                }
                else if (recordWriterFrame.RecordBody is UnknownGatewayMessage)
                {
                    var recordBodyWriting = (UnknownGatewayMessage)recordWriterFrame.RecordBody!;
                    if (!((IMessage)recordBodyWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    recordBodyWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += recordBodyWriting.Trailer.Length;
                    index++;
                    data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0] = unchecked((byte)(((IGatewayMessage)recordBodyWriting).DataValue));
                    {
                        var rtypeStamped = (Rtype)data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0];
                        if (recordWriterFrame.RecordHeader.rtype != rtypeStamped)
                        {
                            recordWriterFrame.RecordHeader.rtype = rtypeStamped;
                            ((IFieldOwner)recordWriterFrame.RecordHeader).Touch();
                        }
                    }
                    recordBodyWrittenCase = RecordBodyNoCase;
                }
                if (recordBodyWrittenCase != RecordBodyNothingWritten)
                {
                    int recordBodyCase;
                    byte rtypeValue;
                    recordBodyCase = RecordBodyNoCase;
                    {
                        if (length < recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader + rtypeDependencyReadWidth) { failure = new EncodeFailure(EncodeStatus.RuleMismatch).InMessage(index); return false; }
                        rtypeValue = (byte)(Rtype)data.Slice(recordHeaderWriterStart + rtypeDependencyDistanceFromRecordHeader, rtypeDependencyReadWidth)[0];
                        const byte RecordBodyBranchRule0Data2 = 0;
                        const byte RecordBodyBranchRule1Data2 = 1;
                        const byte RecordBodyBranchRule2Data2 = 10;
                        const byte RecordBodyBranchRule3Data2 = 17;
                        const byte RecordBodyBranchRule4Data2 = 18;
                        const byte RecordBodyBranchRule5Data2 = 19;
                        const byte RecordBodyBranchRule6Data2 = 20;
                        const byte RecordBodyBranchRule7Data2 = 21;
                        const byte RecordBodyBranchRule8Data2 = 22;
                        const byte RecordBodyBranchRule9Data2 = 23;
                        const byte RecordBodyBranchRule10Data2 = 24;
                        const byte RecordBodyBranchRule11Data2 = 32;
                        const byte RecordBodyBranchRule12Data2 = 33;
                        const byte RecordBodyBranchRule13Data2 = 34;
                        const byte RecordBodyBranchRule14Data2 = 35;
                        const byte RecordBodyBranchRule15Data2 = 36;
                        const byte RecordBodyBranchRule16Data2 = 160;
                        const byte RecordBodyBranchRule17Data2 = 177;
                        const byte RecordBodyBranchRule18Data2 = 192;
                        const byte RecordBodyBranchRule19Data2 = 193;
                        const byte RecordBodyBranchRule20Data2 = 194;
                        const byte RecordBodyBranchRule21Data2 = 195;
                        const byte RecordBodyBranchRule22Data2 = 196;
                        if ((rtypeValue == RecordBodyBranchRule0Data2)) { recordBodyCase = RecordBodyBranchRule0Case; }
                        else if ((rtypeValue == RecordBodyBranchRule1Data2)) { recordBodyCase = RecordBodyBranchRule1Case; }
                        else if ((rtypeValue == RecordBodyBranchRule2Data2)) { recordBodyCase = RecordBodyBranchRule2Case; }
                        else if ((rtypeValue == RecordBodyBranchRule3Data2)) { recordBodyCase = RecordBodyBranchRule3Case; }
                        else if ((rtypeValue == RecordBodyBranchRule4Data2)) { recordBodyCase = RecordBodyBranchRule4Case; }
                        else if ((rtypeValue == RecordBodyBranchRule5Data2)) { recordBodyCase = RecordBodyBranchRule5Case; }
                        else if ((rtypeValue == RecordBodyBranchRule6Data2)) { recordBodyCase = RecordBodyBranchRule6Case; }
                        else if ((rtypeValue == RecordBodyBranchRule7Data2)) { recordBodyCase = RecordBodyBranchRule7Case; }
                        else if ((rtypeValue == RecordBodyBranchRule8Data2)) { recordBodyCase = RecordBodyBranchRule8Case; }
                        else if ((rtypeValue == RecordBodyBranchRule9Data2)) { recordBodyCase = RecordBodyBranchRule9Case; }
                        else if ((rtypeValue == RecordBodyBranchRule10Data2)) { recordBodyCase = RecordBodyBranchRule10Case; }
                        else if ((rtypeValue == RecordBodyBranchRule11Data2)) { recordBodyCase = RecordBodyBranchRule11Case; }
                        else if ((rtypeValue == RecordBodyBranchRule12Data2)) { recordBodyCase = RecordBodyBranchRule12Case; }
                        else if ((rtypeValue == RecordBodyBranchRule13Data2)) { recordBodyCase = RecordBodyBranchRule13Case; }
                        else if ((rtypeValue == RecordBodyBranchRule14Data2)) { recordBodyCase = RecordBodyBranchRule14Case; }
                        else if ((rtypeValue == RecordBodyBranchRule15Data2)) { recordBodyCase = RecordBodyBranchRule15Case; }
                        else if ((rtypeValue == RecordBodyBranchRule16Data2)) { recordBodyCase = RecordBodyBranchRule16Case; }
                        else if ((rtypeValue == RecordBodyBranchRule17Data2)) { recordBodyCase = RecordBodyBranchRule17Case; }
                        else if ((rtypeValue == RecordBodyBranchRule18Data2)) { recordBodyCase = RecordBodyBranchRule18Case; }
                        else if ((rtypeValue == RecordBodyBranchRule19Data2)) { recordBodyCase = RecordBodyBranchRule19Case; }
                        else if ((rtypeValue == RecordBodyBranchRule20Data2)) { recordBodyCase = RecordBodyBranchRule20Case; }
                        else if ((rtypeValue == RecordBodyBranchRule21Data2)) { recordBodyCase = RecordBodyBranchRule21Case; }
                        else if ((rtypeValue == RecordBodyBranchRule22Data2)) { recordBodyCase = RecordBodyBranchRule22Case; }
                    }
                    if (recordBodyCase != recordBodyWrittenCase)
                    {
                        failure = new EncodeFailure(RtypeField, EncodeStatus.RuleMismatch, index);
                        return false;
                    }
                }
            }
            var tsOutTimeWriterStart = cursor;
            bool tsOutTimeWritten;
            {
                bool TsOutTimeOptionalRule0Present;
                const byte TsOutStoreAction0Default0 = 0;
                const byte TsOutTimeOptionalRule0Data2 = 1;
                TsOutTimeOptionalRule0Present = ((tsOutStoredValue ?? TsOutStoreAction0Default0) == TsOutTimeOptionalRule0Data2);
                tsOutTimeWritten = TsOutTimeOptionalRule0Present;
            }
            if (tsOutTimeWritten)
            {
                if (!recordWriterFrame.WriteTsOutTime(data, cursor, out cursor, out failure))
                {
                    failure = failure.InMessage(index);
                    return false;
                }
            }
            {
                const byte TsOutStoreAction0Default0 = 0;
                const byte TsOutTimeOptionalRule0Data2 = 1;
                bool agrees;
                agrees = (((tsOutStoredValue ?? TsOutStoreAction0Default0) == TsOutTimeOptionalRule0Data2)) == (tsOutTimeWritten);
                if (!agrees)
                {
                    failure = new EncodeFailure(EncodeStatus.RuleMismatch).InMessage(index);
                    return false;
                }
            }
            data.Slice(recordHeaderWriterStart + recordLengthDependencyDistanceFromRecordHeader, recordLengthDependencyReadWidth)[0] = unchecked((byte)(recordLengthStampedValue));
            {
                var recordLengthStamped = data.Slice(recordHeaderWriterStart + recordLengthDependencyDistanceFromRecordHeader, recordLengthDependencyReadWidth)[0];
                if (recordWriterFrame.RecordHeader.recordLength != recordLengthStamped)
                {
                    recordWriterFrame.RecordHeader.recordLength = recordLengthStamped;
                    ((IFieldOwner)recordWriterFrame.RecordHeader).Touch();
                }
            }
            {
                byte recordLengthValue;
                const long RecordSizeRule2Data2 = 4;
                global::System.Int128 measure = recordOccurrenceBytes;
                bool agrees;
                try
                {
                    recordLengthValue = checked((byte)(recordLengthStampedValue));
                    agrees = checked((checked((global::System.Int128)(recordLengthValue)) * ((global::System.Int128)(RecordSizeRule2Data2)))) == measure;
                }
                catch (global::System.OverflowException)
                {
                    agrees = false;
                }
                if (!agrees)
                {
                    failure = new EncodeFailure(RecordLengthField, EncodeStatus.RuleMismatch, index);
                    return false;
                }
            }
            recordIndex++;
        }
        {
            global::System.Int128 measure = recordWriterPosition + recordMeasuredBytes;
            bool agrees;
            agrees = checked((global::System.Int128)(length)) == measure;
            if (!agrees)
            {
                failure = new EncodeFailure(EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (Trailer.Length != 0)
        {
            failure = new EncodeFailure(EncodeStatus.RuleMismatch);
            return false;
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
        failures.AddRange(this.Greeting.Validate());
        failures.AddRange(this.ChallengeRequest.Validate());
        failures.AddRange(this.AuthenticationResponse.Validate());
        failures.AddRange(this.Metadata.Validate());
        foreach (var entry in this.RecordFrames)
        {
            failures.AddRange(entry.Validate());
        }
        return failures;
    }

    /// <summary>
    ///  Returns the packet formatted on multiple lines.
    /// </summary>
    /// <remarks>
    ///  The text holds each kind and message of the packet, and a line for each frame entry. It also holds the trailer length if the packet has a trailer.
    /// </remarks>
    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.AppendLine("Packet");
        builder.Append("  RecordFrames: ").Append(this.RecordFrames.Count).AppendLine();
        foreach (var entry in this.RecordFrames)
        {
            builder.Append("  ").AppendLine(entry.ToString());
        }
        if (Trailer.Length != 0)
        {
            builder.Append("  Trailer: ").Append(Trailer.Length).AppendLine(" bytes");
        }
        return builder.ToString();
    }
}
