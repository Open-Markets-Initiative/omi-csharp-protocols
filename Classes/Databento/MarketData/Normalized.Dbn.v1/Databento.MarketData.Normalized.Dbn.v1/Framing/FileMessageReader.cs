using System.Buffers.Binary;
using static Databento.MarketData.Normalized.Dbn.v1.Rules;

namespace Databento.MarketData.Normalized.Dbn.v1;

/// <summary>
///  Represents one message of a packet and the bytes that it occupies.
/// </summary>
public readonly ref struct FileFramedMessage
{
    internal FileFramedMessage(RecordBodyCode dataValue, ReadOnlySpan<byte> header, ReadOnlySpan<byte> payload, int consumed, IFileMessage? message)
    {
        DataValue = dataValue;
        Header = header;
        Payload = payload;
        Consumed = consumed;
        Message = message;
    }

    /// <summary>
    ///  Gets the message type.
    /// </summary>
    public RecordBodyCode DataValue { get; }
    /// <summary>
    ///  Gets the bytes that precede the payload of the message.
    /// </summary>
    public ReadOnlySpan<byte> Header { get; }
    /// <summary>
    ///  Gets the payload bytes of the message.
    /// </summary>
    public ReadOnlySpan<byte> Payload { get; }
    /// <summary>
    ///  Gets the number of packet bytes that the message occupies.
    /// </summary>
    public int Consumed { get; }
    /// <summary>
    ///  Gets the parsed message, or <see langword="null"/> if the packet or frame carries no message.
    /// </summary>
    public IFileMessage? Message { get; }
}

