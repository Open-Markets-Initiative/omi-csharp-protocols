using System.Buffers.Binary;
using System.Diagnostics;
using static Nasdaq.NsmEquities.Orders.Ouch.v5_0.Rules;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  The Unsequenced Data Packets act as an envelope to carry the actual data messages that are transferred from the client to the server
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class UnsequencedDataPacket : IClientMessage, IEquatable<UnsequencedDataPacket>, IFieldOwner
{
    internal const int UnsequencedMessageTypeOffset = 0;

    internal const int UnsequencedMessageTypeLength = 1;

    internal const int UnsequencedMessageOffset = UnsequencedMessageTypeOffset + UnsequencedMessageTypeLength;

    /// <summary>
    ///  The encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public const int MessageByteLength = UnsequencedMessageTypeLength;

    /// <summary>
    ///  Value identifying unsequenced message type
    /// </summary>
    public UnsequencedMessageCode UnsequencedMessageType => this.unsequencedMessage.DataValue;

    internal IUnsequencedMessage unsequencedMessage = new UnknownUnsequencedMessage();

    /// <summary>
    ///  The unsequenced (client to server) message carried by the packet, opaque bytes unless an application source dispatches it
    /// </summary>
    /// <remarks>
    ///  Setting the property stores a copy of the message. Setting <see langword="null"/> stores the value that Clear stores.
    /// </remarks>
    public IUnsequencedMessage UnsequencedMessage
    {
        get => this.unsequencedMessage;
        set
        {
            this.unsequencedMessage.SetOwner(null);
            this.unsequencedMessage = value is null ? new UnknownUnsequencedMessage() : (IUnsequencedMessage)value.Copy();
            this.unsequencedMessage.SetOwner(this);
            Touch();
        }
    }

    /// <summary>
    ///  Gets the message type.
    /// </summary>
    public ClientMessageCode DataValue => ClientMessageCode.UnsequencedDataPacket;

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
            position += UnsequencedMessageTypeLength;
            position += this.unsequencedMessage.EncodedByteLength + this.unsequencedMessage.Trailer.Length;
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
    public bool IsRecognized => this.unsequencedMessage is not UnknownUnsequencedMessage && this.unsequencedMessage.IsRecognized;

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

    IMessage IMessage.Copy() => new UnsequencedDataPacket(this);

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
        ///  Value identifying unsequenced message type
        /// </summary>
        UnsequencedMessageType,
        /// <summary>
        ///  The unsequenced (client to server) message carried by the packet, opaque bytes unless an application source dispatches it
        /// </summary>
        UnsequencedMessage,
    }

    /// <summary>
    ///  Provides access to the fields of one instance, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public readonly struct FieldSet : IEnumerable<FieldView>
    {
        private readonly UnsequencedDataPacket owner;

        internal FieldSet(UnsequencedDataPacket owner)
        {
            this.owner = owner;
        }

        /// <summary>
        ///  The message type.
        /// </summary>
        public FieldView DataValue => this[FieldName.DataValue];

        /// <summary>
        ///  Value identifying unsequenced message type
        /// </summary>
        public FieldView UnsequencedMessageType => this[FieldName.UnsequencedMessageType];

        /// <summary>
        ///  The unsequenced (client to server) message carried by the packet, opaque bytes unless an application source dispatches it
        /// </summary>
        public FieldView UnsequencedMessage => this[FieldName.UnsequencedMessage];

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
        new(nameof(FieldName.UnsequencedMessageType), FieldKind.Enum, UnsequencedMessageTypeLength),
        new(nameof(FieldName.UnsequencedMessage), FieldKind.Message, null),
    ];

    /// <summary>
    ///  Gets the fields in wire order, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public FieldSet Fields => new(this);

    IEnumerable<FieldView> IMessage.Fields => new FieldSet(this);

    /// <summary>
    ///  Initializes a new instance of the <see cref="UnsequencedDataPacket"/> class with default values.
    /// </summary>
    public UnsequencedDataPacket()
    {
        Clear();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="UnsequencedDataPacket"/> class with the values, bytes, and parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public UnsequencedDataPacket(UnsequencedDataPacket other) : this()
    {
        CopyFrom(other);
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(UnsequencedDataPacket other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.unsequencedMessage.SetOwner(null);
        this.unsequencedMessage = (IUnsequencedMessage)other.unsequencedMessage.Copy();
        this.unsequencedMessage.SetOwner(this);
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
        this.unsequencedMessage.SetOwner(null);
        this.unsequencedMessage = new UnknownUnsequencedMessage();
        this.unsequencedMessage.SetOwner(this);
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
    public static UnsequencedDataPacket Parse(ReadOnlySpan<byte> data)
    {
        var instance = new UnsequencedDataPacket();
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
    public static UnsequencedDataPacket Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        var instance = new UnsequencedDataPacket();
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
    public void ParseFrom(ReadOnlySpan<byte> data, ref int offset) => ParseFrom(data, ref offset, null, null);

    internal void ParseFrom(ReadOnlySpan<byte> data, ref int offset, global::System.Int128? sizeRule0Dependency0Passed, global::System.Int128? unsequencedMessageSizeRule0Dependency0Passed)
    {
        Clear();
        if ((uint)offset > (uint)data.Length)
        {
            this.status = ParseStatus.InvalidOffset;
            return;
        }
        var start = offset;
        var cursor = start;
        ReadOnlySpan<byte> unsequencedMessageTypeValue = default;
        {
            int UnsequencedDataPacketSizeRule0End;
            global::System.Int128 UnsequencedDataPacketSizeRule0Width;
            global::System.Int128 sizeRule0Dependency0 = sizeRule0Dependency0Passed.GetValueOrDefault();
            int? unsequencedDataPacketEnd = null;
            if (sizeRule0Dependency0Passed is not null)
            {
                const long UnsequencedDataPacketSizeRule0Data2 = 1;
                try
                {
                    UnsequencedDataPacketSizeRule0Width = checked((sizeRule0Dependency0 - ((global::System.Int128)(UnsequencedDataPacketSizeRule0Data2))));
                }
                catch (global::System.OverflowException)
                {
                    { this.status = ParseStatus.Truncated; this.failedOffset = 0; this.raw = data[start..].ToArray(); return; }
                }
                if (UnsequencedDataPacketSizeRule0Width < RuleEmptyWidth || UnsequencedDataPacketSizeRule0Width > (long)data.Length - start) { this.status = ParseStatus.Truncated; this.failedOffset = 0; this.raw = data[start..].ToArray(); return; }
                UnsequencedDataPacketSizeRule0End = start + (int)UnsequencedDataPacketSizeRule0Width;
                unsequencedDataPacketEnd = UnsequencedDataPacketSizeRule0End;
                if (UnsequencedDataPacketSizeRule0End < cursor) { this.status = ParseStatus.Truncated; this.failedOffset = 0; this.raw = data[start..].ToArray(); return; }
            }
            if (unsequencedDataPacketEnd is int unsequencedDataPacketDeclaredEnd)
            {
                data = data[..unsequencedDataPacketDeclaredEnd];
            }
        }
        if (Truncated(data, start, cursor, UnsequencedMessageTypeLength, FieldName.UnsequencedMessageType)) return;
        unsequencedMessageTypeValue = data.Slice(cursor, UnsequencedMessageTypeLength);
        cursor += UnsequencedMessageTypeLength;
        {
            var unsequencedMessageAvailableBytes = data;
            var unsequencedMessageEndRead = false;
            {
                int UnsequencedMessageSizeRule0End;
                global::System.Int128 UnsequencedMessageSizeRule0Width;
                global::System.Int128 unsequencedMessageSizeRule0Dependency0 = unsequencedMessageSizeRule0Dependency0Passed.GetValueOrDefault();
                int? unsequencedMessageEnd = null;
                if (unsequencedMessageSizeRule0Dependency0Passed is not null)
                {
                    const long UnsequencedMessageSizeRule0Data2 = 2;
                    try
                    {
                        UnsequencedMessageSizeRule0Width = checked((unsequencedMessageSizeRule0Dependency0 - ((global::System.Int128)(UnsequencedMessageSizeRule0Data2))));
                    }
                    catch (global::System.OverflowException)
                    {
                        { Fail(ParseStatus.Truncated, FieldName.UnsequencedMessage, cursor - start, data[start..]); return; }
                    }
                    if (UnsequencedMessageSizeRule0Width < RuleEmptyWidth || UnsequencedMessageSizeRule0Width > (long)data.Length - cursor) { Fail(ParseStatus.Truncated, FieldName.UnsequencedMessage, cursor - start, data[start..]); return; }
                    UnsequencedMessageSizeRule0End = cursor + (int)UnsequencedMessageSizeRule0Width;
                    unsequencedMessageEnd = UnsequencedMessageSizeRule0End;
                }
                if (unsequencedMessageEnd is int unsequencedMessageDeclaredEnd)
                {
                    data = data[..unsequencedMessageDeclaredEnd];
                    unsequencedMessageEndRead = true;
                }
            }
            {
                int unsequencedMessageCase;
                const int UnsequencedMessageNoCase = 0;
                const int UnsequencedMessageBranchRule1Case = 1;
                const int UnsequencedMessageBranchRule2Case = 2;
                const int UnsequencedMessageBranchRule3Case = 3;
                const int UnsequencedMessageBranchRule4Case = 4;
                const int UnsequencedMessageBranchRule5Case = 5;
                const int UnsequencedMessageBranchRule6Case = 6;
                const int UnsequencedMessageBranchRule7Case = 7;
                const int UnsequencedMessageBranchRule8Case = 8;
                unsequencedMessageCase = UnsequencedMessageNoCase;
                {
                    global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule1Data2 = "O"u8;
                    global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule2Data2 = "U"u8;
                    global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule3Data2 = "X"u8;
                    global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule4Data2 = "M"u8;
                    global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule5Data2 = "Q"u8;
                    global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule6Data2 = "C"u8;
                    global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule7Data2 = "D"u8;
                    global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule8Data2 = "E"u8;
                    if (unsequencedMessageTypeValue.SequenceEqual(UnsequencedMessageBranchRule1Data2)) { unsequencedMessageCase = UnsequencedMessageBranchRule1Case; }
                    else if (unsequencedMessageTypeValue.SequenceEqual(UnsequencedMessageBranchRule2Data2)) { unsequencedMessageCase = UnsequencedMessageBranchRule2Case; }
                    else if (unsequencedMessageTypeValue.SequenceEqual(UnsequencedMessageBranchRule3Data2)) { unsequencedMessageCase = UnsequencedMessageBranchRule3Case; }
                    else if (unsequencedMessageTypeValue.SequenceEqual(UnsequencedMessageBranchRule4Data2)) { unsequencedMessageCase = UnsequencedMessageBranchRule4Case; }
                    else if (unsequencedMessageTypeValue.SequenceEqual(UnsequencedMessageBranchRule5Data2)) { unsequencedMessageCase = UnsequencedMessageBranchRule5Case; }
                    else if (unsequencedMessageTypeValue.SequenceEqual(UnsequencedMessageBranchRule6Data2)) { unsequencedMessageCase = UnsequencedMessageBranchRule6Case; }
                    else if (unsequencedMessageTypeValue.SequenceEqual(UnsequencedMessageBranchRule7Data2)) { unsequencedMessageCase = UnsequencedMessageBranchRule7Case; }
                    else if (unsequencedMessageTypeValue.SequenceEqual(UnsequencedMessageBranchRule8Data2)) { unsequencedMessageCase = UnsequencedMessageBranchRule8Case; }
                }
                if (unsequencedMessageCase == UnsequencedMessageBranchRule1Case)
                {
                    var instance = new EnterOrderMessage();
                    instance.ParseFrom(data, ref cursor);
                    this.unsequencedMessage = instance;
                }
                else if (unsequencedMessageCase == UnsequencedMessageBranchRule2Case)
                {
                    var instance = new ReplaceOrderMessage();
                    instance.ParseFrom(data, ref cursor);
                    this.unsequencedMessage = instance;
                }
                else if (unsequencedMessageCase == UnsequencedMessageBranchRule3Case)
                {
                    var instance = new CancelOrderMessage();
                    instance.ParseFrom(data, ref cursor);
                    this.unsequencedMessage = instance;
                }
                else if (unsequencedMessageCase == UnsequencedMessageBranchRule4Case)
                {
                    var instance = new ModifyOrderMessage();
                    instance.ParseFrom(data, ref cursor);
                    this.unsequencedMessage = instance;
                }
                else if (unsequencedMessageCase == UnsequencedMessageBranchRule5Case)
                {
                    var instance = new AccountQueryMessage();
                    instance.ParseFrom(data, ref cursor);
                    this.unsequencedMessage = instance;
                }
                else if (unsequencedMessageCase == UnsequencedMessageBranchRule6Case)
                {
                    var instance = new MassCancelRequestMessage();
                    instance.ParseFrom(data, ref cursor);
                    this.unsequencedMessage = instance;
                }
                else if (unsequencedMessageCase == UnsequencedMessageBranchRule7Case)
                {
                    var instance = new DisableOrderEntryRequestMessage();
                    instance.ParseFrom(data, ref cursor);
                    this.unsequencedMessage = instance;
                }
                else if (unsequencedMessageCase == UnsequencedMessageBranchRule8Case)
                {
                    var instance = new EnableOrderEntryRequestMessage();
                    instance.ParseFrom(data, ref cursor);
                    this.unsequencedMessage = instance;
                }
                else
                {
                    this.unsequencedMessage = UnknownUnsequencedMessage.Parse(data, ref cursor).SetDataValue((UnsequencedMessageCode)unsequencedMessageTypeValue[0]);
                }
                ((IFieldOwner)this.unsequencedMessage).Owner = this;
                if (this.unsequencedMessage.Status != ParseStatus.Ok)
                {
                    Fail(this.unsequencedMessage.Status, FieldName.UnsequencedMessage, cursor - start + this.unsequencedMessage.FailedOffset, data[start..]);
                    return;
                }
                if (unsequencedMessageEndRead)
                {
                    this.unsequencedMessage.SetTrailer(data[cursor..]);
                }
            }
            if (unsequencedMessageEndRead)
            {
                cursor = data.Length;
            }
            data = unsequencedMessageAvailableBytes;
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

    bool IMessage.Write(Span<byte> data, int offset, bool trailerFollows, out int cursor, out EncodeFailure failure) => Write(data, offset, trailerFollows, null, null, out cursor, out failure);

    internal bool Write(Span<byte> data, int offset, bool trailerFollows, global::System.Int128? unsequencedMessageSizeRule0Dependency0Passed, global::System.Int128? sizeRule0Dependency0Passed, out int cursor, out EncodeFailure failure)
    {
        cursor = offset;
        var position = offset;
        position += UnsequencedMessageTypeLength;
        var unsequencedMessageMeasureStart = position;
        position += this.unsequencedMessage.EncodedByteLength + this.unsequencedMessage.Trailer.Length;
        var unsequencedMessageMeasureEnd = position;
        data.Slice(cursor, UnsequencedMessageTypeLength)[0] = unchecked((byte)(this.unsequencedMessage.DataValue));
        cursor += UnsequencedMessageTypeLength;
        {
            int unsequencedMessageCase;
            const int UnsequencedMessageNoCase = 0;
            const int UnsequencedMessageBranchRule1Case = 1;
            const int UnsequencedMessageBranchRule2Case = 2;
            const int UnsequencedMessageBranchRule3Case = 3;
            const int UnsequencedMessageBranchRule4Case = 4;
            const int UnsequencedMessageBranchRule5Case = 5;
            const int UnsequencedMessageBranchRule6Case = 6;
            const int UnsequencedMessageBranchRule7Case = 7;
            const int UnsequencedMessageBranchRule8Case = 8;
            const int unsequencedMessageTypeValueWidth = 1;
            Span<byte> unsequencedMessageTypeValueBytes = stackalloc byte[unsequencedMessageTypeValueWidth];
            unsequencedMessageTypeValueBytes[0] = unchecked((byte)(this.unsequencedMessage.DataValue));
            ReadOnlySpan<byte> unsequencedMessageTypeValue = unsequencedMessageTypeValueBytes;
            unsequencedMessageCase = UnsequencedMessageNoCase;
            {
                global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule1Data2 = "O"u8;
                global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule2Data2 = "U"u8;
                global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule3Data2 = "X"u8;
                global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule4Data2 = "M"u8;
                global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule5Data2 = "Q"u8;
                global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule6Data2 = "C"u8;
                global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule7Data2 = "D"u8;
                global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule8Data2 = "E"u8;
                if (unsequencedMessageTypeValue.SequenceEqual(UnsequencedMessageBranchRule1Data2)) { unsequencedMessageCase = UnsequencedMessageBranchRule1Case; }
                else if (unsequencedMessageTypeValue.SequenceEqual(UnsequencedMessageBranchRule2Data2)) { unsequencedMessageCase = UnsequencedMessageBranchRule2Case; }
                else if (unsequencedMessageTypeValue.SequenceEqual(UnsequencedMessageBranchRule3Data2)) { unsequencedMessageCase = UnsequencedMessageBranchRule3Case; }
                else if (unsequencedMessageTypeValue.SequenceEqual(UnsequencedMessageBranchRule4Data2)) { unsequencedMessageCase = UnsequencedMessageBranchRule4Case; }
                else if (unsequencedMessageTypeValue.SequenceEqual(UnsequencedMessageBranchRule5Data2)) { unsequencedMessageCase = UnsequencedMessageBranchRule5Case; }
                else if (unsequencedMessageTypeValue.SequenceEqual(UnsequencedMessageBranchRule6Data2)) { unsequencedMessageCase = UnsequencedMessageBranchRule6Case; }
                else if (unsequencedMessageTypeValue.SequenceEqual(UnsequencedMessageBranchRule7Data2)) { unsequencedMessageCase = UnsequencedMessageBranchRule7Case; }
                else if (unsequencedMessageTypeValue.SequenceEqual(UnsequencedMessageBranchRule8Data2)) { unsequencedMessageCase = UnsequencedMessageBranchRule8Case; }
            }
            bool unsequencedMessageSelectedClass =
                (unsequencedMessageCase == UnsequencedMessageBranchRule1Case && this.unsequencedMessage is EnterOrderMessage)
                || (unsequencedMessageCase == UnsequencedMessageBranchRule2Case && this.unsequencedMessage is ReplaceOrderMessage)
                || (unsequencedMessageCase == UnsequencedMessageBranchRule3Case && this.unsequencedMessage is CancelOrderMessage)
                || (unsequencedMessageCase == UnsequencedMessageBranchRule4Case && this.unsequencedMessage is ModifyOrderMessage)
                || (unsequencedMessageCase == UnsequencedMessageBranchRule5Case && this.unsequencedMessage is AccountQueryMessage)
                || (unsequencedMessageCase == UnsequencedMessageBranchRule6Case && this.unsequencedMessage is MassCancelRequestMessage)
                || (unsequencedMessageCase == UnsequencedMessageBranchRule7Case && this.unsequencedMessage is DisableOrderEntryRequestMessage)
                || (unsequencedMessageCase == UnsequencedMessageBranchRule8Case && this.unsequencedMessage is EnableOrderEntryRequestMessage)
                || (unsequencedMessageCase == UnsequencedMessageNoCase && this.unsequencedMessage is UnknownUnsequencedMessage);
            if (!unsequencedMessageSelectedClass)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.UnsequencedMessage), EncodeStatus.RuleMismatch);
                return false;
            }
            if (!((IMessage)this.unsequencedMessage).Write(data, cursor, trailerFollows: true, out cursor, out var memberFailure))
            {
                failure = memberFailure.Within(new FieldView(this, (int)FieldName.UnsequencedMessage), null);
                return false;
            }
            ((IMessage)this.unsequencedMessage).Trailer.Span.CopyTo(data[cursor..]);
            cursor += ((IMessage)this.unsequencedMessage).Trailer.Length;
        }
        if (unsequencedMessageSizeRule0Dependency0Passed is not null)
        {
            const long UnsequencedMessageSizeRule0Data2 = 2;
            global::System.Int128 measure = unsequencedMessageMeasureEnd - unsequencedMessageMeasureStart;
            bool agrees;
            try
            {
                global::System.Int128 unsequencedMessageSizeRule0Dependency0 = unsequencedMessageSizeRule0Dependency0Passed.GetValueOrDefault();
                agrees = checked((unsequencedMessageSizeRule0Dependency0 - ((global::System.Int128)(UnsequencedMessageSizeRule0Data2)))) == measure;
            }
            catch (global::System.OverflowException)
            {
                agrees = false;
            }
            if (!agrees)
            {
                failure = new EncodeFailure(EncodeStatus.RuleMismatch);
                return false;
            }
        }
        if (sizeRule0Dependency0Passed is not null)
        {
            const long UnsequencedDataPacketSizeRule0Data2 = 1;
            global::System.Int128 measure = (trailerFollows ? EncodedByteLength + this.trailer.Length : EncodedByteLength);
            bool agrees;
            try
            {
                global::System.Int128 sizeRule0Dependency0 = sizeRule0Dependency0Passed.GetValueOrDefault();
                agrees = checked((sizeRule0Dependency0 - ((global::System.Int128)(UnsequencedDataPacketSizeRule0Data2)))) == measure;
            }
            catch (global::System.OverflowException)
            {
                agrees = false;
            }
            if (!agrees)
            {
                failure = new EncodeFailure(EncodeStatus.RuleMismatch);
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
        foreach (var failure in this.unsequencedMessage.Validate())
        {
            failures.Add(failure.Within(new FieldView(this, (int)FieldName.UnsequencedMessage), null));
        }
        return failures;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(UnsequencedDataPacket? other)
    {
        if (other is null)
        {
            return false;
        }
        if (!this.unsequencedMessage.Equals(other.unsequencedMessage)) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is UnsequencedDataPacket other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.unsequencedMessage);
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
            if (Has(FieldName.DataValue)) names.Add(nameof(FieldName.DataValue));
            if (Has(FieldName.UnsequencedMessageType)) names.Add(nameof(FieldName.UnsequencedMessageType));
            if (Has(FieldName.UnsequencedMessage)) names.Add(nameof(FieldName.UnsequencedMessage));
            return names.Count == 0 ? "None" : string.Join(", ", names);
        }
    }

    /// <summary>
    ///  Compares this instance with another and identifies the fields that differ.
    /// </summary>
    /// <returns>The fields that differ, or None if there are no differences.</returns>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Changes Diff(UnsequencedDataPacket other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (this.UnsequencedMessageType != other.UnsequencedMessageType) diff = diff.With(FieldName.UnsequencedMessageType);
        if (!this.unsequencedMessage.Equals(other.unsequencedMessage)) diff = diff.With(FieldName.UnsequencedMessage);
        return diff;
    }

    private void CopyField(FieldName field, UnsequencedDataPacket from)
    {
        switch (field)
        {
            case FieldName.UnsequencedMessage:
                this.unsequencedMessage.SetOwner(null);
                this.unsequencedMessage = (IUnsequencedMessage)from.unsequencedMessage.Copy();
                this.unsequencedMessage.SetOwner(this);
                break;
            default:
                return;
        }
        Touch();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"UnsequencedDataPacket {{ UnsequencedMessageType: {(Text.Enum(this.UnsequencedMessageType))}, UnsequencedMessage: {(Text.Of(this.unsequencedMessage.ToString()))} }}{(this.trailer.Length == 0 ? string.Empty : $" Trailer: {(Text.Of(this.trailer.Length))} bytes")}";

    private string DebuggerDisplay => $"[{DataValue}]";

    IFieldOwner? IFieldOwner.Owner
    {
        get => this.owner;
        set => this.owner = value;
    }

    FieldDescriptor[] IFieldOwner.Descriptors => Descriptors;

    void IFieldOwner.Touch() => Touch();

    string IFieldOwner.TextOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => Text.Enum(ClientMessageCode.UnsequencedDataPacket),
        FieldName.UnsequencedMessageType => Text.Enum(this.UnsequencedMessageType),
        FieldName.UnsequencedMessage => Text.Of(this.unsequencedMessage.ToString()),
        _ => string.Empty,
    };

    bool IFieldOwner.IsRecognized(int field) => (FieldName)field switch
    {
        FieldName.UnsequencedMessageType => this.unsequencedMessage is not UnknownUnsequencedMessage,
        FieldName.UnsequencedMessage => this.unsequencedMessage.IsRecognized,
        _ => true,
    };

    bool IFieldOwner.IsPresent(int field) => true;

    bool IFieldOwner.IsParsed(int field) => this.status == ParseStatus.Ok || field < (int?)this.failed;

    bool IFieldOwner.IsReached(int field) => this.modified || this.status is ParseStatus.None or ParseStatus.Ok || field <= (int?)this.failed;

    bool IFieldOwner.HasParseLayout(int field) => false;

    int IFieldOwner.OffsetOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => FieldView.NotInMessage,
        FieldName.UnsequencedMessageType => UnsequencedMessageTypeOffset,
        FieldName.UnsequencedMessage => UnsequencedMessageOffset,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    int IFieldOwner.LengthOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => DataValueLength,
        FieldName.UnsequencedMessageType => UnsequencedMessageTypeLength,
        FieldName.UnsequencedMessage => this.unsequencedMessage.EncodedByteLength,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    IReadOnlyList<IGroupEntry>? IFieldOwner.EntriesOf(int field) => null;

    IMessage? IFieldOwner.MessageOf(int field) => (FieldName)field switch
    {
        FieldName.UnsequencedMessage => this.unsequencedMessage,
        _ => null,
    };

    ulong[] IFieldOwner.ChangedAgainst(IFieldOwner other) => other is UnsequencedDataPacket typed ? Diff(typed).ToWords() : throw new ArgumentException("The other instance is not of the same type.", nameof(other));

    void IFieldOwner.CopyField(int field, IFieldOwner from) => CopyField((FieldName)field, from as UnsequencedDataPacket ?? throw new ArgumentException("The other instance is not of the same type.", nameof(from)));

    ReadOnlyMemory<byte> IFieldOwner.CodeBytes(int field)
    {
        if (field == (int)FieldName.DataValue)
        {
            var bytes = new byte[DataValueLength];
            bytes[0] = unchecked((byte)(ClientMessageCode.UnsequencedDataPacket));
            return bytes;
        }
        return ReadOnlyMemory<byte>.Empty;
    }
}
