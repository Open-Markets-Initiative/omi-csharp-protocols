using System.Runtime.CompilerServices;

namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  A bit field indicating event end, message characteristics, and data quality; declared least significant bit first
/// </summary>
public struct Flags
{
    /// <summary>
    ///  The bits of the Reserved Flag flag in <see cref="Value"/>.
    /// </summary>
    public const byte ReservedFlagMask = unchecked((byte)0x1UL);
    /// <summary>
    ///  Bit 0, unused
    /// </summary>
    public bool ReservedFlag
    {
        readonly get => (Value & ReservedFlagMask) != 0;
        set => Value = value ? (byte)(Value | ReservedFlagMask) : (byte)(Value & ~ReservedFlagMask);
    }

    /// <summary>
    ///  The bits of the Publisher Specific flag in <see cref="Value"/>.
    /// </summary>
    public const byte PublisherSpecificMask = unchecked((byte)0x2UL);
    /// <summary>
    ///  Indicates a publisher-specific event (bit 1)
    /// </summary>
    public bool PublisherSpecific
    {
        readonly get => (Value & PublisherSpecificMask) != 0;
        set => Value = value ? (byte)(Value | PublisherSpecificMask) : (byte)(Value & ~PublisherSpecificMask);
    }

    /// <summary>
    ///  The bits of the Maybe Bad Book flag in <see cref="Value"/>.
    /// </summary>
    public const byte MaybeBadBookMask = unchecked((byte)0x4UL);
    /// <summary>
    ///  Indicates an unrecoverable gap was detected in the channel (bit 2)
    /// </summary>
    public bool MaybeBadBook
    {
        readonly get => (Value & MaybeBadBookMask) != 0;
        set => Value = value ? (byte)(Value | MaybeBadBookMask) : (byte)(Value & ~MaybeBadBookMask);
    }

    /// <summary>
    ///  The bits of the Bad Ts Recv flag in <see cref="Value"/>.
    /// </summary>
    public const byte BadTsRecvMask = unchecked((byte)0x8UL);
    /// <summary>
    ///  Indicates the ts_recv value is inaccurate due to clock issues or packet reordering (bit 3)
    /// </summary>
    public bool BadTsRecv
    {
        readonly get => (Value & BadTsRecvMask) != 0;
        set => Value = value ? (byte)(Value | BadTsRecvMask) : (byte)(Value & ~BadTsRecvMask);
    }

    /// <summary>
    ///  The bits of the Mbp flag in <see cref="Value"/>.
    /// </summary>
    public const byte MbpMask = unchecked((byte)0x10UL);
    /// <summary>
    ///  Indicates an aggregated price level message, not an individual order (bit 4)
    /// </summary>
    public bool Mbp
    {
        readonly get => (Value & MbpMask) != 0;
        set => Value = value ? (byte)(Value | MbpMask) : (byte)(Value & ~MbpMask);
    }

    /// <summary>
    ///  The bits of the Snapshot flag in <see cref="Value"/>.
    /// </summary>
    public const byte SnapshotMask = unchecked((byte)0x20UL);
    /// <summary>
    ///  Indicates the message was sourced from a replay, such as a snapshot server (bit 5)
    /// </summary>
    public bool Snapshot
    {
        readonly get => (Value & SnapshotMask) != 0;
        set => Value = value ? (byte)(Value | SnapshotMask) : (byte)(Value & ~SnapshotMask);
    }

    /// <summary>
    ///  The bits of the Tob flag in <see cref="Value"/>.
    /// </summary>
    public const byte TobMask = unchecked((byte)0x40UL);
    /// <summary>
    ///  Indicates a top-of-book message, not an individual order (bit 6)
    /// </summary>
    public bool Tob
    {
        readonly get => (Value & TobMask) != 0;
        set => Value = value ? (byte)(Value | TobMask) : (byte)(Value & ~TobMask);
    }

    /// <summary>
    ///  The bits of the Last flag in <see cref="Value"/>.
    /// </summary>
    public const byte LastMask = unchecked((byte)0x80UL);
    /// <summary>
    ///  Marks the last record in a single event for a given instrument_id (bit 7)
    /// </summary>
    public bool Last
    {
        readonly get => (Value & LastMask) != 0;
        set => Value = value ? (byte)(Value | LastMask) : (byte)(Value & ~LastMask);
    }

    /// <summary>
    ///  The size of the Flags field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  A bit field indicating event end, message characteristics, and data quality; declared least significant bit first
    /// </summary>
    public byte Value
    {
        readonly get => Underlying;
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => $"0x{Value:X}";

    /// <summary>
    ///  Writes a value into the Flags field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(byte value)
        => Underlying = value;

    internal byte Underlying;
}
