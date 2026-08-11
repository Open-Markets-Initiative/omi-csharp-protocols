using System.Text;

namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  Defines the base type for generated fields.
///  Each field has a sealed leaf type that inherits directly from this type.
///  Nullable fields also provide IsPresent.
/// </summary>
public abstract class Field
{
    /// <summary>
    ///  Gets the bytes from which this field was parsed, or null when no parse occurred.
    /// </summary>
    public ReadOnlyMemory<byte>? Raw { get; protected set; }

    /// <summary>
    ///  Gets whether this field was decoded from bytes.
    /// </summary>
    public bool IsDecoded { get; protected set; }

    /// <summary>
    ///  Gets whether this field has a recognized value.
    ///  Enum fields require a declared value. A nullable sentinel is recognized.
    /// </summary>
    public virtual bool IsRecognized => IsDecoded;

    /// <summary>
    ///  Gets whether this field passed structural parsing.
    ///  An unrecognized enum value does not make structural parsing fail.
    /// </summary>
    public virtual bool IsValid => IsDecoded;

    /// <summary>
    ///  Parses bytes and returns false for an unrecognized value.
    /// </summary>
    public virtual bool ParseStrict(ReadOnlySpan<byte> data, ref int offset)
        => Parse(data, ref offset) && IsRecognized;

    /// <summary>
    ///  Parses this field from data at offset.
    ///  Returns true and advances offset on success. Returns false when data is too short.
    /// </summary>
    public abstract bool Parse(ReadOnlySpan<byte> data, ref int offset);

    /// <summary>
    ///  Encodes this field into data at offset.
    ///  Returns the offset after this field.
    /// </summary>
    public abstract int Encode(Span<byte> data, int offset);

    /// <summary>
    ///  Gets the wire-value category of this field.
    ///  Use the leaf type for comparison and dispatch.
    /// </summary>
    public abstract FieldKind Kind { get; }

    /// <summary>
    ///  Gets the declared PascalCase field name.
    /// </summary>
    public abstract string Name { get; }

    /// <summary>
    ///  Resets this field to its default state.
    ///  Leaf types also reset their typed value.
    /// </summary>
    public virtual void Reset() { Raw = null; IsDecoded = false; }

    /// <summary>
    ///  Returns a formatted representation of this field.
    ///  Field output is one line and does not change the indentation depth.
    /// </summary>
    public abstract string ToFormattedString(PrintOptions options = default);
    public abstract void ToFormattedString(StringBuilder builder, PrintOptions options = default);
}
