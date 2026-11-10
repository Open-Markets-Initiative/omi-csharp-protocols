using System.Buffers.Binary;
using System.Diagnostics;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

/// <summary>
///  Informs data recipients what the daily MWCB breach points are set to for the current trading day.
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class MwcbDeclineLevelMessage : ISequencedMessage, IPacketMessage, IFileMessage, IEquatable<MwcbDeclineLevelMessage>, IFieldOwner
{
    internal const int StockLocateOffset = 0;

    internal const int StockLocateLength = 2;

    internal const int TrackingNumberOffset = StockLocateOffset + StockLocateLength;

    internal const int TrackingNumberLength = 2;

    internal const int TimestampOffset = TrackingNumberOffset + TrackingNumberLength;

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

    internal const int Level1Offset = TimestampOffset + TimestampLength;

    internal const int Level1Length = 8;

    /// <summary>
    ///  The number of decimal places in Level 1.
    /// </summary>
    public const byte Level1Scale = 8;

    /// <summary>
    ///  The factor by which <see cref="Level1"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal Level1Factor = 100000000m;

    internal const int Level2Offset = Level1Offset + Level1Length;

    internal const int Level2Length = 8;

    /// <summary>
    ///  The number of decimal places in Level 2.
    /// </summary>
    public const byte Level2Scale = 8;

    /// <summary>
    ///  The factor by which <see cref="Level2"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal Level2Factor = 100000000m;

    internal const int Level3Offset = Level2Offset + Level2Length;

    internal const int Level3Length = 8;

    /// <summary>
    ///  The number of decimal places in Level 3.
    /// </summary>
    public const byte Level3Scale = 8;

    /// <summary>
    ///  The factor by which <see cref="Level3"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal Level3Factor = 100000000m;

    /// <summary>
    ///  The encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public const int MessageByteLength = StockLocateLength + TrackingNumberLength + TimestampLength + Level1Length + Level2Length + Level3Length;

    internal ushort stockLocate;

    /// <summary>
    ///  Locate Code uniquely assigned to the security symbol for the day
    /// </summary>
    public ushort StockLocate
    {
        get => this.stockLocate;
        set
        {
            this.stockLocate = value;
            Touch();
        }
    }

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

    internal ulong timestamp;

    /// <summary>
    ///  Nanoseconds since midnight
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

    internal ulong level1;

    /// <summary>
    ///  Denotes the MWCB Level 1 Value.
    /// </summary>
    /// <remarks>
    ///  A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range.
    /// </remarks>
    public decimal Level1
    {
        get => FixedPoint.ToDecimal(this.level1, Level1Scale);
        set
        {
            this.level1 = FixedPoint.ToRaw<ulong>(value, Level1Factor, ulong.MinValue, ulong.MaxValue);
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="Level1"/>, as the unscaled integer that the field holds.
    /// </summary>
    public ulong Level1Raw
    {
        get => this.level1;
        set
        {
            this.level1 = value;
            Touch();
        }
    }

    internal ulong level2;

    /// <summary>
    ///  Denotes the MWCB Level 2 Value.
    /// </summary>
    /// <remarks>
    ///  A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range.
    /// </remarks>
    public decimal Level2
    {
        get => FixedPoint.ToDecimal(this.level2, Level2Scale);
        set
        {
            this.level2 = FixedPoint.ToRaw<ulong>(value, Level2Factor, ulong.MinValue, ulong.MaxValue);
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="Level2"/>, as the unscaled integer that the field holds.
    /// </summary>
    public ulong Level2Raw
    {
        get => this.level2;
        set
        {
            this.level2 = value;
            Touch();
        }
    }

    internal ulong level3;

    /// <summary>
    ///  Denotes the MWCB Level 3 Value.
    /// </summary>
    /// <remarks>
    ///  A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range.
    /// </remarks>
    public decimal Level3
    {
        get => FixedPoint.ToDecimal(this.level3, Level3Scale);
        set
        {
            this.level3 = FixedPoint.ToRaw<ulong>(value, Level3Factor, ulong.MinValue, ulong.MaxValue);
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="Level3"/>, as the unscaled integer that the field holds.
    /// </summary>
    public ulong Level3Raw
    {
        get => this.level3;
        set
        {
            this.level3 = value;
            Touch();
        }
    }

    /// <summary>
    ///  Gets the message type.
    /// </summary>
    public MessageCode DataValue => MessageCode.MwcbDeclineLevelMessage;

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

    IMessage IMessage.Copy() => new MwcbDeclineLevelMessage(this);

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
        ///  Locate Code uniquely assigned to the security symbol for the day
        /// </summary>
        StockLocate,
        /// <summary>
        ///  Nasdaq internal tracking number
        /// </summary>
        TrackingNumber,
        /// <summary>
        ///  Nanoseconds since midnight
        /// </summary>
        Timestamp,
        /// <summary>
        ///  Denotes the MWCB Level 1 Value.
        /// </summary>
        Level1,
        /// <summary>
        ///  Denotes the MWCB Level 2 Value.
        /// </summary>
        Level2,
        /// <summary>
        ///  Denotes the MWCB Level 3 Value.
        /// </summary>
        Level3,
    }

    /// <summary>
    ///  Provides access to the fields of one instance, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public readonly struct FieldSet : IEnumerable<FieldView>
    {
        private readonly MwcbDeclineLevelMessage owner;

        internal FieldSet(MwcbDeclineLevelMessage owner)
        {
            this.owner = owner;
        }

        /// <summary>
        ///  The message type.
        /// </summary>
        public FieldView DataValue => this[FieldName.DataValue];

        /// <summary>
        ///  Locate Code uniquely assigned to the security symbol for the day
        /// </summary>
        public FieldView StockLocate => this[FieldName.StockLocate];

        /// <summary>
        ///  Nasdaq internal tracking number
        /// </summary>
        public FieldView TrackingNumber => this[FieldName.TrackingNumber];

        /// <summary>
        ///  Nanoseconds since midnight
        /// </summary>
        public FieldView Timestamp => this[FieldName.Timestamp];

        /// <summary>
        ///  Denotes the MWCB Level 1 Value.
        /// </summary>
        public FieldView Level1 => this[FieldName.Level1];

        /// <summary>
        ///  Denotes the MWCB Level 2 Value.
        /// </summary>
        public FieldView Level2 => this[FieldName.Level2];

        /// <summary>
        ///  Denotes the MWCB Level 3 Value.
        /// </summary>
        public FieldView Level3 => this[FieldName.Level3];

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
        new(nameof(FieldName.DataValue), FieldKind.Char, DataValueLength),
        new(nameof(FieldName.StockLocate), FieldKind.UnsignedInteger, StockLocateLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.TrackingNumber), FieldKind.UnsignedInteger, TrackingNumberLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.Timestamp), FieldKind.UnsignedInteger, TimestampLength) { Unit = "Nanoseconds", Epoch = "Midnight", Timezone = "America/New_York", ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.Level1), FieldKind.Decimal, Level1Length) { Scale = Level1Scale, ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.Level2), FieldKind.Decimal, Level2Length) { Scale = Level2Scale, ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.Level3), FieldKind.Decimal, Level3Length) { Scale = Level3Scale, ByteOrder = "Big", IsSigned = false },
    ];

    /// <summary>
    ///  Gets the fields in wire order, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public FieldSet Fields => new(this);

    IEnumerable<FieldView> IMessage.Fields => new FieldSet(this);

    /// <summary>
    ///  Initializes a new instance of the <see cref="MwcbDeclineLevelMessage"/> class with default values.
    /// </summary>
    public MwcbDeclineLevelMessage()
    {
        Clear();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="MwcbDeclineLevelMessage"/> class with the values, bytes, and parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public MwcbDeclineLevelMessage(MwcbDeclineLevelMessage other) : this()
    {
        CopyFrom(other);
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(MwcbDeclineLevelMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.stockLocate = other.stockLocate;
        this.trackingNumber = other.trackingNumber;
        this.timestamp = other.timestamp;
        this.level1 = other.level1;
        this.level2 = other.level2;
        this.level3 = other.level3;
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
        this.stockLocate = default;
        this.trackingNumber = default;
        this.timestamp = default;
        this.level1 = default;
        this.level2 = default;
        this.level3 = default;
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
    public static MwcbDeclineLevelMessage Parse(ReadOnlySpan<byte> data)
    {
        var instance = new MwcbDeclineLevelMessage();
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
    public static MwcbDeclineLevelMessage Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        var instance = new MwcbDeclineLevelMessage();
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
        if (Truncated(data, start, cursor, StockLocateLength, FieldName.StockLocate)) return;
        this.stockLocate = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(cursor, StockLocateLength));
        cursor += StockLocateLength;
        if (Truncated(data, start, cursor, TrackingNumberLength, FieldName.TrackingNumber)) return;
        this.trackingNumber = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(cursor, TrackingNumberLength));
        cursor += TrackingNumberLength;
        if (Truncated(data, start, cursor, TimestampLength, FieldName.Timestamp)) return;
        this.timestamp = Padded.ReadBigEndian<ulong>(data.Slice(cursor, TimestampLength));
        cursor += TimestampLength;
        if (Truncated(data, start, cursor, Level1Length, FieldName.Level1)) return;
        this.level1 = BinaryPrimitives.ReadUInt64BigEndian(data.Slice(cursor, Level1Length));
        cursor += Level1Length;
        if (Truncated(data, start, cursor, Level2Length, FieldName.Level2)) return;
        this.level2 = BinaryPrimitives.ReadUInt64BigEndian(data.Slice(cursor, Level2Length));
        cursor += Level2Length;
        if (Truncated(data, start, cursor, Level3Length, FieldName.Level3)) return;
        this.level3 = BinaryPrimitives.ReadUInt64BigEndian(data.Slice(cursor, Level3Length));
        cursor += Level3Length;
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
        BinaryPrimitives.WriteUInt16BigEndian(data.Slice(cursor, StockLocateLength), this.stockLocate);
        cursor += StockLocateLength;
        BinaryPrimitives.WriteUInt16BigEndian(data.Slice(cursor, TrackingNumberLength), this.trackingNumber);
        cursor += TrackingNumberLength;
        if (this.timestamp > TimestampWireMaximum)
        {
            failure = new EncodeFailure(new FieldView(this, (int)FieldName.Timestamp), EncodeStatus.DoesNotFit);
            return false;
        }
        Padded.WriteBigEndian(data.Slice(cursor, TimestampLength), this.timestamp);
        cursor += TimestampLength;
        BinaryPrimitives.WriteUInt64BigEndian(data.Slice(cursor, Level1Length), this.level1);
        cursor += Level1Length;
        BinaryPrimitives.WriteUInt64BigEndian(data.Slice(cursor, Level2Length), this.level2);
        cursor += Level2Length;
        BinaryPrimitives.WriteUInt64BigEndian(data.Slice(cursor, Level3Length), this.level3);
        cursor += Level3Length;
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
    public List<ValidationFailure> Validate()
    {
        var failures = new List<ValidationFailure>();
        return failures;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(MwcbDeclineLevelMessage? other)
    {
        if (other is null)
        {
            return false;
        }
        if (this.stockLocate != other.stockLocate) return false;
        if (this.trackingNumber != other.trackingNumber) return false;
        if (this.timestamp != other.timestamp) return false;
        if (this.level1 != other.level1) return false;
        if (this.level2 != other.level2) return false;
        if (this.level3 != other.level3) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is MwcbDeclineLevelMessage other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.stockLocate);
        hash.Add(this.trackingNumber);
        hash.Add(this.timestamp);
        hash.Add(this.level1);
        hash.Add(this.level2);
        hash.Add(this.level3);
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
            if (Has(FieldName.StockLocate)) names.Add(nameof(FieldName.StockLocate));
            if (Has(FieldName.TrackingNumber)) names.Add(nameof(FieldName.TrackingNumber));
            if (Has(FieldName.Timestamp)) names.Add(nameof(FieldName.Timestamp));
            if (Has(FieldName.Level1)) names.Add(nameof(FieldName.Level1));
            if (Has(FieldName.Level2)) names.Add(nameof(FieldName.Level2));
            if (Has(FieldName.Level3)) names.Add(nameof(FieldName.Level3));
            return names.Count == 0 ? "None" : string.Join(", ", names);
        }
    }

    /// <summary>
    ///  Compares this instance with another and identifies the fields that differ.
    /// </summary>
    /// <returns>The fields that differ, or None if there are no differences.</returns>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Changes Diff(MwcbDeclineLevelMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (this.stockLocate != other.stockLocate) diff = diff.With(FieldName.StockLocate);
        if (this.trackingNumber != other.trackingNumber) diff = diff.With(FieldName.TrackingNumber);
        if (this.timestamp != other.timestamp) diff = diff.With(FieldName.Timestamp);
        if (this.level1 != other.level1) diff = diff.With(FieldName.Level1);
        if (this.level2 != other.level2) diff = diff.With(FieldName.Level2);
        if (this.level3 != other.level3) diff = diff.With(FieldName.Level3);
        return diff;
    }

    private void CopyField(FieldName field, MwcbDeclineLevelMessage from)
    {
        switch (field)
        {
            case FieldName.StockLocate:
                this.stockLocate = from.stockLocate;
                break;
            case FieldName.TrackingNumber:
                this.trackingNumber = from.trackingNumber;
                break;
            case FieldName.Timestamp:
                this.timestamp = from.timestamp;
                break;
            case FieldName.Level1:
                this.level1 = from.level1;
                break;
            case FieldName.Level2:
                this.level2 = from.level2;
                break;
            case FieldName.Level3:
                this.level3 = from.level3;
                break;
            default:
                return;
        }
        Touch();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"MwcbDeclineLevelMessage {{ StockLocate: {(Text.Of(this.stockLocate))}, TrackingNumber: {(Text.Of(this.trackingNumber))}, Timestamp: {(Text.Midnight(this.timestamp, TimestampNanosecondsPerUnit))}, Level1: {(Text.Of(FixedPoint.ToDecimal(this.level1, Level1Scale)))}, Level2: {(Text.Of(FixedPoint.ToDecimal(this.level2, Level2Scale)))}, Level3: {(Text.Of(FixedPoint.ToDecimal(this.level3, Level3Scale)))} }}{(this.trailer.Length == 0 ? string.Empty : $" Trailer: {(Text.Of(this.trailer.Length))} bytes")}";

    private string DebuggerDisplay => $"[{DataValue}] [{(Text.Of(this.stockLocate))}] [{(Text.Of(this.trackingNumber))}] [{(Text.Midnight(this.timestamp, TimestampNanosecondsPerUnit))}]";

    IFieldOwner? IFieldOwner.Owner
    {
        get => this.owner;
        set => this.owner = value;
    }

    FieldDescriptor[] IFieldOwner.Descriptors => Descriptors;

    void IFieldOwner.Touch() => Touch();

    string IFieldOwner.TextOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => Text.Enum(MessageCode.MwcbDeclineLevelMessage),
        FieldName.StockLocate => Text.Of(this.stockLocate),
        FieldName.TrackingNumber => Text.Of(this.trackingNumber),
        FieldName.Timestamp => Text.Midnight(this.timestamp, TimestampNanosecondsPerUnit),
        FieldName.Level1 => Text.Of(FixedPoint.ToDecimal(this.level1, Level1Scale)),
        FieldName.Level2 => Text.Of(FixedPoint.ToDecimal(this.level2, Level2Scale)),
        FieldName.Level3 => Text.Of(FixedPoint.ToDecimal(this.level3, Level3Scale)),
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
        FieldName.StockLocate => StockLocateOffset,
        FieldName.TrackingNumber => TrackingNumberOffset,
        FieldName.Timestamp => TimestampOffset,
        FieldName.Level1 => Level1Offset,
        FieldName.Level2 => Level2Offset,
        FieldName.Level3 => Level3Offset,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    int IFieldOwner.LengthOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => DataValueLength,
        FieldName.StockLocate => StockLocateLength,
        FieldName.TrackingNumber => TrackingNumberLength,
        FieldName.Timestamp => TimestampLength,
        FieldName.Level1 => Level1Length,
        FieldName.Level2 => Level2Length,
        FieldName.Level3 => Level3Length,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    IReadOnlyList<IGroupEntry>? IFieldOwner.EntriesOf(int field) => null;

    IMessage? IFieldOwner.MessageOf(int field) => null;

    ulong[] IFieldOwner.ChangedAgainst(IFieldOwner other) => other is MwcbDeclineLevelMessage typed ? Diff(typed).ToWords() : throw new ArgumentException("The other instance is not of the same type.", nameof(other));

    void IFieldOwner.CopyField(int field, IFieldOwner from) => CopyField((FieldName)field, from as MwcbDeclineLevelMessage ?? throw new ArgumentException("The other instance is not of the same type.", nameof(from)));

    ReadOnlyMemory<byte> IFieldOwner.CodeBytes(int field)
    {
        if (field == (int)FieldName.DataValue)
        {
            var bytes = new byte[DataValueLength];
            bytes[0] = unchecked((byte)(MessageCode.MwcbDeclineLevelMessage));
            return bytes;
        }
        return ReadOnlyMemory<byte>.Empty;
    }
}
