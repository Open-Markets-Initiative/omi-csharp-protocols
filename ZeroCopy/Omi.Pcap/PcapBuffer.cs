namespace Omi.Pcap;

/// <summary>
///  Holds bytes read from a capture stream and grows to hold one large record or block.
/// </summary>
/// <remarks>
///  A call to <see cref="Ensure"/> that refills or grows the buffer invalidates a pointer to its bytes.
/// </remarks>
internal sealed class PcapBuffer
{
    /// <summary>
    ///  The largest byte count that <see cref="Ensure"/> can make available; a capture that states a longer record or block is damaged.
    /// </summary>
    public const int MaxSize = 16 * 1024 * 1024;

    private const int InitialSize = 65536;

    private readonly Stream source;
    private byte[] buffer = GC.AllocateUninitializedArray<byte>(InitialSize, pinned: true);
    private int position;
    private int valid;

    /// <summary>
    ///  Initializes a new instance of the <see cref="PcapBuffer"/> class.
    /// </summary>
    /// <param name="source">The stream that supplies bytes. The buffer does not dispose it.</param>
    public PcapBuffer(Stream source)
    {
        this.source = source;
    }

    public int Available => valid - position;

    /// <summary>
    ///  Gets the unconsumed bytes.
    /// </summary>
    /// <remarks>
    ///  The span is valid until the next call to <see cref="Ensure"/>, which can move or replace the bytes.
    ///  It does not reflect a later call to <see cref="Consume"/>.
    /// </remarks>
    public ReadOnlySpan<byte> Span
        => new(buffer, position, valid - position);

    /// <summary>
    ///  Reads from the source until the buffer holds the requested number of unconsumed bytes.
    /// </summary>
    /// <param name="count">The required byte count.</param>
    /// <returns><see langword="true"/> if <paramref name="count"/> unconsumed bytes are available; otherwise, <see langword="false"/>, when the source ends first or <paramref name="count"/> is greater than <see cref="MaxSize"/>.</returns>
    public bool Ensure(int count)
    {
        if (count > buffer.Length)
        {
            Grow(count);
        }

        while (Available < count)
        {
            if (!Refill()) { return false; }
        }

        return true;
    }

    public void Consume(int count)
        => position += count;

    private void Grow(int count)
    {
        var grown = GC.AllocateUninitializedArray<byte>(Math.Min(MaxSize, Math.Max(count, buffer.Length * 2)), pinned: true);
        Buffer.BlockCopy(buffer, position, grown, 0, valid - position);
        valid -= position;
        position = 0;
        buffer = grown;
    }

    private bool Refill()
    {
        var remaining = valid - position;

        if (position > 0)
        {
            if (remaining > 0)
            {
                Buffer.BlockCopy(buffer, position, buffer, 0, remaining);
            }
            position = 0;
            valid = remaining;
        }

        var space = buffer.Length - valid;
        if (space == 0) { return false; }

        var got = source.Read(buffer, valid, space);
        valid += got;
        return got > 0;
    }
}
