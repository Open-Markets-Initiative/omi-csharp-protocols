namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  Reassembles packets from a TCP byte stream.
/// </summary>
public sealed class GatewayStreamStream
{
    private byte[] pending = [];

    private int pendingLength;
    private byte? tsOutStoredValue;

    /// <summary>
    ///  Gets the number of received bytes that do not yet form a complete packet.
    /// </summary>
    public int PendingBytes => this.pendingLength;

    /// <summary>
    ///  Gets a value that indicates whether the bytes stated a frame length that is not valid.
    /// </summary>
    public bool IsFailed { get; private set; }

    /// <summary>
    ///  Gets the packet that holds the opening of the stream, or null before the stream has read the opening.
    /// </summary>
    public GatewayStream? Opening { get; private set; }

    /// <summary>
    ///  Appends received bytes to the stream.
    /// </summary>
    /// <param name="data">The received bytes.</param>
    /// <param name="failed">Set to true when the bytes state a frame length that is not valid.</param>
    /// <returns>The packets that the new bytes complete, in order, or an empty list if there are none.</returns>
    /// <exception cref="InvalidOperationException">The stream failed at an earlier frame whose length is not valid, and it cannot be resynchronized.</exception>
    /// <remarks>
    ///  The frames stop before a frame whose length is not valid, and the stream is then invalid.
    /// </remarks>
    public IReadOnlyList<GatewayStream> Append(ReadOnlySpan<byte> data, out bool failed)
    {
        if (this.IsFailed)
        {
            throw new InvalidOperationException("The stream failed at an earlier frame whose length is not valid, and it cannot be resynchronized.");
        }
        var buffer = this.pendingLength == 0 ? data : Join(data);
        var packets = new List<GatewayStream>();
        var position = 0;
        while (position < buffer.Length)
        {
            var rest = buffer[position..];
            if (this.Opening is null)
            {
                if (!Open(rest, packets, out var openingLength))
                {
                    break;
                }
                position += openingLength;
                continue;
            }
            int length;
            try
            {
                if (!GatewayStream.TryGetFrameLength(rest, out length) || length > rest.Length)
                {
                    break;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                this.IsFailed = true;
                break;
            }
            packets.Add(GatewayStream.ParseRecord(rest[..length], this.Opening, ref this.tsOutStoredValue));
            position += length;
        }
        var remaining = buffer[position..];
        if (remaining.Length != this.pendingLength)
        {
            Reserve(remaining.Length);
            remaining.CopyTo(this.pending);
            this.pendingLength = remaining.Length;
        }
        failed = this.IsFailed;
        return packets;
    }

    private bool Open(ReadOnlySpan<byte> bytes, List<GatewayStream> packets, out int length)
    {
        try
        {
            if (!GatewayStream.TryGetOpeningLength(bytes, out length) || length > bytes.Length)
            {
                return false;
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            this.IsFailed = true;
            length = 0;
            return false;
        }
        this.Opening = GatewayStream.Parse(bytes[..length], ref this.tsOutStoredValue);
        packets.Add(this.Opening);
        return true;
    }

    private ReadOnlySpan<byte> Join(ReadOnlySpan<byte> data)
    {
        Reserve(this.pendingLength + data.Length);
        data.CopyTo(this.pending.AsSpan(this.pendingLength));
        this.pendingLength += data.Length;
        return this.pending.AsSpan(0, this.pendingLength);
    }

    private void Reserve(int length)
    {
        if (length > this.pending.Length)
        {
            Array.Resize(ref this.pending, Math.Max(length, this.pending.Length * 2));
        }
    }
}
