namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  The schema definition; always empty
/// </summary>
public struct SchemaDefinition
{
    /// <summary>
    ///  The size of the Schema Definition field in bytes.
    /// </summary>
    public const int Size = 0;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => $"Data[0]";
}
