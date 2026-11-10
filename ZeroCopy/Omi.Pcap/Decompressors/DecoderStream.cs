namespace Omi.Pcap.Decompressors;

/// <summary>
///  A read-only, forward-only stream that returns the output of a decoder, and reports a decoder failure as <see cref="InvalidDataException"/>.
/// </summary>
/// <remarks>
///  A read returns no bytes only when the decoder returns no bytes and takes no bytes from the source. A decoder that
///  stops at the end of its first frame thus ends this stream there, even if the source holds more bytes. When the decoder
///  throws after a read of the source threw, this stream throws the first exception of the source. Disposing this stream disposes the decoder and the source.
/// </remarks>
internal sealed class DecoderStream : Stream
{
    private readonly Stream decoder;
    private readonly PrependStream source;
    private bool disposed;

    public DecoderStream(PrependStream source, Func<PrependStream, Stream> createDecoder)
    {
        this.source = source;
        try
        {
            decoder = createDecoder(source);
        }
        catch (Exception exception) when (IsReplaced(exception))
        {
            throw Replacement(exception);
        }
    }

    public override bool CanRead => !disposed;
    public override bool CanSeek => false;
    public override bool CanWrite => false;
    public override long Length => throw new NotSupportedException();

    public override long Position
    {
        get => throw new NotSupportedException();
        set => throw new NotSupportedException();
    }

    public override int Read(byte[] buffer, int offset, int count)
        => Read(buffer.AsSpan(offset, count));

    /// <summary>
    ///  A decoder can return no bytes at the end of one frame and continue with the next frame on the next read.
    ///  Thus this method reads again while the decoder takes more bytes from the source.
    /// </summary>
    public override int Read(Span<byte> buffer)
    {
        ObjectDisposedException.ThrowIf(disposed, this);
        try
        {
            while (true)
            {
                var before = source.BytesRead;
                var got = decoder.Read(buffer);
                if (got > 0 || buffer.IsEmpty || source.BytesRead == before) { return got; }
            }
        }
        catch (Exception exception) when (IsReplaced(exception))
        {
            throw Replacement(exception);
        }
    }

    public override void Flush() { }
    public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
    public override void SetLength(long value) => throw new NotSupportedException();
    public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();

    protected override void Dispose(bool disposing)
    {
        if (disposing && !disposed)
        {
            disposed = true;
            try
            {
                decoder.Dispose();
            }
            finally
            {
                source.Dispose();
            }
        }
        base.Dispose(disposing);
    }

    private bool IsReplaced(Exception exception)
        => source.Fault is { } fault
            ? !ReferenceEquals(fault.SourceException, exception)
            : exception is not (OutOfMemoryException or OperationCanceledException or InvalidDataException);

    private Exception Replacement(Exception exception)
    {
        source.Fault?.Throw();
        return new InvalidDataException("The compressed capture is corrupt, truncated, or uses an unsupported feature.", exception);
    }
}
