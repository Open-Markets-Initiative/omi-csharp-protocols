using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

/// <summary>
///  Specifies the session the client would like to log into, or all blanks to log into the currently active session.
/// </summary>
public unsafe struct RequestedSession
{
    /// <summary>
    ///  The size of the Requested Session field in bytes.
    /// </summary>
    public const int Size = 10;

    /// <summary>
    ///  The byte that fills the unused bytes of the Requested Session field.
    /// </summary>
    public const byte FillByte = 0x20;

    /// <summary>
    ///  Gets the number of encoded bytes in the Requested Session field.
    /// </summary>
    public readonly int Length
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            for (var index = 0; index < Size; index++)
            {
                if (Bytes[index] != FillByte)
                {
                    return Size - index;
                }
            }

            return 0;
        }
    }

    /// <summary>
    ///  Specifies the session the client would like to log into, or all blanks to log into the currently active session.
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
    ///  Gets the encoded bytes of the text in the Requested Session field, without the fill.
    /// </summary>
    [global::System.Diagnostics.CodeAnalysis.UnscopedRef]
    public readonly ReadOnlySpan<byte> ValueSpan
        => global::System.Runtime.InteropServices.MemoryMarshal.CreateReadOnlySpan(ref global::System.Runtime.CompilerServices.Unsafe.As<RequestedSession, byte>(ref global::System.Runtime.CompilerServices.Unsafe.AsRef(in this)), Size)[(Size - Length)..];

    /// <summary>
    ///  Returns the wire value of the Requested Session field.
    /// </summary>
    [global::System.Diagnostics.CodeAnalysis.UnscopedRef]
    public readonly ReadOnlySpan<byte> Decode()
        => global::System.Runtime.InteropServices.MemoryMarshal.CreateReadOnlySpan(ref global::System.Runtime.CompilerServices.Unsafe.As<RequestedSession, byte>(ref global::System.Runtime.CompilerServices.Unsafe.AsRef(in this)), Size);

    /// <summary>
    ///  Writes a value into the Requested Session field.
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
            throw new ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.FieldTextTooLong, nameof(value));
        }
        var start = Size - length;
        fixed (byte* pointer = Bytes)
        {
            var bytes = new Span<byte>(pointer, Size);
            bytes.Fill(FillByte);
            encoding.GetBytes(value.AsSpan(), bytes.Slice(start));
        }
    }

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => Value;

    internal fixed byte Bytes[Size];
}
