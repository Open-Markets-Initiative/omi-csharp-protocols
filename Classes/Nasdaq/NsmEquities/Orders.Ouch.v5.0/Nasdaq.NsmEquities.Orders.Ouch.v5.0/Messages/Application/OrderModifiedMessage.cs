using System.Buffers.Binary;
using System.Diagnostics;
using static Nasdaq.NsmEquities.Orders.Ouch.v5_0.Rules;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  An Order Modified Message is sent in response to an order modify request.
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class OrderModifiedMessage : ISequencedMessage, IEquatable<OrderModifiedMessage>, IFieldOwner
{
    internal const int TimestampOffset = 0;

    internal const int TimestampLength = 8;

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
    public const ulong TimestampWireMaximum = ulong.MaxValue;

    internal const int UserRefNumOffset = TimestampOffset + TimestampLength;

    internal const int UserRefNumLength = 4;

    internal const int SideOffset = UserRefNumOffset + UserRefNumLength;

    internal const int SideLength = 1;

    internal const int QuantityOffset = SideOffset + SideLength;

    internal const int QuantityLength = 4;

    internal const int AppendageLengthOffset = QuantityOffset + QuantityLength;

    internal const int AppendageLengthLength = 2;

    /// <summary>
    ///  The encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public const int MessageByteLength = TimestampLength + UserRefNumLength + SideLength + QuantityLength;

    private const int OrderModifiedAppendageListEntryByteLength = OrderModifiedAppendage.MessageByteLength;

    internal ulong timestamp;

    /// <summary>
    ///  Expressed as nanoseconds since midnight
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

    internal uint userRefNum;

    /// <summary>
    ///  UserRefNum must be day-unique and strictly increasing for each OUCH account.
    /// </summary>
    public uint UserRefNum
    {
        get => this.userRefNum;
        set
        {
            this.userRefNum = value;
            Touch();
        }
    }

    internal Side side;

    /// <summary>
    ///  Side
    /// </summary>
    public Side Side
    {
        get => this.side;
        set
        {
            this.side = value;
            Touch();
        }
    }

    internal uint quantity;

    /// <summary>
    ///  Total number of shares. Must be greater than zero and less than 1,000,000.
    /// </summary>
    public uint Quantity
    {
        get => this.quantity;
        set
        {
            this.quantity = value;
            Touch();
        }
    }

    internal ushort? appendageLength;

    /// <summary>
    ///  The length of the remaining Optional Appendage field.
    /// </summary>
    public ushort? AppendageLength
    {
        get => this.appendageLength;
        set
        {
            this.appendageLength = value;
            Touch();
        }
    }

    internal OrderModifiedAppendageList appendage = new([]);

    /// <summary>
    ///  Order Modified Appendage
    /// </summary>
    /// <remarks>
    ///  Setting <see langword="null"/> stores the value that Clear stores.
    /// </remarks>
    public OrderModifiedAppendageList Appendage
    {
        get => this.appendage;
        set
        {
            value ??= new([]);
            this.appendage.SetOwner(null);
            this.appendage = new OrderModifiedAppendageList(value);
            this.appendage.SetOwner(this);
            Touch();
        }
    }

    /// <summary>
    ///  Gets the message type.
    /// </summary>
    public SequencedMessageCode DataValue => SequencedMessageCode.OrderModifiedMessage;

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
            position += UserRefNumLength;
            position += SideLength;
            position += QuantityLength;
            if (this.appendageLength is not null || this.appendage.Count != 0)
            {
                position += AppendageLengthLength;
            }
            foreach (var entry in this.appendage)
            {
                position += entry.EncodedByteLength;
            }
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
    public bool IsRecognized => global::System.Enum.IsDefined(this.side) && this.appendage.IsRecognized;

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
    private int appendageOffset;
    private int appendageByteLength;

    private void Touch()
    {
        this.modified = true;
        this.cache = null;
        this.owner?.Touch();
    }

    IMessage IMessage.Copy() => new OrderModifiedMessage(this);

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
        ///  Expressed as nanoseconds since midnight
        /// </summary>
        Timestamp,
        /// <summary>
        ///  UserRefNum must be day-unique and strictly increasing for each OUCH account.
        /// </summary>
        UserRefNum,
        /// <summary>
        ///  Side
        /// </summary>
        Side,
        /// <summary>
        ///  Total number of shares. Must be greater than zero and less than 1,000,000.
        /// </summary>
        Quantity,
        /// <summary>
        ///  The length of the remaining Optional Appendage field.
        /// </summary>
        AppendageLength,
        /// <summary>
        ///  Order Modified Appendage
        /// </summary>
        Appendage,
    }

    /// <summary>
    ///  Provides access to the fields of one instance, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public readonly struct FieldSet : IEnumerable<FieldView>
    {
        private readonly OrderModifiedMessage owner;

        internal FieldSet(OrderModifiedMessage owner)
        {
            this.owner = owner;
        }

        /// <summary>
        ///  The message type.
        /// </summary>
        public FieldView DataValue => this[FieldName.DataValue];

        /// <summary>
        ///  Expressed as nanoseconds since midnight
        /// </summary>
        public FieldView Timestamp => this[FieldName.Timestamp];

        /// <summary>
        ///  UserRefNum must be day-unique and strictly increasing for each OUCH account.
        /// </summary>
        public FieldView UserRefNum => this[FieldName.UserRefNum];

        /// <summary>
        ///  Side
        /// </summary>
        public FieldView Side => this[FieldName.Side];

        /// <summary>
        ///  Total number of shares. Must be greater than zero and less than 1,000,000.
        /// </summary>
        public FieldView Quantity => this[FieldName.Quantity];

        /// <summary>
        ///  The length of the remaining Optional Appendage field.
        /// </summary>
        public FieldView AppendageLength => this[FieldName.AppendageLength];

        /// <summary>
        ///  Order Modified Appendage
        /// </summary>
        public FieldView Appendage => this[FieldName.Appendage];

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
        new(nameof(FieldName.Timestamp), FieldKind.UnsignedInteger, TimestampLength) { Unit = "Nanoseconds", Epoch = "Midnight", Timezone = "UTC", ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.UserRefNum), FieldKind.UnsignedInteger, UserRefNumLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.Side), FieldKind.Enum, SideLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.Quantity), FieldKind.UnsignedInteger, QuantityLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.AppendageLength), FieldKind.UnsignedInteger, AppendageLengthLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.Appendage), FieldKind.Group, null),
    ];

    /// <summary>
    ///  Gets the fields in wire order, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public FieldSet Fields => new(this);

    IEnumerable<FieldView> IMessage.Fields => new FieldSet(this);

    /// <summary>
    ///  Initializes a new instance of the <see cref="OrderModifiedMessage"/> class with default values.
    /// </summary>
    public OrderModifiedMessage()
    {
        Clear();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="OrderModifiedMessage"/> class with the values, bytes, and parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public OrderModifiedMessage(OrderModifiedMessage other) : this()
    {
        CopyFrom(other);
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(OrderModifiedMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.timestamp = other.timestamp;
        this.userRefNum = other.userRefNum;
        this.side = other.side;
        this.quantity = other.quantity;
        this.appendageLength = other.appendageLength;
        this.appendage.SetOwner(null);
        this.appendage = new OrderModifiedAppendageList(other.appendage);
        this.appendage.SetOwner(this);
        this.appendageOffset = other.appendageOffset;
        this.appendageByteLength = other.appendageByteLength;
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
        this.userRefNum = default;
        this.side = default;
        this.quantity = default;
        this.appendageLength = null;
        this.appendage.SetOwner(null);
        this.appendage = new([]);
        this.appendage.SetOwner(this);
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
    public static OrderModifiedMessage Parse(ReadOnlySpan<byte> data)
    {
        var instance = new OrderModifiedMessage();
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
    public static OrderModifiedMessage Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        var instance = new OrderModifiedMessage();
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
        this.timestamp = BinaryPrimitives.ReadUInt64BigEndian(data.Slice(cursor, TimestampLength));
        cursor += TimestampLength;
        if (Truncated(data, start, cursor, UserRefNumLength, FieldName.UserRefNum)) return;
        this.userRefNum = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, UserRefNumLength));
        cursor += UserRefNumLength;
        if (Truncated(data, start, cursor, SideLength, FieldName.Side)) return;
        this.side = (Side)data.Slice(cursor, SideLength)[0];
        cursor += SideLength;
        if (Truncated(data, start, cursor, QuantityLength, FieldName.Quantity)) return;
        this.quantity = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, QuantityLength));
        cursor += QuantityLength;
        {
            bool OrderModifiedMessageAppendageLengthOptionalRule0Present;
            OrderModifiedMessageAppendageLengthOptionalRule0Present = cursor < data.Length;
            if (OrderModifiedMessageAppendageLengthOptionalRule0Present)
            {
                if (Truncated(data, start, cursor, AppendageLengthLength, FieldName.AppendageLength)) return;
                this.appendageLength = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(cursor, AppendageLengthLength));
                cursor += AppendageLengthLength;
            }
        }
        {
            this.appendageOffset = cursor - start;
            var groupStart = cursor;
            this.appendageByteLength = data.Length - groupStart;
            int OrderModifiedAppendageCountRule0End;
            global::System.Int128 OrderModifiedAppendageCountRule0RawEnd;
            ushort orderModifiedMessageAppendageLengthValue = this.appendageLength.GetValueOrDefault();
            var entries = new List<OrderModifiedAppendage>();
            OrderModifiedAppendageCountRule0RawEnd = checked((checked((global::System.Int128)(orderModifiedMessageAppendageLengthValue)) + checked((global::System.Int128)(cursor))));
            if (OrderModifiedAppendageCountRule0RawEnd < cursor || OrderModifiedAppendageCountRule0RawEnd > data.Length) { Fail(ParseStatus.Truncated, FieldName.Appendage, groupStart - start, data[start..]); return; }
            OrderModifiedAppendageCountRule0End = (int)OrderModifiedAppendageCountRule0RawEnd;
            var orderModifiedAppendageOccurrenceBytes = data[..OrderModifiedAppendageCountRule0End];
            while (cursor < OrderModifiedAppendageCountRule0End)
            {
                int orderModifiedAppendageEntryStart = cursor;
                var entry = new OrderModifiedAppendage();
                var contract = (IGroupEntry)entry;
                contract.ParseFrom(orderModifiedAppendageOccurrenceBytes, ref cursor);
                if (contract.Status != ParseStatus.Ok)
                {
                    this.appendage = new OrderModifiedAppendageList(entries, contract.Status, entry);
                    this.appendage.SetOwner(this);
                    Fail(contract.Status, FieldName.Appendage, groupStart - start, data[start..]);
                    return;
                }
                entries.Add(entry);
                if (cursor <= orderModifiedAppendageEntryStart || cursor > OrderModifiedAppendageCountRule0End) { Fail(ParseStatus.Truncated, FieldName.Appendage, groupStart - start, data[start..]); return; }
            }
            if (cursor != OrderModifiedAppendageCountRule0End) { Fail(ParseStatus.BadCount, FieldName.Appendage, groupStart - start, data[start..]); return; }
            this.appendage = new OrderModifiedAppendageList(entries, ParseStatus.Ok, null);
            this.appendage.SetOwner(this);
            this.appendageByteLength = cursor - groupStart;
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
        var position = offset;
        position += TimestampLength;
        position += UserRefNumLength;
        position += SideLength;
        position += QuantityLength;
        var orderModifiedMessageAppendageLengthMeasureStart = position;
        if (this.appendageLength is not null || this.appendage.Count != 0)
        {
            position += AppendageLengthLength;
        }
        var orderModifiedMessageAppendageLengthMeasureEnd = position;
        var orderModifiedAppendageMeasureStart = position;
        foreach (var entry in this.appendage)
        {
            position += entry.EncodedByteLength;
        }
        var orderModifiedAppendageMeasureEnd = position;
        if (this.appendageLength is not null || this.appendage.Count != 0)
        {
            global::System.Int128 measure = orderModifiedAppendageMeasureEnd;
            global::System.Int128 orderModifiedMessageAppendageLengthStampedValue;
            try
            {
                orderModifiedMessageAppendageLengthStampedValue = checked((measure - checked((((global::System.Int128)(RuleNumberZero)) + checked((global::System.Int128)(orderModifiedAppendageMeasureStart))))));
            }
            catch (global::System.OverflowException)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.AppendageLength), EncodeStatus.RuleMismatch);
                return false;
            }
            if (orderModifiedMessageAppendageLengthStampedValue > ushort.MaxValue)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.AppendageLength), EncodeStatus.TooLong);
                return false;
            }
            if (orderModifiedMessageAppendageLengthStampedValue < ushort.MinValue)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.AppendageLength), EncodeStatus.DoesNotFit);
                return false;
            }
            this.appendageLength = checked((ushort)(orderModifiedMessageAppendageLengthStampedValue));
        }
        BinaryPrimitives.WriteUInt64BigEndian(data.Slice(cursor, TimestampLength), this.timestamp);
        cursor += TimestampLength;
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, UserRefNumLength), this.userRefNum);
        cursor += UserRefNumLength;
        data.Slice(cursor, SideLength)[0] = unchecked((byte)(this.side));
        cursor += SideLength;
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, QuantityLength), this.quantity);
        cursor += QuantityLength;
        if (this.appendageLength is not null || this.appendage.Count != 0)
        {
            BinaryPrimitives.WriteUInt16BigEndian(data.Slice(cursor, AppendageLengthLength), this.appendageLength.GetValueOrDefault());
            cursor += AppendageLengthLength;
        }
        {
            this.appendageOffset = cursor - offset;
            var groupStart = cursor;
            for (var index = 0; index < this.appendage.Count; index++)
            {
                if (!((IGroupEntry)this.appendage[index]).Write(data, cursor, out cursor, out var memberFailure))
                {
                    failure = memberFailure.Within(new FieldView(this, (int)FieldName.Appendage), index);
                    return false;
                }
            }
            this.appendageByteLength = cursor - groupStart;
        }
        {
            bool agrees;
            agrees = (orderModifiedMessageAppendageLengthMeasureStart < offset + EncodedByteLength) == (this.appendageLength is not null || this.appendage.Count != 0);
            if (!agrees)
            {
                failure = new EncodeFailure(EncodeStatus.RuleMismatch);
                return false;
            }
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
        if (!global::System.Enum.IsDefined(this.side))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.Side), ValidationStatus.UndeclaredValue));
        }
        for (var index = 0; index < this.appendage.Count; index++)
        {
            foreach (var failure in ((IGroupEntry)this.appendage[index]).Validate())
            {
                failures.Add(failure.Within(new FieldView(this, (int)FieldName.Appendage), index));
            }
        }
        return failures;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(OrderModifiedMessage? other)
    {
        if (other is null)
        {
            return false;
        }
        if (this.timestamp != other.timestamp) return false;
        if (this.userRefNum != other.userRefNum) return false;
        if (this.side != other.side) return false;
        if (this.quantity != other.quantity) return false;
        if (!this.appendage.Equals(other.appendage)) return false;
        if ((this.appendageLength is not null || this.appendage.Count != 0) != (other.appendageLength is not null || other.appendage.Count != 0)) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is OrderModifiedMessage other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.timestamp);
        hash.Add(this.userRefNum);
        hash.Add(this.side);
        hash.Add(this.quantity);
        hash.Add(this.appendage);
        hash.Add(this.appendageLength is not null || this.appendage.Count != 0);
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
        private const int FieldCount = 7;

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
            if (Has(FieldName.UserRefNum)) names.Add(nameof(FieldName.UserRefNum));
            if (Has(FieldName.Side)) names.Add(nameof(FieldName.Side));
            if (Has(FieldName.Quantity)) names.Add(nameof(FieldName.Quantity));
            if (Has(FieldName.AppendageLength)) names.Add(nameof(FieldName.AppendageLength));
            if (Has(FieldName.Appendage)) names.Add(nameof(FieldName.Appendage));
            return names.Count == 0 ? "None" : string.Join(", ", names);
        }
    }

    /// <summary>
    ///  Compares this instance with another and identifies the fields that differ.
    /// </summary>
    /// <returns>The fields that differ, or None if there are no differences.</returns>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Changes Diff(OrderModifiedMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (this.timestamp != other.timestamp) diff = diff.With(FieldName.Timestamp);
        if (this.userRefNum != other.userRefNum) diff = diff.With(FieldName.UserRefNum);
        if (this.side != other.side) diff = diff.With(FieldName.Side);
        if (this.quantity != other.quantity) diff = diff.With(FieldName.Quantity);
        if (!this.appendage.Equals(other.appendage)) diff = diff.With(FieldName.Appendage);
        if ((this.appendageLength is not null || this.appendage.Count != 0) != (other.appendageLength is not null || other.appendage.Count != 0)) diff = diff.With(FieldName.AppendageLength);
        return diff;
    }

    private void CopyField(FieldName field, OrderModifiedMessage from)
    {
        switch (field)
        {
            case FieldName.Timestamp:
                this.timestamp = from.timestamp;
                break;
            case FieldName.UserRefNum:
                this.userRefNum = from.userRefNum;
                break;
            case FieldName.Side:
                this.side = from.side;
                break;
            case FieldName.Quantity:
                this.quantity = from.quantity;
                break;
            case FieldName.AppendageLength:
                this.appendageLength = from.appendageLength;
                break;
            case FieldName.Appendage:
                this.appendage.SetOwner(null);
                this.appendage = new OrderModifiedAppendageList(from.appendage);
                this.appendage.SetOwner(this);
                break;
            default:
                return;
        }
        Touch();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"OrderModifiedMessage {{ Timestamp: {(Text.Midnight(this.timestamp, TimestampNanosecondsPerUnit))}, UserRefNum: {(Text.Of(this.userRefNum))}, Side: {(Text.Enum(this.side))}, Quantity: {(Text.Of(this.quantity))}, AppendageLength: {(Text.Of(this.appendageLength))}, Appendage: {(Text.Entries(this.appendage))} }}{(this.trailer.Length == 0 ? string.Empty : $" Trailer: {(Text.Of(this.trailer.Length))} bytes")}";

    private string DebuggerDisplay => $"[{DataValue}] [{(Text.Midnight(this.timestamp, TimestampNanosecondsPerUnit))}] [{(Text.Of(this.userRefNum))}] [{(Text.Enum(this.side))}]";

    IFieldOwner? IFieldOwner.Owner
    {
        get => this.owner;
        set => this.owner = value;
    }

    FieldDescriptor[] IFieldOwner.Descriptors => Descriptors;

    void IFieldOwner.Touch() => Touch();

    string IFieldOwner.TextOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => Text.Enum(SequencedMessageCode.OrderModifiedMessage),
        FieldName.Timestamp => Text.Midnight(this.timestamp, TimestampNanosecondsPerUnit),
        FieldName.UserRefNum => Text.Of(this.userRefNum),
        FieldName.Side => Text.Enum(this.side),
        FieldName.Quantity => Text.Of(this.quantity),
        FieldName.AppendageLength => Text.Of(this.appendageLength),
        FieldName.Appendage => Text.Entries(this.appendage),
        _ => string.Empty,
    };

    bool IFieldOwner.IsRecognized(int field) => (FieldName)field switch
    {
        FieldName.Side => global::System.Enum.IsDefined(this.side),
        FieldName.Appendage => this.appendage.IsRecognized,
        _ => true,
    };

    bool IFieldOwner.IsPresent(int field) => (FieldName)field switch
    {
        FieldName.AppendageLength => this.appendageLength is not null || this.appendage.Count != 0,
        FieldName.Appendage => this.appendage.Count != 0,
        _ => true,
    };

    bool IFieldOwner.IsParsed(int field) => this.status == ParseStatus.Ok || field < (int?)this.failed;

    bool IFieldOwner.IsReached(int field) => this.modified || this.status is ParseStatus.None or ParseStatus.Ok || field <= (int?)this.failed;

    bool IFieldOwner.HasParseLayout(int field) => (FieldName)field switch
    {
        FieldName.Appendage => true,
        _ => false,
    };

    int IFieldOwner.OffsetOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => FieldView.NotInMessage,
        FieldName.Timestamp => TimestampOffset,
        FieldName.UserRefNum => UserRefNumOffset,
        FieldName.Side => SideOffset,
        FieldName.Quantity => QuantityOffset,
        FieldName.AppendageLength => AppendageLengthOffset,
        FieldName.Appendage => this.appendageOffset,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    int IFieldOwner.LengthOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => DataValueLength,
        FieldName.Timestamp => TimestampLength,
        FieldName.UserRefNum => UserRefNumLength,
        FieldName.Side => SideLength,
        FieldName.Quantity => QuantityLength,
        FieldName.AppendageLength => this.appendageLength is not null || this.appendage.Count != 0 ? AppendageLengthLength : 0,
        FieldName.Appendage => this.appendageByteLength,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    IReadOnlyList<IGroupEntry>? IFieldOwner.EntriesOf(int field) => (FieldName)field switch
    {
        FieldName.Appendage => this.appendage,
        _ => null,
    };

    IMessage? IFieldOwner.MessageOf(int field) => null;

    ulong[] IFieldOwner.ChangedAgainst(IFieldOwner other) => other is OrderModifiedMessage typed ? Diff(typed).ToWords() : throw new ArgumentException("The other instance is not of the same type.", nameof(other));

    void IFieldOwner.CopyField(int field, IFieldOwner from) => CopyField((FieldName)field, from as OrderModifiedMessage ?? throw new ArgumentException("The other instance is not of the same type.", nameof(from)));

    ReadOnlyMemory<byte> IFieldOwner.CodeBytes(int field)
    {
        if (field == (int)FieldName.DataValue)
        {
            var bytes = new byte[DataValueLength];
            bytes[0] = unchecked((byte)(SequencedMessageCode.OrderModifiedMessage));
            return bytes;
        }
        return ReadOnlyMemory<byte>.Empty;
    }
}
