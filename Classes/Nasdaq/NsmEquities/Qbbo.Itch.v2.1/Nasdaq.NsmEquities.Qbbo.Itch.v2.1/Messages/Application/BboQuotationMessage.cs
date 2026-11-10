using System.Buffers.Binary;
using System.Diagnostics;

namespace Nasdaq.NsmEquities.Qbbo.Itch.v2_1;

/// <summary>
///  BBO products will broadcast a real-time update every time that the exchange's best bid and offer quote is updated during the trading day.
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class BboQuotationMessage : IPacketMessage, IEquatable<BboQuotationMessage>, IFieldOwner
{
    internal const int TrackingNumberOffset = 0;

    internal const int TrackingNumberLength = 2;

    internal const int TimeStampOffset = TrackingNumberOffset + TrackingNumberLength;

    internal const int TimeStampLength = 6;

    /// <summary>
    ///  The number of nanoseconds in one unit of the wire value of <see cref="TimeStamp"/>.
    /// </summary>
    public const long TimeStampNanosecondsPerUnit = 1;

    /// <summary>
    ///  The wire value of <see cref="TimeStamp"/> for a time of one full day.
    /// </summary>
    public const long TimeStampUnitsPerDay = TimeSpan.TicksPerDay * TimeSpan.NanosecondsPerTick / TimeStampNanosecondsPerUnit;

    /// <summary>
    ///  The largest wire value that <see cref="TimeStamp"/> can hold.
    /// </summary>
    public const ulong TimeStampWireMaximum = (1UL << 48) - 1UL;

    internal const int StockOffset = TimeStampOffset + TimeStampLength;

    internal const int StockLength = 8;

    /// <summary>
    ///  The byte that fills the unused bytes of Stock.
    /// </summary>
    public const byte StockFill = (byte)' ';

    internal const int SecurityClassOffset = StockOffset + StockLength;

    internal const int SecurityClassLength = 1;

    internal const int BestBidPriceOffset = SecurityClassOffset + SecurityClassLength;

    internal const int BestBidPriceLength = 4;

    /// <summary>
    ///  The number of decimal places in Best Bid Price.
    /// </summary>
    public const byte BestBidPriceScale = 4;

    /// <summary>
    ///  The factor by which <see cref="BestBidPrice"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal BestBidPriceFactor = 10000m;

    internal const int BestBidSizeOffset = BestBidPriceOffset + BestBidPriceLength;

    internal const int BestBidSizeLength = 4;

    internal const int BestOfferPriceOffset = BestBidSizeOffset + BestBidSizeLength;

    internal const int BestOfferPriceLength = 4;

    /// <summary>
    ///  The number of decimal places in Best Offer Price.
    /// </summary>
    public const byte BestOfferPriceScale = 4;

    /// <summary>
    ///  The factor by which <see cref="BestOfferPrice"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal BestOfferPriceFactor = 10000m;

    internal const int BestOfferSizeOffset = BestOfferPriceOffset + BestOfferPriceLength;

    internal const int BestOfferSizeLength = 4;

    /// <summary>
    ///  The encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public const int MessageByteLength = TrackingNumberLength + TimeStampLength + StockLength + SecurityClassLength + BestBidPriceLength + BestBidSizeLength + BestOfferPriceLength + BestOfferSizeLength;

    internal ushort trackingNumber;

    /// <summary>
    ///  Nasdaq internal tracking number
    /// </summary>
    public ushort TrackingNumber
    {
        get => this.trackingNumber;
        set
        {
            this.trackingNumber = value;
            Touch();
        }
    }

    internal ulong timeStamp;

    /// <summary>
    ///  Timestamp
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds a time outside one day. This property has a precision of 100 nanoseconds, and <see cref="TimeStampRaw"/> holds the exact value. A time is rounded to the wire unit of the field, with a midpoint away from zero. A time past the largest wire value is set to that value.
    /// </remarks>
    public TimeOnly? TimeStamp
    {
        get => Midnight.ToTimeOnly(this.timeStamp, TimeStampNanosecondsPerUnit, TimeStampUnitsPerDay);
        set
        {
            this.timeStamp = value is { } present ? Midnight.ToRaw<ulong>(present, TimeStampNanosecondsPerUnit, TimeStampWireMaximum) : default;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="TimeStamp"/>, as the unscaled integer that the field holds.
    /// </summary>
    public ulong TimeStampRaw
    {
        get => this.timeStamp;
        set
        {
            this.timeStamp = value;
            Touch();
        }
    }

    internal string stock = string.Empty;

    /// <summary>
    ///  Denotes the security symbol for the issue in the execution system.
    /// </summary>
    /// <remarks>
    ///  The field is 8 bytes long. The field holds ASCII text. Text that is shorter than the field is padded with spaces at the end. Text longer than the field is cut to fit when the message is encoded. A character that the field cannot hold is replaced with a question mark when the message is encoded.
    /// </remarks>
    public string Stock
    {
        get => this.stock;
        set
        {
            this.stock = value ?? string.Empty;
            Touch();
        }
    }

    internal SecurityClass securityClass;

    /// <summary>
    ///  Indicates the primary listing market for the issue.
    /// </summary>
    public SecurityClass SecurityClass
    {
        get => this.securityClass;
        set
        {
            this.securityClass = value;
            Touch();
        }
    }

    internal uint bestBidPrice;

    /// <summary>
    ///  Denotes the best bid price, or the highest price for market buy order(s) in the execution system.
    /// </summary>
    /// <remarks>
    ///  A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range.
    /// </remarks>
    public decimal BestBidPrice
    {
        get => FixedPoint.ToDecimal(this.bestBidPrice, BestBidPriceScale);
        set
        {
            this.bestBidPrice = FixedPoint.ToRaw<uint>(value, BestBidPriceFactor, uint.MinValue, uint.MaxValue);
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="BestBidPrice"/>, as the unscaled integer that the field holds.
    /// </summary>
    public uint BestBidPriceRaw
    {
        get => this.bestBidPrice;
        set
        {
            this.bestBidPrice = value;
            Touch();
        }
    }

    internal uint bestBidSize;

    /// <summary>
    ///  Denotes the aggregated number of shares available for display within the execution system at the best bid price.
    /// </summary>
    public uint BestBidSize
    {
        get => this.bestBidSize;
        set
        {
            this.bestBidSize = value;
            Touch();
        }
    }

    internal uint bestOfferPrice;

    /// <summary>
    ///  Denotes the Nasdaq exchange's best offer price, or the lowest price for market sell order(s) in the execution system.
    /// </summary>
    /// <remarks>
    ///  A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range.
    /// </remarks>
    public decimal BestOfferPrice
    {
        get => FixedPoint.ToDecimal(this.bestOfferPrice, BestOfferPriceScale);
        set
        {
            this.bestOfferPrice = FixedPoint.ToRaw<uint>(value, BestOfferPriceFactor, uint.MinValue, uint.MaxValue);
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="BestOfferPrice"/>, as the unscaled integer that the field holds.
    /// </summary>
    public uint BestOfferPriceRaw
    {
        get => this.bestOfferPrice;
        set
        {
            this.bestOfferPrice = value;
            Touch();
        }
    }

    internal uint bestOfferSize;

    /// <summary>
    ///  Denotes the aggregated number of shares available for display within the execution system at the best offer price.
    /// </summary>
    public uint BestOfferSize
    {
        get => this.bestOfferSize;
        set
        {
            this.bestOfferSize = value;
            Touch();
        }
    }

    /// <summary>
    ///  Gets the message type.
    /// </summary>
    public PacketMessageCode DataValue => PacketMessageCode.BboQuotationMessage;

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
    public bool IsRecognized => global::System.Enum.IsDefined(this.securityClass);

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

    IMessage IMessage.Copy() => new BboQuotationMessage(this);

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
        ///  Nasdaq internal tracking number
        /// </summary>
        TrackingNumber,
        /// <summary>
        ///  Timestamp
        /// </summary>
        TimeStamp,
        /// <summary>
        ///  Denotes the security symbol for the issue in the execution system.
        /// </summary>
        Stock,
        /// <summary>
        ///  Indicates the primary listing market for the issue.
        /// </summary>
        SecurityClass,
        /// <summary>
        ///  Denotes the best bid price, or the highest price for market buy order(s) in the execution system.
        /// </summary>
        BestBidPrice,
        /// <summary>
        ///  Denotes the aggregated number of shares available for display within the execution system at the best bid price.
        /// </summary>
        BestBidSize,
        /// <summary>
        ///  Denotes the Nasdaq exchange's best offer price, or the lowest price for market sell order(s) in the execution system.
        /// </summary>
        BestOfferPrice,
        /// <summary>
        ///  Denotes the aggregated number of shares available for display within the execution system at the best offer price.
        /// </summary>
        BestOfferSize,
    }

    /// <summary>
    ///  Provides access to the fields of one instance, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public readonly struct FieldSet : IEnumerable<FieldView>
    {
        private readonly BboQuotationMessage owner;

        internal FieldSet(BboQuotationMessage owner)
        {
            this.owner = owner;
        }

        /// <summary>
        ///  The message type.
        /// </summary>
        public FieldView DataValue => this[FieldName.DataValue];

        /// <summary>
        ///  Nasdaq internal tracking number
        /// </summary>
        public FieldView TrackingNumber => this[FieldName.TrackingNumber];

        /// <summary>
        ///  Timestamp
        /// </summary>
        public FieldView TimeStamp => this[FieldName.TimeStamp];

        /// <summary>
        ///  Denotes the security symbol for the issue in the execution system.
        /// </summary>
        public FieldView Stock => this[FieldName.Stock];

        /// <summary>
        ///  Indicates the primary listing market for the issue.
        /// </summary>
        public FieldView SecurityClass => this[FieldName.SecurityClass];

        /// <summary>
        ///  Denotes the best bid price, or the highest price for market buy order(s) in the execution system.
        /// </summary>
        public FieldView BestBidPrice => this[FieldName.BestBidPrice];

        /// <summary>
        ///  Denotes the aggregated number of shares available for display within the execution system at the best bid price.
        /// </summary>
        public FieldView BestBidSize => this[FieldName.BestBidSize];

        /// <summary>
        ///  Denotes the Nasdaq exchange's best offer price, or the lowest price for market sell order(s) in the execution system.
        /// </summary>
        public FieldView BestOfferPrice => this[FieldName.BestOfferPrice];

        /// <summary>
        ///  Denotes the aggregated number of shares available for display within the execution system at the best offer price.
        /// </summary>
        public FieldView BestOfferSize => this[FieldName.BestOfferSize];

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
        new(nameof(FieldName.TrackingNumber), FieldKind.UnsignedInteger, TrackingNumberLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.TimeStamp), FieldKind.UnsignedInteger, TimeStampLength) { Unit = "Nanoseconds", Epoch = "Midnight", Timezone = "America/New_York", ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.Stock), FieldKind.String, StockLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.SecurityClass), FieldKind.Enum, SecurityClassLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.BestBidPrice), FieldKind.Decimal, BestBidPriceLength) { Scale = BestBidPriceScale, ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.BestBidSize), FieldKind.UnsignedInteger, BestBidSizeLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.BestOfferPrice), FieldKind.Decimal, BestOfferPriceLength) { Scale = BestOfferPriceScale, ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.BestOfferSize), FieldKind.UnsignedInteger, BestOfferSizeLength) { ByteOrder = "Big", IsSigned = false },
    ];

    /// <summary>
    ///  Gets the fields in wire order, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public FieldSet Fields => new(this);

    IEnumerable<FieldView> IMessage.Fields => new FieldSet(this);

    /// <summary>
    ///  Initializes a new instance of the <see cref="BboQuotationMessage"/> class with default values.
    /// </summary>
    public BboQuotationMessage()
    {
        Clear();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="BboQuotationMessage"/> class with the values, bytes, and parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public BboQuotationMessage(BboQuotationMessage other) : this()
    {
        CopyFrom(other);
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(BboQuotationMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.trackingNumber = other.trackingNumber;
        this.timeStamp = other.timeStamp;
        this.stock = other.stock;
        this.securityClass = other.securityClass;
        this.bestBidPrice = other.bestBidPrice;
        this.bestBidSize = other.bestBidSize;
        this.bestOfferPrice = other.bestOfferPrice;
        this.bestOfferSize = other.bestOfferSize;
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
        this.trackingNumber = default;
        this.timeStamp = default;
        this.stock = string.Empty;
        this.securityClass = default;
        this.bestBidPrice = default;
        this.bestBidSize = default;
        this.bestOfferPrice = default;
        this.bestOfferSize = default;
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
    public static BboQuotationMessage Parse(ReadOnlySpan<byte> data)
    {
        var instance = new BboQuotationMessage();
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
    public static BboQuotationMessage Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        var instance = new BboQuotationMessage();
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
        if (Truncated(data, start, cursor, TrackingNumberLength, FieldName.TrackingNumber)) return;
        this.trackingNumber = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(cursor, TrackingNumberLength));
        cursor += TrackingNumberLength;
        if (Truncated(data, start, cursor, TimeStampLength, FieldName.TimeStamp)) return;
        this.timeStamp = Padded.ReadBigEndian<ulong>(data.Slice(cursor, TimeStampLength));
        cursor += TimeStampLength;
        if (Truncated(data, start, cursor, StockLength, FieldName.Stock)) return;
        this.stock = TextEncoding.Ascii.ReadLeftJustified(data.Slice(cursor, StockLength), StockFill);
        cursor += StockLength;
        if (Truncated(data, start, cursor, SecurityClassLength, FieldName.SecurityClass)) return;
        this.securityClass = (SecurityClass)data.Slice(cursor, SecurityClassLength)[0];
        cursor += SecurityClassLength;
        if (Truncated(data, start, cursor, BestBidPriceLength, FieldName.BestBidPrice)) return;
        this.bestBidPrice = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, BestBidPriceLength));
        cursor += BestBidPriceLength;
        if (Truncated(data, start, cursor, BestBidSizeLength, FieldName.BestBidSize)) return;
        this.bestBidSize = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, BestBidSizeLength));
        cursor += BestBidSizeLength;
        if (Truncated(data, start, cursor, BestOfferPriceLength, FieldName.BestOfferPrice)) return;
        this.bestOfferPrice = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, BestOfferPriceLength));
        cursor += BestOfferPriceLength;
        if (Truncated(data, start, cursor, BestOfferSizeLength, FieldName.BestOfferSize)) return;
        this.bestOfferSize = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, BestOfferSizeLength));
        cursor += BestOfferSizeLength;
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
        BinaryPrimitives.WriteUInt16BigEndian(data.Slice(cursor, TrackingNumberLength), this.trackingNumber);
        cursor += TrackingNumberLength;
        if (this.timeStamp > TimeStampWireMaximum)
        {
            failure = new EncodeFailure(new FieldView(this, (int)FieldName.TimeStamp), EncodeStatus.DoesNotFit);
            return false;
        }
        Padded.WriteBigEndian(data.Slice(cursor, TimeStampLength), this.timeStamp);
        cursor += TimeStampLength;
        TextEncoding.Ascii.WriteLeftJustified(data.Slice(cursor, StockLength), TextEncoding.Ascii.Fit(this.stock, StockLength), StockFill);
        cursor += StockLength;
        data.Slice(cursor, SecurityClassLength)[0] = unchecked((byte)(this.securityClass));
        cursor += SecurityClassLength;
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, BestBidPriceLength), this.bestBidPrice);
        cursor += BestBidPriceLength;
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, BestBidSizeLength), this.bestBidSize);
        cursor += BestBidSizeLength;
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, BestOfferPriceLength), this.bestOfferPrice);
        cursor += BestOfferPriceLength;
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, BestOfferSizeLength), this.bestOfferSize);
        cursor += BestOfferSizeLength;
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
        if (!global::System.Text.Ascii.IsValid(this.stock))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.Stock), ValidationStatus.NonAsciiText));
        }
        if (!global::System.Enum.IsDefined(this.securityClass))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.SecurityClass), ValidationStatus.UndeclaredValue));
        }
        return failures;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(BboQuotationMessage? other)
    {
        if (other is null)
        {
            return false;
        }
        if (this.trackingNumber != other.trackingNumber) return false;
        if (this.timeStamp != other.timeStamp) return false;
        if (this.stock != other.stock) return false;
        if (this.securityClass != other.securityClass) return false;
        if (this.bestBidPrice != other.bestBidPrice) return false;
        if (this.bestBidSize != other.bestBidSize) return false;
        if (this.bestOfferPrice != other.bestOfferPrice) return false;
        if (this.bestOfferSize != other.bestOfferSize) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is BboQuotationMessage other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.trackingNumber);
        hash.Add(this.timeStamp);
        hash.Add(this.stock);
        hash.Add(this.securityClass);
        hash.Add(this.bestBidPrice);
        hash.Add(this.bestBidSize);
        hash.Add(this.bestOfferPrice);
        hash.Add(this.bestOfferSize);
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
        private const int FieldCount = 9;

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
            if (Has(FieldName.TrackingNumber)) names.Add(nameof(FieldName.TrackingNumber));
            if (Has(FieldName.TimeStamp)) names.Add(nameof(FieldName.TimeStamp));
            if (Has(FieldName.Stock)) names.Add(nameof(FieldName.Stock));
            if (Has(FieldName.SecurityClass)) names.Add(nameof(FieldName.SecurityClass));
            if (Has(FieldName.BestBidPrice)) names.Add(nameof(FieldName.BestBidPrice));
            if (Has(FieldName.BestBidSize)) names.Add(nameof(FieldName.BestBidSize));
            if (Has(FieldName.BestOfferPrice)) names.Add(nameof(FieldName.BestOfferPrice));
            if (Has(FieldName.BestOfferSize)) names.Add(nameof(FieldName.BestOfferSize));
            return names.Count == 0 ? "None" : string.Join(", ", names);
        }
    }

    /// <summary>
    ///  Compares this instance with another and identifies the fields that differ.
    /// </summary>
    /// <returns>The fields that differ, or None if there are no differences.</returns>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Changes Diff(BboQuotationMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (this.trackingNumber != other.trackingNumber) diff = diff.With(FieldName.TrackingNumber);
        if (this.timeStamp != other.timeStamp) diff = diff.With(FieldName.TimeStamp);
        if (this.stock != other.stock) diff = diff.With(FieldName.Stock);
        if (this.securityClass != other.securityClass) diff = diff.With(FieldName.SecurityClass);
        if (this.bestBidPrice != other.bestBidPrice) diff = diff.With(FieldName.BestBidPrice);
        if (this.bestBidSize != other.bestBidSize) diff = diff.With(FieldName.BestBidSize);
        if (this.bestOfferPrice != other.bestOfferPrice) diff = diff.With(FieldName.BestOfferPrice);
        if (this.bestOfferSize != other.bestOfferSize) diff = diff.With(FieldName.BestOfferSize);
        return diff;
    }

    private void CopyField(FieldName field, BboQuotationMessage from)
    {
        switch (field)
        {
            case FieldName.TrackingNumber:
                this.trackingNumber = from.trackingNumber;
                break;
            case FieldName.TimeStamp:
                this.timeStamp = from.timeStamp;
                break;
            case FieldName.Stock:
                this.stock = from.stock;
                break;
            case FieldName.SecurityClass:
                this.securityClass = from.securityClass;
                break;
            case FieldName.BestBidPrice:
                this.bestBidPrice = from.bestBidPrice;
                break;
            case FieldName.BestBidSize:
                this.bestBidSize = from.bestBidSize;
                break;
            case FieldName.BestOfferPrice:
                this.bestOfferPrice = from.bestOfferPrice;
                break;
            case FieldName.BestOfferSize:
                this.bestOfferSize = from.bestOfferSize;
                break;
            default:
                return;
        }
        Touch();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"BboQuotationMessage {{ TrackingNumber: {(Text.Of(this.trackingNumber))}, TimeStamp: {(Text.Midnight(this.timeStamp, TimeStampNanosecondsPerUnit))}, Stock: {(Text.Of(this.stock))}, SecurityClass: {(Text.Enum(this.securityClass))}, BestBidPrice: {(Text.Of(FixedPoint.ToDecimal(this.bestBidPrice, BestBidPriceScale)))}, BestBidSize: {(Text.Of(this.bestBidSize))}, BestOfferPrice: {(Text.Of(FixedPoint.ToDecimal(this.bestOfferPrice, BestOfferPriceScale)))}, BestOfferSize: {(Text.Of(this.bestOfferSize))} }}{(this.trailer.Length == 0 ? string.Empty : $" Trailer: {(Text.Of(this.trailer.Length))} bytes")}";

    private string DebuggerDisplay => $"[{DataValue}] [{(Text.Of(this.trackingNumber))}] [{(Text.Midnight(this.timeStamp, TimeStampNanosecondsPerUnit))}] [{(Text.Of(this.stock))}]";

    IFieldOwner? IFieldOwner.Owner
    {
        get => this.owner;
        set => this.owner = value;
    }

    FieldDescriptor[] IFieldOwner.Descriptors => Descriptors;

    void IFieldOwner.Touch() => Touch();

    string IFieldOwner.TextOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => Text.Enum(PacketMessageCode.BboQuotationMessage),
        FieldName.TrackingNumber => Text.Of(this.trackingNumber),
        FieldName.TimeStamp => Text.Midnight(this.timeStamp, TimeStampNanosecondsPerUnit),
        FieldName.Stock => Text.Of(this.stock),
        FieldName.SecurityClass => Text.Enum(this.securityClass),
        FieldName.BestBidPrice => Text.Of(FixedPoint.ToDecimal(this.bestBidPrice, BestBidPriceScale)),
        FieldName.BestBidSize => Text.Of(this.bestBidSize),
        FieldName.BestOfferPrice => Text.Of(FixedPoint.ToDecimal(this.bestOfferPrice, BestOfferPriceScale)),
        FieldName.BestOfferSize => Text.Of(this.bestOfferSize),
        _ => string.Empty,
    };

    bool IFieldOwner.IsRecognized(int field) => (FieldName)field switch
    {
        FieldName.SecurityClass => global::System.Enum.IsDefined(this.securityClass),
        _ => true,
    };

    bool IFieldOwner.IsPresent(int field) => true;

    bool IFieldOwner.IsParsed(int field) => this.status == ParseStatus.Ok || field < (int?)this.failed;

    bool IFieldOwner.IsReached(int field) => this.modified || this.status is ParseStatus.None or ParseStatus.Ok || field <= (int?)this.failed;

    bool IFieldOwner.HasParseLayout(int field) => false;

    int IFieldOwner.OffsetOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => FieldView.NotInMessage,
        FieldName.TrackingNumber => TrackingNumberOffset,
        FieldName.TimeStamp => TimeStampOffset,
        FieldName.Stock => StockOffset,
        FieldName.SecurityClass => SecurityClassOffset,
        FieldName.BestBidPrice => BestBidPriceOffset,
        FieldName.BestBidSize => BestBidSizeOffset,
        FieldName.BestOfferPrice => BestOfferPriceOffset,
        FieldName.BestOfferSize => BestOfferSizeOffset,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    int IFieldOwner.LengthOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => DataValueLength,
        FieldName.TrackingNumber => TrackingNumberLength,
        FieldName.TimeStamp => TimeStampLength,
        FieldName.Stock => StockLength,
        FieldName.SecurityClass => SecurityClassLength,
        FieldName.BestBidPrice => BestBidPriceLength,
        FieldName.BestBidSize => BestBidSizeLength,
        FieldName.BestOfferPrice => BestOfferPriceLength,
        FieldName.BestOfferSize => BestOfferSizeLength,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    IReadOnlyList<IGroupEntry>? IFieldOwner.EntriesOf(int field) => null;

    IMessage? IFieldOwner.MessageOf(int field) => null;

    ulong[] IFieldOwner.ChangedAgainst(IFieldOwner other) => other is BboQuotationMessage typed ? Diff(typed).ToWords() : throw new ArgumentException("The other instance is not of the same type.", nameof(other));

    void IFieldOwner.CopyField(int field, IFieldOwner from) => CopyField((FieldName)field, from as BboQuotationMessage ?? throw new ArgumentException("The other instance is not of the same type.", nameof(from)));

    ReadOnlyMemory<byte> IFieldOwner.CodeBytes(int field)
    {
        if (field == (int)FieldName.DataValue)
        {
            var bytes = new byte[DataValueLength];
            bytes[0] = unchecked((byte)(PacketMessageCode.BboQuotationMessage));
            return bytes;
        }
        return ReadOnlyMemory<byte>.Empty;
    }
}
