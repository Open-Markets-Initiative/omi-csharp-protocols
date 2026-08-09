using System.Text;

namespace Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
///  Abstract base for all field types. Each field is represented by its own
///  sealed leaf class that inherits directly from Field.
/// </summary>
public abstract class Field
{
    /// <summary>
    ///  The raw binary data from which this field was parsed, or null if set directly.
    /// </summary>
    public ReadOnlyMemory<byte>? Raw { get; protected set; }

    /// <summary>
    ///  True if this field has been populated by a Parse call; false if default or set directly.
    /// </summary>
    public bool IsDecoded { get; protected set; }

    /// <summary>
    ///  True if this field's value is one we recognize — declared enum member, non-sentinel
    ///  nullable, or for base types simply true if IsDecoded.
    ///  Enum fields override to return Enum.IsDefined(Value).
    ///  Nullable fields do NOT override — null is recognized (it is a declared sentinel).
    /// </summary>
    public virtual bool IsRecognized => IsDecoded;

    /// <summary>
    ///  True if this field was structurally parsed (IsDecoded). Does not require IsRecognized.
    ///  Used by the parsing constructor to fast-fail on truncated data without stopping on
    ///  unrecognized enum values — semantic failures do not break structural parse.
    /// </summary>
    public virtual bool IsValid => IsDecoded;

    /// <summary>
    ///  Strict parse: reads bytes AND checks IsRecognized. Stub — not invoked anywhere.
    ///  Future use: strict-mode parse loop that fast-fails on unrecognized values.
    /// </summary>
    public virtual bool ParseStrict(ReadOnlySpan<byte> data, ref int offset)
        => Parse(data, ref offset) && IsRecognized;

    /// <summary>
    ///  Parses this field's value from the given data span starting at offset.
    ///  Returns true and advances offset by ByteLength on success; returns false if data is too short.
    /// </summary>
    public abstract bool Parse(ReadOnlySpan<byte> data, ref int offset);

    /// <summary>
    ///  Encodes this field's value into the given data span starting at offset,
    ///  and returns the new offset after writing this field's bytes.
    /// </summary>
    public abstract int Encode(Span<byte> data, int offset);

    /// <summary>
    ///  The wire/value category of this field. Used for diagnostics and reporting;
    ///  not for comparison or dispatch — leaves are the load-bearing types.
    /// </summary>
    public abstract FieldKind Kind { get; }

    /// <summary>
    ///  The PascalCase name of this field as declared on the message class.
    /// </summary>
    public abstract string Name { get; }

    /// <summary>
    ///  Resets this field to its default state, clearing Raw, IsDecoded, and resetting Value to default.
    ///  Override in leaf classes to also reset the typed Value property.
    /// </summary>
    public virtual void Reset() { Raw = null; IsDecoded = false; }

    /// <summary>
    ///  Returns a human-readable string representation of this field's value.
    ///  The <paramref name="options"/> parameter is accepted for signature compatibility with message-level
    ///  formatting; field values are single-line and do not consume indent depth.
    /// </summary>
    public abstract string ToFormattedString(PrintOptions options = default);
    public abstract void ToFormattedString(StringBuilder builder, PrintOptions options = default);
}
