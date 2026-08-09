using System.Text;

namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  Common interface implemented by all generated transport-header classes.
///  Headers carry packet framing fields; they are not message payloads and do not implement IMessage.
/// </summary>
public interface IHeader
{
    /// <summary>The raw binary data slice captured during the last parse attempt, or null if no parse has been attempted.
    ///  States: (a) no parse attempted — Raw is null, IsDecoded is false; (b) parse failed — Raw is an empty or partial slice, IsDecoded is false;
    ///  (c) parse succeeded — Raw is the full header slice, IsDecoded is true; (d) cleared — Raw is null, IsDecoded is false.</summary>
    ReadOnlyMemory<byte>? Raw { get; }

    /// <summary>Total fixed byte length of this header.</summary>
    int ByteLength { get; }

    /// <summary>True if Parse() completed without structural error (every field decoded). False if default-constructed, cleared, or parse failed.</summary>
    bool IsDecoded { get; }

    /// <summary>True if every field's IsRecognized is true. Non-enum fields inherit IsRecognized => IsDecoded.</summary>
    bool IsRecognized { get; }

    /// <summary>Convenience: true if IsDecoded and IsRecognized are both true.</summary>
    bool IsValid { get; }

    /// <summary>The first field that failed structural parsing, or null if the last parse succeeded or no parse has been attempted.</summary>
    Field? FailedAt { get; }

    /// <summary>Encodes this header into the supplied span starting at offset; returns the offset after the last written byte.</summary>
    int Encode(Span<byte> data, int offset);

    /// <summary>Encodes this header into the supplied span starting at offset 0; returns the offset after the last written byte.</summary>
    int Encode(Span<byte> data);

    /// <summary>Resets all fields to their default values and clears the raw backing store.</summary>
    void Clear();

    /// <summary>Parses binary data into this header instance in place, reusing existing field objects.</summary>
    void ParseFrom(ReadOnlySpan<byte> data);

    /// <summary>Returns true if the header was fully decoded, Raw is present, and its bytes equal data.</summary>
    bool Equals(ReadOnlySpan<byte> data);

    /// <summary>Enumerates all fields in declaration order.</summary>
    IEnumerable<Field> Fields { get; }

    /// <summary>Looks up a field by its declared PascalCase name. Returns false if the name is not found.</summary>
    bool TryGetField(string name, out Field field);

    /// <summary>Enumerates fields that differ between this header and another. Other must be the same header type.</summary>
    IEnumerable<Field> DiffFields(IHeader other);

    /// <summary>Returns a human-readable multi-line representation of this header.</summary>
    string ToFormattedString(PrintOptions options = default);

    /// <summary>Appends a human-readable multi-line representation of this header to builder.</summary>
    void ToFormattedString(StringBuilder builder, PrintOptions options = default);
}
