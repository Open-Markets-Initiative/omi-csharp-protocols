namespace Databento.MarketData.Normalized.Dbn.v2;

/// <summary>
///  Specifies why encoding failed.
/// </summary>
public enum EncodeStatus
{
    /// <summary>
    ///  Every value fits its field and the span is large enough.
    /// </summary>
    Ok = 0,
    /// <summary>
    ///  The value is out of range, has a character that the field cannot hold, or does not fill the field.
    /// </summary>
    DoesNotFit = 1,
    /// <summary>
    ///  The text, the bytes, or the group has more characters, bytes, or entries than the field holds.
    /// </summary>
    TooLong = 2,
    /// <summary>
    ///  The span is too small for the encoded bytes.
    /// </summary>
    BufferTooSmall = 4,
    /// <summary>
    ///  A nested message has a trailer, which the message that holds it has no place to write.
    /// </summary>
    NestedTrailer = 6,
    /// <summary>
    ///  A value disagrees with a protocol rule, such as a count, a length, or a condition on other values.
    /// </summary>
    RuleMismatch = 7,
}
