using System.Text;

namespace Databento.MarketData.Normalized.Dbn.v1;

internal sealed class TextEncoding
{
    private const int CharacterBytes = 1;
    private const int SurrogatePairChars = 2;
    private const string Replacement = "?";
    private const int Windows1252CodePage = 1252;
    private const int Latin5CodePage = 28599;
    private const int EucKrCodePage = 51949;
    private const int Utf16LittleCodeUnitBytes = 2;
    private const int Utf16BigCodeUnitBytes = 2;

    internal static readonly TextEncoding Ascii = new(Encoding.Latin1, repertoire: Encoding.ASCII);
    internal static readonly TextEncoding Utf8 = new(Encoding.UTF8);
    internal static readonly TextEncoding Latin1 = new(Encoding.Latin1);
    internal static readonly TextEncoding Windows1252 = new(CodePagesEncodingProvider.Instance.GetEncoding(Windows1252CodePage)!);
    internal static readonly TextEncoding Latin5 = new(CodePagesEncodingProvider.Instance.GetEncoding(Latin5CodePage)!);
    internal static readonly TextEncoding EucKr = new(CodePagesEncodingProvider.Instance.GetEncoding(EucKrCodePage)!);
    internal static readonly TextEncoding Utf16Little = new(Encoding.Unicode, Utf16LittleCodeUnitBytes);
    internal static readonly TextEncoding Utf16Big = new(Encoding.BigEndianUnicode, Utf16BigCodeUnitBytes);

    private readonly Encoding encoding;
    private readonly Encoding strict;
    private readonly Encoding replacing;
    private readonly int codeUnitBytes;

    private TextEncoding(Encoding encoding, int codeUnitBytes = CharacterBytes, Encoding? repertoire = null)
    {
        this.encoding = encoding;
        this.codeUnitBytes = codeUnitBytes;
        this.strict = (Encoding)(repertoire ?? encoding).Clone();
        this.strict.EncoderFallback = EncoderFallback.ExceptionFallback;
        this.replacing = (Encoding)(repertoire ?? encoding).Clone();
        this.replacing.EncoderFallback = new EncoderReplacementFallback(Replacement);
    }

    internal string Read(ReadOnlySpan<byte> wire) => this.encoding.GetString(wire);

    internal string ReadLeftJustified(ReadOnlySpan<byte> wire, byte fill)
        => this.codeUnitBytes == CharacterBytes ? this.encoding.GetString(wire.TrimEnd(fill)) : this.encoding.GetString(wire).TrimEnd((char)fill);

    internal string ReadRightJustified(ReadOnlySpan<byte> wire, byte fill)
        => this.codeUnitBytes == CharacterBytes ? this.encoding.GetString(wire.TrimStart(fill)) : this.encoding.GetString(wire).TrimStart((char)fill);

    internal char ReadChar(byte value) => ReadChar([value]);

    internal char ReadChar(ReadOnlySpan<byte> wire)
    {
        Span<char> characters = stackalloc char[this.encoding.GetMaxCharCount(wire.Length)];
        this.encoding.GetChars(wire, characters);
        return characters[0];
    }

    internal int Write(Span<byte> wire, string value) => this.encoding.GetBytes(value, wire);

    internal void WriteLeftJustified(Span<byte> wire, string value, byte fill)
    {
        Fill(wire, fill);
        this.encoding.GetBytes(value, wire);
    }

    internal void WriteRightJustified(Span<byte> wire, string value, byte fill)
    {
        Fill(wire, fill);
        this.encoding.GetBytes(value, wire[^this.encoding.GetByteCount(value)..]);
    }

    internal byte WriteChar(char value)
    {
        ReadOnlySpan<char> characters = [value];
        Span<byte> wire = stackalloc byte[this.encoding.GetMaxByteCount(CharacterBytes)];
        this.encoding.GetBytes(characters, wire);
        return wire[0];
    }

    internal int ByteCount(string? value) => value is null ? 0 : this.encoding.GetByteCount(value);

    internal bool CanWrite(string? value) => value is null || TryStrictByteCount(value, out _);

    internal bool CanWrite(char? value) => value is not { } character || TryStrictByteCount([character], out var count) && count == CharacterBytes;

    internal string Holdable(string value) => TryStrictByteCount(value, out _) ? value : this.replacing.GetString(this.replacing.GetBytes(value));

    internal string Fit(string value, int length)
    {
        if (TryStrictByteCount(value, out var count) && count <= length)
        {
            return value;
        }
        var holdable = Holdable(value);
        var bytes = 0;
        var end = 0;
        Span<char> unit = stackalloc char[SurrogatePairChars];
        foreach (var rune in holdable.EnumerateRunes())
        {
            var size = rune.EncodeToUtf16(unit);
            var next = bytes + this.encoding.GetByteCount(unit[..size]);
            if (next > length)
            {
                break;
            }
            bytes = next;
            end += size;
        }
        return holdable[..end];
    }

    internal byte[] Bytes(string? value) => value is null ? [] : this.encoding.GetBytes(value);

    internal void Fill(Span<byte> wire, byte fill)
    {
        if (this.codeUnitBytes == CharacterBytes)
        {
            wire.Fill(fill);
            return;
        }
        ReadOnlySpan<char> unit = [(char)fill];
        for (var start = 0; start < wire.Length; start += this.codeUnitBytes)
        {
            this.encoding.GetBytes(unit, wire[start..]);
        }
    }

    private bool TryStrictByteCount(ReadOnlySpan<char> value, out int count)
    {
        try
        {
            count = this.strict.GetByteCount(value);
            return true;
        }
        catch (EncoderFallbackException)
        {
            count = default;
            return false;
        }
    }
}
