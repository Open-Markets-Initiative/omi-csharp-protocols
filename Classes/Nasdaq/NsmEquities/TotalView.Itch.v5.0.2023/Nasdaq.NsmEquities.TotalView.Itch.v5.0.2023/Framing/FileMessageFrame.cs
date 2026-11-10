using System.Buffers.Binary;
using System.Diagnostics;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

/// <summary>
///  Nasdaq historical file message: its length, then the message
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class FileMessageFrame : IEquatable<FileMessageFrame>
{
    private MessageHeader messageHeader = new MessageHeader();

    /// <summary>
    ///  Mold Udp 64 Message Header
    /// </summary>
    public MessageHeader MessageHeader => this.messageHeader;

    internal IFileMessage? filePayload;

    /// <summary>
    ///  Branch
    /// </summary>
    /// <remarks>
    ///  Setting a message also sets each kind that leads to it. The value is <see langword="null"/> where the packet carries no message here.
    /// </remarks>
    public IFileMessage? FilePayload
    {
        get => this.filePayload;
        set => this.filePayload = value;
    }

    internal IEnumerable<IFileMessage> Collect()
    {
        if (this.filePayload is not null) yield return this.filePayload;
        yield break;
    }

    internal void SetMessageHeader(MessageHeader value)
    {
        this.messageHeader = value;
    }

    /// <summary>
    ///  Checks every field of the header parts of the entry against the values that the protocol specification allows.
    /// </summary>
    /// <returns>The fields whose values are not allowed, each with the reason, or an empty list if every value is allowed.</returns>
    public List<ValidationFailure> Validate()
    {
        var failures = new List<ValidationFailure>();
        failures.AddRange(this.MessageHeader.Validate());
        return failures;
    }

    internal FileMessageFrame Copy()
    {
        var copy = new FileMessageFrame();
        copy.messageHeader = new MessageHeader(this.messageHeader);
        copy.filePayload = (IFileMessage?)((IMessage?)this.filePayload)?.Copy();
        return copy;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(FileMessageFrame? other)
    {
        if (other is null)
        {
            return false;
        }
        if (!this.messageHeader.Equals(other.messageHeader)) return false;
        if (!Equals(this.filePayload, other.filePayload)) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is FileMessageFrame other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.messageHeader);
        hash.Add(this.filePayload);
        return hash.ToHashCode();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"FileMessageFrame {{ MessageHeader: {(Text.Of(this.messageHeader.ToString()))}, FilePayload: {(Text.Of(this.filePayload?.ToString()))} }}";

    private string DebuggerDisplay => $"FileMessageFrame";
}
