using System.Buffers.Binary;
using System.Diagnostics;

namespace Databento.MarketData.Normalized.Dbn.v2;

/// <summary>
///  A DBN record: the common header, the record its rtype selects, and the ts_out trailer when the metadata asks for it
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class RecordFrame : IEquatable<RecordFrame>
{
    internal const int TsOutTimeLength = 8;
    /// <summary>
    ///  The number of nanoseconds in one unit of the wire value of <see cref="TsOutTime"/>.
    /// </summary>
    public const long TsOutTimeNanosecondsPerUnit = 1;
    private RecordHeader recordHeader = new RecordHeader();

    /// <summary>
    ///  DBN common header of every record
    /// </summary>
    public RecordHeader RecordHeader => this.recordHeader;

    private ulong? tsOutTime;

    /// <summary>
    ///  The live gateway send timestamp, present on every record when the metadata ts_out flag is set
    /// </summary>
    public DateTime? TsOutTime
    {
        get => this.tsOutTime is null ? (DateTime?)null : Instant.ToDateTime(this.tsOutTime.GetValueOrDefault(), TsOutTimeNanosecondsPerUnit);
        set => this.tsOutTime = value is { } present ? Instant.ToRaw<ulong>(present, TsOutTimeNanosecondsPerUnit, ulong.MinValue, ulong.MaxValue) : (ulong?)null;
    }

    internal IFileMessage? recordBody;

    /// <summary>
    ///  Branch
    /// </summary>
    /// <remarks>
    ///  Setting a message also sets each kind that leads to it. The value is <see langword="null"/> where the packet carries no message here.
    /// </remarks>
    public IFileMessage? RecordBody
    {
        get => this.recordBody;
        set => this.recordBody = value;
    }

    internal IEnumerable<IFileMessage> Collect()
    {
        if (this.recordBody is not null) yield return this.recordBody;
        yield break;
    }

    internal void SetRecordHeader(RecordHeader value)
    {
        this.recordHeader = value;
    }

    internal void SetTsOutTime(ReadOnlySpan<byte> bytes)
    {
        this.tsOutTime = BinaryPrimitives.ReadUInt64LittleEndian(bytes);
    }

    internal int TsOutTimeEncodedLength => TsOutTimeLength;

    internal bool WriteTsOutTime(Span<byte> data, int offset, out int cursor, out EncodeFailure failure)
    {
        cursor = offset;
        BinaryPrimitives.WriteUInt64LittleEndian(data.Slice(cursor, TsOutTimeLength), this.tsOutTime.GetValueOrDefault());
        cursor += TsOutTimeLength;
        failure = default;
        return true;
    }

    internal ulong TsOutTimeRuleValue => this.tsOutTime.GetValueOrDefault();

    /// <summary>
    ///  Checks every field of the header parts of the entry against the values that the protocol specification allows.
    /// </summary>
    /// <returns>The fields whose values are not allowed, each with the reason, or an empty list if every value is allowed.</returns>
    public List<ValidationFailure> Validate()
    {
        var failures = new List<ValidationFailure>();
        failures.AddRange(this.RecordHeader.Validate());
        return failures;
    }

    internal RecordFrame Copy()
    {
        var copy = new RecordFrame();
        copy.recordHeader = new RecordHeader(this.recordHeader);
        copy.tsOutTime = this.tsOutTime;
        copy.recordBody = (IFileMessage?)((IMessage?)this.recordBody)?.Copy();
        return copy;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(RecordFrame? other)
    {
        if (other is null)
        {
            return false;
        }
        if (!this.recordHeader.Equals(other.recordHeader)) return false;
        if (this.tsOutTime != other.tsOutTime) return false;
        if (!Equals(this.recordBody, other.recordBody)) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is RecordFrame other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.recordHeader);
        hash.Add(this.tsOutTime);
        hash.Add(this.recordBody);
        return hash.ToHashCode();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"RecordFrame {{ RecordHeader: {(Text.Of(this.recordHeader.ToString()))}, TsOutTime: {(Text.Instant(this.tsOutTime, TsOutTimeNanosecondsPerUnit))}, RecordBody: {(Text.Of(this.recordBody?.ToString()))} }}";

    private string DebuggerDisplay => $"[{(Text.Instant(this.tsOutTime, TsOutTimeNanosecondsPerUnit))}]";
}
