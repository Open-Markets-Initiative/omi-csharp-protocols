namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Reserved
/// </summary>

public unsafe struct Reserved
{
    /// <summary>
    ///  Size of Reserved in bytes
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Returns the string representation of the Reserved value.
    /// </summary>
    public readonly override string ToString()
        => $"Data[{Size}]";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal fixed byte Bytes[Size];
}
