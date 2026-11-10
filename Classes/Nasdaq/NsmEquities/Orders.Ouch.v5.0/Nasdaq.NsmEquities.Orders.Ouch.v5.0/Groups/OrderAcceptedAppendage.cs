using System.Buffers.Binary;
using System.Diagnostics;
using static Nasdaq.NsmEquities.Orders.Ouch.v5_0.Rules;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Order Accepted Appendage
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class OrderAcceptedAppendage : IGroupEntry, IEquatable<OrderAcceptedAppendage>, IFieldOwner
{
    internal const int OptionalFieldLengthOffset = 0;

    internal const int OptionalFieldLengthLength = 1;

    internal const int OrderAcceptedOptionalFieldOffset = OptionalFieldLengthOffset + OptionalFieldLengthLength;

    internal const int OrderAcceptedOptionalFieldLength = 1;

    internal const int OrderAcceptedOptionalValueOffset = OrderAcceptedOptionalFieldOffset + OrderAcceptedOptionalFieldLength;

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

    private const byte RouteTag = 14;

    private const byte ExpireTimeTag = 15;

    private const byte TradeNowTag = 16;

    private const byte HandleInstTag = 17;

    private const byte BboWeightIndicatorTag = 18;

    private const byte GroupIdTag = 24;

    private const byte SharesLocatedTag = 25;

    private const byte UserRefIdxTag = 28;

    private const byte AiqStrategyTag = 29;

    private const byte AiqGroupIdTag = 30;

    /// <summary>
    ///  The encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public const int MessageByteLength = OptionalFieldLengthLength + OrderAcceptedOptionalFieldLength;

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

    internal OrderAcceptedOptionalField orderAcceptedOptionalField;

    /// <summary>
    ///  Appendage Id
    /// </summary>
    public OrderAcceptedOptionalField OrderAcceptedOptionalField
    {
        get => this.orderAcceptedOptionalField;
        set
        {
            this.orderAcceptedOptionalField = value;
            Touch();
        }
    }

    internal OrderAcceptedOptionalValue orderAcceptedOptionalValue = new OrderAcceptedOptionalValue();

    /// <summary>
    ///  Branch
    /// </summary>
    /// <remarks>
    ///  Setting <see langword="null"/> stores the value that Clear stores.
    /// </remarks>
    public OrderAcceptedOptionalValue OrderAcceptedOptionalValue
    {
        get => this.orderAcceptedOptionalValue;
        set
        {
            var copy = value is null ? new OrderAcceptedOptionalValue() : new OrderAcceptedOptionalValue(value);
            ((IFieldOwner)this.orderAcceptedOptionalValue).Owner = null;
            this.orderAcceptedOptionalValue = copy;
            ((IFieldOwner)this.orderAcceptedOptionalValue).Owner = this;
            Touch();
        }
    }

    private OrderAcceptedOptionalValue.FieldName? OrderAcceptedOptionalValueCase()
    {
        bool OrderAcceptedOptionalValueBranchRule0Selected;
        bool OrderAcceptedOptionalValueBranchRule1Selected;
        bool OrderAcceptedOptionalValueBranchRule2Selected;
        bool OrderAcceptedOptionalValueBranchRule3Selected;
        bool OrderAcceptedOptionalValueBranchRule4Selected;
        bool OrderAcceptedOptionalValueBranchRule5Selected;
        bool OrderAcceptedOptionalValueBranchRule6Selected;
        bool OrderAcceptedOptionalValueBranchRule7Selected;
        bool OrderAcceptedOptionalValueBranchRule8Selected;
        bool OrderAcceptedOptionalValueBranchRule9Selected;
        bool OrderAcceptedOptionalValueBranchRule10Selected;
        bool OrderAcceptedOptionalValueBranchRule11Selected;
        bool OrderAcceptedOptionalValueBranchRule12Selected;
        bool OrderAcceptedOptionalValueBranchRule13Selected;
        bool OrderAcceptedOptionalValueBranchRule14Selected;
        bool OrderAcceptedOptionalValueBranchRule15Selected;
        bool OrderAcceptedOptionalValueBranchRule16Selected;
        bool OrderAcceptedOptionalValueBranchRule17Selected;
        bool OrderAcceptedOptionalValueBranchRule18Selected;
        bool OrderAcceptedOptionalValueBranchRule19Selected;
        bool OrderAcceptedOptionalValueBranchRule20Selected;
        {
            const byte OrderAcceptedOptionalValueBranchRule0Data2 = 2;
            {
                byte orderAcceptedOptionalFieldValue = (byte)this.orderAcceptedOptionalField;
                OrderAcceptedOptionalValueBranchRule0Selected = (orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule0Data2);
            }
            if (OrderAcceptedOptionalValueBranchRule0Selected)
            {
                return OrderAcceptedOptionalValue.FieldName.Firm;
            }
        }
        {
            const byte OrderAcceptedOptionalValueBranchRule1Data2 = 3;
            {
                byte orderAcceptedOptionalFieldValue = (byte)this.orderAcceptedOptionalField;
                OrderAcceptedOptionalValueBranchRule1Selected = (orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule1Data2);
            }
            if (OrderAcceptedOptionalValueBranchRule1Selected)
            {
                return OrderAcceptedOptionalValue.FieldName.MinQty;
            }
        }
        {
            const byte OrderAcceptedOptionalValueBranchRule2Data2 = 4;
            {
                byte orderAcceptedOptionalFieldValue = (byte)this.orderAcceptedOptionalField;
                OrderAcceptedOptionalValueBranchRule2Selected = (orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule2Data2);
            }
            if (OrderAcceptedOptionalValueBranchRule2Selected)
            {
                return OrderAcceptedOptionalValue.FieldName.CustomerType;
            }
        }
        {
            const byte OrderAcceptedOptionalValueBranchRule3Data2 = 5;
            {
                byte orderAcceptedOptionalFieldValue = (byte)this.orderAcceptedOptionalField;
                OrderAcceptedOptionalValueBranchRule3Selected = (orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule3Data2);
            }
            if (OrderAcceptedOptionalValueBranchRule3Selected)
            {
                return OrderAcceptedOptionalValue.FieldName.MaxFloor;
            }
        }
        {
            const byte OrderAcceptedOptionalValueBranchRule4Data2 = 6;
            {
                byte orderAcceptedOptionalFieldValue = (byte)this.orderAcceptedOptionalField;
                OrderAcceptedOptionalValueBranchRule4Selected = (orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule4Data2);
            }
            if (OrderAcceptedOptionalValueBranchRule4Selected)
            {
                return OrderAcceptedOptionalValue.FieldName.PriceType;
            }
        }
        {
            const byte OrderAcceptedOptionalValueBranchRule5Data2 = 7;
            {
                byte orderAcceptedOptionalFieldValue = (byte)this.orderAcceptedOptionalField;
                OrderAcceptedOptionalValueBranchRule5Selected = (orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule5Data2);
            }
            if (OrderAcceptedOptionalValueBranchRule5Selected)
            {
                return OrderAcceptedOptionalValue.FieldName.PegOffset;
            }
        }
        {
            const byte OrderAcceptedOptionalValueBranchRule6Data2 = 9;
            {
                byte orderAcceptedOptionalFieldValue = (byte)this.orderAcceptedOptionalField;
                OrderAcceptedOptionalValueBranchRule6Selected = (orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule6Data2);
            }
            if (OrderAcceptedOptionalValueBranchRule6Selected)
            {
                return OrderAcceptedOptionalValue.FieldName.DiscretionPrice;
            }
        }
        {
            const byte OrderAcceptedOptionalValueBranchRule7Data2 = 10;
            {
                byte orderAcceptedOptionalFieldValue = (byte)this.orderAcceptedOptionalField;
                OrderAcceptedOptionalValueBranchRule7Selected = (orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule7Data2);
            }
            if (OrderAcceptedOptionalValueBranchRule7Selected)
            {
                return OrderAcceptedOptionalValue.FieldName.DiscretionPriceType;
            }
        }
        {
            const byte OrderAcceptedOptionalValueBranchRule8Data2 = 11;
            {
                byte orderAcceptedOptionalFieldValue = (byte)this.orderAcceptedOptionalField;
                OrderAcceptedOptionalValueBranchRule8Selected = (orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule8Data2);
            }
            if (OrderAcceptedOptionalValueBranchRule8Selected)
            {
                return OrderAcceptedOptionalValue.FieldName.DiscretionPegOffset;
            }
        }
        {
            const byte OrderAcceptedOptionalValueBranchRule9Data2 = 12;
            {
                byte orderAcceptedOptionalFieldValue = (byte)this.orderAcceptedOptionalField;
                OrderAcceptedOptionalValueBranchRule9Selected = (orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule9Data2);
            }
            if (OrderAcceptedOptionalValueBranchRule9Selected)
            {
                return OrderAcceptedOptionalValue.FieldName.PostOnly;
            }
        }
        {
            const byte OrderAcceptedOptionalValueBranchRule10Data2 = 13;
            {
                byte orderAcceptedOptionalFieldValue = (byte)this.orderAcceptedOptionalField;
                OrderAcceptedOptionalValueBranchRule10Selected = (orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule10Data2);
            }
            if (OrderAcceptedOptionalValueBranchRule10Selected)
            {
                return OrderAcceptedOptionalValue.FieldName.RandomReserves;
            }
        }
        {
            const byte OrderAcceptedOptionalValueBranchRule11Data2 = 14;
            {
                byte orderAcceptedOptionalFieldValue = (byte)this.orderAcceptedOptionalField;
                OrderAcceptedOptionalValueBranchRule11Selected = (orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule11Data2);
            }
            if (OrderAcceptedOptionalValueBranchRule11Selected)
            {
                return OrderAcceptedOptionalValue.FieldName.Route;
            }
        }
        {
            const byte OrderAcceptedOptionalValueBranchRule12Data2 = 15;
            {
                byte orderAcceptedOptionalFieldValue = (byte)this.orderAcceptedOptionalField;
                OrderAcceptedOptionalValueBranchRule12Selected = (orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule12Data2);
            }
            if (OrderAcceptedOptionalValueBranchRule12Selected)
            {
                return OrderAcceptedOptionalValue.FieldName.ExpireTime;
            }
        }
        {
            const byte OrderAcceptedOptionalValueBranchRule13Data2 = 16;
            {
                byte orderAcceptedOptionalFieldValue = (byte)this.orderAcceptedOptionalField;
                OrderAcceptedOptionalValueBranchRule13Selected = (orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule13Data2);
            }
            if (OrderAcceptedOptionalValueBranchRule13Selected)
            {
                return OrderAcceptedOptionalValue.FieldName.TradeNow;
            }
        }
        {
            const byte OrderAcceptedOptionalValueBranchRule14Data2 = 17;
            {
                byte orderAcceptedOptionalFieldValue = (byte)this.orderAcceptedOptionalField;
                OrderAcceptedOptionalValueBranchRule14Selected = (orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule14Data2);
            }
            if (OrderAcceptedOptionalValueBranchRule14Selected)
            {
                return OrderAcceptedOptionalValue.FieldName.HandleInst;
            }
        }
        {
            const byte OrderAcceptedOptionalValueBranchRule15Data2 = 18;
            {
                byte orderAcceptedOptionalFieldValue = (byte)this.orderAcceptedOptionalField;
                OrderAcceptedOptionalValueBranchRule15Selected = (orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule15Data2);
            }
            if (OrderAcceptedOptionalValueBranchRule15Selected)
            {
                return OrderAcceptedOptionalValue.FieldName.BboWeightIndicator;
            }
        }
        {
            const byte OrderAcceptedOptionalValueBranchRule16Data2 = 24;
            {
                byte orderAcceptedOptionalFieldValue = (byte)this.orderAcceptedOptionalField;
                OrderAcceptedOptionalValueBranchRule16Selected = (orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule16Data2);
            }
            if (OrderAcceptedOptionalValueBranchRule16Selected)
            {
                return OrderAcceptedOptionalValue.FieldName.GroupId;
            }
        }
        {
            const byte OrderAcceptedOptionalValueBranchRule17Data2 = 25;
            {
                byte orderAcceptedOptionalFieldValue = (byte)this.orderAcceptedOptionalField;
                OrderAcceptedOptionalValueBranchRule17Selected = (orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule17Data2);
            }
            if (OrderAcceptedOptionalValueBranchRule17Selected)
            {
                return OrderAcceptedOptionalValue.FieldName.SharesLocated;
            }
        }
        {
            const byte OrderAcceptedOptionalValueBranchRule18Data2 = 28;
            {
                byte orderAcceptedOptionalFieldValue = (byte)this.orderAcceptedOptionalField;
                OrderAcceptedOptionalValueBranchRule18Selected = (orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule18Data2);
            }
            if (OrderAcceptedOptionalValueBranchRule18Selected)
            {
                return OrderAcceptedOptionalValue.FieldName.UserRefIdx;
            }
        }
        {
            const byte OrderAcceptedOptionalValueBranchRule19Data2 = 29;
            {
                byte orderAcceptedOptionalFieldValue = (byte)this.orderAcceptedOptionalField;
                OrderAcceptedOptionalValueBranchRule19Selected = (orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule19Data2);
            }
            if (OrderAcceptedOptionalValueBranchRule19Selected)
            {
                return OrderAcceptedOptionalValue.FieldName.AiqStrategy;
            }
        }
        {
            const byte OrderAcceptedOptionalValueBranchRule20Data2 = 30;
            {
                byte orderAcceptedOptionalFieldValue = (byte)this.orderAcceptedOptionalField;
                OrderAcceptedOptionalValueBranchRule20Selected = (orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule20Data2);
            }
            if (OrderAcceptedOptionalValueBranchRule20Selected)
            {
                return OrderAcceptedOptionalValue.FieldName.AiqGroupId;
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
            position += OrderAcceptedOptionalFieldLength;
            position += this.orderAcceptedOptionalValue.EncodedByteLength;
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
    public bool IsRecognized => global::System.Enum.IsDefined(this.orderAcceptedOptionalField) && this.orderAcceptedOptionalValue.IsRecognized;

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
        OrderAcceptedOptionalField,
        /// <summary>
        ///  Branch
        /// </summary>
        OrderAcceptedOptionalValue,
    }

    /// <summary>
    ///  Provides access to the fields of one instance, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public readonly struct FieldSet : IEnumerable<FieldView>
    {
        private readonly OrderAcceptedAppendage owner;

        internal FieldSet(OrderAcceptedAppendage owner)
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
        public FieldView OrderAcceptedOptionalField => this[FieldName.OrderAcceptedOptionalField];

        /// <summary>
        ///  Branch
        /// </summary>
        public FieldView OrderAcceptedOptionalValue => this[FieldName.OrderAcceptedOptionalValue];

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
        new(nameof(FieldName.OrderAcceptedOptionalField), FieldKind.Enum, OrderAcceptedOptionalFieldLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.OrderAcceptedOptionalValue), FieldKind.Struct, null),
    ];

    /// <summary>
    ///  Gets the fields in wire order, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public FieldSet Fields => new(this);

    IEnumerable<FieldView> IGroupEntry.Fields => new FieldSet(this);

    /// <summary>
    ///  Initializes a new instance of the <see cref="OrderAcceptedAppendage"/> class with default values.
    /// </summary>
    public OrderAcceptedAppendage()
    {
        Clear();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="OrderAcceptedAppendage"/> class with the values, bytes, and parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public OrderAcceptedAppendage(OrderAcceptedAppendage other) : this()
    {
        CopyFrom(other);
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(OrderAcceptedAppendage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.optionalFieldLength = other.optionalFieldLength;
        this.orderAcceptedOptionalField = other.orderAcceptedOptionalField;
        ((IFieldOwner)this.orderAcceptedOptionalValue).Owner = null;
        this.orderAcceptedOptionalValue = new OrderAcceptedOptionalValue(other.orderAcceptedOptionalValue);
        ((IFieldOwner)this.orderAcceptedOptionalValue).Owner = this;
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
        this.orderAcceptedOptionalField = default;
        ((IFieldOwner)this.orderAcceptedOptionalValue).Owner = null;
        this.orderAcceptedOptionalValue = new OrderAcceptedOptionalValue();
        ((IFieldOwner)this.orderAcceptedOptionalValue).Owner = this;
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
    public static OrderAcceptedAppendage Parse(ReadOnlySpan<byte> data)
    {
        var instance = new OrderAcceptedAppendage();
        instance.ParseFrom(data);
        return instance;
    }

    /// <summary>
    ///  Parses a new instance starting at the specified offset.
    /// </summary>
    /// <param name="data">The bytes to parse.</param>
    /// <param name="offset">The offset at which to start. A successful parse advances it past the parsed bytes, and a failed parse does not change it.</param>
    /// <returns>The new instance. Check Status to determine whether the parse succeeded.</returns>
    public static OrderAcceptedAppendage Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        var instance = new OrderAcceptedAppendage();
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
            int OrderAcceptedAppendageSizeRule1End;
            global::System.Int128 OrderAcceptedAppendageSizeRule1Width;
            byte orderAcceptedAppendageOptionalFieldLengthValue = this.optionalFieldLength;
            int? orderAcceptedAppendageEnd = null;
            const long OrderAcceptedAppendageSizeRule1Data2 = 1;
            OrderAcceptedAppendageSizeRule1Width = checked((checked((global::System.Int128)(orderAcceptedAppendageOptionalFieldLengthValue)) + ((global::System.Int128)(OrderAcceptedAppendageSizeRule1Data2))));
            if (OrderAcceptedAppendageSizeRule1Width < RuleEmptyWidth || OrderAcceptedAppendageSizeRule1Width > (long)data.Length - start) { Fail(ParseStatus.Truncated, FieldName.OptionalFieldLength, OptionalFieldLengthOffset, data[start..]); return; }
            OrderAcceptedAppendageSizeRule1End = start + (int)OrderAcceptedAppendageSizeRule1Width;
            orderAcceptedAppendageEnd = OrderAcceptedAppendageSizeRule1End;
            if (OrderAcceptedAppendageSizeRule1End < cursor) { Fail(ParseStatus.Truncated, FieldName.OptionalFieldLength, OptionalFieldLengthOffset, data[start..]); return; }
            if (orderAcceptedAppendageEnd is int orderAcceptedAppendageDeclaredEnd)
            {
                data = data[..orderAcceptedAppendageDeclaredEnd];
            }
        }
        if (Truncated(data, start, cursor, OrderAcceptedOptionalFieldLength, FieldName.OrderAcceptedOptionalField)) return;
        this.orderAcceptedOptionalField = (OrderAcceptedOptionalField)data.Slice(cursor, OrderAcceptedOptionalFieldLength)[0];
        cursor += OrderAcceptedOptionalFieldLength;
        {
            var instance = new OrderAcceptedOptionalValue();
            instance.ParseFrom(data, ref cursor, this.OrderAcceptedOptionalValueCase());
            this.orderAcceptedOptionalValue = instance;
            ((IFieldOwner)this.orderAcceptedOptionalValue).Owner = this;
            if (this.orderAcceptedOptionalValue.Status != ParseStatus.Ok)
            {
                Fail(this.orderAcceptedOptionalValue.Status, FieldName.OrderAcceptedOptionalValue, cursor - start + this.orderAcceptedOptionalValue.FailedOffset, data[start..]);
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
        if (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.Firm)
        {
            this.orderAcceptedOptionalField = (OrderAcceptedOptionalField)FirmTag;
        }
        else if (this.orderAcceptedOptionalValue.selectedCase is null)
        {
            if ((this.OrderAcceptedOptionalValueCase() == OrderAcceptedOptionalValue.FieldName.Firm) != (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.Firm))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.OrderAcceptedOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.MinQty)
        {
            this.orderAcceptedOptionalField = (OrderAcceptedOptionalField)MinQtyTag;
        }
        else if (this.orderAcceptedOptionalValue.selectedCase is null)
        {
            if ((this.OrderAcceptedOptionalValueCase() == OrderAcceptedOptionalValue.FieldName.MinQty) != (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.MinQty))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.OrderAcceptedOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.CustomerType)
        {
            this.orderAcceptedOptionalField = (OrderAcceptedOptionalField)CustomerTypeTag;
        }
        else if (this.orderAcceptedOptionalValue.selectedCase is null)
        {
            if ((this.OrderAcceptedOptionalValueCase() == OrderAcceptedOptionalValue.FieldName.CustomerType) != (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.CustomerType))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.OrderAcceptedOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.MaxFloor)
        {
            this.orderAcceptedOptionalField = (OrderAcceptedOptionalField)MaxFloorTag;
        }
        else if (this.orderAcceptedOptionalValue.selectedCase is null)
        {
            if ((this.OrderAcceptedOptionalValueCase() == OrderAcceptedOptionalValue.FieldName.MaxFloor) != (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.MaxFloor))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.OrderAcceptedOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.PriceType)
        {
            this.orderAcceptedOptionalField = (OrderAcceptedOptionalField)PriceTypeTag;
        }
        else if (this.orderAcceptedOptionalValue.selectedCase is null)
        {
            if ((this.OrderAcceptedOptionalValueCase() == OrderAcceptedOptionalValue.FieldName.PriceType) != (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.PriceType))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.OrderAcceptedOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.PegOffset)
        {
            this.orderAcceptedOptionalField = (OrderAcceptedOptionalField)PegOffsetTag;
        }
        else if (this.orderAcceptedOptionalValue.selectedCase is null)
        {
            if ((this.OrderAcceptedOptionalValueCase() == OrderAcceptedOptionalValue.FieldName.PegOffset) != (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.PegOffset))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.OrderAcceptedOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.DiscretionPrice)
        {
            this.orderAcceptedOptionalField = (OrderAcceptedOptionalField)DiscretionPriceTag;
        }
        else if (this.orderAcceptedOptionalValue.selectedCase is null)
        {
            if ((this.OrderAcceptedOptionalValueCase() == OrderAcceptedOptionalValue.FieldName.DiscretionPrice) != (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.DiscretionPrice))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.OrderAcceptedOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.DiscretionPriceType)
        {
            this.orderAcceptedOptionalField = (OrderAcceptedOptionalField)DiscretionPriceTypeTag;
        }
        else if (this.orderAcceptedOptionalValue.selectedCase is null)
        {
            if ((this.OrderAcceptedOptionalValueCase() == OrderAcceptedOptionalValue.FieldName.DiscretionPriceType) != (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.DiscretionPriceType))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.OrderAcceptedOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.DiscretionPegOffset)
        {
            this.orderAcceptedOptionalField = (OrderAcceptedOptionalField)DiscretionPegOffsetTag;
        }
        else if (this.orderAcceptedOptionalValue.selectedCase is null)
        {
            if ((this.OrderAcceptedOptionalValueCase() == OrderAcceptedOptionalValue.FieldName.DiscretionPegOffset) != (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.DiscretionPegOffset))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.OrderAcceptedOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.PostOnly)
        {
            this.orderAcceptedOptionalField = (OrderAcceptedOptionalField)PostOnlyTag;
        }
        else if (this.orderAcceptedOptionalValue.selectedCase is null)
        {
            if ((this.OrderAcceptedOptionalValueCase() == OrderAcceptedOptionalValue.FieldName.PostOnly) != (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.PostOnly))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.OrderAcceptedOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.RandomReserves)
        {
            this.orderAcceptedOptionalField = (OrderAcceptedOptionalField)RandomReservesTag;
        }
        else if (this.orderAcceptedOptionalValue.selectedCase is null)
        {
            if ((this.OrderAcceptedOptionalValueCase() == OrderAcceptedOptionalValue.FieldName.RandomReserves) != (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.RandomReserves))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.OrderAcceptedOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.Route)
        {
            this.orderAcceptedOptionalField = (OrderAcceptedOptionalField)RouteTag;
        }
        else if (this.orderAcceptedOptionalValue.selectedCase is null)
        {
            if ((this.OrderAcceptedOptionalValueCase() == OrderAcceptedOptionalValue.FieldName.Route) != (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.Route))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.OrderAcceptedOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.ExpireTime)
        {
            this.orderAcceptedOptionalField = (OrderAcceptedOptionalField)ExpireTimeTag;
        }
        else if (this.orderAcceptedOptionalValue.selectedCase is null)
        {
            if ((this.OrderAcceptedOptionalValueCase() == OrderAcceptedOptionalValue.FieldName.ExpireTime) != (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.ExpireTime))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.OrderAcceptedOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.TradeNow)
        {
            this.orderAcceptedOptionalField = (OrderAcceptedOptionalField)TradeNowTag;
        }
        else if (this.orderAcceptedOptionalValue.selectedCase is null)
        {
            if ((this.OrderAcceptedOptionalValueCase() == OrderAcceptedOptionalValue.FieldName.TradeNow) != (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.TradeNow))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.OrderAcceptedOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.HandleInst)
        {
            this.orderAcceptedOptionalField = (OrderAcceptedOptionalField)HandleInstTag;
        }
        else if (this.orderAcceptedOptionalValue.selectedCase is null)
        {
            if ((this.OrderAcceptedOptionalValueCase() == OrderAcceptedOptionalValue.FieldName.HandleInst) != (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.HandleInst))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.OrderAcceptedOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.BboWeightIndicator)
        {
            this.orderAcceptedOptionalField = (OrderAcceptedOptionalField)BboWeightIndicatorTag;
        }
        else if (this.orderAcceptedOptionalValue.selectedCase is null)
        {
            if ((this.OrderAcceptedOptionalValueCase() == OrderAcceptedOptionalValue.FieldName.BboWeightIndicator) != (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.BboWeightIndicator))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.OrderAcceptedOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.GroupId)
        {
            this.orderAcceptedOptionalField = (OrderAcceptedOptionalField)GroupIdTag;
        }
        else if (this.orderAcceptedOptionalValue.selectedCase is null)
        {
            if ((this.OrderAcceptedOptionalValueCase() == OrderAcceptedOptionalValue.FieldName.GroupId) != (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.GroupId))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.OrderAcceptedOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.SharesLocated)
        {
            this.orderAcceptedOptionalField = (OrderAcceptedOptionalField)SharesLocatedTag;
        }
        else if (this.orderAcceptedOptionalValue.selectedCase is null)
        {
            if ((this.OrderAcceptedOptionalValueCase() == OrderAcceptedOptionalValue.FieldName.SharesLocated) != (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.SharesLocated))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.OrderAcceptedOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.UserRefIdx)
        {
            this.orderAcceptedOptionalField = (OrderAcceptedOptionalField)UserRefIdxTag;
        }
        else if (this.orderAcceptedOptionalValue.selectedCase is null)
        {
            if ((this.OrderAcceptedOptionalValueCase() == OrderAcceptedOptionalValue.FieldName.UserRefIdx) != (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.UserRefIdx))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.OrderAcceptedOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.AiqStrategy)
        {
            this.orderAcceptedOptionalField = (OrderAcceptedOptionalField)AiqStrategyTag;
        }
        else if (this.orderAcceptedOptionalValue.selectedCase is null)
        {
            if ((this.OrderAcceptedOptionalValueCase() == OrderAcceptedOptionalValue.FieldName.AiqStrategy) != (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.AiqStrategy))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.OrderAcceptedOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.AiqGroupId)
        {
            this.orderAcceptedOptionalField = (OrderAcceptedOptionalField)AiqGroupIdTag;
        }
        else if (this.orderAcceptedOptionalValue.selectedCase is null)
        {
            if ((this.OrderAcceptedOptionalValueCase() == OrderAcceptedOptionalValue.FieldName.AiqGroupId) != (this.orderAcceptedOptionalValue.selectedCase == OrderAcceptedOptionalValue.FieldName.AiqGroupId))
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.OrderAcceptedOptionalValue), EncodeStatus.RuleMismatch);
                return false;
            }
        }
        {
            const long OrderAcceptedAppendageSizeRule1Data2 = 1;
            global::System.Int128 measure = EncodedByteLength;
            global::System.Int128 orderAcceptedAppendageOptionalFieldLengthStampedValue;
            try
            {
                orderAcceptedAppendageOptionalFieldLengthStampedValue = checked((measure - checked((((global::System.Int128)(RuleNumberZero)) + ((global::System.Int128)(OrderAcceptedAppendageSizeRule1Data2))))));
            }
            catch (global::System.OverflowException)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.OptionalFieldLength), EncodeStatus.RuleMismatch);
                return false;
            }
            if (orderAcceptedAppendageOptionalFieldLengthStampedValue > byte.MaxValue)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.OptionalFieldLength), EncodeStatus.TooLong);
                return false;
            }
            if (orderAcceptedAppendageOptionalFieldLengthStampedValue < byte.MinValue)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.OptionalFieldLength), EncodeStatus.DoesNotFit);
                return false;
            }
            this.optionalFieldLength = checked((byte)(orderAcceptedAppendageOptionalFieldLengthStampedValue));
        }
        data.Slice(cursor, OptionalFieldLengthLength)[0] = unchecked((byte)(this.optionalFieldLength));
        cursor += OptionalFieldLengthLength;
        data.Slice(cursor, OrderAcceptedOptionalFieldLength)[0] = unchecked((byte)(this.orderAcceptedOptionalField));
        cursor += OrderAcceptedOptionalFieldLength;
        {
            if (!((IGroupEntry)this.orderAcceptedOptionalValue).Write(data, cursor, out cursor, out var memberFailure))
            {
                failure = memberFailure.Within(new FieldView(this, (int)FieldName.OrderAcceptedOptionalValue), null);
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
        if (!global::System.Enum.IsDefined(this.orderAcceptedOptionalField))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.OrderAcceptedOptionalField), ValidationStatus.UndeclaredValue));
        }
        foreach (var failure in this.orderAcceptedOptionalValue.Validate())
        {
            failures.Add(failure.Within(new FieldView(this, (int)FieldName.OrderAcceptedOptionalValue), null));
        }
        return failures;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(OrderAcceptedAppendage? other)
    {
        if (other is null)
        {
            return false;
        }
        if (this.orderAcceptedOptionalField != other.orderAcceptedOptionalField) return false;
        if (!this.orderAcceptedOptionalValue.Equals(other.orderAcceptedOptionalValue)) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is OrderAcceptedAppendage other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.orderAcceptedOptionalField);
        hash.Add(this.orderAcceptedOptionalValue);
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
            if (Has(FieldName.OrderAcceptedOptionalField)) names.Add(nameof(FieldName.OrderAcceptedOptionalField));
            if (Has(FieldName.OrderAcceptedOptionalValue)) names.Add(nameof(FieldName.OrderAcceptedOptionalValue));
            return names.Count == 0 ? "None" : string.Join(", ", names);
        }
    }

    /// <summary>
    ///  Compares this instance with another and identifies the fields that differ.
    /// </summary>
    /// <returns>The fields that differ, or None if there are no differences.</returns>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Changes Diff(OrderAcceptedAppendage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (this.orderAcceptedOptionalField != other.orderAcceptedOptionalField) diff = diff.With(FieldName.OrderAcceptedOptionalField);
        if (!this.orderAcceptedOptionalValue.Equals(other.orderAcceptedOptionalValue)) diff = diff.With(FieldName.OrderAcceptedOptionalValue);
        return diff;
    }

    private void CopyField(FieldName field, OrderAcceptedAppendage from)
    {
        switch (field)
        {
            case FieldName.OptionalFieldLength:
                this.optionalFieldLength = from.optionalFieldLength;
                break;
            case FieldName.OrderAcceptedOptionalField:
                this.orderAcceptedOptionalField = from.orderAcceptedOptionalField;
                break;
            case FieldName.OrderAcceptedOptionalValue:
                ((IFieldOwner)this.orderAcceptedOptionalValue).Owner = null;
                this.orderAcceptedOptionalValue = new OrderAcceptedOptionalValue(from.orderAcceptedOptionalValue);
                ((IFieldOwner)this.orderAcceptedOptionalValue).Owner = this;
                break;
            default:
                return;
        }
        Touch();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"OrderAcceptedAppendage {{ OptionalFieldLength: {(Text.Of(this.optionalFieldLength))}, OrderAcceptedOptionalField: {(Text.Enum(this.orderAcceptedOptionalField))}, OrderAcceptedOptionalValue: {(Text.Of(this.orderAcceptedOptionalValue.ToString()))} }}{(this.trailer.Length == 0 ? string.Empty : $" Trailer: {(Text.Of(this.trailer.Length))} bytes")}";

    private string DebuggerDisplay => $"[{(Text.Of(this.optionalFieldLength))}] [{(Text.Enum(this.orderAcceptedOptionalField))}]";

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
        FieldName.OrderAcceptedOptionalField => Text.Enum(this.orderAcceptedOptionalField),
        FieldName.OrderAcceptedOptionalValue => Text.Of(this.orderAcceptedOptionalValue.ToString()),
        _ => string.Empty,
    };

    bool IFieldOwner.IsRecognized(int field) => (FieldName)field switch
    {
        FieldName.OrderAcceptedOptionalField => global::System.Enum.IsDefined(this.orderAcceptedOptionalField),
        FieldName.OrderAcceptedOptionalValue => this.orderAcceptedOptionalValue.IsRecognized,
        _ => true,
    };

    bool IFieldOwner.IsPresent(int field) => true;

    bool IFieldOwner.IsParsed(int field) => this.status == ParseStatus.Ok || field < (int?)this.failed;

    bool IFieldOwner.IsReached(int field) => this.modified || this.status is ParseStatus.None or ParseStatus.Ok || field <= (int?)this.failed;

    bool IFieldOwner.HasParseLayout(int field) => false;

    int IFieldOwner.OffsetOf(int field) => (FieldName)field switch
    {
        FieldName.OptionalFieldLength => OptionalFieldLengthOffset,
        FieldName.OrderAcceptedOptionalField => OrderAcceptedOptionalFieldOffset,
        FieldName.OrderAcceptedOptionalValue => OrderAcceptedOptionalValueOffset,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    int IFieldOwner.LengthOf(int field) => (FieldName)field switch
    {
        FieldName.OptionalFieldLength => OptionalFieldLengthLength,
        FieldName.OrderAcceptedOptionalField => OrderAcceptedOptionalFieldLength,
        FieldName.OrderAcceptedOptionalValue => this.orderAcceptedOptionalValue.EncodedByteLength,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    IReadOnlyList<IGroupEntry>? IFieldOwner.EntriesOf(int field) => null;

    IMessage? IFieldOwner.MessageOf(int field) => null;

    ulong[] IFieldOwner.ChangedAgainst(IFieldOwner other) => other is OrderAcceptedAppendage typed ? Diff(typed).ToWords() : throw new ArgumentException("The other instance is not of the same type.", nameof(other));

    void IFieldOwner.CopyField(int field, IFieldOwner from) => CopyField((FieldName)field, from as OrderAcceptedAppendage ?? throw new ArgumentException("The other instance is not of the same type.", nameof(from)));
}
