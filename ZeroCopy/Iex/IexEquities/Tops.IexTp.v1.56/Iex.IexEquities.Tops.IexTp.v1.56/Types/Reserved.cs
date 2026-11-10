namespace Iex.IexEquities.Tops.IexTp.v1_56;

/// <summary>
///  Reserved byte
/// </summary>
public unsafe struct Reserved
{
    /// <summary>
    ///  The size of the Reserved field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Reserved byte
    /// </summary>
    /// <exception cref="ArgumentException">
    ///  The value is not as long as the field.
    /// </exception>
    [global::System.Diagnostics.CodeAnalysis.UnscopedRef]
    public ReadOnlySpan<byte> Value
    {
        readonly get => global::System.Runtime.InteropServices.MemoryMarshal.CreateReadOnlySpan(ref global::System.Runtime.CompilerServices.Unsafe.As<Reserved, byte>(ref global::System.Runtime.CompilerServices.Unsafe.AsRef(in this)), Size);
        set
        {
            if (value.Length != Size)
            {
                throw new ArgumentException(global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.FieldBytesLength, nameof(value));
            }
            fixed (byte* pointer = Bytes)
            {
                value.CopyTo(new Span<byte>(pointer, Size));
            }
        }
    }

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => $"Data[{Size}]";

    internal fixed byte Bytes[Size];
}
