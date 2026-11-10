namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Manager;

/// <summary>
///  Keeps the values of the conversation that a later packet reads.
/// </summary>
public sealed class ConversationState
{
    private const ulong SequenceStep = 1UL;

    private static T? Number<T>(string text, global::System.Globalization.NumberStyles styles) where T : struct, global::System.Numerics.INumberBase<T> => T.TryParse(text, styles, global::System.Globalization.CultureInfo.InvariantCulture, out var number) ? number : null;

    /// <summary>
    ///  Gets the last Accepted Sequence Number that the conversation gave, or null before it gives one.
    /// </summary>
    public global::System.UInt128? AcceptedSequenceNumber { get; private set; }

    private global::System.UInt128? sequencedDataPacketNext;

    /// <summary>
    ///  Gets the sequence number of the last Sequenced Data Packet counted, or null before the conversation gives the Accepted Sequence Number.
    /// </summary>
    public global::System.UInt128? SequencedDataPacketNumber { get; private set; }

    /// <summary>
    ///  Counts a Sequenced Data Packet: it takes the next sequence number of the conversation.
    /// </summary>
    public void CountSequencedDataPacket()
    {
        this.sequencedDataPacketNext ??= this.AcceptedSequenceNumber;
        this.SequencedDataPacketNumber = this.sequencedDataPacketNext;
        if (this.sequencedDataPacketNext is { } number) this.sequencedDataPacketNext = unchecked((global::System.UInt128)(number + SequenceStep));
    }

    /// <summary>
    ///  Keeps the values of the conversation that the Login Accepted Packet gives.
    /// </summary>
    public void Apply(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginAcceptedPacket.Layout loginAcceptedPacket)
    {
        this.AcceptedSequenceNumber = Number<global::System.UInt128>(loginAcceptedPacket.AcceptedSequenceNumber.Value, global::System.Globalization.NumberStyles.Integer);
    }

    /// <summary>
    ///  Clears the values that the conversation gave.
    /// </summary>
    public void Reset()
    {
        this.AcceptedSequenceNumber = null;
        this.sequencedDataPacketNext = null;
        this.SequencedDataPacketNumber = null;
    }
}
