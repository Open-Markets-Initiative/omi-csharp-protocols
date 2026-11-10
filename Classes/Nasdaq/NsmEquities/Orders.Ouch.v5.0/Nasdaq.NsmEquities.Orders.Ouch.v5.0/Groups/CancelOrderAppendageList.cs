namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Represents the entries of Cancel Order Appendage, in wire order.
/// </summary>
public sealed class CancelOrderAppendageList : IReadOnlyList<CancelOrderAppendage>, IEquatable<CancelOrderAppendageList>
{
    private readonly CancelOrderAppendage[] items;

    /// <summary>
    ///  Gets the number of entries.
    /// </summary>
    public int Count => this.items.Length;

    /// <summary>
    ///  Gets the result of the most recent parse.
    /// </summary>
    public ParseStatus Status { get; }

    /// <summary>
    ///  Gets the entry where the parse stopped, or <see langword="null"/> if every entry parsed.
    /// </summary>
    public CancelOrderAppendage? FailedEntry { get; }

    /// <summary>
    ///  Gets a value indicating whether every entry holds values that the protocol defines.
    /// </summary>
    public bool IsRecognized => Array.TrueForAll(this.items, static entry => ((IGroupEntry)entry).IsRecognized);

    /// <summary>
    ///  Gets the entry at the specified index.
    /// </summary>
    public CancelOrderAppendage this[int index] => this.items[index];

    /// <summary>
    ///  Returns an enumerator that iterates through the entries in wire order.
    /// </summary>
    public IEnumerator<CancelOrderAppendage> GetEnumerator() => ((IEnumerable<CancelOrderAppendage>)this.items).GetEnumerator();

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    ///  Initializes a new instance of the <see cref="CancelOrderAppendageList"/> class with copies of the entries and the parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public CancelOrderAppendageList(CancelOrderAppendageList other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.items = new CancelOrderAppendage[other.items.Length];
        for (var i = 0; i < this.items.Length; i++)
        {
            this.items[i] = new CancelOrderAppendage(other.items[i]);
        }
        Status = other.Status;
        FailedEntry = other.FailedEntry is null ? null : new CancelOrderAppendage(other.FailedEntry);
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="CancelOrderAppendageList"/> class that contains the specified entries, in order.
    /// </summary>
    /// <exception cref="ArgumentNullException">The entries, or one of the entries, is <see langword="null"/>.</exception>
    public CancelOrderAppendageList(IEnumerable<CancelOrderAppendage> entries)
    {
        ArgumentNullException.ThrowIfNull(entries);
        this.items = [.. entries];
        foreach (var entry in this.items)
        {
            ArgumentNullException.ThrowIfNull(entry, nameof(entries));
        }
    }

    internal CancelOrderAppendageList(List<CancelOrderAppendage> entries, ParseStatus status, CancelOrderAppendage? failedEntry)
    {
        this.items = [.. entries];
        Status = status;
        FailedEntry = failedEntry;
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="CancelOrderAppendageList"/> class by parsing entries from the offset to the end of the bytes.
    /// </summary>
    /// <param name="data">The bytes to parse.</param>
    /// <param name="offset">The offset at which to start. A successful parse advances it past the parsed bytes, and a failed parse does not change it.</param>
    public CancelOrderAppendageList(ReadOnlySpan<byte> data, ref int offset)
    {
        var cursor = offset;
        var entries = new List<CancelOrderAppendage>();
        var CancelOrderAppendageCountRule0End = data.Length;
        while (cursor < CancelOrderAppendageCountRule0End)
        {
            int cancelOrderAppendageEntryStart = cursor;
            var entry = new CancelOrderAppendage();
            var contract = (IGroupEntry)entry;
            contract.ParseFrom(data[..CancelOrderAppendageCountRule0End], ref cursor);
            if (contract.Status != ParseStatus.Ok)
            {
                this.items = [.. entries];
                Status = contract.Status;
                FailedEntry = entry;
                return;
            }
            entries.Add(entry);
            if (cursor <= cancelOrderAppendageEntryStart || cursor > CancelOrderAppendageCountRule0End) { this.items = [.. entries]; Status = ParseStatus.Truncated; return; }
        }
        if (cursor != CancelOrderAppendageCountRule0End) { this.items = [.. entries]; Status = ParseStatus.BadCount; return; }
        this.items = [.. entries];
        Status = ParseStatus.Ok;
        offset = cursor;
    }

    /// <summary>
    ///  Gets the number of bytes that the entries take when encoded.
    /// </summary>
    public int EncodedByteLength
    {
        get
        {
            var total = 0;
            foreach (var entry in this.items)
            {
                total += entry.EncodedByteLength;
            }
            return total;
        }
    }

    internal void SetOwner(IFieldOwner? owner)
    {
        foreach (var entry in this.items)
        {
            ((IFieldOwner)entry).Owner = owner;
        }
    }

    /// <summary>
    ///  Determines whether another instance contains equal entries in the same order.
    /// </summary>
    public bool Equals(CancelOrderAppendageList? other) => other is not null && this.items.AsSpan().SequenceEqual(other.items);

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type that contains equal entries in the same order.
    /// </summary>
    public override bool Equals(object? obj) => obj is CancelOrderAppendageList other && Equals(other);

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
