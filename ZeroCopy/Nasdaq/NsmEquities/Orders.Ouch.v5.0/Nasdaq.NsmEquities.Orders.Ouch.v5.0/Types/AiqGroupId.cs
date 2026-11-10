using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Specified to enable self match prevention at a more granular level within a given matching level
/// </summary>
public unsafe struct AiqGroupId
{
    /// <summary>
    ///  The size of the Aiq Group Id field in bytes.
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Gets the number of encoded bytes in the Aiq Group Id field.
    /// </summary>
    public readonly int Length
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Size;
    }

    /// <summary>
    ///  Specified to enable self match prevention at a more granular level within a given matching level
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
    ///  Gets the encoded bytes of the text in the Aiq Group Id field, without the fill.
    /// </summary>
    [global::System.Diagnostics.CodeAnalysis.UnscopedRef]
    public readonly ReadOnlySpan<byte> ValueSpan
        => global::System.Runtime.InteropServices.MemoryMarshal.CreateReadOnlySpan(ref global::System.Runtime.CompilerServices.Unsafe.As<AiqGroupId, byte>(ref global::System.Runtime.CompilerServices.Unsafe.AsRef(in this)), Size)[..Length];

    /// <summary>
    ///  Returns the wire value of the Aiq Group Id field.
    /// </summary>
    [global::System.Diagnostics.CodeAnalysis.UnscopedRef]
    public readonly ReadOnlySpan<byte> Decode()
        => global::System.Runtime.InteropServices.MemoryMarshal.CreateReadOnlySpan(ref global::System.Runtime.CompilerServices.Unsafe.As<AiqGroupId, byte>(ref global::System.Runtime.CompilerServices.Unsafe.AsRef(in this)), Size);

    /// <summary>
    ///  Writes a value into the Aiq Group Id field.
    /// </summary>
    /// <exception cref="ArgumentException">
    ///  The encoded text does not fill the field exactly.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(string value)
    {
        var encoding = global::System.Text.Encoding.ASCII;
        if (encoding.GetByteCount(value) != Size)
        {
            throw new ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.FieldTextNotExact, nameof(value));
        }
        fixed (byte* pointer = Bytes)
        {
            encoding.GetBytes(value.AsSpan(), new Span<byte>(pointer, Size));
        }
    }

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => Value;

    internal fixed byte Bytes[Size];
}
