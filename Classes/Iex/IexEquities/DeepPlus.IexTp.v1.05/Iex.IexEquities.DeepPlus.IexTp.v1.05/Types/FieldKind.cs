namespace Iex.IexEquities.DeepPlus.IexTp.v1_05;

/// <summary>
///  Specifies the kind of value that a field holds.
/// </summary>
public enum FieldKind
{
    /// <summary>
    ///  An unsigned integer.
    /// </summary>
    UnsignedInteger,
    /// <summary>
    ///  A signed integer.
    /// </summary>
    SignedInteger,
    /// <summary>
    ///  ASCII text, of a fixed or a variable length.
    /// </summary>
    String,
    /// <summary>
    ///  One ASCII character.
    /// </summary>
    Char,
    /// <summary>
    ///  A point in time.
    /// </summary>
    DateTime,
    /// <summary>
    ///  A decimal number with a fixed number of decimal places.
    /// </summary>
    Decimal,
    /// <summary>
    ///  Named bits in one integer.
    /// </summary>
    Bitfield,
    /// <summary>
    ///  One of a defined set of values.
    /// </summary>
    Enum,
    /// <summary>
    ///  A fixed block of bytes.
    /// </summary>
    Struct,
    /// <summary>
    ///  Bytes with no further structure.
    /// </summary>
    Data,
    /// <summary>
    ///  Bytes that carry no value.
    /// </summary>
    Padding,
    /// <summary>
    ///  A repeating group of entries.
    /// </summary>
    Group,
    /// <summary>
    ///  A nested message identified by its message type.
    /// </summary>
    Message,
    /// <summary>
    ///  An IEEE 754 floating-point number.
    /// </summary>
    FloatingPoint,
}
