using System.Runtime.CompilerServices;

namespace Databento.MarketData.Normalized.Dbn.v1;

/// <summary>
///  Additional details about the cause of the status change
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum StatusReason : ushort
{
    /// <summary>
    ///  None
    /// </summary>
    None = 0,

    /// <summary>
    ///  Scheduled
    /// </summary>
    Scheduled = 1,

    /// <summary>
    ///  Surveillance Intervention
    /// </summary>
    SurveillanceIntervention = 2,

    /// <summary>
    ///  Market Event
    /// </summary>
    MarketEvent = 3,

    /// <summary>
    ///  Instrument Activation
    /// </summary>
    InstrumentActivation = 4,

    /// <summary>
    ///  Instrument Expiration
    /// </summary>
    InstrumentExpiration = 5,

    /// <summary>
    ///  Recovery In Process
    /// </summary>
    RecoveryInProcess = 6,

    /// <summary>
    ///  Regulatory
    /// </summary>
    Regulatory = 10,

    /// <summary>
    ///  Administrative
    /// </summary>
    Administrative = 11,

    /// <summary>
    ///  Non Compliance
    /// </summary>
    NonCompliance = 12,

    /// <summary>
    ///  Filings Not Current
    /// </summary>
    FilingsNotCurrent = 13,

    /// <summary>
    ///  Sec Trading Suspension
    /// </summary>
    SecTradingSuspension = 14,

    /// <summary>
    ///  New Issue
    /// </summary>
    NewIssue = 15,

    /// <summary>
    ///  Issue Available
    /// </summary>
    IssueAvailable = 16,

    /// <summary>
    ///  Issues Reviewed
    /// </summary>
    IssuesReviewed = 17,

    /// <summary>
    ///  Filing Reqs Satisfied
    /// </summary>
    FilingReqsSatisfied = 18,

    /// <summary>
    ///  News Pending
    /// </summary>
    NewsPending = 30,

    /// <summary>
    ///  News Released
    /// </summary>
    NewsReleased = 31,

    /// <summary>
    ///  News And Resumption Times
    /// </summary>
    NewsAndResumptionTimes = 32,

    /// <summary>
    ///  News Not Forthcoming
    /// </summary>
    NewsNotForthcoming = 33,

    /// <summary>
    ///  Order Imbalance
    /// </summary>
    OrderImbalance = 40,

    /// <summary>
    ///  Luld Pause
    /// </summary>
    LuldPause = 50,

    /// <summary>
    ///  Operational
    /// </summary>
    Operational = 60,

    /// <summary>
    ///  Additional Information Requested
    /// </summary>
    AdditionalInformationRequested = 70,

    /// <summary>
    ///  Merger Effective
    /// </summary>
    MergerEffective = 80,

    /// <summary>
    ///  Etf
    /// </summary>
    Etf = 90,

    /// <summary>
    ///  Corporate Action
    /// </summary>
    CorporateAction = 100,

    /// <summary>
    ///  New Security Offering
    /// </summary>
    NewSecurityOffering = 110,

    /// <summary>
    ///  Market Wide Halt Level1
    /// </summary>
    MarketWideHaltLevel1 = 120,

    /// <summary>
    ///  Market Wide Halt Level2
    /// </summary>
    MarketWideHaltLevel2 = 121,

    /// <summary>
    ///  Market Wide Halt Level3
    /// </summary>
    MarketWideHaltLevel3 = 122,

    /// <summary>
    ///  Market Wide Halt Carryover
    /// </summary>
    MarketWideHaltCarryover = 123,

    /// <summary>
    ///  Market Wide Halt Resumption
    /// </summary>
    MarketWideHaltResumption = 124,

    /// <summary>
    ///  Quotation Not Available
    /// </summary>
    QuotationNotAvailable = 130,

}

/// <summary>
///  Reads and writes the Status Reason field in place.
/// </summary>
public struct StatusReasonField
{
    /// <summary>
    ///  The size of the Status Reason field in bytes.
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Additional details about the cause of the status change
    /// </summary>
    public StatusReason Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Status Reason field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly StatusReason Decode()
        => (StatusReason)global::System.Buffers.Binary.BinaryPrimitives.ReadUInt16LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.ReadOnlySpan<ushort>(in Underlying)));

    /// <summary>
    ///  Writes an unscaled wire value into the Status Reason field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(StatusReason value)
        => global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16LittleEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.Span<ushort>(ref Underlying)), (ushort)value);

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal ushort Underlying;
}
