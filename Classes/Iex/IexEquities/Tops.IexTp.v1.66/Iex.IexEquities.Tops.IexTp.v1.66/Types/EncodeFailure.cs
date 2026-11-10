namespace Iex.IexEquities.Tops.IexTp.v1_66;

/// <summary>
///  Describes why encoding failed, which is the first value that cannot be encoded, or else a span too small.
/// </summary>
public readonly struct EncodeFailure
{
    internal EncodeFailure(FieldView field, EncodeStatus reason)
        : this(field.Descriptor, field, reason, null, null, null)
    {
    }

    internal EncodeFailure(FieldDescriptor descriptor, EncodeStatus reason, int? messageIndex)
        : this(descriptor, null, reason, messageIndex, null, null)
    {
    }

    internal EncodeFailure(EncodeStatus reason)
        : this(null, null, reason, null, null, null)
    {
    }

    private EncodeFailure(FieldDescriptor? descriptor, FieldView? field, EncodeStatus reason, int? messageIndex, FieldView? parent, int? entryIndex)
    {
        Descriptor = descriptor;
        Field = field;
        Reason = reason;
        MessageIndex = messageIndex;
        Parent = parent;
        EntryIndex = entryIndex;
    }

    /// <summary>
    ///  Gets the descriptor of the field whose value cannot be encoded, or <see langword="null"/> if the failure has no field.
    /// </summary>
    public FieldDescriptor? Descriptor { get; }

    /// <summary>
    ///  Gets the field whose value cannot be encoded, or <see langword="null"/> if the failure has no field.
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if every value fits or if the value that does not fit is the message type. It is also <see langword="null"/> if the value is the message count or a length, which the packet computes.
    /// </remarks>
    public FieldView? Field { get; }

    /// <summary>
    ///  Gets the reason that encoding failed, or <see cref="EncodeStatus.Ok"/> if every value fits and the span is large enough.
    /// </summary>
    public EncodeStatus Reason { get; }

    /// <summary>
    ///  Gets the index of the message that contains the field or whose length does not fit, or <see langword="null"/> if none.
    /// </summary>
    public int? MessageIndex { get; }

    /// <summary>
    ///  Gets the innermost group or field holding the entry or nested message of the failing field, or <see langword="null"/> if none.
    /// </summary>
    public FieldView? Parent { get; }

    /// <summary>
    ///  Gets the index of the entry in <see cref="Parent"/> that contains the field, or <see langword="null"/> if no group entry contains it.
    /// </summary>
    public int? EntryIndex { get; }

    internal EncodeFailure InMessage(int index) => new(Descriptor, Field, Reason, index, Parent, EntryIndex);

    internal EncodeFailure Within(FieldView parent, int? entryIndex)
        => Parent is null ? new(Descriptor, Field, Reason, MessageIndex, parent, entryIndex) : this;
}
