using System.Buffers.Binary;
using System.Diagnostics;

namespace Iex.IexEquities.Tops.IexTp.v1_66;

/// <summary>
///  IEX disseminates a full pre-market spin of Security Directory Messages for all IEX-listed securities. After the pre-market spin, IEX will use the Security Directory Message to relay changes for an individual security.
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class SecurityDirectoryMessage : IPacketMessage, IEquatable<SecurityDirectoryMessage>, IFieldOwner
{
    internal const int SecurityDirectoryFlagsOffset = 0;

    internal const int SecurityDirectoryFlagsLength = 1;

    internal const int TimestampOffset = SecurityDirectoryFlagsOffset + SecurityDirectoryFlagsLength;

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

    internal const int RoundLotSizeOffset = SymbolOffset + SymbolLength;

    internal const int RoundLotSizeLength = 4;

    internal const int AdjustedPocPriceOffset = RoundLotSizeOffset + RoundLotSizeLength;

    internal const int AdjustedPocPriceLength = 8;

    /// <summary>
    ///  The number of decimal places in Adjusted Poc Price.
    /// </summary>
    public const byte AdjustedPocPriceScale = 4;

    /// <summary>
    ///  The factor by which <see cref="AdjustedPocPrice"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal AdjustedPocPriceFactor = 10000m;

    internal const int LuldTierOffset = AdjustedPocPriceOffset + AdjustedPocPriceLength;

    internal const int LuldTierLength = 1;

    /// <summary>
    ///  The encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public const int MessageByteLength = SecurityDirectoryFlagsLength + TimestampLength + SymbolLength + RoundLotSizeLength + AdjustedPocPriceLength + LuldTierLength;

    internal SecurityDirectoryFlags securityDirectoryFlags;

    /// <summary>
    ///  Security Directory Flags
    /// </summary>
    public SecurityDirectoryFlags SecurityDirectoryFlags
    {
        get => this.securityDirectoryFlags;
        set
        {
            this.securityDirectoryFlags = value;
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

    internal uint roundLotSize;

    /// <summary>
    ///  Number of shares that represent a round lot
    /// </summary>
    public uint RoundLotSize
    {
        get => this.roundLotSize;
        set
        {
            this.roundLotSize = value;
            Touch();
        }
    }

    internal long adjustedPocPrice;

    /// <summary>
    ///  Corporate action adjusted previous official closing price
    /// </summary>
    /// <remarks>
    ///  A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range.
    /// </remarks>
    public decimal AdjustedPocPrice
    {
        get => FixedPoint.ToDecimal(this.adjustedPocPrice, AdjustedPocPriceScale);
        set
        {
            this.adjustedPocPrice = FixedPoint.ToRaw<long>(value, AdjustedPocPriceFactor, long.MinValue, long.MaxValue);
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="AdjustedPocPrice"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long AdjustedPocPriceRaw
    {
        get => this.adjustedPocPrice;
        set
        {
            this.adjustedPocPrice = value;
            Touch();
        }
    }

    internal LuldTier luldTier;

    /// <summary>
    ///  Indicates which Limit Up-Limit Down price band calculation parameter is to be used
    /// </summary>
    public LuldTier LuldTier
    {
        get => this.luldTier;
        set
        {
            this.luldTier = value;
            Touch();
        }
    }

    /// <summary>
    ///  Gets the message type.
    /// </summary>
    public PacketMessageCode DataValue => PacketMessageCode.SecurityDirectoryMessage;

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
    public bool IsRecognized => global::System.Enum.IsDefined(this.luldTier);

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

    IMessage IMessage.Copy() => new SecurityDirectoryMessage(this);

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
        ///  Security Directory Flags
        /// </summary>
        SecurityDirectoryFlags,
        /// <summary>
        ///  Time stamp of the system event
        /// </summary>
        Timestamp,
        /// <summary>
        ///  Security identifier
        /// </summary>
        Symbol,
        /// <summary>
        ///  Number of shares that represent a round lot
        /// </summary>
        RoundLotSize,
        /// <summary>
        ///  Corporate action adjusted previous official closing price
        /// </summary>
        AdjustedPocPrice,
        /// <summary>
        ///  Indicates which Limit Up-Limit Down price band calculation parameter is to be used
        /// </summary>
        LuldTier,
    }

    /// <summary>
    ///  Provides access to the fields of one instance, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public readonly struct FieldSet : IEnumerable<FieldView>
    {
        private readonly SecurityDirectoryMessage owner;

        internal FieldSet(SecurityDirectoryMessage owner)
        {
            this.owner = owner;
        }

        /// <summary>
        ///  The message type.
        /// </summary>
        public FieldView DataValue => this[FieldName.DataValue];

        /// <summary>
        ///  Security Directory Flags
        /// </summary>
        public FieldView SecurityDirectoryFlags => this[FieldName.SecurityDirectoryFlags];

        /// <summary>
        ///  Time stamp of the system event
        /// </summary>
        public FieldView Timestamp => this[FieldName.Timestamp];

        /// <summary>
        ///  Security identifier
        /// </summary>
        public FieldView Symbol => this[FieldName.Symbol];

        /// <summary>
        ///  Number of shares that represent a round lot
        /// </summary>
        public FieldView RoundLotSize => this[FieldName.RoundLotSize];

        /// <summary>
        ///  Corporate action adjusted previous official closing price
        /// </summary>
        public FieldView AdjustedPocPrice => this[FieldName.AdjustedPocPrice];

        /// <summary>
        ///  Indicates which Limit Up-Limit Down price band calculation parameter is to be used
        /// </summary>
        public FieldView LuldTier => this[FieldName.LuldTier];

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
        new(nameof(FieldName.SecurityDirectoryFlags), FieldKind.Bitfield, SecurityDirectoryFlagsLength) { IsSigned = false, Bits = [(nameof(global::Iex.IexEquities.Tops.IexTp.v1_66.SecurityDirectoryFlags.Etp), global::Iex.IexEquities.Tops.IexTp.v1_66.SecurityDirectoryFlags.EtpShift, global::Iex.IexEquities.Tops.IexTp.v1_66.SecurityDirectoryFlags.EtpWidth), (nameof(global::Iex.IexEquities.Tops.IexTp.v1_66.SecurityDirectoryFlags.WhenIssued), global::Iex.IexEquities.Tops.IexTp.v1_66.SecurityDirectoryFlags.WhenIssuedShift, global::Iex.IexEquities.Tops.IexTp.v1_66.SecurityDirectoryFlags.WhenIssuedWidth), (nameof(global::Iex.IexEquities.Tops.IexTp.v1_66.SecurityDirectoryFlags.TestSecurity), global::Iex.IexEquities.Tops.IexTp.v1_66.SecurityDirectoryFlags.TestSecurityShift, global::Iex.IexEquities.Tops.IexTp.v1_66.SecurityDirectoryFlags.TestSecurityWidth)] },
        new(nameof(FieldName.Timestamp), FieldKind.DateTime, TimestampLength) { Unit = "Nanoseconds", Epoch = "Unix", ByteOrder = "Little", IsSigned = true },
        new(nameof(FieldName.Symbol), FieldKind.String, SymbolLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.RoundLotSize), FieldKind.UnsignedInteger, RoundLotSizeLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.AdjustedPocPrice), FieldKind.Decimal, AdjustedPocPriceLength) { Scale = AdjustedPocPriceScale, ByteOrder = "Little", IsSigned = true },
        new(nameof(FieldName.LuldTier), FieldKind.Enum, LuldTierLength) { IsSigned = false },
    ];

    /// <summary>
    ///  Gets the fields in wire order, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public FieldSet Fields => new(this);

    IEnumerable<FieldView> IMessage.Fields => new FieldSet(this);

    /// <summary>
    ///  Initializes a new instance of the <see cref="SecurityDirectoryMessage"/> class with default values.
    /// </summary>
    public SecurityDirectoryMessage()
    {
        Clear();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="SecurityDirectoryMessage"/> class with the values, bytes, and parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public SecurityDirectoryMessage(SecurityDirectoryMessage other) : this()
    {
        CopyFrom(other);
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(SecurityDirectoryMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.securityDirectoryFlags = other.securityDirectoryFlags;
        this.timestamp = other.timestamp;
        this.symbol = other.symbol;
        this.roundLotSize = other.roundLotSize;
        this.adjustedPocPrice = other.adjustedPocPrice;
        this.luldTier = other.luldTier;
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
        this.securityDirectoryFlags = default;
        this.timestamp = default;
        this.symbol = string.Empty;
        this.roundLotSize = default;
        this.adjustedPocPrice = default;
        this.luldTier = default;
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
    public static SecurityDirectoryMessage Parse(ReadOnlySpan<byte> data)
    {
        var instance = new SecurityDirectoryMessage();
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
    public static SecurityDirectoryMessage Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        var instance = new SecurityDirectoryMessage();
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
        if (Truncated(data, start, cursor, SecurityDirectoryFlagsLength, FieldName.SecurityDirectoryFlags)) return;
        this.securityDirectoryFlags = new SecurityDirectoryFlags(data.Slice(cursor, SecurityDirectoryFlagsLength)[0]);
        cursor += SecurityDirectoryFlagsLength;
        if (Truncated(data, start, cursor, TimestampLength, FieldName.Timestamp)) return;
        this.timestamp = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(cursor, TimestampLength));
        cursor += TimestampLength;
        if (Truncated(data, start, cursor, SymbolLength, FieldName.Symbol)) return;
        this.symbol = TextEncoding.Ascii.ReadLeftJustified(data.Slice(cursor, SymbolLength), SymbolFill);
        cursor += SymbolLength;
        if (Truncated(data, start, cursor, RoundLotSizeLength, FieldName.RoundLotSize)) return;
        this.roundLotSize = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(cursor, RoundLotSizeLength));
        cursor += RoundLotSizeLength;
        if (Truncated(data, start, cursor, AdjustedPocPriceLength, FieldName.AdjustedPocPrice)) return;
        this.adjustedPocPrice = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(cursor, AdjustedPocPriceLength));
        cursor += AdjustedPocPriceLength;
        if (Truncated(data, start, cursor, LuldTierLength, FieldName.LuldTier)) return;
        this.luldTier = (LuldTier)data.Slice(cursor, LuldTierLength)[0];
        cursor += LuldTierLength;
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
        data.Slice(cursor, SecurityDirectoryFlagsLength)[0] = this.securityDirectoryFlags.Value;
        cursor += SecurityDirectoryFlagsLength;
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(cursor, TimestampLength), this.timestamp);
        cursor += TimestampLength;
        TextEncoding.Ascii.WriteLeftJustified(data.Slice(cursor, SymbolLength), TextEncoding.Ascii.Fit(this.symbol, SymbolLength), SymbolFill);
        cursor += SymbolLength;
        BinaryPrimitives.WriteUInt32LittleEndian(data.Slice(cursor, RoundLotSizeLength), this.roundLotSize);
        cursor += RoundLotSizeLength;
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(cursor, AdjustedPocPriceLength), this.adjustedPocPrice);
        cursor += AdjustedPocPriceLength;
        data.Slice(cursor, LuldTierLength)[0] = unchecked((byte)(this.luldTier));
        cursor += LuldTierLength;
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
        if (!global::System.Enum.IsDefined(this.luldTier))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.LuldTier), ValidationStatus.UndeclaredValue));
        }
        return failures;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(SecurityDirectoryMessage? other)
    {
        if (other is null)
        {
            return false;
        }
        if (this.securityDirectoryFlags != other.securityDirectoryFlags) return false;
        if (this.timestamp != other.timestamp) return false;
        if (this.symbol != other.symbol) return false;
        if (this.roundLotSize != other.roundLotSize) return false;
        if (this.adjustedPocPrice != other.adjustedPocPrice) return false;
        if (this.luldTier != other.luldTier) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is SecurityDirectoryMessage other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.securityDirectoryFlags);
        hash.Add(this.timestamp);
        hash.Add(this.symbol);
        hash.Add(this.roundLotSize);
        hash.Add(this.adjustedPocPrice);
        hash.Add(this.luldTier);
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
            if (Has(FieldName.SecurityDirectoryFlags)) names.Add(nameof(FieldName.SecurityDirectoryFlags));
            if (Has(FieldName.Timestamp)) names.Add(nameof(FieldName.Timestamp));
            if (Has(FieldName.Symbol)) names.Add(nameof(FieldName.Symbol));
            if (Has(FieldName.RoundLotSize)) names.Add(nameof(FieldName.RoundLotSize));
            if (Has(FieldName.AdjustedPocPrice)) names.Add(nameof(FieldName.AdjustedPocPrice));
            if (Has(FieldName.LuldTier)) names.Add(nameof(FieldName.LuldTier));
            return names.Count == 0 ? "None" : string.Join(", ", names);
        }
    }

    /// <summary>
    ///  Compares this instance with another and identifies the fields that differ.
    /// </summary>
    /// <returns>The fields that differ, or None if there are no differences.</returns>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Changes Diff(SecurityDirectoryMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (this.securityDirectoryFlags != other.securityDirectoryFlags) diff = diff.With(FieldName.SecurityDirectoryFlags);
        if (this.timestamp != other.timestamp) diff = diff.With(FieldName.Timestamp);
        if (this.symbol != other.symbol) diff = diff.With(FieldName.Symbol);
        if (this.roundLotSize != other.roundLotSize) diff = diff.With(FieldName.RoundLotSize);
        if (this.adjustedPocPrice != other.adjustedPocPrice) diff = diff.With(FieldName.AdjustedPocPrice);
        if (this.luldTier != other.luldTier) diff = diff.With(FieldName.LuldTier);
        return diff;
    }

    private void CopyField(FieldName field, SecurityDirectoryMessage from)
    {
        switch (field)
        {
            case FieldName.SecurityDirectoryFlags:
                this.securityDirectoryFlags = from.securityDirectoryFlags;
                break;
            case FieldName.Timestamp:
                this.timestamp = from.timestamp;
                break;
            case FieldName.Symbol:
                this.symbol = from.symbol;
                break;
            case FieldName.RoundLotSize:
                this.roundLotSize = from.roundLotSize;
                break;
            case FieldName.AdjustedPocPrice:
                this.adjustedPocPrice = from.adjustedPocPrice;
                break;
            case FieldName.LuldTier:
                this.luldTier = from.luldTier;
                break;
            default:
                return;
        }
        Touch();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"SecurityDirectoryMessage {{ SecurityDirectoryFlags: {(Text.Value(this.securityDirectoryFlags))}, Timestamp: {(Text.Instant(this.timestamp, TimestampNanosecondsPerUnit))}, Symbol: {(Text.Of(this.symbol))}, RoundLotSize: {(Text.Of(this.roundLotSize))}, AdjustedPocPrice: {(Text.Of(FixedPoint.ToDecimal(this.adjustedPocPrice, AdjustedPocPriceScale)))}, LuldTier: {(Text.Enum(this.luldTier))} }}{(this.trailer.Length == 0 ? string.Empty : $" Trailer: {(Text.Of(this.trailer.Length))} bytes")}";

    private string DebuggerDisplay => $"[{DataValue}] [{(Text.Value(this.securityDirectoryFlags))}] [{(Text.Instant(this.timestamp, TimestampNanosecondsPerUnit))}] [{(Text.Of(this.symbol))}]";

    IFieldOwner? IFieldOwner.Owner
    {
        get => this.owner;
        set => this.owner = value;
    }

    FieldDescriptor[] IFieldOwner.Descriptors => Descriptors;

    void IFieldOwner.Touch() => Touch();

    string IFieldOwner.TextOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => Text.Enum(PacketMessageCode.SecurityDirectoryMessage),
        FieldName.SecurityDirectoryFlags => Text.Value(this.securityDirectoryFlags),
        FieldName.Timestamp => Text.Instant(this.timestamp, TimestampNanosecondsPerUnit),
        FieldName.Symbol => Text.Of(this.symbol),
        FieldName.RoundLotSize => Text.Of(this.roundLotSize),
        FieldName.AdjustedPocPrice => Text.Of(FixedPoint.ToDecimal(this.adjustedPocPrice, AdjustedPocPriceScale)),
        FieldName.LuldTier => Text.Enum(this.luldTier),
        _ => string.Empty,
    };

    bool IFieldOwner.IsRecognized(int field) => (FieldName)field switch
    {
        FieldName.LuldTier => global::System.Enum.IsDefined(this.luldTier),
        _ => true,
    };

    bool IFieldOwner.IsPresent(int field) => true;

    bool IFieldOwner.IsParsed(int field) => this.status == ParseStatus.Ok || field < (int?)this.failed;

    bool IFieldOwner.IsReached(int field) => this.modified || this.status is ParseStatus.None or ParseStatus.Ok || field <= (int?)this.failed;

    bool IFieldOwner.HasParseLayout(int field) => false;

    int IFieldOwner.OffsetOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => FieldView.NotInMessage,
        FieldName.SecurityDirectoryFlags => SecurityDirectoryFlagsOffset,
        FieldName.Timestamp => TimestampOffset,
        FieldName.Symbol => SymbolOffset,
        FieldName.RoundLotSize => RoundLotSizeOffset,
        FieldName.AdjustedPocPrice => AdjustedPocPriceOffset,
        FieldName.LuldTier => LuldTierOffset,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    int IFieldOwner.LengthOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => DataValueLength,
        FieldName.SecurityDirectoryFlags => SecurityDirectoryFlagsLength,
        FieldName.Timestamp => TimestampLength,
        FieldName.Symbol => SymbolLength,
        FieldName.RoundLotSize => RoundLotSizeLength,
        FieldName.AdjustedPocPrice => AdjustedPocPriceLength,
        FieldName.LuldTier => LuldTierLength,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    IReadOnlyList<IGroupEntry>? IFieldOwner.EntriesOf(int field) => null;

    IMessage? IFieldOwner.MessageOf(int field) => null;

    ulong[] IFieldOwner.ChangedAgainst(IFieldOwner other) => other is SecurityDirectoryMessage typed ? Diff(typed).ToWords() : throw new ArgumentException("The other instance is not of the same type.", nameof(other));

    void IFieldOwner.CopyField(int field, IFieldOwner from) => CopyField((FieldName)field, from as SecurityDirectoryMessage ?? throw new ArgumentException("The other instance is not of the same type.", nameof(from)));

    ReadOnlyMemory<byte> IFieldOwner.CodeBytes(int field)
    {
        if (field == (int)FieldName.DataValue)
        {
            var bytes = new byte[DataValueLength];
            bytes[0] = unchecked((byte)(PacketMessageCode.SecurityDirectoryMessage));
            return bytes;
        }
        return ReadOnlyMemory<byte>.Empty;
    }
}
