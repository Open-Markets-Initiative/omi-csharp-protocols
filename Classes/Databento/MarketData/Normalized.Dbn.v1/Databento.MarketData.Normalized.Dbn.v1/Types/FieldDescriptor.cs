namespace Databento.MarketData.Normalized.Dbn.v1;

/// <summary>
///  Describes the name, kind, byte length, and protocol attributes of a field.
/// </summary>
public sealed class FieldDescriptor
{
    internal FieldDescriptor(string name, FieldKind kind, int? length)
    {
        Name = name;
        Kind = kind;
        Length = length;
    }

    /// <summary>
    ///  Gets the name of the field.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///  Gets the kind of value that the field holds.
    /// </summary>
    public FieldKind Kind { get; }

    /// <summary>
    ///  Gets the length of the field in bytes, or <see langword="null"/> if the field does not have a fixed length.
    /// </summary>
    public int? Length { get; }

    /// <summary>
    ///  Gets the decimal places, 0 for a field scaled by a factor, or <see langword="null"/> if the field has none.
    /// </summary>
    public int? Scale { get; internal init; }

    /// <summary>
    ///  Gets the time unit specified by the protocol, or <see langword="null"/> if none is specified.
    /// </summary>
    public string? Unit { get; internal init; }

    /// <summary>
    ///  Gets the time epoch specified by the protocol, or <see langword="null"/> if none is specified.
    /// </summary>
    public string? Epoch { get; internal init; }

    /// <summary>
    ///  Gets the time zone specified by the protocol, or <see langword="null"/> if none is specified.
    /// </summary>
    public string? Timezone { get; internal init; }

    /// <summary>
    ///  Gets the byte order specified by the protocol, or <see langword="null"/> if none is specified.
    /// </summary>
    public string? ByteOrder { get; internal init; }

    /// <summary>
    ///  Gets a value indicating whether the protocol specifies a signed value, or <see langword="null"/> if it does not specify signedness.
    /// </summary>
    public bool? IsSigned { get; internal init; }

    /// <summary>
    ///  Gets the padding of unused bytes specified by the protocol, or <see langword="null"/> if none is specified.
    /// </summary>
    public string? Fill { get; internal init; }

    /// <summary>
    ///  Gets the text justification specified by the protocol, or <see langword="null"/> if none is specified.
    /// </summary>
    public string? Justification { get; internal init; }

    /// <summary>
    ///  Gets how the protocol indicates that the field has no value, or <see langword="null"/> if it does not.
    /// </summary>
    public string? Nullable { get; internal init; }

    /// <summary>
    ///  Gets the encoded value that means no value, or <see langword="null"/> if the field always has a value.
    /// </summary>
    public object? NullValue { get; internal init; }

    /// <summary>
    ///  Gets the name, bit offset, and bit width of each named bit range, or <see langword="null"/> if the field has none.
    /// </summary>
    public IReadOnlyList<(string Name, int Offset, int Width)>? Bits { get; internal init; }
}
