using System.Buffers.Binary;
using System.Diagnostics;
using static Nasdaq.NsmEquities.Orders.Ouch.v5_0.Rules;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Disable Order Entry Request Appendage
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class DisableOrderEntryRequestAppendage : IGroupEntry, IEquatable<DisableOrderEntryRequestAppendage>, IFieldOwner
{
    internal const int OptionalFieldLengthOffset = 0;

    internal const int OptionalFieldLengthLength = 1;

    internal const int DisableOrderEntryRequestOptionalFieldOffset = OptionalFieldLengthOffset + OptionalFieldLengthLength;

    internal const int DisableOrderEntryRequestOptionalFieldLength = 1;

    internal const int DisableOrderEntryRequestOptionalValueOffset = DisableOrderEntryRequestOptionalFieldOffset + DisableOrderEntryRequestOptionalFieldLength;

    private const byte UserRefIdxTag = 28;

    /// <summary>
    ///  The encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public const int MessageByteLength = OptionalFieldLengthLength + DisableOrderEntryRequestOptionalFieldLength;

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

    internal DisableOrderEntryRequestOptionalField disableOrderEntryRequestOptionalField;

    /// <summary>
    ///  Appendage Id
    /// </summary>
    public DisableOrderEntryRequestOptionalField DisableOrderEntryRequestOptionalField
    {
        get => this.disableOrderEntryRequestOptionalField;
        set
        {
            this.disableOrderEntryRequestOptionalField = value;
            Touch();
        }
    }

    internal DisableOrderEntryRequestOptionalValue disableOrderEntryRequestOptionalValue = new DisableOrderEntryRequestOptionalValue();

    /// <summary>
    ///  Branch
    /// </summary>
    /// <remarks>
    ///  Setting <see langword="null"/> stores the value that Clear stores.
    /// </remarks>
    public DisableOrderEntryRequestOptionalValue DisableOrderEntryRequestOptionalValue
    {
        get => this.disableOrderEntryRequestOptionalValue;
        set
        {
            var copy = value is null ? new DisableOrderEntryRequestOptionalValue() : new DisableOrderEntryRequestOptionalValue(value);
            ((IFieldOwner)this.disableOrderEntryRequestOptionalValue).Owner = null;
            this.disableOrderEntryRequestOptionalValue = copy;
            ((IFieldOwner)this.disableOrderEntryRequestOptionalValue).Owner = this;
            Touch();
        }
    }

    private DisableOrderEntryRequestOptionalValue.FieldName? DisableOrderEntryRequestOptionalValueCase()
    {
        bool DisableOrderEntryRequestOptionalValueBranchRule0Selected;
        {
            const byte DisableOrderEntryRequestOptionalValueBranchRule0Data2 = 28;
            {
                byte disableOrderEntryRequestOptionalFieldValue = (byte)this.disableOrderEntryRequestOptionalField;
                DisableOrderEntryRequestOptionalValueBranchRule0Selected = (disableOrderEntryRequestOptionalFieldValue == DisableOrderEntryRequestOptionalValueBranchRule0Data2);
            }
            if (DisableOrderEntryRequestOptionalValueBranchRule0Selected)
            {
                return DisableOrderEntryRequestOptionalValue.FieldName.UserRefIdx;
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
            position += DisableOrderEntryRequestOptionalFieldLength;
            position += this.disableOrderEntryRequestOptionalValue.EncodedByteLength;
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
    public bool IsRecognized => global::System.Enum.IsDefined(this.disableOrderEntryRequestOptionalField) && this.disableOrderEntryRequestOptionalValue.IsRecognized;

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
        DisableOrderEntryRequestOptionalField,
        /// <summary>
        ///  Branch
        /// </summary>
        DisableOrderEntryRequestOptionalValue,
    }

    /// <summary>
    ///  Provides access to the fields of one instance, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public readonly struct FieldSet : IEnumerable<FieldView>
    {
        private readonly DisableOrderEntryRequestAppendage owner;

        internal FieldSet(DisableOrderEntryRequestAppendage owner)
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
        public FieldView DisableOrderEntryRequestOptionalField => this[FieldName.DisableOrderEntryRequestOptionalField];

        /// <summary>
        ///  Branch
        /// </summary>
        public FieldView DisableOrderEntryRequestOptionalValue => this[FieldName.DisableOrderEntryRequestOptionalValue];

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
        new(nameof(FieldName.DisableOrderEntryRequestOptionalField), FieldKind.Enum, DisableOrderEntryRequestOptionalFieldLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.DisableOrderEntryRequestOptionalValue), FieldKind.Struct, null),
    ];

    /// <summary>
    ///  Gets the fields in wire order, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public FieldSet Fields => new(this);

    IEnumerable<FieldView> IGroupEntry.Fields => new FieldSet(this);

    /// <summary>
    ///  Initializes a new instance of the <see cref="DisableOrderEntryRequestAppendage"/> class with default values.
    /// </summary>
    public DisableOrderEntryRequestAppendage()
    {
        Clear();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="DisableOrderEntryRequestAppendage"/> class with the values, bytes, and parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public DisableOrderEntryRequestAppendage(DisableOrderEntryRequestAppendage other) : this()
    {
        CopyFrom(other);
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(DisableOrderEntryRequestAppendage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.optionalFieldLength = other.optionalFieldLength;
        this.disableOrderEntryRequestOptionalField = other.disableOrderEntryRequestOptionalField;
        ((IFieldOwner)this.disableOrderEntryRequestOptionalValue).Owner = null;
        this.disableOrderEntryRequestOptionalValue = new DisableOrderEntryRequestOptionalValue(other.disableOrderEntryRequestOptionalValue);
        ((IFieldOwner)this.disableOrderEntryRequestOptionalValue).Owner = this;
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
        this.disableOrderEntryRequestOptionalField = default;
        ((IFieldOwner)this.disableOrderEntryRequestOptionalValue).Owner = null;
        this.disableOrderEntryRequestOptionalValue = new DisableOrderEntryRequestOptionalValue();
        ((IFieldOwner)this.disableOrderEntryRequestOptionalValue).Owner = this;
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
    public static DisableOrderEntryRequestAppendage Parse(ReadOnlySpan<byte> data)
    {
        var instance = new DisableOrderEntryRequestAppendage();
        instance.ParseFrom(data);
        return instance;
    }

    /// <summary>
    ///  Parses a new instance starting at the specified offset.
    /// </summary>
    /// <param name="data">The bytes to parse.</param>
    /// <param name="offset">The offset at which to start. A successful parse advances it past the parsed bytes, and a failed parse does not change it.</param>
    /// <returns>The new instance. Check Status to determine whether the parse succeeded.</returns>
    public static DisableOrderEntryRequestAppendage Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        var instance = new DisableOrderEntryRequestAppendage();
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
            int DisableOrderEntryRequestAppendageSizeRule1End;
            global::System.Int128 DisableOrderEntryRequestAppendageSizeRule1Width;
            byte disableOrderEntryRequestAppendageOptionalFieldLengthValue = this.optionalFieldLength;
            int? disableOrderEntryRequestAppendageEnd = null;
            const long DisableOrderEntryRequestAppendageSizeRule1Data2 = 1;
            DisableOrderEntryRequestAppendageSizeRule1Width = checked((checked((global::System.Int128)(disableOrderEntryRequestAppendageOptionalFieldLengthValue)) + ((global::System.Int128)(DisableOrderEntryRequestAppendageSizeRule1Data2))));
            if (DisableOrderEntryRequestAppendageSizeRule1Width < RuleEmptyWidth || DisableOrderEntryRequestAppendageSizeRule1Width > (long)data.Length - start) { Fail(ParseStatus.Truncated, FieldName.OptionalFieldLength, OptionalFieldLengthOffset, data[start..]); return; }
            DisableOrderEntryRequestAppendageSizeRule1End = start + (int)DisableOrderEntryRequestAppendageSizeRule1Width;
            disableOrderEntryRequestAppendageEnd = DisableOrderEntryRequestAppendageSizeRule1End;
            if (DisableOrderEntryRequestAppendageSizeRule1End < cursor) { Fail(ParseStatus.Truncated, FieldName.OptionalFieldLength, OptionalFieldLengthOffset, data[start..]); return; }
            if (disableOrderEntryRequestAppendageEnd is int disableOrderEntryRequestAppendageDeclaredEnd)
            {
                data = data[..disableOrderEntryRequestAppendageDeclaredEnd];
            }
        }
        if (Truncated(data, start, cursor, DisableOrderEntryRequestOptionalFieldLength, FieldName.DisableOrderEntryRequestOptionalField)) return;
        this.disableOrderEntryRequestOptionalField = (DisableOrderEntryRequestOptionalField)data.Slice(cursor, DisableOrderEntryRequestOptionalFieldLength)[0];
        cursor += DisableOrderEntryRequestOptionalFieldLength;
        {
            var instance = new DisableOrderEntryRequestOptionalValue();
            instance.ParseFrom(data, ref cursor, this.DisableOrderEntryRequestOptionalValueCase());
            this.disableOrderEntryRequestOptionalValue = instance;
            ((IFieldOwner)this.disableOrderEntryRequestOptionalValue).Owner = this;
            if (this.disableOrderEntryRequestOptionalValue.Status != ParseStatus.Ok)
            {
                Fail(this.disableOrderEntryRequestOptionalValue.Status, FieldName.DisableOrderEntryRequestOptionalValue, cursor - start + this.disableOrderEntryRequestOptionalValue.FailedOffset, data[start..]);
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
        if (this.disableOrderEntryRequestOptionalValue.selectedCase == DisableOrderEntryRequestOptionalValue.FieldName.UserRefIdx)
        {
            this.disableOrderEntryRequestOptionalField = (DisableOrderEntryRequestOptionalField)UserRefIdxTag;
        }
        else if (this.disableOrderEntryRequestOptionalValue.selectedCase is null)
        {
            if ((this.DisableOrderEntryRequestOptionalValueCase() == DisableOrderEntryRequestOptionalValue.FieldName.UserRefIdx) != (this.disableOrderEntryRequestOptionalValue.selectedCase == DisableOrderEntryRequestOptionalValue.FieldName.UserRefIdx))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.DisableOrderEntryRequestOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        {
            const long DisableOrderEntryRequestAppendageSizeRule1Data2 = 1;
            global::System.Int128 measure = EncodedByteLength;
            global::System.Int128 disableOrderEntryRequestAppendageOptionalFieldLengthStampedValue;
            try
            {
                disableOrderEntryRequestAppendageOptionalFieldLengthStampedValue = checked((measure - checked((((global::System.Int128)(RuleNumberZero)) + ((global::System.Int128)(DisableOrderEntryRequestAppendageSizeRule1Data2))))));
            }
            catch (global::System.OverflowException)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.OptionalFieldLength), EncodeStatus.RuleMismatch);
                return false;
            }
            if (disableOrderEntryRequestAppendageOptionalFieldLengthStampedValue > byte.MaxValue)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.OptionalFieldLength), EncodeStatus.TooLong);
                return false;
            }
            if (disableOrderEntryRequestAppendageOptionalFieldLengthStampedValue < byte.MinValue)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.OptionalFieldLength), EncodeStatus.DoesNotFit);
                return false;
            }
            this.optionalFieldLength = checked((byte)(disableOrderEntryRequestAppendageOptionalFieldLengthStampedValue));
        }
        data.Slice(cursor, OptionalFieldLengthLength)[0] = unchecked((byte)(this.optionalFieldLength));
        cursor += OptionalFieldLengthLength;
        data.Slice(cursor, DisableOrderEntryRequestOptionalFieldLength)[0] = unchecked((byte)(this.disableOrderEntryRequestOptionalField));
        cursor += DisableOrderEntryRequestOptionalFieldLength;
        {
            if (!((IGroupEntry)this.disableOrderEntryRequestOptionalValue).Write(data, cursor, out cursor, out var memberFailure))
            {
                failure = memberFailure.Within(new FieldView(this, (int)FieldName.DisableOrderEntryRequestOptionalValue), null);
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
        if (!global::System.Enum.IsDefined(this.disableOrderEntryRequestOptionalField))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.DisableOrderEntryRequestOptionalField), ValidationStatus.UndeclaredValue));
        }
        return failures;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(DisableOrderEntryRequestAppendage? other)
    {
        if (other is null)
        {
            return false;
        }
        if (this.disableOrderEntryRequestOptionalField != other.disableOrderEntryRequestOptionalField) return false;
        if (!this.disableOrderEntryRequestOptionalValue.Equals(other.disableOrderEntryRequestOptionalValue)) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is DisableOrderEntryRequestAppendage other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.disableOrderEntryRequestOptionalField);
        hash.Add(this.disableOrderEntryRequestOptionalValue);
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
            if (Has(FieldName.DisableOrderEntryRequestOptionalField)) names.Add(nameof(FieldName.DisableOrderEntryRequestOptionalField));
            if (Has(FieldName.DisableOrderEntryRequestOptionalValue)) names.Add(nameof(FieldName.DisableOrderEntryRequestOptionalValue));
            return names.Count == 0 ? "None" : string.Join(", ", names);
        }
    }

    /// <summary>
    ///  Compares this instance with another and identifies the fields that differ.
    /// </summary>
    /// <returns>The fields that differ, or None if there are no differences.</returns>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Changes Diff(DisableOrderEntryRequestAppendage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (this.disableOrderEntryRequestOptionalField != other.disableOrderEntryRequestOptionalField) diff = diff.With(FieldName.DisableOrderEntryRequestOptionalField);
        if (!this.disableOrderEntryRequestOptionalValue.Equals(other.disableOrderEntryRequestOptionalValue)) diff = diff.With(FieldName.DisableOrderEntryRequestOptionalValue);
        return diff;
    }

    private void CopyField(FieldName field, DisableOrderEntryRequestAppendage from)
    {
        switch (field)
        {
            case FieldName.OptionalFieldLength:
                this.optionalFieldLength = from.optionalFieldLength;
                break;
            case FieldName.DisableOrderEntryRequestOptionalField:
                this.disableOrderEntryRequestOptionalField = from.disableOrderEntryRequestOptionalField;
                break;
            case FieldName.DisableOrderEntryRequestOptionalValue:
                ((IFieldOwner)this.disableOrderEntryRequestOptionalValue).Owner = null;
                this.disableOrderEntryRequestOptionalValue = new DisableOrderEntryRequestOptionalValue(from.disableOrderEntryRequestOptionalValue);
                ((IFieldOwner)this.disableOrderEntryRequestOptionalValue).Owner = this;
                break;
            default:
                return;
        }
        Touch();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"DisableOrderEntryRequestAppendage {{ OptionalFieldLength: {(Text.Of(this.optionalFieldLength))}, DisableOrderEntryRequestOptionalField: {(Text.Enum(this.disableOrderEntryRequestOptionalField))}, DisableOrderEntryRequestOptionalValue: {(Text.Of(this.disableOrderEntryRequestOptionalValue.ToString()))} }}{(this.trailer.Length == 0 ? string.Empty : $" Trailer: {(Text.Of(this.trailer.Length))} bytes")}";

    private string DebuggerDisplay => $"[{(Text.Of(this.optionalFieldLength))}] [{(Text.Enum(this.disableOrderEntryRequestOptionalField))}]";

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
        FieldName.DisableOrderEntryRequestOptionalField => Text.Enum(this.disableOrderEntryRequestOptionalField),
        FieldName.DisableOrderEntryRequestOptionalValue => Text.Of(this.disableOrderEntryRequestOptionalValue.ToString()),
        _ => string.Empty,
    };

    bool IFieldOwner.IsRecognized(int field) => (FieldName)field switch
    {
        FieldName.DisableOrderEntryRequestOptionalField => global::System.Enum.IsDefined(this.disableOrderEntryRequestOptionalField),
        FieldName.DisableOrderEntryRequestOptionalValue => this.disableOrderEntryRequestOptionalValue.IsRecognized,
        _ => true,
    };

    bool IFieldOwner.IsPresent(int field) => true;

    bool IFieldOwner.IsParsed(int field) => this.status == ParseStatus.Ok || field < (int?)this.failed;

    bool IFieldOwner.IsReached(int field) => this.modified || this.status is ParseStatus.None or ParseStatus.Ok || field <= (int?)this.failed;

    bool IFieldOwner.HasParseLayout(int field) => false;

    int IFieldOwner.OffsetOf(int field) => (FieldName)field switch
    {
        FieldName.OptionalFieldLength => OptionalFieldLengthOffset,
        FieldName.DisableOrderEntryRequestOptionalField => DisableOrderEntryRequestOptionalFieldOffset,
        FieldName.DisableOrderEntryRequestOptionalValue => DisableOrderEntryRequestOptionalValueOffset,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    int IFieldOwner.LengthOf(int field) => (FieldName)field switch
    {
        FieldName.OptionalFieldLength => OptionalFieldLengthLength,
        FieldName.DisableOrderEntryRequestOptionalField => DisableOrderEntryRequestOptionalFieldLength,
        FieldName.DisableOrderEntryRequestOptionalValue => this.disableOrderEntryRequestOptionalValue.EncodedByteLength,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    IReadOnlyList<IGroupEntry>? IFieldOwner.EntriesOf(int field) => null;

    IMessage? IFieldOwner.MessageOf(int field) => null;

    ulong[] IFieldOwner.ChangedAgainst(IFieldOwner other) => other is DisableOrderEntryRequestAppendage typed ? Diff(typed).ToWords() : throw new ArgumentException("The other instance is not of the same type.", nameof(other));

    void IFieldOwner.CopyField(int field, IFieldOwner from) => CopyField((FieldName)field, from as DisableOrderEntryRequestAppendage ?? throw new ArgumentException("The other instance is not of the same type.", nameof(from)));
}
