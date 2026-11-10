namespace Databento.MarketData.Normalized.Dbn.v2;

/// <summary>
///  ASCII key=value pairs separated by the | character, terminated by a newline character
/// </summary>
public struct ControlFields
{
    /// <summary>
    ///  The size of the Control Fields field in bytes.
    /// </summary>
    public const int Size = 0;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => $"Data[0]";
}