/// <summary>
///  Reads the messages of a packet one at a time.
/// </summary>
public ref struct FileMessageReader
{
    private const int Start = 0;
    private const int TsOutTimePresentReached = 1;
    private const int RecordBodyReached = 2;
    private const int RecordOccurrence = 3;
    private const int RecordFrame = 4;
    private const int MappingIntervalOccurrence = 5;
    private const int SymbolMappingOccurrence = 6;
    private const int NotFoundSymbolOccurrence = 7;
    private const int PartialSymbolOccurrence = 8;
    private const int SymbolOccurrence = 9;
    private const int Ended = 10;
    private readonly ReadOnlySpan<byte> buffer;
    private int state;
    private int position;
    private readonly FileValue target;
    private RecordFrame? recordFrame;
    private const int Unset = -1;
    private IFileMessage found = null!;
    private int frameStart;
    private int foundStart;
    private int foundFinish = Unset;
    private int payloadFinish;
    private RecordBodyCode foundDataValue;
    private int countedFinish = Unset;
    private ReadOnlySpan<byte> trailerBytes;
    private ulong declaredCount;
    int metadataEntryStart;
    int metadataEnd;
    int MetadataSizeRule0End;
    global::System.Int128 MetadataSizeRule0Width;
    uint metadataLengthValue;
    byte tsOutValue;
    int schemaDefinitionEntryStart;
    int schemaDefinitionEnd;
    int SchemaDefinitionSizeRule0End;
    global::System.Int128 SchemaDefinitionSizeRule0Width;
    uint schemaDefinitionLengthValue;
    uint symbolsCountValue;
    global::System.UInt128 SymbolCountRule0Entries;
    bool TsOutTimeOptionalRule0Present;
    int tsOutTimeEntryStart;
    int recordEntryStart;
    int recordEnd;
    int RecordReassembleRule1End;
    global::System.Int128 RecordReassembleRule1Width;
    byte recordLengthValue;
    int RecordSizeRule2End;
    global::System.Int128 RecordSizeRule2Width;
    int recordHeaderEntryStart;
    int recordBodyCase;
    byte rtypeValue;
    int RecordPayloadRule0End;
    global::System.Int128 RecordPayloadRule0RawEnd;
    int mappingIntervalEntryStart;
    int symbolMappingEntryStart;
    uint intervalCountValue;
    global::System.UInt128 MappingIntervalCountRule0Entries;
    int notFoundSymbolEntryStart;
    uint mappingsCountValue;
    global::System.UInt128 SymbolMappingCountRule0Entries;
    int partialSymbolEntryStart;
    uint notFoundCountValue;
    global::System.UInt128 NotFoundSymbolCountRule0Entries;
    int symbolEntryStart;
    uint partialCountValue;
    global::System.UInt128 PartialSymbolCountRule0Entries;
    const int metadataLengthDependencyDistanceFromMetadata = 4;
    const int metadataLengthDependencyReadWidth = 4;
    const int tsOutDependencyDistanceFromTsOut = 0;
    const int tsOutDependencyReadWidth = 1;
    const int schemaDefinitionLengthDependencyDistanceFromSchemaDefinition = -4;
    const int schemaDefinitionLengthDependencyReadWidth = 4;
    const int symbolsCountDependencyDistanceFromSymbol = -4;
    const int symbolsCountDependencyReadWidth = 4;
    const int recordLengthDependencyDistanceFromRecord = 0;
    const int recordLengthDependencyReadWidth = 1;
    const int rtypeDependencyDistanceFromRecordBody = -15;
    const int rtypeDependencyReadWidth = 1;
    const int rtypeDependencyDistanceFromOhlcvMsg = -15;
    const int rtypeDependencyDistanceFromCmbp1Msg = -15;
    const int rtypeDependencyDistanceFromCbboMsg = -15;
    const int rtypeDependencyDistanceFromBboMsg = -15;
    const int intervalCountDependencyDistanceFromMappingInterval = -4;
    const int intervalCountDependencyReadWidth = 4;
    const int mappingsCountDependencyDistanceFromSymbolMapping = -4;
    const int mappingsCountDependencyReadWidth = 4;
    const int notFoundCountDependencyDistanceFromNotFoundSymbol = -4;
    const int notFoundCountDependencyReadWidth = 4;
    const int partialCountDependencyDistanceFromPartialSymbol = -4;
    const int partialCountDependencyReadWidth = 4;
    private ref byte? tsOutStoredValue;

    /// <summary>
    ///  Initializes a new instance of the <see cref="FileMessageReader"/> structure that reads the messages from the bytes of a packet.
    /// </summary>
    /// <remarks>
    ///  Each stored value holds what earlier packets of the conversation stored, or null before any store. The bytes that this call reads update it.
    /// </remarks>
    public FileMessageReader(ReadOnlySpan<byte> buffer, ref byte? tsOutStoredValue) : this(buffer, new FileValue(), ref tsOutStoredValue)
    {
    }

    internal FileMessageReader(ReadOnlySpan<byte> buffer, FileValue target, ref byte? tsOutStoredValue, bool records = false)
    {
        this.buffer = buffer;
        Status = ReadStatus.Complete;
        this.target = target;
        this.tsOutStoredValue = ref tsOutStoredValue;
        if (records) this.state = RecordFrame;
    }

    /// <summary>
    ///  Gets the message at the current position.
    /// </summary>
    public FileFramedMessage Current { get; private set; }
    /// <summary>
    ///  Gets the reason that reading stopped, or <see cref="ReadStatus.Complete"/> if no error stopped it.
    /// </summary>
    public ReadStatus Status { get; private set; }
    /// <summary>
    ///  Gets a value indicating whether no error has stopped reading.
    /// </summary>
    public bool IsComplete => Status == ReadStatus.Complete;
    /// <summary>
    ///  Gets the offset of the next unread byte.
    /// </summary>
    public int Consumed => position;
    /// <summary>
    ///  Gets the message count declared by the packet, or 0 if the packet carries no messages.
    /// </summary>
    public ulong MessageCount => declaredCount;
    internal ReadOnlySpan<byte> Trailer => trailerBytes;

    /// <summary>
    ///  Advances to the next message.
    /// </summary>
    /// <returns><see langword="true"/> if there is another message, or <see langword="false"/> if the end was reached or an error stopped reading.</returns>
    public bool MoveNext()
    {
        switch (state)
        {
            case Start:
            {
                metadataEntryStart = position;
                const long MetadataSizeRule0Data2 = 8;
                {
                    if (buffer.Length < position + metadataLengthDependencyDistanceFromMetadata + metadataLengthDependencyReadWidth) return Stop(ReadStatus.Truncated);
                    metadataLengthValue = BinaryPrimitives.ReadUInt32LittleEndian(buffer.Slice(position + metadataLengthDependencyDistanceFromMetadata, metadataLengthDependencyReadWidth));
                    MetadataSizeRule0Width = checked((checked((global::System.Int128)(metadataLengthValue)) + ((global::System.Int128)(MetadataSizeRule0Data2))));
                }
                if (MetadataSizeRule0Width < RuleEmptyWidth) return Stop(ReadStatus.InvalidLength);
                if (MetadataSizeRule0Width > (long)buffer.Length - position) return Stop(ReadStatus.Truncated);
                MetadataSizeRule0End = position + (int)MetadataSizeRule0Width;
                metadataEnd = MetadataSizeRule0End;
                const int MagicWidth = 3;
                if (!Take(MagicWidth, metadataEnd)) return false;
                const int VersionWidth = 1;
                if (!Take(VersionWidth, metadataEnd)) return false;
                const int MetadataLengthWidth = 4;
                if (!Take(MetadataLengthWidth, metadataEnd)) return false;
                const int DatasetWidth = 16;
                if (!Take(DatasetWidth, metadataEnd)) return false;
                const int SchemaWidth = 2;
                if (!Take(SchemaWidth, metadataEnd)) return false;
                const int StartWidth = 8;
                if (!Take(StartWidth, metadataEnd)) return false;
                const int EndWidth = 8;
                if (!Take(EndWidth, metadataEnd)) return false;
                const int LimitWidth = 8;
                if (!Take(LimitWidth, metadataEnd)) return false;
                const int MetadataReserved8Width = 8;
                if (!Take(MetadataReserved8Width, metadataEnd)) return false;
                const int StypeInWidth = 1;
                if (!Take(StypeInWidth, metadataEnd)) return false;
                const int StypeOutWidth = 1;
                if (!Take(StypeOutWidth, metadataEnd)) return false;
                if (metadataEnd < position + tsOutDependencyDistanceFromTsOut + tsOutDependencyReadWidth) return Stop(ReadStatus.Truncated);
                tsOutValue = (byte)(TsOut)buffer.Slice(position + tsOutDependencyDistanceFromTsOut, tsOutDependencyReadWidth)[0];
                tsOutStoredValue = tsOutValue;
                const int TsOutWidth = 1;
                if (!Take(TsOutWidth, metadataEnd)) return false;
                const int MetadataReservedWidth = 47;
                if (!Take(MetadataReservedWidth, metadataEnd)) return false;
                const int SchemaDefinitionLengthWidth = 4;
                if (!Take(SchemaDefinitionLengthWidth, metadataEnd)) return false;
                schemaDefinitionEntryStart = position;
                {
                    if (metadataEnd < position + schemaDefinitionLengthDependencyDistanceFromSchemaDefinition + schemaDefinitionLengthDependencyReadWidth) return Stop(ReadStatus.Truncated);
                    schemaDefinitionLengthValue = BinaryPrimitives.ReadUInt32LittleEndian(buffer.Slice(position + schemaDefinitionLengthDependencyDistanceFromSchemaDefinition, schemaDefinitionLengthDependencyReadWidth));
                    SchemaDefinitionSizeRule0Width = checked((global::System.Int128)(schemaDefinitionLengthValue));
                }
                if (SchemaDefinitionSizeRule0Width < RuleEmptyWidth) return Stop(ReadStatus.InvalidLength);
                if (SchemaDefinitionSizeRule0Width > (long)metadataEnd - position) return Stop(ReadStatus.Truncated);
                SchemaDefinitionSizeRule0End = position + (int)SchemaDefinitionSizeRule0Width;
                schemaDefinitionEnd = SchemaDefinitionSizeRule0End;
                position = schemaDefinitionEnd;
                if (position < schemaDefinitionEnd)
                {
                    if (countedFinish != Unset && countedFinish >= foundFinish)
                    {
                        trailerBytes = buffer[position..schemaDefinitionEnd];
                        Status = ReadStatus.TrailingBytes;
                    }
                    else if (position == foundFinish)
                    {
                        found.SetTrailer(buffer[position..schemaDefinitionEnd]);
                        payloadFinish = schemaDefinitionEnd;
                    }
                }
                position = schemaDefinitionEnd;
                countedFinish = Unset;
                foundFinish = Unset;
                const int SymbolsCountWidth = 4;
                if (!Take(SymbolsCountWidth, metadataEnd)) return false;
                {
                    if (metadataEnd < position + symbolsCountDependencyDistanceFromSymbol + symbolsCountDependencyReadWidth) return Stop(ReadStatus.Truncated);
                    symbolsCountValue = BinaryPrimitives.ReadUInt32LittleEndian(buffer.Slice(position + symbolsCountDependencyDistanceFromSymbol, symbolsCountDependencyReadWidth));
                    SymbolCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(symbolsCountValue)));
                    if (SymbolCountRule0Entries > ulong.MaxValue - declaredCount) return Stop(ReadStatus.InvalidLength);
                    declaredCount += (ulong)SymbolCountRule0Entries;
                }
                goto case SymbolOccurrence;
            }
            case TsOutTimePresentReached:
            {
                if (position < recordEnd)
                {
                    if (countedFinish != Unset && countedFinish >= foundFinish)
                    {
                        trailerBytes = buffer[position..recordEnd];
                        Status = ReadStatus.TrailingBytes;
                    }
                    else if (position == foundFinish)
                    {
                        found.SetTrailer(buffer[position..recordEnd]);
                        payloadFinish = recordEnd;
                    }
                }
                position = recordEnd;
                countedFinish = Unset;
                foundFinish = Unset;
                if (position <= recordEntryStart || position > RecordPayloadRule0End) return Stop(ReadStatus.InvalidLength);
                Current = new FileFramedMessage(foundDataValue, buffer[frameStart..foundStart], buffer[foundStart..payloadFinish], position - frameStart, found);
                state = RecordOccurrence;
                return true;
            }
            case RecordBodyReached:
            {
                {
                    const byte TsOutStoreAction0Default0 = 0;
                    const byte TsOutTimeOptionalRule0Data2 = 1;
                    TsOutTimeOptionalRule0Present = ((tsOutStoredValue ?? TsOutStoreAction0Default0) == TsOutTimeOptionalRule0Data2);
                    if (TsOutTimeOptionalRule0Present)
                    {
                        tsOutTimeEntryStart = position;
                        const int TsOutTimeWidth = 8;
                        if (!Take(TsOutTimeWidth, recordEnd)) return false;
                        recordFrame!.SetTsOutTime(buffer[tsOutTimeEntryStart..position]);
                        goto case TsOutTimePresentReached;
                    }
                }
                goto case TsOutTimePresentReached;
            }
            case RecordOccurrence:
            {
                if (!(position < RecordPayloadRule0End))
                {
                    {
                        if (position != RecordPayloadRule0End) return Stop(ReadStatus.InvalidLength);
                        position = RecordPayloadRule0End;
                        countedFinish = Unset;
                        foundFinish = Unset;
                        position = buffer.Length;
                        if (state == Start)
                        {
                            Current = new FileFramedMessage(default, buffer[0..position], buffer[position..position], position - 0, null);
                            state = Ended;
                            return true;
                        }
                        state = Ended;
                        return false;
                    }
                }
                recordEntryStart = position;
                {
                    recordFrame = new RecordFrame();
                    target.RecordFrames.Add(recordFrame);
                    const long RecordReassembleRule1Data2 = 4;
                    try
                    {
                        if (RecordPayloadRule0End < position + recordLengthDependencyDistanceFromRecord + recordLengthDependencyReadWidth) return Stop(ReadStatus.Truncated);
                        recordLengthValue = buffer.Slice(position + recordLengthDependencyDistanceFromRecord, recordLengthDependencyReadWidth)[0];
                        RecordReassembleRule1Width = checked((checked((global::System.Int128)(recordLengthValue)) * ((global::System.Int128)(RecordReassembleRule1Data2))));
                    }
                    catch (global::System.OverflowException)
                    {
                        return Stop(ReadStatus.InvalidLength);
                    }
                    if (RecordReassembleRule1Width < RuleEmptyWidth) return Stop(ReadStatus.InvalidLength);
                    if (RecordReassembleRule1Width > (long)RecordPayloadRule0End - position) return Stop(ReadStatus.Truncated);
                    RecordReassembleRule1End = position + (int)RecordReassembleRule1Width;
                    const long RecordSizeRule2Data2 = 4;
                    try
                    {
                        RecordSizeRule2Width = checked((checked((global::System.Int128)(recordLengthValue)) * ((global::System.Int128)(RecordSizeRule2Data2))));
                    }
                    catch (global::System.OverflowException)
                    {
                        return Stop(ReadStatus.InvalidLength);
                    }
                    if (RecordSizeRule2Width < RuleEmptyWidth) return Stop(ReadStatus.InvalidLength);
                    if (RecordSizeRule2Width > (long)RecordPayloadRule0End - position) return Stop(ReadStatus.Truncated);
                    RecordSizeRule2End = position + (int)RecordSizeRule2Width;
                    recordEnd = RecordReassembleRule1End;
                    if (RecordSizeRule2End != recordEnd) return Stop(ReadStatus.InvalidLength);
                    recordHeaderEntryStart = position;
                    const int RecordLengthWidth = 1;
                    if (!Take(RecordLengthWidth, recordEnd)) return false;
                    const int RtypeWidth = 1;
                    if (!Take(RtypeWidth, recordEnd)) return false;
                    const int PublisherIdWidth = 2;
                    if (!Take(PublisherIdWidth, recordEnd)) return false;
                    const int InstrumentIdWidth = 4;
                    if (!Take(InstrumentIdWidth, recordEnd)) return false;
                    const int TsEventWidth = 8;
                    if (!Take(TsEventWidth, recordEnd)) return false;
                    {
                        var recordHeaderParsed = new RecordHeader();
                        var recordHeaderOffset = recordHeaderEntryStart;
                        recordHeaderParsed.ParseFrom(buffer[..recordEnd], ref recordHeaderOffset);
                        recordFrame!.SetRecordHeader(recordHeaderParsed);
                    }
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
                        recordBodyCase = RecordBodyNoCase;
                        {
                            if (recordEnd < position + rtypeDependencyDistanceFromRecordBody + rtypeDependencyReadWidth) return Stop(ReadStatus.Truncated);
                            rtypeValue = (byte)(Rtype)buffer.Slice(position + rtypeDependencyDistanceFromRecordBody, rtypeDependencyReadWidth)[0];
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
                        if (recordBodyCase == RecordBodyBranchRule0Case)
                        {
                            var parsed = new TradeMsg();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..recordEnd], ref position);
                            found = parsed;
                        }
                        else if (recordBodyCase == RecordBodyBranchRule1Case)
                        {
                            var parsed = new Mbp1Msg();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..recordEnd], ref position);
                            found = parsed;
                        }
                        else if (recordBodyCase == RecordBodyBranchRule2Case)
                        {
                            var parsed = new Mbp10Msg();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..recordEnd], ref position);
                            found = parsed;
                        }
                        else if (recordBodyCase == RecordBodyBranchRule3Case)
                        {
                            if (recordEnd < position + rtypeDependencyDistanceFromOhlcvMsg + rtypeDependencyReadWidth) return Stop(ReadStatus.Truncated);
                            var code = (RecordBodyCode)buffer.Slice(position + rtypeDependencyDistanceFromOhlcvMsg, rtypeDependencyReadWidth)[0];
                            var parsed = new OhlcvMsg().SetDataValue(code);
                            foundStart = position;
                            parsed.ParseFrom(buffer[..recordEnd], ref position);
                            found = parsed;
                        }
                        else if (recordBodyCase == RecordBodyBranchRule4Case)
                        {
                            var parsed = new StatusMsg();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..recordEnd], ref position);
                            found = parsed;
                        }
                        else if (recordBodyCase == RecordBodyBranchRule5Case)
                        {
                            var parsed = new InstrumentDefMsg();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..recordEnd], ref position);
                            found = parsed;
                        }
                        else if (recordBodyCase == RecordBodyBranchRule6Case)
                        {
                            var parsed = new ImbalanceMsg();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..recordEnd], ref position);
                            found = parsed;
                        }
                        else if (recordBodyCase == RecordBodyBranchRule7Case)
                        {
                            var parsed = new ErrorMsg();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..recordEnd], ref position);
                            found = parsed;
                        }
                        else if (recordBodyCase == RecordBodyBranchRule8Case)
                        {
                            var parsed = new SymbolMappingMsg();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..recordEnd], ref position);
                            found = parsed;
                        }
                        else if (recordBodyCase == RecordBodyBranchRule9Case)
                        {
                            var parsed = new SystemMsg();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..recordEnd], ref position);
                            found = parsed;
                        }
                        else if (recordBodyCase == RecordBodyBranchRule10Case)
                        {
                            var parsed = new StatMsg();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..recordEnd], ref position);
                            found = parsed;
                        }
                        else if (recordBodyCase == RecordBodyBranchRule11Case)
                        {
                            if (recordEnd < position + rtypeDependencyDistanceFromOhlcvMsg + rtypeDependencyReadWidth) return Stop(ReadStatus.Truncated);
                            var code = (RecordBodyCode)buffer.Slice(position + rtypeDependencyDistanceFromOhlcvMsg, rtypeDependencyReadWidth)[0];
                            var parsed = new OhlcvMsg().SetDataValue(code);
                            foundStart = position;
                            parsed.ParseFrom(buffer[..recordEnd], ref position);
                            found = parsed;
                        }
                        else if (recordBodyCase == RecordBodyBranchRule12Case)
                        {
                            if (recordEnd < position + rtypeDependencyDistanceFromOhlcvMsg + rtypeDependencyReadWidth) return Stop(ReadStatus.Truncated);
                            var code = (RecordBodyCode)buffer.Slice(position + rtypeDependencyDistanceFromOhlcvMsg, rtypeDependencyReadWidth)[0];
                            var parsed = new OhlcvMsg().SetDataValue(code);
                            foundStart = position;
                            parsed.ParseFrom(buffer[..recordEnd], ref position);
                            found = parsed;
                        }
                        else if (recordBodyCase == RecordBodyBranchRule13Case)
                        {
                            if (recordEnd < position + rtypeDependencyDistanceFromOhlcvMsg + rtypeDependencyReadWidth) return Stop(ReadStatus.Truncated);
                            var code = (RecordBodyCode)buffer.Slice(position + rtypeDependencyDistanceFromOhlcvMsg, rtypeDependencyReadWidth)[0];
                            var parsed = new OhlcvMsg().SetDataValue(code);
                            foundStart = position;
                            parsed.ParseFrom(buffer[..recordEnd], ref position);
                            found = parsed;
                        }
                        else if (recordBodyCase == RecordBodyBranchRule14Case)
                        {
                            if (recordEnd < position + rtypeDependencyDistanceFromOhlcvMsg + rtypeDependencyReadWidth) return Stop(ReadStatus.Truncated);
                            var code = (RecordBodyCode)buffer.Slice(position + rtypeDependencyDistanceFromOhlcvMsg, rtypeDependencyReadWidth)[0];
                            var parsed = new OhlcvMsg().SetDataValue(code);
                            foundStart = position;
                            parsed.ParseFrom(buffer[..recordEnd], ref position);
                            found = parsed;
                        }
                        else if (recordBodyCase == RecordBodyBranchRule15Case)
                        {
                            if (recordEnd < position + rtypeDependencyDistanceFromOhlcvMsg + rtypeDependencyReadWidth) return Stop(ReadStatus.Truncated);
                            var code = (RecordBodyCode)buffer.Slice(position + rtypeDependencyDistanceFromOhlcvMsg, rtypeDependencyReadWidth)[0];
                            var parsed = new OhlcvMsg().SetDataValue(code);
                            foundStart = position;
                            parsed.ParseFrom(buffer[..recordEnd], ref position);
                            found = parsed;
                        }
                        else if (recordBodyCase == RecordBodyBranchRule16Case)
                        {
                            var parsed = new MboMsg();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..recordEnd], ref position);
                            found = parsed;
                        }
                        else if (recordBodyCase == RecordBodyBranchRule17Case)
                        {
                            if (recordEnd < position + rtypeDependencyDistanceFromCmbp1Msg + rtypeDependencyReadWidth) return Stop(ReadStatus.Truncated);
                            var code = (RecordBodyCode)buffer.Slice(position + rtypeDependencyDistanceFromCmbp1Msg, rtypeDependencyReadWidth)[0];
                            var parsed = new Cmbp1Msg().SetDataValue(code);
                            foundStart = position;
                            parsed.ParseFrom(buffer[..recordEnd], ref position);
                            found = parsed;
                        }
                        else if (recordBodyCase == RecordBodyBranchRule18Case)
                        {
                            if (recordEnd < position + rtypeDependencyDistanceFromCbboMsg + rtypeDependencyReadWidth) return Stop(ReadStatus.Truncated);
                            var code = (RecordBodyCode)buffer.Slice(position + rtypeDependencyDistanceFromCbboMsg, rtypeDependencyReadWidth)[0];
                            var parsed = new CbboMsg().SetDataValue(code);
                            foundStart = position;
                            parsed.ParseFrom(buffer[..recordEnd], ref position);
                            found = parsed;
                        }
                        else if (recordBodyCase == RecordBodyBranchRule19Case)
                        {
                            if (recordEnd < position + rtypeDependencyDistanceFromCbboMsg + rtypeDependencyReadWidth) return Stop(ReadStatus.Truncated);
                            var code = (RecordBodyCode)buffer.Slice(position + rtypeDependencyDistanceFromCbboMsg, rtypeDependencyReadWidth)[0];
                            var parsed = new CbboMsg().SetDataValue(code);
                            foundStart = position;
                            parsed.ParseFrom(buffer[..recordEnd], ref position);
                            found = parsed;
                        }
                        else if (recordBodyCase == RecordBodyBranchRule20Case)
                        {
                            if (recordEnd < position + rtypeDependencyDistanceFromCmbp1Msg + rtypeDependencyReadWidth) return Stop(ReadStatus.Truncated);
                            var code = (RecordBodyCode)buffer.Slice(position + rtypeDependencyDistanceFromCmbp1Msg, rtypeDependencyReadWidth)[0];
                            var parsed = new Cmbp1Msg().SetDataValue(code);
                            foundStart = position;
                            parsed.ParseFrom(buffer[..recordEnd], ref position);
                            found = parsed;
                        }
                        else if (recordBodyCase == RecordBodyBranchRule21Case)
                        {
                            if (recordEnd < position + rtypeDependencyDistanceFromBboMsg + rtypeDependencyReadWidth) return Stop(ReadStatus.Truncated);
                            var code = (RecordBodyCode)buffer.Slice(position + rtypeDependencyDistanceFromBboMsg, rtypeDependencyReadWidth)[0];
                            var parsed = new BboMsg().SetDataValue(code);
                            foundStart = position;
                            parsed.ParseFrom(buffer[..recordEnd], ref position);
                            found = parsed;
                        }
                        else if (recordBodyCase == RecordBodyBranchRule22Case)
                        {
                            if (recordEnd < position + rtypeDependencyDistanceFromBboMsg + rtypeDependencyReadWidth) return Stop(ReadStatus.Truncated);
                            var code = (RecordBodyCode)buffer.Slice(position + rtypeDependencyDistanceFromBboMsg, rtypeDependencyReadWidth)[0];
                            var parsed = new BboMsg().SetDataValue(code);
                            foundStart = position;
                            parsed.ParseFrom(buffer[..recordEnd], ref position);
                            found = parsed;
                        }
                        else
                        {
                            {
                                if (recordEnd < position + rtypeDependencyDistanceFromRecordBody + rtypeDependencyReadWidth) return Stop(ReadStatus.Truncated);
                                var code = (RecordBodyCode)buffer.Slice(position + rtypeDependencyDistanceFromRecordBody, rtypeDependencyReadWidth)[0];
                                foundStart = position;
                                found = UnknownFileMessage.Parse(buffer[..recordEnd], ref position).SetDataValue(code);
                            }
                        }
                        foundDataValue = ((IFileMessage)found).DataValue;
                        frameStart = recordEntryStart;
                        foundFinish = position;
                        payloadFinish = position;
                        if (found.Status != ParseStatus.Ok)
                        {
                            Current = new FileFramedMessage(foundDataValue, buffer[frameStart..foundStart], buffer[foundStart..payloadFinish], position - frameStart, found);
                            state = Ended;
                            return true;
                        }
                        recordFrame!.recordBody = (IFileMessage)found;
                        goto case RecordBodyReached;
                    }
                }
            }
            case RecordFrame:
            {
                {
                    RecordPayloadRule0RawEnd = checked((global::System.Int128)(buffer.Length));
                    if (RecordPayloadRule0RawEnd < position || RecordPayloadRule0RawEnd > buffer.Length) return Stop(ReadStatus.Truncated);
                    RecordPayloadRule0End = (int)RecordPayloadRule0RawEnd;
                }
                goto case RecordOccurrence;
            }
            case MappingIntervalOccurrence:
            {
                if (!(MappingIntervalCountRule0Entries > RuleCountNone))
                {
                    {
                        if (MappingIntervalCountRule0Entries > RuleCountNone) return Stop(ReadStatus.Truncated);
                        countedFinish = position;
                        SymbolMappingCountRule0Entries--;
                        goto case SymbolMappingOccurrence;
                    }
                }
                mappingIntervalEntryStart = position;
                {
                    const int StartDateWidth = 4;
                    if (!Take(StartDateWidth, metadataEnd)) return false;
                    const int EndDateWidth = 4;
                    if (!Take(EndDateWidth, metadataEnd)) return false;
                    const int MappedSymbolWidth = 22;
                    if (!Take(MappedSymbolWidth, metadataEnd)) return false;
                    MappingIntervalCountRule0Entries--;
                    goto case MappingIntervalOccurrence;
                }
            }
            case SymbolMappingOccurrence:
            {
                if (!(SymbolMappingCountRule0Entries > RuleCountNone))
                {
                    {
                        if (SymbolMappingCountRule0Entries > RuleCountNone) return Stop(ReadStatus.Truncated);
                        countedFinish = position;
                        if (position < metadataEnd)
                        {
                            if (countedFinish != Unset && countedFinish >= foundFinish)
                            {
                                trailerBytes = buffer[position..metadataEnd];
                                Status = ReadStatus.TrailingBytes;
                            }
                            else if (position == foundFinish)
                            {
                                found.SetTrailer(buffer[position..metadataEnd]);
                                payloadFinish = metadataEnd;
                            }
                        }
                        position = metadataEnd;
                        countedFinish = Unset;
                        foundFinish = Unset;
                        {
                            var metadataParsed = new Metadata();
                            var metadataOffset = metadataEntryStart;
                            metadataParsed.ParseFrom(buffer[..buffer.Length], ref metadataOffset);
                            target.SetMetadata(metadataParsed);
                        }
                        goto case RecordFrame;
                    }
                }
                symbolMappingEntryStart = position;
                {
                    const int RawSymbolInWidth = 22;
                    if (!Take(RawSymbolInWidth, metadataEnd)) return false;
                    const int IntervalCountWidth = 4;
                    if (!Take(IntervalCountWidth, metadataEnd)) return false;
                    {
                        if (metadataEnd < position + intervalCountDependencyDistanceFromMappingInterval + intervalCountDependencyReadWidth) return Stop(ReadStatus.Truncated);
                        intervalCountValue = BinaryPrimitives.ReadUInt32LittleEndian(buffer.Slice(position + intervalCountDependencyDistanceFromMappingInterval, intervalCountDependencyReadWidth));
                        MappingIntervalCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(intervalCountValue)));
                        if (MappingIntervalCountRule0Entries > ulong.MaxValue - declaredCount) return Stop(ReadStatus.InvalidLength);
                        declaredCount += (ulong)MappingIntervalCountRule0Entries;
                    }
                    goto case MappingIntervalOccurrence;
                }
            }
            case NotFoundSymbolOccurrence:
            {
                if (!(NotFoundSymbolCountRule0Entries > RuleCountNone))
                {
                    {
                        if (NotFoundSymbolCountRule0Entries > RuleCountNone) return Stop(ReadStatus.Truncated);
                        countedFinish = position;
                        const int MappingsCountWidth = 4;
                        if (!Take(MappingsCountWidth, metadataEnd)) return false;
                        {
                            if (metadataEnd < position + mappingsCountDependencyDistanceFromSymbolMapping + mappingsCountDependencyReadWidth) return Stop(ReadStatus.Truncated);
                            mappingsCountValue = BinaryPrimitives.ReadUInt32LittleEndian(buffer.Slice(position + mappingsCountDependencyDistanceFromSymbolMapping, mappingsCountDependencyReadWidth));
                            SymbolMappingCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(mappingsCountValue)));
                            if (SymbolMappingCountRule0Entries > ulong.MaxValue - declaredCount) return Stop(ReadStatus.InvalidLength);
                            declaredCount += (ulong)SymbolMappingCountRule0Entries;
                        }
                        goto case SymbolMappingOccurrence;
                    }
                }
                notFoundSymbolEntryStart = position;
                {
                    const int NotFoundSymbolWidth = 22;
                    if (!Take(NotFoundSymbolWidth, metadataEnd)) return false;
                    NotFoundSymbolCountRule0Entries--;
                    goto case NotFoundSymbolOccurrence;
                }
            }
            case PartialSymbolOccurrence:
            {
                if (!(PartialSymbolCountRule0Entries > RuleCountNone))
                {
                    {
                        if (PartialSymbolCountRule0Entries > RuleCountNone) return Stop(ReadStatus.Truncated);
                        countedFinish = position;
                        const int NotFoundCountWidth = 4;
                        if (!Take(NotFoundCountWidth, metadataEnd)) return false;
                        {
                            if (metadataEnd < position + notFoundCountDependencyDistanceFromNotFoundSymbol + notFoundCountDependencyReadWidth) return Stop(ReadStatus.Truncated);
                            notFoundCountValue = BinaryPrimitives.ReadUInt32LittleEndian(buffer.Slice(position + notFoundCountDependencyDistanceFromNotFoundSymbol, notFoundCountDependencyReadWidth));
                            NotFoundSymbolCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(notFoundCountValue)));
                            if (NotFoundSymbolCountRule0Entries > ulong.MaxValue - declaredCount) return Stop(ReadStatus.InvalidLength);
                            declaredCount += (ulong)NotFoundSymbolCountRule0Entries;
                        }
                        goto case NotFoundSymbolOccurrence;
                    }
                }
                partialSymbolEntryStart = position;
                {
                    const int PartialSymbolWidth = 22;
                    if (!Take(PartialSymbolWidth, metadataEnd)) return false;
                    PartialSymbolCountRule0Entries--;
                    goto case PartialSymbolOccurrence;
                }
            }
            case SymbolOccurrence:
            {
                if (!(SymbolCountRule0Entries > RuleCountNone))
                {
                    {
                        if (SymbolCountRule0Entries > RuleCountNone) return Stop(ReadStatus.Truncated);
                        countedFinish = position;
                        const int PartialCountWidth = 4;
                        if (!Take(PartialCountWidth, metadataEnd)) return false;
                        {
                            if (metadataEnd < position + partialCountDependencyDistanceFromPartialSymbol + partialCountDependencyReadWidth) return Stop(ReadStatus.Truncated);
                            partialCountValue = BinaryPrimitives.ReadUInt32LittleEndian(buffer.Slice(position + partialCountDependencyDistanceFromPartialSymbol, partialCountDependencyReadWidth));
                            PartialSymbolCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(partialCountValue)));
                            if (PartialSymbolCountRule0Entries > ulong.MaxValue - declaredCount) return Stop(ReadStatus.InvalidLength);
                            declaredCount += (ulong)PartialSymbolCountRule0Entries;
                        }
                        goto case PartialSymbolOccurrence;
                    }
                }
                symbolEntryStart = position;
                {
                    const int SymbolWidth = 22;
                    if (!Take(SymbolWidth, metadataEnd)) return false;
                    SymbolCountRule0Entries--;
                    goto case SymbolOccurrence;
                }
            }
            default:
            {
                return false;
            }
        }
    }

    private bool Stop(ReadStatus status) { Status = status; state = Ended; return false; }

    private bool Take(int width, int end)
    {
        if (position + width > end) return Stop(ReadStatus.Truncated);
        position += width;
        return true;
    }
}
