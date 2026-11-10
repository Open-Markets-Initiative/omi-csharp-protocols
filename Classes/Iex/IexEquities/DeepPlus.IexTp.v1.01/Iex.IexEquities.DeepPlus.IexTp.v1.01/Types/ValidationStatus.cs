namespace Iex.IexEquities.DeepPlus.IexTp.v1_01;

/// <summary>
///  Specifies why the protocol specification does not allow a value.
/// </summary>
public enum ValidationStatus
{
    /// <summary>
    ///  The value is below the minimum of the field.
    /// </summary>
    BelowMinimum,
    /// <summary>
    ///  The value is above the maximum of the field.
    /// </summary>
    AboveMaximum,
    /// <summary>
    ///  The value is not one of the values that the field defines.
    /// </summary>
    UndeclaredValue,
    /// <summary>
    ///  The value is not one of the fixed values that the field requires.
    /// </summary>
    NotStaticValue,
    /// <summary>
    ///  The text holds a character that is not ASCII.
    /// </summary>
    NonAsciiText,
    /// <summary>
    ///  The text is shorter than the field, which has no fill for the unused bytes.
    /// </summary>
    TextTooShort,
}
