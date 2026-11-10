using System.Buffers.Binary;
using System.Diagnostics;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

/// <summary>
///  Soup Bin Tcp Packet sent by the client
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class ClientSoupBinTcpPacketFrame : IEquatable<ClientSoupBinTcpPacketFrame>
{
    private ClientPacketHeader clientPacketHeader = new ClientPacketHeader();

    /// <summary>
    ///  Packet header of a packet sent by the client
    /// </summary>
    public ClientPacketHeader ClientPacketHeader => this.clientPacketHeader;

    /// <summary>
    ///  Gets or sets the kind of the Client Payload, which is the case that the packet carries.
    /// </summary>
    public ClientPayloadKind ClientPayloadKind { get; set; } = ClientPayloadKind.Messages;

    internal IClientMessage? clientPayload;

    /// <summary>
    ///  Branch
    /// </summary>
    /// <remarks>
    ///  Setting a message also sets each kind that leads to it. The value is <see langword="null"/> where the packet carries no message here.
    /// </remarks>
    public IClientMessage? ClientPayload
    {
        get => this.clientPayload;
        set
        {
            this.clientPayload = value;
            if (value is not null)
            {
                ClientPayloadKind = ClientPayloadKind.Messages;
            }
        }
    }

    internal IEnumerable<IClientMessage> Collect()
    {
        if (this.ClientPayloadKind == ClientPayloadKind.Messages && this.clientPayload is not null) yield return this.clientPayload;
        yield break;
    }

    internal void SetClientPacketHeader(ClientPacketHeader value)
    {
        this.clientPacketHeader = value;
    }

    /// <summary>
    ///  Checks every field of the header parts of the entry against the values that the protocol specification allows.
    /// </summary>
    /// <returns>The fields whose values are not allowed, each with the reason, or an empty list if every value is allowed.</returns>
    public List<ValidationFailure> Validate()
    {
        var failures = new List<ValidationFailure>();
        failures.AddRange(this.ClientPacketHeader.Validate());
        return failures;
    }

    internal ClientSoupBinTcpPacketFrame Copy()
    {
        var copy = new ClientSoupBinTcpPacketFrame();
        copy.clientPacketHeader = new ClientPacketHeader(this.clientPacketHeader);
        copy.ClientPayloadKind = this.ClientPayloadKind;
        copy.clientPayload = (IClientMessage?)((IMessage?)this.clientPayload)?.Copy();
        return copy;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(ClientSoupBinTcpPacketFrame? other)
    {
        if (other is null)
        {
            return false;
        }
        if (!this.clientPacketHeader.Equals(other.clientPacketHeader)) return false;
        if (this.ClientPayloadKind != other.ClientPayloadKind) return false;
        if (!Equals(this.clientPayload, other.clientPayload)) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is ClientSoupBinTcpPacketFrame other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.clientPacketHeader);
        hash.Add(this.ClientPayloadKind);
        hash.Add(this.clientPayload);
        return hash.ToHashCode();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"ClientSoupBinTcpPacketFrame {{ ClientPacketHeader: {(Text.Of(this.clientPacketHeader.ToString()))}, ClientPayloadKind: {(Text.Enum(this.ClientPayloadKind))}, ClientPayload: {(Text.Of(this.clientPayload?.ToString()))} }}";

    private string DebuggerDisplay => $"ClientSoupBinTcpPacketFrame";
}
