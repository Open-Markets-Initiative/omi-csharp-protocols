using System.Buffers.Binary;
using System.Diagnostics;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  The SoupBinTCP client must send a Login Request Packet immediately upon establishing a new TCP/IP socket connection to the server
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class LoginRequestPacket : IClientMessage, IEquatable<LoginRequestPacket>, IFieldOwner
{
    internal const int UsernameOffset = 0;

    internal const int UsernameLength = 6;

    /// <summary>
    ///  The byte that fills the unused bytes of Username.
    /// </summary>
    public const byte UsernameFill = (byte)' ';

    internal const int PasswordOffset = UsernameOffset + UsernameLength;

    internal const int PasswordLength = 10;

    /// <summary>
    ///  The byte that fills the unused bytes of Password.
    /// </summary>
    public const byte PasswordFill = (byte)' ';

    internal const int RequestedSessionOffset = PasswordOffset + PasswordLength;

    internal const int RequestedSessionLength = 10;

    /// <summary>
    ///  The byte that fills the unused bytes of Requested Session.
    /// </summary>
    public const byte RequestedSessionFill = (byte)' ';

    internal const int RequestedSequenceNumberOffset = RequestedSessionOffset + RequestedSessionLength;

    internal const int RequestedSequenceNumberLength = 20;

    /// <summary>
    ///  The byte that fills the unused bytes of Requested Sequence Number.
    /// </summary>
    public const byte RequestedSequenceNumberFill = (byte)' ';

    /// <summary>
    ///  The encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public const int MessageByteLength = UsernameLength + PasswordLength + RequestedSessionLength + RequestedSequenceNumberLength;

    internal string username = string.Empty;

    /// <summary>
    ///  Session username
    /// </summary>
    /// <remarks>
    ///  The field is 6 bytes long. The field holds ASCII text. Text that is shorter than the field is padded with spaces at the end. Text longer than the field is cut to fit when the message is encoded. A character that the field cannot hold is replaced with a question mark when the message is encoded.
    /// </remarks>
    public string Username
    {
        get => this.username;
        set
        {
            this.username = value ?? string.Empty;
            Touch();
        }
    }

    internal string password = string.Empty;

    /// <summary>
    ///  Login password
    /// </summary>
    /// <remarks>
    ///  The field is 10 bytes long. The field holds ASCII text. Text that is shorter than the field is padded with spaces at the end. Text longer than the field is cut to fit when the message is encoded. A character that the field cannot hold is replaced with a question mark when the message is encoded.
    /// </remarks>
    public string Password
    {
        get => this.password;
        set
        {
            this.password = value ?? string.Empty;
            Touch();
        }
    }

    internal string requestedSession = string.Empty;

    /// <summary>
    ///  Specifies the session the client would like to log into, or all blanks to log into the currently active session.
    /// </summary>
    /// <remarks>
    ///  The field is 10 bytes long. The field holds ASCII text. Text that is shorter than the field is padded with spaces at the start. Text longer than the field is cut to fit when the message is encoded. A character that the field cannot hold is replaced with a question mark when the message is encoded.
    /// </remarks>
    public string RequestedSession
    {
        get => this.requestedSession;
        set
        {
            this.requestedSession = value ?? string.Empty;
            Touch();
        }
    }

    internal string requestedSequenceNumber = string.Empty;

    /// <summary>
    ///  Specifies the next sequence number in ASCII the client wants to receive upon connection, or 0 to start receiving the most recently generated message.
    /// </summary>
    /// <remarks>
    ///  The field is 20 bytes long. The field holds ASCII text. Text that is shorter than the field is padded with spaces at the start. Text longer than the field is cut to fit when the message is encoded. A character that the field cannot hold is replaced with a question mark when the message is encoded.
    /// </remarks>
    public string RequestedSequenceNumber
    {
        get => this.requestedSequenceNumber;
        set
        {
            this.requestedSequenceNumber = value ?? string.Empty;
            Touch();
        }
    }

    /// <summary>
    ///  Gets the message type.
    /// </summary>
    public ClientMessageCode DataValue => ClientMessageCode.LoginRequestPacket;

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

    IMessage IMessage.Copy() => new LoginRequestPacket(this);

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
        ///  Session username
        /// </summary>
        Username,
        /// <summary>
        ///  Login password
        /// </summary>
        Password,
        /// <summary>
        ///  Specifies the session the client would like to log into, or all blanks to log into the currently active session.
        /// </summary>
        RequestedSession,
        /// <summary>
        ///  Specifies the next sequence number in ASCII the client wants to receive upon connection, or 0 to start receiving the most recently generated message.
        /// </summary>
        RequestedSequenceNumber,
    }

    /// <summary>
    ///  Provides access to the fields of one instance, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public readonly struct FieldSet : IEnumerable<FieldView>
    {
        private readonly LoginRequestPacket owner;

        internal FieldSet(LoginRequestPacket owner)
        {
            this.owner = owner;
        }

        /// <summary>
        ///  The message type.
        /// </summary>
        public FieldView DataValue => this[FieldName.DataValue];

        /// <summary>
        ///  Session username
        /// </summary>
        public FieldView Username => this[FieldName.Username];

        /// <summary>
        ///  Login password
        /// </summary>
        public FieldView Password => this[FieldName.Password];

        /// <summary>
        ///  Specifies the session the client would like to log into, or all blanks to log into the currently active session.
        /// </summary>
        public FieldView RequestedSession => this[FieldName.RequestedSession];

        /// <summary>
        ///  Specifies the next sequence number in ASCII the client wants to receive upon connection, or 0 to start receiving the most recently generated message.
        /// </summary>
        public FieldView RequestedSequenceNumber => this[FieldName.RequestedSequenceNumber];

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
        new(nameof(FieldName.Username), FieldKind.String, UsernameLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.Password), FieldKind.String, PasswordLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.RequestedSession), FieldKind.String, RequestedSessionLength) { Fill = "Spaces", Justification = "Right" },
        new(nameof(FieldName.RequestedSequenceNumber), FieldKind.String, RequestedSequenceNumberLength) { Fill = "Spaces", Justification = "Right" },
    ];

    /// <summary>
    ///  Gets the fields in wire order, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public FieldSet Fields => new(this);

    IEnumerable<FieldView> IMessage.Fields => new FieldSet(this);

    /// <summary>
    ///  Initializes a new instance of the <see cref="LoginRequestPacket"/> class with default values.
    /// </summary>
    public LoginRequestPacket()
    {
        Clear();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="LoginRequestPacket"/> class with the values, bytes, and parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public LoginRequestPacket(LoginRequestPacket other) : this()
    {
        CopyFrom(other);
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(LoginRequestPacket other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.username = other.username;
        this.password = other.password;
        this.requestedSession = other.requestedSession;
        this.requestedSequenceNumber = other.requestedSequenceNumber;
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
        this.username = string.Empty;
        this.password = string.Empty;
        this.requestedSession = string.Empty;
        this.requestedSequenceNumber = string.Empty;
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
    public static LoginRequestPacket Parse(ReadOnlySpan<byte> data)
    {
        var instance = new LoginRequestPacket();
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
    public static LoginRequestPacket Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        var instance = new LoginRequestPacket();
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
        if (Truncated(data, start, cursor, UsernameLength, FieldName.Username)) return;
        this.username = TextEncoding.Ascii.ReadLeftJustified(data.Slice(cursor, UsernameLength), UsernameFill);
        cursor += UsernameLength;
        if (Truncated(data, start, cursor, PasswordLength, FieldName.Password)) return;
        this.password = TextEncoding.Ascii.ReadLeftJustified(data.Slice(cursor, PasswordLength), PasswordFill);
        cursor += PasswordLength;
        if (Truncated(data, start, cursor, RequestedSessionLength, FieldName.RequestedSession)) return;
        this.requestedSession = TextEncoding.Ascii.ReadRightJustified(data.Slice(cursor, RequestedSessionLength), RequestedSessionFill);
        cursor += RequestedSessionLength;
        if (Truncated(data, start, cursor, RequestedSequenceNumberLength, FieldName.RequestedSequenceNumber)) return;
        this.requestedSequenceNumber = TextEncoding.Ascii.ReadRightJustified(data.Slice(cursor, RequestedSequenceNumberLength), RequestedSequenceNumberFill);
        cursor += RequestedSequenceNumberLength;
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
    /// <exception cref="ArgumentException">The span is too small for the encoded bytes.</exception>
    /// <remarks>
    ///  The bytes are those of the fields of the message, from its first field to its last.
    /// </remarks>
    public int Encode(Span<byte> data) => Encode(data, 0);

    /// <summary>
    ///  Encodes this instance into a new array.
    /// </summary>
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
        TextEncoding.Ascii.WriteLeftJustified(data.Slice(cursor, UsernameLength), TextEncoding.Ascii.Fit(this.username, UsernameLength), UsernameFill);
        cursor += UsernameLength;
        TextEncoding.Ascii.WriteLeftJustified(data.Slice(cursor, PasswordLength), TextEncoding.Ascii.Fit(this.password, PasswordLength), PasswordFill);
        cursor += PasswordLength;
        TextEncoding.Ascii.WriteRightJustified(data.Slice(cursor, RequestedSessionLength), TextEncoding.Ascii.Fit(this.requestedSession, RequestedSessionLength), RequestedSessionFill);
        cursor += RequestedSessionLength;
        TextEncoding.Ascii.WriteRightJustified(data.Slice(cursor, RequestedSequenceNumberLength), TextEncoding.Ascii.Fit(this.requestedSequenceNumber, RequestedSequenceNumberLength), RequestedSequenceNumberFill);
        cursor += RequestedSequenceNumberLength;
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
        if (!global::System.Text.Ascii.IsValid(this.username))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.Username), ValidationStatus.NonAsciiText));
        }
        if (!global::System.Text.Ascii.IsValid(this.password))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.Password), ValidationStatus.NonAsciiText));
        }
        if (!global::System.Text.Ascii.IsValid(this.requestedSession))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.RequestedSession), ValidationStatus.NonAsciiText));
        }
        if (!global::System.Text.Ascii.IsValid(this.requestedSequenceNumber))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.RequestedSequenceNumber), ValidationStatus.NonAsciiText));
        }
        return failures;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(LoginRequestPacket? other)
    {
        if (other is null)
        {
            return false;
        }
        if (this.username != other.username) return false;
        if (this.password != other.password) return false;
        if (this.requestedSession != other.requestedSession) return false;
        if (this.requestedSequenceNumber != other.requestedSequenceNumber) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is LoginRequestPacket other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.username);
        hash.Add(this.password);
        hash.Add(this.requestedSession);
        hash.Add(this.requestedSequenceNumber);
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
        private const int FieldCount = 5;

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
            if (Has(FieldName.Username)) names.Add(nameof(FieldName.Username));
            if (Has(FieldName.Password)) names.Add(nameof(FieldName.Password));
            if (Has(FieldName.RequestedSession)) names.Add(nameof(FieldName.RequestedSession));
            if (Has(FieldName.RequestedSequenceNumber)) names.Add(nameof(FieldName.RequestedSequenceNumber));
            return names.Count == 0 ? "None" : string.Join(", ", names);
        }
    }

    /// <summary>
    ///  Compares this instance with another and identifies the fields that differ.
    /// </summary>
    /// <returns>The fields that differ, or None if there are no differences.</returns>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Changes Diff(LoginRequestPacket other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (this.username != other.username) diff = diff.With(FieldName.Username);
        if (this.password != other.password) diff = diff.With(FieldName.Password);
        if (this.requestedSession != other.requestedSession) diff = diff.With(FieldName.RequestedSession);
        if (this.requestedSequenceNumber != other.requestedSequenceNumber) diff = diff.With(FieldName.RequestedSequenceNumber);
        return diff;
    }

    private void CopyField(FieldName field, LoginRequestPacket from)
    {
        switch (field)
        {
            case FieldName.Username:
                this.username = from.username;
                break;
            case FieldName.Password:
                this.password = from.password;
                break;
            case FieldName.RequestedSession:
                this.requestedSession = from.requestedSession;
                break;
            case FieldName.RequestedSequenceNumber:
                this.requestedSequenceNumber = from.requestedSequenceNumber;
                break;
            default:
                return;
        }
        Touch();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"LoginRequestPacket {{ Username: {(Text.Of(this.username))}, Password: {(Text.Of(this.password))}, RequestedSession: {(Text.Of(this.requestedSession))}, RequestedSequenceNumber: {(Text.Of(this.requestedSequenceNumber))} }}{(this.trailer.Length == 0 ? string.Empty : $" Trailer: {(Text.Of(this.trailer.Length))} bytes")}";

    private string DebuggerDisplay => $"[{DataValue}] [{(Text.Of(this.username))}] [{(Text.Of(this.password))}] [{(Text.Of(this.requestedSession))}]";

    IFieldOwner? IFieldOwner.Owner
    {
        get => this.owner;
        set => this.owner = value;
    }

    FieldDescriptor[] IFieldOwner.Descriptors => Descriptors;

    void IFieldOwner.Touch() => Touch();

    string IFieldOwner.TextOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => Text.Enum(ClientMessageCode.LoginRequestPacket),
        FieldName.Username => Text.Of(this.username),
        FieldName.Password => Text.Of(this.password),
        FieldName.RequestedSession => Text.Of(this.requestedSession),
        FieldName.RequestedSequenceNumber => Text.Of(this.requestedSequenceNumber),
        _ => string.Empty,
    };

    bool IFieldOwner.IsRecognized(int field) => true;

    bool IFieldOwner.IsPresent(int field) => true;

    bool IFieldOwner.IsParsed(int field) => this.status == ParseStatus.Ok || field < (int?)this.failed;

    bool IFieldOwner.IsReached(int field) => this.modified || this.status is ParseStatus.None or ParseStatus.Ok || field <= (int?)this.failed;

    bool IFieldOwner.HasParseLayout(int field) => false;

    int IFieldOwner.OffsetOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => FieldView.NotInMessage,
        FieldName.Username => UsernameOffset,
        FieldName.Password => PasswordOffset,
        FieldName.RequestedSession => RequestedSessionOffset,
        FieldName.RequestedSequenceNumber => RequestedSequenceNumberOffset,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    int IFieldOwner.LengthOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => DataValueLength,
        FieldName.Username => UsernameLength,
        FieldName.Password => PasswordLength,
        FieldName.RequestedSession => RequestedSessionLength,
        FieldName.RequestedSequenceNumber => RequestedSequenceNumberLength,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    IReadOnlyList<IGroupEntry>? IFieldOwner.EntriesOf(int field) => null;

    IMessage? IFieldOwner.MessageOf(int field) => null;

    ulong[] IFieldOwner.ChangedAgainst(IFieldOwner other) => other is LoginRequestPacket typed ? Diff(typed).ToWords() : throw new ArgumentException("The other instance is not of the same type.", nameof(other));

    void IFieldOwner.CopyField(int field, IFieldOwner from) => CopyField((FieldName)field, from as LoginRequestPacket ?? throw new ArgumentException("The other instance is not of the same type.", nameof(from)));

    ReadOnlyMemory<byte> IFieldOwner.CodeBytes(int field)
    {
        if (field == (int)FieldName.DataValue)
        {
            var bytes = new byte[DataValueLength];
            bytes[0] = unchecked((byte)(ClientMessageCode.LoginRequestPacket));
            return bytes;
        }
        return ReadOnlyMemory<byte>.Empty;
    }
}
