namespace Iex.IexEquities.Tops.IexTp.v1_66;

/// <summary>
///  Provides the details of one field of one instance.
/// </summary>
public readonly struct FieldView
{
    /// <summary>
    ///  The offset reported for a field that the most recent parse did not reach.
    /// </summary>
    /// <remarks>
    ///  It is also the offset of a field that has no bytes because a value cannot be encoded.
    /// </remarks>
    public const int NotReached = -1;

    /// <summary>
    ///  The offset reported for a field whose bytes are outside the bytes of the message.
    /// </summary>
    public const int NotInMessage = -2;

    private readonly IFieldOwner owner;
    private readonly int field;

    internal FieldView(IFieldOwner owner, int field)
    {
        this.owner = owner;
        this.field = field;
    }

    /// <summary>
    ///  Gets the descriptor of the field.
    /// </summary>
    public FieldDescriptor Descriptor => this.owner.Descriptors[this.field];

    /// <summary>
    ///  Gets the name of the field.
    /// </summary>
    public string Name => Descriptor.Name;

    /// <summary>
    ///  Gets the kind of value that the field holds.
    /// </summary>
    public FieldKind Kind => Descriptor.Kind;

    /// <summary>
    ///  Gets the decimal places, 0 for a field scaled by a factor, or <see langword="null"/> if the field has none.
    /// </summary>
    public int? Scale => Descriptor.Scale;

    /// <summary>
    ///  Gets the time unit specified by the protocol, or <see langword="null"/> if none is specified.
    /// </summary>
    public string? Unit => Descriptor.Unit;

    /// <summary>
    ///  Gets the time epoch specified by the protocol, or <see langword="null"/> if none is specified.
    /// </summary>
    public string? Epoch => Descriptor.Epoch;

    /// <summary>
    ///  Gets the time zone specified by the protocol, or <see langword="null"/> if none is specified.
    /// </summary>
    public string? Timezone => Descriptor.Timezone;

    /// <summary>
    ///  Gets the byte order specified by the protocol, or <see langword="null"/> if none is specified.
    /// </summary>
    public string? ByteOrder => Descriptor.ByteOrder;

    /// <summary>
    ///  Gets a value indicating whether the protocol specifies a signed value, or <see langword="null"/> if it does not specify signedness.
    /// </summary>
    public bool? IsSigned => Descriptor.IsSigned;

    /// <summary>
    ///  Gets the padding of unused bytes specified by the protocol, or <see langword="null"/> if none is specified.
    /// </summary>
    public string? Fill => Descriptor.Fill;

    /// <summary>
    ///  Gets the text justification specified by the protocol, or <see langword="null"/> if none is specified.
    /// </summary>
    public string? Justification => Descriptor.Justification;

    /// <summary>
    ///  Gets how the protocol indicates that the field has no value, or <see langword="null"/> if it does not.
    /// </summary>
    public string? Nullable => Descriptor.Nullable;

    /// <summary>
    ///  Gets the encoded value that means no value, or <see langword="null"/> if the field always has a value.
    /// </summary>
    public object? NullValue => Descriptor.NullValue;

    /// <summary>
    ///  Gets the name, bit offset, and bit width of each named bit range, or <see langword="null"/> if the field has none.
    /// </summary>
    public IReadOnlyList<(string Name, int Offset, int Width)>? Bits => Descriptor.Bits;

    /// <summary>
    ///  Gets a value indicating whether the most recent parse read the field.
    /// </summary>
    public bool IsParsed => this.owner.IsParsed(this.field);

    /// <summary>
    ///  Gets the byte offset of the field.
    /// </summary>
    /// <remarks>
    ///  The value is <see cref="NotInMessage"/> if the bytes of the field are outside the message. It is <see cref="NotReached"/> if the most recent parse stopped before the field. It is also <see cref="NotReached"/> if the field is or follows a variable-length field and a value cannot be encoded.
    /// </remarks>
    public int Offset => IsReached ? this.owner.OffsetOf(this.field) : NotReached;

    /// <summary>
    ///  Gets the length of the field in bytes.
    /// </summary>
    /// <remarks>
    ///  The value is 0 if the most recent parse stopped before the field. It is also 0 if the field is or follows a variable-length field and a value cannot be encoded.
    /// </remarks>
    public int Length => IsReached ? this.owner.LengthOf(this.field) : 0;

    /// <summary>
    ///  Gets the bytes of the field.
    /// </summary>
    /// <remarks>
    ///  The value is empty for a field inside the message that the most recent parse did not reach. It is also empty for a field inside the message when a value cannot be encoded.
    /// </remarks>
    public ReadOnlyMemory<byte> Raw
    {
        get
        {
            if (this.owner.OffsetOf(this.field) == NotInMessage)
            {
                return this.owner.CodeBytes(this.field);
            }
            if (!IsReached || !this.owner.TryRaw(out var raw))
            {
                return ReadOnlyMemory<byte>.Empty;
            }
            var offset = this.owner.OffsetOf(this.field);
            return raw.Slice(offset, Math.Min(this.owner.LengthOf(this.field), raw.Length - offset));
        }
    }

    /// <summary>
    ///  Gets the value of the field as culture-invariant text.
    /// </summary>
    public string Text => Kind == FieldKind.Padding ? global::Iex.IexEquities.Tops.IexTp.v1_66.Text.Bytes(Raw) : this.owner.TextOf(this.field);

    /// <summary>
    ///  Gets the entries of a group field, or <see langword="null"/> if the field is not a group.
    /// </summary>
    public IReadOnlyList<IGroupEntry>? Entries => this.owner.EntriesOf(this.field);

    /// <summary>
    ///  Gets the nested message of the field, or <see langword="null"/> if the field has none.
    /// </summary>
    public IMessage? Message => this.owner.MessageOf(this.field);

    /// <summary>
    ///  Gets the nested messages of a repeated field, or <see langword="null"/> if the field does not repeat messages.
    /// </summary>
    public IReadOnlyList<IMessage>? Messages => this.owner.MessagesOf(this.field);

    /// <summary>
    ///  Gets a value indicating whether the field holds a value that the protocol defines.
    /// </summary>
    public bool IsRecognized => this.owner.IsRecognized(this.field);

    /// <summary>
    ///  Gets a value indicating whether the field has a value.
    /// </summary>
    public bool IsPresent => this.owner.IsPresent(this.field);

    /// <summary>
    ///  Copies the value of the same field from another instance.
    /// </summary>
    /// <exception cref="ArgumentException">The other field is not the same field of an instance of the same type.</exception>
    public void CopyFrom(FieldView other)
    {
        if (other.field != this.field || other.owner is null || other.owner.GetType() != this.owner.GetType())
        {
            throw new ArgumentException("The other field is not the same field of an instance of the same type.", nameof(other));
        }
        this.owner.CopyField(this.field, other.owner);
    }

    /// <summary>
    ///  Returns the value of the field as culture-invariant text.
    /// </summary>
    public override string ToString() => Text;

    private bool IsReached
    {
        get
        {
            if (this.owner.OffsetOf(this.field) == NotInMessage)
            {
                return true;
            }
            if (!this.owner.IsReached(this.field))
            {
                return false;
            }
            return !this.owner.HasParseLayout(this.field) || this.owner.TryRaw(out _);
        }
    }
}
