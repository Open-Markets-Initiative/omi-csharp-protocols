using System.Buffers.Binary;
using System.Diagnostics;

namespace Databento.MarketData.Normalized.Dbn.v2;

/// <summary>
///  Consolidated market by price implementation with a known book depth of 1; the record of the cmbp-1 and tcbbo schemas
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class Cmbp1Msg : IFileMessage, IGatewayMessage, IEquatable<Cmbp1Msg>, IFieldOwner
{
    internal const int PriceOffset = 0;

    internal const int PriceLength = 8;

    /// <summary>
    ///  The number of decimal places in Price.
    /// </summary>
    public const byte PriceScale = 9;

    /// <summary>
    ///  The factor by which <see cref="Price"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal PriceFactor = 1000000000m;

    /// <summary>
    ///  The encoded value of Price that means no value.
    /// </summary>
    public const long PriceNullValue = 9223372036854775807L;

    internal const int SizeOffset = PriceOffset + PriceLength;

    internal const int SizeLength = 4;

    internal const int ActionOffset = SizeOffset + SizeLength;

    internal const int ActionLength = 1;

    internal const int SideOffset = ActionOffset + ActionLength;

    internal const int SideLength = 1;

    internal const int FlagsOffset = SideOffset + SideLength;

    internal const int FlagsLength = 1;

    internal const int Reserved1Offset = FlagsOffset + FlagsLength;

    internal const int Reserved1Length = 1;

    internal const int TsRecvOffset = Reserved1Offset + Reserved1Length;

    internal const int TsRecvLength = 8;

    /// <summary>
    ///  The number of nanoseconds in one unit of the wire value of <see cref="TsRecv"/>.
    /// </summary>
    public const long TsRecvNanosecondsPerUnit = 1;

    /// <summary>
    ///  The encoded value of Ts Recv that means no value.
    /// </summary>
    public const ulong TsRecvNullValue = 18446744073709551615UL;

    internal const int TsInDeltaOffset = TsRecvOffset + TsRecvLength;

    internal const int TsInDeltaLength = 4;

    internal const int Reserved4Offset = TsInDeltaOffset + TsInDeltaLength;

    internal const int Reserved4Length = 4;

    internal const int ConsolidatedBidAskPairOffset = Reserved4Offset + Reserved4Length;

    internal const int ConsolidatedBidAskPairLength = 32;

    /// <summary>
    ///  The encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public const int MessageByteLength = PriceLength + SizeLength + ActionLength + SideLength + FlagsLength + Reserved1Length + TsRecvLength + TsInDeltaLength + Reserved4Length + ConsolidatedBidAskPairLength;

    internal long price;

    /// <summary>
    ///  The price where every 1 unit corresponds to 1e-9
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="PriceNullValue"/>. Setting <see langword="null"/> writes <see cref="PriceNullValue"/>. A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range. A result equal to <see cref="PriceNullValue"/> is moved one step to the nearest other value.
    /// </remarks>
    public decimal? Price
    {
        get => this.price == PriceNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.price, PriceScale);
        set
        {
            this.price = value is { } present ? FixedPoint.ToRaw<long>(present, PriceFactor, long.MinValue, long.MaxValue, PriceNullValue) : PriceNullValue;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="Price"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long PriceRaw
    {
        get => this.price;
        set
        {
            this.price = value;
            Touch();
        }
    }

    internal uint size;

    /// <summary>
    ///  The order quantity
    /// </summary>
    public uint Size
    {
        get => this.size;
        set
        {
            this.size = value;
            Touch();
        }
    }

    internal ActionValue action;

    /// <summary>
    ///  The event action
    /// </summary>
    public ActionValue Action
    {
        get => this.action;
        set
        {
            this.action = value;
            Touch();
        }
    }

    internal Side side;

    /// <summary>
    ///  The side that initiates the event
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

    internal Flags flags;

    /// <summary>
    ///  A bit field indicating event end, message characteristics, and data quality; declared least significant bit first
    /// </summary>
    public Flags Flags
    {
        get => this.flags;
        set
        {
            this.flags = value;
            Touch();
        }
    }

    internal ulong tsRecv;

    /// <summary>
    ///  The capture-server-received timestamp expressed as the number of nanoseconds since the UNIX epoch
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="TsRecvNullValue"/>. Setting <see langword="null"/> writes <see cref="TsRecvNullValue"/>. This property has a precision of 100 nanoseconds, and <see cref="TsRecvRaw"/> holds the exact value. Setting a local time converts it to UTC, and a time of unspecified kind is treated as UTC. A time outside the range of the field is set to the nearest limit of the range. A result equal to <see cref="TsRecvNullValue"/> is moved one step to the nearest other value.
    /// </remarks>
    public DateTime? TsRecv
    {
        get => this.tsRecv == TsRecvNullValue ? (DateTime?)null : Instant.ToDateTime(this.tsRecv, TsRecvNanosecondsPerUnit);
        set
        {
            this.tsRecv = value is { } present ? Instant.ToRaw<ulong>(present, TsRecvNanosecondsPerUnit, ulong.MinValue, ulong.MaxValue, TsRecvNullValue) : TsRecvNullValue;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="TsRecv"/>, as the unscaled integer that the field holds.
    /// </summary>
    public ulong TsRecvRaw
    {
        get => this.tsRecv;
        set
        {
            this.tsRecv = value;
            Touch();
        }
    }

    internal int tsInDelta;

    /// <summary>
    ///  The matching-engine-sending timestamp expressed as the number of nanoseconds before ts_recv
    /// </summary>
    public int TsInDelta
    {
        get => this.tsInDelta;
        set
        {
            this.tsInDelta = value;
            Touch();
        }
    }

    internal ConsolidatedBidAskPair consolidatedBidAskPair = new ConsolidatedBidAskPair();

    /// <summary>
    ///  A price level consolidated from multiple venues
    /// </summary>
    /// <remarks>
    ///  Setting <see langword="null"/> stores the value that Clear stores.
    /// </remarks>
    public ConsolidatedBidAskPair ConsolidatedBidAskPair
    {
        get => this.consolidatedBidAskPair;
        set
        {
            var copy = value is null ? new ConsolidatedBidAskPair() : new ConsolidatedBidAskPair(value);
            ((IFieldOwner)this.consolidatedBidAskPair).Owner = null;
            this.consolidatedBidAskPair = copy;
            ((IFieldOwner)this.consolidatedBidAskPair).Owner = this;
            Touch();
        }
    }

    private RecordBodyCode dataValue;

    /// <summary>
    ///  Gets or sets the message type.
    /// </summary>
    /// <remarks>
    ///  ParseFrom preserves the message type because the bytes that it reads do not contain it. Clear restores the default message type.
    /// </remarks>
    public RecordBodyCode DataValue
    {
        get => this.dataValue;
        set
        {
            this.dataValue = value;
            Touch();
        }
    }

    internal Cmbp1Msg SetDataValue(RecordBodyCode type)
    {
        this.dataValue = type;
        return this;
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
    public bool IsRecognized => global::System.Enum.IsDefined(this.action) && global::System.Enum.IsDefined(this.side) && this.consolidatedBidAskPair.IsRecognized;

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

    IMessage IMessage.Copy() => new Cmbp1Msg(this);

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
        ///  The price where every 1 unit corresponds to 1e-9
        /// </summary>
        Price,
        /// <summary>
        ///  The order quantity
        /// </summary>
        Size,
        /// <summary>
        ///  The event action
        /// </summary>
        Action,
        /// <summary>
        ///  The side that initiates the event
        /// </summary>
        Side,
        /// <summary>
        ///  A bit field indicating event end, message characteristics, and data quality; declared least significant bit first
        /// </summary>
        Flags,
        /// <summary>
        ///  Reserved, 1 byte
        /// </summary>
        Reserved1,
        /// <summary>
        ///  The capture-server-received timestamp expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        TsRecv,
        /// <summary>
        ///  The matching-engine-sending timestamp expressed as the number of nanoseconds before ts_recv
        /// </summary>
        TsInDelta,
        /// <summary>
        ///  Reserved, 4 bytes
        /// </summary>
        Reserved4,
        /// <summary>
        ///  A price level consolidated from multiple venues
        /// </summary>
        ConsolidatedBidAskPair,
    }

    /// <summary>
    ///  Provides access to the fields of one instance, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public readonly struct FieldSet : IEnumerable<FieldView>
    {
        private readonly Cmbp1Msg owner;

        internal FieldSet(Cmbp1Msg owner)
        {
            this.owner = owner;
        }

        /// <summary>
        ///  The message type.
        /// </summary>
        public FieldView DataValue => this[FieldName.DataValue];

        /// <summary>
        ///  The price where every 1 unit corresponds to 1e-9
        /// </summary>
        public FieldView Price => this[FieldName.Price];

        /// <summary>
        ///  The order quantity
        /// </summary>
        public FieldView Size => this[FieldName.Size];

        /// <summary>
        ///  The event action
        /// </summary>
        public FieldView Action => this[FieldName.Action];

        /// <summary>
        ///  The side that initiates the event
        /// </summary>
        public FieldView Side => this[FieldName.Side];

        /// <summary>
        ///  A bit field indicating event end, message characteristics, and data quality; declared least significant bit first
        /// </summary>
        public FieldView Flags => this[FieldName.Flags];

        /// <summary>
        ///  Reserved, 1 byte
        /// </summary>
        public FieldView Reserved1 => this[FieldName.Reserved1];

        /// <summary>
        ///  The capture-server-received timestamp expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public FieldView TsRecv => this[FieldName.TsRecv];

        /// <summary>
        ///  The matching-engine-sending timestamp expressed as the number of nanoseconds before ts_recv
        /// </summary>
        public FieldView TsInDelta => this[FieldName.TsInDelta];

        /// <summary>
        ///  Reserved, 4 bytes
        /// </summary>
        public FieldView Reserved4 => this[FieldName.Reserved4];

        /// <summary>
        ///  A price level consolidated from multiple venues
        /// </summary>
        public FieldView ConsolidatedBidAskPair => this[FieldName.ConsolidatedBidAskPair];

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
        new(nameof(FieldName.Price), FieldKind.Decimal, PriceLength) { Scale = PriceScale, ByteOrder = "Little", IsSigned = true, Nullable = "Value", NullValue = PriceNullValue },
        new(nameof(FieldName.Size), FieldKind.UnsignedInteger, SizeLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.Action), FieldKind.Enum, ActionLength),
        new(nameof(FieldName.Side), FieldKind.Enum, SideLength),
        new(nameof(FieldName.Flags), FieldKind.Bitfield, FlagsLength) { IsSigned = false, Bits = [(nameof(global::Databento.MarketData.Normalized.Dbn.v2.Flags.ReservedFlag), global::Databento.MarketData.Normalized.Dbn.v2.Flags.ReservedFlagShift, global::Databento.MarketData.Normalized.Dbn.v2.Flags.ReservedFlagWidth), (nameof(global::Databento.MarketData.Normalized.Dbn.v2.Flags.PublisherSpecific), global::Databento.MarketData.Normalized.Dbn.v2.Flags.PublisherSpecificShift, global::Databento.MarketData.Normalized.Dbn.v2.Flags.PublisherSpecificWidth), (nameof(global::Databento.MarketData.Normalized.Dbn.v2.Flags.MaybeBadBook), global::Databento.MarketData.Normalized.Dbn.v2.Flags.MaybeBadBookShift, global::Databento.MarketData.Normalized.Dbn.v2.Flags.MaybeBadBookWidth), (nameof(global::Databento.MarketData.Normalized.Dbn.v2.Flags.BadTsRecv), global::Databento.MarketData.Normalized.Dbn.v2.Flags.BadTsRecvShift, global::Databento.MarketData.Normalized.Dbn.v2.Flags.BadTsRecvWidth), (nameof(global::Databento.MarketData.Normalized.Dbn.v2.Flags.Mbp), global::Databento.MarketData.Normalized.Dbn.v2.Flags.MbpShift, global::Databento.MarketData.Normalized.Dbn.v2.Flags.MbpWidth), (nameof(global::Databento.MarketData.Normalized.Dbn.v2.Flags.Snapshot), global::Databento.MarketData.Normalized.Dbn.v2.Flags.SnapshotShift, global::Databento.MarketData.Normalized.Dbn.v2.Flags.SnapshotWidth), (nameof(global::Databento.MarketData.Normalized.Dbn.v2.Flags.Tob), global::Databento.MarketData.Normalized.Dbn.v2.Flags.TobShift, global::Databento.MarketData.Normalized.Dbn.v2.Flags.TobWidth), (nameof(global::Databento.MarketData.Normalized.Dbn.v2.Flags.Last), global::Databento.MarketData.Normalized.Dbn.v2.Flags.LastShift, global::Databento.MarketData.Normalized.Dbn.v2.Flags.LastWidth)] },
        new(nameof(FieldName.Reserved1), FieldKind.Padding, Reserved1Length),
        new(nameof(FieldName.TsRecv), FieldKind.DateTime, TsRecvLength) { Unit = "Nanoseconds", Epoch = "Unix", ByteOrder = "Little", IsSigned = false, Nullable = "Value", NullValue = TsRecvNullValue },
        new(nameof(FieldName.TsInDelta), FieldKind.SignedInteger, TsInDeltaLength) { ByteOrder = "Little", IsSigned = true },
        new(nameof(FieldName.Reserved4), FieldKind.Padding, Reserved4Length),
        new(nameof(FieldName.ConsolidatedBidAskPair), FieldKind.Struct, ConsolidatedBidAskPairLength),
    ];

    /// <summary>
    ///  Gets the fields in wire order, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public FieldSet Fields => new(this);

    IEnumerable<FieldView> IMessage.Fields => new FieldSet(this);

    /// <summary>
    ///  Initializes a new instance of the <see cref="Cmbp1Msg"/> class with default values.
    /// </summary>
    public Cmbp1Msg()
    {
        Clear();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="Cmbp1Msg"/> class with the values, bytes, and parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Cmbp1Msg(Cmbp1Msg other) : this()
    {
        CopyFrom(other);
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(Cmbp1Msg other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.price = other.price;
        this.size = other.size;
        this.action = other.action;
        this.side = other.side;
        this.flags = other.flags;
        this.tsRecv = other.tsRecv;
        this.tsInDelta = other.tsInDelta;
        ((IFieldOwner)this.consolidatedBidAskPair).Owner = null;
        this.consolidatedBidAskPair = new ConsolidatedBidAskPair(other.consolidatedBidAskPair);
        ((IFieldOwner)this.consolidatedBidAskPair).Owner = this;
        this.dataValue = other.dataValue;
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
        this.price = default;
        this.size = default;
        this.action = default;
        this.side = default;
        this.flags = default;
        this.tsRecv = default;
        this.tsInDelta = default;
        ((IFieldOwner)this.consolidatedBidAskPair).Owner = null;
        this.consolidatedBidAskPair = new ConsolidatedBidAskPair();
        ((IFieldOwner)this.consolidatedBidAskPair).Owner = this;
        this.dataValue = RecordBodyCode.Cmbp1Msg;
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
    public static Cmbp1Msg Parse(ReadOnlySpan<byte> data)
    {
        var instance = new Cmbp1Msg();
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
    public static Cmbp1Msg Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        var instance = new Cmbp1Msg();
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
        var storedCode = this.dataValue;
        Clear();
        this.dataValue = storedCode;
        if ((uint)offset > (uint)data.Length)
        {
            this.status = ParseStatus.InvalidOffset;
            return;
        }
        var start = offset;
        var cursor = start;
        if (Truncated(data, start, cursor, PriceLength, FieldName.Price)) return;
        this.price = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(cursor, PriceLength));
        cursor += PriceLength;
        if (Truncated(data, start, cursor, SizeLength, FieldName.Size)) return;
        this.size = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(cursor, SizeLength));
        cursor += SizeLength;
        if (Truncated(data, start, cursor, ActionLength, FieldName.Action)) return;
        this.action = (ActionValue)data.Slice(cursor, ActionLength)[0];
        cursor += ActionLength;
        if (Truncated(data, start, cursor, SideLength, FieldName.Side)) return;
        this.side = (Side)data.Slice(cursor, SideLength)[0];
        cursor += SideLength;
        if (Truncated(data, start, cursor, FlagsLength, FieldName.Flags)) return;
        this.flags = new Flags(data.Slice(cursor, FlagsLength)[0]);
        cursor += FlagsLength;
        if (Truncated(data, start, cursor, Reserved1Length, FieldName.Reserved1)) return;
        cursor += Reserved1Length;
        if (Truncated(data, start, cursor, TsRecvLength, FieldName.TsRecv)) return;
        this.tsRecv = BinaryPrimitives.ReadUInt64LittleEndian(data.Slice(cursor, TsRecvLength));
        cursor += TsRecvLength;
        if (Truncated(data, start, cursor, TsInDeltaLength, FieldName.TsInDelta)) return;
        this.tsInDelta = BinaryPrimitives.ReadInt32LittleEndian(data.Slice(cursor, TsInDeltaLength));
        cursor += TsInDeltaLength;
        if (Truncated(data, start, cursor, Reserved4Length, FieldName.Reserved4)) return;
        cursor += Reserved4Length;
        {
            var instance = new ConsolidatedBidAskPair();
            instance.ParseFrom(data, ref cursor);
            this.consolidatedBidAskPair = instance;
            ((IFieldOwner)this.consolidatedBidAskPair).Owner = this;
            if (this.consolidatedBidAskPair.Status != ParseStatus.Ok)
            {
                Fail(this.consolidatedBidAskPair.Status, FieldName.ConsolidatedBidAskPair, cursor - start + this.consolidatedBidAskPair.FailedOffset, data[start..]);
                return;
            }
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
        if (this.dataValue is not (RecordBodyCode.Cmbp1Msg or RecordBodyCode.Cmbp1Msg2))
        {
            failure = new EncodeFailure(Descriptors[(int)FieldName.DataValue], EncodeStatus.DoesNotFit, null);
            return false;
        }
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(cursor, PriceLength), this.price);
        cursor += PriceLength;
        BinaryPrimitives.WriteUInt32LittleEndian(data.Slice(cursor, SizeLength), this.size);
        cursor += SizeLength;
        data.Slice(cursor, ActionLength)[0] = unchecked((byte)(this.action));
        cursor += ActionLength;
        data.Slice(cursor, SideLength)[0] = unchecked((byte)(this.side));
        cursor += SideLength;
        data.Slice(cursor, FlagsLength)[0] = this.flags.Value;
        cursor += FlagsLength;
        {
            if (this.raw.Length >= Reserved1Offset + Reserved1Length)
            {
                this.raw.AsSpan(Reserved1Offset, Reserved1Length).CopyTo(data.Slice(cursor, Reserved1Length));
            }
            else
            {
                data.Slice(cursor, Reserved1Length).Clear();
            }
            cursor += Reserved1Length;
        }
        BinaryPrimitives.WriteUInt64LittleEndian(data.Slice(cursor, TsRecvLength), this.tsRecv);
        cursor += TsRecvLength;
        BinaryPrimitives.WriteInt32LittleEndian(data.Slice(cursor, TsInDeltaLength), this.tsInDelta);
        cursor += TsInDeltaLength;
        {
            if (this.raw.Length >= Reserved4Offset + Reserved4Length)
            {
                this.raw.AsSpan(Reserved4Offset, Reserved4Length).CopyTo(data.Slice(cursor, Reserved4Length));
            }
            else
            {
                data.Slice(cursor, Reserved4Length).Clear();
            }
            cursor += Reserved4Length;
        }
        {
            if (!((IGroupEntry)this.consolidatedBidAskPair).Write(data, cursor, out cursor, out var memberFailure))
            {
                failure = memberFailure.Within(new FieldView(this, (int)FieldName.ConsolidatedBidAskPair), null);
                return false;
            }
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
        if (!global::System.Enum.IsDefined(this.action))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.Action), ValidationStatus.UndeclaredValue));
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
    public bool Equals(Cmbp1Msg? other)
    {
        if (other is null)
        {
            return false;
        }
        if (this.price != other.price) return false;
        if (this.size != other.size) return false;
        if (this.action != other.action) return false;
        if (this.side != other.side) return false;
        if (this.flags != other.flags) return false;
        if (this.tsRecv != other.tsRecv) return false;
        if (this.tsInDelta != other.tsInDelta) return false;
        if (!this.consolidatedBidAskPair.Equals(other.consolidatedBidAskPair)) return false;
        if (this.dataValue != other.dataValue) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is Cmbp1Msg other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.price);
        hash.Add(this.size);
        hash.Add(this.action);
        hash.Add(this.side);
        hash.Add(this.flags);
        hash.Add(this.tsRecv);
        hash.Add(this.tsInDelta);
        hash.Add(this.consolidatedBidAskPair);
        hash.Add(this.dataValue);
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
        private const int FieldCount = 11;

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
            if (Has(FieldName.Price)) names.Add(nameof(FieldName.Price));
            if (Has(FieldName.Size)) names.Add(nameof(FieldName.Size));
            if (Has(FieldName.Action)) names.Add(nameof(FieldName.Action));
            if (Has(FieldName.Side)) names.Add(nameof(FieldName.Side));
            if (Has(FieldName.Flags)) names.Add(nameof(FieldName.Flags));
            if (Has(FieldName.Reserved1)) names.Add(nameof(FieldName.Reserved1));
            if (Has(FieldName.TsRecv)) names.Add(nameof(FieldName.TsRecv));
            if (Has(FieldName.TsInDelta)) names.Add(nameof(FieldName.TsInDelta));
            if (Has(FieldName.Reserved4)) names.Add(nameof(FieldName.Reserved4));
            if (Has(FieldName.ConsolidatedBidAskPair)) names.Add(nameof(FieldName.ConsolidatedBidAskPair));
            return names.Count == 0 ? "None" : string.Join(", ", names);
        }
    }

    /// <summary>
    ///  Compares this instance with another and identifies the fields that differ.
    /// </summary>
    /// <returns>The fields that differ, or None if there are no differences.</returns>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Changes Diff(Cmbp1Msg other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (this.dataValue != other.dataValue) diff = diff.With(FieldName.DataValue);
        if (this.price != other.price) diff = diff.With(FieldName.Price);
        if (this.size != other.size) diff = diff.With(FieldName.Size);
        if (this.action != other.action) diff = diff.With(FieldName.Action);
        if (this.side != other.side) diff = diff.With(FieldName.Side);
        if (this.flags != other.flags) diff = diff.With(FieldName.Flags);
        if (this.tsRecv != other.tsRecv) diff = diff.With(FieldName.TsRecv);
        if (this.tsInDelta != other.tsInDelta) diff = diff.With(FieldName.TsInDelta);
        if (!this.consolidatedBidAskPair.Equals(other.consolidatedBidAskPair)) diff = diff.With(FieldName.ConsolidatedBidAskPair);
        return diff;
    }

    private void CopyField(FieldName field, Cmbp1Msg from)
    {
        switch (field)
        {
            case FieldName.DataValue:
                this.dataValue = from.dataValue;
                break;
            case FieldName.Price:
                this.price = from.price;
                break;
            case FieldName.Size:
                this.size = from.size;
                break;
            case FieldName.Action:
                this.action = from.action;
                break;
            case FieldName.Side:
                this.side = from.side;
                break;
            case FieldName.Flags:
                this.flags = from.flags;
                break;
            case FieldName.TsRecv:
                this.tsRecv = from.tsRecv;
                break;
            case FieldName.TsInDelta:
                this.tsInDelta = from.tsInDelta;
                break;
            case FieldName.ConsolidatedBidAskPair:
                ((IFieldOwner)this.consolidatedBidAskPair).Owner = null;
                this.consolidatedBidAskPair = new ConsolidatedBidAskPair(from.consolidatedBidAskPair);
                ((IFieldOwner)this.consolidatedBidAskPair).Owner = this;
                break;
            default:
                return;
        }
        Touch();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"Cmbp1Msg {{ DataValue: {Text.Enum(this.dataValue)}, Price: {(Text.Of(this.price == PriceNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.price, PriceScale)))}, Size: {(Text.Of(this.size))}, Action: {(Text.Enum(this.action))}, Side: {(Text.Enum(this.side))}, Flags: {(Text.Value(this.flags))}, TsRecv: {(Text.Instant(this.tsRecv, TsRecvNanosecondsPerUnit, TsRecvNullValue))}, TsInDelta: {(Text.Of(this.tsInDelta))}, ConsolidatedBidAskPair: {(Text.Of(this.consolidatedBidAskPair.ToString()))} }}{(this.trailer.Length == 0 ? string.Empty : $" Trailer: {(Text.Of(this.trailer.Length))} bytes")}";

    private string DebuggerDisplay => $"[{DataValue}] [{(Text.Of(this.price == PriceNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.price, PriceScale)))}] [{(Text.Of(this.size))}] [{(Text.Enum(this.action))}]";

    IFieldOwner? IFieldOwner.Owner
    {
        get => this.owner;
        set => this.owner = value;
    }

    FieldDescriptor[] IFieldOwner.Descriptors => Descriptors;

    void IFieldOwner.Touch() => Touch();

    string IFieldOwner.TextOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => Text.Enum(this.dataValue),
        FieldName.Price => Text.Of(this.price == PriceNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.price, PriceScale)),
        FieldName.Size => Text.Of(this.size),
        FieldName.Action => Text.Enum(this.action),
        FieldName.Side => Text.Enum(this.side),
        FieldName.Flags => Text.Value(this.flags),
        FieldName.TsRecv => Text.Instant(this.tsRecv, TsRecvNanosecondsPerUnit, TsRecvNullValue),
        FieldName.TsInDelta => Text.Of(this.tsInDelta),
        FieldName.ConsolidatedBidAskPair => Text.Of(this.consolidatedBidAskPair.ToString()),
        _ => string.Empty,
    };

    bool IFieldOwner.IsRecognized(int field) => (FieldName)field switch
    {
        FieldName.Action => global::System.Enum.IsDefined(this.action),
        FieldName.Side => global::System.Enum.IsDefined(this.side),
        FieldName.ConsolidatedBidAskPair => this.consolidatedBidAskPair.IsRecognized,
        _ => true,
    };

    bool IFieldOwner.IsPresent(int field) => (FieldName)field switch
    {
        FieldName.Price => this.price != PriceNullValue,
        FieldName.TsRecv => this.tsRecv != TsRecvNullValue,
        _ => true,
    };

    bool IFieldOwner.IsParsed(int field) => this.status == ParseStatus.Ok || field < (int?)this.failed;

    bool IFieldOwner.IsReached(int field) => this.modified || this.status is ParseStatus.None or ParseStatus.Ok || field <= (int?)this.failed;

    bool IFieldOwner.HasParseLayout(int field) => false;

    int IFieldOwner.OffsetOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => FieldView.NotInMessage,
        FieldName.Price => PriceOffset,
        FieldName.Size => SizeOffset,
        FieldName.Action => ActionOffset,
        FieldName.Side => SideOffset,
        FieldName.Flags => FlagsOffset,
        FieldName.Reserved1 => Reserved1Offset,
        FieldName.TsRecv => TsRecvOffset,
        FieldName.TsInDelta => TsInDeltaOffset,
        FieldName.Reserved4 => Reserved4Offset,
        FieldName.ConsolidatedBidAskPair => ConsolidatedBidAskPairOffset,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    int IFieldOwner.LengthOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => DataValueLength,
        FieldName.Price => PriceLength,
        FieldName.Size => SizeLength,
        FieldName.Action => ActionLength,
        FieldName.Side => SideLength,
        FieldName.Flags => FlagsLength,
        FieldName.Reserved1 => Reserved1Length,
        FieldName.TsRecv => TsRecvLength,
        FieldName.TsInDelta => TsInDeltaLength,
        FieldName.Reserved4 => Reserved4Length,
        FieldName.ConsolidatedBidAskPair => ConsolidatedBidAskPairLength,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    IReadOnlyList<IGroupEntry>? IFieldOwner.EntriesOf(int field) => null;

    IMessage? IFieldOwner.MessageOf(int field) => null;

    ulong[] IFieldOwner.ChangedAgainst(IFieldOwner other) => other is Cmbp1Msg typed ? Diff(typed).ToWords() : throw new ArgumentException("The other instance is not of the same type.", nameof(other));

    void IFieldOwner.CopyField(int field, IFieldOwner from) => CopyField((FieldName)field, from as Cmbp1Msg ?? throw new ArgumentException("The other instance is not of the same type.", nameof(from)));

    ReadOnlyMemory<byte> IFieldOwner.CodeBytes(int field)
    {
        if (field == (int)FieldName.DataValue)
        {
            var bytes = new byte[DataValueLength];
            bytes[0] = unchecked((byte)(this.dataValue));
            return bytes;
        }
        return ReadOnlyMemory<byte>.Empty;
    }
}
