using System.Collections;

namespace Iex.IexEquities.Deep.IexTp.v1_08;

/// <summary>
///  Enumerates the fields of one instance in wire order.
/// </summary>
public struct FieldEnumerator : IEnumerator<FieldView>
{
    private const int BeforeFirst = -1;
    private readonly IFieldOwner owner;
    private int field;

    internal FieldEnumerator(IFieldOwner owner)
    {
        this.owner = owner;
        this.field = BeforeFirst;
    }

    /// <summary>
    ///  Gets the current field.
    /// </summary>
    public readonly FieldView Current => new(this.owner, this.field);

    readonly object IEnumerator.Current => Current;

    /// <summary>
    ///  Advances the enumerator to the next field.
    /// </summary>
    public bool MoveNext() => ++this.field < this.owner.Descriptors.Length;

    /// <summary>
    ///  Sets the enumerator to its initial position, before the first field.
    /// </summary>
    public void Reset() => this.field = BeforeFirst;

    /// <summary>
    ///  Releases the enumerator.
    /// </summary>
    public readonly void Dispose() { }
}
