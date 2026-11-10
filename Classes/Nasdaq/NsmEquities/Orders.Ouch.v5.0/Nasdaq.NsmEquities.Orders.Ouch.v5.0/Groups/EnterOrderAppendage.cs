using System.Buffers.Binary;
using System.Diagnostics;
using static Nasdaq.NsmEquities.Orders.Ouch.v5_0.Rules;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Enter Order Appendage
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class EnterOrderAppendage : IGroupEntry, IEquatable<EnterOrderAppendage>, IFieldOwner
{
    internal const int OptionalFieldLengthOffset = 0;

    internal const int OptionalFieldLengthLength = 1;

    internal const int EnterOrderOptionalFieldOffset = OptionalFieldLengthOffset + OptionalFieldLengthLength;

    internal const int EnterOrderOptionalFieldLength = 1;

    internal const int EnterOrderOptionalValueOffset = EnterOrderOptionalFieldOffset + EnterOrderOptionalFieldLength;

    private const byte FirmTag = 2;

    private const byte MinQtyTag = 3;

    private const byte CustomerTypeTag = 4;

    private const byte MaxFloorTag = 5;

    private const byte PriceTypeTag = 6;

    private const byte PegOffsetTag = 7;

    private const byte DiscretionPriceTag = 9;

    private const byte DiscretionPriceTypeTag = 10;

    private const byte DiscretionPegOffsetTag = 11;

    private const byte PostOnlyTag = 12;

    private const byte RandomReservesTag = 13;

    private const byte ExpireTimeTag = 15;

    private const byte TradeNowTag = 16;

    private const byte HandleInstTag = 17;

    private const byte GroupIdTag = 24;

    private const byte SharesLocatedTag = 25;

    private const byte LocateBrokerTag = 26;

    private const byte UserRefIdxTag = 28;

    private const byte AiqStrategyTag = 29;

    private const byte AiqGroupIdTag = 30;

    /// <summary>
    ///  The encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public const int MessageByteLength = OptionalFieldLengthLength + EnterOrderOptionalFieldLength;

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

    internal EnterOrderOptionalField enterOrderOptionalField;

    /// <summary>
    ///  Appendage Id
    /// </summary>
    public EnterOrderOptionalField EnterOrderOptionalField
    {
        get => this.enterOrderOptionalField;
        set
        {
            this.enterOrderOptionalField = value;
            Touch();
        }
    }

    internal EnterOrderOptionalValue enterOrderOptionalValue = new EnterOrderOptionalValue();

    /// <summary>
    ///  Branch
    /// </summary>
    /// <remarks>
    ///  Setting <see langword="null"/> stores the value that Clear stores.
    /// </remarks>
    public EnterOrderOptionalValue EnterOrderOptionalValue
    {
        get => this.enterOrderOptionalValue;
        set
        {
            var copy = value is null ? new EnterOrderOptionalValue() : new EnterOrderOptionalValue(value);
            ((IFieldOwner)this.enterOrderOptionalValue).Owner = null;
            this.enterOrderOptionalValue = copy;
            ((IFieldOwner)this.enterOrderOptionalValue).Owner = this;
            Touch();
        }
    }

    private EnterOrderOptionalValue.FieldName? EnterOrderOptionalValueCase()
    {
        bool EnterOrderOptionalValueBranchRule0Selected;
        bool EnterOrderOptionalValueBranchRule1Selected;
        bool EnterOrderOptionalValueBranchRule2Selected;
        bool EnterOrderOptionalValueBranchRule3Selected;
        bool EnterOrderOptionalValueBranchRule4Selected;
        bool EnterOrderOptionalValueBranchRule5Selected;
        bool EnterOrderOptionalValueBranchRule6Selected;
        bool EnterOrderOptionalValueBranchRule7Selected;
        bool EnterOrderOptionalValueBranchRule8Selected;
        bool EnterOrderOptionalValueBranchRule9Selected;
        bool EnterOrderOptionalValueBranchRule10Selected;
        bool EnterOrderOptionalValueBranchRule11Selected;
        bool EnterOrderOptionalValueBranchRule12Selected;
        bool EnterOrderOptionalValueBranchRule13Selected;
        bool EnterOrderOptionalValueBranchRule14Selected;
        bool EnterOrderOptionalValueBranchRule15Selected;
        bool EnterOrderOptionalValueBranchRule16Selected;
        bool EnterOrderOptionalValueBranchRule17Selected;
        bool EnterOrderOptionalValueBranchRule18Selected;
        bool EnterOrderOptionalValueBranchRule19Selected;
        {
            const byte EnterOrderOptionalValueBranchRule0Data2 = 2;
            {
                byte enterOrderOptionalFieldValue = (byte)this.enterOrderOptionalField;
                EnterOrderOptionalValueBranchRule0Selected = (enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule0Data2);
            }
            if (EnterOrderOptionalValueBranchRule0Selected)
            {
                return EnterOrderOptionalValue.FieldName.Firm;
            }
        }
        {
            const byte EnterOrderOptionalValueBranchRule1Data2 = 3;
            {
                byte enterOrderOptionalFieldValue = (byte)this.enterOrderOptionalField;
                EnterOrderOptionalValueBranchRule1Selected = (enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule1Data2);
            }
            if (EnterOrderOptionalValueBranchRule1Selected)
            {
                return EnterOrderOptionalValue.FieldName.MinQty;
            }
        }
        {
            const byte EnterOrderOptionalValueBranchRule2Data2 = 4;
            {
                byte enterOrderOptionalFieldValue = (byte)this.enterOrderOptionalField;
                EnterOrderOptionalValueBranchRule2Selected = (enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule2Data2);
            }
            if (EnterOrderOptionalValueBranchRule2Selected)
            {
                return EnterOrderOptionalValue.FieldName.CustomerType;
            }
        }
        {
            const byte EnterOrderOptionalValueBranchRule3Data2 = 5;
            {
                byte enterOrderOptionalFieldValue = (byte)this.enterOrderOptionalField;
                EnterOrderOptionalValueBranchRule3Selected = (enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule3Data2);
            }
            if (EnterOrderOptionalValueBranchRule3Selected)
            {
                return EnterOrderOptionalValue.FieldName.MaxFloor;
            }
        }
        {
            const byte EnterOrderOptionalValueBranchRule4Data2 = 6;
            {
                byte enterOrderOptionalFieldValue = (byte)this.enterOrderOptionalField;
                EnterOrderOptionalValueBranchRule4Selected = (enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule4Data2);
            }
            if (EnterOrderOptionalValueBranchRule4Selected)
            {
                return EnterOrderOptionalValue.FieldName.PriceType;
            }
        }
        {
            const byte EnterOrderOptionalValueBranchRule5Data2 = 7;
            {
                byte enterOrderOptionalFieldValue = (byte)this.enterOrderOptionalField;
                EnterOrderOptionalValueBranchRule5Selected = (enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule5Data2);
            }
            if (EnterOrderOptionalValueBranchRule5Selected)
            {
                return EnterOrderOptionalValue.FieldName.PegOffset;
            }
        }
        {
            const byte EnterOrderOptionalValueBranchRule6Data2 = 9;
            {
                byte enterOrderOptionalFieldValue = (byte)this.enterOrderOptionalField;
                EnterOrderOptionalValueBranchRule6Selected = (enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule6Data2);
            }
            if (EnterOrderOptionalValueBranchRule6Selected)
            {
                return EnterOrderOptionalValue.FieldName.DiscretionPrice;
            }
        }
        {
            const byte EnterOrderOptionalValueBranchRule7Data2 = 10;
            {
                byte enterOrderOptionalFieldValue = (byte)this.enterOrderOptionalField;
                EnterOrderOptionalValueBranchRule7Selected = (enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule7Data2);
            }
            if (EnterOrderOptionalValueBranchRule7Selected)
            {
                return EnterOrderOptionalValue.FieldName.DiscretionPriceType;
            }
        }
        {
            const byte EnterOrderOptionalValueBranchRule8Data2 = 11;
            {
                byte enterOrderOptionalFieldValue = (byte)this.enterOrderOptionalField;
                EnterOrderOptionalValueBranchRule8Selected = (enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule8Data2);
            }
            if (EnterOrderOptionalValueBranchRule8Selected)
            {
                return EnterOrderOptionalValue.FieldName.DiscretionPegOffset;
            }
        }
        {
            const byte EnterOrderOptionalValueBranchRule9Data2 = 12;
            {
                byte enterOrderOptionalFieldValue = (byte)this.enterOrderOptionalField;
                EnterOrderOptionalValueBranchRule9Selected = (enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule9Data2);
            }
            if (EnterOrderOptionalValueBranchRule9Selected)
            {
                return EnterOrderOptionalValue.FieldName.PostOnly;
            }
        }
        {
            const byte EnterOrderOptionalValueBranchRule10Data2 = 13;
            {
                byte enterOrderOptionalFieldValue = (byte)this.enterOrderOptionalField;
                EnterOrderOptionalValueBranchRule10Selected = (enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule10Data2);
            }
            if (EnterOrderOptionalValueBranchRule10Selected)
            {
                return EnterOrderOptionalValue.FieldName.RandomReserves;
            }
        }
        {
            const byte EnterOrderOptionalValueBranchRule11Data2 = 15;
            {
                byte enterOrderOptionalFieldValue = (byte)this.enterOrderOptionalField;
                EnterOrderOptionalValueBranchRule11Selected = (enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule11Data2);
            }
            if (EnterOrderOptionalValueBranchRule11Selected)
            {
                return EnterOrderOptionalValue.FieldName.ExpireTime;
            }
        }
        {
            const byte EnterOrderOptionalValueBranchRule12Data2 = 16;
            {
                byte enterOrderOptionalFieldValue = (byte)this.enterOrderOptionalField;
                EnterOrderOptionalValueBranchRule12Selected = (enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule12Data2);
            }
            if (EnterOrderOptionalValueBranchRule12Selected)
            {
                return EnterOrderOptionalValue.FieldName.TradeNow;
            }
        }
        {
            const byte EnterOrderOptionalValueBranchRule13Data2 = 17;
            {
                byte enterOrderOptionalFieldValue = (byte)this.enterOrderOptionalField;
                EnterOrderOptionalValueBranchRule13Selected = (enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule13Data2);
            }
            if (EnterOrderOptionalValueBranchRule13Selected)
            {
                return EnterOrderOptionalValue.FieldName.HandleInst;
            }
        }
        {
            const byte EnterOrderOptionalValueBranchRule14Data2 = 24;
            {
                byte enterOrderOptionalFieldValue = (byte)this.enterOrderOptionalField;
                EnterOrderOptionalValueBranchRule14Selected = (enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule14Data2);
            }
            if (EnterOrderOptionalValueBranchRule14Selected)
            {
                return EnterOrderOptionalValue.FieldName.GroupId;
            }
        }
        {
            const byte EnterOrderOptionalValueBranchRule15Data2 = 25;
            {
                byte enterOrderOptionalFieldValue = (byte)this.enterOrderOptionalField;
                EnterOrderOptionalValueBranchRule15Selected = (enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule15Data2);
            }
            if (EnterOrderOptionalValueBranchRule15Selected)
            {
                return EnterOrderOptionalValue.FieldName.SharesLocated;
            }
        }
        {
            const byte EnterOrderOptionalValueBranchRule16Data2 = 26;
            {
                byte enterOrderOptionalFieldValue = (byte)this.enterOrderOptionalField;
                EnterOrderOptionalValueBranchRule16Selected = (enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule16Data2);
            }
            if (EnterOrderOptionalValueBranchRule16Selected)
            {
                return EnterOrderOptionalValue.FieldName.LocateBroker;
            }
        }
        {
            const byte EnterOrderOptionalValueBranchRule17Data2 = 28;
            {
                byte enterOrderOptionalFieldValue = (byte)this.enterOrderOptionalField;
                EnterOrderOptionalValueBranchRule17Selected = (enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule17Data2);
            }
            if (EnterOrderOptionalValueBranchRule17Selected)
            {
                return EnterOrderOptionalValue.FieldName.UserRefIdx;
            }
        }
        {
            const byte EnterOrderOptionalValueBranchRule18Data2 = 29;
            {
                byte enterOrderOptionalFieldValue = (byte)this.enterOrderOptionalField;
                EnterOrderOptionalValueBranchRule18Selected = (enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule18Data2);
            }
            if (EnterOrderOptionalValueBranchRule18Selected)
            {
                return EnterOrderOptionalValue.FieldName.AiqStrategy;
            }
        }
        {
            const byte EnterOrderOptionalValueBranchRule19Data2 = 30;
            {
                byte enterOrderOptionalFieldValue = (byte)this.enterOrderOptionalField;
                EnterOrderOptionalValueBranchRule19Selected = (enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule19Data2);
            }
            if (EnterOrderOptionalValueBranchRule19Selected)
            {
                return EnterOrderOptionalValue.FieldName.AiqGroupId;
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
            position += EnterOrderOptionalFieldLength;
            position += this.enterOrderOptionalValue.EncodedByteLength;
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
    public bool IsRecognized => global::System.Enum.IsDefined(this.enterOrderOptionalField) && this.enterOrderOptionalValue.IsRecognized;

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
        EnterOrderOptionalField,
        /// <summary>
        ///  Branch
        /// </summary>
        EnterOrderOptionalValue,
    }

    /// <summary>
    ///  Provides access to the fields of one instance, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public readonly struct FieldSet : IEnumerable<FieldView>
    {
        private readonly EnterOrderAppendage owner;

        internal FieldSet(EnterOrderAppendage owner)
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
        public FieldView EnterOrderOptionalField => this[FieldName.EnterOrderOptionalField];

        /// <summary>
        ///  Branch
        /// </summary>
        public FieldView EnterOrderOptionalValue => this[FieldName.EnterOrderOptionalValue];

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
        new(nameof(FieldName.EnterOrderOptionalField), FieldKind.Enum, EnterOrderOptionalFieldLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.EnterOrderOptionalValue), FieldKind.Struct, null),
    ];

    /// <summary>
    ///  Gets the fields in wire order, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public FieldSet Fields => new(this);

    IEnumerable<FieldView> IGroupEntry.Fields => new FieldSet(this);

    /// <summary>
    ///  Initializes a new instance of the <see cref="EnterOrderAppendage"/> class with default values.
    /// </summary>
    public EnterOrderAppendage()
    {
        Clear();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="EnterOrderAppendage"/> class with the values, bytes, and parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public EnterOrderAppendage(EnterOrderAppendage other) : this()
    {
        CopyFrom(other);
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(EnterOrderAppendage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.optionalFieldLength = other.optionalFieldLength;
        this.enterOrderOptionalField = other.enterOrderOptionalField;
        ((IFieldOwner)this.enterOrderOptionalValue).Owner = null;
        this.enterOrderOptionalValue = new EnterOrderOptionalValue(other.enterOrderOptionalValue);
        ((IFieldOwner)this.enterOrderOptionalValue).Owner = this;
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
        this.enterOrderOptionalField = default;
        ((IFieldOwner)this.enterOrderOptionalValue).Owner = null;
        this.enterOrderOptionalValue = new EnterOrderOptionalValue();
        ((IFieldOwner)this.enterOrderOptionalValue).Owner = this;
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
    public static EnterOrderAppendage Parse(ReadOnlySpan<byte> data)
    {
        var instance = new EnterOrderAppendage();
        instance.ParseFrom(data);
        return instance;
    }

    /// <summary>
    ///  Parses a new instance starting at the specified offset.
    /// </summary>
    /// <param name="data">The bytes to parse.</param>
    /// <param name="offset">The offset at which to start. A successful parse advances it past the parsed bytes, and a failed parse does not change it.</param>
    /// <returns>The new instance. Check Status to determine whether the parse succeeded.</returns>
    public static EnterOrderAppendage Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        var instance = new EnterOrderAppendage();
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
            int EnterOrderAppendageSizeRule1End;
            global::System.Int128 EnterOrderAppendageSizeRule1Width;
            byte enterOrderAppendageOptionalFieldLengthValue = this.optionalFieldLength;
            int? enterOrderAppendageEnd = null;
            const long EnterOrderAppendageSizeRule1Data2 = 1;
            EnterOrderAppendageSizeRule1Width = checked((checked((global::System.Int128)(enterOrderAppendageOptionalFieldLengthValue)) + ((global::System.Int128)(EnterOrderAppendageSizeRule1Data2))));
            if (EnterOrderAppendageSizeRule1Width < RuleEmptyWidth || EnterOrderAppendageSizeRule1Width > (long)data.Length - start) { Fail(ParseStatus.Truncated, FieldName.OptionalFieldLength, OptionalFieldLengthOffset, data[start..]); return; }
            EnterOrderAppendageSizeRule1End = start + (int)EnterOrderAppendageSizeRule1Width;
            enterOrderAppendageEnd = EnterOrderAppendageSizeRule1End;
            if (EnterOrderAppendageSizeRule1End < cursor) { Fail(ParseStatus.Truncated, FieldName.OptionalFieldLength, OptionalFieldLengthOffset, data[start..]); return; }
            if (enterOrderAppendageEnd is int enterOrderAppendageDeclaredEnd)
            {
                data = data[..enterOrderAppendageDeclaredEnd];
            }
        }
        if (Truncated(data, start, cursor, EnterOrderOptionalFieldLength, FieldName.EnterOrderOptionalField)) return;
        this.enterOrderOptionalField = (EnterOrderOptionalField)data.Slice(cursor, EnterOrderOptionalFieldLength)[0];
        cursor += EnterOrderOptionalFieldLength;
        {
            var instance = new EnterOrderOptionalValue();
            instance.ParseFrom(data, ref cursor, this.EnterOrderOptionalValueCase());
            this.enterOrderOptionalValue = instance;
            ((IFieldOwner)this.enterOrderOptionalValue).Owner = this;
            if (this.enterOrderOptionalValue.Status != ParseStatus.Ok)
            {
                Fail(this.enterOrderOptionalValue.Status, FieldName.EnterOrderOptionalValue, cursor - start + this.enterOrderOptionalValue.FailedOffset, data[start..]);
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
        if (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.Firm)
        {
            this.enterOrderOptionalField = (EnterOrderOptionalField)FirmTag;
        }
        else if (this.enterOrderOptionalValue.selectedCase is null)
        {
            if ((this.EnterOrderOptionalValueCase() == EnterOrderOptionalValue.FieldName.Firm) != (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.Firm))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.EnterOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.MinQty)
        {
            this.enterOrderOptionalField = (EnterOrderOptionalField)MinQtyTag;
        }
        else if (this.enterOrderOptionalValue.selectedCase is null)
        {
            if ((this.EnterOrderOptionalValueCase() == EnterOrderOptionalValue.FieldName.MinQty) != (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.MinQty))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.EnterOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.CustomerType)
        {
            this.enterOrderOptionalField = (EnterOrderOptionalField)CustomerTypeTag;
        }
        else if (this.enterOrderOptionalValue.selectedCase is null)
        {
            if ((this.EnterOrderOptionalValueCase() == EnterOrderOptionalValue.FieldName.CustomerType) != (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.CustomerType))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.EnterOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.MaxFloor)
        {
            this.enterOrderOptionalField = (EnterOrderOptionalField)MaxFloorTag;
        }
        else if (this.enterOrderOptionalValue.selectedCase is null)
        {
            if ((this.EnterOrderOptionalValueCase() == EnterOrderOptionalValue.FieldName.MaxFloor) != (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.MaxFloor))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.EnterOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.PriceType)
        {
            this.enterOrderOptionalField = (EnterOrderOptionalField)PriceTypeTag;
        }
        else if (this.enterOrderOptionalValue.selectedCase is null)
        {
            if ((this.EnterOrderOptionalValueCase() == EnterOrderOptionalValue.FieldName.PriceType) != (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.PriceType))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.EnterOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.PegOffset)
        {
            this.enterOrderOptionalField = (EnterOrderOptionalField)PegOffsetTag;
        }
        else if (this.enterOrderOptionalValue.selectedCase is null)
        {
            if ((this.EnterOrderOptionalValueCase() == EnterOrderOptionalValue.FieldName.PegOffset) != (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.PegOffset))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.EnterOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.DiscretionPrice)
        {
            this.enterOrderOptionalField = (EnterOrderOptionalField)DiscretionPriceTag;
        }
        else if (this.enterOrderOptionalValue.selectedCase is null)
        {
            if ((this.EnterOrderOptionalValueCase() == EnterOrderOptionalValue.FieldName.DiscretionPrice) != (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.DiscretionPrice))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.EnterOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.DiscretionPriceType)
        {
            this.enterOrderOptionalField = (EnterOrderOptionalField)DiscretionPriceTypeTag;
        }
        else if (this.enterOrderOptionalValue.selectedCase is null)
        {
            if ((this.EnterOrderOptionalValueCase() == EnterOrderOptionalValue.FieldName.DiscretionPriceType) != (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.DiscretionPriceType))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.EnterOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.DiscretionPegOffset)
        {
            this.enterOrderOptionalField = (EnterOrderOptionalField)DiscretionPegOffsetTag;
        }
        else if (this.enterOrderOptionalValue.selectedCase is null)
        {
            if ((this.EnterOrderOptionalValueCase() == EnterOrderOptionalValue.FieldName.DiscretionPegOffset) != (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.DiscretionPegOffset))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.EnterOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.PostOnly)
        {
            this.enterOrderOptionalField = (EnterOrderOptionalField)PostOnlyTag;
        }
        else if (this.enterOrderOptionalValue.selectedCase is null)
        {
            if ((this.EnterOrderOptionalValueCase() == EnterOrderOptionalValue.FieldName.PostOnly) != (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.PostOnly))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.EnterOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.RandomReserves)
        {
            this.enterOrderOptionalField = (EnterOrderOptionalField)RandomReservesTag;
        }
        else if (this.enterOrderOptionalValue.selectedCase is null)
        {
            if ((this.EnterOrderOptionalValueCase() == EnterOrderOptionalValue.FieldName.RandomReserves) != (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.RandomReserves))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.EnterOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.ExpireTime)
        {
            this.enterOrderOptionalField = (EnterOrderOptionalField)ExpireTimeTag;
        }
        else if (this.enterOrderOptionalValue.selectedCase is null)
        {
            if ((this.EnterOrderOptionalValueCase() == EnterOrderOptionalValue.FieldName.ExpireTime) != (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.ExpireTime))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.EnterOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.TradeNow)
        {
            this.enterOrderOptionalField = (EnterOrderOptionalField)TradeNowTag;
        }
        else if (this.enterOrderOptionalValue.selectedCase is null)
        {
            if ((this.EnterOrderOptionalValueCase() == EnterOrderOptionalValue.FieldName.TradeNow) != (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.TradeNow))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.EnterOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.HandleInst)
        {
            this.enterOrderOptionalField = (EnterOrderOptionalField)HandleInstTag;
        }
        else if (this.enterOrderOptionalValue.selectedCase is null)
        {
            if ((this.EnterOrderOptionalValueCase() == EnterOrderOptionalValue.FieldName.HandleInst) != (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.HandleInst))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.EnterOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.GroupId)
        {
            this.enterOrderOptionalField = (EnterOrderOptionalField)GroupIdTag;
        }
        else if (this.enterOrderOptionalValue.selectedCase is null)
        {
            if ((this.EnterOrderOptionalValueCase() == EnterOrderOptionalValue.FieldName.GroupId) != (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.GroupId))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.EnterOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.SharesLocated)
        {
            this.enterOrderOptionalField = (EnterOrderOptionalField)SharesLocatedTag;
        }
        else if (this.enterOrderOptionalValue.selectedCase is null)
        {
            if ((this.EnterOrderOptionalValueCase() == EnterOrderOptionalValue.FieldName.SharesLocated) != (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.SharesLocated))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.EnterOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.LocateBroker)
        {
            this.enterOrderOptionalField = (EnterOrderOptionalField)LocateBrokerTag;
        }
        else if (this.enterOrderOptionalValue.selectedCase is null)
        {
            if ((this.EnterOrderOptionalValueCase() == EnterOrderOptionalValue.FieldName.LocateBroker) != (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.LocateBroker))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.EnterOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.UserRefIdx)
        {
            this.enterOrderOptionalField = (EnterOrderOptionalField)UserRefIdxTag;
        }
        else if (this.enterOrderOptionalValue.selectedCase is null)
        {
            if ((this.EnterOrderOptionalValueCase() == EnterOrderOptionalValue.FieldName.UserRefIdx) != (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.UserRefIdx))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.EnterOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.AiqStrategy)
        {
            this.enterOrderOptionalField = (EnterOrderOptionalField)AiqStrategyTag;
        }
        else if (this.enterOrderOptionalValue.selectedCase is null)
        {
            if ((this.EnterOrderOptionalValueCase() == EnterOrderOptionalValue.FieldName.AiqStrategy) != (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.AiqStrategy))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.EnterOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.AiqGroupId)
        {
            this.enterOrderOptionalField = (EnterOrderOptionalField)AiqGroupIdTag;
        }
        else if (this.enterOrderOptionalValue.selectedCase is null)
        {
            if ((this.EnterOrderOptionalValueCase() == EnterOrderOptionalValue.FieldName.AiqGroupId) != (this.enterOrderOptionalValue.selectedCase == EnterOrderOptionalValue.FieldName.AiqGroupId))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.EnterOrderOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        {
            const long EnterOrderAppendageSizeRule1Data2 = 1;
            global::System.Int128 measure = EncodedByteLength;
            global::System.Int128 enterOrderAppendageOptionalFieldLengthStampedValue;
            try
            {
                enterOrderAppendageOptionalFieldLengthStampedValue = checked((measure - checked((((global::System.Int128)(RuleNumberZero)) + ((global::System.Int128)(EnterOrderAppendageSizeRule1Data2))))));
            }
            catch (global::System.OverflowException)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.OptionalFieldLength), EncodeStatus.RuleMismatch);
                return false;
            }
            if (enterOrderAppendageOptionalFieldLengthStampedValue > byte.MaxValue)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.OptionalFieldLength), EncodeStatus.TooLong);
                return false;
            }
            if (enterOrderAppendageOptionalFieldLengthStampedValue < byte.MinValue)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.OptionalFieldLength), EncodeStatus.DoesNotFit);
                return false;
            }
            this.optionalFieldLength = checked((byte)(enterOrderAppendageOptionalFieldLengthStampedValue));
        }
        data.Slice(cursor, OptionalFieldLengthLength)[0] = unchecked((byte)(this.optionalFieldLength));
        cursor += OptionalFieldLengthLength;
        data.Slice(cursor, EnterOrderOptionalFieldLength)[0] = unchecked((byte)(this.enterOrderOptionalField));
        cursor += EnterOrderOptionalFieldLength;
        {
            if (!((IGroupEntry)this.enterOrderOptionalValue).Write(data, cursor, out cursor, out var memberFailure))
            {
                failure = memberFailure.Within(new FieldView(this, (int)FieldName.EnterOrderOptionalValue), null);
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
        if (!global::System.Enum.IsDefined(this.enterOrderOptionalField))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.EnterOrderOptionalField), ValidationStatus.UndeclaredValue));
        }
        foreach (var failure in this.enterOrderOptionalValue.Validate())
        {
            failures.Add(failure.Within(new FieldView(this, (int)FieldName.EnterOrderOptionalValue), null));
        }
        return failures;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(EnterOrderAppendage? other)
    {
        if (other is null)
        {
            return false;
        }
        if (this.enterOrderOptionalField != other.enterOrderOptionalField) return false;
        if (!this.enterOrderOptionalValue.Equals(other.enterOrderOptionalValue)) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is EnterOrderAppendage other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.enterOrderOptionalField);
        hash.Add(this.enterOrderOptionalValue);
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
            if (Has(FieldName.EnterOrderOptionalField)) names.Add(nameof(FieldName.EnterOrderOptionalField));
            if (Has(FieldName.EnterOrderOptionalValue)) names.Add(nameof(FieldName.EnterOrderOptionalValue));
            return names.Count == 0 ? "None" : string.Join(", ", names);
        }
    }

    /// <summary>
    ///  Compares this instance with another and identifies the fields that differ.
    /// </summary>
    /// <returns>The fields that differ, or None if there are no differences.</returns>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Changes Diff(EnterOrderAppendage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (this.enterOrderOptionalField != other.enterOrderOptionalField) diff = diff.With(FieldName.EnterOrderOptionalField);
        if (!this.enterOrderOptionalValue.Equals(other.enterOrderOptionalValue)) diff = diff.With(FieldName.EnterOrderOptionalValue);
        return diff;
    }

    private void CopyField(FieldName field, EnterOrderAppendage from)
    {
        switch (field)
        {
            case FieldName.OptionalFieldLength:
                this.optionalFieldLength = from.optionalFieldLength;
                break;
            case FieldName.EnterOrderOptionalField:
                this.enterOrderOptionalField = from.enterOrderOptionalField;
                break;
            case FieldName.EnterOrderOptionalValue:
                ((IFieldOwner)this.enterOrderOptionalValue).Owner = null;
                this.enterOrderOptionalValue = new EnterOrderOptionalValue(from.enterOrderOptionalValue);
                ((IFieldOwner)this.enterOrderOptionalValue).Owner = this;
                break;
            default:
                return;
        }
        Touch();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"EnterOrderAppendage {{ OptionalFieldLength: {(Text.Of(this.optionalFieldLength))}, EnterOrderOptionalField: {(Text.Enum(this.enterOrderOptionalField))}, EnterOrderOptionalValue: {(Text.Of(this.enterOrderOptionalValue.ToString()))} }}{(this.trailer.Length == 0 ? string.Empty : $" Trailer: {(Text.Of(this.trailer.Length))} bytes")}";

    private string DebuggerDisplay => $"[{(Text.Of(this.optionalFieldLength))}] [{(Text.Enum(this.enterOrderOptionalField))}]";

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
        FieldName.EnterOrderOptionalField => Text.Enum(this.enterOrderOptionalField),
        FieldName.EnterOrderOptionalValue => Text.Of(this.enterOrderOptionalValue.ToString()),
        _ => string.Empty,
    };

    bool IFieldOwner.IsRecognized(int field) => (FieldName)field switch
    {
        FieldName.EnterOrderOptionalField => global::System.Enum.IsDefined(this.enterOrderOptionalField),
        FieldName.EnterOrderOptionalValue => this.enterOrderOptionalValue.IsRecognized,
        _ => true,
    };

    bool IFieldOwner.IsPresent(int field) => true;

    bool IFieldOwner.IsParsed(int field) => this.status == ParseStatus.Ok || field < (int?)this.failed;

    bool IFieldOwner.IsReached(int field) => this.modified || this.status is ParseStatus.None or ParseStatus.Ok || field <= (int?)this.failed;

    bool IFieldOwner.HasParseLayout(int field) => false;

    int IFieldOwner.OffsetOf(int field) => (FieldName)field switch
    {
        FieldName.OptionalFieldLength => OptionalFieldLengthOffset,
        FieldName.EnterOrderOptionalField => EnterOrderOptionalFieldOffset,
        FieldName.EnterOrderOptionalValue => EnterOrderOptionalValueOffset,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    int IFieldOwner.LengthOf(int field) => (FieldName)field switch
    {
        FieldName.OptionalFieldLength => OptionalFieldLengthLength,
        FieldName.EnterOrderOptionalField => EnterOrderOptionalFieldLength,
        FieldName.EnterOrderOptionalValue => this.enterOrderOptionalValue.EncodedByteLength,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    IReadOnlyList<IGroupEntry>? IFieldOwner.EntriesOf(int field) => null;

    IMessage? IFieldOwner.MessageOf(int field) => null;

    ulong[] IFieldOwner.ChangedAgainst(IFieldOwner other) => other is EnterOrderAppendage typed ? Diff(typed).ToWords() : throw new ArgumentException("The other instance is not of the same type.", nameof(other));

    void IFieldOwner.CopyField(int field, IFieldOwner from) => CopyField((FieldName)field, from as EnterOrderAppendage ?? throw new ArgumentException("The other instance is not of the same type.", nameof(from)));
}
