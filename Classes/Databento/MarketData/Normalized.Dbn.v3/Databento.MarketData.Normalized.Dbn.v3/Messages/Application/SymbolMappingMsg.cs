using System.Buffers.Binary;
using System.Diagnostics;

namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  A symbol mapping message, which maps a symbol of one SType to another
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class SymbolMappingMsg : IFileMessage, IGatewayMessage, IEquatable<SymbolMappingMsg>, IFieldOwner
{
    internal const int StypeInOffset = 0;

    internal const int StypeInLength = 1;

    /// <summary>
    ///  The encoded value of Stype In that means no value.
    /// </summary>
    public const StypeIn StypeInNullValue = (StypeIn)255;

    internal const int StypeInSymbolOffset = StypeInOffset + StypeInLength;

    internal const int StypeInSymbolLength = 71;

    /// <summary>
    ///  The byte that fills the unused bytes of Stype In Symbol.
    /// </summary>
    public const byte StypeInSymbolFill = (byte)0;

    internal const int StypeOutOffset = StypeInSymbolOffset + StypeInSymbolLength;

    internal const int StypeOutLength = 1;

    internal const int StypeOutSymbolOffset = StypeOutOffset + StypeOutLength;

    internal const int StypeOutSymbolLength = 71;

    /// <summary>
    ///  The byte that fills the unused bytes of Stype Out Symbol.
    /// </summary>
    public const byte StypeOutSymbolFill = (byte)0;

    internal const int StartTsOffset = StypeOutSymbolOffset + StypeOutSymbolLength;

    internal const int StartTsLength = 8;

    /// <summary>
    ///  The number of nanoseconds in one unit of the wire value of <see cref="StartTs"/>.
    /// </summary>
    public const long StartTsNanosecondsPerUnit = 1;

    /// <summary>
    ///  The encoded value of Start Ts that means no value.
    /// </summary>
    public const ulong StartTsNullValue = 18446744073709551615UL;

    internal const int EndTsOffset = StartTsOffset + StartTsLength;

    internal const int EndTsLength = 8;

    /// <summary>
    ///  The number of nanoseconds in one unit of the wire value of <see cref="EndTs"/>.
    /// </summary>
    public const long EndTsNanosecondsPerUnit = 1;

    /// <summary>
    ///  The encoded value of End Ts that means no value.
    /// </summary>
    public const ulong EndTsNullValue = 18446744073709551615UL;

    /// <summary>
    ///  The encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public const int MessageByteLength = StypeInLength + StypeInSymbolLength + StypeOutLength + StypeOutSymbolLength + StartTsLength + EndTsLength;

    internal StypeIn stypeIn;

    /// <summary>
    ///  The input symbology type
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="StypeInNullValue"/>. Setting <see langword="null"/> writes <see cref="StypeInNullValue"/>, and a value equal to <see cref="StypeInNullValue"/> also reads back as <see langword="null"/>.
    /// </remarks>
    public StypeIn? StypeIn
    {
        get => this.stypeIn == StypeInNullValue ? (StypeIn?)null : this.stypeIn;
        set
        {
            this.stypeIn = value ?? StypeInNullValue;
            Touch();
        }
    }

    internal string stypeInSymbol = string.Empty;

    /// <summary>
    ///  The input symbol
    /// </summary>
    /// <remarks>
    ///  The field is 71 bytes long. The field holds ASCII text. Text that is shorter than the field is padded with null characters at the end. Text longer than the field is cut to fit when the message is encoded. A character that the field cannot hold is replaced with a question mark when the message is encoded.
    /// </remarks>
    public string StypeInSymbol
    {
        get => this.stypeInSymbol;
        set
        {
            this.stypeInSymbol = value ?? string.Empty;
            Touch();
        }
    }

    internal StypeOut stypeOut;

    /// <summary>
    ///  The output symbology type
    /// </summary>
    public StypeOut StypeOut
    {
        get => this.stypeOut;
        set
        {
            this.stypeOut = value;
            Touch();
        }
    }

    internal string stypeOutSymbol = string.Empty;

    /// <summary>
    ///  The output symbol
    /// </summary>
    /// <remarks>
    ///  The field is 71 bytes long. The field holds ASCII text. Text that is shorter than the field is padded with null characters at the end. Text longer than the field is cut to fit when the message is encoded. A character that the field cannot hold is replaced with a question mark when the message is encoded.
    /// </remarks>
    public string StypeOutSymbol
    {
        get => this.stypeOutSymbol;
        set
        {
            this.stypeOutSymbol = value ?? string.Empty;
            Touch();
        }
    }

    internal ulong startTs;

    /// <summary>
    ///  The start of the mapping interval expressed as the number of nanoseconds since the UNIX epoch
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="StartTsNullValue"/>. Setting <see langword="null"/> writes <see cref="StartTsNullValue"/>. This property has a precision of 100 nanoseconds, and <see cref="StartTsRaw"/> holds the exact value. Setting a local time converts it to UTC, and a time of unspecified kind is treated as UTC. A time outside the range of the field is set to the nearest limit of the range. A result equal to <see cref="StartTsNullValue"/> is moved one step to the nearest other value.
    /// </remarks>
    public DateTime? StartTs
    {
        get => this.startTs == StartTsNullValue ? (DateTime?)null : Instant.ToDateTime(this.startTs, StartTsNanosecondsPerUnit);
        set
        {
            this.startTs = value is { } present ? Instant.ToRaw<ulong>(present, StartTsNanosecondsPerUnit, ulong.MinValue, ulong.MaxValue, StartTsNullValue) : StartTsNullValue;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="StartTs"/>, as the unscaled integer that the field holds.
    /// </summary>
    public ulong StartTsRaw
    {
        get => this.startTs;
        set
        {
            this.startTs = value;
            Touch();
        }
    }

    internal ulong endTs;

    /// <summary>
    ///  The end of the mapping interval expressed as the number of nanoseconds since the UNIX epoch
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="EndTsNullValue"/>. Setting <see langword="null"/> writes <see cref="EndTsNullValue"/>. This property has a precision of 100 nanoseconds, and <see cref="EndTsRaw"/> holds the exact value. Setting a local time converts it to UTC, and a time of unspecified kind is treated as UTC. A time outside the range of the field is set to the nearest limit of the range. A result equal to <see cref="EndTsNullValue"/> is moved one step to the nearest other value.
    /// </remarks>
    public DateTime? EndTs
    {
        get => this.endTs == EndTsNullValue ? (DateTime?)null : Instant.ToDateTime(this.endTs, EndTsNanosecondsPerUnit);
        set
        {
            this.endTs = value is { } present ? Instant.ToRaw<ulong>(present, EndTsNanosecondsPerUnit, ulong.MinValue, ulong.MaxValue, EndTsNullValue) : EndTsNullValue;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="EndTs"/>, as the unscaled integer that the field holds.
    /// </summary>
    public ulong EndTsRaw
    {
        get => this.endTs;
        set
        {
            this.endTs = value;
            Touch();
        }
    }

    /// <summary>
    ///  Gets the message type.
    /// </summary>
    public RecordBodyCode DataValue => RecordBodyCode.SymbolMappingMsg;

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
    public bool IsRecognized => (this.stypeIn == StypeInNullValue || global::System.Enum.IsDefined(this.stypeIn)) && global::System.Enum.IsDefined(this.stypeOut);

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

    IMessage IMessage.Copy() => new SymbolMappingMsg(this);

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
        ///  The input symbology type
        /// </summary>
        StypeIn,
        /// <summary>
        ///  The input symbol
        /// </summary>
        StypeInSymbol,
        /// <summary>
        ///  The output symbology type
        /// </summary>
        StypeOut,
        /// <summary>
        ///  The output symbol
        /// </summary>
        StypeOutSymbol,
        /// <summary>
        ///  The start of the mapping interval expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        StartTs,
        /// <summary>
        ///  The end of the mapping interval expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        EndTs,
    }

    /// <summary>
    ///  Provides access to the fields of one instance, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public readonly struct FieldSet : IEnumerable<FieldView>
    {
        private readonly SymbolMappingMsg owner;

        internal FieldSet(SymbolMappingMsg owner)
        {
            this.owner = owner;
        }

        /// <summary>
        ///  The message type.
        /// </summary>
        public FieldView DataValue => this[FieldName.DataValue];

        /// <summary>
        ///  The input symbology type
        /// </summary>
        public FieldView StypeIn => this[FieldName.StypeIn];

        /// <summary>
        ///  The input symbol
        /// </summary>
        public FieldView StypeInSymbol => this[FieldName.StypeInSymbol];

        /// <summary>
        ///  The output symbology type
        /// </summary>
        public FieldView StypeOut => this[FieldName.StypeOut];

        /// <summary>
        ///  The output symbol
        /// </summary>
        public FieldView StypeOutSymbol => this[FieldName.StypeOutSymbol];

        /// <summary>
        ///  The start of the mapping interval expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public FieldView StartTs => this[FieldName.StartTs];

        /// <summary>
        ///  The end of the mapping interval expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public FieldView EndTs => this[FieldName.EndTs];

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
        new(nameof(FieldName.StypeIn), FieldKind.Enum, StypeInLength) { IsSigned = false, Nullable = "Value", NullValue = StypeInNullValue },
        new(nameof(FieldName.StypeInSymbol), FieldKind.String, StypeInSymbolLength) { Fill = "NullCharacters", Justification = "Left" },
        new(nameof(FieldName.StypeOut), FieldKind.Enum, StypeOutLength) { IsSigned = false },
        new(nameof(FieldName.StypeOutSymbol), FieldKind.String, StypeOutSymbolLength) { Fill = "NullCharacters", Justification = "Left" },
        new(nameof(FieldName.StartTs), FieldKind.DateTime, StartTsLength) { Unit = "Nanoseconds", Epoch = "Unix", ByteOrder = "Little", IsSigned = false, Nullable = "Value", NullValue = StartTsNullValue },
        new(nameof(FieldName.EndTs), FieldKind.DateTime, EndTsLength) { Unit = "Nanoseconds", Epoch = "Unix", ByteOrder = "Little", IsSigned = false, Nullable = "Value", NullValue = EndTsNullValue },
    ];

    /// <summary>
    ///  Gets the fields in wire order, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public FieldSet Fields => new(this);

    IEnumerable<FieldView> IMessage.Fields => new FieldSet(this);

    /// <summary>
    ///  Initializes a new instance of the <see cref="SymbolMappingMsg"/> class with default values.
    /// </summary>
    public SymbolMappingMsg()
    {
        Clear();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="SymbolMappingMsg"/> class with the values, bytes, and parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public SymbolMappingMsg(SymbolMappingMsg other) : this()
    {
        CopyFrom(other);
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(SymbolMappingMsg other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.stypeIn = other.stypeIn;
        this.stypeInSymbol = other.stypeInSymbol;
        this.stypeOut = other.stypeOut;
        this.stypeOutSymbol = other.stypeOutSymbol;
        this.startTs = other.startTs;
        this.endTs = other.endTs;
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
        this.stypeIn = default;
        this.stypeInSymbol = string.Empty;
        this.stypeOut = default;
        this.stypeOutSymbol = string.Empty;
        this.startTs = default;
        this.endTs = default;
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
    public static SymbolMappingMsg Parse(ReadOnlySpan<byte> data)
    {
        var instance = new SymbolMappingMsg();
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
    public static SymbolMappingMsg Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        var instance = new SymbolMappingMsg();
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
        if (Truncated(data, start, cursor, StypeInLength, FieldName.StypeIn)) return;
        this.stypeIn = (StypeIn)data.Slice(cursor, StypeInLength)[0];
        cursor += StypeInLength;
        if (Truncated(data, start, cursor, StypeInSymbolLength, FieldName.StypeInSymbol)) return;
        this.stypeInSymbol = TextEncoding.Ascii.ReadLeftJustified(data.Slice(cursor, StypeInSymbolLength), StypeInSymbolFill);
        cursor += StypeInSymbolLength;
        if (Truncated(data, start, cursor, StypeOutLength, FieldName.StypeOut)) return;
        this.stypeOut = (StypeOut)data.Slice(cursor, StypeOutLength)[0];
        cursor += StypeOutLength;
        if (Truncated(data, start, cursor, StypeOutSymbolLength, FieldName.StypeOutSymbol)) return;
        this.stypeOutSymbol = TextEncoding.Ascii.ReadLeftJustified(data.Slice(cursor, StypeOutSymbolLength), StypeOutSymbolFill);
        cursor += StypeOutSymbolLength;
        if (Truncated(data, start, cursor, StartTsLength, FieldName.StartTs)) return;
        this.startTs = BinaryPrimitives.ReadUInt64LittleEndian(data.Slice(cursor, StartTsLength));
        cursor += StartTsLength;
        if (Truncated(data, start, cursor, EndTsLength, FieldName.EndTs)) return;
        this.endTs = BinaryPrimitives.ReadUInt64LittleEndian(data.Slice(cursor, EndTsLength));
        cursor += EndTsLength;
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
        data.Slice(cursor, StypeInLength)[0] = unchecked((byte)(this.stypeIn));
        cursor += StypeInLength;
        TextEncoding.Ascii.WriteLeftJustified(data.Slice(cursor, StypeInSymbolLength), TextEncoding.Ascii.Fit(this.stypeInSymbol, StypeInSymbolLength), StypeInSymbolFill);
        cursor += StypeInSymbolLength;
        data.Slice(cursor, StypeOutLength)[0] = unchecked((byte)(this.stypeOut));
        cursor += StypeOutLength;
        TextEncoding.Ascii.WriteLeftJustified(data.Slice(cursor, StypeOutSymbolLength), TextEncoding.Ascii.Fit(this.stypeOutSymbol, StypeOutSymbolLength), StypeOutSymbolFill);
        cursor += StypeOutSymbolLength;
        BinaryPrimitives.WriteUInt64LittleEndian(data.Slice(cursor, StartTsLength), this.startTs);
        cursor += StartTsLength;
        BinaryPrimitives.WriteUInt64LittleEndian(data.Slice(cursor, EndTsLength), this.endTs);
        cursor += EndTsLength;
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
    ///  Validate checks the current values. After a parse, check Status first, because a field that the parse did not reach holds its cleared value. A new or cleared instance holds zero in a field that can be null, until the field is set.
    /// </remarks>
    public List<ValidationFailure> Validate()
    {
        var failures = new List<ValidationFailure>();
        if (!(this.stypeIn == StypeInNullValue || global::System.Enum.IsDefined(this.stypeIn)))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.StypeIn), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Text.Ascii.IsValid(this.stypeInSymbol))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.StypeInSymbol), ValidationStatus.NonAsciiText));
        }
        if (!global::System.Enum.IsDefined(this.stypeOut))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.StypeOut), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Text.Ascii.IsValid(this.stypeOutSymbol))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.StypeOutSymbol), ValidationStatus.NonAsciiText));
        }
        return failures;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(SymbolMappingMsg? other)
    {
        if (other is null)
        {
            return false;
        }
        if (this.stypeIn != other.stypeIn) return false;
        if (this.stypeInSymbol != other.stypeInSymbol) return false;
        if (this.stypeOut != other.stypeOut) return false;
        if (this.stypeOutSymbol != other.stypeOutSymbol) return false;
        if (this.startTs != other.startTs) return false;
        if (this.endTs != other.endTs) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is SymbolMappingMsg other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.stypeIn);
        hash.Add(this.stypeInSymbol);
        hash.Add(this.stypeOut);
        hash.Add(this.stypeOutSymbol);
        hash.Add(this.startTs);
        hash.Add(this.endTs);
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
            if (Has(FieldName.StypeIn)) names.Add(nameof(FieldName.StypeIn));
            if (Has(FieldName.StypeInSymbol)) names.Add(nameof(FieldName.StypeInSymbol));
            if (Has(FieldName.StypeOut)) names.Add(nameof(FieldName.StypeOut));
            if (Has(FieldName.StypeOutSymbol)) names.Add(nameof(FieldName.StypeOutSymbol));
            if (Has(FieldName.StartTs)) names.Add(nameof(FieldName.StartTs));
            if (Has(FieldName.EndTs)) names.Add(nameof(FieldName.EndTs));
            return names.Count == 0 ? "None" : string.Join(", ", names);
        }
    }

    /// <summary>
    ///  Compares this instance with another and identifies the fields that differ.
    /// </summary>
    /// <returns>The fields that differ, or None if there are no differences.</returns>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Changes Diff(SymbolMappingMsg other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (this.stypeIn != other.stypeIn) diff = diff.With(FieldName.StypeIn);
        if (this.stypeInSymbol != other.stypeInSymbol) diff = diff.With(FieldName.StypeInSymbol);
        if (this.stypeOut != other.stypeOut) diff = diff.With(FieldName.StypeOut);
        if (this.stypeOutSymbol != other.stypeOutSymbol) diff = diff.With(FieldName.StypeOutSymbol);
        if (this.startTs != other.startTs) diff = diff.With(FieldName.StartTs);
        if (this.endTs != other.endTs) diff = diff.With(FieldName.EndTs);
        return diff;
    }

    private void CopyField(FieldName field, SymbolMappingMsg from)
    {
        switch (field)
        {
            case FieldName.StypeIn:
                this.stypeIn = from.stypeIn;
                break;
            case FieldName.StypeInSymbol:
                this.stypeInSymbol = from.stypeInSymbol;
                break;
            case FieldName.StypeOut:
                this.stypeOut = from.stypeOut;
                break;
            case FieldName.StypeOutSymbol:
                this.stypeOutSymbol = from.stypeOutSymbol;
                break;
            case FieldName.StartTs:
                this.startTs = from.startTs;
                break;
            case FieldName.EndTs:
                this.endTs = from.endTs;
                break;
            default:
                return;
        }
        Touch();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"SymbolMappingMsg {{ StypeIn: {(Text.Enum(this.stypeIn == StypeInNullValue ? (StypeIn?)null : this.stypeIn))}, StypeInSymbol: {(Text.Of(this.stypeInSymbol))}, StypeOut: {(Text.Enum(this.stypeOut))}, StypeOutSymbol: {(Text.Of(this.stypeOutSymbol))}, StartTs: {(Text.Instant(this.startTs, StartTsNanosecondsPerUnit, StartTsNullValue))}, EndTs: {(Text.Instant(this.endTs, EndTsNanosecondsPerUnit, EndTsNullValue))} }}{(this.trailer.Length == 0 ? string.Empty : $" Trailer: {(Text.Of(this.trailer.Length))} bytes")}";

    private string DebuggerDisplay => $"[{DataValue}] [{(Text.Enum(this.stypeIn == StypeInNullValue ? (StypeIn?)null : this.stypeIn))}] [{(Text.Of(this.stypeInSymbol))}] [{(Text.Enum(this.stypeOut))}]";

    IFieldOwner? IFieldOwner.Owner
    {
        get => this.owner;
        set => this.owner = value;
    }

    FieldDescriptor[] IFieldOwner.Descriptors => Descriptors;

    void IFieldOwner.Touch() => Touch();

    string IFieldOwner.TextOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => Text.Enum(RecordBodyCode.SymbolMappingMsg),
        FieldName.StypeIn => Text.Enum(this.stypeIn == StypeInNullValue ? (StypeIn?)null : this.stypeIn),
        FieldName.StypeInSymbol => Text.Of(this.stypeInSymbol),
        FieldName.StypeOut => Text.Enum(this.stypeOut),
        FieldName.StypeOutSymbol => Text.Of(this.stypeOutSymbol),
        FieldName.StartTs => Text.Instant(this.startTs, StartTsNanosecondsPerUnit, StartTsNullValue),
        FieldName.EndTs => Text.Instant(this.endTs, EndTsNanosecondsPerUnit, EndTsNullValue),
        _ => string.Empty,
    };

    bool IFieldOwner.IsRecognized(int field) => (FieldName)field switch
    {
        FieldName.StypeIn => (this.stypeIn == StypeInNullValue || global::System.Enum.IsDefined(this.stypeIn)),
        FieldName.StypeOut => global::System.Enum.IsDefined(this.stypeOut),
        _ => true,
    };

    bool IFieldOwner.IsPresent(int field) => (FieldName)field switch
    {
        FieldName.StypeIn => this.stypeIn != StypeInNullValue,
        FieldName.StartTs => this.startTs != StartTsNullValue,
        FieldName.EndTs => this.endTs != EndTsNullValue,
        _ => true,
    };

    bool IFieldOwner.IsParsed(int field) => this.status == ParseStatus.Ok || field < (int?)this.failed;

    bool IFieldOwner.IsReached(int field) => this.modified || this.status is ParseStatus.None or ParseStatus.Ok || field <= (int?)this.failed;

    bool IFieldOwner.HasParseLayout(int field) => false;

    int IFieldOwner.OffsetOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => FieldView.NotInMessage,
        FieldName.StypeIn => StypeInOffset,
        FieldName.StypeInSymbol => StypeInSymbolOffset,
        FieldName.StypeOut => StypeOutOffset,
        FieldName.StypeOutSymbol => StypeOutSymbolOffset,
        FieldName.StartTs => StartTsOffset,
        FieldName.EndTs => EndTsOffset,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    int IFieldOwner.LengthOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => DataValueLength,
        FieldName.StypeIn => StypeInLength,
        FieldName.StypeInSymbol => StypeInSymbolLength,
        FieldName.StypeOut => StypeOutLength,
        FieldName.StypeOutSymbol => StypeOutSymbolLength,
        FieldName.StartTs => StartTsLength,
        FieldName.EndTs => EndTsLength,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    IReadOnlyList<IGroupEntry>? IFieldOwner.EntriesOf(int field) => null;

    IMessage? IFieldOwner.MessageOf(int field) => null;

    ulong[] IFieldOwner.ChangedAgainst(IFieldOwner other) => other is SymbolMappingMsg typed ? Diff(typed).ToWords() : throw new ArgumentException("The other instance is not of the same type.", nameof(other));

    void IFieldOwner.CopyField(int field, IFieldOwner from) => CopyField((FieldName)field, from as SymbolMappingMsg ?? throw new ArgumentException("The other instance is not of the same type.", nameof(from)));

    ReadOnlyMemory<byte> IFieldOwner.CodeBytes(int field)
    {
        if (field == (int)FieldName.DataValue)
        {
            var bytes = new byte[DataValueLength];
            bytes[0] = unchecked((byte)(RecordBodyCode.SymbolMappingMsg));
            return bytes;
        }
        return ReadOnlyMemory<byte>.Empty;
    }
}
