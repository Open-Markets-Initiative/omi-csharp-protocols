namespace Nasdaq.NtxEquities.TotalView.Itch.v5_0;

/// <summary>
///  Trading Action reason
/// </summary>
public static class TradingActionReason
{
    /// <summary>
    ///  Halt News Pending
    /// </summary>
    public const string HaltNewsPending = "T1";

    /// <summary>
    ///  Halt News Disseminated
    /// </summary>
    public const string HaltNewsDisseminated = "T2";

    /// <summary>
    ///  Single Security Trading Pause In Effect
    /// </summary>
    public const string SingleSecurityTradingPauseInEffect = "T5";

    /// <summary>
    ///  Regulatory Halt - Extraordinary Market Activity
    /// </summary>
    public const string RegulatoryHalt = "T6";

    /// <summary>
    ///  Halt ETF
    /// </summary>
    public const string HaltEtf = "T8";

    /// <summary>
    ///  Trading Halted; For Information Requested by Listing Market
    /// </summary>
    public const string TradingHalted = "T12";

    /// <summary>
    ///  Halt Non-Compliance
    /// </summary>
    public const string HaltNonCompliance = "H4";

    /// <summary>
    ///  Halt Filings Not Current
    /// </summary>
    public const string HaltFilingsNotCurrent = "H9";

    /// <summary>
    ///  Halt SEC Trading Suspension
    /// </summary>
    public const string HaltSecTradingSuspension = "H10";

    /// <summary>
    ///  Halt Regulatory Concern
    /// </summary>
    public const string HaltRegulatoryConcern = "H11";

    /// <summary>
    ///  Operations Halt; Contact Market Operations
    /// </summary>
    public const string OperationsHalt = "O1";

    /// <summary>
    ///  Volatility Trading Pause
    /// </summary>
    public const string VolatilityTradingPause = "LUDP";

    /// <summary>
    ///  Volatility Trading Pause - Straddle Condition
    /// </summary>
    public const string VolatilityTradingPauseStraddleCondition = "LUDS";

    /// <summary>
    ///  Market Wide Circuit Breaker Halt - Level 1
    /// </summary>
    public const string MarketWideCircuitBreakerHaltLevel1 = "MWC1";

    /// <summary>
    ///  Market Wide Circuit Breaker Halt - Level 2
    /// </summary>
    public const string MarketWideCircuitBreakerHaltLevel2 = "MWC2";

    /// <summary>
    ///  Market Wide Circuit Breaker Halt - Level 3
    /// </summary>
    public const string MarketWideCircuitBreakerHaltLevel3 = "MWC3";

    /// <summary>
    ///  Market Wide Circuit Breaker Halt - Carry over from previous day
    /// </summary>
    public const string MarketWideCircuitBreakerHaltCarryOverFromPreviousDay = "MWC0";

    /// <summary>
    ///  IPO Issue Not Yet Trading
    /// </summary>
    public const string IpoIssueNotYetTrading = "IPO1";

    /// <summary>
    ///  Corporate Action
    /// </summary>
    public const string CorporateAction = "M1";

    /// <summary>
    ///  Quotation Not Available
    /// </summary>
    public const string QuotationNotAvailable = "M2";

    /// <summary>
    ///  Reason Not Available
    /// </summary>
    public const string ReasonNotAvailable = "";

    /// <summary>
    ///  News and Resumption Times
    /// </summary>
    public const string NewsAndResumptionTimes = "T3";

    /// <summary>
    ///  Single Security Trading Pause / Quotation Only Period
    /// </summary>
    public const string SingleSecurityTradingPauseQuotationOnlyPeriod = "T7";

    /// <summary>
    ///  Qualifications Issues Reviewed/Resolved; Quotations/Trading to Resume
    /// </summary>
    public const string QualificationsIssuesReviewedResolved = "R4";

    /// <summary>
    ///  Filing Requirements Satisfied/Resolved; Quotations/Trading To Resume
    /// </summary>
    public const string FilingRequirementsSatisfiedResolved = "R9";

    /// <summary>
    ///  Issuer News Not Forthcoming; Quotations/Trading To Resume
    /// </summary>
    public const string IssuerNewsNotForthcoming = "C3";

    /// <summary>
    ///  Qualifications Halt ended; Maintenance Requirements Met; Resume
    /// </summary>
    public const string QualificationsHaltEnded = "C4";

    /// <summary>
    ///  Qualifications Halt Concluded; Filings Met; Quotes/Trades To Resume
    /// </summary>
    public const string QualificationsHaltConcluded = "C9";

    /// <summary>
    ///  Trade Halt Concluded By Other Regulatory Auth.; Quotes/Trades Resume
    /// </summary>
    public const string TradeHaltConcludedByOtherRegulatoryAuth = "C11";

    /// <summary>
    ///  Market Wide Circuit Breaker Resumption
    /// </summary>
    public const string MarketWideCircuitBreakerResumption = "MWCQ";

    /// <summary>
    ///  New Issue Available
    /// </summary>
    public const string NewIssueAvailable = "R1";

    /// <summary>
    ///  Issue Available
    /// </summary>
    public const string IssueAvailable = "R2";

    /// <summary>
    ///  IPO Security Released for Quotation (Nasdaq Texas Securities Only)
    /// </summary>
    public const string IpoSecurityReleasedForQuotation = "IPOQ";

    /// <summary>
    ///  IPO Security - Positioning Window Extension (Nasdaq Texas Securities Only)
    /// </summary>
    public const string IpoSecurityPositioningWindowExtension = "IPOE";

    /// <summary>
    ///  Determines whether the value is one that the field defines.
    /// </summary>
    public static bool IsDefined(string value) => value is HaltNewsPending or HaltNewsDisseminated or SingleSecurityTradingPauseInEffect or RegulatoryHalt or HaltEtf or TradingHalted or HaltNonCompliance or HaltFilingsNotCurrent or HaltSecTradingSuspension or HaltRegulatoryConcern or OperationsHalt or VolatilityTradingPause or VolatilityTradingPauseStraddleCondition or MarketWideCircuitBreakerHaltLevel1 or MarketWideCircuitBreakerHaltLevel2 or MarketWideCircuitBreakerHaltLevel3 or MarketWideCircuitBreakerHaltCarryOverFromPreviousDay or IpoIssueNotYetTrading or CorporateAction or QuotationNotAvailable or ReasonNotAvailable or NewsAndResumptionTimes or SingleSecurityTradingPauseQuotationOnlyPeriod or QualificationsIssuesReviewedResolved or FilingRequirementsSatisfiedResolved or IssuerNewsNotForthcoming or QualificationsHaltEnded or QualificationsHaltConcluded or TradeHaltConcludedByOtherRegulatoryAuth or MarketWideCircuitBreakerResumption or NewIssueAvailable or IssueAvailable or IpoSecurityReleasedForQuotation or IpoSecurityPositioningWindowExtension;
}
