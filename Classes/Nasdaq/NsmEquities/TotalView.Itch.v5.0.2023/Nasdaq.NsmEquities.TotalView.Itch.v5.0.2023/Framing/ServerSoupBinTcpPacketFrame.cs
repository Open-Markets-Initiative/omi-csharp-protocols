using System.Buffers.Binary;
using System.Diagnostics;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

/// <summary>
///  Soup Bin Tcp Packet sent by the server
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class ServerSoupBinTcpPacketFrame : IEquatable<ServerSoupBinTcpPacketFrame>
{
    private ServerPacketHeader serverPacketHeader = new ServerPacketHeader();

    /// <summary>
    ///  Packet header of a packet sent by the server
    /// </summary>
    public ServerPacketHeader ServerPacketHeader => this.serverPacketHeader;

    /// <summary>
    ///  Gets or sets the kind of the Server Payload, which is the case that the packet carries.
    /// </summary>
    public ServerPayloadKind ServerPayloadKind { get; set; } = ServerPayloadKind.Messages;

    internal IServerMessage? serverPayload;

    /// <summary>
    ///  Branch
    /// </summary>
    /// <remarks>
    ///  Setting a message also sets each kind that leads to it. The value is <see langword="null"/> where the packet carries no message here.
    /// </remarks>
    public IServerMessage? ServerPayload
    {
        get => this.serverPayload;
        set
        {
            this.serverPayload = value;
            if (value is not null)
            {
                ServerPayloadKind = ServerPayloadKind.Messages;
            }
        }
    }

    internal IEnumerable<IServerMessage> Collect()
    {
        if (this.ServerPayloadKind == ServerPayloadKind.Messages && this.serverPayload is not null) yield return this.serverPayload;
        yield break;
    }

    internal void SetServerPacketHeader(ServerPacketHeader value)
    {
        this.serverPacketHeader = value;
    }

    /// <summary>
    ///  Checks every field of the header parts of the entry against the values that the protocol specification allows.
    /// </summary>
    /// <returns>The fields whose values are not allowed, each with the reason, or an empty list if every value is allowed.</returns>
    public List<ValidationFailure> Validate()
    {
        var failures = new List<ValidationFailure>();
        failures.AddRange(this.ServerPacketHeader.Validate());
        return failures;
    }

    internal ServerSoupBinTcpPacketFrame Copy()
    {
        var copy = new ServerSoupBinTcpPacketFrame();
        copy.serverPacketHeader = new ServerPacketHeader(this.serverPacketHeader);
        copy.ServerPayloadKind = this.ServerPayloadKind;
        copy.serverPayload = (IServerMessage?)((IMessage?)this.serverPayload)?.Copy();
        return copy;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(ServerSoupBinTcpPacketFrame? other)
    {
        if (other is null)
        {
            return false;
        }
        if (!this.serverPacketHeader.Equals(other.serverPacketHeader)) return false;
        if (this.ServerPayloadKind != other.ServerPayloadKind) return false;
        if (!Equals(this.serverPayload, other.serverPayload)) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is ServerSoupBinTcpPacketFrame other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.serverPacketHeader);
        hash.Add(this.ServerPayloadKind);
        hash.Add(this.serverPayload);
        return hash.ToHashCode();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"ServerSoupBinTcpPacketFrame {{ ServerPacketHeader: {(Text.Of(this.serverPacketHeader.ToString()))}, ServerPayloadKind: {(Text.Enum(this.ServerPayloadKind))}, ServerPayload: {(Text.Of(this.serverPayload?.ToString()))} }}";

    private string DebuggerDisplay => $"ServerSoupBinTcpPacketFrame";
}
