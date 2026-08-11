using System.Text;

namespace Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
/// Defines a parsed message.
/// </summary>
public interface IMessage
{
    /// <summary>Gets the message type, or '\0' when no type is defined.</summary>
    char Type { get; }

    /// <summary>
    /// Gets the bytes from the last parse attempt, or null when no parse occurred.
    /// A failed parse can contain an empty or partial byte range.
    /// </summary>
    ReadOnlyMemory<byte>? Raw { get; }

    /// <summary>Gets the fixed byte length of this message.</summary>
    int ByteLength { get; }

    /// <summary>Gets the timestamp value, or null when this message has no timestamp field.</summary>
    DateTime? Timestamp { get; }

    /// <summary>Gets whether all fields decoded without a structural error.</summary>
    bool IsDecoded { get; }

    /// <summary>Gets whether all decoded fields have recognized values.</summary>
    bool IsRecognized { get; }

    /// <summary>Gets whether this message is decoded and recognized.</summary>
    bool IsValid { get; }

    /// <summary>Gets the first field that failed to decode, or null.</summary>
    Field? FailedAt { get; }

    /// <summary>Encodes this message at offset and returns the next offset.</summary>
    int Encode(Span<byte> data, int offset);

    /// <summary>Encodes this message at offset zero and returns the next offset.</summary>
    int Encode(Span<byte> data);

    /// <summary>Resets all fields and clears the raw data.</summary>
    void Clear();

    /// <summary>Parses data into this message. The parse reuses the existing field objects.</summary>
    void ParseFrom(ReadOnlySpan<byte> data);

    /// <summary>Returns true when this message is decoded and its raw bytes equal data.</summary>
    bool Equals(ReadOnlySpan<byte> data);

    /// <summary>Gets fields in declaration order.</summary>
    IEnumerable<Field> Fields { get; }

    /// <summary>Gets a field by its declared PascalCase name.</summary>
    bool TryGetField(string name, out Field field);

    /// <summary>Gets fields that differ from another message of the same type.</summary>
    IEnumerable<Field> DiffFields(IMessage other);

    /// <summary>Returns a formatted representation of this message.</summary>
    string ToFormattedString(PrintOptions options = default);
    /// <summary>Appends a formatted representation of this message.</summary>
    void ToFormattedString(StringBuilder builder, PrintOptions options = default);

    /// <summary>
    /// Sets the packet that produced this message.
    /// Row classes use the default no-op implementation.
    /// </summary>
    void SetPacket(Packet packet) { }
}
