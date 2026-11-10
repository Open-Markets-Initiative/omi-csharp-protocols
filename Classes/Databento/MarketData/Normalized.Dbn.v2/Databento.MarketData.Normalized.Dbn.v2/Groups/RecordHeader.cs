using System.Buffers.Binary;
using System.Diagnostics;

namespace Databento.MarketData.Normalized.Dbn.v2;

/// <summary>
///  DBN common header of every record
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class RecordHeader : IGroupEntry, IEquatable<RecordHeader>, IFieldOwner
{
    internal const int RecordLengthOffset = 0;

    internal const int RecordLengthLength = 1;

    internal const int RtypeOffset = RecordLengthOffset + RecordLengthLength;

    internal const int RtypeLength = 1;

    internal const int PublisherIdOffset = RtypeOffset + RtypeLength;

    internal const int PublisherIdLength = 2;

    internal const int InstrumentIdOffset = PublisherIdOffset + PublisherIdLength;

    internal const int InstrumentIdLength = 4;

    internal const int TsEventOffset = InstrumentIdOffset + InstrumentIdLength;

    internal const int TsEventLength = 8;

    /// <summary>
    ///  The number of nanoseconds in one unit of the wire value of <see cref="TsEvent"/>.
    /// </summary>
    public const long TsEventNanosecondsPerUnit = 1;

    /// <summary>
    ///  The encoded value of Ts Event that means no value.
    /// </summary>
    public const ulong TsEventNullValue = 18446744073709551615UL;

    /// <summary>
    ///  The encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public const int MessageByteLength = RecordLengthLength + RtypeLength + PublisherIdLength + InstrumentIdLength + TsEventLength;

    internal byte recordLength;

    /// <summary>
    ///  The length of the record in 32-bit words, including this header and any ts_out trailer
    /// </summary>
    public byte RecordLength => this.recordLength;

    internal Rtype rtype;

    /// <summary>
    ///  The record type, which selects the record that follows the header
    /// </summary>
    public Rtype Rtype => this.rtype;

    internal ushort publisherId;

    /// <summary>
    ///  The publisher ID assigned by Databento, which denotes the dataset and venue
    /// </summary>
    public ushort PublisherId
    {
        get => this.publisherId;
        set
        {
            this.publisherId = value;
            Touch();
        }
    }

    internal uint instrumentId;

    /// <summary>
    ///  The numeric instrument ID
    /// </summary>
    public uint InstrumentId
    {
        get => this.instrumentId;
        set
        {
            this.instrumentId = value;
            Touch();
        }
    }

    internal ulong tsEvent;

    /// <summary>
    ///  The matching-engine-received timestamp expressed as the number of nanoseconds since the UNIX epoch
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="TsEventNullValue"/>. Setting <see langword="null"/> writes <see cref="TsEventNullValue"/>. This property has a precision of 100 nanoseconds, and <see cref="TsEventRaw"/> holds the exact value. Setting a local time converts it to UTC, and a time of unspecified kind is treated as UTC. A time outside the range of the field is set to the nearest limit of the range. A result equal to <see cref="TsEventNullValue"/> is moved one step to the nearest other value.
    /// </remarks>
    public DateTime? TsEvent
    {
        get => this.tsEvent == TsEventNullValue ? (DateTime?)null : Instant.ToDateTime(this.tsEvent, TsEventNanosecondsPerUnit);
        set
        {
            this.tsEvent = value is { } present ? Instant.ToRaw<ulong>(present, TsEventNanosecondsPerUnit, ulong.MinValue, ulong.MaxValue, TsEventNullValue) : TsEventNullValue;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="TsEvent"/>, as the unscaled integer that the field holds.
    /// </summary>
    public ulong TsEventRaw
    {
        get => this.tsEvent;
        set
        {
            this.tsEvent = value;
            Touch();
        }
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
    public bool IsRecognized => global::System.Enum.IsDefined(this.rtype);

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

    /// <summary>
    ///  Identifies each field, in wire order.
    /// </summary>
    public enum FieldName
    {
        /// <summary>
        ///  The length of the record in 32-bit words, including this header and any ts_out trailer
        /// </summary>
        RecordLength,
        /// <summary>
        ///  The record type, which selects the record that follows the header
        /// </summary>
        Rtype,
        /// <summary>
        ///  The publisher ID assigned by Databento, which denotes the dataset and venue
        /// </summary>
        PublisherId,
        /// <summary>
        ///  The numeric instrument ID
        /// </summary>
        InstrumentId,
        /// <summary>
        ///  The matching-engine-received timestamp expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        TsEvent,
    }

    /// <summary>
    ///  Provides access to the fields of one instance, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public readonly struct FieldSet : IEnumerable<FieldView>
    {
        private readonly RecordHeader owner;

        internal FieldSet(RecordHeader owner)
        {
            this.owner = owner;
        }

        /// <summary>
        ///  The length of the record in 32-bit words, including this header and any ts_out trailer
        /// </summary>
        public FieldView RecordLength => this[FieldName.RecordLength];

        /// <summary>
        ///  The record type, which selects the record that follows the header
        /// </summary>
        public FieldView Rtype => this[FieldName.Rtype];

        /// <summary>
        ///  The publisher ID assigned by Databento, which denotes the dataset and venue
        /// </summary>
        public FieldView PublisherId => this[FieldName.PublisherId];

        /// <summary>
        ///  The numeric instrument ID
        /// </summary>
        public FieldView InstrumentId => this[FieldName.InstrumentId];

        /// <summary>
        ///  The matching-engine-received timestamp expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public FieldView TsEvent => this[FieldName.TsEvent];

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
        new(nameof(FieldName.RecordLength), FieldKind.UnsignedInteger, RecordLengthLength) { IsSigned = false },
        new(nameof(FieldName.Rtype), FieldKind.Enum, RtypeLength) { IsSigned = false },
        new(nameof(FieldName.PublisherId), FieldKind.UnsignedInteger, PublisherIdLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.InstrumentId), FieldKind.UnsignedInteger, InstrumentIdLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.TsEvent), FieldKind.DateTime, TsEventLength) { Unit = "Nanoseconds", Epoch = "Unix", ByteOrder = "Little", IsSigned = false, Nullable = "Value", NullValue = TsEventNullValue },
    ];

    /// <summary>
    ///  Gets the fields in wire order, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public FieldSet Fields => new(this);

    IEnumerable<FieldView> IGroupEntry.Fields => new FieldSet(this);

    /// <summary>
    ///  Initializes a new instance of the <see cref="RecordHeader"/> class with default values.
    /// </summary>
    public RecordHeader()
    {
        Clear();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="RecordHeader"/> class with the values, bytes, and parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public RecordHeader(RecordHeader other) : this()
    {
        CopyFrom(other);
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(RecordHeader other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.recordLength = other.recordLength;
        this.rtype = other.rtype;
        this.publisherId = other.publisherId;
        this.instrumentId = other.instrumentId;
        this.tsEvent = other.tsEvent;
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
        this.recordLength = default;
        this.rtype = default;
        this.publisherId = default;
        this.instrumentId = default;
        this.tsEvent = default;
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
    public static RecordHeader Parse(ReadOnlySpan<byte> data)
    {
        var instance = new RecordHeader();
        instance.ParseFrom(data);
        return instance;
    }

    /// <summary>
    ///  Parses a new instance starting at the specified offset.
    /// </summary>
    /// <param name="data">The bytes to parse.</param>
    /// <param name="offset">The offset at which to start. A successful parse advances it past the parsed bytes, and a failed parse does not change it.</param>
    /// <returns>The new instance. Check Status to determine whether the parse succeeded.</returns>
    public static RecordHeader Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        var instance = new RecordHeader();
        instance.ParseFrom(data, ref offset);
        return instance;
    }

    /// <summary>
    ///  Parses the bytes into this instance, starting at the first byte.
    /// </summary>
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
        if (Truncated(data, start, cursor, RecordLengthLength, FieldName.RecordLength)) return;
        this.recordLength = data.Slice(cursor, RecordLengthLength)[0];
        cursor += RecordLengthLength;
        if (Truncated(data, start, cursor, RtypeLength, FieldName.Rtype)) return;
        this.rtype = (Rtype)data.Slice(cursor, RtypeLength)[0];
        cursor += RtypeLength;
        if (Truncated(data, start, cursor, PublisherIdLength, FieldName.PublisherId)) return;
        this.publisherId = BinaryPrimitives.ReadUInt16LittleEndian(data.Slice(cursor, PublisherIdLength));
        cursor += PublisherIdLength;
        if (Truncated(data, start, cursor, InstrumentIdLength, FieldName.InstrumentId)) return;
        this.instrumentId = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(cursor, InstrumentIdLength));
        cursor += InstrumentIdLength;
        if (Truncated(data, start, cursor, TsEventLength, FieldName.TsEvent)) return;
        this.tsEvent = BinaryPrimitives.ReadUInt64LittleEndian(data.Slice(cursor, TsEventLength));
        cursor += TsEventLength;
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
    public int Encode(Span<byte> data, int offset)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(offset);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, data.Length);
        if (data.Length - offset < MessageByteLength)
        {
            throw new ArgumentException("The span is too small for the encoded bytes.", nameof(data));
        }
        if (!((IGroupEntry)this).Write(data, offset, out var cursor, out var failure))
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
    public int Encode(Span<byte> data) => Encode(data, 0);

    /// <summary>
    ///  Encodes this instance into a new array.
    /// </summary>
    public byte[] ToBytes()
    {
        var buffer = new byte[MessageByteLength];
        Encode(buffer);
        return buffer;
    }

    /// <summary>
    ///  Tries to encode the entry into the span.
    /// </summary>
    /// <param name="data">The span that receives the bytes from its first byte.</param>
    /// <param name="written">When this method returns, contains the number of bytes written, or 0 if encoding failed.</param>
    /// <param name="failure">When this method returns <see langword="false"/>, contains the first field whose value cannot be encoded and the reason. It contains <see cref="EncodeStatus.BufferTooSmall"/> if the span is shorter than the encoded bytes. For a field in a group entry or nested message, it also identifies the group or field that holds it.</param>
    /// <returns><see langword="true"/> if the entry was written, or <see langword="false"/> if a value cannot be encoded or the span is too small.</returns>
    /// <remarks>
    ///  If a nested message has a trailer, the reason is <see cref="EncodeStatus.NestedTrailer"/>, and the field is the one holding the message.
    /// </remarks>
    public bool TryEncode(Span<byte> data, out int written, out EncodeFailure failure)
    {
        written = 0;
        if (data.Length < MessageByteLength)
        {
            failure = new EncodeFailure(EncodeStatus.BufferTooSmall);
            return false;
        }
        if (!((IGroupEntry)this).Write(data, 0, out var cursor, out failure))
        {
            return false;
        }
        written = cursor;
        return true;
    }

    bool IGroupEntry.Write(Span<byte> data, int offset, out int cursor, out EncodeFailure failure)
    {
        cursor = offset;
        data.Slice(cursor, RecordLengthLength)[0] = unchecked((byte)(this.recordLength));
        cursor += RecordLengthLength;
        data.Slice(cursor, RtypeLength)[0] = unchecked((byte)(this.rtype));
        cursor += RtypeLength;
        BinaryPrimitives.WriteUInt16LittleEndian(data.Slice(cursor, PublisherIdLength), this.publisherId);
        cursor += PublisherIdLength;
        BinaryPrimitives.WriteUInt32LittleEndian(data.Slice(cursor, InstrumentIdLength), this.instrumentId);
        cursor += InstrumentIdLength;
        BinaryPrimitives.WriteUInt64LittleEndian(data.Slice(cursor, TsEventLength), this.tsEvent);
        cursor += TsEventLength;
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
                if (!((IGroupEntry)this).Write(buffer, 0, out _, out _))
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
        if (!global::System.Enum.IsDefined(this.rtype))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.Rtype), ValidationStatus.UndeclaredValue));
        }
        return failures;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(RecordHeader? other)
    {
        if (other is null)
        {
            return false;
        }
        if (this.recordLength != other.recordLength) return false;
        if (this.rtype != other.rtype) return false;
        if (this.publisherId != other.publisherId) return false;
        if (this.instrumentId != other.instrumentId) return false;
        if (this.tsEvent != other.tsEvent) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is RecordHeader other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.recordLength);
        hash.Add(this.rtype);
        hash.Add(this.publisherId);
        hash.Add(this.instrumentId);
        hash.Add(this.tsEvent);
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
        private const int FieldCount = 5;

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
            if (Has(FieldName.RecordLength)) names.Add(nameof(FieldName.RecordLength));
            if (Has(FieldName.Rtype)) names.Add(nameof(FieldName.Rtype));
            if (Has(FieldName.PublisherId)) names.Add(nameof(FieldName.PublisherId));
            if (Has(FieldName.InstrumentId)) names.Add(nameof(FieldName.InstrumentId));
            if (Has(FieldName.TsEvent)) names.Add(nameof(FieldName.TsEvent));
            return names.Count == 0 ? "None" : string.Join(", ", names);
        }
    }

    /// <summary>
    ///  Compares this instance with another and identifies the fields that differ.
    /// </summary>
    /// <returns>The fields that differ, or None if there are no differences.</returns>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Changes Diff(RecordHeader other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (this.recordLength != other.recordLength) diff = diff.With(FieldName.RecordLength);
        if (this.rtype != other.rtype) diff = diff.With(FieldName.Rtype);
        if (this.publisherId != other.publisherId) diff = diff.With(FieldName.PublisherId);
        if (this.instrumentId != other.instrumentId) diff = diff.With(FieldName.InstrumentId);
        if (this.tsEvent != other.tsEvent) diff = diff.With(FieldName.TsEvent);
        return diff;
    }

    private void CopyField(FieldName field, RecordHeader from)
    {
        switch (field)
        {
            case FieldName.RecordLength:
                this.recordLength = from.recordLength;
                break;
            case FieldName.Rtype:
                this.rtype = from.rtype;
                break;
            case FieldName.PublisherId:
                this.publisherId = from.publisherId;
                break;
            case FieldName.InstrumentId:
                this.instrumentId = from.instrumentId;
                break;
            case FieldName.TsEvent:
                this.tsEvent = from.tsEvent;
                break;
            default:
                return;
        }
        Touch();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"RecordHeader {{ RecordLength: {(Text.Of(this.recordLength))}, Rtype: {(Text.Enum(this.rtype))}, PublisherId: {(Text.Of(this.publisherId))}, InstrumentId: {(Text.Of(this.instrumentId))}, TsEvent: {(Text.Instant(this.tsEvent, TsEventNanosecondsPerUnit, TsEventNullValue))} }}";

    private string DebuggerDisplay => $"[{(Text.Of(this.recordLength))}] [{(Text.Enum(this.rtype))}] [{(Text.Of(this.publisherId))}]";

    IFieldOwner? IFieldOwner.Owner
    {
        get => this.owner;
        set => this.owner = value;
    }

    FieldDescriptor[] IFieldOwner.Descriptors => Descriptors;

    void IFieldOwner.Touch() => Touch();

    string IFieldOwner.TextOf(int field) => (FieldName)field switch
    {
        FieldName.RecordLength => Text.Of(this.recordLength),
        FieldName.Rtype => Text.Enum(this.rtype),
        FieldName.PublisherId => Text.Of(this.publisherId),
        FieldName.InstrumentId => Text.Of(this.instrumentId),
        FieldName.TsEvent => Text.Instant(this.tsEvent, TsEventNanosecondsPerUnit, TsEventNullValue),
        _ => string.Empty,
    };

    bool IFieldOwner.IsRecognized(int field) => (FieldName)field switch
    {
        FieldName.Rtype => global::System.Enum.IsDefined(this.rtype),
        _ => true,
    };

    bool IFieldOwner.IsPresent(int field) => (FieldName)field switch
    {
        FieldName.TsEvent => this.tsEvent != TsEventNullValue,
        _ => true,
    };

    bool IFieldOwner.IsParsed(int field) => this.status == ParseStatus.Ok || field < (int?)this.failed;

    bool IFieldOwner.IsReached(int field) => this.modified || this.status is ParseStatus.None or ParseStatus.Ok || field <= (int?)this.failed;

    bool IFieldOwner.HasParseLayout(int field) => false;

    int IFieldOwner.OffsetOf(int field) => (FieldName)field switch
    {
        FieldName.RecordLength => RecordLengthOffset,
        FieldName.Rtype => RtypeOffset,
        FieldName.PublisherId => PublisherIdOffset,
        FieldName.InstrumentId => InstrumentIdOffset,
        FieldName.TsEvent => TsEventOffset,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    int IFieldOwner.LengthOf(int field) => (FieldName)field switch
    {
        FieldName.RecordLength => RecordLengthLength,
        FieldName.Rtype => RtypeLength,
        FieldName.PublisherId => PublisherIdLength,
        FieldName.InstrumentId => InstrumentIdLength,
        FieldName.TsEvent => TsEventLength,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    IReadOnlyList<IGroupEntry>? IFieldOwner.EntriesOf(int field) => null;

    IMessage? IFieldOwner.MessageOf(int field) => null;

    ulong[] IFieldOwner.ChangedAgainst(IFieldOwner other) => other is RecordHeader typed ? Diff(typed).ToWords() : throw new ArgumentException("The other instance is not of the same type.", nameof(other));

    void IFieldOwner.CopyField(int field, IFieldOwner from) => CopyField((FieldName)field, from as RecordHeader ?? throw new ArgumentException("The other instance is not of the same type.", nameof(from)));
}
