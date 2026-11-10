namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  Padding to an 8 byte boundary
/// </summary>
public struct MetadataPadding
{
    /// <summary>
    ///  The size of the Metadata Padding field in bytes.
    /// </summary>
    public const int Size = 0;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => $"Data[0]";
}
