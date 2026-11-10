namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

/// <summary>
///  Specifies the result of a parse.
/// </summary>
public enum ParseStatus
{
    /// <summary>
    ///  No parse has run since the instance was created or cleared.
    /// </summary>
    None,
    /// <summary>
    ///  The parse read every field.
    /// </summary>
    Ok,
    /// <summary>
    ///  The bytes ended before a field was complete.
    /// </summary>
    Truncated,
    /// <summary>
    ///  The starting offset was outside the bytes.
    /// </summary>
    InvalidOffset,
    /// <summary>
    ///  An entry count was negative or exceeded what the group allows.
    /// </summary>
    BadCount,
}
