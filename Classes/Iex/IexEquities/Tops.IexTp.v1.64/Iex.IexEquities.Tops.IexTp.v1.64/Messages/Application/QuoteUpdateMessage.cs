using System.Buffers.Binary;
using System.Diagnostics;

namespace Iex.IexEquities.Tops.IexTp.v1_64;

/// <summary>
///  Tops broadcasts a real-time Quote Update Message each time IEX's best bid or offer quotation is updated during the trading day
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class QuoteUpdateMessage : IPacketMessage, IEquatable<QuoteUpdateMessage>, IFieldOwner
{
    internal const int QuoteUpdateFlagsOffset = 0;

    internal const int QuoteUpdateFlagsLength = 1;

    internal const int TimestampOffset = QuoteUpdateFlagsOffset + QuoteUpdateFlagsLength;

    internal const int TimestampLength = 8;

    /// <summary>
    ///  The number of nanoseconds in one unit of the wire value of <see cref="Timestamp"/>.
    /// </summary>
    public const long TimestampNanosecondsPerUnit = 1;

    internal const int SymbolOffset = TimestampOffset + TimestampLength;

    internal const int SymbolLength = 8;

    /// <summary>
    ///  The byte that fills the unused bytes of Symbol.
    /// </summary>
    public const byte SymbolFill = (byte)' ';

    internal const int BidSizeOffset = SymbolOffset + SymbolLength;

    internal const int BidSizeLength = 4;

    internal const int BidPriceOffset = BidSizeOffset + BidSizeLength;

    internal const int BidPriceLength = 8;

    /// <summary>
    ///  The number of decimal places in Bid Price.
    /// </summary>
    public const byte BidPriceScale = 4;

    /// <summary>
    ///  The factor by which <see cref="BidPrice"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal BidPriceFactor = 10000m;

    internal const int AskPriceOffset = BidPriceOffset + BidPriceLength;

    internal const int AskPriceLength = 8;

    /// <summary>
    ///  The number of decimal places in Ask Price.
    /// </summary>
    public const byte AskPriceScale = 4;

    /// <summary>
    ///  The factor by which <see cref="AskPrice"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal AskPriceFactor = 10000m;

    internal const int AskSizeOffset = AskPriceOffset + AskPriceLength;

    internal const int AskSizeLength = 4;

    /// <summary>
    ///  The encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public const int MessageByteLength = QuoteUpdateFlagsLength + TimestampLength + SymbolLength + BidSizeLength + BidPriceLength + AskPriceLength + AskSizeLength;

    internal QuoteUpdateFlags quoteUpdateFlags;

    /// <summary>
    ///  Quote Update Flags
    /// </summary>
    public QuoteUpdateFlags QuoteUpdateFlags
    {
        get => this.quoteUpdateFlags;
        set
        {
            this.quoteUpdateFlags = value;
            Touch();
        }
    }

    internal long timestamp;

    /// <summary>
    ///  Time stamp of the system event
    /// </summary>
    /// <remarks>
    ///  This property has a precision of 100 nanoseconds, and <see cref="TimestampRaw"/> holds the exact value. Setting a local time converts it to UTC, and a time of unspecified kind is treated as UTC. A time outside the range of the field is set to the nearest limit of the range.
    /// </remarks>
    public DateTime Timestamp
    {
        get => Instant.ToDateTime(this.timestamp, TimestampNanosecondsPerUnit);
        set
        {
            this.timestamp = Instant.ToRaw<long>(value, TimestampNanosecondsPerUnit, long.MinValue, long.MaxValue);
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="Timestamp"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long TimestampRaw
    {
        get => this.timestamp;
        set
        {
            this.timestamp = value;
            Touch();
        }
    }

    internal string symbol = string.Empty;

    /// <summary>
    ///  Security identifier
    /// </summary>
    /// <remarks>
    ///  The field is 8 bytes long. The field holds ASCII text. Text that is shorter than the field is padded with spaces at the end. Text longer than the field is cut to fit when the message is encoded. A character that the field cannot hold is replaced with a question mark when the message is encoded.
    /// </remarks>
    public string Symbol
    {
        get => this.symbol;
        set
        {
            this.symbol = value ?? string.Empty;
            Touch();
        }
    }

    internal uint bidSize;

    /// <summary>
    ///  Aggregate quoted best bid size
    /// </summary>
    public uint BidSize
    {
        get => this.bidSize;
        set
        {
            this.bidSize = value;
            Touch();
        }
    }

    internal long bidPrice;

    /// <summary>
    ///  Best quoted bid price
    /// </summary>
    /// <remarks>
    ///  A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range.
    /// </remarks>
    public decimal BidPrice
    {
        get => FixedPoint.ToDecimal(this.bidPrice, BidPriceScale);
        set
        {
            this.bidPrice = FixedPoint.ToRaw<long>(value, BidPriceFactor, long.MinValue, long.MaxValue);
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="BidPrice"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long BidPriceRaw
    {
        get => this.bidPrice;
        set
        {
            this.bidPrice = value;
            Touch();
        }
    }

    internal long askPrice;

    /// <summary>
    ///  Best quoted ask price
    /// </summary>
    /// <remarks>
    ///  A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range.
    /// </remarks>
    public decimal AskPrice
    {
        get => FixedPoint.ToDecimal(this.askPrice, AskPriceScale);
        set
        {
            this.askPrice = FixedPoint.ToRaw<long>(value, AskPriceFactor, long.MinValue, long.MaxValue);
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="AskPrice"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long AskPriceRaw
    {
        get => this.askPrice;
        set
        {
            this.askPrice = value;
            Touch();
        }
    }

    internal uint askSize;

    /// <summary>
    ///  Aggregate quoted best ask size
    /// </summary>
    public uint AskSize
    {
        get => this.askSize;
        set
        {
            this.askSize = value;
            Touch();
        }
    }

    /// <summary>
    ///  Gets the message type.
    /// </summary>
    public PacketMessageCode DataValue => PacketMessageCode.QuoteUpdateMessage;

    /// <summary>
    ///  Gets the encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public int ByteLength => MessageByteLength;

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
    public bool IsRecognized => true;

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

    private void Touch()
    {
        this.modified = true;
        this.cache = null;
        this.owner?.Touch();
    }

    IMessage IMessage.Copy() => new QuoteUpdateMessage(this);

    void IMessage.SetOwner(IFieldOwner? owner) => this.owner = owner;

    int IMessage.EncodedByteLength => MessageByteLength;

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
        ///  Quote Update Flags
        /// </summary>
        QuoteUpdateFlags,
        /// <summary>
        ///  Time stamp of the system event
        /// </summary>
        Timestamp,
        /// <summary>
        ///  Security identifier
        /// </summary>
        Symbol,
        /// <summary>
        ///  Aggregate quoted best bid size
        /// </summary>
        BidSize,
        /// <summary>
        ///  Best quoted bid price
        /// </summary>
        BidPrice,
        /// <summary>
        ///  Best quoted ask price
        /// </summary>
        AskPrice,
        /// <summary>
        ///  Aggregate quoted best ask size
        /// </summary>
        AskSize,
    }

    /// <summary>
    ///  Provides access to the fields of one instance, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public readonly struct FieldSet : IEnumerable<FieldView>
    {
        private readonly QuoteUpdateMessage owner;

        internal FieldSet(QuoteUpdateMessage owner)
        {
            this.owner = owner;
        }

        /// <summary>
        ///  The message type.
        /// </summary>
        public FieldView DataValue => this[FieldName.DataValue];

        /// <summary>
        ///  Quote Update Flags
        /// </summary>
        public FieldView QuoteUpdateFlags => this[FieldName.QuoteUpdateFlags];

        /// <summary>
        ///  Time stamp of the system event
        /// </summary>
        public FieldView Timestamp => this[FieldName.Timestamp];

        /// <summary>
        ///  Security identifier
        /// </summary>
        public FieldView Symbol => this[FieldName.Symbol];

        /// <summary>
        ///  Aggregate quoted best bid size
        /// </summary>
        public FieldView BidSize => this[FieldName.BidSize];

        /// <summary>
        ///  Best quoted bid price
        /// </summary>
        public FieldView BidPrice => this[FieldName.BidPrice];

        /// <summary>
        ///  Best quoted ask price
        /// </summary>
        public FieldView AskPrice => this[FieldName.AskPrice];

        /// <summary>
        ///  Aggregate quoted best ask size
        /// </summary>
        public FieldView AskSize => this[FieldName.AskSize];

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
        new(nameof(FieldName.QuoteUpdateFlags), FieldKind.Bitfield, QuoteUpdateFlagsLength) { IsSigned = false, Bits = [(nameof(global::Iex.IexEquities.Tops.IexTp.v1_64.QuoteUpdateFlags.MarketSession), global::Iex.IexEquities.Tops.IexTp.v1_64.QuoteUpdateFlags.MarketSessionShift, global::Iex.IexEquities.Tops.IexTp.v1_64.QuoteUpdateFlags.MarketSessionWidth), (nameof(global::Iex.IexEquities.Tops.IexTp.v1_64.QuoteUpdateFlags.SymbolAvailability), global::Iex.IexEquities.Tops.IexTp.v1_64.QuoteUpdateFlags.SymbolAvailabilityShift, global::Iex.IexEquities.Tops.IexTp.v1_64.QuoteUpdateFlags.SymbolAvailabilityWidth)] },
        new(nameof(FieldName.Timestamp), FieldKind.DateTime, TimestampLength) { Unit = "Nanoseconds", Epoch = "Unix", ByteOrder = "Little", IsSigned = true },
        new(nameof(FieldName.Symbol), FieldKind.String, SymbolLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.BidSize), FieldKind.UnsignedInteger, BidSizeLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.BidPrice), FieldKind.Decimal, BidPriceLength) { Scale = BidPriceScale, ByteOrder = "Little", IsSigned = true },
        new(nameof(FieldName.AskPrice), FieldKind.Decimal, AskPriceLength) { Scale = AskPriceScale, ByteOrder = "Little", IsSigned = true },
        new(nameof(FieldName.AskSize), FieldKind.UnsignedInteger, AskSizeLength) { ByteOrder = "Little", IsSigned = false },
    ];

    /// <summary>
    ///  Gets the fields in wire order, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public FieldSet Fields => new(this);

    IEnumerable<FieldView> IMessage.Fields => new FieldSet(this);

    /// <summary>
    ///  Initializes a new instance of the <see cref="QuoteUpdateMessage"/> class with default values.
    /// </summary>
    public QuoteUpdateMessage()
    {
        Clear();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="QuoteUpdateMessage"/> class with the values, bytes, and parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public QuoteUpdateMessage(QuoteUpdateMessage other) : this()
    {
        CopyFrom(other);
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(QuoteUpdateMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.quoteUpdateFlags = other.quoteUpdateFlags;
        this.timestamp = other.timestamp;
        this.symbol = other.symbol;
        this.bidSize = other.bidSize;
        this.bidPrice = other.bidPrice;
        this.askPrice = other.askPrice;
        this.askSize = other.askSize;
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
        this.quoteUpdateFlags = default;
        this.timestamp = default;
        this.symbol = string.Empty;
        this.bidSize = default;
        this.bidPrice = default;
        this.askPrice = default;
        this.askSize = default;
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
    public static QuoteUpdateMessage Parse(ReadOnlySpan<byte> data)
    {
        var instance = new QuoteUpdateMessage();
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
    public static QuoteUpdateMessage Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        var instance = new QuoteUpdateMessage();
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
        if (Truncated(data, start, cursor, QuoteUpdateFlagsLength, FieldName.QuoteUpdateFlags)) return;
        this.quoteUpdateFlags = new QuoteUpdateFlags(data.Slice(cursor, QuoteUpdateFlagsLength)[0]);
        cursor += QuoteUpdateFlagsLength;
        if (Truncated(data, start, cursor, TimestampLength, FieldName.Timestamp)) return;
        this.timestamp = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(cursor, TimestampLength));
        cursor += TimestampLength;
        if (Truncated(data, start, cursor, SymbolLength, FieldName.Symbol)) return;
        this.symbol = TextEncoding.Ascii.ReadLeftJustified(data.Slice(cursor, SymbolLength), SymbolFill);
        cursor += SymbolLength;
        if (Truncated(data, start, cursor, BidSizeLength, FieldName.BidSize)) return;
        this.bidSize = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(cursor, BidSizeLength));
        cursor += BidSizeLength;
        if (Truncated(data, start, cursor, BidPriceLength, FieldName.BidPrice)) return;
        this.bidPrice = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(cursor, BidPriceLength));
        cursor += BidPriceLength;
        if (Truncated(data, start, cursor, AskPriceLength, FieldName.AskPrice)) return;
        this.askPrice = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(cursor, AskPriceLength));
        cursor += AskPriceLength;
        if (Truncated(data, start, cursor, AskSizeLength, FieldName.AskSize)) return;
        this.askSize = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(cursor, AskSizeLength));
        cursor += AskSizeLength;
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
    /// <exception cref="ArgumentOutOfRangeException">The value of offset is negative or greater than the length of the span.</exception>
    /// <exception cref="ArgumentException">The span is too small for the encoded bytes.</exception>
    /// <remarks>
    ///  The bytes are those of the fields of the message, from its first field to its last.
    /// </remarks>
    public int Encode(Span<byte> data, int offset)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(offset);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, data.Length);
        if (data.Length - offset < MessageByteLength)
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
    /// <exception cref="ArgumentException">The span is too small for the encoded bytes.</exception>
    /// <remarks>
    ///  The bytes are those of the fields of the message, from its first field to its last.
    /// </remarks>
    public int Encode(Span<byte> data) => Encode(data, 0);

    /// <summary>
    ///  Encodes this instance into a new array.
    /// </summary>
    /// <remarks>
    ///  The bytes are those of the fields of the message, from its first field to its last.
    /// </remarks>
    public byte[] ToBytes()
    {
        var buffer = new byte[MessageByteLength];
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
        if (data.Length < MessageByteLength)
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
        data.Slice(cursor, QuoteUpdateFlagsLength)[0] = this.quoteUpdateFlags.Value;
        cursor += QuoteUpdateFlagsLength;
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(cursor, TimestampLength), this.timestamp);
        cursor += TimestampLength;
        TextEncoding.Ascii.WriteLeftJustified(data.Slice(cursor, SymbolLength), TextEncoding.Ascii.Fit(this.symbol, SymbolLength), SymbolFill);
        cursor += SymbolLength;
        BinaryPrimitives.WriteUInt32LittleEndian(data.Slice(cursor, BidSizeLength), this.bidSize);
        cursor += BidSizeLength;
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(cursor, BidPriceLength), this.bidPrice);
        cursor += BidPriceLength;
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(cursor, AskPriceLength), this.askPrice);
        cursor += AskPriceLength;
        BinaryPrimitives.WriteUInt32LittleEndian(data.Slice(cursor, AskSizeLength), this.askSize);
        cursor += AskSizeLength;
        failure = default;
        return true;
    }

    bool IFieldOwner.TryRaw(out ReadOnlyMemory<byte> raw)
    {
        if (this.status == ParseStatus.None || this.modified)
        {
            if (this.cache is null)
            {
                var buffer = new byte[MessageByteLength];
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
        if (!global::System.Text.Ascii.IsValid(this.symbol))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.Symbol), ValidationStatus.NonAsciiText));
        }
        return failures;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(QuoteUpdateMessage? other)
    {
        if (other is null)
        {
            return false;
        }
        if (this.quoteUpdateFlags != other.quoteUpdateFlags) return false;
        if (this.timestamp != other.timestamp) return false;
        if (this.symbol != other.symbol) return false;
        if (this.bidSize != other.bidSize) return false;
        if (this.bidPrice != other.bidPrice) return false;
        if (this.askPrice != other.askPrice) return false;
        if (this.askSize != other.askSize) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is QuoteUpdateMessage other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.quoteUpdateFlags);
        hash.Add(this.timestamp);
        hash.Add(this.symbol);
        hash.Add(this.bidSize);
        hash.Add(this.bidPrice);
        hash.Add(this.askPrice);
        hash.Add(this.askSize);
        return hash.ToHashCode();
    }

    /// <summary>
    ///  Determines whether the specified bytes equal the result of <see cref="ToBytes"/>.
    /// </summary>
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
            if (Has(FieldName.QuoteUpdateFlags)) names.Add(nameof(FieldName.QuoteUpdateFlags));
            if (Has(FieldName.Timestamp)) names.Add(nameof(FieldName.Timestamp));
            if (Has(FieldName.Symbol)) names.Add(nameof(FieldName.Symbol));
            if (Has(FieldName.BidSize)) names.Add(nameof(FieldName.BidSize));
            if (Has(FieldName.BidPrice)) names.Add(nameof(FieldName.BidPrice));
            if (Has(FieldName.AskPrice)) names.Add(nameof(FieldName.AskPrice));
            if (Has(FieldName.AskSize)) names.Add(nameof(FieldName.AskSize));
            return names.Count == 0 ? "None" : string.Join(", ", names);
        }
    }

    /// <summary>
    ///  Compares this instance with another and identifies the fields that differ.
    /// </summary>
    /// <returns>The fields that differ, or None if there are no differences.</returns>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Changes Diff(QuoteUpdateMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (this.quoteUpdateFlags != other.quoteUpdateFlags) diff = diff.With(FieldName.QuoteUpdateFlags);
        if (this.timestamp != other.timestamp) diff = diff.With(FieldName.Timestamp);
        if (this.symbol != other.symbol) diff = diff.With(FieldName.Symbol);
        if (this.bidSize != other.bidSize) diff = diff.With(FieldName.BidSize);
        if (this.bidPrice != other.bidPrice) diff = diff.With(FieldName.BidPrice);
        if (this.askPrice != other.askPrice) diff = diff.With(FieldName.AskPrice);
        if (this.askSize != other.askSize) diff = diff.With(FieldName.AskSize);
        return diff;
    }

    private void CopyField(FieldName field, QuoteUpdateMessage from)
    {
        switch (field)
        {
            case FieldName.QuoteUpdateFlags:
                this.quoteUpdateFlags = from.quoteUpdateFlags;
                break;
            case FieldName.Timestamp:
                this.timestamp = from.timestamp;
                break;
            case FieldName.Symbol:
                this.symbol = from.symbol;
                break;
            case FieldName.BidSize:
                this.bidSize = from.bidSize;
                break;
            case FieldName.BidPrice:
                this.bidPrice = from.bidPrice;
                break;
            case FieldName.AskPrice:
                this.askPrice = from.askPrice;
                break;
            case FieldName.AskSize:
                this.askSize = from.askSize;
                break;
            default:
                return;
        }
        Touch();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"QuoteUpdateMessage {{ QuoteUpdateFlags: {(Text.Value(this.quoteUpdateFlags))}, Timestamp: {(Text.Instant(this.timestamp, TimestampNanosecondsPerUnit))}, Symbol: {(Text.Of(this.symbol))}, BidSize: {(Text.Of(this.bidSize))}, BidPrice: {(Text.Of(FixedPoint.ToDecimal(this.bidPrice, BidPriceScale)))}, AskPrice: {(Text.Of(FixedPoint.ToDecimal(this.askPrice, AskPriceScale)))}, AskSize: {(Text.Of(this.askSize))} }}{(this.trailer.Length == 0 ? string.Empty : $" Trailer: {(Text.Of(this.trailer.Length))} bytes")}";

    private string DebuggerDisplay => $"[{DataValue}] [{(Text.Value(this.quoteUpdateFlags))}] [{(Text.Instant(this.timestamp, TimestampNanosecondsPerUnit))}] [{(Text.Of(this.symbol))}]";

    IFieldOwner? IFieldOwner.Owner
    {
        get => this.owner;
        set => this.owner = value;
    }

    FieldDescriptor[] IFieldOwner.Descriptors => Descriptors;

    void IFieldOwner.Touch() => Touch();

    string IFieldOwner.TextOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => Text.Enum(PacketMessageCode.QuoteUpdateMessage),
        FieldName.QuoteUpdateFlags => Text.Value(this.quoteUpdateFlags),
        FieldName.Timestamp => Text.Instant(this.timestamp, TimestampNanosecondsPerUnit),
        FieldName.Symbol => Text.Of(this.symbol),
        FieldName.BidSize => Text.Of(this.bidSize),
        FieldName.BidPrice => Text.Of(FixedPoint.ToDecimal(this.bidPrice, BidPriceScale)),
        FieldName.AskPrice => Text.Of(FixedPoint.ToDecimal(this.askPrice, AskPriceScale)),
        FieldName.AskSize => Text.Of(this.askSize),
        _ => string.Empty,
    };

    bool IFieldOwner.IsRecognized(int field) => true;

    bool IFieldOwner.IsPresent(int field) => true;

    bool IFieldOwner.IsParsed(int field) => this.status == ParseStatus.Ok || field < (int?)this.failed;

    bool IFieldOwner.IsReached(int field) => this.modified || this.status is ParseStatus.None or ParseStatus.Ok || field <= (int?)this.failed;

    bool IFieldOwner.HasParseLayout(int field) => false;

    int IFieldOwner.OffsetOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => FieldView.NotInMessage,
        FieldName.QuoteUpdateFlags => QuoteUpdateFlagsOffset,
        FieldName.Timestamp => TimestampOffset,
        FieldName.Symbol => SymbolOffset,
        FieldName.BidSize => BidSizeOffset,
        FieldName.BidPrice => BidPriceOffset,
        FieldName.AskPrice => AskPriceOffset,
        FieldName.AskSize => AskSizeOffset,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    int IFieldOwner.LengthOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => DataValueLength,
        FieldName.QuoteUpdateFlags => QuoteUpdateFlagsLength,
        FieldName.Timestamp => TimestampLength,
        FieldName.Symbol => SymbolLength,
        FieldName.BidSize => BidSizeLength,
        FieldName.BidPrice => BidPriceLength,
        FieldName.AskPrice => AskPriceLength,
        FieldName.AskSize => AskSizeLength,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    IReadOnlyList<IGroupEntry>? IFieldOwner.EntriesOf(int field) => null;

    IMessage? IFieldOwner.MessageOf(int field) => null;

    ulong[] IFieldOwner.ChangedAgainst(IFieldOwner other) => other is QuoteUpdateMessage typed ? Diff(typed).ToWords() : throw new ArgumentException("The other instance is not of the same type.", nameof(other));

    void IFieldOwner.CopyField(int field, IFieldOwner from) => CopyField((FieldName)field, from as QuoteUpdateMessage ?? throw new ArgumentException("The other instance is not of the same type.", nameof(from)));

    ReadOnlyMemory<byte> IFieldOwner.CodeBytes(int field)
    {
        if (field == (int)FieldName.DataValue)
        {
            var bytes = new byte[DataValueLength];
            bytes[0] = unchecked((byte)(PacketMessageCode.QuoteUpdateMessage));
            return bytes;
        }
        return ReadOnlyMemory<byte>.Empty;
    }
}
