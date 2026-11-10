namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  A bit field indicating event end, message characteristics, and data quality; declared least significant bit first
/// </summary>
public readonly record struct Flags
{
    internal Flags(byte value)
    {
        Value = value;
    }

    /// <summary>
    ///  Gets the integer that contains the bits.
    /// </summary>
    public byte Value { get; init; }

    /// <summary>
    ///  The bit mask of Reserved Flag.
    /// </summary>
    public const byte ReservedFlagMask = 0x1;

    /// <summary>
    ///  The position of the lowest bit of Reserved Flag.
    /// </summary>
    public const int ReservedFlagShift = 0;

    /// <summary>
    ///  The number of bits in Reserved Flag.
    /// </summary>
    public const int ReservedFlagWidth = 1;

    /// <summary>
    ///  Bit 0, unused
    /// </summary>
    public bool ReservedFlag
    {
        get => (Value & ReservedFlagMask) != 0;
        init => Value = (byte)(value ? (ulong)Value | ReservedFlagMask : (ulong)Value & ~(ulong)ReservedFlagMask);
    }

    /// <summary>
    ///  The bit mask of Publisher Specific.
    /// </summary>
    public const byte PublisherSpecificMask = 0x2;

    /// <summary>
    ///  The position of the lowest bit of Publisher Specific.
    /// </summary>
    public const int PublisherSpecificShift = 1;

    /// <summary>
    ///  The number of bits in Publisher Specific.
    /// </summary>
    public const int PublisherSpecificWidth = 1;

    /// <summary>
    ///  Indicates a publisher-specific event (bit 1)
    /// </summary>
    public bool PublisherSpecific
    {
        get => (Value & PublisherSpecificMask) != 0;
        init => Value = (byte)(value ? (ulong)Value | PublisherSpecificMask : (ulong)Value & ~(ulong)PublisherSpecificMask);
    }

    /// <summary>
    ///  The bit mask of Maybe Bad Book.
    /// </summary>
    public const byte MaybeBadBookMask = 0x4;

    /// <summary>
    ///  The position of the lowest bit of Maybe Bad Book.
    /// </summary>
    public const int MaybeBadBookShift = 2;

    /// <summary>
    ///  The number of bits in Maybe Bad Book.
    /// </summary>
    public const int MaybeBadBookWidth = 1;

    /// <summary>
    ///  Indicates an unrecoverable gap was detected in the channel (bit 2)
    /// </summary>
    public bool MaybeBadBook
    {
        get => (Value & MaybeBadBookMask) != 0;
        init => Value = (byte)(value ? (ulong)Value | MaybeBadBookMask : (ulong)Value & ~(ulong)MaybeBadBookMask);
    }

    /// <summary>
    ///  The bit mask of Bad Ts Recv.
    /// </summary>
    public const byte BadTsRecvMask = 0x8;

    /// <summary>
    ///  The position of the lowest bit of Bad Ts Recv.
    /// </summary>
    public const int BadTsRecvShift = 3;

    /// <summary>
    ///  The number of bits in Bad Ts Recv.
    /// </summary>
    public const int BadTsRecvWidth = 1;

    /// <summary>
    ///  Indicates the ts_recv value is inaccurate due to clock issues or packet reordering (bit 3)
    /// </summary>
    public bool BadTsRecv
    {
        get => (Value & BadTsRecvMask) != 0;
        init => Value = (byte)(value ? (ulong)Value | BadTsRecvMask : (ulong)Value & ~(ulong)BadTsRecvMask);
    }

    /// <summary>
    ///  The bit mask of Mbp.
    /// </summary>
    public const byte MbpMask = 0x10;

    /// <summary>
    ///  The position of the lowest bit of Mbp.
    /// </summary>
    public const int MbpShift = 4;

    /// <summary>
    ///  The number of bits in Mbp.
    /// </summary>
    public const int MbpWidth = 1;

    /// <summary>
    ///  Indicates an aggregated price level message, not an individual order (bit 4)
    /// </summary>
    public bool Mbp
    {
        get => (Value & MbpMask) != 0;
        init => Value = (byte)(value ? (ulong)Value | MbpMask : (ulong)Value & ~(ulong)MbpMask);
    }

    /// <summary>
    ///  The bit mask of Snapshot.
    /// </summary>
    public const byte SnapshotMask = 0x20;

    /// <summary>
    ///  The position of the lowest bit of Snapshot.
    /// </summary>
    public const int SnapshotShift = 5;

    /// <summary>
    ///  The number of bits in Snapshot.
    /// </summary>
    public const int SnapshotWidth = 1;

    /// <summary>
    ///  Indicates the message was sourced from a replay, such as a snapshot server (bit 5)
    /// </summary>
    public bool Snapshot
    {
        get => (Value & SnapshotMask) != 0;
        init => Value = (byte)(value ? (ulong)Value | SnapshotMask : (ulong)Value & ~(ulong)SnapshotMask);
    }

    /// <summary>
    ///  The bit mask of Tob.
    /// </summary>
    public const byte TobMask = 0x40;

    /// <summary>
    ///  The position of the lowest bit of Tob.
    /// </summary>
    public const int TobShift = 6;

    /// <summary>
    ///  The number of bits in Tob.
    /// </summary>
    public const int TobWidth = 1;

    /// <summary>
    ///  Indicates a top-of-book message, not an individual order (bit 6)
    /// </summary>
    public bool Tob
    {
        get => (Value & TobMask) != 0;
        init => Value = (byte)(value ? (ulong)Value | TobMask : (ulong)Value & ~(ulong)TobMask);
    }

    /// <summary>
    ///  The bit mask of Last.
    /// </summary>
    public const byte LastMask = 0x80;

    /// <summary>
    ///  The position of the lowest bit of Last.
    /// </summary>
    public const int LastShift = 7;

    /// <summary>
    ///  The number of bits in Last.
    /// </summary>
    public const int LastWidth = 1;

    /// <summary>
    ///  Marks the last record in a single event for a given instrument_id (bit 7)
    /// </summary>
    public bool Last
    {
        get => (Value & LastMask) != 0;
        init => Value = (byte)(value ? (ulong)Value | LastMask : (ulong)Value & ~(ulong)LastMask);
    }
}
