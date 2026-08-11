using System.Text;

namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
/// Defines a parsed transport header.
/// Headers carry packet framing fields and do not implement IMessage.
/// </summary>
public interface IHeader
{
    /// <summary>
    /// Gets the bytes from the last parse attempt, or null when no parse occurred.
    /// A failed parse can contain an empty or partial byte range.
    /// </summary>
    ReadOnlyMemory<byte>? Raw { get; }

    /// <summary>Gets the fixed byte length of this header.</summary>
    int ByteLength { get; }

    /// <summary>Gets whether all fields decoded without a structural error.</summary>
    bool IsDecoded { get; }

    /// <summary>Gets whether all decoded fields have recognized values.</summary>
    bool IsRecognized { get; }

    /// <summary>Gets whether this header is decoded and recognized.</summary>
    bool IsValid { get; }

    /// <summary>Gets the first field that failed to decode, or null.</summary>
    Field? FailedAt { get; }

    /// <summary>Encodes this header at offset and returns the next offset.</summary>
    int Encode(Span<byte> data, int offset);

    /// <summary>Encodes this header at offset zero and returns the next offset.</summary>
    int Encode(Span<byte> data);

    /// <summary>Resets all fields and clears the raw data.</summary>
    void Clear();

    /// <summary>Parses data into this header. The parse reuses the existing field objects.</summary>
    void ParseFrom(ReadOnlySpan<byte> data);

    /// <summary>Returns true when this header is decoded and its raw bytes equal data.</summary>
    bool Equals(ReadOnlySpan<byte> data);

    /// <summary>Gets fields in declaration order.</summary>
    IEnumerable<Field> Fields { get; }

    /// <summary>Gets a field by its declared PascalCase name.</summary>
    bool TryGetField(string name, out Field field);

    /// <summary>Gets fields that differ from another header of the same type.</summary>
    IEnumerable<Field> DiffFields(IHeader other);

    /// <summary>Returns a formatted representation of this header.</summary>
    string ToFormattedString(PrintOptions options = default);

    /// <summary>Appends a formatted representation of this header.</summary>
    void ToFormattedString(StringBuilder builder, PrintOptions options = default);
}
