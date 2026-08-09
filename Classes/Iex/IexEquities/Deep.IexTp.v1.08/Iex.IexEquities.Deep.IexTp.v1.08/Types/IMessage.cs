using System.Text;

namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  Common interface implemented by all generated message classes.
/// </summary>
public interface IMessage
{
    /// <summary>The message type character constant, or '\0' if no constant is defined.</summary>
    char Type { get; }

    /// <summary>The raw binary data slice captured during the last parse attempt, or null if no parse has been attempted.
    ///  States: (a) no parse attempted — Raw is null, IsDecoded is false; (b) parse failed (insufficient bytes) — Raw is an empty or partial slice, IsDecoded is false;
    ///  (c) parse succeeded — Raw is the full message slice, IsDecoded is true; (d) cleared — Raw is null, IsDecoded is false.</summary>
    ReadOnlyMemory<byte>? Raw { get; }

    /// <summary>The total fixed byte length of this message.</summary>
    int ByteLength { get; }

    /// <summary>The timestamp field value if the message has a Unix nanosecond timestamp field, otherwise null.</summary>
    DateTime? Timestamp { get; }

    /// <summary>True if Parse() completed without structural error (every field decoded). False if default-constructed, cleared, or parse failed.</summary>
    bool IsDecoded { get; }

    /// <summary>True if every field's IsRecognized is true. Non-enum fields inherit IsRecognized => IsDecoded, so they are false until parsed. Enum fields additionally require the value to be a declared enum member.</summary>
    bool IsRecognized { get; }

    /// <summary>Convenience: true if IsDecoded and IsRecognized are both true.</summary>
    bool IsValid { get; }

    /// <summary>The first field that failed structural parsing (insufficient bytes), or null if the last parse succeeded or no parse has been attempted.</summary>
    Field? FailedAt { get; }

    /// <summary>Encodes this message into the supplied span starting at offset and returns the offset after the last written byte.</summary>
    int Encode(Span<byte> data, int offset);

    /// <summary>Encodes this message into the supplied span starting at offset 0 and returns the offset after the last written byte.</summary>
    int Encode(Span<byte> data);

    /// <summary>Resets all fields to their default values and clears the raw backing store.</summary>
    void Clear();

    /// <summary>Parses binary data into this message instance in place, reusing existing field objects.</summary>
    void ParseFrom(ReadOnlySpan<byte> data);

    /// <summary>Returns true if the message was fully decoded (IsDecoded is true), Raw is present, and its bytes are equal to data.
    ///  Returns false if IsDecoded is false, even if Raw happens to contain bytes that match.</summary>
    bool Equals(ReadOnlySpan<byte> data);

    /// <summary>Enumerates all fields in declaration order. Each element is a <see cref="Field"/> leaf instance.</summary>
    IEnumerable<Field> Fields { get; }

    /// <summary>Looks up a field by its declared PascalCase name. Returns false if the name is not found.</summary>
    bool TryGetField(string name, out Field field);

    /// <summary>Enumerates fields that differ between this message and another. The other message must be the same message type.</summary>
    IEnumerable<Field> DiffFields(IMessage other);

    /// <summary>Returns a human-readable multi-line representation of this message, indented according to <paramref name="options"/>.</summary>
    string ToFormattedString(PrintOptions options = default);
    /// <summary>Appends a human-readable multi-line representation of this message to <paramref name="builder"/>, indented according to <paramref name="options"/>.</summary>
    void ToFormattedString(StringBuilder builder, PrintOptions options = default);

    /// <summary>Sets the back-pointer to the packet that produced this message. Called by Packet.Parse only.
    ///  Default no-op so counted-group row classes that also implement IMessage
    ///  do not need to emit an override they never use. Application message classes override.</summary>
    void SetPacket(Packet packet) { }
}
