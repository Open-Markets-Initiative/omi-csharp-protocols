using System.Runtime.CompilerServices;

namespace Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0;

/// <summary>
///  Underlying Symbol for the strategy. All legs in this strategy belong to this Underlying
/// </summary>
public unsafe struct UnderlyingSymbol
{
    /// <summary>
    ///  The size of the Underlying Symbol field in bytes.
    /// </summary>
    public const int Size = 13;

    /// <summary>
    ///  The byte that fills the unused bytes of the Underlying Symbol field.
    /// </summary>
    public const byte FillByte = 0x20;

    /// <summary>
    ///  Gets the number of encoded bytes in the Underlying Symbol field.
    /// </summary>
    public readonly int Length
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            for (var index = Size - 1; index >= 0; index--)
            {
                if (Bytes[index] != FillByte)
                {
                    return index + 1;
                }
            }

            return 0;
        }
    }

    /// <summary>
    ///  Underlying Symbol for the strategy. All legs in this strategy belong to this Underlying
    /// </summary>
    /// <exception cref="ArgumentException">
    ///  The encoded text is longer than the field.
    /// </exception>
    public string Value
    {
        readonly get => global::System.Text.Encoding.ASCII.GetString(ValueSpan);
        set => Encode(value);
    }

    /// <summary>
    ///  Gets the encoded bytes of the text in the Underlying Symbol field, without the fill.
    /// </summary>
    [global::System.Diagnostics.CodeAnalysis.UnscopedRef]
    public readonly ReadOnlySpan<byte> ValueSpan
        => global::System.Runtime.InteropServices.MemoryMarshal.CreateReadOnlySpan(ref global::System.Runtime.CompilerServices.Unsafe.As<UnderlyingSymbol, byte>(ref global::System.Runtime.CompilerServices.Unsafe.AsRef(in this)), Size)[..Length];

    /// <summary>
    ///  Returns the wire value of the Underlying Symbol field.
    /// </summary>
    [global::System.Diagnostics.CodeAnalysis.UnscopedRef]
    public readonly ReadOnlySpan<byte> Decode()
        => global::System.Runtime.InteropServices.MemoryMarshal.CreateReadOnlySpan(ref global::System.Runtime.CompilerServices.Unsafe.As<UnderlyingSymbol, byte>(ref global::System.Runtime.CompilerServices.Unsafe.AsRef(in this)), Size);

    /// <summary>
    ///  Writes a value into the Underlying Symbol field.
    /// </summary>
    /// <exception cref="ArgumentException">
    ///  The encoded text is longer than the field.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(string value)
    {
        var encoding = global::System.Text.Encoding.ASCII;
        var length = encoding.GetByteCount(value);
        if (length > Size)
        {
            throw new ArgumentException(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.FieldTextTooLong, nameof(value));
        }
        fixed (byte* pointer = Bytes)
        {
            var bytes = new Span<byte>(pointer, Size);
            bytes.Fill(FillByte);
            encoding.GetBytes(value.AsSpan(), bytes);
        }
    }

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => Value;

    internal fixed byte Bytes[Size];
}
