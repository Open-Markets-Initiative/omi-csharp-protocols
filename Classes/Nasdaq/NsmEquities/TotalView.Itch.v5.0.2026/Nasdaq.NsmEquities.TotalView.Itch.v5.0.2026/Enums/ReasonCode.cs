namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

/// <summary>
///  Trading Action reason
/// </summary>
public static class ReasonCode
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
    ///  Regulatory Halt Extraordinary Market Activity
    /// </summary>
    public const string RegulatoryHaltExtraordinaryMarketActivity = "T6";

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
    ///  Volatility Trading Pause – Straddle Condition
    /// </summary>
    public const string StraddleConditionTradingPause = "LUDS";

    /// <summary>
    ///  Market Wide Circuit Breaker Halt – Level 1
    /// </summary>
    public const string CircuitBreakerHaltLevel1 = "MWC1";

    /// <summary>
    ///  Market Wide Circuit Breaker Halt – Level 2
    /// </summary>
    public const string CircuitBreakerHaltLevel2 = "MWC2";

    /// <summary>
    ///  Market Wide Circuit Breaker Halt – Level 3
    /// </summary>
    public const string CircuitBreakerHaltLevel3 = "MWC3";

    /// <summary>
    ///  Market Wide Circuit Breaker Halt – Carry over from previous day
    /// </summary>
    public const string CarryOverCircuitBreakerHalt = "MWC0";

    /// <summary>
    ///  IPO Issue Not Yet Trading
    /// </summary>
    public const string IpoIssue = "IPO1";

    /// <summary>
    ///  Corporate Action
    /// </summary>
    public const string CorporateAction = "M1";

    /// <summary>
    ///  Quotation Not Available
    /// </summary>
    public const string NotAvailable = "M2";

    /// <summary>
    ///  News and Resumption Times
    /// </summary>
    public const string NewsAndResumptionTimes = "T3";

    /// <summary>
    ///  Single Security Trading Pause / Quotation Only Period
    /// </summary>
    public const string TradingPauseQuotationOnlyPeriod = "T7";

    /// <summary>
    ///  Qualifications Issues Reviewed/Resolved; Quotations/Trading to Resume
    /// </summary>
    public const string QualificationsIssuesResolved = "R4";

    /// <summary>
    ///  Filing Requirements Satisfied/Resolved; Quotations/Trading To Resume
    /// </summary>
    public const string FilingRequirementsSatisfied = "R9";

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
    ///  Trade Halt Concluded By Other Regulatory Authority; Quotes/Trades Resume
    /// </summary>
    public const string TradeHaltConcludedByOtherRegulatoryAuthority = "C11";

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
    ///  IPO Security Released for Quotation (NASDAQ Securities Only)
    /// </summary>
    public const string IpoSecurityReleased = "IPOQ";

    /// <summary>
    ///  IPO Security — Positioning Window Extension (NASDAQ Securities Only)
    /// </summary>
    public const string IpoSecurityPositioningWindowExtension = "IPOE";

    /// <summary>
    ///  Reason Not Available
    /// </summary>
    public const string ReasonNotAvailable = "";

    /// <summary>
    ///  Determines whether the value is one that the field defines.
    /// </summary>
    public static bool IsDefined(string value) => value is HaltNewsPending or HaltNewsDisseminated or SingleSecurityTradingPauseInEffect or RegulatoryHaltExtraordinaryMarketActivity or HaltEtf or TradingHalted or HaltNonCompliance or HaltFilingsNotCurrent or HaltSecTradingSuspension or HaltRegulatoryConcern or OperationsHalt or VolatilityTradingPause or StraddleConditionTradingPause or CircuitBreakerHaltLevel1 or CircuitBreakerHaltLevel2 or CircuitBreakerHaltLevel3 or CarryOverCircuitBreakerHalt or IpoIssue or CorporateAction or NotAvailable or NewsAndResumptionTimes or TradingPauseQuotationOnlyPeriod or QualificationsIssuesResolved or FilingRequirementsSatisfied or IssuerNewsNotForthcoming or QualificationsHaltEnded or QualificationsHaltConcluded or TradeHaltConcludedByOtherRegulatoryAuthority or MarketWideCircuitBreakerResumption or NewIssueAvailable or IssueAvailable or IpoSecurityReleased or IpoSecurityPositioningWindowExtension or ReasonNotAvailable;
}
