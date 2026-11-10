using System.Buffers.Binary;
using System.Diagnostics;

namespace Databento.MarketData.Normalized.Dbn.v2;

/// <summary>
///  Open, high, low, close, and volume; the record of the ohlcv schemas
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class OhlcvMsg : IFileMessage, IGatewayMessage, IEquatable<OhlcvMsg>, IFieldOwner
{
    internal const int OpenOffset = 0;

    internal const int OpenLength = 8;

    /// <summary>
    ///  The number of decimal places in Open.
    /// </summary>
    public const byte OpenScale = 9;

    /// <summary>
    ///  The factor by which <see cref="Open"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal OpenFactor = 1000000000m;

    /// <summary>
    ///  The encoded value of Open that means no value.
    /// </summary>
    public const long OpenNullValue = 9223372036854775807L;

    internal const int HighOffset = OpenOffset + OpenLength;

    internal const int HighLength = 8;

    /// <summary>
    ///  The number of decimal places in High.
    /// </summary>
    public const byte HighScale = 9;

    /// <summary>
    ///  The factor by which <see cref="High"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal HighFactor = 1000000000m;

    /// <summary>
    ///  The encoded value of High that means no value.
    /// </summary>
    public const long HighNullValue = 9223372036854775807L;

    internal const int LowOffset = HighOffset + HighLength;

    internal const int LowLength = 8;

    /// <summary>
    ///  The number of decimal places in Low.
    /// </summary>
    public const byte LowScale = 9;

    /// <summary>
    ///  The factor by which <see cref="Low"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal LowFactor = 1000000000m;

    /// <summary>
    ///  The encoded value of Low that means no value.
    /// </summary>
    public const long LowNullValue = 9223372036854775807L;

    internal const int CloseOffset = LowOffset + LowLength;

    internal const int CloseLength = 8;

    /// <summary>
    ///  The number of decimal places in Close.
    /// </summary>
    public const byte CloseScale = 9;

    /// <summary>
    ///  The factor by which <see cref="Close"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal CloseFactor = 1000000000m;

    /// <summary>
    ///  The encoded value of Close that means no value.
    /// </summary>
    public const long CloseNullValue = 9223372036854775807L;

    internal const int VolumeOffset = CloseOffset + CloseLength;

    internal const int VolumeLength = 8;

    /// <summary>
    ///  The encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public const int MessageByteLength = OpenLength + HighLength + LowLength + CloseLength + VolumeLength;

    internal long open;

    /// <summary>
    ///  The open price for the bar
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="OpenNullValue"/>. Setting <see langword="null"/> writes <see cref="OpenNullValue"/>. A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range. A result equal to <see cref="OpenNullValue"/> is moved one step to the nearest other value.
    /// </remarks>
    public decimal? Open
    {
        get => this.open == OpenNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.open, OpenScale);
        set
        {
            this.open = value is { } present ? FixedPoint.ToRaw<long>(present, OpenFactor, long.MinValue, long.MaxValue, OpenNullValue) : OpenNullValue;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="Open"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long OpenRaw
    {
        get => this.open;
        set
        {
            this.open = value;
            Touch();
        }
    }

    internal long high;

    /// <summary>
    ///  The high price for the bar
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="HighNullValue"/>. Setting <see langword="null"/> writes <see cref="HighNullValue"/>. A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range. A result equal to <see cref="HighNullValue"/> is moved one step to the nearest other value.
    /// </remarks>
    public decimal? High
    {
        get => this.high == HighNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.high, HighScale);
        set
        {
            this.high = value is { } present ? FixedPoint.ToRaw<long>(present, HighFactor, long.MinValue, long.MaxValue, HighNullValue) : HighNullValue;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="High"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long HighRaw
    {
        get => this.high;
        set
        {
            this.high = value;
            Touch();
        }
    }

    internal long low;

    /// <summary>
    ///  The low price for the bar
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="LowNullValue"/>. Setting <see langword="null"/> writes <see cref="LowNullValue"/>. A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range. A result equal to <see cref="LowNullValue"/> is moved one step to the nearest other value.
    /// </remarks>
    public decimal? Low
    {
        get => this.low == LowNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.low, LowScale);
        set
        {
            this.low = value is { } present ? FixedPoint.ToRaw<long>(present, LowFactor, long.MinValue, long.MaxValue, LowNullValue) : LowNullValue;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="Low"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long LowRaw
    {
        get => this.low;
        set
        {
            this.low = value;
            Touch();
        }
    }

    internal long close;

    /// <summary>
    ///  The close price for the bar
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="CloseNullValue"/>. Setting <see langword="null"/> writes <see cref="CloseNullValue"/>. A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range. A result equal to <see cref="CloseNullValue"/> is moved one step to the nearest other value.
    /// </remarks>
    public decimal? Close
    {
        get => this.close == CloseNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.close, CloseScale);
        set
        {
            this.close = value is { } present ? FixedPoint.ToRaw<long>(present, CloseFactor, long.MinValue, long.MaxValue, CloseNullValue) : CloseNullValue;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="Close"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long CloseRaw
    {
        get => this.close;
        set
        {
            this.close = value;
            Touch();
        }
    }

    internal ulong volume;

    /// <summary>
    ///  The total volume traded during the aggregation period
    /// </summary>
    public ulong Volume
    {
        get => this.volume;
        set
        {
            this.volume = value;
            Touch();
        }
    }

    private RecordBodyCode dataValue;

    /// <summary>
    ///  Gets or sets the message type.
    /// </summary>
    /// <remarks>
    ///  ParseFrom preserves the message type because the bytes that it reads do not contain it. Clear restores the default message type.
    /// </remarks>
    public RecordBodyCode DataValue
    {
        get => this.dataValue;
        set
        {
            this.dataValue = value;
            Touch();
        }
    }

    internal OhlcvMsg SetDataValue(RecordBodyCode type)
    {
        this.dataValue = type;
        return this;
    }

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

    IMessage IMessage.Copy() => new OhlcvMsg(this);

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
        ///  The open price for the bar
        /// </summary>
        Open,
        /// <summary>
        ///  The high price for the bar
        /// </summary>
        High,
        /// <summary>
        ///  The low price for the bar
        /// </summary>
        Low,
        /// <summary>
        ///  The close price for the bar
        /// </summary>
        Close,
        /// <summary>
        ///  The total volume traded during the aggregation period
        /// </summary>
        Volume,
    }

    /// <summary>
    ///  Provides access to the fields of one instance, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public readonly struct FieldSet : IEnumerable<FieldView>
    {
        private readonly OhlcvMsg owner;

        internal FieldSet(OhlcvMsg owner)
        {
            this.owner = owner;
        }

        /// <summary>
        ///  The message type.
        /// </summary>
        public FieldView DataValue => this[FieldName.DataValue];

        /// <summary>
        ///  The open price for the bar
        /// </summary>
        public FieldView Open => this[FieldName.Open];

        /// <summary>
        ///  The high price for the bar
        /// </summary>
        public FieldView High => this[FieldName.High];

        /// <summary>
        ///  The low price for the bar
        /// </summary>
        public FieldView Low => this[FieldName.Low];

        /// <summary>
        ///  The close price for the bar
        /// </summary>
        public FieldView Close => this[FieldName.Close];

        /// <summary>
        ///  The total volume traded during the aggregation period
        /// </summary>
        public FieldView Volume => this[FieldName.Volume];

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
        new(nameof(FieldName.Open), FieldKind.Decimal, OpenLength) { Scale = OpenScale, ByteOrder = "Little", IsSigned = true, Nullable = "Value", NullValue = OpenNullValue },
        new(nameof(FieldName.High), FieldKind.Decimal, HighLength) { Scale = HighScale, ByteOrder = "Little", IsSigned = true, Nullable = "Value", NullValue = HighNullValue },
        new(nameof(FieldName.Low), FieldKind.Decimal, LowLength) { Scale = LowScale, ByteOrder = "Little", IsSigned = true, Nullable = "Value", NullValue = LowNullValue },
        new(nameof(FieldName.Close), FieldKind.Decimal, CloseLength) { Scale = CloseScale, ByteOrder = "Little", IsSigned = true, Nullable = "Value", NullValue = CloseNullValue },
        new(nameof(FieldName.Volume), FieldKind.UnsignedInteger, VolumeLength) { ByteOrder = "Little", IsSigned = false },
    ];

    /// <summary>
    ///  Gets the fields in wire order, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public FieldSet Fields => new(this);

    IEnumerable<FieldView> IMessage.Fields => new FieldSet(this);

    /// <summary>
    ///  Initializes a new instance of the <see cref="OhlcvMsg"/> class with default values.
    /// </summary>
    public OhlcvMsg()
    {
        Clear();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="OhlcvMsg"/> class with the values, bytes, and parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public OhlcvMsg(OhlcvMsg other) : this()
    {
        CopyFrom(other);
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(OhlcvMsg other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.open = other.open;
        this.high = other.high;
        this.low = other.low;
        this.close = other.close;
        this.volume = other.volume;
        this.dataValue = other.dataValue;
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
        this.open = default;
        this.high = default;
        this.low = default;
        this.close = default;
        this.volume = default;
        this.dataValue = RecordBodyCode.OhlcvMsg;
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
    public static OhlcvMsg Parse(ReadOnlySpan<byte> data)
    {
        var instance = new OhlcvMsg();
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
    public static OhlcvMsg Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        var instance = new OhlcvMsg();
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
        var storedCode = this.dataValue;
        Clear();
        this.dataValue = storedCode;
        if ((uint)offset > (uint)data.Length)
        {
            this.status = ParseStatus.InvalidOffset;
            return;
        }
        var start = offset;
        var cursor = start;
        if (Truncated(data, start, cursor, OpenLength, FieldName.Open)) return;
        this.open = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(cursor, OpenLength));
        cursor += OpenLength;
        if (Truncated(data, start, cursor, HighLength, FieldName.High)) return;
        this.high = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(cursor, HighLength));
        cursor += HighLength;
        if (Truncated(data, start, cursor, LowLength, FieldName.Low)) return;
        this.low = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(cursor, LowLength));
        cursor += LowLength;
        if (Truncated(data, start, cursor, CloseLength, FieldName.Close)) return;
        this.close = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(cursor, CloseLength));
        cursor += CloseLength;
        if (Truncated(data, start, cursor, VolumeLength, FieldName.Volume)) return;
        this.volume = BinaryPrimitives.ReadUInt64LittleEndian(data.Slice(cursor, VolumeLength));
        cursor += VolumeLength;
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
        if (this.dataValue is not (RecordBodyCode.OhlcvMsg or RecordBodyCode.OhlcvMsg2 or RecordBodyCode.OhlcvMsg3 or RecordBodyCode.OhlcvMsg4 or RecordBodyCode.OhlcvMsg5 or RecordBodyCode.OhlcvMsg6))
        {
            failure = new EncodeFailure(Descriptors[(int)FieldName.DataValue], EncodeStatus.DoesNotFit, null);
            return false;
        }
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(cursor, OpenLength), this.open);
        cursor += OpenLength;
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(cursor, HighLength), this.high);
        cursor += HighLength;
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(cursor, LowLength), this.low);
        cursor += LowLength;
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(cursor, CloseLength), this.close);
        cursor += CloseLength;
        BinaryPrimitives.WriteUInt64LittleEndian(data.Slice(cursor, VolumeLength), this.volume);
        cursor += VolumeLength;
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
    public bool Equals(OhlcvMsg? other)
    {
        if (other is null)
        {
            return false;
        }
        if (this.open != other.open) return false;
        if (this.high != other.high) return false;
        if (this.low != other.low) return false;
        if (this.close != other.close) return false;
        if (this.volume != other.volume) return false;
        if (this.dataValue != other.dataValue) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is OhlcvMsg other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.open);
        hash.Add(this.high);
        hash.Add(this.low);
        hash.Add(this.close);
        hash.Add(this.volume);
        hash.Add(this.dataValue);
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
        private const int FieldCount = 6;

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
            if (Has(FieldName.Open)) names.Add(nameof(FieldName.Open));
            if (Has(FieldName.High)) names.Add(nameof(FieldName.High));
            if (Has(FieldName.Low)) names.Add(nameof(FieldName.Low));
            if (Has(FieldName.Close)) names.Add(nameof(FieldName.Close));
            if (Has(FieldName.Volume)) names.Add(nameof(FieldName.Volume));
            return names.Count == 0 ? "None" : string.Join(", ", names);
        }
    }

    /// <summary>
    ///  Compares this instance with another and identifies the fields that differ.
    /// </summary>
    /// <returns>The fields that differ, or None if there are no differences.</returns>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Changes Diff(OhlcvMsg other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (this.dataValue != other.dataValue) diff = diff.With(FieldName.DataValue);
        if (this.open != other.open) diff = diff.With(FieldName.Open);
        if (this.high != other.high) diff = diff.With(FieldName.High);
        if (this.low != other.low) diff = diff.With(FieldName.Low);
        if (this.close != other.close) diff = diff.With(FieldName.Close);
        if (this.volume != other.volume) diff = diff.With(FieldName.Volume);
        return diff;
    }

    private void CopyField(FieldName field, OhlcvMsg from)
    {
        switch (field)
        {
            case FieldName.DataValue:
                this.dataValue = from.dataValue;
                break;
            case FieldName.Open:
                this.open = from.open;
                break;
            case FieldName.High:
                this.high = from.high;
                break;
            case FieldName.Low:
                this.low = from.low;
                break;
            case FieldName.Close:
                this.close = from.close;
                break;
            case FieldName.Volume:
                this.volume = from.volume;
                break;
            default:
                return;
        }
        Touch();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"OhlcvMsg {{ DataValue: {Text.Enum(this.dataValue)}, Open: {(Text.Of(this.open == OpenNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.open, OpenScale)))}, High: {(Text.Of(this.high == HighNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.high, HighScale)))}, Low: {(Text.Of(this.low == LowNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.low, LowScale)))}, Close: {(Text.Of(this.close == CloseNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.close, CloseScale)))}, Volume: {(Text.Of(this.volume))} }}{(this.trailer.Length == 0 ? string.Empty : $" Trailer: {(Text.Of(this.trailer.Length))} bytes")}";

    private string DebuggerDisplay => $"[{DataValue}] [{(Text.Of(this.open == OpenNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.open, OpenScale)))}] [{(Text.Of(this.high == HighNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.high, HighScale)))}] [{(Text.Of(this.low == LowNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.low, LowScale)))}]";

    IFieldOwner? IFieldOwner.Owner
    {
        get => this.owner;
        set => this.owner = value;
    }

    FieldDescriptor[] IFieldOwner.Descriptors => Descriptors;

    void IFieldOwner.Touch() => Touch();

    string IFieldOwner.TextOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => Text.Enum(this.dataValue),
        FieldName.Open => Text.Of(this.open == OpenNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.open, OpenScale)),
        FieldName.High => Text.Of(this.high == HighNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.high, HighScale)),
        FieldName.Low => Text.Of(this.low == LowNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.low, LowScale)),
        FieldName.Close => Text.Of(this.close == CloseNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.close, CloseScale)),
        FieldName.Volume => Text.Of(this.volume),
        _ => string.Empty,
    };

    bool IFieldOwner.IsRecognized(int field) => true;

    bool IFieldOwner.IsPresent(int field) => (FieldName)field switch
    {
        FieldName.Open => this.open != OpenNullValue,
        FieldName.High => this.high != HighNullValue,
        FieldName.Low => this.low != LowNullValue,
        FieldName.Close => this.close != CloseNullValue,
        _ => true,
    };

    bool IFieldOwner.IsParsed(int field) => this.status == ParseStatus.Ok || field < (int?)this.failed;

    bool IFieldOwner.IsReached(int field) => this.modified || this.status is ParseStatus.None or ParseStatus.Ok || field <= (int?)this.failed;

    bool IFieldOwner.HasParseLayout(int field) => false;

    int IFieldOwner.OffsetOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => FieldView.NotInMessage,
        FieldName.Open => OpenOffset,
        FieldName.High => HighOffset,
        FieldName.Low => LowOffset,
        FieldName.Close => CloseOffset,
        FieldName.Volume => VolumeOffset,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    int IFieldOwner.LengthOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => DataValueLength,
        FieldName.Open => OpenLength,
        FieldName.High => HighLength,
        FieldName.Low => LowLength,
        FieldName.Close => CloseLength,
        FieldName.Volume => VolumeLength,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    IReadOnlyList<IGroupEntry>? IFieldOwner.EntriesOf(int field) => null;

    IMessage? IFieldOwner.MessageOf(int field) => null;

    ulong[] IFieldOwner.ChangedAgainst(IFieldOwner other) => other is OhlcvMsg typed ? Diff(typed).ToWords() : throw new ArgumentException("The other instance is not of the same type.", nameof(other));

    void IFieldOwner.CopyField(int field, IFieldOwner from) => CopyField((FieldName)field, from as OhlcvMsg ?? throw new ArgumentException("The other instance is not of the same type.", nameof(from)));

    ReadOnlyMemory<byte> IFieldOwner.CodeBytes(int field)
    {
        if (field == (int)FieldName.DataValue)
        {
            var bytes = new byte[DataValueLength];
            bytes[0] = unchecked((byte)(this.dataValue));
            return bytes;
        }
        return ReadOnlyMemory<byte>.Empty;
    }
}
