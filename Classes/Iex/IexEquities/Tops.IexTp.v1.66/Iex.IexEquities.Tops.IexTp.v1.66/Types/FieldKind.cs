namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Flat category enum identifying the wire/value archetype of a field.
///  Returned by <see cref="Field.Kind"/> on every field instance.
///  Used for diagnostics and reporting; not for inheritance dispatch.
/// </summary>
public enum FieldKind
{
    UnsignedInteger,
    SignedInteger,
    String,
    Char,
    DateTime,
    Decimal,
    Bitfield,
    Enum,
    Struct
}
