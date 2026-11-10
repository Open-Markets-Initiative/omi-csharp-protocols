using System.Buffers;
using System.Net;

namespace Omi.Pcap;

/// <summary>
///  One direction of one TCP connection, from <see cref="Source"/> to <see cref="Destination"/>.
/// </summary>
/// <remarks>
///  Instances come from <see cref="TcpReassembler.Add"/>.
/// </remarks>
public sealed class TcpFlow
{
    private const int MaxBufferedBytes = 1 << 20;

    private readonly List<Segment> pending = new();
    private byte[] released = [];
    private int releasedLength;
    private int bufferedBytes;
    private uint next;
    private uint? synchronizeSequence;
    private uint? closeSequence;
    private bool started;
    private TcpFlow? reverse;

    internal TcpFlow(IPEndPoint source, IPEndPoint destination)
    {
        Source = source;
        Destination = destination;
    }

    /// <summary>
    ///  Gets the address and port that send the bytes of this flow.
    /// </summary>
    public IPEndPoint Source { get; }

    /// <summary>
    ///  Gets the address and port that receive the bytes of this flow.
    /// </summary>
    public IPEndPoint Destination { get; }

    /// <summary>
    ///  Gets the side of the connection that sends the bytes of this flow.
    /// </summary>
    /// <remarks>
    ///  The value is <see cref="TcpSide.Unknown"/> while no SYN of the connection has been seen in either direction.
    /// </remarks>
    public TcpSide Side { get; private set; }

    /// <summary>
    ///  Gets a value that indicates whether the flow ended and gives no more bytes.
    /// </summary>
    /// <remarks>
    ///  A FIN or RST ends the flow once every byte before it in sequence order has been given. A gap that the flow
    ///  cannot wait for also ends it, and sets <see cref="Lost"/>. A SYN for a new connection on the same addresses
    ///  and ports ends it too.
    /// </remarks>
    public bool Ended { get; private set; }

    /// <summary>
    ///  Gets a value that indicates whether the flow ended at a gap in the captured bytes.
    /// </summary>
    /// <remarks>
    ///  The flow gave every byte before the gap and gives none after it. When the capture has no SYN for the
    ///  connection, the flow starts at the first captured segment with bytes; bytes before that segment are not given
    ///  and do not set this value.
    /// </remarks>
    public bool Lost { get; private set; }

    /// <summary>
    ///  Gets a value that indicates whether the flow holds bytes, or a FIN or RST, that follow bytes the capture has not yet given.
    /// </summary>
    /// <remarks>
    ///  A flow that is still waiting when the capture ends misses those bytes.
    /// </remarks>
    public bool Waiting => !Ended && (pending.Count > 0 || closeSequence is { } close && IsAfter(close, next));

    /// <summary>
    ///  A SYN with the sequence number of the SYN that opened this flow is a retransmission, not a new connection.
    /// </summary>
    internal bool IsOtherConnection(uint synchronize)
        => Ended || (synchronizeSequence is { } sequence ? sequence != synchronize : started);

    /// <summary>
    ///  Also gives this flow the side opposite to a known side of <paramref name="opposite"/>.
    /// </summary>
    internal void Link(TcpFlow opposite)
    {
        reverse = opposite;
        opposite.reverse = this;
        if (opposite.Side != TcpSide.Unknown)
            Side = Opposite(opposite.Side);
    }

    /// <summary>
    ///  Sets <paramref name="data"/> to the new in-order bytes, or to an empty span; a SYN takes one sequence number, so its payload starts after it.
    /// </summary>
    internal void Add(byte flags, uint sequence, ReadOnlySpan<byte> payload, out ReadOnlySpan<byte> data)
    {
        data = default;
        if (Ended)
            return;

        var close = (flags & (NetworkHeaders.TcpFlagFin | NetworkHeaders.TcpFlagRst)) != 0;
        if ((flags & NetworkHeaders.TcpFlagSyn) != 0)
        {
            SetSide((flags & NetworkHeaders.TcpFlagAck) != 0 ? TcpSide.Server : TcpSide.Client);
            if (!started)
            {
                synchronizeSequence = sequence;
                next = sequence + 1;
                started = true;
            }

            sequence++;
        }
        else if (!started)
        {
            if (payload.IsEmpty)
            {
                if (close)
                    Finish(lost: false);
                return;
            }

            next = sequence;
            started = true;
        }

        if (close)
            closeSequence ??= sequence + (uint)payload.Length;

        if (!payload.IsEmpty)
            Accept(sequence, payload, out data);
        if (!Ended && closeSequence is { } last && !IsBefore(next, last))
            Finish(lost: false);
    }

