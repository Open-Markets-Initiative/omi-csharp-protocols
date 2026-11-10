using System.Runtime.CompilerServices;

namespace Iex.IexEquities.DeepPlus.IexTp.v1_01;

/// <summary>
///  Reason for the trading status change
/// </summary>
public unsafe struct Reason
{
    /// <summary>
    ///  The size of the Reason field in bytes.
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  The byte that fills the unused bytes of the Reason field.
    /// </summary>
    public const byte FillByte = 0x20;

    /// <summary>
    ///  Gets the number of encoded bytes in the Reason field.
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
    ///  Reason for the trading status change
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
    ///  Gets the encoded bytes of the text in the Reason field, without the fill.
    /// </summary>
    [global::System.Diagnostics.CodeAnalysis.UnscopedRef]
    public readonly ReadOnlySpan<byte> ValueSpan
        => global::System.Runtime.InteropServices.MemoryMarshal.CreateReadOnlySpan(ref global::System.Runtime.CompilerServices.Unsafe.As<Reason, byte>(ref global::System.Runtime.CompilerServices.Unsafe.AsRef(in this)), Size)[..Length];

    /// <summary>
    ///  Returns the wire value of the Reason field.
    /// </summary>
    [global::System.Diagnostics.CodeAnalysis.UnscopedRef]
    public readonly ReadOnlySpan<byte> Decode()
        => global::System.Runtime.InteropServices.MemoryMarshal.CreateReadOnlySpan(ref global::System.Runtime.CompilerServices.Unsafe.As<Reason, byte>(ref global::System.Runtime.CompilerServices.Unsafe.AsRef(in this)), Size);

    /// <summary>
    ///  Writes a value into the Reason field.
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
            throw new ArgumentException(global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.FieldTextTooLong, nameof(value));
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
