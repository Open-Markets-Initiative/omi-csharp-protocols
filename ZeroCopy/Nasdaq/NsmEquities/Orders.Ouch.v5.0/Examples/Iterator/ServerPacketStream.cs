using System;
using System.Buffers.Binary;
namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0.Iterator;
/// <summary>
///  Reads Server Packets from the segments of one connection.
/// </summary>
/// <remarks>
///  The stream reads only the length of a frame until the frame is whole. It then gives each whole frame once. The frames of a receive and their Readers borrow the bytes of the stream and the segment. Keep both unchanged until the next receive or reset. The frames throw after the next receive or reset, but a Reader does not check. A caller that reuses its receive buffer finishes with the frames first.
/// </remarks>
public sealed class ServerPacketStream
{
    private const int FirstByte = 0;
    private const int OneByte = 1;
    private byte[] carry = [];
    private byte[] tail = [];
    private int carried;
    private int needed;
    private long received;
    private long consumed;
    private long epoch;
    private bool invalid;

    /// <summary>
    ///  Gets the offset in the stream up to which every frame is whole.
    /// </summary>
    public long Consumed => this.consumed;
    /// <summary>
    ///  Gets the number of bytes that the incomplete frame still needs.
    /// </summary>
    public int NeededBytes => this.needed;
    /// <summary>
    ///  Gets the number of bytes that the stream keeps for the next receive.
    /// </summary>
    public int PendingBytes => this.carried;
    /// <summary>
    ///  Gets a value that indicates whether the bytes stated a frame length that is not valid.
    /// </summary>
    /// <remarks>
    ///  An invalid stream gives no more frames until it is reset.
    /// </remarks>
    public bool Invalid => this.invalid;
    internal long Epoch => this.epoch;

