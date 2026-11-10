using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  This field should contain all caps. Firm identifier for the order entry firm. One logical OUCH account can potentially enter orders for multiple firms in a Service Bureau configuration.
/// </summary>
public unsafe struct Firm
{
    /// <summary>
    ///  The size of the Firm field in bytes.
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  The byte that fills the unused bytes of the Firm field.
    /// </summary>
    public const byte FillByte = 0x20;

    /// <summary>
    ///  Gets the number of encoded bytes in the Firm field.
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
    ///  This field should contain all caps. Firm identifier for the order entry firm. One logical OUCH account can potentially enter orders for multiple firms in a Service Bureau configuration.
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
    ///  Gets the encoded bytes of the text in the Firm field, without the fill.
    /// </summary>
    [global::System.Diagnostics.CodeAnalysis.UnscopedRef]
    public readonly ReadOnlySpan<byte> ValueSpan
        => global::System.Runtime.InteropServices.MemoryMarshal.CreateReadOnlySpan(ref global::System.Runtime.CompilerServices.Unsafe.As<Firm, byte>(ref global::System.Runtime.CompilerServices.Unsafe.AsRef(in this)), Size)[..Length];

    /// <summary>
    ///  Returns the wire value of the Firm field.
    /// </summary>
    [global::System.Diagnostics.CodeAnalysis.UnscopedRef]
    public readonly ReadOnlySpan<byte> Decode()
        => global::System.Runtime.InteropServices.MemoryMarshal.CreateReadOnlySpan(ref global::System.Runtime.CompilerServices.Unsafe.As<Firm, byte>(ref global::System.Runtime.CompilerServices.Unsafe.AsRef(in this)), Size);

    /// <summary>
    ///  Writes a value into the Firm field.
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
            throw new ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.FieldTextTooLong, nameof(value));
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
