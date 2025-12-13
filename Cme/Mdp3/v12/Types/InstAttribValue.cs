namespace Cme.Mdp3;

using System.Runtime.CompilerServices;

/// <summary>
///  Inst Attrib Value: Bitfield
/// </summary>

public sealed class InstAttribValue
{
    /// <summary>
    ///  Electronic Match Eligible
    /// </summary>
    public const byte ElectronicMatchEligible = 1 << 0;

    /// <summary>
    ///  Order Cross Eligible
    /// </summary>
    public const byte OrderCrossEligible = 1 << 1;

    /// <summary>
    ///  Block Trade Eligible
    /// </summary>
    public const byte BlockTradeEligible = 1 << 2;

    /// <summary>
    ///  Efp Eligible
    /// </summary>
    public const byte EfpEligible = 1 << 3;

    /// <summary>
    ///  Ebf Eligible
    /// </summary>
    public const byte EbfEligible = 1 << 4;

    /// <summary>
    ///  Efs Eligible
    /// </summary>
    public const byte EfsEligible = 1 << 5;

    /// <summary>
    ///  Efr Eligible
    /// </summary>
    public const byte EfrEligible = 1 << 6;

    /// <summary>
    ///  Otc Eligible
    /// </summary>
    public const byte OtcEligible = 1 << 7;

    /// <summary>
    ///  ILink Indicative Mass Quoting Eligible
    /// </summary>
    public const byte IlinkIndicativeMassQuotingEligible = 1 << 8;

    /// <summary>
    ///  Negative Strike Eligible
    /// </summary>
    public const byte NegativeStrikeEligible = 1 << 9;

    /// <summary>
    ///  Negative Price Outright Eligible
    /// </summary>
    public const byte NegativePriceOutrightEligible = 1 << 10;

    /// <summary>
    ///  Is Fractional
    /// </summary>
    public const byte IsFractional = 1 << 11;

    /// <summary>
    ///  Volatility Quoted Option
    /// </summary>
    public const byte VolatilityQuotedOption = 1 << 12;

    /// <summary>
    ///  Rfq Cross Eligible
    /// </summary>
    public const byte RfqCrossEligible = 1 << 13;

    /// <summary>
    ///  Zero Price Outright Eligible
    /// </summary>
    public const byte ZeroPriceOutrightEligible = 1 << 14;

    /// <summary>
    ///  Decaying Product Eligibility
    /// </summary>
    public const byte DecayingProductEligibility = 1 << 15;

    /// <summary>
    ///  Variable Product Eligibility
    /// </summary>
    public const byte VariableProductEligibility = 1 << 16;

    /// <summary>
    ///  Daily Product Eligibility
    /// </summary>
    public const byte DailyProductEligibility = 1 << 17;

    /// <summary>
    ///  Gt Orders Eligibility
    /// </summary>
    public const byte GtOrdersEligibility = 1 << 18;

    /// <summary>
    ///  Implied Matching Eligibility
    /// </summary>
    public const byte ImpliedMatchingEligibility = 1 << 19;

    /// <summary>
    ///  Triangulation Eligible
    /// </summary>
    public const byte TriangulationEligible = 1 << 20;

    /// <summary>
    ///  Variable Cab Eligible
    /// </summary>
    public const byte VariableCabEligible = 1 << 21;

    /// <summary>
    ///  Inverted Book
    /// </summary>
    public const byte InvertedBook = 1 << 22;

    /// <summary>
    ///  Is Aon Instrument
    /// </summary>
    public const byte IsAonInstrument = 1 << 23;

    /// <summary>
    ///  Sef Regulated
    /// </summary>
    public const byte SefRegulated = 1 << 24;

    /// <summary>
    ///  Mtf Regulated
    /// </summary>
    public const byte MtfRegulated = 1 << 25;

    /// <summary>
    ///  Efix Instrument
    /// </summary>
    public const byte EfixInstrument = 1 << 26;

    /// <summary>
    ///  Hedge Instrument
    /// </summary>
    public const byte HedgeInstrument = 1 << 27;

    /// <summary>
    ///  Reserved 4
    /// </summary>
    public const byte Reserved4 = 1 << 28;

    /// <summary>
    ///  Length of Inst Attrib Value in bytes
    /// </summary>
    public const int Length = 4;
}