    /// <summary>
    ///  Gives every whole frame that the bytes received so far hold, and keeps the bytes of an incomplete last frame.
    /// </summary>
    /// <param name="segment">
    ///  The bytes that the connection received next.
    /// </param>
    /// <returns>
    ///  The packets of the whole frames that the receive completed.
    /// </returns>
    /// <remarks>
    ///  The frames stop before a frame whose length is not valid, and the stream is then invalid.
    /// </remarks>
    public Frames Receive(ReadOnlySpan<byte> segment)
    {
        this.epoch++;
        if (this.invalid) return new Frames(default, default, this, this.epoch);
        long start = this.received - this.carried;
        this.received += segment.Length;
        ReadOnlySpan<byte> completed = default;
        int used = 0;
        if (this.carried > 0)
        {
            used = Complete(segment);
            if (this.needed > 0 || this.invalid) return new Frames(default, default, this, this.epoch);
            completed = this.carry.AsSpan(0, this.carried);
            start += this.carried;
            this.carried = 0;
        }
        var rest = segment[used..];
        int whole = Whole(rest, out this.needed);
        if (this.needed > 0) Retain(rest[whole..]);
        this.consumed = start + whole;
        return new Frames(completed, rest[..whole], this, this.epoch);
    }
    /// <summary>
    ///  Clears the buffered bytes.
    /// </summary>
    /// <remarks>
    ///  Moving through the frames from before the reset, or getting their current packet, throws an exception.
    /// </remarks>
    public void Reset()
    {
        this.epoch++;
        this.carried = 0;
        this.needed = 0;
        this.received = 0;
        this.consumed = 0;
        this.invalid = false;
    }
    private int Complete(ReadOnlySpan<byte> segment)
    {
        int used = 0;
        while (this.needed > 0 && used < segment.Length)
        {
            int take = Math.Min(this.needed, segment.Length - used);
            Append(segment.Slice(used, take));
            used += take;
            this.needed = Length(this.carry.AsSpan(0, this.carried), out int length) ? Math.Max(length - this.carried, 0) : this.invalid ? 0 : OneByte;
        }
        return used;
    }
    private int Whole(ReadOnlySpan<byte> bytes, out int needed)
    {
        int position = FirstByte;
        needed = 0;
        while (position < bytes.Length)
        {
            var rest = bytes[position..];
            if (!Length(rest, out int length)) { needed = this.invalid ? 0 : OneByte; break; }
            if (length > rest.Length) { needed = length - rest.Length; break; }
            position += length;
        }
        return position;
    }
    private bool Length(ReadOnlySpan<byte> bytes, out int length)
    {
        try
        {
            return global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPacket.TryGetFrameLength(bytes, out length);
        }
        catch (global::System.ArgumentOutOfRangeException)
        {
            this.invalid = true;
            length = 0;
            return false;
        }
    }
    private void Append(ReadOnlySpan<byte> bytes)
    {
        int length = checked(this.carried + bytes.Length);
        if (length > this.carry.Length) Array.Resize(ref this.carry, length);
        bytes.CopyTo(this.carry.AsSpan(this.carried));
        this.carried = length;
    }
    private void Retain(ReadOnlySpan<byte> bytes)
    {
        if (bytes.Length > this.tail.Length) this.tail = new byte[bytes.Length];
        bytes.CopyTo(this.tail);
        (this.carry, this.tail) = (this.tail, this.carry);
        this.carried = bytes.Length;
    }
    /// <summary>
    ///  Steps through the packets of the whole frames of one receive in wire order.
    /// </summary>
    /// <remarks>
    ///  The first packet is in the frame that the receive completed. A copy steps on its own.
    /// </remarks>
    public ref struct Frames
    {
        private const string ReceivedAgain = "The stream has received again or was reset since these frames.";
        private const string FrameTooShort = "The frame is shorter than the fixed fields at its start.";
        private readonly ServerPacketStream stream;
        private readonly long epoch;
        private readonly ReadOnlySpan<byte> completed;
        private readonly ReadOnlySpan<byte> rest;
        private int position;
        private bool second;
        private long index;
        private global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPacket.Reader current;

        internal Frames(ReadOnlySpan<byte> completed, ReadOnlySpan<byte> rest, ServerPacketStream stream, long epoch)
        {
            this.completed = completed;
            this.rest = rest;
            this.stream = stream;
            this.epoch = epoch;
        }
        /// <summary>
        ///  Gets the enumerator over the packets.
        /// </summary>
        public readonly Frames GetEnumerator() => this;

        /// <summary>
        ///  Gets the one-based number of the current packet in this receive, or zero before the first.
        /// </summary>
        public readonly long Index => this.index;

        /// <summary>
        ///  Moves to the next packet.
        /// </summary>
        /// <returns>
        ///  <see langword="true"/> when there is another packet; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The stream has received again or was reset since these frames.
        /// </exception>
        /// <exception cref="global::System.ArgumentOutOfRangeException">
        ///  The frame is shorter than the fixed fields at its start.
        /// </exception>
        public bool MoveNext()
        {
            Check();
            if (!this.second)
            {
                this.second = true;
                if (!this.completed.IsEmpty) return Next(this.completed);
            }
            if (this.position >= this.rest.Length) return false;
            var frame = this.rest[this.position..];
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPacket.TryGetFrameLength(frame, out int length);
            this.position += length;
            return Next(frame[..length]);
        }
        /// <summary>
        ///  Gets the current packet.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The stream has received again or was reset since these frames.
        /// </exception>
        public readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPacket.Reader Current
        {
            get
            {
                Check();
                return this.current;
            }
        }
        private bool Next(ReadOnlySpan<byte> frame)
        {
            if (!global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPacket.TryRead(frame, out this.current)) throw new global::System.ArgumentOutOfRangeException(nameof(frame), FrameTooShort);
            this.index++;
            return true;
        }
        private readonly void Check()
        {
            if (this.stream.Epoch != this.epoch) throw new global::System.InvalidOperationException(ReceivedAgain);
        }
    }
}
