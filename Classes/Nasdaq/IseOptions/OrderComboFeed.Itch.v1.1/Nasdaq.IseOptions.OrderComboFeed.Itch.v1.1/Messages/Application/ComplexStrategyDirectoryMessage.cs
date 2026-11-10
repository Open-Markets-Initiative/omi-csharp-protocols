using System.Buffers.Binary;
using System.Diagnostics;
using static Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Rules;

namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

/// <summary>
///  Whenever a complex order is added in the system for an underlying, the order is normalized and results in either the creation of a new complex strategy or is added to an existing strategy. A Complex Strategy Message containing the strategy definition will be sent.
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class ComplexStrategyDirectoryMessage : IPacketMessage, IEquatable<ComplexStrategyDirectoryMessage>, IFieldOwner
{
    internal const int TimestampOffset = 0;

    internal const int TimestampLength = 6;

    /// <summary>
    ///  The number of nanoseconds in one unit of the wire value of <see cref="Timestamp"/>.
    /// </summary>
    public const long TimestampNanosecondsPerUnit = 1;

    /// <summary>
    ///  The wire value of <see cref="Timestamp"/> for a time of one full day.
    /// </summary>
    public const long TimestampUnitsPerDay = TimeSpan.TicksPerDay * TimeSpan.NanosecondsPerTick / TimestampNanosecondsPerUnit;

    /// <summary>
    ///  The largest wire value that <see cref="Timestamp"/> can hold.
    /// </summary>
    public const ulong TimestampWireMaximum = (1UL << 48) - 1UL;

    internal const int StrategyIdOffset = TimestampOffset + TimestampLength;

    internal const int StrategyIdLength = 4;

    internal const int StrategyTypeOffset = StrategyIdOffset + StrategyIdLength;

    internal const int StrategyTypeLength = 1;

    internal const int SourceOffset = StrategyTypeOffset + StrategyTypeLength;

    internal const int SourceLength = 1;

    internal const int UnderlyingSymbolOffset = SourceOffset + SourceLength;

    internal const int UnderlyingSymbolLength = 13;

    /// <summary>
    ///  The byte that fills the unused bytes of Underlying Symbol.
    /// </summary>
    public const byte UnderlyingSymbolFill = (byte)' ';

    internal const int NumberOfLegsOffset = UnderlyingSymbolOffset + UnderlyingSymbolLength;

    internal const int NumberOfLegsLength = 1;

    internal const int LegInformationListOffset = NumberOfLegsOffset + NumberOfLegsLength;

    /// <summary>
    ///  The encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public const int MessageByteLength = TimestampLength + StrategyIdLength + StrategyTypeLength + SourceLength + UnderlyingSymbolLength + NumberOfLegsLength;

    private const int LegInformationListEntryByteLength = LegInformation.MessageByteLength;

    internal ulong timestamp;

    /// <summary>
    ///  The time, expressed as the number of nanoseconds after midnight.
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds a time outside one day. This property has a precision of 100 nanoseconds, and <see cref="TimestampRaw"/> holds the exact value. A time is rounded to the wire unit of the field, with a midpoint away from zero. A time past the largest wire value is set to that value.
    /// </remarks>
    public TimeOnly? Timestamp
    {
        get => Midnight.ToTimeOnly(this.timestamp, TimestampNanosecondsPerUnit, TimestampUnitsPerDay);
        set
        {
            this.timestamp = value is { } present ? Midnight.ToRaw<ulong>(present, TimestampNanosecondsPerUnit, TimestampWireMaximum) : default;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="Timestamp"/>, as the unscaled integer that the field holds.
    /// </summary>
    public ulong TimestampRaw
    {
        get => this.timestamp;
        set
        {
            this.timestamp = value;
            Touch();
        }
    }

    internal uint strategyId;

    /// <summary>
    ///  ISE’s Strategy ID assigned daily, valid while there are any open complex orders for the day
    /// </summary>
    public uint StrategyId
    {
        get => this.strategyId;
        set
        {
            this.strategyId = value;
            Touch();
        }
    }

    internal StrategyType strategyType;

    /// <summary>
    ///  StrategyType value
    /// </summary>
    public StrategyType StrategyType
    {
        get => this.strategyType;
        set
        {
            this.strategyType = value;
            Touch();
        }
    }

    internal byte source;

    /// <summary>
    ///  Identifies the source of the Strategy, valid for the trading day
    /// </summary>
    public byte Source
    {
        get => this.source;
        set
        {
            this.source = value;
            Touch();
        }
    }

    internal string underlyingSymbol = string.Empty;

    /// <summary>
    ///  Underlying Symbol for the strategy. All legs in this strategy belong to this Underlying
    /// </summary>
    /// <remarks>
    ///  The field is 13 bytes long. The field holds ASCII text. Text that is shorter than the field is padded with spaces at the end. Text longer than the field is cut to fit when the message is encoded. A character that the field cannot hold is replaced with a question mark when the message is encoded.
    /// </remarks>
    public string UnderlyingSymbol
    {
        get => this.underlyingSymbol;
        set
        {
            this.underlyingSymbol = value ?? string.Empty;
            Touch();
        }
    }

    internal byte numberOfLegs;

    /// <summary>
    ///  Number of legs in the strategy NOTE: Leg field offsets below are an equation, where “n” is the zero based leg number (0, 1, …)
    /// </summary>
    public byte NumberOfLegs
    {
        get => this.numberOfLegs;
        set
        {
            this.numberOfLegs = value;
            Touch();
        }
    }

    internal LegInformationList legInformationList = new([]);

    /// <summary>
    ///  Leg information
    /// </summary>
    /// <remarks>
    ///  Setting <see langword="null"/> stores the value that Clear stores.
    /// </remarks>
    public LegInformationList LegInformationList
    {
        get => this.legInformationList;
        set
        {
            value ??= new([]);
            this.legInformationList.SetOwner(null);
            this.legInformationList = new LegInformationList(value);
            this.legInformationList.SetOwner(this);
            Touch();
        }
    }

    /// <summary>
    ///  Gets the message type.
    /// </summary>
    public PacketMessageCode DataValue => PacketMessageCode.ComplexStrategyDirectoryMessage;

    /// <summary>
    ///  Gets the encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public int ByteLength => MessageByteLength;

    /// <summary>
    ///  Gets the number of bytes that Encode writes, including variable-length content and each omittable field that is present.
    /// </summary>
    public int EncodedByteLength
    {
        get
        {
            var position = 0;
            position += TimestampLength;
            position += StrategyIdLength;
            position += StrategyTypeLength;
            position += SourceLength;
            position += UnderlyingSymbolLength;
            position += NumberOfLegsLength;
            position += this.legInformationList.Count * LegInformationListEntryByteLength;
            return position;
        }
    }

    /// <summary>
    ///  Gets the result of the most recent parse.
    /// </summary>
    public ParseStatus Status => this.status;

    /// <summary>
    ///  Gets the field where the most recent parse stopped, or <see langword="null"/> if it did not stop at a field.
    /// </summary>
    public FieldView? FailedField => this.failed is { } entry ? new FieldView(this, (int)entry) : null;

    /// <summary>
    ///  Gets the byte offset, relative to the start of the parsed bytes, where the most recent parse stopped.
    /// </summary>
    public int FailedOffset => this.failedOffset;

    /// <summary>
    ///  Gets a value indicating whether every message type and every enumerated value is one that the protocol defines.
    /// </summary>
    public bool IsRecognized => global::System.Enum.IsDefined(this.strategyType) && this.legInformationList.IsRecognized;

    /// <summary>
    ///  Gets the bytes that were parsed, or no bytes if the instance was not parsed.
    /// </summary>
    public ReadOnlyMemory<byte> Raw => this.raw;

    /// <summary>
    ///  Gets a value indicating whether any value has changed since the most recent parse or clear.
    /// </summary>
    public bool IsModified => this.modified;

    private ParseStatus status;
    private FieldName? failed;
    private int failedOffset;
    private byte[] raw = [];
    private byte[]? cache;
    private bool modified;
    private IFieldOwner? owner;
    private int legInformationListLength;

    private void Touch()
    {
        this.modified = true;
        this.cache = null;
        this.owner?.Touch();
    }

    IMessage IMessage.Copy() => new ComplexStrategyDirectoryMessage(this);

    void IMessage.SetOwner(IFieldOwner? owner) => this.owner = owner;

    int IMessage.EncodedByteLength => EncodedByteLength;

    private byte[] trailer = [];

    /// <summary>
    ///  Gets or sets any extra bytes that follow the message within its declared length.
    /// </summary>
    /// <remarks>
    ///  These bytes are written only when the message is encoded in a packet that declares each message length. Setting this property marks the message as modified.
    /// </remarks>
    public ReadOnlyMemory<byte> Trailer
    {
        get => this.trailer;
        set
        {
            this.trailer = value.ToArray();
            Touch();
        }
    }

    void IMessage.SetTrailer(ReadOnlySpan<byte> bytes) => this.trailer = bytes.ToArray();

    private const int DataValueLength = 1;

    /// <summary>
    ///  Identifies each field, in wire order.
    /// </summary>
    public enum FieldName
    {
        /// <summary>
        ///  The message type.
        /// </summary>
        DataValue,
        /// <summary>
        ///  The time, expressed as the number of nanoseconds after midnight.
        /// </summary>
        Timestamp,
        /// <summary>
        ///  ISE’s Strategy ID assigned daily, valid while there are any open complex orders for the day
        /// </summary>
        StrategyId,
        /// <summary>
        ///  StrategyType value
        /// </summary>
        StrategyType,
        /// <summary>
        ///  Identifies the source of the Strategy, valid for the trading day
        /// </summary>
        Source,
        /// <summary>
        ///  Underlying Symbol for the strategy. All legs in this strategy belong to this Underlying
        /// </summary>
        UnderlyingSymbol,
        /// <summary>
        ///  Number of legs in the strategy NOTE: Leg field offsets below are an equation, where “n” is the zero based leg number (0, 1, …)
        /// </summary>
        NumberOfLegs,
        /// <summary>
        ///  Leg information
        /// </summary>
        LegInformationList,
    }

    /// <summary>
    ///  Provides access to the fields of one instance, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public readonly struct FieldSet : IEnumerable<FieldView>
    {
        private readonly ComplexStrategyDirectoryMessage owner;

        internal FieldSet(ComplexStrategyDirectoryMessage owner)
        {
            this.owner = owner;
        }

        /// <summary>
        ///  The message type.
        /// </summary>
        public FieldView DataValue => this[FieldName.DataValue];

        /// <summary>
        ///  The time, expressed as the number of nanoseconds after midnight.
        /// </summary>
        public FieldView Timestamp => this[FieldName.Timestamp];

        /// <summary>
        ///  ISE’s Strategy ID assigned daily, valid while there are any open complex orders for the day
        /// </summary>
        public FieldView StrategyId => this[FieldName.StrategyId];

        /// <summary>
        ///  StrategyType value
        /// </summary>
        public FieldView StrategyType => this[FieldName.StrategyType];

        /// <summary>
        ///  Identifies the source of the Strategy, valid for the trading day
        /// </summary>
        public FieldView Source => this[FieldName.Source];

        /// <summary>
        ///  Underlying Symbol for the strategy. All legs in this strategy belong to this Underlying
        /// </summary>
        public FieldView UnderlyingSymbol => this[FieldName.UnderlyingSymbol];

        /// <summary>
        ///  Number of legs in the strategy NOTE: Leg field offsets below are an equation, where “n” is the zero based leg number (0, 1, …)
        /// </summary>
        public FieldView NumberOfLegs => this[FieldName.NumberOfLegs];

        /// <summary>
        ///  Leg information
        /// </summary>
        public FieldView LegInformationList => this[FieldName.LegInformationList];

        /// <summary>
        ///  Gets the specified field.
        /// </summary>
        public FieldView this[FieldName field] => new(this.owner, (int)field);

        /// <summary>
        ///  Returns an enumerator that iterates through the fields in wire order.
        /// </summary>
        public FieldEnumerator GetEnumerator() => new(this.owner);

        IEnumerator<FieldView> IEnumerable<FieldView>.GetEnumerator() => GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }

    private static readonly FieldDescriptor[] Descriptors =
    [
        new(nameof(FieldName.DataValue), FieldKind.Enum, DataValueLength),
        new(nameof(FieldName.Timestamp), FieldKind.UnsignedInteger, TimestampLength) { Unit = "Nanoseconds", Epoch = "Midnight", Timezone = "America/New_York", ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.StrategyId), FieldKind.UnsignedInteger, StrategyIdLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.StrategyType), FieldKind.Enum, StrategyTypeLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.Source), FieldKind.UnsignedInteger, SourceLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.UnderlyingSymbol), FieldKind.String, UnderlyingSymbolLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.NumberOfLegs), FieldKind.UnsignedInteger, NumberOfLegsLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.LegInformationList), FieldKind.Group, null),
    ];

    /// <summary>
    ///  Gets the fields in wire order, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public FieldSet Fields => new(this);

    IEnumerable<FieldView> IMessage.Fields => new FieldSet(this);

    /// <summary>
    ///  Initializes a new instance of the <see cref="ComplexStrategyDirectoryMessage"/> class with default values.
    /// </summary>
    public ComplexStrategyDirectoryMessage()
    {
        Clear();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="ComplexStrategyDirectoryMessage"/> class with the values, bytes, and parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public ComplexStrategyDirectoryMessage(ComplexStrategyDirectoryMessage other) : this()
    {
        CopyFrom(other);
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(ComplexStrategyDirectoryMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.timestamp = other.timestamp;
        this.strategyId = other.strategyId;
        this.strategyType = other.strategyType;
        this.source = other.source;
        this.underlyingSymbol = other.underlyingSymbol;
        this.numberOfLegs = other.numberOfLegs;
        this.legInformationList.SetOwner(null);
        this.legInformationList = new LegInformationList(other.legInformationList);
        this.legInformationList.SetOwner(this);
        this.legInformationListLength = other.legInformationListLength;
        this.trailer = other.trailer;
        this.status = other.status;
        this.failed = other.failed;
        this.failedOffset = other.failedOffset;
        this.raw = other.raw;
        this.cache = other.cache;
        this.modified = other.modified;
        this.owner?.Touch();
    }

    /// <summary>
    ///  Resets all values, the bytes, and the parse result to their defaults.
    /// </summary>
    /// <remarks>
    ///  A field with one fixed value is reset to that value.
    /// </remarks>
    public void Clear()
    {
        this.timestamp = default;
        this.strategyId = default;
        this.strategyType = default;
        this.source = default;
        this.underlyingSymbol = string.Empty;
        this.numberOfLegs = default;
        this.legInformationList.SetOwner(null);
        this.legInformationList = new([]);
        this.legInformationList.SetOwner(this);
        this.trailer = [];
        this.status = default;
        this.failed = null;
        this.failedOffset = default;
        this.raw = [];
        this.cache = null;
        this.modified = false;
        this.owner?.Touch();
    }

    /// <summary>
    ///  Parses a new instance from the start of the bytes.
    /// </summary>
    /// <returns>The new instance. Check Status to determine whether the parse succeeded.</returns>
    /// <remarks>
    ///  The bytes are those of the fields of the message, from its first field to its last.
    /// </remarks>
    public static ComplexStrategyDirectoryMessage Parse(ReadOnlySpan<byte> data)
    {
        var instance = new ComplexStrategyDirectoryMessage();
        instance.ParseFrom(data);
        return instance;
    }

    /// <summary>
    ///  Parses a new instance starting at the specified offset.
    /// </summary>
    /// <param name="data">The bytes to parse.</param>
    /// <param name="offset">The offset at which to start. A successful parse advances it past the parsed bytes, and a failed parse does not change it.</param>
    /// <returns>The new instance. Check Status to determine whether the parse succeeded.</returns>
    /// <remarks>
    ///  The bytes are those of the fields of the message, from its first field to its last.
    /// </remarks>
    public static ComplexStrategyDirectoryMessage Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        var instance = new ComplexStrategyDirectoryMessage();
        instance.ParseFrom(data, ref offset);
        return instance;
    }

    /// <summary>
    ///  Parses the bytes into this instance, starting at the first byte.
    /// </summary>
    /// <remarks>
    ///  The bytes are those of the fields of the message, from its first field to its last.
    /// </remarks>
    public void ParseFrom(ReadOnlySpan<byte> data)
    {
        var offset = 0;
        ParseFrom(data, ref offset);
    }

    /// <summary>
    ///  Parses the bytes into this instance, starting at the specified offset.
    /// </summary>
    /// <param name="data">The bytes to parse.</param>
    /// <param name="offset">The offset at which to start. A successful parse advances it past the parsed bytes, and a failed parse does not change it.</param>
    /// <remarks>
    ///  The bytes are those of the fields of the message, from its first field to its last.
    /// </remarks>
    public void ParseFrom(ReadOnlySpan<byte> data, ref int offset)
    {
        Clear();
        if ((uint)offset > (uint)data.Length)
        {
            this.status = ParseStatus.InvalidOffset;
            return;
        }
        var start = offset;
        var cursor = start;
        if (Truncated(data, start, cursor, TimestampLength, FieldName.Timestamp)) return;
        this.timestamp = Padded.ReadBigEndian<ulong>(data.Slice(cursor, TimestampLength));
        cursor += TimestampLength;
        if (Truncated(data, start, cursor, StrategyIdLength, FieldName.StrategyId)) return;
        this.strategyId = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, StrategyIdLength));
        cursor += StrategyIdLength;
        if (Truncated(data, start, cursor, StrategyTypeLength, FieldName.StrategyType)) return;
        this.strategyType = (StrategyType)data.Slice(cursor, StrategyTypeLength)[0];
        cursor += StrategyTypeLength;
        if (Truncated(data, start, cursor, SourceLength, FieldName.Source)) return;
        this.source = data.Slice(cursor, SourceLength)[0];
        cursor += SourceLength;
        if (Truncated(data, start, cursor, UnderlyingSymbolLength, FieldName.UnderlyingSymbol)) return;
        this.underlyingSymbol = TextEncoding.Ascii.ReadLeftJustified(data.Slice(cursor, UnderlyingSymbolLength), UnderlyingSymbolFill);
        cursor += UnderlyingSymbolLength;
        if (Truncated(data, start, cursor, NumberOfLegsLength, FieldName.NumberOfLegs)) return;
        this.numberOfLegs = data.Slice(cursor, NumberOfLegsLength)[0];
        cursor += NumberOfLegsLength;
        {
            var groupStart = cursor;
            this.legInformationListLength = data.Length - groupStart;
            global::System.UInt128 LegInformationCountRule0Entries;
            byte numberOfLegsValue = this.numberOfLegs;
            var entries = new List<LegInformation>();
            LegInformationCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(numberOfLegsValue)));
            while (LegInformationCountRule0Entries > RuleCountNone)
            {
                var entry = new LegInformation();
                var contract = (IGroupEntry)entry;
                contract.ParseFrom(data, ref cursor);
                if (contract.Status != ParseStatus.Ok)
                {
                    this.legInformationList = new LegInformationList(entries, contract.Status, entry);
                    this.legInformationList.SetOwner(this);
                    Fail(contract.Status, FieldName.LegInformationList, groupStart - start, data[start..]);
                    return;
                }
                entries.Add(entry);
                LegInformationCountRule0Entries--;
            }
            if (LegInformationCountRule0Entries > RuleCountNone) { Fail(ParseStatus.BadCount, FieldName.LegInformationList, groupStart - start, data[start..]); return; }
            this.legInformationList = new LegInformationList(entries, ParseStatus.Ok, null);
            this.legInformationList.SetOwner(this);
            this.legInformationListLength = cursor - groupStart;
        }
        this.raw = data[start..cursor].ToArray();
        this.status = ParseStatus.Ok;
        offset = cursor;
    }

    private void Fail(ParseStatus status, FieldName field, int offset, ReadOnlySpan<byte> available)
    {
        this.status = status;
        this.failed = field;
        this.failedOffset = offset;
        this.raw = available.ToArray();
    }

    private bool Truncated(ReadOnlySpan<byte> data, int start, int cursor, int length, FieldName field)
    {
        if (data.Length - cursor >= length) return false;
        Fail(ParseStatus.Truncated, field, cursor - start, data[start..]);
        return true;
    }

    /// <summary>
    ///  Encodes this instance into the span, starting at the specified offset.
    /// </summary>
    /// <param name="data">The span that receives the bytes.</param>
    /// <param name="offset">The offset in the span of the first byte to write.</param>
    /// <returns>The offset immediately after the last byte written.</returns>
    /// <exception cref="EncodeException">A value does not fit its field.</exception>
    /// <exception cref="ArgumentOutOfRangeException">The value of offset is negative or greater than the length of the span.</exception>
    /// <exception cref="ArgumentException">The span is too small for the encoded bytes.</exception>
    /// <remarks>
    ///  The bytes are those of the fields of the message, from its first field to its last.
    /// </remarks>
    public int Encode(Span<byte> data, int offset)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(offset);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, data.Length);
        if (data.Length - offset < EncodedByteLength)
        {
            throw new ArgumentException("The span is too small for the encoded bytes.", nameof(data));
        }
        if (!((IMessage)this).Write(data, offset, trailerFollows: false, out var cursor, out var failure))
        {
            throw new EncodeException(failure);
        }
        return cursor;
    }

    /// <summary>
    ///  Encodes this instance into the start of the span.
    /// </summary>
    /// <param name="data">The span that receives the bytes from its first byte.</param>
    /// <returns>The number of bytes written.</returns>
    /// <exception cref="EncodeException">A value does not fit its field.</exception>
    /// <exception cref="ArgumentException">The span is too small for the encoded bytes.</exception>
    /// <remarks>
    ///  The bytes are those of the fields of the message, from its first field to its last.
    /// </remarks>
    public int Encode(Span<byte> data) => Encode(data, 0);

    /// <summary>
    ///  Encodes this instance into a new array.
    /// </summary>
    /// <exception cref="EncodeException">A value does not fit its field.</exception>
    /// <remarks>
    ///  The bytes are those of the fields of the message, from its first field to its last.
    /// </remarks>
    public byte[] ToBytes()
    {
        var buffer = new byte[EncodedByteLength];
        Encode(buffer);
        return buffer;
    }

    /// <summary>
    ///  Tries to encode the message into the span.
    /// </summary>
    /// <param name="data">The span that receives the bytes from its first byte.</param>
    /// <param name="written">When this method returns, contains the number of bytes written, or 0 if encoding failed.</param>
    /// <param name="failure">When this method returns <see langword="false"/>, contains the first field whose value cannot be encoded and the reason. It contains <see cref="EncodeStatus.BufferTooSmall"/> if the span is shorter than the encoded bytes. For a field in a group entry or nested message, it also identifies the group or field that holds it.</param>
    /// <returns><see langword="true"/> if the message was written, or <see langword="false"/> if a value cannot be encoded or the span is too small.</returns>
    /// <remarks>
    ///  The bytes are those of the fields of the message, from its first field to its last. If a nested message has a trailer, the reason is <see cref="EncodeStatus.NestedTrailer"/>, and the field is the one holding the message.
    /// </remarks>
    public bool TryEncode(Span<byte> data, out int written, out EncodeFailure failure)
    {
        written = 0;
        if (data.Length < EncodedByteLength)
        {
            failure = new EncodeFailure(EncodeStatus.BufferTooSmall);
            return false;
        }
        if (!((IMessage)this).Write(data, 0, trailerFollows: false, out var cursor, out failure))
        {
            return false;
        }
        written = cursor;
        return true;
    }

    bool IMessage.Write(Span<byte> data, int offset, bool trailerFollows, out int cursor, out EncodeFailure failure)
    {
        cursor = offset;
        {
            global::System.Int128 measure = this.legInformationList.Count;
            global::System.Int128 numberOfLegsStampedValue;
            numberOfLegsStampedValue = measure;
            if (numberOfLegsStampedValue > byte.MaxValue)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.NumberOfLegs), EncodeStatus.TooLong);
                return false;
            }
            if (numberOfLegsStampedValue < byte.MinValue)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.NumberOfLegs), EncodeStatus.DoesNotFit);
                return false;
            }
            this.numberOfLegs = checked((byte)(numberOfLegsStampedValue));
        }
        if (this.timestamp > TimestampWireMaximum)
        {
            failure = new EncodeFailure(new FieldView(this, (int)FieldName.Timestamp), EncodeStatus.DoesNotFit);
            return false;
        }
        Padded.WriteBigEndian(data.Slice(cursor, TimestampLength), this.timestamp);
        cursor += TimestampLength;
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, StrategyIdLength), this.strategyId);
        cursor += StrategyIdLength;
        data.Slice(cursor, StrategyTypeLength)[0] = unchecked((byte)(this.strategyType));
        cursor += StrategyTypeLength;
        data.Slice(cursor, SourceLength)[0] = unchecked((byte)(this.source));
        cursor += SourceLength;
        TextEncoding.Ascii.WriteLeftJustified(data.Slice(cursor, UnderlyingSymbolLength), TextEncoding.Ascii.Fit(this.underlyingSymbol, UnderlyingSymbolLength), UnderlyingSymbolFill);
        cursor += UnderlyingSymbolLength;
        data.Slice(cursor, NumberOfLegsLength)[0] = unchecked((byte)(this.numberOfLegs));
        cursor += NumberOfLegsLength;
        {
            var groupStart = cursor;
            for (var index = 0; index < this.legInformationList.Count; index++)
            {
                if (!((IGroupEntry)this.legInformationList[index]).Write(data, cursor, out cursor, out var memberFailure))
                {
                    failure = memberFailure.Within(new FieldView(this, (int)FieldName.LegInformationList), index);
                    return false;
                }
            }
            this.legInformationListLength = cursor - groupStart;
        }
        failure = default;
        return true;
    }

    bool IFieldOwner.TryRaw(out ReadOnlyMemory<byte> raw)
    {
        if (this.status == ParseStatus.None || this.modified)
        {
            if (this.cache is null)
            {
                var buffer = new byte[EncodedByteLength];
                if (!((IMessage)this).Write(buffer, 0, trailerFollows: false, out _, out _))
                {
                    raw = ReadOnlyMemory<byte>.Empty;
                    return false;
                }
                this.cache = buffer;
            }
            raw = this.cache;
            return true;
        }
        raw = this.raw;
        return true;
    }

    /// <summary>
    ///  Checks every field against the values that the protocol specification allows.
    /// </summary>
    /// <returns>The fields whose values are not allowed, each with the reason, or an empty list if every value is allowed.</returns>
    /// <remarks>
    ///  Validate checks the current values. After a parse, check Status first, because a field that the parse did not reach holds its cleared value.
    /// </remarks>
    public List<ValidationFailure> Validate()
    {
        var failures = new List<ValidationFailure>();
        if (!global::System.Enum.IsDefined(this.strategyType))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.StrategyType), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Text.Ascii.IsValid(this.underlyingSymbol))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.UnderlyingSymbol), ValidationStatus.NonAsciiText));
        }
        for (var index = 0; index < this.legInformationList.Count; index++)
        {
            foreach (var failure in ((IGroupEntry)this.legInformationList[index]).Validate())
            {
                failures.Add(failure.Within(new FieldView(this, (int)FieldName.LegInformationList), index));
            }
        }
        return failures;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(ComplexStrategyDirectoryMessage? other)
    {
        if (other is null)
        {
            return false;
        }
        if (this.timestamp != other.timestamp) return false;
        if (this.strategyId != other.strategyId) return false;
        if (this.strategyType != other.strategyType) return false;
        if (this.source != other.source) return false;
        if (this.underlyingSymbol != other.underlyingSymbol) return false;
        if (!this.legInformationList.Equals(other.legInformationList)) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is ComplexStrategyDirectoryMessage other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.timestamp);
        hash.Add(this.strategyId);
        hash.Add(this.strategyType);
        hash.Add(this.source);
        hash.Add(this.underlyingSymbol);
        hash.Add(this.legInformationList);
        return hash.ToHashCode();
    }

    /// <summary>
    ///  Determines whether the specified bytes equal the result of <see cref="ToBytes"/>.
    /// </summary>
    /// <exception cref="EncodeException">A value does not fit its field.</exception>
    public bool BytesEqual(ReadOnlySpan<byte> data) => ToBytes().AsSpan().SequenceEqual(data);

    /// <summary>
    ///  Identifies the fields that differ between two instances.
    /// </summary>
    public readonly struct Changes : IEquatable<Changes>, IEnumerable<FieldName>
    {
        internal const int WordShift = 6;
        internal const int WordBitMask = 63;
        private const int WordCount = 1;
        private const int FieldCount = 8;

        [global::System.Runtime.CompilerServices.InlineArray(WordCount)]
        private struct Words
        {
            private ulong word;
        }

        private readonly Words words;

        private Changes(Words words)
        {
            this.words = words;
        }

        /// <summary>
        ///  Gets a value with no fields marked.
        /// </summary>
        public static Changes None => default;

        /// <summary>
        ///  Gets a value indicating whether no fields are marked.
        /// </summary>
        public bool IsNone => ((ReadOnlySpan<ulong>)this.words).IndexOfAnyExcept(0UL) < 0;

        internal ulong[] ToWords() => ((ReadOnlySpan<ulong>)this.words).ToArray();

        internal Changes With(int bit)
        {
            var word = bit >> WordShift;
            if ((uint)word >= WordCount) return this;
            var words = this.words;
            words[word] |= 1UL << (bit & WordBitMask);
            return new(words);
        }

        internal Changes With(FieldName field) => With((int)field);

        internal bool Has(int bit)
        {
            var word = bit >> WordShift;
            return (uint)word < WordCount && (this.words[word] & (1UL << (bit & WordBitMask))) != 0;
        }

        /// <summary>
        ///  Determines whether the specified field is marked.
        /// </summary>
        public bool Has(FieldName field) => Has((int)field);

        /// <summary>
        ///  Determines whether another value marks the same fields.
        /// </summary>
        public bool Equals(Changes other) => ((ReadOnlySpan<ulong>)this.words).SequenceEqual((ReadOnlySpan<ulong>)other.words);

        /// <summary>
        ///  Determines whether the specified object is a value that marks the same fields.
        /// </summary>
        public override bool Equals(object? obj) => obj is Changes other && Equals(other);

        /// <summary>
        ///  Returns a hash code computed from the marked fields.
        /// </summary>
        public override int GetHashCode()
        {
            var hc = new HashCode();
            foreach (var word in this.words)
            {
                hc.Add(word);
            }
            return hc.ToHashCode();
        }

        /// <summary>
        ///  Determines whether two values mark the same fields.
        /// </summary>
        public static bool operator ==(Changes a, Changes b) => a.Equals(b);

        /// <summary>
        ///  Determines whether two values mark different fields.
        /// </summary>
        public static bool operator !=(Changes a, Changes b) => !a.Equals(b);

        /// <summary>
        ///  Returns an enumerator that iterates through the marked fields in field order.
        /// </summary>
        public Enumerator GetEnumerator() => new(this);

        IEnumerator<FieldName> IEnumerable<FieldName>.GetEnumerator() => GetEnumerator();

        global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        ///  Enumerates the marked fields of a value.
        /// </summary>
        public struct Enumerator : IEnumerator<FieldName>
        {
            private const int BeforeFirst = -1;
            private readonly Changes changes;
            private int field;

            internal Enumerator(Changes changes)
            {
                this.changes = changes;
                this.field = BeforeFirst;
            }

            /// <summary>
            ///  Gets the current marked field.
            /// </summary>
            public readonly FieldName Current => (FieldName)this.field;

            readonly object global::System.Collections.IEnumerator.Current => Current;

            /// <summary>
            ///  Advances the enumerator to the next marked field.
            /// </summary>
            public bool MoveNext()
            {
                while (++this.field < FieldCount)
                {
                    if (this.changes.Has(this.field)) return true;
                }
                return false;
            }

            /// <summary>
            ///  Sets the enumerator to its initial position, before the first field.
            /// </summary>
            public void Reset() => this.field = BeforeFirst;

            /// <summary>
            ///  Releases the enumerator.
            /// </summary>
            public readonly void Dispose() { }
        }

        /// <summary>
        ///  Returns the names of the marked fields, or None if no fields are marked.
        /// </summary>
        public override string ToString()
        {
            var names = new List<string>();
            if (Has(FieldName.DataValue)) names.Add(nameof(FieldName.DataValue));
            if (Has(FieldName.Timestamp)) names.Add(nameof(FieldName.Timestamp));
            if (Has(FieldName.StrategyId)) names.Add(nameof(FieldName.StrategyId));
            if (Has(FieldName.StrategyType)) names.Add(nameof(FieldName.StrategyType));
            if (Has(FieldName.Source)) names.Add(nameof(FieldName.Source));
            if (Has(FieldName.UnderlyingSymbol)) names.Add(nameof(FieldName.UnderlyingSymbol));
            if (Has(FieldName.NumberOfLegs)) names.Add(nameof(FieldName.NumberOfLegs));
            if (Has(FieldName.LegInformationList)) names.Add(nameof(FieldName.LegInformationList));
            return names.Count == 0 ? "None" : string.Join(", ", names);
        }
    }

    /// <summary>
    ///  Compares this instance with another and identifies the fields that differ.
    /// </summary>
    /// <returns>The fields that differ, or None if there are no differences.</returns>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Changes Diff(ComplexStrategyDirectoryMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (this.timestamp != other.timestamp) diff = diff.With(FieldName.Timestamp);
        if (this.strategyId != other.strategyId) diff = diff.With(FieldName.StrategyId);
        if (this.strategyType != other.strategyType) diff = diff.With(FieldName.StrategyType);
        if (this.source != other.source) diff = diff.With(FieldName.Source);
        if (this.underlyingSymbol != other.underlyingSymbol) diff = diff.With(FieldName.UnderlyingSymbol);
        if (!this.legInformationList.Equals(other.legInformationList)) diff = diff.With(FieldName.LegInformationList);
        return diff;
    }

    private void CopyField(FieldName field, ComplexStrategyDirectoryMessage from)
    {
        switch (field)
        {
            case FieldName.Timestamp:
                this.timestamp = from.timestamp;
                break;
            case FieldName.StrategyId:
                this.strategyId = from.strategyId;
                break;
            case FieldName.StrategyType:
                this.strategyType = from.strategyType;
                break;
            case FieldName.Source:
                this.source = from.source;
                break;
            case FieldName.UnderlyingSymbol:
                this.underlyingSymbol = from.underlyingSymbol;
                break;
            case FieldName.NumberOfLegs:
                this.numberOfLegs = from.numberOfLegs;
                break;
            case FieldName.LegInformationList:
                this.legInformationList.SetOwner(null);
                this.legInformationList = new LegInformationList(from.legInformationList);
                this.legInformationList.SetOwner(this);
                break;
            default:
                return;
        }
        Touch();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"ComplexStrategyDirectoryMessage {{ Timestamp: {(Text.Midnight(this.timestamp, TimestampNanosecondsPerUnit))}, StrategyId: {(Text.Of(this.strategyId))}, StrategyType: {(Text.Enum(this.strategyType))}, Source: {(Text.Of(this.source))}, UnderlyingSymbol: {(Text.Of(this.underlyingSymbol))}, NumberOfLegs: {(Text.Of(this.numberOfLegs))}, LegInformationList: {(Text.Entries(this.legInformationList))} }}{(this.trailer.Length == 0 ? string.Empty : $" Trailer: {(Text.Of(this.trailer.Length))} bytes")}";

    private string DebuggerDisplay => $"[{DataValue}] [{(Text.Midnight(this.timestamp, TimestampNanosecondsPerUnit))}] [{(Text.Of(this.strategyId))}] [{(Text.Enum(this.strategyType))}]";

    IFieldOwner? IFieldOwner.Owner
    {
        get => this.owner;
        set => this.owner = value;
    }

    FieldDescriptor[] IFieldOwner.Descriptors => Descriptors;

    void IFieldOwner.Touch() => Touch();

    string IFieldOwner.TextOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => Text.Enum(PacketMessageCode.ComplexStrategyDirectoryMessage),
        FieldName.Timestamp => Text.Midnight(this.timestamp, TimestampNanosecondsPerUnit),
        FieldName.StrategyId => Text.Of(this.strategyId),
        FieldName.StrategyType => Text.Enum(this.strategyType),
        FieldName.Source => Text.Of(this.source),
        FieldName.UnderlyingSymbol => Text.Of(this.underlyingSymbol),
        FieldName.NumberOfLegs => Text.Of(this.numberOfLegs),
        FieldName.LegInformationList => Text.Entries(this.legInformationList),
        _ => string.Empty,
    };

    bool IFieldOwner.IsRecognized(int field) => (FieldName)field switch
    {
        FieldName.StrategyType => global::System.Enum.IsDefined(this.strategyType),
        FieldName.LegInformationList => this.legInformationList.IsRecognized,
        _ => true,
    };

    bool IFieldOwner.IsPresent(int field) => (FieldName)field switch
    {
        FieldName.LegInformationList => this.legInformationList.Count != 0,
        _ => true,
    };

    bool IFieldOwner.IsParsed(int field) => this.status == ParseStatus.Ok || field < (int?)this.failed;

    bool IFieldOwner.IsReached(int field) => this.modified || this.status is ParseStatus.None or ParseStatus.Ok || field <= (int?)this.failed;

    bool IFieldOwner.HasParseLayout(int field) => (FieldName)field switch
    {
        FieldName.LegInformationList => true,
        _ => false,
    };

    int IFieldOwner.OffsetOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => FieldView.NotInMessage,
        FieldName.Timestamp => TimestampOffset,
        FieldName.StrategyId => StrategyIdOffset,
        FieldName.StrategyType => StrategyTypeOffset,
        FieldName.Source => SourceOffset,
        FieldName.UnderlyingSymbol => UnderlyingSymbolOffset,
        FieldName.NumberOfLegs => NumberOfLegsOffset,
        FieldName.LegInformationList => LegInformationListOffset,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    int IFieldOwner.LengthOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => DataValueLength,
        FieldName.Timestamp => TimestampLength,
        FieldName.StrategyId => StrategyIdLength,
        FieldName.StrategyType => StrategyTypeLength,
        FieldName.Source => SourceLength,
        FieldName.UnderlyingSymbol => UnderlyingSymbolLength,
        FieldName.NumberOfLegs => NumberOfLegsLength,
        FieldName.LegInformationList => this.legInformationListLength,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    IReadOnlyList<IGroupEntry>? IFieldOwner.EntriesOf(int field) => (FieldName)field switch
    {
        FieldName.LegInformationList => this.legInformationList,
        _ => null,
    };

    IMessage? IFieldOwner.MessageOf(int field) => null;

    ulong[] IFieldOwner.ChangedAgainst(IFieldOwner other) => other is ComplexStrategyDirectoryMessage typed ? Diff(typed).ToWords() : throw new ArgumentException("The other instance is not of the same type.", nameof(other));

    void IFieldOwner.CopyField(int field, IFieldOwner from) => CopyField((FieldName)field, from as ComplexStrategyDirectoryMessage ?? throw new ArgumentException("The other instance is not of the same type.", nameof(from)));

    ReadOnlyMemory<byte> IFieldOwner.CodeBytes(int field)
    {
        if (field == (int)FieldName.DataValue)
        {
            var bytes = new byte[DataValueLength];
            bytes[0] = unchecked((byte)(PacketMessageCode.ComplexStrategyDirectoryMessage));
            return bytes;
        }
        return ReadOnlyMemory<byte>.Empty;
    }
}
