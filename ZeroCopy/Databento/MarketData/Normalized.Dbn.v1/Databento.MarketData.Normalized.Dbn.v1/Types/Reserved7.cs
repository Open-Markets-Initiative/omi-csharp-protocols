namespace Databento.MarketData.Normalized.Dbn.v1;

/// <summary>
///  Reserved, 7 bytes
/// </summary>
public unsafe struct Reserved7
{
    /// <summary>
    ///  The size of the Reserved 7 field in bytes.
    /// </summary>
    public const int Size = 7;

    /// <summary>
    ///  Reserved, 7 bytes
    /// </summary>
    /// <exception cref="ArgumentException">
    ///  The value is not as long as the field.
    /// </exception>
    [global::System.Diagnostics.CodeAnalysis.UnscopedRef]
    public ReadOnlySpan<byte> Value
    {
        readonly get => global::System.Runtime.InteropServices.MemoryMarshal.CreateReadOnlySpan(ref global::System.Runtime.CompilerServices.Unsafe.As<Reserved7, byte>(ref global::System.Runtime.CompilerServices.Unsafe.AsRef(in this)), Size);
        set
        {
            if (value.Length != Size)
            {
                throw new ArgumentException(global::Databento.MarketData.Normalized.Dbn.v1.Parser.FieldBytesLength, nameof(value));
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
