using System.Buffers.Binary;
using System.Diagnostics;

namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  A trading status update message; the record of the status schema
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class StatusMsg : IFileMessage, IGatewayMessage, IEquatable<StatusMsg>, IFieldOwner
{
    internal const int TsRecvOffset = 0;

    internal const int TsRecvLength = 8;

    /// <summary>
    ///  The number of nanoseconds in one unit of the wire value of <see cref="TsRecv"/>.
    /// </summary>
    public const long TsRecvNanosecondsPerUnit = 1;

    /// <summary>
    ///  The encoded value of Ts Recv that means no value.
    /// </summary>
    public const ulong TsRecvNullValue = 18446744073709551615UL;

    internal const int StatusActionOffset = TsRecvOffset + TsRecvLength;

    internal const int StatusActionLength = 2;

    internal const int StatusReasonOffset = StatusActionOffset + StatusActionLength;

    internal const int StatusReasonLength = 2;

    internal const int TradingEventOffset = StatusReasonOffset + StatusReasonLength;

    internal const int TradingEventLength = 2;

    internal const int IsTradingOffset = TradingEventOffset + TradingEventLength;

    internal const int IsTradingLength = 1;

    internal const int IsQuotingOffset = IsTradingOffset + IsTradingLength;

    internal const int IsQuotingLength = 1;

    internal const int IsShortSellRestrictedOffset = IsQuotingOffset + IsQuotingLength;

    internal const int IsShortSellRestrictedLength = 1;

    internal const int Reserved7Offset = IsShortSellRestrictedOffset + IsShortSellRestrictedLength;

    internal const int Reserved7Length = 7;

    /// <summary>
    ///  The encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public const int MessageByteLength = TsRecvLength + StatusActionLength + StatusReasonLength + TradingEventLength + IsTradingLength + IsQuotingLength + IsShortSellRestrictedLength + Reserved7Length;

    internal ulong tsRecv;

    /// <summary>
    ///  The capture-server-received timestamp expressed as the number of nanoseconds since the UNIX epoch
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="TsRecvNullValue"/>. Setting <see langword="null"/> writes <see cref="TsRecvNullValue"/>. This property has a precision of 100 nanoseconds, and <see cref="TsRecvRaw"/> holds the exact value. Setting a local time converts it to UTC, and a time of unspecified kind is treated as UTC. A time outside the range of the field is set to the nearest limit of the range. A result equal to <see cref="TsRecvNullValue"/> is moved one step to the nearest other value.
    /// </remarks>
    public DateTime? TsRecv
    {
        get => this.tsRecv == TsRecvNullValue ? (DateTime?)null : Instant.ToDateTime(this.tsRecv, TsRecvNanosecondsPerUnit);
        set
        {
            this.tsRecv = value is { } present ? Instant.ToRaw<ulong>(present, TsRecvNanosecondsPerUnit, ulong.MinValue, ulong.MaxValue, TsRecvNullValue) : TsRecvNullValue;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="TsRecv"/>, as the unscaled integer that the field holds.
    /// </summary>
    public ulong TsRecvRaw
    {
        get => this.tsRecv;
        set
        {
            this.tsRecv = value;
            Touch();
        }
    }

    internal StatusAction statusAction;

    /// <summary>
    ///  The type of status change
    /// </summary>
    public StatusAction StatusAction
    {
        get => this.statusAction;
        set
        {
            this.statusAction = value;
            Touch();
        }
    }

    internal StatusReason statusReason;

    /// <summary>
    ///  Additional details about the cause of the status change
    /// </summary>
    public StatusReason StatusReason
    {
        get => this.statusReason;
        set
        {
            this.statusReason = value;
            Touch();
        }
    }

    internal TradingEvent tradingEvent;

    /// <summary>
    ///  Further information about a status update
    /// </summary>
    public TradingEvent TradingEvent
    {
        get => this.tradingEvent;
        set
        {
            this.tradingEvent = value;
            Touch();
        }
    }

    internal IsTrading isTrading;

    /// <summary>
    ///  The state of trading in the instrument
    /// </summary>
    public IsTrading IsTrading
    {
        get => this.isTrading;
        set
        {
            this.isTrading = value;
            Touch();
        }
    }

    internal IsQuoting isQuoting;

    /// <summary>
    ///  The state of quoting in the instrument
    /// </summary>
    public IsQuoting IsQuoting
    {
        get => this.isQuoting;
        set
        {
            this.isQuoting = value;
            Touch();
        }
    }

    internal IsShortSellRestricted isShortSellRestricted;

    /// <summary>
    ///  The state of short sell restrictions for the instrument
    /// </summary>
    public IsShortSellRestricted IsShortSellRestricted
    {
        get => this.isShortSellRestricted;
        set
        {
            this.isShortSellRestricted = value;
            Touch();
        }
    }

    /// <summary>
    ///  Gets the message type.
    /// </summary>
    public RecordBodyCode DataValue => RecordBodyCode.StatusMsg;

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
    public bool IsRecognized => global::System.Enum.IsDefined(this.statusAction) && global::System.Enum.IsDefined(this.statusReason) && global::System.Enum.IsDefined(this.tradingEvent) && global::System.Enum.IsDefined(this.isTrading) && global::System.Enum.IsDefined(this.isQuoting) && global::System.Enum.IsDefined(this.isShortSellRestricted);

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

    IMessage IMessage.Copy() => new StatusMsg(this);

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
        ///  The capture-server-received timestamp expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        TsRecv,
        /// <summary>
        ///  The type of status change
        /// </summary>
        StatusAction,
        /// <summary>
        ///  Additional details about the cause of the status change
        /// </summary>
        StatusReason,
        /// <summary>
        ///  Further information about a status update
        /// </summary>
        TradingEvent,
        /// <summary>
        ///  The state of trading in the instrument
        /// </summary>
        IsTrading,
        /// <summary>
        ///  The state of quoting in the instrument
        /// </summary>
        IsQuoting,
        /// <summary>
        ///  The state of short sell restrictions for the instrument
        /// </summary>
        IsShortSellRestricted,
        /// <summary>
        ///  Reserved, 7 bytes
        /// </summary>
        Reserved7,
    }

    /// <summary>
    ///  Provides access to the fields of one instance, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public readonly struct FieldSet : IEnumerable<FieldView>
    {
        private readonly StatusMsg owner;

        internal FieldSet(StatusMsg owner)
        {
            this.owner = owner;
        }

        /// <summary>
        ///  The message type.
        /// </summary>
        public FieldView DataValue => this[FieldName.DataValue];

        /// <summary>
        ///  The capture-server-received timestamp expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public FieldView TsRecv => this[FieldName.TsRecv];

        /// <summary>
        ///  The type of status change
        /// </summary>
        public FieldView StatusAction => this[FieldName.StatusAction];

        /// <summary>
        ///  Additional details about the cause of the status change
        /// </summary>
        public FieldView StatusReason => this[FieldName.StatusReason];

        /// <summary>
        ///  Further information about a status update
        /// </summary>
        public FieldView TradingEvent => this[FieldName.TradingEvent];

        /// <summary>
        ///  The state of trading in the instrument
        /// </summary>
        public FieldView IsTrading => this[FieldName.IsTrading];

        /// <summary>
        ///  The state of quoting in the instrument
        /// </summary>
        public FieldView IsQuoting => this[FieldName.IsQuoting];

        /// <summary>
        ///  The state of short sell restrictions for the instrument
        /// </summary>
        public FieldView IsShortSellRestricted => this[FieldName.IsShortSellRestricted];

        /// <summary>
        ///  Reserved, 7 bytes
        /// </summary>
        public FieldView Reserved7 => this[FieldName.Reserved7];

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
        new(nameof(FieldName.DataValue), FieldKind.Enum, DataValueLength) { IsSigned = false },
        new(nameof(FieldName.TsRecv), FieldKind.DateTime, TsRecvLength) { Unit = "Nanoseconds", Epoch = "Unix", ByteOrder = "Little", IsSigned = false, Nullable = "Value", NullValue = TsRecvNullValue },
        new(nameof(FieldName.StatusAction), FieldKind.Enum, StatusActionLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.StatusReason), FieldKind.Enum, StatusReasonLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.TradingEvent), FieldKind.Enum, TradingEventLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.IsTrading), FieldKind.Enum, IsTradingLength),
        new(nameof(FieldName.IsQuoting), FieldKind.Enum, IsQuotingLength),
        new(nameof(FieldName.IsShortSellRestricted), FieldKind.Enum, IsShortSellRestrictedLength),
        new(nameof(FieldName.Reserved7), FieldKind.Padding, Reserved7Length),
    ];

    /// <summary>
    ///  Gets the fields in wire order, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public FieldSet Fields => new(this);

    IEnumerable<FieldView> IMessage.Fields => new FieldSet(this);

    /// <summary>
    ///  Initializes a new instance of the <see cref="StatusMsg"/> class with default values.
    /// </summary>
    public StatusMsg()
    {
        Clear();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="StatusMsg"/> class with the values, bytes, and parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public StatusMsg(StatusMsg other) : this()
    {
        CopyFrom(other);
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(StatusMsg other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.tsRecv = other.tsRecv;
        this.statusAction = other.statusAction;
        this.statusReason = other.statusReason;
        this.tradingEvent = other.tradingEvent;
        this.isTrading = other.isTrading;
        this.isQuoting = other.isQuoting;
        this.isShortSellRestricted = other.isShortSellRestricted;
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
        this.tsRecv = default;
        this.statusAction = default;
        this.statusReason = default;
        this.tradingEvent = default;
        this.isTrading = default;
        this.isQuoting = default;
        this.isShortSellRestricted = default;
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
    public static StatusMsg Parse(ReadOnlySpan<byte> data)
    {
        var instance = new StatusMsg();
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
    public static StatusMsg Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        var instance = new StatusMsg();
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
        if (Truncated(data, start, cursor, TsRecvLength, FieldName.TsRecv)) return;
        this.tsRecv = BinaryPrimitives.ReadUInt64LittleEndian(data.Slice(cursor, TsRecvLength));
        cursor += TsRecvLength;
        if (Truncated(data, start, cursor, StatusActionLength, FieldName.StatusAction)) return;
        this.statusAction = (StatusAction)BinaryPrimitives.ReadUInt16LittleEndian(data.Slice(cursor, StatusActionLength));
        cursor += StatusActionLength;
        if (Truncated(data, start, cursor, StatusReasonLength, FieldName.StatusReason)) return;
        this.statusReason = (StatusReason)BinaryPrimitives.ReadUInt16LittleEndian(data.Slice(cursor, StatusReasonLength));
        cursor += StatusReasonLength;
        if (Truncated(data, start, cursor, TradingEventLength, FieldName.TradingEvent)) return;
        this.tradingEvent = (TradingEvent)BinaryPrimitives.ReadUInt16LittleEndian(data.Slice(cursor, TradingEventLength));
        cursor += TradingEventLength;
        if (Truncated(data, start, cursor, IsTradingLength, FieldName.IsTrading)) return;
        this.isTrading = (IsTrading)data.Slice(cursor, IsTradingLength)[0];
        cursor += IsTradingLength;
        if (Truncated(data, start, cursor, IsQuotingLength, FieldName.IsQuoting)) return;
        this.isQuoting = (IsQuoting)data.Slice(cursor, IsQuotingLength)[0];
        cursor += IsQuotingLength;
        if (Truncated(data, start, cursor, IsShortSellRestrictedLength, FieldName.IsShortSellRestricted)) return;
        this.isShortSellRestricted = (IsShortSellRestricted)data.Slice(cursor, IsShortSellRestrictedLength)[0];
        cursor += IsShortSellRestrictedLength;
        if (Truncated(data, start, cursor, Reserved7Length, FieldName.Reserved7)) return;
        cursor += Reserved7Length;
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
        BinaryPrimitives.WriteUInt64LittleEndian(data.Slice(cursor, TsRecvLength), this.tsRecv);
        cursor += TsRecvLength;
        BinaryPrimitives.WriteUInt16LittleEndian(data.Slice(cursor, StatusActionLength), unchecked((ushort)(this.statusAction)));
        cursor += StatusActionLength;
        BinaryPrimitives.WriteUInt16LittleEndian(data.Slice(cursor, StatusReasonLength), unchecked((ushort)(this.statusReason)));
        cursor += StatusReasonLength;
        BinaryPrimitives.WriteUInt16LittleEndian(data.Slice(cursor, TradingEventLength), unchecked((ushort)(this.tradingEvent)));
        cursor += TradingEventLength;
        data.Slice(cursor, IsTradingLength)[0] = unchecked((byte)(this.isTrading));
        cursor += IsTradingLength;
        data.Slice(cursor, IsQuotingLength)[0] = unchecked((byte)(this.isQuoting));
        cursor += IsQuotingLength;
        data.Slice(cursor, IsShortSellRestrictedLength)[0] = unchecked((byte)(this.isShortSellRestricted));
        cursor += IsShortSellRestrictedLength;
        {
            if (this.raw.Length >= Reserved7Offset + Reserved7Length)
            {
                this.raw.AsSpan(Reserved7Offset, Reserved7Length).CopyTo(data.Slice(cursor, Reserved7Length));
            }
            else
            {
                data.Slice(cursor, Reserved7Length).Clear();
            }
            cursor += Reserved7Length;
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
        if (!global::System.Enum.IsDefined(this.statusAction))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.StatusAction), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Enum.IsDefined(this.statusReason))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.StatusReason), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Enum.IsDefined(this.tradingEvent))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.TradingEvent), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Enum.IsDefined(this.isTrading))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.IsTrading), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Enum.IsDefined(this.isQuoting))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.IsQuoting), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Enum.IsDefined(this.isShortSellRestricted))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.IsShortSellRestricted), ValidationStatus.UndeclaredValue));
        }
        return failures;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(StatusMsg? other)
    {
        if (other is null)
        {
            return false;
        }
        if (this.tsRecv != other.tsRecv) return false;
        if (this.statusAction != other.statusAction) return false;
        if (this.statusReason != other.statusReason) return false;
        if (this.tradingEvent != other.tradingEvent) return false;
        if (this.isTrading != other.isTrading) return false;
        if (this.isQuoting != other.isQuoting) return false;
        if (this.isShortSellRestricted != other.isShortSellRestricted) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is StatusMsg other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.tsRecv);
        hash.Add(this.statusAction);
        hash.Add(this.statusReason);
        hash.Add(this.tradingEvent);
        hash.Add(this.isTrading);
        hash.Add(this.isQuoting);
        hash.Add(this.isShortSellRestricted);
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
            if (Has(FieldName.TsRecv)) names.Add(nameof(FieldName.TsRecv));
            if (Has(FieldName.StatusAction)) names.Add(nameof(FieldName.StatusAction));
            if (Has(FieldName.StatusReason)) names.Add(nameof(FieldName.StatusReason));
            if (Has(FieldName.TradingEvent)) names.Add(nameof(FieldName.TradingEvent));
            if (Has(FieldName.IsTrading)) names.Add(nameof(FieldName.IsTrading));
            if (Has(FieldName.IsQuoting)) names.Add(nameof(FieldName.IsQuoting));
            if (Has(FieldName.IsShortSellRestricted)) names.Add(nameof(FieldName.IsShortSellRestricted));
            if (Has(FieldName.Reserved7)) names.Add(nameof(FieldName.Reserved7));
            return names.Count == 0 ? "None" : string.Join(", ", names);
        }
    }

    /// <summary>
    ///  Compares this instance with another and identifies the fields that differ.
    /// </summary>
    /// <returns>The fields that differ, or None if there are no differences.</returns>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Changes Diff(StatusMsg other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (this.tsRecv != other.tsRecv) diff = diff.With(FieldName.TsRecv);
        if (this.statusAction != other.statusAction) diff = diff.With(FieldName.StatusAction);
        if (this.statusReason != other.statusReason) diff = diff.With(FieldName.StatusReason);
        if (this.tradingEvent != other.tradingEvent) diff = diff.With(FieldName.TradingEvent);
        if (this.isTrading != other.isTrading) diff = diff.With(FieldName.IsTrading);
        if (this.isQuoting != other.isQuoting) diff = diff.With(FieldName.IsQuoting);
        if (this.isShortSellRestricted != other.isShortSellRestricted) diff = diff.With(FieldName.IsShortSellRestricted);
        return diff;
    }

    private void CopyField(FieldName field, StatusMsg from)
    {
        switch (field)
        {
            case FieldName.TsRecv:
                this.tsRecv = from.tsRecv;
                break;
            case FieldName.StatusAction:
                this.statusAction = from.statusAction;
                break;
            case FieldName.StatusReason:
                this.statusReason = from.statusReason;
                break;
            case FieldName.TradingEvent:
                this.tradingEvent = from.tradingEvent;
                break;
            case FieldName.IsTrading:
                this.isTrading = from.isTrading;
                break;
            case FieldName.IsQuoting:
                this.isQuoting = from.isQuoting;
                break;
            case FieldName.IsShortSellRestricted:
                this.isShortSellRestricted = from.isShortSellRestricted;
                break;
            default:
                return;
        }
        Touch();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"StatusMsg {{ TsRecv: {(Text.Instant(this.tsRecv, TsRecvNanosecondsPerUnit, TsRecvNullValue))}, StatusAction: {(Text.Enum(this.statusAction))}, StatusReason: {(Text.Enum(this.statusReason))}, TradingEvent: {(Text.Enum(this.tradingEvent))}, IsTrading: {(Text.Enum(this.isTrading))}, IsQuoting: {(Text.Enum(this.isQuoting))}, IsShortSellRestricted: {(Text.Enum(this.isShortSellRestricted))} }}{(this.trailer.Length == 0 ? string.Empty : $" Trailer: {(Text.Of(this.trailer.Length))} bytes")}";

    private string DebuggerDisplay => $"[{DataValue}] [{(Text.Instant(this.tsRecv, TsRecvNanosecondsPerUnit, TsRecvNullValue))}] [{(Text.Enum(this.statusAction))}] [{(Text.Enum(this.statusReason))}]";

    IFieldOwner? IFieldOwner.Owner
    {
        get => this.owner;
        set => this.owner = value;
    }

    FieldDescriptor[] IFieldOwner.Descriptors => Descriptors;

    void IFieldOwner.Touch() => Touch();

    string IFieldOwner.TextOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => Text.Enum(RecordBodyCode.StatusMsg),
        FieldName.TsRecv => Text.Instant(this.tsRecv, TsRecvNanosecondsPerUnit, TsRecvNullValue),
        FieldName.StatusAction => Text.Enum(this.statusAction),
        FieldName.StatusReason => Text.Enum(this.statusReason),
        FieldName.TradingEvent => Text.Enum(this.tradingEvent),
        FieldName.IsTrading => Text.Enum(this.isTrading),
        FieldName.IsQuoting => Text.Enum(this.isQuoting),
        FieldName.IsShortSellRestricted => Text.Enum(this.isShortSellRestricted),
        _ => string.Empty,
    };

    bool IFieldOwner.IsRecognized(int field) => (FieldName)field switch
    {
        FieldName.StatusAction => global::System.Enum.IsDefined(this.statusAction),
        FieldName.StatusReason => global::System.Enum.IsDefined(this.statusReason),
        FieldName.TradingEvent => global::System.Enum.IsDefined(this.tradingEvent),
        FieldName.IsTrading => global::System.Enum.IsDefined(this.isTrading),
        FieldName.IsQuoting => global::System.Enum.IsDefined(this.isQuoting),
        FieldName.IsShortSellRestricted => global::System.Enum.IsDefined(this.isShortSellRestricted),
        _ => true,
    };

    bool IFieldOwner.IsPresent(int field) => (FieldName)field switch
    {
        FieldName.TsRecv => this.tsRecv != TsRecvNullValue,
        _ => true,
    };

    bool IFieldOwner.IsParsed(int field) => this.status == ParseStatus.Ok || field < (int?)this.failed;

    bool IFieldOwner.IsReached(int field) => this.modified || this.status is ParseStatus.None or ParseStatus.Ok || field <= (int?)this.failed;

    bool IFieldOwner.HasParseLayout(int field) => false;

    int IFieldOwner.OffsetOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => FieldView.NotInMessage,
        FieldName.TsRecv => TsRecvOffset,
        FieldName.StatusAction => StatusActionOffset,
        FieldName.StatusReason => StatusReasonOffset,
        FieldName.TradingEvent => TradingEventOffset,
        FieldName.IsTrading => IsTradingOffset,
        FieldName.IsQuoting => IsQuotingOffset,
        FieldName.IsShortSellRestricted => IsShortSellRestrictedOffset,
        FieldName.Reserved7 => Reserved7Offset,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    int IFieldOwner.LengthOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => DataValueLength,
        FieldName.TsRecv => TsRecvLength,
        FieldName.StatusAction => StatusActionLength,
        FieldName.StatusReason => StatusReasonLength,
        FieldName.TradingEvent => TradingEventLength,
        FieldName.IsTrading => IsTradingLength,
        FieldName.IsQuoting => IsQuotingLength,
        FieldName.IsShortSellRestricted => IsShortSellRestrictedLength,
        FieldName.Reserved7 => Reserved7Length,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    IReadOnlyList<IGroupEntry>? IFieldOwner.EntriesOf(int field) => null;

    IMessage? IFieldOwner.MessageOf(int field) => null;

    ulong[] IFieldOwner.ChangedAgainst(IFieldOwner other) => other is StatusMsg typed ? Diff(typed).ToWords() : throw new ArgumentException("The other instance is not of the same type.", nameof(other));

    void IFieldOwner.CopyField(int field, IFieldOwner from) => CopyField((FieldName)field, from as StatusMsg ?? throw new ArgumentException("The other instance is not of the same type.", nameof(from)));

    ReadOnlyMemory<byte> IFieldOwner.CodeBytes(int field)
    {
        if (field == (int)FieldName.DataValue)
        {
            var bytes = new byte[DataValueLength];
            bytes[0] = unchecked((byte)(RecordBodyCode.StatusMsg));
            return bytes;
        }
        return ReadOnlyMemory<byte>.Empty;
    }
}
