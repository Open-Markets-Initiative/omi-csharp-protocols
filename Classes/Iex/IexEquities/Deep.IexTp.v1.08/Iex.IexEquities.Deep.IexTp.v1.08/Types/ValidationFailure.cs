namespace Iex.IexEquities.Deep.IexTp.v1_08;

/// <summary>
///  Describes a field whose value the protocol specification does not allow.
/// </summary>
public readonly struct ValidationFailure
{
    internal ValidationFailure(FieldView field, ValidationStatus reason)
        : this(field, reason, null, null, null)
    {
    }

    private ValidationFailure(FieldView field, ValidationStatus reason, int? messageIndex, FieldView? parent, int? entryIndex)
    {
        Descriptor = field.Descriptor;
        Field = field;
        Reason = reason;
        MessageIndex = messageIndex;
        Parent = parent;
        EntryIndex = entryIndex;
    }

    /// <summary>
    ///  Gets the descriptor of the field.
    /// </summary>
    public FieldDescriptor Descriptor { get; }

    /// <summary>
    ///  Gets the field whose value is not allowed.
    /// </summary>
    public FieldView Field { get; }

    /// <summary>
    ///  Gets why the value is not allowed.
    /// </summary>
    public ValidationStatus Reason { get; }

    /// <summary>
    ///  Gets the index of the message that contains the field, or <see langword="null"/> if no message of a packet contains it.
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

    internal ValidationFailure InMessage(int index) => new(Field, Reason, index, Parent, EntryIndex);

    internal ValidationFailure Within(FieldView parent, int? entryIndex)
        => Parent is null ? new(Field, Reason, MessageIndex, parent, entryIndex) : this;
}
