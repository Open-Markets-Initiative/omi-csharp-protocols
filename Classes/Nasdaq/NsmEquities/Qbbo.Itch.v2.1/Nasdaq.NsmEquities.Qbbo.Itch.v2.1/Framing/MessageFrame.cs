using System.Buffers.Binary;
using System.Diagnostics;

namespace Nasdaq.NsmEquities.Qbbo.Itch.v2_1;

/// <summary>
///  Mold Udp64 Message
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class MessageFrame : IEquatable<MessageFrame>
{
    private MessageHeader messageHeader = new MessageHeader();

    /// <summary>
    ///  Mold Udp 64 Message Header
    /// </summary>
    public MessageHeader MessageHeader => this.messageHeader;

    internal IPacketMessage? payload;

    /// <summary>
    ///  Branch
    /// </summary>
    /// <remarks>
    ///  Setting a message also sets each kind that leads to it. The value is <see langword="null"/> where the packet carries no message here.
    /// </remarks>
    public IPacketMessage? Payload
    {
        get => this.payload;
        set => this.payload = value;
    }

    internal IEnumerable<IPacketMessage> Collect()
    {
        if (this.payload is not null) yield return this.payload;
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

    internal MessageFrame Copy()
    {
        var copy = new MessageFrame();
        copy.messageHeader = new MessageHeader(this.messageHeader);
        copy.payload = (IPacketMessage?)((IMessage?)this.payload)?.Copy();
        return copy;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(MessageFrame? other)
    {
        if (other is null)
        {
            return false;
        }
        if (!this.messageHeader.Equals(other.messageHeader)) return false;
        if (!Equals(this.payload, other.payload)) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is MessageFrame other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.messageHeader);
        hash.Add(this.payload);
        return hash.ToHashCode();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"MessageFrame {{ MessageHeader: {(Text.Of(this.messageHeader.ToString()))}, Payload: {(Text.Of(this.payload?.ToString()))} }}";

    private string DebuggerDisplay => $"MessageFrame";
}
