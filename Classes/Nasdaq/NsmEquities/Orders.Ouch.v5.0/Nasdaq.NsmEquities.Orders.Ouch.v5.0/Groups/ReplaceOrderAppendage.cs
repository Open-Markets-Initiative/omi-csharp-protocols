using System.Buffers.Binary;
using System.Diagnostics;
using static Nasdaq.NsmEquities.Orders.Ouch.v5_0.Rules;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Replace Order Appendage
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class ReplaceOrderAppendage : IGroupEntry, IEquatable<ReplaceOrderAppendage>, IFieldOwner
{
    internal const int OptionalFieldLengthOffset = 0;

    internal const int OptionalFieldLengthLength = 1;

    internal const int ReplaceOrderOptionalFieldOffset = OptionalFieldLengthOffset + OptionalFieldLengthLength;

    internal const int ReplaceOrderOptionalFieldLength = 1;

    internal const int ReplaceOrderOptionalValueOffset = ReplaceOrderOptionalFieldOffset + ReplaceOrderOptionalFieldLength;

    private const byte MinQtyTag = 3;

    private const byte MaxFloorTag = 5;

    private const byte PriceTypeTag = 6;

    private const byte PostOnlyTag = 12;

    private const byte ExpireTimeTag = 15;

    private const byte TradeNowTag = 16;

    private const byte HandleInstTag = 17;

    private const byte RandomReservesTag = 13;

    private const byte PegOffsetTag = 7;

    private const byte DiscretionPriceTag = 9;

    private const byte DiscretionPriceTypeTag = 10;

    private const byte DiscretionPegOffsetTag = 11;

    private const byte SharesLocatedTag = 25;

    private const byte LocateBrokerTag = 26;

    private const byte UserRefIdxTag = 28;

    private const byte SideTag = 27;

    private const byte AiqStrategyTag = 29;

    private const byte AiqGroupIdTag = 30;

    /// <summary>
    ///  The encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public const int MessageByteLength = OptionalFieldLengthLength + ReplaceOrderOptionalFieldLength;

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

    internal ReplaceOrderOptionalField replaceOrderOptionalField;

    /// <summary>
    ///  Appendage Id
    /// </summary>
    public ReplaceOrderOptionalField ReplaceOrderOptionalField
    {
        get => this.replaceOrderOptionalField;
        set
        {
            this.replaceOrderOptionalField = value;
            Touch();
        }
    }

    internal ReplaceOrderOptionalValue replaceOrderOptionalValue = new ReplaceOrderOptionalValue();

    /// <summary>
    ///  Branch
    /// </summary>
    /// <remarks>
    ///  Setting <see langword="null"/> stores the value that Clear stores.
    /// </remarks>
    public ReplaceOrderOptionalValue ReplaceOrderOptionalValue
    {
        get => this.replaceOrderOptionalValue;
        set
        {
            var copy = value is null ? new ReplaceOrderOptionalValue() : new ReplaceOrderOptionalValue(value);
            ((IFieldOwner)this.replaceOrderOptionalValue).Owner = null;
            this.replaceOrderOptionalValue = copy;
            ((IFieldOwner)this.replaceOrderOptionalValue).Owner = this;
            Touch();
        }
    }

    private ReplaceOrderOptionalValue.FieldName? ReplaceOrderOptionalValueCase()
    {
        bool ReplaceOrderOptionalValueBranchRule0Selected;
        bool ReplaceOrderOptionalValueBranchRule1Selected;
        bool ReplaceOrderOptionalValueBranchRule2Selected;
        bool ReplaceOrderOptionalValueBranchRule3Selected;
        bool ReplaceOrderOptionalValueBranchRule4Selected;
        bool ReplaceOrderOptionalValueBranchRule5Selected;
        bool ReplaceOrderOptionalValueBranchRule6Selected;
        bool ReplaceOrderOptionalValueBranchRule7Selected;
        bool ReplaceOrderOptionalValueBranchRule8Selected;
        bool ReplaceOrderOptionalValueBranchRule9Selected;
        bool ReplaceOrderOptionalValueBranchRule10Selected;
        bool ReplaceOrderOptionalValueBranchRule11Selected;
        bool ReplaceOrderOptionalValueBranchRule12Selected;
        bool ReplaceOrderOptionalValueBranchRule13Selected;
        bool ReplaceOrderOptionalValueBranchRule14Selected;
        bool ReplaceOrderOptionalValueBranchRule15Selected;
        bool ReplaceOrderOptionalValueBranchRule16Selected;
        bool ReplaceOrderOptionalValueBranchRule17Selected;
        {
            const byte ReplaceOrderOptionalValueBranchRule0Data2 = 3;
            {
                byte replaceOrderOptionalFieldValue = (byte)this.replaceOrderOptionalField;
                ReplaceOrderOptionalValueBranchRule0Selected = (replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule0Data2);
            }
            if (ReplaceOrderOptionalValueBranchRule0Selected)
            {
                return ReplaceOrderOptionalValue.FieldName.MinQty;
            }
        }
        {
            const byte ReplaceOrderOptionalValueBranchRule1Data2 = 5;
            {
                byte replaceOrderOptionalFieldValue = (byte)this.replaceOrderOptionalField;
                ReplaceOrderOptionalValueBranchRule1Selected = (replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule1Data2);
            }
            if (ReplaceOrderOptionalValueBranchRule1Selected)
            {
                return ReplaceOrderOptionalValue.FieldName.MaxFloor;
            }
        }
        {
            const byte ReplaceOrderOptionalValueBranchRule2Data2 = 6;
            {
                byte replaceOrderOptionalFieldValue = (byte)this.replaceOrderOptionalField;
                ReplaceOrderOptionalValueBranchRule2Selected = (replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule2Data2);
            }
            if (ReplaceOrderOptionalValueBranchRule2Selected)
            {
                return ReplaceOrderOptionalValue.FieldName.PriceType;
            }
        }
        {
            const byte ReplaceOrderOptionalValueBranchRule3Data2 = 12;
            {
                byte replaceOrderOptionalFieldValue = (byte)this.replaceOrderOptionalField;
                ReplaceOrderOptionalValueBranchRule3Selected = (replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule3Data2);
            }
            if (ReplaceOrderOptionalValueBranchRule3Selected)
            {
                return ReplaceOrderOptionalValue.FieldName.PostOnly;
            }
        }
        {
            const byte ReplaceOrderOptionalValueBranchRule4Data2 = 15;
            {
                byte replaceOrderOptionalFieldValue = (byte)this.replaceOrderOptionalField;
                ReplaceOrderOptionalValueBranchRule4Selected = (replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule4Data2);
            }
            if (ReplaceOrderOptionalValueBranchRule4Selected)
            {
                return ReplaceOrderOptionalValue.FieldName.ExpireTime;
            }
        }
        {
            const byte ReplaceOrderOptionalValueBranchRule5Data2 = 16;
            {
                byte replaceOrderOptionalFieldValue = (byte)this.replaceOrderOptionalField;
                ReplaceOrderOptionalValueBranchRule5Selected = (replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule5Data2);
            }
            if (ReplaceOrderOptionalValueBranchRule5Selected)
            {
                return ReplaceOrderOptionalValue.FieldName.TradeNow;
            }
        }
        {
            const byte ReplaceOrderOptionalValueBranchRule6Data2 = 17;
            {
                byte replaceOrderOptionalFieldValue = (byte)this.replaceOrderOptionalField;
                ReplaceOrderOptionalValueBranchRule6Selected = (replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule6Data2);
            }
            if (ReplaceOrderOptionalValueBranchRule6Selected)
            {
                return ReplaceOrderOptionalValue.FieldName.HandleInst;
            }
        }
        {
            const byte ReplaceOrderOptionalValueBranchRule7Data2 = 13;
            {
                byte replaceOrderOptionalFieldValue = (byte)this.replaceOrderOptionalField;
                ReplaceOrderOptionalValueBranchRule7Selected = (replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule7Data2);
            }
            if (ReplaceOrderOptionalValueBranchRule7Selected)
            {
                return ReplaceOrderOptionalValue.FieldName.RandomReserves;
            }
        }
        {
            const byte ReplaceOrderOptionalValueBranchRule8Data2 = 7;
            {
                byte replaceOrderOptionalFieldValue = (byte)this.replaceOrderOptionalField;
                ReplaceOrderOptionalValueBranchRule8Selected = (replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule8Data2);
            }
            if (ReplaceOrderOptionalValueBranchRule8Selected)
            {
                return ReplaceOrderOptionalValue.FieldName.PegOffset;
            }
        }
        {
            const byte ReplaceOrderOptionalValueBranchRule9Data2 = 9;
            {
                byte replaceOrderOptionalFieldValue = (byte)this.replaceOrderOptionalField;
                ReplaceOrderOptionalValueBranchRule9Selected = (replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule9Data2);
            }
            if (ReplaceOrderOptionalValueBranchRule9Selected)
            {
                return ReplaceOrderOptionalValue.FieldName.DiscretionPrice;
            }
        }
        {
            const byte ReplaceOrderOptionalValueBranchRule10Data2 = 10;
            {
                byte replaceOrderOptionalFieldValue = (byte)this.replaceOrderOptionalField;
                ReplaceOrderOptionalValueBranchRule10Selected = (replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule10Data2);
            }
            if (ReplaceOrderOptionalValueBranchRule10Selected)
            {
                return ReplaceOrderOptionalValue.FieldName.DiscretionPriceType;
            }
        }
        {
            const byte ReplaceOrderOptionalValueBranchRule11Data2 = 11;
            {
                byte replaceOrderOptionalFieldValue = (byte)this.replaceOrderOptionalField;
                ReplaceOrderOptionalValueBranchRule11Selected = (replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule11Data2);
            }
            if (ReplaceOrderOptionalValueBranchRule11Selected)
            {
                return ReplaceOrderOptionalValue.FieldName.DiscretionPegOffset;
            }
        }
        {
            const byte ReplaceOrderOptionalValueBranchRule12Data2 = 25;
            {
                byte replaceOrderOptionalFieldValue = (byte)this.replaceOrderOptionalField;
                ReplaceOrderOptionalValueBranchRule12Selected = (replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule12Data2);
            }
            if (ReplaceOrderOptionalValueBranchRule12Selected)
            {
                return ReplaceOrderOptionalValue.FieldName.SharesLocated;
            }
        }
        {
            const byte ReplaceOrderOptionalValueBranchRule13Data2 = 26;
            {
                byte replaceOrderOptionalFieldValue = (byte)this.replaceOrderOptionalField;
                ReplaceOrderOptionalValueBranchRule13Selected = (replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule13Data2);
            }
            if (ReplaceOrderOptionalValueBranchRule13Selected)
            {
                return ReplaceOrderOptionalValue.FieldName.LocateBroker;
            }
        }
        {
            const byte ReplaceOrderOptionalValueBranchRule14Data2 = 28;
            {
                byte replaceOrderOptionalFieldValue = (byte)this.replaceOrderOptionalField;
                ReplaceOrderOptionalValueBranchRule14Selected = (replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule14Data2);
            }
            if (ReplaceOrderOptionalValueBranchRule14Selected)
            {
                return ReplaceOrderOptionalValue.FieldName.UserRefIdx;
            }
        }
        {
            const byte ReplaceOrderOptionalValueBranchRule15Data2 = 27;
            {
                byte replaceOrderOptionalFieldValue = (byte)this.replaceOrderOptionalField;
                ReplaceOrderOptionalValueBranchRule15Selected = (replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule15Data2);
            }
            if (ReplaceOrderOptionalValueBranchRule15Selected)
            {
                return ReplaceOrderOptionalValue.FieldName.Side;
            }
        }
        {
            const byte ReplaceOrderOptionalValueBranchRule16Data2 = 29;
            {
                byte replaceOrderOptionalFieldValue = (byte)this.replaceOrderOptionalField;
                ReplaceOrderOptionalValueBranchRule16Selected = (replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule16Data2);
            }
            if (ReplaceOrderOptionalValueBranchRule16Selected)
            {
                return ReplaceOrderOptionalValue.FieldName.AiqStrategy;
            }
        }
        {
            const byte ReplaceOrderOptionalValueBranchRule17Data2 = 30;
            {
                byte replaceOrderOptionalFieldValue = (byte)this.replaceOrderOptionalField;
                ReplaceOrderOptionalValueBranchRule17Selected = (replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule17Data2);
            }
            if (ReplaceOrderOptionalValueBranchRule17Selected)
            {
                return ReplaceOrderOptionalValue.FieldName.AiqGroupId;
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
            position += ReplaceOrderOptionalFieldLength;
            position += this.replaceOrderOptionalValue.EncodedByteLength;
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
    public bool IsRecognized => global::System.Enum.IsDefined(this.replaceOrderOptionalField) && this.replaceOrderOptionalValue.IsRecognized;

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
        ReplaceOrderOptionalField,
        /// <summary>
        ///  Branch
        /// </summary>
        ReplaceOrderOptionalValue,
    }

    /// <summary>
    ///  Provides access to the fields of one instance, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public readonly struct FieldSet : IEnumerable<FieldView>
    {
        private readonly ReplaceOrderAppendage owner;

        internal FieldSet(ReplaceOrderAppendage owner)
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
        public FieldView ReplaceOrderOptionalField => this[FieldName.ReplaceOrderOptionalField];

        /// <summary>
        ///  Branch
        /// </summary>
        public FieldView ReplaceOrderOptionalValue => this[FieldName.ReplaceOrderOptionalValue];

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
        new(nameof(FieldName.ReplaceOrderOptionalField), FieldKind.Enum, ReplaceOrderOptionalFieldLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.ReplaceOrderOptionalValue), FieldKind.Struct, null),
    ];

    /// <summary>
    ///  Gets the fields in wire order, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public FieldSet Fields => new(this);

    IEnumerable<FieldView> IGroupEntry.Fields => new FieldSet(this);

    /// <summary>
    ///  Initializes a new instance of the <see cref="ReplaceOrderAppendage"/> class with default values.
    /// </summary>
    public ReplaceOrderAppendage()
    {
        Clear();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="ReplaceOrderAppendage"/> class with the values, bytes, and parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public ReplaceOrderAppendage(ReplaceOrderAppendage other) : this()
    {
        CopyFrom(other);
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(ReplaceOrderAppendage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.optionalFieldLength = other.optionalFieldLength;
        this.replaceOrderOptionalField = other.replaceOrderOptionalField;
        ((IFieldOwner)this.replaceOrderOptionalValue).Owner = null;
        this.replaceOrderOptionalValue = new ReplaceOrderOptionalValue(other.replaceOrderOptionalValue);
        ((IFieldOwner)this.replaceOrderOptionalValue).Owner = this;
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
        this.replaceOrderOptionalField = default;
        ((IFieldOwner)this.replaceOrderOptionalValue).Owner = null;
        this.replaceOrderOptionalValue = new ReplaceOrderOptionalValue();
        ((IFieldOwner)this.replaceOrderOptionalValue).Owner = this;
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
    public static ReplaceOrderAppendage Parse(ReadOnlySpan<byte> data)
    {
        var instance = new ReplaceOrderAppendage();
        instance.ParseFrom(data);
        return instance;
    }

    /// <summary>
    ///  Parses a new instance starting at the specified offset.
    /// </summary>
    /// <param name="data">The bytes to parse.</param>
    /// <param name="offset">The offset at which to start. A successful parse advances it past the parsed bytes, and a failed parse does not change it.</param>
    /// <returns>The new instance. Check Status to determine whether the parse succeeded.</returns>
    public static ReplaceOrderAppendage Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        var instance = new ReplaceOrderAppendage();
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
            int ReplaceOrderAppendageSizeRule1End;
            global::System.Int128 ReplaceOrderAppendageSizeRule1Width;
            byte replaceOrderAppendageOptionalFieldLengthValue = this.optionalFieldLength;
            int? replaceOrderAppendageEnd = null;
            const long ReplaceOrderAppendageSizeRule1Data2 = 1;
            ReplaceOrderAppendageSizeRule1Width = checked((checked((global::System.Int128)(replaceOrderAppendageOptionalFieldLengthValue)) + ((global::System.Int128)(ReplaceOrderAppendageSizeRule1Data2))));
            if (ReplaceOrderAppendageSizeRule1Width < RuleEmptyWidth || ReplaceOrderAppendageSizeRule1Width > (long)data.Length - start) { Fail(ParseStatus.Truncated, FieldName.OptionalFieldLength, OptionalFieldLengthOffset, data[start..]); return; }
            ReplaceOrderAppendageSizeRule1End = start + (int)ReplaceOrderAppendageSizeRule1Width;
            replaceOrderAppendageEnd = ReplaceOrderAppendageSizeRule1End;
            if (ReplaceOrderAppendageSizeRule1End < cursor) { Fail(ParseStatus.Truncated, FieldName.OptionalFieldLength, OptionalFieldLengthOffset, data[start..]); return; }
            if (replaceOrderAppendageEnd is int replaceOrderAppendageDeclaredEnd)
            {
                data = data[..replaceOrderAppendageDeclaredEnd];
            }
        }
        if (Truncated(data, start, cursor, ReplaceOrderOptionalFieldLength, FieldName.ReplaceOrderOptionalField)) return;
        this.replaceOrderOptionalField = (ReplaceOrderOptionalField)data.Slice(cursor, ReplaceOrderOptionalFieldLength)[0];
        cursor += ReplaceOrderOptionalFieldLength;
        {
            var instance = new ReplaceOrderOptionalValue();
            instance.ParseFrom(data, ref cursor, this.ReplaceOrderOptionalValueCase());
            this.replaceOrderOptionalValue = instance;
            ((IFieldOwner)this.replaceOrderOptionalValue).Owner = this;
            if (this.replaceOrderOptionalValue.Status != ParseStatus.Ok)
            {
                Fail(this.replaceOrderOptionalValue.Status, FieldName.ReplaceOrderOptionalValue, cursor - start + this.replaceOrderOptionalValue.FailedOffset, data[start..]);
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
        if (this.replaceOrderOptionalValue.selectedCase == ReplaceOrderOptionalValue.FieldName.MinQty)
        {
            this.replaceOrderOptionalField = (ReplaceOrderOptionalField)MinQtyTag;
        }
        else if (this.replaceOrderOptionalValue.selectedCase is null)
        {
            if ((this.ReplaceOrderOptionalValueCase() == ReplaceOrderOptionalValue.FieldName.MinQty) != (this.replaceOrderOptionalValue.selectedCase == ReplaceOrderOptionalValue.FieldName.MinQty))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.ReplaceOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.replaceOrderOptionalValue.selectedCase == ReplaceOrderOptionalValue.FieldName.MaxFloor)
        {
            this.replaceOrderOptionalField = (ReplaceOrderOptionalField)MaxFloorTag;
        }
        else if (this.replaceOrderOptionalValue.selectedCase is null)
        {
            if ((this.ReplaceOrderOptionalValueCase() == ReplaceOrderOptionalValue.FieldName.MaxFloor) != (this.replaceOrderOptionalValue.selectedCase == ReplaceOrderOptionalValue.FieldName.MaxFloor))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.ReplaceOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.replaceOrderOptionalValue.selectedCase == ReplaceOrderOptionalValue.FieldName.PriceType)
        {
            this.replaceOrderOptionalField = (ReplaceOrderOptionalField)PriceTypeTag;
        }
        else if (this.replaceOrderOptionalValue.selectedCase is null)
        {
            if ((this.ReplaceOrderOptionalValueCase() == ReplaceOrderOptionalValue.FieldName.PriceType) != (this.replaceOrderOptionalValue.selectedCase == ReplaceOrderOptionalValue.FieldName.PriceType))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.ReplaceOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.replaceOrderOptionalValue.selectedCase == ReplaceOrderOptionalValue.FieldName.PostOnly)
        {
            this.replaceOrderOptionalField = (ReplaceOrderOptionalField)PostOnlyTag;
        }
        else if (this.replaceOrderOptionalValue.selectedCase is null)
        {
            if ((this.ReplaceOrderOptionalValueCase() == ReplaceOrderOptionalValue.FieldName.PostOnly) != (this.replaceOrderOptionalValue.selectedCase == ReplaceOrderOptionalValue.FieldName.PostOnly))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.ReplaceOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.replaceOrderOptionalValue.selectedCase == ReplaceOrderOptionalValue.FieldName.ExpireTime)
        {
            this.replaceOrderOptionalField = (ReplaceOrderOptionalField)ExpireTimeTag;
        }
        else if (this.replaceOrderOptionalValue.selectedCase is null)
        {
            if ((this.ReplaceOrderOptionalValueCase() == ReplaceOrderOptionalValue.FieldName.ExpireTime) != (this.replaceOrderOptionalValue.selectedCase == ReplaceOrderOptionalValue.FieldName.ExpireTime))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.ReplaceOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.replaceOrderOptionalValue.selectedCase == ReplaceOrderOptionalValue.FieldName.TradeNow)
        {
            this.replaceOrderOptionalField = (ReplaceOrderOptionalField)TradeNowTag;
        }
        else if (this.replaceOrderOptionalValue.selectedCase is null)
        {
            if ((this.ReplaceOrderOptionalValueCase() == ReplaceOrderOptionalValue.FieldName.TradeNow) != (this.replaceOrderOptionalValue.selectedCase == ReplaceOrderOptionalValue.FieldName.TradeNow))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.ReplaceOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.replaceOrderOptionalValue.selectedCase == ReplaceOrderOptionalValue.FieldName.HandleInst)
        {
            this.replaceOrderOptionalField = (ReplaceOrderOptionalField)HandleInstTag;
        }
        else if (this.replaceOrderOptionalValue.selectedCase is null)
        {
            if ((this.ReplaceOrderOptionalValueCase() == ReplaceOrderOptionalValue.FieldName.HandleInst) != (this.replaceOrderOptionalValue.selectedCase == ReplaceOrderOptionalValue.FieldName.HandleInst))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.ReplaceOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.replaceOrderOptionalValue.selectedCase == ReplaceOrderOptionalValue.FieldName.RandomReserves)
        {
            this.replaceOrderOptionalField = (ReplaceOrderOptionalField)RandomReservesTag;
        }
        else if (this.replaceOrderOptionalValue.selectedCase is null)
        {
            if ((this.ReplaceOrderOptionalValueCase() == ReplaceOrderOptionalValue.FieldName.RandomReserves) != (this.replaceOrderOptionalValue.selectedCase == ReplaceOrderOptionalValue.FieldName.RandomReserves))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.ReplaceOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.replaceOrderOptionalValue.selectedCase == ReplaceOrderOptionalValue.FieldName.PegOffset)
        {
            this.replaceOrderOptionalField = (ReplaceOrderOptionalField)PegOffsetTag;
        }
        else if (this.replaceOrderOptionalValue.selectedCase is null)
        {
            if ((this.ReplaceOrderOptionalValueCase() == ReplaceOrderOptionalValue.FieldName.PegOffset) != (this.replaceOrderOptionalValue.selectedCase == ReplaceOrderOptionalValue.FieldName.PegOffset))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.ReplaceOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.replaceOrderOptionalValue.selectedCase == ReplaceOrderOptionalValue.FieldName.DiscretionPrice)
        {
            this.replaceOrderOptionalField = (ReplaceOrderOptionalField)DiscretionPriceTag;
        }
        else if (this.replaceOrderOptionalValue.selectedCase is null)
        {
            if ((this.ReplaceOrderOptionalValueCase() == ReplaceOrderOptionalValue.FieldName.DiscretionPrice) != (this.replaceOrderOptionalValue.selectedCase == ReplaceOrderOptionalValue.FieldName.DiscretionPrice))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.ReplaceOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.replaceOrderOptionalValue.selectedCase == ReplaceOrderOptionalValue.FieldName.DiscretionPriceType)
        {
            this.replaceOrderOptionalField = (ReplaceOrderOptionalField)DiscretionPriceTypeTag;
        }
        else if (this.replaceOrderOptionalValue.selectedCase is null)
        {
            if ((this.ReplaceOrderOptionalValueCase() == ReplaceOrderOptionalValue.FieldName.DiscretionPriceType) != (this.replaceOrderOptionalValue.selectedCase == ReplaceOrderOptionalValue.FieldName.DiscretionPriceType))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.ReplaceOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.replaceOrderOptionalValue.selectedCase == ReplaceOrderOptionalValue.FieldName.DiscretionPegOffset)
        {
            this.replaceOrderOptionalField = (ReplaceOrderOptionalField)DiscretionPegOffsetTag;
        }
        else if (this.replaceOrderOptionalValue.selectedCase is null)
        {
            if ((this.ReplaceOrderOptionalValueCase() == ReplaceOrderOptionalValue.FieldName.DiscretionPegOffset) != (this.replaceOrderOptionalValue.selectedCase == ReplaceOrderOptionalValue.FieldName.DiscretionPegOffset))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.ReplaceOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.replaceOrderOptionalValue.selectedCase == ReplaceOrderOptionalValue.FieldName.SharesLocated)
        {
            this.replaceOrderOptionalField = (ReplaceOrderOptionalField)SharesLocatedTag;
        }
        else if (this.replaceOrderOptionalValue.selectedCase is null)
        {
            if ((this.ReplaceOrderOptionalValueCase() == ReplaceOrderOptionalValue.FieldName.SharesLocated) != (this.replaceOrderOptionalValue.selectedCase == ReplaceOrderOptionalValue.FieldName.SharesLocated))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.ReplaceOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.replaceOrderOptionalValue.selectedCase == ReplaceOrderOptionalValue.FieldName.LocateBroker)
        {
            this.replaceOrderOptionalField = (ReplaceOrderOptionalField)LocateBrokerTag;
        }
        else if (this.replaceOrderOptionalValue.selectedCase is null)
        {
            if ((this.ReplaceOrderOptionalValueCase() == ReplaceOrderOptionalValue.FieldName.LocateBroker) != (this.replaceOrderOptionalValue.selectedCase == ReplaceOrderOptionalValue.FieldName.LocateBroker))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.ReplaceOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.replaceOrderOptionalValue.selectedCase == ReplaceOrderOptionalValue.FieldName.UserRefIdx)
        {
            this.replaceOrderOptionalField = (ReplaceOrderOptionalField)UserRefIdxTag;
        }
        else if (this.replaceOrderOptionalValue.selectedCase is null)
        {
            if ((this.ReplaceOrderOptionalValueCase() == ReplaceOrderOptionalValue.FieldName.UserRefIdx) != (this.replaceOrderOptionalValue.selectedCase == ReplaceOrderOptionalValue.FieldName.UserRefIdx))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.ReplaceOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.replaceOrderOptionalValue.selectedCase == ReplaceOrderOptionalValue.FieldName.Side)
        {
            this.replaceOrderOptionalField = (ReplaceOrderOptionalField)SideTag;
        }
        else if (this.replaceOrderOptionalValue.selectedCase is null)
        {
            if ((this.ReplaceOrderOptionalValueCase() == ReplaceOrderOptionalValue.FieldName.Side) != (this.replaceOrderOptionalValue.selectedCase == ReplaceOrderOptionalValue.FieldName.Side))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.ReplaceOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.replaceOrderOptionalValue.selectedCase == ReplaceOrderOptionalValue.FieldName.AiqStrategy)
        {
            this.replaceOrderOptionalField = (ReplaceOrderOptionalField)AiqStrategyTag;
        }
        else if (this.replaceOrderOptionalValue.selectedCase is null)
        {
            if ((this.ReplaceOrderOptionalValueCase() == ReplaceOrderOptionalValue.FieldName.AiqStrategy) != (this.replaceOrderOptionalValue.selectedCase == ReplaceOrderOptionalValue.FieldName.AiqStrategy))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.ReplaceOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.replaceOrderOptionalValue.selectedCase == ReplaceOrderOptionalValue.FieldName.AiqGroupId)
        {
            this.replaceOrderOptionalField = (ReplaceOrderOptionalField)AiqGroupIdTag;
        }
        else if (this.replaceOrderOptionalValue.selectedCase is null)
        {
            if ((this.ReplaceOrderOptionalValueCase() == ReplaceOrderOptionalValue.FieldName.AiqGroupId) != (this.replaceOrderOptionalValue.selectedCase == ReplaceOrderOptionalValue.FieldName.AiqGroupId))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.ReplaceOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        {
            const long ReplaceOrderAppendageSizeRule1Data2 = 1;
            global::System.Int128 measure = EncodedByteLength;
            global::System.Int128 replaceOrderAppendageOptionalFieldLengthStampedValue;
            try
            {
                replaceOrderAppendageOptionalFieldLengthStampedValue = checked((measure - checked((((global::System.Int128)(RuleNumberZero)) + ((global::System.Int128)(ReplaceOrderAppendageSizeRule1Data2))))));
            }
            catch (global::System.OverflowException)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.OptionalFieldLength), EncodeStatus.RuleMismatch);
                return false;
            }
            if (replaceOrderAppendageOptionalFieldLengthStampedValue > byte.MaxValue)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.OptionalFieldLength), EncodeStatus.TooLong);
                return false;
            }
            if (replaceOrderAppendageOptionalFieldLengthStampedValue < byte.MinValue)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.OptionalFieldLength), EncodeStatus.DoesNotFit);
                return false;
            }
            this.optionalFieldLength = checked((byte)(replaceOrderAppendageOptionalFieldLengthStampedValue));
        }
        data.Slice(cursor, OptionalFieldLengthLength)[0] = unchecked((byte)(this.optionalFieldLength));
        cursor += OptionalFieldLengthLength;
        data.Slice(cursor, ReplaceOrderOptionalFieldLength)[0] = unchecked((byte)(this.replaceOrderOptionalField));
        cursor += ReplaceOrderOptionalFieldLength;
        {
            if (!((IGroupEntry)this.replaceOrderOptionalValue).Write(data, cursor, out cursor, out var memberFailure))
            {
                failure = memberFailure.Within(new FieldView(this, (int)FieldName.ReplaceOrderOptionalValue), null);
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
        if (!global::System.Enum.IsDefined(this.replaceOrderOptionalField))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.ReplaceOrderOptionalField), ValidationStatus.UndeclaredValue));
        }
        foreach (var failure in this.replaceOrderOptionalValue.Validate())
        {
            failures.Add(failure.Within(new FieldView(this, (int)FieldName.ReplaceOrderOptionalValue), null));
        }
        return failures;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(ReplaceOrderAppendage? other)
    {
        if (other is null)
        {
            return false;
        }
        if (this.replaceOrderOptionalField != other.replaceOrderOptionalField) return false;
        if (!this.replaceOrderOptionalValue.Equals(other.replaceOrderOptionalValue)) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is ReplaceOrderAppendage other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.replaceOrderOptionalField);
        hash.Add(this.replaceOrderOptionalValue);
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
            if (Has(FieldName.ReplaceOrderOptionalField)) names.Add(nameof(FieldName.ReplaceOrderOptionalField));
            if (Has(FieldName.ReplaceOrderOptionalValue)) names.Add(nameof(FieldName.ReplaceOrderOptionalValue));
            return names.Count == 0 ? "None" : string.Join(", ", names);
        }
    }

    /// <summary>
    ///  Compares this instance with another and identifies the fields that differ.
    /// </summary>
    /// <returns>The fields that differ, or None if there are no differences.</returns>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Changes Diff(ReplaceOrderAppendage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (this.replaceOrderOptionalField != other.replaceOrderOptionalField) diff = diff.With(FieldName.ReplaceOrderOptionalField);
        if (!this.replaceOrderOptionalValue.Equals(other.replaceOrderOptionalValue)) diff = diff.With(FieldName.ReplaceOrderOptionalValue);
        return diff;
    }

    private void CopyField(FieldName field, ReplaceOrderAppendage from)
    {
        switch (field)
        {
            case FieldName.OptionalFieldLength:
                this.optionalFieldLength = from.optionalFieldLength;
                break;
            case FieldName.ReplaceOrderOptionalField:
                this.replaceOrderOptionalField = from.replaceOrderOptionalField;
                break;
            case FieldName.ReplaceOrderOptionalValue:
                ((IFieldOwner)this.replaceOrderOptionalValue).Owner = null;
                this.replaceOrderOptionalValue = new ReplaceOrderOptionalValue(from.replaceOrderOptionalValue);
                ((IFieldOwner)this.replaceOrderOptionalValue).Owner = this;
                break;
            default:
                return;
        }
        Touch();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"ReplaceOrderAppendage {{ OptionalFieldLength: {(Text.Of(this.optionalFieldLength))}, ReplaceOrderOptionalField: {(Text.Enum(this.replaceOrderOptionalField))}, ReplaceOrderOptionalValue: {(Text.Of(this.replaceOrderOptionalValue.ToString()))} }}{(this.trailer.Length == 0 ? string.Empty : $" Trailer: {(Text.Of(this.trailer.Length))} bytes")}";

    private string DebuggerDisplay => $"[{(Text.Of(this.optionalFieldLength))}] [{(Text.Enum(this.replaceOrderOptionalField))}]";

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
        FieldName.ReplaceOrderOptionalField => Text.Enum(this.replaceOrderOptionalField),
        FieldName.ReplaceOrderOptionalValue => Text.Of(this.replaceOrderOptionalValue.ToString()),
        _ => string.Empty,
    };

    bool IFieldOwner.IsRecognized(int field) => (FieldName)field switch
    {
        FieldName.ReplaceOrderOptionalField => global::System.Enum.IsDefined(this.replaceOrderOptionalField),
        FieldName.ReplaceOrderOptionalValue => this.replaceOrderOptionalValue.IsRecognized,
        _ => true,
    };

    bool IFieldOwner.IsPresent(int field) => true;

    bool IFieldOwner.IsParsed(int field) => this.status == ParseStatus.Ok || field < (int?)this.failed;

    bool IFieldOwner.IsReached(int field) => this.modified || this.status is ParseStatus.None or ParseStatus.Ok || field <= (int?)this.failed;

    bool IFieldOwner.HasParseLayout(int field) => false;

    int IFieldOwner.OffsetOf(int field) => (FieldName)field switch
    {
        FieldName.OptionalFieldLength => OptionalFieldLengthOffset,
        FieldName.ReplaceOrderOptionalField => ReplaceOrderOptionalFieldOffset,
        FieldName.ReplaceOrderOptionalValue => ReplaceOrderOptionalValueOffset,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    int IFieldOwner.LengthOf(int field) => (FieldName)field switch
    {
        FieldName.OptionalFieldLength => OptionalFieldLengthLength,
        FieldName.ReplaceOrderOptionalField => ReplaceOrderOptionalFieldLength,
        FieldName.ReplaceOrderOptionalValue => this.replaceOrderOptionalValue.EncodedByteLength,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    IReadOnlyList<IGroupEntry>? IFieldOwner.EntriesOf(int field) => null;

    IMessage? IFieldOwner.MessageOf(int field) => null;

    ulong[] IFieldOwner.ChangedAgainst(IFieldOwner other) => other is ReplaceOrderAppendage typed ? Diff(typed).ToWords() : throw new ArgumentException("The other instance is not of the same type.", nameof(other));

    void IFieldOwner.CopyField(int field, IFieldOwner from) => CopyField((FieldName)field, from as ReplaceOrderAppendage ?? throw new ArgumentException("The other instance is not of the same type.", nameof(from)));
}
