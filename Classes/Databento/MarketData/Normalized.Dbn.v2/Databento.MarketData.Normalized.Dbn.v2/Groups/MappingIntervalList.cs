using static Databento.MarketData.Normalized.Dbn.v2.Rules;
namespace Databento.MarketData.Normalized.Dbn.v2;

/// <summary>
///  Represents the entries of Mapping Interval, in wire order.
/// </summary>
public sealed class MappingIntervalList : IReadOnlyList<MappingInterval>, IEquatable<MappingIntervalList>
{
    private readonly MappingInterval[] items;

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
    public MappingInterval? FailedEntry { get; }

    /// <summary>
    ///  Gets a value indicating whether every entry holds values that the protocol defines.
    /// </summary>
    public bool IsRecognized => Array.TrueForAll(this.items, static entry => ((IGroupEntry)entry).IsRecognized);

    /// <summary>
    ///  Gets the entry at the specified index.
    /// </summary>
    public MappingInterval this[int index] => this.items[index];

    /// <summary>
    ///  Returns an enumerator that iterates through the entries in wire order.
    /// </summary>
    public IEnumerator<MappingInterval> GetEnumerator() => ((IEnumerable<MappingInterval>)this.items).GetEnumerator();

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    ///  Initializes a new instance of the <see cref="MappingIntervalList"/> class with copies of the entries and the parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public MappingIntervalList(MappingIntervalList other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.items = new MappingInterval[other.items.Length];
        for (var i = 0; i < this.items.Length; i++)
        {
            this.items[i] = new MappingInterval(other.items[i]);
        }
        Status = other.Status;
        FailedEntry = other.FailedEntry is null ? null : new MappingInterval(other.FailedEntry);
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="MappingIntervalList"/> class that contains the specified entries, in order.
    /// </summary>
    /// <exception cref="ArgumentNullException">The entries, or one of the entries, is <see langword="null"/>.</exception>
    public MappingIntervalList(IEnumerable<MappingInterval> entries)
    {
        ArgumentNullException.ThrowIfNull(entries);
        this.items = [.. entries];
        foreach (var entry in this.items)
        {
            ArgumentNullException.ThrowIfNull(entry, nameof(entries));
        }
    }

    internal MappingIntervalList(List<MappingInterval> entries, ParseStatus status, MappingInterval? failedEntry)
    {
        this.items = [.. entries];
        Status = status;
        FailedEntry = failedEntry;
    }

    internal MappingIntervalList(int count)
    {
        this.items = new MappingInterval[count];
        for (var i = 0; i < count; i++)
        {
            this.items[i] = new MappingInterval();
        }
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="MappingIntervalList"/> class with the specified number of entries parsed from the offset.
    /// </summary>
    /// <param name="data">The bytes to parse.</param>
    /// <param name="offset">The offset at which to start. A successful parse advances it past the parsed bytes, and a failed parse does not change it.</param>
    /// <param name="count">The number of entries to parse.</param>
    /// <exception cref="ArgumentOutOfRangeException">The count is negative.</exception>
    public MappingIntervalList(ReadOnlySpan<byte> data, ref int offset, int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(count);
        var cursor = offset;
        var entries = new List<MappingInterval>();
        var MappingIntervalCountRule0Entries = CreateRuleCount((global::System.Int128)count);
        while (MappingIntervalCountRule0Entries > RuleCountNone)
        {
            var entry = new MappingInterval();
            var contract = (IGroupEntry)entry;
            contract.ParseFrom(data, ref cursor);
            if (contract.Status != ParseStatus.Ok)
            {
                this.items = [.. entries];
                Status = contract.Status;
                FailedEntry = entry;
                return;
            }
            entries.Add(entry);
            MappingIntervalCountRule0Entries--;
        }
        if (MappingIntervalCountRule0Entries > RuleCountNone) { this.items = [.. entries]; Status = ParseStatus.BadCount; return; }
        this.items = [.. entries];
        Status = ParseStatus.Ok;
        offset = cursor;
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
    public bool Equals(MappingIntervalList? other) => other is not null && this.items.AsSpan().SequenceEqual(other.items);

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type that contains equal entries in the same order.
    /// </summary>
    public override bool Equals(object? obj) => obj is MappingIntervalList other && Equals(other);

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