    private void SetSide(TcpSide side)
    {
        Side = side;
        reverse?.Side = Opposite(side);
    }

    private void Accept(uint sequence, ReadOnlySpan<byte> payload, out ReadOnlySpan<byte> data)
    {
        data = default;
        var end = sequence + (uint)payload.Length;
        if (!IsAfter(end, next))
            return;

        if (IsBefore(sequence, next))
        {
            payload = payload[(int)(next - sequence)..];
            sequence = next;
        }

        if (sequence != next)
        {
            Buffer(sequence, payload);
            if (bufferedBytes > MaxBufferedBytes)
                Finish(lost: true);
            return;
        }

        next = end;
        if (FindReleasable() < 0)
        {
            data = payload;
            return;
        }

        releasedLength = 0;
        Append(payload);
        Release();
        data = released.AsSpan(0, releasedLength);
    }

    /// <summary>
    ///  A retransmission of a kept segment is not kept again.
    /// </summary>
    private void Buffer(uint sequence, ReadOnlySpan<byte> payload)
    {
        foreach (var segment in pending)
        {
            if (segment.Sequence == sequence && segment.Length >= payload.Length)
                return;
        }

        var buffer = ArrayPool<byte>.Shared.Rent(payload.Length);
        payload.CopyTo(buffer);
        pending.Add(new Segment(sequence, buffer, payload.Length));
        bufferedBytes += payload.Length;
    }

    private void Release()
    {
        for (var index = FindReleasable(); index >= 0; index = FindReleasable())
        {
            var segment = pending[index];
            pending[index] = pending[^1];
            pending.RemoveAt(pending.Count - 1);

            if (IsAfter(segment.End, next))
            {
                Append(segment.Buffer.AsSpan((int)(next - segment.Sequence), (int)(segment.End - next)));
                next = segment.End;
            }

            bufferedBytes -= segment.Length;
            ArrayPool<byte>.Shared.Return(segment.Buffer);
        }
    }

    private int FindReleasable()
    {
        for (var index = 0; index < pending.Count; index++)
        {
            if (!IsAfter(pending[index].Sequence, next))
                return index;
        }
        return -1;
    }

    private void Append(ReadOnlySpan<byte> bytes)
    {
        var length = releasedLength + bytes.Length;
        if (length > released.Length)
            Array.Resize(ref released, Math.Max(length, released.Length * 2));
        bytes.CopyTo(released.AsSpan(releasedLength));
        releasedLength = length;
    }

    /// <summary>
    ///  The flow is lost only when it keeps bytes that it did not give.
    /// </summary>
    internal void EndForNewConnection()
        => Finish(lost: pending.Count > 0);

    private void Finish(bool lost)
    {
        Ended = true;
        Lost = lost;
        foreach (var segment in pending)
            ArrayPool<byte>.Shared.Return(segment.Buffer);
        pending.Clear();
        pending.TrimExcess();
        bufferedBytes = 0;
        released = [];
        releasedLength = 0;
        reverse?.reverse = null;
        reverse = null;
    }

    private static TcpSide Opposite(TcpSide side)
        => side == TcpSide.Client ? TcpSide.Server : TcpSide.Client;

    /// <summary>
    ///  Sequence numbers wrap at 2^32, so the order of two numbers is the sign of their 32-bit difference.
    /// </summary>
    private static bool IsBefore(uint left, uint right) => (int)(left - right) < 0;

    private static bool IsAfter(uint left, uint right) => (int)(left - right) > 0;

    private readonly record struct Segment(uint Sequence, byte[] Buffer, int Length)
    {
        public uint End => Sequence + (uint)Length;
    }
}
