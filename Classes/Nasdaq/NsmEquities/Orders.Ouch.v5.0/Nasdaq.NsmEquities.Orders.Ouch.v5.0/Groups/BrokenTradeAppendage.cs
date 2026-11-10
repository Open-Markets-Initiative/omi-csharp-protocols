using System.Buffers.Binary;
using System.Diagnostics;
using static Nasdaq.NsmEquities.Orders.Ouch.v5_0.Rules;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Broken Trade Appendage
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class BrokenTradeAppendage : IGroupEntry, IEquatable<BrokenTradeAppendage>, IFieldOwner
{
    internal const int OptionalFieldLengthOffset = 0;

    internal const int OptionalFieldLengthLength = 1;

    internal const int BrokenTradeOptionalFieldOffset = OptionalFieldLengthOffset + OptionalFieldLengthLength;

    internal const int BrokenTradeOptionalFieldLength = 1;

    internal const int BrokenTradeOptionalValueOffset = BrokenTradeOptionalFieldOffset + BrokenTradeOptionalFieldLength;

    private const byte UserRefIdxTag = 28;

    /// <summary>
    ///  The encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public const int MessageByteLength = OptionalFieldLengthLength + BrokenTradeOptionalFieldLength;

    internal byte optionalFieldLength;

    /// <summary>
    ///  Appendage Length Type
    /// </summary>
    public byte OptionalFieldLength
    {
        get => this.optionalFieldLength;
        set
        {
            this.optionalFieldLength = value;
            Touch();
        }
    }

    internal BrokenTradeOptionalField brokenTradeOptionalField;

    /// <summary>
    ///  Appendage Id
    /// </summary>
    public BrokenTradeOptionalField BrokenTradeOptionalField
    {
        get => this.brokenTradeOptionalField;
        set
        {
            this.brokenTradeOptionalField = value;
            Touch();
        }
    }

    internal BrokenTradeOptionalValue brokenTradeOptionalValue = new BrokenTradeOptionalValue();

    /// <summary>
    ///  Branch
    /// </summary>
    /// <remarks>
    ///  Setting <see langword="null"/> stores the value that Clear stores.
    /// </remarks>
    public BrokenTradeOptionalValue BrokenTradeOptionalValue
    {
        get => this.brokenTradeOptionalValue;
        set
        {
            var copy = value is null ? new BrokenTradeOptionalValue() : new BrokenTradeOptionalValue(value);
            ((IFieldOwner)this.brokenTradeOptionalValue).Owner = null;
            this.brokenTradeOptionalValue = copy;
            ((IFieldOwner)this.brokenTradeOptionalValue).Owner = this;
            Touch();
        }
    }

    private BrokenTradeOptionalValue.FieldName? BrokenTradeOptionalValueCase()
    {
        bool BrokenTradeOptionalValueBranchRule0Selected;
        {
            const byte BrokenTradeOptionalValueBranchRule0Data2 = 28;
            {
                byte brokenTradeOptionalFieldValue = (byte)this.brokenTradeOptionalField;
                BrokenTradeOptionalValueBranchRule0Selected = (brokenTradeOptionalFieldValue == BrokenTradeOptionalValueBranchRule0Data2);
            }
            if (BrokenTradeOptionalValueBranchRule0Selected)
            {
                return BrokenTradeOptionalValue.FieldName.UserRefIdx;
            }
        }
        return null;
    }

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
            position += OptionalFieldLengthLength;
            position += BrokenTradeOptionalFieldLength;
            position += this.brokenTradeOptionalValue.EncodedByteLength;
            position += this.trailer.Length;
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
    public bool IsRecognized => global::System.Enum.IsDefined(this.brokenTradeOptionalField) && this.brokenTradeOptionalValue.IsRecognized;

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

    private byte[] trailer = [];

    /// <summary>
    ///  Gets or sets any extra bytes that follow the entry within its stated length.
    /// </summary>
    /// <remarks>
    ///  Encode writes these bytes after the fields of the entry. Setting this property marks the entry as modified.
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

    /// <summary>
    ///  Identifies each field, in wire order.
    /// </summary>
    public enum FieldName
    {
        /// <summary>
        ///  Appendage Length Type
        /// </summary>
        OptionalFieldLength,
        /// <summary>
        ///  Appendage Id
        /// </summary>
        BrokenTradeOptionalField,
        /// <summary>
        ///  Branch
        /// </summary>
        BrokenTradeOptionalValue,
    }

    /// <summary>
    ///  Provides access to the fields of one instance, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public readonly struct FieldSet : IEnumerable<FieldView>
    {
        private readonly BrokenTradeAppendage owner;

        internal FieldSet(BrokenTradeAppendage owner)
        {
            this.owner = owner;
        }

        /// <summary>
        ///  Appendage Length Type
        /// </summary>
        public FieldView OptionalFieldLength => this[FieldName.OptionalFieldLength];

        /// <summary>
        ///  Appendage Id
        /// </summary>
        public FieldView BrokenTradeOptionalField => this[FieldName.BrokenTradeOptionalField];

        /// <summary>
        ///  Branch
        /// </summary>
        public FieldView BrokenTradeOptionalValue => this[FieldName.BrokenTradeOptionalValue];

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
        new(nameof(FieldName.OptionalFieldLength), FieldKind.UnsignedInteger, OptionalFieldLengthLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.BrokenTradeOptionalField), FieldKind.Enum, BrokenTradeOptionalFieldLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.BrokenTradeOptionalValue), FieldKind.Struct, null),
    ];

    /// <summary>
    ///  Gets the fields in wire order, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public FieldSet Fields => new(this);

    IEnumerable<FieldView> IGroupEntry.Fields => new FieldSet(this);

    /// <summary>
    ///  Initializes a new instance of the <see cref="BrokenTradeAppendage"/> class with default values.
    /// </summary>
    public BrokenTradeAppendage()
    {
        Clear();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="BrokenTradeAppendage"/> class with the values, bytes, and parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public BrokenTradeAppendage(BrokenTradeAppendage other) : this()
    {
        CopyFrom(other);
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(BrokenTradeAppendage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.optionalFieldLength = other.optionalFieldLength;
        this.brokenTradeOptionalField = other.brokenTradeOptionalField;
        ((IFieldOwner)this.brokenTradeOptionalValue).Owner = null;
        this.brokenTradeOptionalValue = new BrokenTradeOptionalValue(other.brokenTradeOptionalValue);
        ((IFieldOwner)this.brokenTradeOptionalValue).Owner = this;
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
        this.optionalFieldLength = default;
        this.brokenTradeOptionalField = default;
        ((IFieldOwner)this.brokenTradeOptionalValue).Owner = null;
        this.brokenTradeOptionalValue = new BrokenTradeOptionalValue();
        ((IFieldOwner)this.brokenTradeOptionalValue).Owner = this;
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
    public static BrokenTradeAppendage Parse(ReadOnlySpan<byte> data)
    {
        var instance = new BrokenTradeAppendage();
        instance.ParseFrom(data);
        return instance;
    }

    /// <summary>
    ///  Parses a new instance starting at the specified offset.
    /// </summary>
    /// <param name="data">The bytes to parse.</param>
    /// <param name="offset">The offset at which to start. A successful parse advances it past the parsed bytes, and a failed parse does not change it.</param>
    /// <returns>The new instance. Check Status to determine whether the parse succeeded.</returns>
    public static BrokenTradeAppendage Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        var instance = new BrokenTradeAppendage();
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
        if (Truncated(data, start, cursor, OptionalFieldLengthLength, FieldName.OptionalFieldLength)) return;
        this.optionalFieldLength = data.Slice(cursor, OptionalFieldLengthLength)[0];
        cursor += OptionalFieldLengthLength;
        {
            int BrokenTradeAppendageSizeRule1End;
            global::System.Int128 BrokenTradeAppendageSizeRule1Width;
            byte brokenTradeAppendageOptionalFieldLengthValue = this.optionalFieldLength;
            int? brokenTradeAppendageEnd = null;
            const long BrokenTradeAppendageSizeRule1Data2 = 1;
            BrokenTradeAppendageSizeRule1Width = checked((checked((global::System.Int128)(brokenTradeAppendageOptionalFieldLengthValue)) + ((global::System.Int128)(BrokenTradeAppendageSizeRule1Data2))));
            if (BrokenTradeAppendageSizeRule1Width < RuleEmptyWidth || BrokenTradeAppendageSizeRule1Width > (long)data.Length - start) { Fail(ParseStatus.Truncated, FieldName.OptionalFieldLength, OptionalFieldLengthOffset, data[start..]); return; }
            BrokenTradeAppendageSizeRule1End = start + (int)BrokenTradeAppendageSizeRule1Width;
            brokenTradeAppendageEnd = BrokenTradeAppendageSizeRule1End;
            if (BrokenTradeAppendageSizeRule1End < cursor) { Fail(ParseStatus.Truncated, FieldName.OptionalFieldLength, OptionalFieldLengthOffset, data[start..]); return; }
            if (brokenTradeAppendageEnd is int brokenTradeAppendageDeclaredEnd)
            {
                data = data[..brokenTradeAppendageDeclaredEnd];
            }
        }
        if (Truncated(data, start, cursor, BrokenTradeOptionalFieldLength, FieldName.BrokenTradeOptionalField)) return;
        this.brokenTradeOptionalField = (BrokenTradeOptionalField)data.Slice(cursor, BrokenTradeOptionalFieldLength)[0];
        cursor += BrokenTradeOptionalFieldLength;
        {
            var instance = new BrokenTradeOptionalValue();
            instance.ParseFrom(data, ref cursor, this.BrokenTradeOptionalValueCase());
            this.brokenTradeOptionalValue = instance;
            ((IFieldOwner)this.brokenTradeOptionalValue).Owner = this;
            if (this.brokenTradeOptionalValue.Status != ParseStatus.Ok)
            {
                Fail(this.brokenTradeOptionalValue.Status, FieldName.BrokenTradeOptionalValue, cursor - start + this.brokenTradeOptionalValue.FailedOffset, data[start..]);
                return;
            }
        }
        this.trailer = data[cursor..].ToArray();
        cursor = data.Length;
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
    public int Encode(Span<byte> data, int offset)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(offset);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, data.Length);
        if (data.Length - offset < EncodedByteLength)
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
    /// <exception cref="EncodeException">A value does not fit its field.</exception>
    /// <exception cref="ArgumentException">The span is too small for the encoded bytes.</exception>
    public int Encode(Span<byte> data) => Encode(data, 0);

    /// <summary>
    ///  Encodes this instance into a new array.
    /// </summary>
    /// <exception cref="EncodeException">A value does not fit its field.</exception>
    public byte[] ToBytes()
    {
        var buffer = new byte[EncodedByteLength];
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
        if (data.Length < EncodedByteLength)
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
        if (this.brokenTradeOptionalValue.selectedCase == BrokenTradeOptionalValue.FieldName.UserRefIdx)
        {
            this.brokenTradeOptionalField = (BrokenTradeOptionalField)UserRefIdxTag;
        }
        else if (this.brokenTradeOptionalValue.selectedCase is null)
        {
            if ((this.BrokenTradeOptionalValueCase() == BrokenTradeOptionalValue.FieldName.UserRefIdx) != (this.brokenTradeOptionalValue.selectedCase == BrokenTradeOptionalValue.FieldName.UserRefIdx))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.BrokenTradeOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        {
            const long BrokenTradeAppendageSizeRule1Data2 = 1;
            global::System.Int128 measure = EncodedByteLength;
            global::System.Int128 brokenTradeAppendageOptionalFieldLengthStampedValue;
            try
            {
                brokenTradeAppendageOptionalFieldLengthStampedValue = checked((measure - checked((((global::System.Int128)(RuleNumberZero)) + ((global::System.Int128)(BrokenTradeAppendageSizeRule1Data2))))));
            }
            catch (global::System.OverflowException)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.OptionalFieldLength), EncodeStatus.RuleMismatch);
                return false;
            }
            if (brokenTradeAppendageOptionalFieldLengthStampedValue > byte.MaxValue)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.OptionalFieldLength), EncodeStatus.TooLong);
                return false;
            }
            if (brokenTradeAppendageOptionalFieldLengthStampedValue < byte.MinValue)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.OptionalFieldLength), EncodeStatus.DoesNotFit);
                return false;
            }
            this.optionalFieldLength = checked((byte)(brokenTradeAppendageOptionalFieldLengthStampedValue));
        }
        data.Slice(cursor, OptionalFieldLengthLength)[0] = unchecked((byte)(this.optionalFieldLength));
        cursor += OptionalFieldLengthLength;
        data.Slice(cursor, BrokenTradeOptionalFieldLength)[0] = unchecked((byte)(this.brokenTradeOptionalField));
        cursor += BrokenTradeOptionalFieldLength;
        {
            if (!((IGroupEntry)this.brokenTradeOptionalValue).Write(data, cursor, out cursor, out var memberFailure))
            {
                failure = memberFailure.Within(new FieldView(this, (int)FieldName.BrokenTradeOptionalValue), null);
                return false;
            }
        }
        this.trailer.CopyTo(data[cursor..]);
        cursor += this.trailer.Length;
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
        if (!global::System.Enum.IsDefined(this.brokenTradeOptionalField))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.BrokenTradeOptionalField), ValidationStatus.UndeclaredValue));
        }
        return failures;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(BrokenTradeAppendage? other)
    {
        if (other is null)
        {
            return false;
        }
        if (this.brokenTradeOptionalField != other.brokenTradeOptionalField) return false;
        if (!this.brokenTradeOptionalValue.Equals(other.brokenTradeOptionalValue)) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is BrokenTradeAppendage other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.brokenTradeOptionalField);
        hash.Add(this.brokenTradeOptionalValue);
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
        private const int FieldCount = 3;

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
            if (Has(FieldName.OptionalFieldLength)) names.Add(nameof(FieldName.OptionalFieldLength));
            if (Has(FieldName.BrokenTradeOptionalField)) names.Add(nameof(FieldName.BrokenTradeOptionalField));
            if (Has(FieldName.BrokenTradeOptionalValue)) names.Add(nameof(FieldName.BrokenTradeOptionalValue));
            return names.Count == 0 ? "None" : string.Join(", ", names);
        }
    }

    /// <summary>
    ///  Compares this instance with another and identifies the fields that differ.
    /// </summary>
    /// <returns>The fields that differ, or None if there are no differences.</returns>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Changes Diff(BrokenTradeAppendage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (this.brokenTradeOptionalField != other.brokenTradeOptionalField) diff = diff.With(FieldName.BrokenTradeOptionalField);
        if (!this.brokenTradeOptionalValue.Equals(other.brokenTradeOptionalValue)) diff = diff.With(FieldName.BrokenTradeOptionalValue);
        return diff;
    }

    private void CopyField(FieldName field, BrokenTradeAppendage from)
    {
        switch (field)
        {
            case FieldName.OptionalFieldLength:
                this.optionalFieldLength = from.optionalFieldLength;
                break;
            case FieldName.BrokenTradeOptionalField:
                this.brokenTradeOptionalField = from.brokenTradeOptionalField;
                break;
            case FieldName.BrokenTradeOptionalValue:
                ((IFieldOwner)this.brokenTradeOptionalValue).Owner = null;
                this.brokenTradeOptionalValue = new BrokenTradeOptionalValue(from.brokenTradeOptionalValue);
                ((IFieldOwner)this.brokenTradeOptionalValue).Owner = this;
                break;
            default:
                return;
        }
        Touch();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"BrokenTradeAppendage {{ OptionalFieldLength: {(Text.Of(this.optionalFieldLength))}, BrokenTradeOptionalField: {(Text.Enum(this.brokenTradeOptionalField))}, BrokenTradeOptionalValue: {(Text.Of(this.brokenTradeOptionalValue.ToString()))} }}{(this.trailer.Length == 0 ? string.Empty : $" Trailer: {(Text.Of(this.trailer.Length))} bytes")}";

    private string DebuggerDisplay => $"[{(Text.Of(this.optionalFieldLength))}] [{(Text.Enum(this.brokenTradeOptionalField))}]";

    IFieldOwner? IFieldOwner.Owner
    {
        get => this.owner;
        set => this.owner = value;
    }

    FieldDescriptor[] IFieldOwner.Descriptors => Descriptors;

    void IFieldOwner.Touch() => Touch();

    string IFieldOwner.TextOf(int field) => (FieldName)field switch
    {
        FieldName.OptionalFieldLength => Text.Of(this.optionalFieldLength),
        FieldName.BrokenTradeOptionalField => Text.Enum(this.brokenTradeOptionalField),
        FieldName.BrokenTradeOptionalValue => Text.Of(this.brokenTradeOptionalValue.ToString()),
        _ => string.Empty,
    };

    bool IFieldOwner.IsRecognized(int field) => (FieldName)field switch
    {
        FieldName.BrokenTradeOptionalField => global::System.Enum.IsDefined(this.brokenTradeOptionalField),
        FieldName.BrokenTradeOptionalValue => this.brokenTradeOptionalValue.IsRecognized,
        _ => true,
    };

    bool IFieldOwner.IsPresent(int field) => true;

    bool IFieldOwner.IsParsed(int field) => this.status == ParseStatus.Ok || field < (int?)this.failed;

    bool IFieldOwner.IsReached(int field) => this.modified || this.status is ParseStatus.None or ParseStatus.Ok || field <= (int?)this.failed;

    bool IFieldOwner.HasParseLayout(int field) => false;

    int IFieldOwner.OffsetOf(int field) => (FieldName)field switch
    {
        FieldName.OptionalFieldLength => OptionalFieldLengthOffset,
        FieldName.BrokenTradeOptionalField => BrokenTradeOptionalFieldOffset,
        FieldName.BrokenTradeOptionalValue => BrokenTradeOptionalValueOffset,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    int IFieldOwner.LengthOf(int field) => (FieldName)field switch
    {
        FieldName.OptionalFieldLength => OptionalFieldLengthLength,
        FieldName.BrokenTradeOptionalField => BrokenTradeOptionalFieldLength,
        FieldName.BrokenTradeOptionalValue => this.brokenTradeOptionalValue.EncodedByteLength,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    IReadOnlyList<IGroupEntry>? IFieldOwner.EntriesOf(int field) => null;

    IMessage? IFieldOwner.MessageOf(int field) => null;

    ulong[] IFieldOwner.ChangedAgainst(IFieldOwner other) => other is BrokenTradeAppendage typed ? Diff(typed).ToWords() : throw new ArgumentException("The other instance is not of the same type.", nameof(other));

    void IFieldOwner.CopyField(int field, IFieldOwner from) => CopyField((FieldName)field, from as BrokenTradeAppendage ?? throw new ArgumentException("The other instance is not of the same type.", nameof(from)));
}
