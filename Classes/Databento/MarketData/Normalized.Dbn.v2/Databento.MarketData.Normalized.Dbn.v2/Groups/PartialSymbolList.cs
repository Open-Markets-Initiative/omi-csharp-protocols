namespace Databento.MarketData.Normalized.Dbn.v2;

/// <summary>
///  Represents the entries of Partial Symbol, in wire order.
/// </summary>
public sealed class PartialSymbolList : IReadOnlyList<string>, IEquatable<PartialSymbolList>
{
    private readonly string[] items;

    /// <summary>
    ///  Gets the number of entries.
    /// </summary>
    public int Count => this.items.Length;

    /// <summary>
    ///  Gets the result of the most recent parse.
    /// </summary>
    public ParseStatus Status { get; }

    /// <summary>
    ///  Gets the entry at the specified index.
    /// </summary>
    public string this[int index] => this.items[index];

    /// <summary>
    ///  Returns an enumerator that iterates through the entries in wire order.
    /// </summary>
    public IEnumerator<string> GetEnumerator() => ((IEnumerable<string>)this.items).GetEnumerator();

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    ///  Initializes a new instance of the <see cref="PartialSymbolList"/> class with copies of the entries and the parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public PartialSymbolList(PartialSymbolList other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.items = new string[other.items.Length];
        for (var i = 0; i < this.items.Length; i++)
        {
            this.items[i] = other.items[i];
        }
        Status = other.Status;
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="PartialSymbolList"/> class that contains the specified entries, in order.
    /// </summary>
    /// <exception cref="ArgumentNullException">The entries are <see langword="null"/>.</exception>
    public PartialSymbolList(IEnumerable<string> entries)
    {
        ArgumentNullException.ThrowIfNull(entries);
        this.items = [.. entries];
    }

    internal PartialSymbolList(List<string> entries, ParseStatus status)
    {
        this.items = [.. entries];
        Status = status;
    }

    internal PartialSymbolList(int count, string entry)
    {
        this.items = new string[count];
        Array.Fill(this.items, entry);
    }

    /// <summary>
    ///  Determines whether another instance contains equal entries in the same order.
    /// </summary>
    public bool Equals(PartialSymbolList? other)
    {
        if (other is null || other.items.Length != this.items.Length) return false;
        for (var i = 0; i < this.items.Length; i++)
        {
            if (this.items[i] != other.items[i]) return false;
        }
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type that contains equal entries in the same order.
    /// </summary>
    public override bool Equals(object? obj) => obj is PartialSymbolList other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the entries.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        foreach (var entry in this.items)
        {
            hash.Add(entry);
        }
        return hash.ToHashCode();
    }
}
