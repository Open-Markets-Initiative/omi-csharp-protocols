using System.Buffers.Binary;
using System.Diagnostics;

namespace Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0;

/// <summary>
///  Leg information
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class LegInformation : IGroupEntry, IEquatable<LegInformation>, IFieldOwner
{
    internal const int OptionIdOffset = 0;

    internal const int OptionIdLength = 4;

    internal const int SecuritySymbolOffset = OptionIdOffset + OptionIdLength;

    internal const int SecuritySymbolLength = 6;

    /// <summary>
    ///  The byte that fills the unused bytes of Security Symbol.
    /// </summary>
    public const byte SecuritySymbolFill = (byte)' ';

    internal const int LegIdOffset = SecuritySymbolOffset + SecuritySymbolLength;

    internal const int LegIdLength = 1;

    internal const int ExpirationYearOffset = LegIdOffset + LegIdLength;

    internal const int ExpirationYearLength = 1;

    internal const int ExpirationMonthOffset = ExpirationYearOffset + ExpirationYearLength;

    internal const int ExpirationMonthLength = 1;

    internal const int ExpirationDayOffset = ExpirationMonthOffset + ExpirationMonthLength;

    internal const int ExpirationDayLength = 1;

    internal const int ExplicitStrikePriceOffset = ExpirationDayOffset + ExpirationDayLength;

    internal const int ExplicitStrikePriceLength = 8;

    /// <summary>
    ///  The number of decimal places in Explicit Strike Price.
    /// </summary>
    public const byte ExplicitStrikePriceScale = 8;

    /// <summary>
    ///  The factor by which <see cref="ExplicitStrikePrice"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal ExplicitStrikePriceFactor = 100000000m;

    internal const int OptionTypeOffset = ExplicitStrikePriceOffset + ExplicitStrikePriceLength;

    internal const int OptionTypeLength = 1;

    internal const int SideOffset = OptionTypeOffset + OptionTypeLength;

    internal const int SideLength = 1;

    internal const int LegRatioOffset = SideOffset + SideLength;

    internal const int LegRatioLength = 4;

    /// <summary>
    ///  The encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public const int MessageByteLength = OptionIdLength + SecuritySymbolLength + LegIdLength + ExpirationYearLength + ExpirationMonthLength + ExpirationDayLength + ExplicitStrikePriceLength + OptionTypeLength + SideLength + LegRatioLength;

    internal uint optionId;

    /// <summary>
    ///  ISE’s Option ID for this leg, valid for the trading day. The same ID as the corresponding Option in the Options Directory Message. Zero (0) for Stock Leg.
    /// </summary>
    public uint OptionId
    {
        get => this.optionId;
        set
        {
            this.optionId = value;
            Touch();
        }
    }

    internal string securitySymbol = string.Empty;

    /// <summary>
    ///  Denotes the option root symbol (security symbol)
    /// </summary>
    /// <remarks>
    ///  The field is 6 bytes long. The field holds ASCII text. Text that is shorter than the field is padded with spaces at the end. Text longer than the field is cut to fit when the message is encoded. A character that the field cannot hold is replaced with a question mark when the message is encoded.
    /// </remarks>
    public string SecuritySymbol
    {
        get => this.securitySymbol;
        set
        {
            this.securitySymbol = value ?? string.Empty;
            Touch();
        }
    }

    internal byte legId;

    /// <summary>
    ///  Leg identifier within this strategy. This is an exchange-assigned 0-based index. E.g. Nth leg has LegId=N-1.
    /// </summary>
    public byte LegId
    {
        get => this.legId;
        set
        {
            this.legId = value;
            Touch();
        }
    }

    internal byte expirationYear;

    /// <summary>
    ///  Last two digits of the year of the option expiration
    /// </summary>
    public byte ExpirationYear
    {
        get => this.expirationYear;
        set
        {
            this.expirationYear = value;
            Touch();
        }
    }

    internal byte expirationMonth;

    /// <summary>
    ///  Expiration Month of the option (1-12)
    /// </summary>
    public byte ExpirationMonth
    {
        get => this.expirationMonth;
        set
        {
            this.expirationMonth = value;
            Touch();
        }
    }

    internal byte expirationDay;

    /// <summary>
    ///  Day of the Month of expiration (1-31)
    /// </summary>
    public byte ExpirationDay
    {
        get => this.expirationDay;
        set
        {
            this.expirationDay = value;
            Touch();
        }
    }

    internal long explicitStrikePrice;

    /// <summary>
    ///  Denotes the explicit strike price of the option. Refer to Data Types for field processing notes.
    /// </summary>
    /// <remarks>
    ///  A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range.
    /// </remarks>
    public decimal ExplicitStrikePrice
    {
        get => FixedPoint.ToDecimal(this.explicitStrikePrice, ExplicitStrikePriceScale);
        set
        {
            this.explicitStrikePrice = FixedPoint.ToRaw<long>(value, ExplicitStrikePriceFactor, long.MinValue, long.MaxValue);
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="ExplicitStrikePrice"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long ExplicitStrikePriceRaw
    {
        get => this.explicitStrikePrice;
        set
        {
            this.explicitStrikePrice = value;
            Touch();
        }
    }

    internal OptionType optionType;

    /// <summary>
    ///  Option Type:
    /// </summary>
    public OptionType OptionType
    {
        get => this.optionType;
        set
        {
            this.optionType = value;
            Touch();
        }
    }

    internal Side side;

    /// <summary>
    ///  Indicates the side of the leg
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

    internal uint legRatio;

    /// <summary>
    ///  Leg Ratio
    /// </summary>
    public uint LegRatio
    {
        get => this.legRatio;
        set
        {
            this.legRatio = value;
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
    public bool IsRecognized => global::System.Enum.IsDefined(this.optionType) && global::System.Enum.IsDefined(this.side);

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
        ///  ISE’s Option ID for this leg, valid for the trading day. The same ID as the corresponding Option in the Options Directory Message. Zero (0) for Stock Leg.
        /// </summary>
        OptionId,
        /// <summary>
        ///  Denotes the option root symbol (security symbol)
        /// </summary>
        SecuritySymbol,
        /// <summary>
        ///  Leg identifier within this strategy. This is an exchange-assigned 0-based index. E.g. Nth leg has LegId=N-1.
        /// </summary>
        LegId,
        /// <summary>
        ///  Last two digits of the year of the option expiration
        /// </summary>
        ExpirationYear,
        /// <summary>
        ///  Expiration Month of the option (1-12)
        /// </summary>
        ExpirationMonth,
        /// <summary>
        ///  Day of the Month of expiration (1-31)
        /// </summary>
        ExpirationDay,
        /// <summary>
        ///  Denotes the explicit strike price of the option. Refer to Data Types for field processing notes.
        /// </summary>
        ExplicitStrikePrice,
        /// <summary>
        ///  Option Type:
        /// </summary>
        OptionType,
        /// <summary>
        ///  Indicates the side of the leg
        /// </summary>
        Side,
        /// <summary>
        ///  Leg Ratio
        /// </summary>
        LegRatio,
    }

    /// <summary>
    ///  Provides access to the fields of one instance, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public readonly struct FieldSet : IEnumerable<FieldView>
    {
        private readonly LegInformation owner;

        internal FieldSet(LegInformation owner)
        {
            this.owner = owner;
        }

        /// <summary>
        ///  ISE’s Option ID for this leg, valid for the trading day. The same ID as the corresponding Option in the Options Directory Message. Zero (0) for Stock Leg.
        /// </summary>
        public FieldView OptionId => this[FieldName.OptionId];

        /// <summary>
        ///  Denotes the option root symbol (security symbol)
        /// </summary>
        public FieldView SecuritySymbol => this[FieldName.SecuritySymbol];

        /// <summary>
        ///  Leg identifier within this strategy. This is an exchange-assigned 0-based index. E.g. Nth leg has LegId=N-1.
        /// </summary>
        public FieldView LegId => this[FieldName.LegId];

        /// <summary>
        ///  Last two digits of the year of the option expiration
        /// </summary>
        public FieldView ExpirationYear => this[FieldName.ExpirationYear];

        /// <summary>
        ///  Expiration Month of the option (1-12)
        /// </summary>
        public FieldView ExpirationMonth => this[FieldName.ExpirationMonth];

        /// <summary>
        ///  Day of the Month of expiration (1-31)
        /// </summary>
        public FieldView ExpirationDay => this[FieldName.ExpirationDay];

        /// <summary>
        ///  Denotes the explicit strike price of the option. Refer to Data Types for field processing notes.
        /// </summary>
        public FieldView ExplicitStrikePrice => this[FieldName.ExplicitStrikePrice];

        /// <summary>
        ///  Option Type:
        /// </summary>
        public FieldView OptionType => this[FieldName.OptionType];

        /// <summary>
        ///  Indicates the side of the leg
        /// </summary>
        public FieldView Side => this[FieldName.Side];

        /// <summary>
        ///  Leg Ratio
        /// </summary>
        public FieldView LegRatio => this[FieldName.LegRatio];

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
        new(nameof(FieldName.OptionId), FieldKind.UnsignedInteger, OptionIdLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.SecuritySymbol), FieldKind.String, SecuritySymbolLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.LegId), FieldKind.UnsignedInteger, LegIdLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.ExpirationYear), FieldKind.UnsignedInteger, ExpirationYearLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.ExpirationMonth), FieldKind.UnsignedInteger, ExpirationMonthLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.ExpirationDay), FieldKind.UnsignedInteger, ExpirationDayLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.ExplicitStrikePrice), FieldKind.Decimal, ExplicitStrikePriceLength) { Scale = ExplicitStrikePriceScale, ByteOrder = "Big", IsSigned = true },
        new(nameof(FieldName.OptionType), FieldKind.Enum, OptionTypeLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.Side), FieldKind.Enum, SideLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.LegRatio), FieldKind.UnsignedInteger, LegRatioLength) { ByteOrder = "Big", IsSigned = false },
    ];

    /// <summary>
    ///  Gets the fields in wire order, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public FieldSet Fields => new(this);

    IEnumerable<FieldView> IGroupEntry.Fields => new FieldSet(this);

    /// <summary>
    ///  Initializes a new instance of the <see cref="LegInformation"/> class with default values.
    /// </summary>
    public LegInformation()
    {
        Clear();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="LegInformation"/> class with the values, bytes, and parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public LegInformation(LegInformation other) : this()
    {
        CopyFrom(other);
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(LegInformation other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.optionId = other.optionId;
        this.securitySymbol = other.securitySymbol;
        this.legId = other.legId;
        this.expirationYear = other.expirationYear;
        this.expirationMonth = other.expirationMonth;
        this.expirationDay = other.expirationDay;
        this.explicitStrikePrice = other.explicitStrikePrice;
        this.optionType = other.optionType;
        this.side = other.side;
        this.legRatio = other.legRatio;
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
        this.optionId = default;
        this.securitySymbol = string.Empty;
        this.legId = default;
        this.expirationYear = default;
        this.expirationMonth = default;
        this.expirationDay = default;
        this.explicitStrikePrice = default;
        this.optionType = default;
        this.side = default;
        this.legRatio = default;
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
    public static LegInformation Parse(ReadOnlySpan<byte> data)
    {
        var instance = new LegInformation();
        instance.ParseFrom(data);
        return instance;
    }

    /// <summary>
    ///  Parses a new instance starting at the specified offset.
    /// </summary>
    /// <param name="data">The bytes to parse.</param>
    /// <param name="offset">The offset at which to start. A successful parse advances it past the parsed bytes, and a failed parse does not change it.</param>
    /// <returns>The new instance. Check Status to determine whether the parse succeeded.</returns>
    public static LegInformation Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        var instance = new LegInformation();
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
        if (Truncated(data, start, cursor, OptionIdLength, FieldName.OptionId)) return;
        this.optionId = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, OptionIdLength));
        cursor += OptionIdLength;
        if (Truncated(data, start, cursor, SecuritySymbolLength, FieldName.SecuritySymbol)) return;
        this.securitySymbol = TextEncoding.Ascii.ReadLeftJustified(data.Slice(cursor, SecuritySymbolLength), SecuritySymbolFill);
        cursor += SecuritySymbolLength;
        if (Truncated(data, start, cursor, LegIdLength, FieldName.LegId)) return;
        this.legId = data.Slice(cursor, LegIdLength)[0];
        cursor += LegIdLength;
        if (Truncated(data, start, cursor, ExpirationYearLength, FieldName.ExpirationYear)) return;
        this.expirationYear = data.Slice(cursor, ExpirationYearLength)[0];
        cursor += ExpirationYearLength;
        if (Truncated(data, start, cursor, ExpirationMonthLength, FieldName.ExpirationMonth)) return;
        this.expirationMonth = data.Slice(cursor, ExpirationMonthLength)[0];
        cursor += ExpirationMonthLength;
        if (Truncated(data, start, cursor, ExpirationDayLength, FieldName.ExpirationDay)) return;
        this.expirationDay = data.Slice(cursor, ExpirationDayLength)[0];
        cursor += ExpirationDayLength;
        if (Truncated(data, start, cursor, ExplicitStrikePriceLength, FieldName.ExplicitStrikePrice)) return;
        this.explicitStrikePrice = BinaryPrimitives.ReadInt64BigEndian(data.Slice(cursor, ExplicitStrikePriceLength));
        cursor += ExplicitStrikePriceLength;
        if (Truncated(data, start, cursor, OptionTypeLength, FieldName.OptionType)) return;
        this.optionType = (OptionType)data.Slice(cursor, OptionTypeLength)[0];
        cursor += OptionTypeLength;
        if (Truncated(data, start, cursor, SideLength, FieldName.Side)) return;
        this.side = (Side)data.Slice(cursor, SideLength)[0];
        cursor += SideLength;
        if (Truncated(data, start, cursor, LegRatioLength, FieldName.LegRatio)) return;
        this.legRatio = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, LegRatioLength));
        cursor += LegRatioLength;
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
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, OptionIdLength), this.optionId);
        cursor += OptionIdLength;
        TextEncoding.Ascii.WriteLeftJustified(data.Slice(cursor, SecuritySymbolLength), TextEncoding.Ascii.Fit(this.securitySymbol, SecuritySymbolLength), SecuritySymbolFill);
        cursor += SecuritySymbolLength;
        data.Slice(cursor, LegIdLength)[0] = unchecked((byte)(this.legId));
        cursor += LegIdLength;
        data.Slice(cursor, ExpirationYearLength)[0] = unchecked((byte)(this.expirationYear));
        cursor += ExpirationYearLength;
        data.Slice(cursor, ExpirationMonthLength)[0] = unchecked((byte)(this.expirationMonth));
        cursor += ExpirationMonthLength;
        data.Slice(cursor, ExpirationDayLength)[0] = unchecked((byte)(this.expirationDay));
        cursor += ExpirationDayLength;
        BinaryPrimitives.WriteInt64BigEndian(data.Slice(cursor, ExplicitStrikePriceLength), this.explicitStrikePrice);
        cursor += ExplicitStrikePriceLength;
        data.Slice(cursor, OptionTypeLength)[0] = unchecked((byte)(this.optionType));
        cursor += OptionTypeLength;
        data.Slice(cursor, SideLength)[0] = unchecked((byte)(this.side));
        cursor += SideLength;
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, LegRatioLength), this.legRatio);
        cursor += LegRatioLength;
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
        if (!global::System.Text.Ascii.IsValid(this.securitySymbol))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.SecuritySymbol), ValidationStatus.NonAsciiText));
        }
        if (!global::System.Enum.IsDefined(this.optionType))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.OptionType), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Enum.IsDefined(this.side))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.Side), ValidationStatus.UndeclaredValue));
        }
        return failures;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(LegInformation? other)
    {
        if (other is null)
        {
            return false;
        }
        if (this.optionId != other.optionId) return false;
        if (this.securitySymbol != other.securitySymbol) return false;
        if (this.legId != other.legId) return false;
        if (this.expirationYear != other.expirationYear) return false;
        if (this.expirationMonth != other.expirationMonth) return false;
        if (this.expirationDay != other.expirationDay) return false;
        if (this.explicitStrikePrice != other.explicitStrikePrice) return false;
        if (this.optionType != other.optionType) return false;
        if (this.side != other.side) return false;
        if (this.legRatio != other.legRatio) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is LegInformation other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.optionId);
        hash.Add(this.securitySymbol);
        hash.Add(this.legId);
        hash.Add(this.expirationYear);
        hash.Add(this.expirationMonth);
        hash.Add(this.expirationDay);
        hash.Add(this.explicitStrikePrice);
        hash.Add(this.optionType);
        hash.Add(this.side);
        hash.Add(this.legRatio);
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
        private const int FieldCount = 10;

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
            if (Has(FieldName.OptionId)) names.Add(nameof(FieldName.OptionId));
            if (Has(FieldName.SecuritySymbol)) names.Add(nameof(FieldName.SecuritySymbol));
            if (Has(FieldName.LegId)) names.Add(nameof(FieldName.LegId));
            if (Has(FieldName.ExpirationYear)) names.Add(nameof(FieldName.ExpirationYear));
            if (Has(FieldName.ExpirationMonth)) names.Add(nameof(FieldName.ExpirationMonth));
            if (Has(FieldName.ExpirationDay)) names.Add(nameof(FieldName.ExpirationDay));
            if (Has(FieldName.ExplicitStrikePrice)) names.Add(nameof(FieldName.ExplicitStrikePrice));
            if (Has(FieldName.OptionType)) names.Add(nameof(FieldName.OptionType));
            if (Has(FieldName.Side)) names.Add(nameof(FieldName.Side));
            if (Has(FieldName.LegRatio)) names.Add(nameof(FieldName.LegRatio));
            return names.Count == 0 ? "None" : string.Join(", ", names);
        }
    }

    /// <summary>
    ///  Compares this instance with another and identifies the fields that differ.
    /// </summary>
    /// <returns>The fields that differ, or None if there are no differences.</returns>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Changes Diff(LegInformation other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (this.optionId != other.optionId) diff = diff.With(FieldName.OptionId);
        if (this.securitySymbol != other.securitySymbol) diff = diff.With(FieldName.SecuritySymbol);
        if (this.legId != other.legId) diff = diff.With(FieldName.LegId);
        if (this.expirationYear != other.expirationYear) diff = diff.With(FieldName.ExpirationYear);
        if (this.expirationMonth != other.expirationMonth) diff = diff.With(FieldName.ExpirationMonth);
        if (this.expirationDay != other.expirationDay) diff = diff.With(FieldName.ExpirationDay);
        if (this.explicitStrikePrice != other.explicitStrikePrice) diff = diff.With(FieldName.ExplicitStrikePrice);
        if (this.optionType != other.optionType) diff = diff.With(FieldName.OptionType);
        if (this.side != other.side) diff = diff.With(FieldName.Side);
        if (this.legRatio != other.legRatio) diff = diff.With(FieldName.LegRatio);
        return diff;
    }

    private void CopyField(FieldName field, LegInformation from)
    {
        switch (field)
        {
            case FieldName.OptionId:
                this.optionId = from.optionId;
                break;
            case FieldName.SecuritySymbol:
                this.securitySymbol = from.securitySymbol;
                break;
            case FieldName.LegId:
                this.legId = from.legId;
                break;
            case FieldName.ExpirationYear:
                this.expirationYear = from.expirationYear;
                break;
            case FieldName.ExpirationMonth:
                this.expirationMonth = from.expirationMonth;
                break;
            case FieldName.ExpirationDay:
                this.expirationDay = from.expirationDay;
                break;
            case FieldName.ExplicitStrikePrice:
                this.explicitStrikePrice = from.explicitStrikePrice;
                break;
            case FieldName.OptionType:
                this.optionType = from.optionType;
                break;
            case FieldName.Side:
                this.side = from.side;
                break;
            case FieldName.LegRatio:
                this.legRatio = from.legRatio;
                break;
            default:
                return;
        }
        Touch();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"LegInformation {{ OptionId: {(Text.Of(this.optionId))}, SecuritySymbol: {(Text.Of(this.securitySymbol))}, LegId: {(Text.Of(this.legId))}, ExpirationYear: {(Text.Of(this.expirationYear))}, ExpirationMonth: {(Text.Of(this.expirationMonth))}, ExpirationDay: {(Text.Of(this.expirationDay))}, ExplicitStrikePrice: {(Text.Of(FixedPoint.ToDecimal(this.explicitStrikePrice, ExplicitStrikePriceScale)))}, OptionType: {(Text.Enum(this.optionType))}, Side: {(Text.Enum(this.side))}, LegRatio: {(Text.Of(this.legRatio))} }}";

    private string DebuggerDisplay => $"[{(Text.Of(this.optionId))}] [{(Text.Of(this.securitySymbol))}] [{(Text.Of(this.legId))}]";

    IFieldOwner? IFieldOwner.Owner
    {
        get => this.owner;
        set => this.owner = value;
    }

    FieldDescriptor[] IFieldOwner.Descriptors => Descriptors;

    void IFieldOwner.Touch() => Touch();

    string IFieldOwner.TextOf(int field) => (FieldName)field switch
    {
        FieldName.OptionId => Text.Of(this.optionId),
        FieldName.SecuritySymbol => Text.Of(this.securitySymbol),
        FieldName.LegId => Text.Of(this.legId),
        FieldName.ExpirationYear => Text.Of(this.expirationYear),
        FieldName.ExpirationMonth => Text.Of(this.expirationMonth),
        FieldName.ExpirationDay => Text.Of(this.expirationDay),
        FieldName.ExplicitStrikePrice => Text.Of(FixedPoint.ToDecimal(this.explicitStrikePrice, ExplicitStrikePriceScale)),
        FieldName.OptionType => Text.Enum(this.optionType),
        FieldName.Side => Text.Enum(this.side),
        FieldName.LegRatio => Text.Of(this.legRatio),
        _ => string.Empty,
    };

    bool IFieldOwner.IsRecognized(int field) => (FieldName)field switch
    {
        FieldName.OptionType => global::System.Enum.IsDefined(this.optionType),
        FieldName.Side => global::System.Enum.IsDefined(this.side),
        _ => true,
    };

    bool IFieldOwner.IsPresent(int field) => true;

    bool IFieldOwner.IsParsed(int field) => this.status == ParseStatus.Ok || field < (int?)this.failed;

    bool IFieldOwner.IsReached(int field) => this.modified || this.status is ParseStatus.None or ParseStatus.Ok || field <= (int?)this.failed;

    bool IFieldOwner.HasParseLayout(int field) => false;

    int IFieldOwner.OffsetOf(int field) => (FieldName)field switch
    {
        FieldName.OptionId => OptionIdOffset,
        FieldName.SecuritySymbol => SecuritySymbolOffset,
        FieldName.LegId => LegIdOffset,
        FieldName.ExpirationYear => ExpirationYearOffset,
        FieldName.ExpirationMonth => ExpirationMonthOffset,
        FieldName.ExpirationDay => ExpirationDayOffset,
        FieldName.ExplicitStrikePrice => ExplicitStrikePriceOffset,
        FieldName.OptionType => OptionTypeOffset,
        FieldName.Side => SideOffset,
        FieldName.LegRatio => LegRatioOffset,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    int IFieldOwner.LengthOf(int field) => (FieldName)field switch
    {
        FieldName.OptionId => OptionIdLength,
        FieldName.SecuritySymbol => SecuritySymbolLength,
        FieldName.LegId => LegIdLength,
        FieldName.ExpirationYear => ExpirationYearLength,
        FieldName.ExpirationMonth => ExpirationMonthLength,
        FieldName.ExpirationDay => ExpirationDayLength,
        FieldName.ExplicitStrikePrice => ExplicitStrikePriceLength,
        FieldName.OptionType => OptionTypeLength,
        FieldName.Side => SideLength,
        FieldName.LegRatio => LegRatioLength,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    IReadOnlyList<IGroupEntry>? IFieldOwner.EntriesOf(int field) => null;

    IMessage? IFieldOwner.MessageOf(int field) => null;

    ulong[] IFieldOwner.ChangedAgainst(IFieldOwner other) => other is LegInformation typed ? Diff(typed).ToWords() : throw new ArgumentException("The other instance is not of the same type.", nameof(other));

    void IFieldOwner.CopyField(int field, IFieldOwner from) => CopyField((FieldName)field, from as LegInformation ?? throw new ArgumentException("The other instance is not of the same type.", nameof(from)));
}
