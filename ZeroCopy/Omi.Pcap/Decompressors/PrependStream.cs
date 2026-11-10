namespace Omi.Pcap.Decompressors;

using System.Runtime.ExceptionServices;

/// <summary>
///  A read-only, forward-only stream that returns a byte prefix and then the bytes of an inner stream.
/// </summary>
/// <remarks>
///  Disposing this stream disposes the inner stream.
/// </remarks>
internal sealed class PrependStream : Stream
{
    private readonly byte[] prefix;
    private readonly Stream inner;
    private int prefixPosition;

    public PrependStream(byte[] prefix, int prefixLength, Stream inner)
    {
        this.prefix = prefix;
        this.inner = inner;
        prefixPosition = 0;
        PrefixLength = prefixLength;
    }

    private int PrefixLength { get; }

    /// <summary>
    ///  Gets the number of bytes that this stream returned.
    /// </summary>
    public long BytesRead { get; private set; }

    /// <summary>
    ///  Gets the first exception that a read of the inner stream threw, or <see langword="null"/> if no read threw.
    /// </summary>
    public ExceptionDispatchInfo? Fault { get; private set; }

    public override bool CanRead => true;
    public override bool CanSeek => false;
    public override bool CanWrite => false;
    public override long Length => throw new NotSupportedException();
    public override long Position
    {
        get => throw new NotSupportedException();
        set => throw new NotSupportedException();
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        var total = 0;

        if (prefixPosition < PrefixLength)
        {
            var take = Math.Min(count, PrefixLength - prefixPosition);
            Buffer.BlockCopy(prefix, prefixPosition, buffer, offset, take);
            prefixPosition += take;
            offset += take;
            count -= take;
            total += take;
        }

        if (count > 0)
        {
            total += ReadInner(buffer.AsSpan(offset, count));
        }

        BytesRead += total;
        return total;
    }

    public override int Read(Span<byte> buffer)
    {
        var total = 0;

        if (prefixPosition < PrefixLength)
        {
            var take = Math.Min(buffer.Length, PrefixLength - prefixPosition);
            prefix.AsSpan(prefixPosition, take).CopyTo(buffer);
            prefixPosition += take;
            buffer = buffer[take..];
            total += take;
        }

        if (buffer.Length > 0)
        {
            total += ReadInner(buffer);
        }

        BytesRead += total;
        return total;
    }

    public override void Flush() { }
    public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
    public override void SetLength(long value) => throw new NotSupportedException();
    public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();

    protected override void Dispose(bool disposing)
    {
        if (disposing) { inner.Dispose(); }
        base.Dispose(disposing);
    }

    private int ReadInner(Span<byte> buffer)
    {
        try
        {
            return inner.Read(buffer);
        }
        catch (Exception exception)
        {
            Fault ??= ExceptionDispatchInfo.Capture(exception);
            throw;
        }
    }
}
