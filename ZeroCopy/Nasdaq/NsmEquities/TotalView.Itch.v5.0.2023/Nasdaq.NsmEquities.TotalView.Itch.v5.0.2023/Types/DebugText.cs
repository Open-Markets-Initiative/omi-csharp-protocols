namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

/// <summary>
///  Free form human readable text
/// </summary>
public struct DebugText
{
    /// <summary>
    ///  The size of the Debug Text field in bytes.
    /// </summary>
    public const int Size = 0;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => $"Data[0]";
}
