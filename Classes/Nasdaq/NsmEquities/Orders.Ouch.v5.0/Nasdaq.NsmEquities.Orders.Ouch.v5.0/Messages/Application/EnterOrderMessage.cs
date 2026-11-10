using System.Buffers.Binary;
using System.Diagnostics;
using static Nasdaq.NsmEquities.Orders.Ouch.v5_0.Rules;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Enter Order Message
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class EnterOrderMessage : IUnsequencedMessage, IEquatable<EnterOrderMessage>, IFieldOwner
{
    internal const int UserRefNumOffset = 0;

    internal const int UserRefNumLength = 4;

    internal const int SideOffset = UserRefNumOffset + UserRefNumLength;

    internal const int SideLength = 1;

    internal const int QuantityOffset = SideOffset + SideLength;

    internal const int QuantityLength = 4;

    internal const int SymbolOffset = QuantityOffset + QuantityLength;

    internal const int SymbolLength = 8;

    /// <summary>
    ///  The byte that fills the unused bytes of Symbol.
    /// </summary>
    public const byte SymbolFill = (byte)' ';

    internal const int PriceOffset = SymbolOffset + SymbolLength;

    internal const int PriceLength = 8;

    /// <summary>
    ///  The number of decimal places in Price.
    /// </summary>
    public const byte PriceScale = 4;

    /// <summary>
    ///  The factor by which <see cref="Price"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal PriceFactor = 10000m;

    internal const int TimeInForceOffset = PriceOffset + PriceLength;

    internal const int TimeInForceLength = 1;

    internal const int DisplayOffset = TimeInForceOffset + TimeInForceLength;

    internal const int DisplayLength = 1;

    internal const int CapacityOffset = DisplayOffset + DisplayLength;

    internal const int CapacityLength = 1;

    internal const int InterMarketSweepEligibilityOffset = CapacityOffset + CapacityLength;

    internal const int InterMarketSweepEligibilityLength = 1;

    internal const int CrossTypeOffset = InterMarketSweepEligibilityOffset + InterMarketSweepEligibilityLength;

    internal const int CrossTypeLength = 1;

    internal const int ClordidOffset = CrossTypeOffset + CrossTypeLength;

    internal const int ClordidLength = 14;

    /// <summary>
    ///  The byte that fills the unused bytes of ClOrdId.
    /// </summary>
    public const byte ClordidFill = (byte)' ';

    internal const int AppendageLengthOffset = ClordidOffset + ClordidLength;

    internal const int AppendageLengthLength = 2;

    internal const int AppendageOffset = AppendageLengthOffset + AppendageLengthLength;

    /// <summary>
    ///  The encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public const int MessageByteLength = UserRefNumLength + SideLength + QuantityLength + SymbolLength + PriceLength + TimeInForceLength + DisplayLength + CapacityLength + InterMarketSweepEligibilityLength + CrossTypeLength + ClordidLength + AppendageLengthLength;

    private const int EnterOrderAppendageListEntryByteLength = EnterOrderAppendage.MessageByteLength;

    internal uint userRefNum;

    /// <summary>
    ///  UserRefNum must be day-unique and strictly increasing for each OUCH account.
    /// </summary>
    public uint UserRefNum
    {
        get => this.userRefNum;
        set
        {
            this.userRefNum = value;
            Touch();
        }
    }

    internal Side side;

    /// <summary>
    ///  Side
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

    internal uint quantity;

    /// <summary>
    ///  Total number of shares. Must be greater than zero and less than 1,000,000.
    /// </summary>
    public uint Quantity
    {
        get => this.quantity;
        set
        {
            this.quantity = value;
            Touch();
        }
    }

    internal string symbol = string.Empty;

    /// <summary>
    ///  Stock Symbol
    /// </summary>
    /// <remarks>
    ///  The field is 8 bytes long. The field holds ASCII text. Text that is shorter than the field is padded with spaces at the end. Text longer than the field is cut to fit when the message is encoded. A character that the field cannot hold is replaced with a question mark when the message is encoded.
    /// </remarks>
    public string Symbol
    {
        get => this.symbol;
        set
        {
            this.symbol = value ?? string.Empty;
            Touch();
        }
    }

    internal ulong price;

    /// <summary>
    ///  The price of the order.
    /// </summary>
    /// <remarks>
    ///  A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range.
    /// </remarks>
    public decimal Price
    {
        get => FixedPoint.ToDecimal(this.price, PriceScale);
        set
        {
            this.price = FixedPoint.ToRaw<ulong>(value, PriceFactor, ulong.MinValue, ulong.MaxValue);
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="Price"/>, as the unscaled integer that the field holds.
    /// </summary>
    public ulong PriceRaw
    {
        get => this.price;
        set
        {
            this.price = value;
            Touch();
        }
    }

    internal TimeInForce timeInForce;

    /// <summary>
    ///  Corresponds to TimeInForce (59) in Nasdaq FIX
    /// </summary>
    public TimeInForce TimeInForce
    {
        get => this.timeInForce;
        set
        {
            this.timeInForce = value;
            Touch();
        }
    }

    internal Display display;

    /// <summary>
    ///  Display
    /// </summary>
    public Display Display
    {
        get => this.display;
        set
        {
            this.display = value;
            Touch();
        }
    }

    internal Capacity capacity;

    /// <summary>
    ///  Capacity
    /// </summary>
    public Capacity Capacity
    {
        get => this.capacity;
        set
        {
            this.capacity = value;
            Touch();
        }
    }

    internal InterMarketSweepEligibility interMarketSweepEligibility;

    /// <summary>
    ///  InterMarket Sweep Eligibility
    /// </summary>
    public InterMarketSweepEligibility InterMarketSweepEligibility
    {
        get => this.interMarketSweepEligibility;
        set
        {
            this.interMarketSweepEligibility = value;
            Touch();
        }
    }

    internal CrossType crossType;

    /// <summary>
    ///  CrossType
    /// </summary>
    public CrossType CrossType
    {
        get => this.crossType;
        set
        {
            this.crossType = value;
            Touch();
        }
    }

    internal string clordid = string.Empty;

    /// <summary>
    ///  Customer order identifier. ClOrdID will not be checked for day-uniqueness for each OUCH account.
    /// </summary>
    /// <remarks>
    ///  The field is 14 bytes long. The field holds ASCII text. Text that is shorter than the field is padded with spaces at the end. Text longer than the field is cut to fit when the message is encoded. A character that the field cannot hold is replaced with a question mark when the message is encoded.
    /// </remarks>
    public string Clordid
    {
        get => this.clordid;
        set
        {
            this.clordid = value ?? string.Empty;
            Touch();
        }
    }

    internal ushort appendageLength;

    /// <summary>
    ///  The length of the remaining Optional Appendage field.
    /// </summary>
    public ushort AppendageLength
    {
        get => this.appendageLength;
        set
        {
            this.appendageLength = value;
            Touch();
        }
    }

    internal EnterOrderAppendageList appendage = new([]);

    /// <summary>
    ///  Enter Order Appendage
    /// </summary>
    /// <remarks>
    ///  Setting <see langword="null"/> stores the value that Clear stores.
    /// </remarks>
    public EnterOrderAppendageList Appendage
    {
        get => this.appendage;
        set
        {
            value ??= new([]);
            this.appendage.SetOwner(null);
            this.appendage = new EnterOrderAppendageList(value);
            this.appendage.SetOwner(this);
            Touch();
        }
    }

    /// <summary>
    ///  Gets the message type.
    /// </summary>
    public UnsequencedMessageCode DataValue => UnsequencedMessageCode.EnterOrderMessage;

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
            position += UserRefNumLength;
            position += SideLength;
            position += QuantityLength;
            position += SymbolLength;
            position += PriceLength;
            position += TimeInForceLength;
            position += DisplayLength;
            position += CapacityLength;
            position += InterMarketSweepEligibilityLength;
            position += CrossTypeLength;
            position += ClordidLength;
            position += AppendageLengthLength;
            foreach (var entry in this.appendage)
            {
                position += entry.EncodedByteLength;
            }
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
    public bool IsRecognized => global::System.Enum.IsDefined(this.side) && global::System.Enum.IsDefined(this.timeInForce) && global::System.Enum.IsDefined(this.display) && global::System.Enum.IsDefined(this.capacity) && global::System.Enum.IsDefined(this.interMarketSweepEligibility) && global::System.Enum.IsDefined(this.crossType) && this.appendage.IsRecognized;

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
    private int appendageByteLength;

    private void Touch()
    {
        this.modified = true;
        this.cache = null;
        this.owner?.Touch();
    }

    IMessage IMessage.Copy() => new EnterOrderMessage(this);

    void IMessage.SetOwner(IFieldOwner? owner) => this.owner = owner;

    int IMessage.EncodedByteLength => EncodedByteLength;

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
        ///  UserRefNum must be day-unique and strictly increasing for each OUCH account.
        /// </summary>
        UserRefNum,
        /// <summary>
        ///  Side
        /// </summary>
        Side,
        /// <summary>
        ///  Total number of shares. Must be greater than zero and less than 1,000,000.
        /// </summary>
        Quantity,
        /// <summary>
        ///  Stock Symbol
        /// </summary>
        Symbol,
        /// <summary>
        ///  The price of the order.
        /// </summary>
        Price,
        /// <summary>
        ///  Corresponds to TimeInForce (59) in Nasdaq FIX
        /// </summary>
        TimeInForce,
        /// <summary>
        ///  Display
        /// </summary>
        Display,
        /// <summary>
        ///  Capacity
        /// </summary>
        Capacity,
        /// <summary>
        ///  InterMarket Sweep Eligibility
        /// </summary>
        InterMarketSweepEligibility,
        /// <summary>
        ///  CrossType
        /// </summary>
        CrossType,
        /// <summary>
        ///  Customer order identifier. ClOrdID will not be checked for day-uniqueness for each OUCH account.
        /// </summary>
        Clordid,
        /// <summary>
        ///  The length of the remaining Optional Appendage field.
        /// </summary>
        AppendageLength,
        /// <summary>
        ///  Enter Order Appendage
        /// </summary>
        Appendage,
    }

    /// <summary>
    ///  Provides access to the fields of one instance, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public readonly struct FieldSet : IEnumerable<FieldView>
    {
        private readonly EnterOrderMessage owner;

        internal FieldSet(EnterOrderMessage owner)
        {
            this.owner = owner;
        }

        /// <summary>
        ///  The message type.
        /// </summary>
        public FieldView DataValue => this[FieldName.DataValue];

        /// <summary>
        ///  UserRefNum must be day-unique and strictly increasing for each OUCH account.
        /// </summary>
        public FieldView UserRefNum => this[FieldName.UserRefNum];

        /// <summary>
        ///  Side
        /// </summary>
        public FieldView Side => this[FieldName.Side];

        /// <summary>
        ///  Total number of shares. Must be greater than zero and less than 1,000,000.
        /// </summary>
        public FieldView Quantity => this[FieldName.Quantity];

        /// <summary>
        ///  Stock Symbol
        /// </summary>
        public FieldView Symbol => this[FieldName.Symbol];

        /// <summary>
        ///  The price of the order.
        /// </summary>
        public FieldView Price => this[FieldName.Price];

        /// <summary>
        ///  Corresponds to TimeInForce (59) in Nasdaq FIX
        /// </summary>
        public FieldView TimeInForce => this[FieldName.TimeInForce];

        /// <summary>
        ///  Display
        /// </summary>
        public FieldView Display => this[FieldName.Display];

        /// <summary>
        ///  Capacity
        /// </summary>
        public FieldView Capacity => this[FieldName.Capacity];

        /// <summary>
        ///  InterMarket Sweep Eligibility
        /// </summary>
        public FieldView InterMarketSweepEligibility => this[FieldName.InterMarketSweepEligibility];

        /// <summary>
        ///  CrossType
        /// </summary>
        public FieldView CrossType => this[FieldName.CrossType];

        /// <summary>
        ///  Customer order identifier. ClOrdID will not be checked for day-uniqueness for each OUCH account.
        /// </summary>
        public FieldView Clordid => this[FieldName.Clordid];

        /// <summary>
        ///  The length of the remaining Optional Appendage field.
        /// </summary>
        public FieldView AppendageLength => this[FieldName.AppendageLength];

        /// <summary>
        ///  Enter Order Appendage
        /// </summary>
        public FieldView Appendage => this[FieldName.Appendage];

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
        new(nameof(FieldName.DataValue), FieldKind.Enum, DataValueLength),
        new(nameof(FieldName.UserRefNum), FieldKind.UnsignedInteger, UserRefNumLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.Side), FieldKind.Enum, SideLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.Quantity), FieldKind.UnsignedInteger, QuantityLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.Symbol), FieldKind.String, SymbolLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.Price), FieldKind.Decimal, PriceLength) { Scale = PriceScale, ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.TimeInForce), FieldKind.Enum, TimeInForceLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.Display), FieldKind.Enum, DisplayLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.Capacity), FieldKind.Enum, CapacityLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.InterMarketSweepEligibility), FieldKind.Enum, InterMarketSweepEligibilityLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.CrossType), FieldKind.Enum, CrossTypeLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.Clordid), FieldKind.String, ClordidLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.AppendageLength), FieldKind.UnsignedInteger, AppendageLengthLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.Appendage), FieldKind.Group, null),
    ];

    /// <summary>
    ///  Gets the fields in wire order, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public FieldSet Fields => new(this);

    IEnumerable<FieldView> IMessage.Fields => new FieldSet(this);

    /// <summary>
    ///  Initializes a new instance of the <see cref="EnterOrderMessage"/> class with default values.
    /// </summary>
    public EnterOrderMessage()
    {
        Clear();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="EnterOrderMessage"/> class with the values, bytes, and parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public EnterOrderMessage(EnterOrderMessage other) : this()
    {
        CopyFrom(other);
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(EnterOrderMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.userRefNum = other.userRefNum;
        this.side = other.side;
        this.quantity = other.quantity;
        this.symbol = other.symbol;
        this.price = other.price;
        this.timeInForce = other.timeInForce;
        this.display = other.display;
        this.capacity = other.capacity;
        this.interMarketSweepEligibility = other.interMarketSweepEligibility;
        this.crossType = other.crossType;
        this.clordid = other.clordid;
        this.appendageLength = other.appendageLength;
        this.appendage.SetOwner(null);
        this.appendage = new EnterOrderAppendageList(other.appendage);
        this.appendage.SetOwner(this);
        this.appendageByteLength = other.appendageByteLength;
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
        this.userRefNum = default;
        this.side = default;
        this.quantity = default;
        this.symbol = string.Empty;
        this.price = default;
        this.timeInForce = default;
        this.display = default;
        this.capacity = default;
        this.interMarketSweepEligibility = default;
        this.crossType = default;
        this.clordid = string.Empty;
        this.appendageLength = default;
        this.appendage.SetOwner(null);
        this.appendage = new([]);
        this.appendage.SetOwner(this);
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
    public static EnterOrderMessage Parse(ReadOnlySpan<byte> data)
    {
        var instance = new EnterOrderMessage();
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
    public static EnterOrderMessage Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        var instance = new EnterOrderMessage();
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
        if (Truncated(data, start, cursor, UserRefNumLength, FieldName.UserRefNum)) return;
        this.userRefNum = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, UserRefNumLength));
        cursor += UserRefNumLength;
        if (Truncated(data, start, cursor, SideLength, FieldName.Side)) return;
        this.side = (Side)data.Slice(cursor, SideLength)[0];
        cursor += SideLength;
        if (Truncated(data, start, cursor, QuantityLength, FieldName.Quantity)) return;
        this.quantity = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, QuantityLength));
        cursor += QuantityLength;
        if (Truncated(data, start, cursor, SymbolLength, FieldName.Symbol)) return;
        this.symbol = TextEncoding.Ascii.ReadLeftJustified(data.Slice(cursor, SymbolLength), SymbolFill);
        cursor += SymbolLength;
        if (Truncated(data, start, cursor, PriceLength, FieldName.Price)) return;
        this.price = BinaryPrimitives.ReadUInt64BigEndian(data.Slice(cursor, PriceLength));
        cursor += PriceLength;
        if (Truncated(data, start, cursor, TimeInForceLength, FieldName.TimeInForce)) return;
        this.timeInForce = (TimeInForce)data.Slice(cursor, TimeInForceLength)[0];
        cursor += TimeInForceLength;
        if (Truncated(data, start, cursor, DisplayLength, FieldName.Display)) return;
        this.display = (Display)data.Slice(cursor, DisplayLength)[0];
        cursor += DisplayLength;
        if (Truncated(data, start, cursor, CapacityLength, FieldName.Capacity)) return;
        this.capacity = (Capacity)data.Slice(cursor, CapacityLength)[0];
        cursor += CapacityLength;
        if (Truncated(data, start, cursor, InterMarketSweepEligibilityLength, FieldName.InterMarketSweepEligibility)) return;
        this.interMarketSweepEligibility = (InterMarketSweepEligibility)data.Slice(cursor, InterMarketSweepEligibilityLength)[0];
        cursor += InterMarketSweepEligibilityLength;
        if (Truncated(data, start, cursor, CrossTypeLength, FieldName.CrossType)) return;
        this.crossType = (CrossType)data.Slice(cursor, CrossTypeLength)[0];
        cursor += CrossTypeLength;
        if (Truncated(data, start, cursor, ClordidLength, FieldName.Clordid)) return;
        this.clordid = TextEncoding.Ascii.ReadLeftJustified(data.Slice(cursor, ClordidLength), ClordidFill);
        cursor += ClordidLength;
        if (Truncated(data, start, cursor, AppendageLengthLength, FieldName.AppendageLength)) return;
        this.appendageLength = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(cursor, AppendageLengthLength));
        cursor += AppendageLengthLength;
        {
            var groupStart = cursor;
            this.appendageByteLength = data.Length - groupStart;
            int EnterOrderAppendageCountRule0End;
            global::System.Int128 EnterOrderAppendageCountRule0RawEnd;
            ushort enterOrderMessageAppendageLengthValue = this.appendageLength;
            var entries = new List<EnterOrderAppendage>();
            EnterOrderAppendageCountRule0RawEnd = checked((checked((global::System.Int128)(enterOrderMessageAppendageLengthValue)) + checked((global::System.Int128)(cursor))));
            if (EnterOrderAppendageCountRule0RawEnd < cursor || EnterOrderAppendageCountRule0RawEnd > data.Length) { Fail(ParseStatus.Truncated, FieldName.Appendage, groupStart - start, data[start..]); return; }
            EnterOrderAppendageCountRule0End = (int)EnterOrderAppendageCountRule0RawEnd;
            while (cursor < EnterOrderAppendageCountRule0End)
            {
                int enterOrderAppendageEntryStart = cursor;
                var entry = new EnterOrderAppendage();
                var contract = (IGroupEntry)entry;
                contract.ParseFrom(data[..EnterOrderAppendageCountRule0End], ref cursor);
                if (contract.Status != ParseStatus.Ok)
                {
                    this.appendage = new EnterOrderAppendageList(entries, contract.Status, entry);
                    this.appendage.SetOwner(this);
                    Fail(contract.Status, FieldName.Appendage, groupStart - start, data[start..]);
                    return;
                }
                entries.Add(entry);
                if (cursor <= enterOrderAppendageEntryStart || cursor > EnterOrderAppendageCountRule0End) { Fail(ParseStatus.Truncated, FieldName.Appendage, groupStart - start, data[start..]); return; }
            }
            if (cursor != EnterOrderAppendageCountRule0End) { Fail(ParseStatus.BadCount, FieldName.Appendage, groupStart - start, data[start..]); return; }
            this.appendage = new EnterOrderAppendageList(entries, ParseStatus.Ok, null);
            this.appendage.SetOwner(this);
            this.appendageByteLength = cursor - groupStart;
        }
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
        if (data.Length - offset < EncodedByteLength)
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
        var buffer = new byte[EncodedByteLength];
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
        if (data.Length < EncodedByteLength)
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
        var position = offset;
        position += UserRefNumLength;
        position += SideLength;
        position += QuantityLength;
        position += SymbolLength;
        position += PriceLength;
        position += TimeInForceLength;
        position += DisplayLength;
        position += CapacityLength;
        position += InterMarketSweepEligibilityLength;
        position += CrossTypeLength;
        position += ClordidLength;
        position += AppendageLengthLength;
        var enterOrderAppendageMeasureStart = position;
        foreach (var entry in this.appendage)
        {
            position += entry.EncodedByteLength;
        }
        var enterOrderAppendageMeasureEnd = position;
        {
            global::System.Int128 measure = enterOrderAppendageMeasureEnd;
            global::System.Int128 enterOrderMessageAppendageLengthStampedValue;
            try
            {
                enterOrderMessageAppendageLengthStampedValue = checked((measure - checked((((global::System.Int128)(RuleNumberZero)) + checked((global::System.Int128)(enterOrderAppendageMeasureStart))))));
            }
            catch (global::System.OverflowException)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.AppendageLength), EncodeStatus.RuleMismatch);
                return false;
            }
            if (enterOrderMessageAppendageLengthStampedValue > ushort.MaxValue)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.AppendageLength), EncodeStatus.TooLong);
                return false;
            }
            if (enterOrderMessageAppendageLengthStampedValue < ushort.MinValue)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.AppendageLength), EncodeStatus.DoesNotFit);
                return false;
            }
            this.appendageLength = checked((ushort)(enterOrderMessageAppendageLengthStampedValue));
        }
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, UserRefNumLength), this.userRefNum);
        cursor += UserRefNumLength;
        data.Slice(cursor, SideLength)[0] = unchecked((byte)(this.side));
        cursor += SideLength;
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, QuantityLength), this.quantity);
        cursor += QuantityLength;
        TextEncoding.Ascii.WriteLeftJustified(data.Slice(cursor, SymbolLength), TextEncoding.Ascii.Fit(this.symbol, SymbolLength), SymbolFill);
        cursor += SymbolLength;
        BinaryPrimitives.WriteUInt64BigEndian(data.Slice(cursor, PriceLength), this.price);
        cursor += PriceLength;
        data.Slice(cursor, TimeInForceLength)[0] = unchecked((byte)(this.timeInForce));
        cursor += TimeInForceLength;
        data.Slice(cursor, DisplayLength)[0] = unchecked((byte)(this.display));
        cursor += DisplayLength;
        data.Slice(cursor, CapacityLength)[0] = unchecked((byte)(this.capacity));
        cursor += CapacityLength;
        data.Slice(cursor, InterMarketSweepEligibilityLength)[0] = unchecked((byte)(this.interMarketSweepEligibility));
        cursor += InterMarketSweepEligibilityLength;
        data.Slice(cursor, CrossTypeLength)[0] = unchecked((byte)(this.crossType));
        cursor += CrossTypeLength;
        TextEncoding.Ascii.WriteLeftJustified(data.Slice(cursor, ClordidLength), TextEncoding.Ascii.Fit(this.clordid, ClordidLength), ClordidFill);
        cursor += ClordidLength;
        BinaryPrimitives.WriteUInt16BigEndian(data.Slice(cursor, AppendageLengthLength), this.appendageLength);
        cursor += AppendageLengthLength;
        {
            var groupStart = cursor;
            for (var index = 0; index < this.appendage.Count; index++)
            {
                if (!((IGroupEntry)this.appendage[index]).Write(data, cursor, out cursor, out var memberFailure))
                {
                    failure = memberFailure.Within(new FieldView(this, (int)FieldName.Appendage), index);
                    return false;
                }
            }
            this.appendageByteLength = cursor - groupStart;
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
                var buffer = new byte[EncodedByteLength];
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
    ///  Validate checks the current values. After a parse, check Status first, because a field that the parse did not reach holds its cleared value.
    /// </remarks>
    public List<ValidationFailure> Validate()
    {
        var failures = new List<ValidationFailure>();
        if (!global::System.Enum.IsDefined(this.side))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.Side), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Text.Ascii.IsValid(this.symbol))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.Symbol), ValidationStatus.NonAsciiText));
        }
        if (!global::System.Enum.IsDefined(this.timeInForce))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.TimeInForce), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Enum.IsDefined(this.display))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.Display), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Enum.IsDefined(this.capacity))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.Capacity), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Enum.IsDefined(this.interMarketSweepEligibility))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.InterMarketSweepEligibility), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Enum.IsDefined(this.crossType))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.CrossType), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Text.Ascii.IsValid(this.clordid))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.Clordid), ValidationStatus.NonAsciiText));
        }
        for (var index = 0; index < this.appendage.Count; index++)
        {
            foreach (var failure in ((IGroupEntry)this.appendage[index]).Validate())
            {
                failures.Add(failure.Within(new FieldView(this, (int)FieldName.Appendage), index));
            }
        }
        return failures;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(EnterOrderMessage? other)
    {
        if (other is null)
        {
            return false;
        }
        if (this.userRefNum != other.userRefNum) return false;
        if (this.side != other.side) return false;
        if (this.quantity != other.quantity) return false;
        if (this.symbol != other.symbol) return false;
        if (this.price != other.price) return false;
        if (this.timeInForce != other.timeInForce) return false;
        if (this.display != other.display) return false;
        if (this.capacity != other.capacity) return false;
        if (this.interMarketSweepEligibility != other.interMarketSweepEligibility) return false;
        if (this.crossType != other.crossType) return false;
        if (this.clordid != other.clordid) return false;
        if (!this.appendage.Equals(other.appendage)) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is EnterOrderMessage other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.userRefNum);
        hash.Add(this.side);
        hash.Add(this.quantity);
        hash.Add(this.symbol);
        hash.Add(this.price);
        hash.Add(this.timeInForce);
        hash.Add(this.display);
        hash.Add(this.capacity);
        hash.Add(this.interMarketSweepEligibility);
        hash.Add(this.crossType);
        hash.Add(this.clordid);
        hash.Add(this.appendage);
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
        private const int FieldCount = 14;

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
            if (Has(FieldName.UserRefNum)) names.Add(nameof(FieldName.UserRefNum));
            if (Has(FieldName.Side)) names.Add(nameof(FieldName.Side));
            if (Has(FieldName.Quantity)) names.Add(nameof(FieldName.Quantity));
            if (Has(FieldName.Symbol)) names.Add(nameof(FieldName.Symbol));
            if (Has(FieldName.Price)) names.Add(nameof(FieldName.Price));
            if (Has(FieldName.TimeInForce)) names.Add(nameof(FieldName.TimeInForce));
            if (Has(FieldName.Display)) names.Add(nameof(FieldName.Display));
            if (Has(FieldName.Capacity)) names.Add(nameof(FieldName.Capacity));
            if (Has(FieldName.InterMarketSweepEligibility)) names.Add(nameof(FieldName.InterMarketSweepEligibility));
            if (Has(FieldName.CrossType)) names.Add(nameof(FieldName.CrossType));
            if (Has(FieldName.Clordid)) names.Add(nameof(FieldName.Clordid));
            if (Has(FieldName.AppendageLength)) names.Add(nameof(FieldName.AppendageLength));
            if (Has(FieldName.Appendage)) names.Add(nameof(FieldName.Appendage));
            return names.Count == 0 ? "None" : string.Join(", ", names);
        }
    }

    /// <summary>
    ///  Compares this instance with another and identifies the fields that differ.
    /// </summary>
    /// <returns>The fields that differ, or None if there are no differences.</returns>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Changes Diff(EnterOrderMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (this.userRefNum != other.userRefNum) diff = diff.With(FieldName.UserRefNum);
        if (this.side != other.side) diff = diff.With(FieldName.Side);
        if (this.quantity != other.quantity) diff = diff.With(FieldName.Quantity);
        if (this.symbol != other.symbol) diff = diff.With(FieldName.Symbol);
        if (this.price != other.price) diff = diff.With(FieldName.Price);
        if (this.timeInForce != other.timeInForce) diff = diff.With(FieldName.TimeInForce);
        if (this.display != other.display) diff = diff.With(FieldName.Display);
        if (this.capacity != other.capacity) diff = diff.With(FieldName.Capacity);
        if (this.interMarketSweepEligibility != other.interMarketSweepEligibility) diff = diff.With(FieldName.InterMarketSweepEligibility);
        if (this.crossType != other.crossType) diff = diff.With(FieldName.CrossType);
        if (this.clordid != other.clordid) diff = diff.With(FieldName.Clordid);
        if (!this.appendage.Equals(other.appendage)) diff = diff.With(FieldName.Appendage);
        return diff;
    }

    private void CopyField(FieldName field, EnterOrderMessage from)
    {
        switch (field)
        {
            case FieldName.UserRefNum:
                this.userRefNum = from.userRefNum;
                break;
            case FieldName.Side:
                this.side = from.side;
                break;
            case FieldName.Quantity:
                this.quantity = from.quantity;
                break;
            case FieldName.Symbol:
                this.symbol = from.symbol;
                break;
            case FieldName.Price:
                this.price = from.price;
                break;
            case FieldName.TimeInForce:
                this.timeInForce = from.timeInForce;
                break;
            case FieldName.Display:
                this.display = from.display;
                break;
            case FieldName.Capacity:
                this.capacity = from.capacity;
                break;
            case FieldName.InterMarketSweepEligibility:
                this.interMarketSweepEligibility = from.interMarketSweepEligibility;
                break;
            case FieldName.CrossType:
                this.crossType = from.crossType;
                break;
            case FieldName.Clordid:
                this.clordid = from.clordid;
                break;
            case FieldName.AppendageLength:
                this.appendageLength = from.appendageLength;
                break;
            case FieldName.Appendage:
                this.appendage.SetOwner(null);
                this.appendage = new EnterOrderAppendageList(from.appendage);
                this.appendage.SetOwner(this);
                break;
            default:
                return;
        }
        Touch();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"EnterOrderMessage {{ UserRefNum: {(Text.Of(this.userRefNum))}, Side: {(Text.Enum(this.side))}, Quantity: {(Text.Of(this.quantity))}, Symbol: {(Text.Of(this.symbol))}, Price: {(Text.Of(FixedPoint.ToDecimal(this.price, PriceScale)))}, TimeInForce: {(Text.Enum(this.timeInForce))}, Display: {(Text.Enum(this.display))}, Capacity: {(Text.Enum(this.capacity))}, InterMarketSweepEligibility: {(Text.Enum(this.interMarketSweepEligibility))}, CrossType: {(Text.Enum(this.crossType))}, Clordid: {(Text.Of(this.clordid))}, AppendageLength: {(Text.Of(this.appendageLength))}, Appendage: {(Text.Entries(this.appendage))} }}{(this.trailer.Length == 0 ? string.Empty : $" Trailer: {(Text.Of(this.trailer.Length))} bytes")}";

    private string DebuggerDisplay => $"[{DataValue}] [{(Text.Of(this.userRefNum))}] [{(Text.Enum(this.side))}] [{(Text.Of(this.quantity))}]";

    IFieldOwner? IFieldOwner.Owner
    {
        get => this.owner;
        set => this.owner = value;
    }

    FieldDescriptor[] IFieldOwner.Descriptors => Descriptors;

    void IFieldOwner.Touch() => Touch();

    string IFieldOwner.TextOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => Text.Enum(UnsequencedMessageCode.EnterOrderMessage),
        FieldName.UserRefNum => Text.Of(this.userRefNum),
        FieldName.Side => Text.Enum(this.side),
        FieldName.Quantity => Text.Of(this.quantity),
        FieldName.Symbol => Text.Of(this.symbol),
        FieldName.Price => Text.Of(FixedPoint.ToDecimal(this.price, PriceScale)),
        FieldName.TimeInForce => Text.Enum(this.timeInForce),
        FieldName.Display => Text.Enum(this.display),
        FieldName.Capacity => Text.Enum(this.capacity),
        FieldName.InterMarketSweepEligibility => Text.Enum(this.interMarketSweepEligibility),
        FieldName.CrossType => Text.Enum(this.crossType),
        FieldName.Clordid => Text.Of(this.clordid),
        FieldName.AppendageLength => Text.Of(this.appendageLength),
        FieldName.Appendage => Text.Entries(this.appendage),
        _ => string.Empty,
    };

    bool IFieldOwner.IsRecognized(int field) => (FieldName)field switch
    {
        FieldName.Side => global::System.Enum.IsDefined(this.side),
        FieldName.TimeInForce => global::System.Enum.IsDefined(this.timeInForce),
        FieldName.Display => global::System.Enum.IsDefined(this.display),
        FieldName.Capacity => global::System.Enum.IsDefined(this.capacity),
        FieldName.InterMarketSweepEligibility => global::System.Enum.IsDefined(this.interMarketSweepEligibility),
        FieldName.CrossType => global::System.Enum.IsDefined(this.crossType),
        FieldName.Appendage => this.appendage.IsRecognized,
        _ => true,
    };

    bool IFieldOwner.IsPresent(int field) => (FieldName)field switch
    {
        FieldName.Appendage => this.appendage.Count != 0,
        _ => true,
    };

    bool IFieldOwner.IsParsed(int field) => this.status == ParseStatus.Ok || field < (int?)this.failed;

    bool IFieldOwner.IsReached(int field) => this.modified || this.status is ParseStatus.None or ParseStatus.Ok || field <= (int?)this.failed;

    bool IFieldOwner.HasParseLayout(int field) => (FieldName)field switch
    {
        FieldName.Appendage => true,
        _ => false,
    };

    int IFieldOwner.OffsetOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => FieldView.NotInMessage,
        FieldName.UserRefNum => UserRefNumOffset,
        FieldName.Side => SideOffset,
        FieldName.Quantity => QuantityOffset,
        FieldName.Symbol => SymbolOffset,
        FieldName.Price => PriceOffset,
        FieldName.TimeInForce => TimeInForceOffset,
        FieldName.Display => DisplayOffset,
        FieldName.Capacity => CapacityOffset,
        FieldName.InterMarketSweepEligibility => InterMarketSweepEligibilityOffset,
        FieldName.CrossType => CrossTypeOffset,
        FieldName.Clordid => ClordidOffset,
        FieldName.AppendageLength => AppendageLengthOffset,
        FieldName.Appendage => AppendageOffset,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    int IFieldOwner.LengthOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => DataValueLength,
        FieldName.UserRefNum => UserRefNumLength,
        FieldName.Side => SideLength,
        FieldName.Quantity => QuantityLength,
        FieldName.Symbol => SymbolLength,
        FieldName.Price => PriceLength,
        FieldName.TimeInForce => TimeInForceLength,
        FieldName.Display => DisplayLength,
        FieldName.Capacity => CapacityLength,
        FieldName.InterMarketSweepEligibility => InterMarketSweepEligibilityLength,
        FieldName.CrossType => CrossTypeLength,
        FieldName.Clordid => ClordidLength,
        FieldName.AppendageLength => AppendageLengthLength,
        FieldName.Appendage => this.appendageByteLength,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    IReadOnlyList<IGroupEntry>? IFieldOwner.EntriesOf(int field) => (FieldName)field switch
    {
        FieldName.Appendage => this.appendage,
        _ => null,
    };

    IMessage? IFieldOwner.MessageOf(int field) => null;

    ulong[] IFieldOwner.ChangedAgainst(IFieldOwner other) => other is EnterOrderMessage typed ? Diff(typed).ToWords() : throw new ArgumentException("The other instance is not of the same type.", nameof(other));

    void IFieldOwner.CopyField(int field, IFieldOwner from) => CopyField((FieldName)field, from as EnterOrderMessage ?? throw new ArgumentException("The other instance is not of the same type.", nameof(from)));

    ReadOnlyMemory<byte> IFieldOwner.CodeBytes(int field)
    {
        if (field == (int)FieldName.DataValue)
        {
            var bytes = new byte[DataValueLength];
            bytes[0] = unchecked((byte)(UnsequencedMessageCode.EnterOrderMessage));
            return bytes;
        }
        return ReadOnlyMemory<byte>.Empty;
    }
}
