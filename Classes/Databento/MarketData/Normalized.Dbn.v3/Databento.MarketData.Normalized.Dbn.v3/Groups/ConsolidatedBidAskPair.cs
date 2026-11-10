using System.Buffers.Binary;
using System.Diagnostics;

namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  A price level consolidated from multiple venues
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class ConsolidatedBidAskPair : IGroupEntry, IEquatable<ConsolidatedBidAskPair>, IFieldOwner
{
    internal const int BidPxOffset = 0;

    internal const int BidPxLength = 8;

    /// <summary>
    ///  The number of decimal places in Bid Px.
    /// </summary>
    public const byte BidPxScale = 9;

    /// <summary>
    ///  The factor by which <see cref="BidPx"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal BidPxFactor = 1000000000m;

    /// <summary>
    ///  The encoded value of Bid Px that means no value.
    /// </summary>
    public const long BidPxNullValue = 9223372036854775807L;

    internal const int AskPxOffset = BidPxOffset + BidPxLength;

    internal const int AskPxLength = 8;

    /// <summary>
    ///  The number of decimal places in Ask Px.
    /// </summary>
    public const byte AskPxScale = 9;

    /// <summary>
    ///  The factor by which <see cref="AskPx"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal AskPxFactor = 1000000000m;

    /// <summary>
    ///  The encoded value of Ask Px that means no value.
    /// </summary>
    public const long AskPxNullValue = 9223372036854775807L;

    internal const int BidSzOffset = AskPxOffset + AskPxLength;

    internal const int BidSzLength = 4;

    internal const int AskSzOffset = BidSzOffset + BidSzLength;

    internal const int AskSzLength = 4;

    internal const int BidPbOffset = AskSzOffset + AskSzLength;

    internal const int BidPbLength = 2;

    internal const int Reserved2Offset = BidPbOffset + BidPbLength;

    internal const int Reserved2Length = 2;

    internal const int AskPbOffset = Reserved2Offset + Reserved2Length;

    internal const int AskPbLength = 2;

    internal const int SecondReserved2Offset = AskPbOffset + AskPbLength;

    internal const int SecondReserved2Length = 2;

    /// <summary>
    ///  The encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public const int MessageByteLength = BidPxLength + AskPxLength + BidSzLength + AskSzLength + BidPbLength + Reserved2Length + AskPbLength + SecondReserved2Length;

    internal long bidPx;

    /// <summary>
    ///  The bid price
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="BidPxNullValue"/>. Setting <see langword="null"/> writes <see cref="BidPxNullValue"/>. A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range. A result equal to <see cref="BidPxNullValue"/> is moved one step to the nearest other value.
    /// </remarks>
    public decimal? BidPx
    {
        get => this.bidPx == BidPxNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.bidPx, BidPxScale);
        set
        {
            this.bidPx = value is { } present ? FixedPoint.ToRaw<long>(present, BidPxFactor, long.MinValue, long.MaxValue, BidPxNullValue) : BidPxNullValue;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="BidPx"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long BidPxRaw
    {
        get => this.bidPx;
        set
        {
            this.bidPx = value;
            Touch();
        }
    }

    internal long askPx;

    /// <summary>
    ///  The ask price
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="AskPxNullValue"/>. Setting <see langword="null"/> writes <see cref="AskPxNullValue"/>. A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range. A result equal to <see cref="AskPxNullValue"/> is moved one step to the nearest other value.
    /// </remarks>
    public decimal? AskPx
    {
        get => this.askPx == AskPxNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.askPx, AskPxScale);
        set
        {
            this.askPx = value is { } present ? FixedPoint.ToRaw<long>(present, AskPxFactor, long.MinValue, long.MaxValue, AskPxNullValue) : AskPxNullValue;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="AskPx"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long AskPxRaw
    {
        get => this.askPx;
        set
        {
            this.askPx = value;
            Touch();
        }
    }

    internal uint bidSz;

    /// <summary>
    ///  The bid size
    /// </summary>
    public uint BidSz
    {
        get => this.bidSz;
        set
        {
            this.bidSz = value;
            Touch();
        }
    }

    internal uint askSz;

    /// <summary>
    ///  The ask size
    /// </summary>
    public uint AskSz
    {
        get => this.askSz;
        set
        {
            this.askSz = value;
            Touch();
        }
    }

    internal ushort bidPb;

    /// <summary>
    ///  The publisher ID indicating the venue containing the best bid
    /// </summary>
    public ushort BidPb
    {
        get => this.bidPb;
        set
        {
            this.bidPb = value;
            Touch();
        }
    }

    internal ushort askPb;

    /// <summary>
    ///  The publisher ID indicating the venue containing the best ask
    /// </summary>
    public ushort AskPb
    {
        get => this.askPb;
        set
        {
            this.askPb = value;
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

    /// <summary>
    ///  Identifies each field, in wire order.
    /// </summary>
    public enum FieldName
    {
        /// <summary>
        ///  The bid price
        /// </summary>
        BidPx,
        /// <summary>
        ///  The ask price
        /// </summary>
        AskPx,
        /// <summary>
        ///  The bid size
        /// </summary>
        BidSz,
        /// <summary>
        ///  The ask size
        /// </summary>
        AskSz,
        /// <summary>
        ///  The publisher ID indicating the venue containing the best bid
        /// </summary>
        BidPb,
        /// <summary>
        ///  Reserved, 2 bytes
        /// </summary>
        Reserved2,
        /// <summary>
        ///  The publisher ID indicating the venue containing the best ask
        /// </summary>
        AskPb,
        /// <summary>
        ///  Reserved, 2 bytes
        /// </summary>
        SecondReserved2,
    }

    /// <summary>
    ///  Provides access to the fields of one instance, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public readonly struct FieldSet : IEnumerable<FieldView>
    {
        private readonly ConsolidatedBidAskPair owner;

        internal FieldSet(ConsolidatedBidAskPair owner)
        {
            this.owner = owner;
        }

        /// <summary>
        ///  The bid price
        /// </summary>
        public FieldView BidPx => this[FieldName.BidPx];

        /// <summary>
        ///  The ask price
        /// </summary>
        public FieldView AskPx => this[FieldName.AskPx];

        /// <summary>
        ///  The bid size
        /// </summary>
        public FieldView BidSz => this[FieldName.BidSz];

        /// <summary>
        ///  The ask size
        /// </summary>
        public FieldView AskSz => this[FieldName.AskSz];

        /// <summary>
        ///  The publisher ID indicating the venue containing the best bid
        /// </summary>
        public FieldView BidPb => this[FieldName.BidPb];

        /// <summary>
        ///  Reserved, 2 bytes
        /// </summary>
        public FieldView Reserved2 => this[FieldName.Reserved2];

        /// <summary>
        ///  The publisher ID indicating the venue containing the best ask
        /// </summary>
        public FieldView AskPb => this[FieldName.AskPb];

        /// <summary>
        ///  Reserved, 2 bytes
        /// </summary>
        public FieldView SecondReserved2 => this[FieldName.SecondReserved2];

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
        new(nameof(FieldName.BidPx), FieldKind.Decimal, BidPxLength) { Scale = BidPxScale, ByteOrder = "Little", IsSigned = true, Nullable = "Value", NullValue = BidPxNullValue },
        new(nameof(FieldName.AskPx), FieldKind.Decimal, AskPxLength) { Scale = AskPxScale, ByteOrder = "Little", IsSigned = true, Nullable = "Value", NullValue = AskPxNullValue },
        new(nameof(FieldName.BidSz), FieldKind.UnsignedInteger, BidSzLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.AskSz), FieldKind.UnsignedInteger, AskSzLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.BidPb), FieldKind.UnsignedInteger, BidPbLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.Reserved2), FieldKind.Padding, Reserved2Length),
        new(nameof(FieldName.AskPb), FieldKind.UnsignedInteger, AskPbLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.SecondReserved2), FieldKind.Padding, SecondReserved2Length),
    ];

    /// <summary>
    ///  Gets the fields in wire order, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public FieldSet Fields => new(this);

    IEnumerable<FieldView> IGroupEntry.Fields => new FieldSet(this);

    /// <summary>
    ///  Initializes a new instance of the <see cref="ConsolidatedBidAskPair"/> class with default values.
    /// </summary>
    public ConsolidatedBidAskPair()
    {
        Clear();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="ConsolidatedBidAskPair"/> class with the values, bytes, and parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public ConsolidatedBidAskPair(ConsolidatedBidAskPair other) : this()
    {
        CopyFrom(other);
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(ConsolidatedBidAskPair other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.bidPx = other.bidPx;
        this.askPx = other.askPx;
        this.bidSz = other.bidSz;
        this.askSz = other.askSz;
        this.bidPb = other.bidPb;
        this.askPb = other.askPb;
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
        this.bidPx = default;
        this.askPx = default;
        this.bidSz = default;
        this.askSz = default;
        this.bidPb = default;
        this.askPb = default;
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
    public static ConsolidatedBidAskPair Parse(ReadOnlySpan<byte> data)
    {
        var instance = new ConsolidatedBidAskPair();
        instance.ParseFrom(data);
        return instance;
    }

    /// <summary>
    ///  Parses a new instance starting at the specified offset.
    /// </summary>
    /// <param name="data">The bytes to parse.</param>
    /// <param name="offset">The offset at which to start. A successful parse advances it past the parsed bytes, and a failed parse does not change it.</param>
    /// <returns>The new instance. Check Status to determine whether the parse succeeded.</returns>
    public static ConsolidatedBidAskPair Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        var instance = new ConsolidatedBidAskPair();
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
        if (Truncated(data, start, cursor, BidPxLength, FieldName.BidPx)) return;
        this.bidPx = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(cursor, BidPxLength));
        cursor += BidPxLength;
        if (Truncated(data, start, cursor, AskPxLength, FieldName.AskPx)) return;
        this.askPx = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(cursor, AskPxLength));
        cursor += AskPxLength;
        if (Truncated(data, start, cursor, BidSzLength, FieldName.BidSz)) return;
        this.bidSz = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(cursor, BidSzLength));
        cursor += BidSzLength;
        if (Truncated(data, start, cursor, AskSzLength, FieldName.AskSz)) return;
        this.askSz = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(cursor, AskSzLength));
        cursor += AskSzLength;
        if (Truncated(data, start, cursor, BidPbLength, FieldName.BidPb)) return;
        this.bidPb = BinaryPrimitives.ReadUInt16LittleEndian(data.Slice(cursor, BidPbLength));
        cursor += BidPbLength;
        if (Truncated(data, start, cursor, Reserved2Length, FieldName.Reserved2)) return;
        cursor += Reserved2Length;
        if (Truncated(data, start, cursor, AskPbLength, FieldName.AskPb)) return;
        this.askPb = BinaryPrimitives.ReadUInt16LittleEndian(data.Slice(cursor, AskPbLength));
        cursor += AskPbLength;
        if (Truncated(data, start, cursor, SecondReserved2Length, FieldName.SecondReserved2)) return;
        cursor += SecondReserved2Length;
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
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(cursor, BidPxLength), this.bidPx);
        cursor += BidPxLength;
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(cursor, AskPxLength), this.askPx);
        cursor += AskPxLength;
        BinaryPrimitives.WriteUInt32LittleEndian(data.Slice(cursor, BidSzLength), this.bidSz);
        cursor += BidSzLength;
        BinaryPrimitives.WriteUInt32LittleEndian(data.Slice(cursor, AskSzLength), this.askSz);
        cursor += AskSzLength;
        BinaryPrimitives.WriteUInt16LittleEndian(data.Slice(cursor, BidPbLength), this.bidPb);
        cursor += BidPbLength;
        {
            if (this.raw.Length >= Reserved2Offset + Reserved2Length)
            {
                this.raw.AsSpan(Reserved2Offset, Reserved2Length).CopyTo(data.Slice(cursor, Reserved2Length));
            }
            else
            {
                data.Slice(cursor, Reserved2Length).Clear();
            }
            cursor += Reserved2Length;
        }
        BinaryPrimitives.WriteUInt16LittleEndian(data.Slice(cursor, AskPbLength), this.askPb);
        cursor += AskPbLength;
        {
            if (this.raw.Length >= SecondReserved2Offset + SecondReserved2Length)
            {
                this.raw.AsSpan(SecondReserved2Offset, SecondReserved2Length).CopyTo(data.Slice(cursor, SecondReserved2Length));
            }
            else
            {
                data.Slice(cursor, SecondReserved2Length).Clear();
            }
            cursor += SecondReserved2Length;
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
    public List<ValidationFailure> Validate()
    {
        var failures = new List<ValidationFailure>();
        return failures;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(ConsolidatedBidAskPair? other)
    {
        if (other is null)
        {
            return false;
        }
        if (this.bidPx != other.bidPx) return false;
        if (this.askPx != other.askPx) return false;
        if (this.bidSz != other.bidSz) return false;
        if (this.askSz != other.askSz) return false;
        if (this.bidPb != other.bidPb) return false;
        if (this.askPb != other.askPb) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is ConsolidatedBidAskPair other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.bidPx);
        hash.Add(this.askPx);
        hash.Add(this.bidSz);
        hash.Add(this.askSz);
        hash.Add(this.bidPb);
        hash.Add(this.askPb);
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
        private const int FieldCount = 8;

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
            if (Has(FieldName.BidPx)) names.Add(nameof(FieldName.BidPx));
            if (Has(FieldName.AskPx)) names.Add(nameof(FieldName.AskPx));
            if (Has(FieldName.BidSz)) names.Add(nameof(FieldName.BidSz));
            if (Has(FieldName.AskSz)) names.Add(nameof(FieldName.AskSz));
            if (Has(FieldName.BidPb)) names.Add(nameof(FieldName.BidPb));
            if (Has(FieldName.Reserved2)) names.Add(nameof(FieldName.Reserved2));
            if (Has(FieldName.AskPb)) names.Add(nameof(FieldName.AskPb));
            if (Has(FieldName.SecondReserved2)) names.Add(nameof(FieldName.SecondReserved2));
            return names.Count == 0 ? "None" : string.Join(", ", names);
        }
    }

    /// <summary>
    ///  Compares this instance with another and identifies the fields that differ.
    /// </summary>
    /// <returns>The fields that differ, or None if there are no differences.</returns>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Changes Diff(ConsolidatedBidAskPair other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (this.bidPx != other.bidPx) diff = diff.With(FieldName.BidPx);
        if (this.askPx != other.askPx) diff = diff.With(FieldName.AskPx);
        if (this.bidSz != other.bidSz) diff = diff.With(FieldName.BidSz);
        if (this.askSz != other.askSz) diff = diff.With(FieldName.AskSz);
        if (this.bidPb != other.bidPb) diff = diff.With(FieldName.BidPb);
        if (this.askPb != other.askPb) diff = diff.With(FieldName.AskPb);
        return diff;
    }

    private void CopyField(FieldName field, ConsolidatedBidAskPair from)
    {
        switch (field)
        {
            case FieldName.BidPx:
                this.bidPx = from.bidPx;
                break;
            case FieldName.AskPx:
                this.askPx = from.askPx;
                break;
            case FieldName.BidSz:
                this.bidSz = from.bidSz;
                break;
            case FieldName.AskSz:
                this.askSz = from.askSz;
                break;
            case FieldName.BidPb:
                this.bidPb = from.bidPb;
                break;
            case FieldName.AskPb:
                this.askPb = from.askPb;
                break;
            default:
                return;
        }
        Touch();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"ConsolidatedBidAskPair {{ BidPx: {(Text.Of(this.bidPx == BidPxNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.bidPx, BidPxScale)))}, AskPx: {(Text.Of(this.askPx == AskPxNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.askPx, AskPxScale)))}, BidSz: {(Text.Of(this.bidSz))}, AskSz: {(Text.Of(this.askSz))}, BidPb: {(Text.Of(this.bidPb))}, AskPb: {(Text.Of(this.askPb))} }}";

    private string DebuggerDisplay => $"[{(Text.Of(this.bidPx == BidPxNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.bidPx, BidPxScale)))}] [{(Text.Of(this.askPx == AskPxNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.askPx, AskPxScale)))}] [{(Text.Of(this.bidSz))}]";

    IFieldOwner? IFieldOwner.Owner
    {
        get => this.owner;
        set => this.owner = value;
    }

    FieldDescriptor[] IFieldOwner.Descriptors => Descriptors;

    void IFieldOwner.Touch() => Touch();

    string IFieldOwner.TextOf(int field) => (FieldName)field switch
    {
        FieldName.BidPx => Text.Of(this.bidPx == BidPxNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.bidPx, BidPxScale)),
        FieldName.AskPx => Text.Of(this.askPx == AskPxNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.askPx, AskPxScale)),
        FieldName.BidSz => Text.Of(this.bidSz),
        FieldName.AskSz => Text.Of(this.askSz),
        FieldName.BidPb => Text.Of(this.bidPb),
        FieldName.AskPb => Text.Of(this.askPb),
        _ => string.Empty,
    };

    bool IFieldOwner.IsRecognized(int field) => true;

    bool IFieldOwner.IsPresent(int field) => (FieldName)field switch
    {
        FieldName.BidPx => this.bidPx != BidPxNullValue,
        FieldName.AskPx => this.askPx != AskPxNullValue,
        _ => true,
    };

    bool IFieldOwner.IsParsed(int field) => this.status == ParseStatus.Ok || field < (int?)this.failed;

    bool IFieldOwner.IsReached(int field) => this.modified || this.status is ParseStatus.None or ParseStatus.Ok || field <= (int?)this.failed;

    bool IFieldOwner.HasParseLayout(int field) => false;

    int IFieldOwner.OffsetOf(int field) => (FieldName)field switch
    {
        FieldName.BidPx => BidPxOffset,
        FieldName.AskPx => AskPxOffset,
        FieldName.BidSz => BidSzOffset,
        FieldName.AskSz => AskSzOffset,
        FieldName.BidPb => BidPbOffset,
        FieldName.Reserved2 => Reserved2Offset,
        FieldName.AskPb => AskPbOffset,
        FieldName.SecondReserved2 => SecondReserved2Offset,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    int IFieldOwner.LengthOf(int field) => (FieldName)field switch
    {
        FieldName.BidPx => BidPxLength,
        FieldName.AskPx => AskPxLength,
        FieldName.BidSz => BidSzLength,
        FieldName.AskSz => AskSzLength,
        FieldName.BidPb => BidPbLength,
        FieldName.Reserved2 => Reserved2Length,
        FieldName.AskPb => AskPbLength,
        FieldName.SecondReserved2 => SecondReserved2Length,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    IReadOnlyList<IGroupEntry>? IFieldOwner.EntriesOf(int field) => null;

    IMessage? IFieldOwner.MessageOf(int field) => null;

    ulong[] IFieldOwner.ChangedAgainst(IFieldOwner other) => other is ConsolidatedBidAskPair typed ? Diff(typed).ToWords() : throw new ArgumentException("The other instance is not of the same type.", nameof(other));

    void IFieldOwner.CopyField(int field, IFieldOwner from) => CopyField((FieldName)field, from as ConsolidatedBidAskPair ?? throw new ArgumentException("The other instance is not of the same type.", nameof(from)));
}
