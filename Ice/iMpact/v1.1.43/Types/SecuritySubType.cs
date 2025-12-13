namespace Ice.iMpact;

/// <summary>
///  Security Sub Type: Contains the Strategy Code for defined market where applicable. See Appendix E for list of codes.
/// </summary>

public enum SecuritySubType : short
{
    /// <summary>
    ///  Na
    /// </summary>
    None = 0,

    /// <summary>
    ///  Call
    /// </summary>
    Call = 1,

    /// <summary>
    ///  Put
    /// </summary>
    Put = 2,

    /// <summary>
    ///  Ffly
    /// </summary>
    FuturesButterfly = 3,

    /// <summary>
    ///  Callfly
    /// </summary>
    CallButterfly = 4,

    /// <summary>
    ///  Putfly
    /// </summary>
    PutButterfly = 5,

    /// <summary>
    ///  Callspr
    /// </summary>
    CallSpread = 6,

    /// <summary>
    ///  Putspr
    /// </summary>
    PutSpread = 7,

    /// <summary>
    ///  Calldiagsp
    /// </summary>
    DiagonalCallSpread = 9,

    /// <summary>
    ///  Putdiagsp
    /// </summary>
    DiagonalPutSpread = 10,

    /// <summary>
    ///  Gut
    /// </summary>
    GutStrangle = 11,

    /// <summary>
    ///  Ratiocspr
    /// </summary>
    1X2CallSpreadToThe2 = 12,

    /// <summary>
    ///  Ratiopspr
    /// </summary>
    1X2PutSpreadToThe2 = 13,

    /// <summary>
    ///  Ifly
    /// </summary>
    IronButterfly = 14,

    /// <summary>
    ///  Strangle
    /// </summary>
    Strangle = 16,

    /// <summary>
    ///  Callladr
    /// </summary>
    CallLadderTree = 17,

    /// <summary>
    ///  Putladr
    /// </summary>
    PutLadderTree = 18,

    /// <summary>
    ///  Calstrd
    /// </summary>
    StraddleSpread = 19,

    /// <summary>
    ///  Revcon
    /// </summary>
    ReversalConversionToTheCall = 21,

    /// <summary>
    ///  Revcon
    /// </summary>
    ReversalConversionToThePut = 21,

    /// <summary>
    ///  Straddle
    /// </summary>
    Straddle = 22,

    /// <summary>
    ///  Fcondr
    /// </summary>
    FuturesCondor = 23,

    /// <summary>
    ///  Callcondr
    /// </summary>
    CallCondor = 24,

    /// <summary>
    ///  Putcondr
    /// </summary>
    PutCondor = 25,

    /// <summary>
    ///  Box
    /// </summary>
    Box = 26,

    /// <summary>
    ///  Syn
    /// </summary>
    SyntheticUnderlying = 33,

    /// <summary>
    ///  Callsprp
    /// </summary>
    CallSpreadVsPut3Way = 34,

    /// <summary>
    ///  Putsprc
    /// </summary>
    PutSpreadVsCall3Way = 35,

    /// <summary>
    ///  Call 3 Way
    /// </summary>
    StraddleVsCall3Way = 36,

    /// <summary>
    ///  Put 3 Way
    /// </summary>
    StraddleVsPut3Way = 37,

    /// <summary>
    ///  Callcalspr
    /// </summary>
    CallCalendarSpread = 38,

    /// <summary>
    ///  Putcalspr
    /// </summary>
    PutCalendarSpread = 39,

    /// <summary>
    ///  Icondr
    /// </summary>
    IronCondor = 40,

    /// <summary>
    ///  Jroll
    /// </summary>
    JellyRoll = 41,

    /// <summary>
    ///  Ratiocsprx
    /// </summary>
    Hedged1X2CallSpreadToThe2 = 42,

    /// <summary>
    ///  Ratiopsprx
    /// </summary>
    Hedged1X2PutSpreadToThe2 = 43,

    /// <summary>
    ///  Sprvspx
    /// </summary>
    CallSpreadVersusSellPutHedge = 44,

    /// <summary>
    ///  Sprvscx
    /// </summary>
    PutSpreadVersusSellCallHedge = 45,

    /// <summary>
    ///  Callcalx
    /// </summary>
    HedgedCallCalendar = 46,

    /// <summary>
    ///  Putcalx
    /// </summary>
    HedgedPutCalendar = 47,

    /// <summary>
    ///  Callladrx
    /// </summary>
    HedgedCallLadderTree = 48,

    /// <summary>
    ///  Putladrx
    /// </summary>
    HedgedPutLadderTree = 49,

    /// <summary>
    ///  Callsprx
    /// </summary>
    HedgedCallSpread = 50,

    /// <summary>
    ///  Putsprx
    /// </summary>
    HedgedPutSpread = 51,

    /// <summary>
    ///  Strdx
    /// </summary>
    HedgedStraddle = 53,

    /// <summary>
    ///  Strgx
    /// </summary>
    HedgedStrangle = 54,

    /// <summary>
    ///  Callx
    /// </summary>
    HedgedCall = 55,

    /// <summary>
    ///  Putx
    /// </summary>
    HedgedPut = 56,

    /// <summary>
    ///  Cust
    /// </summary>
    Custom = 58,

    /// <summary>
    ///  Calstrdx
    /// </summary>
    HedgedStraddleSpread = 59,

    /// <summary>
    ///  Ccondrx
    /// </summary>
    HedgedCallCondor = 60,

    /// <summary>
    ///  Pcondrx
    /// </summary>
    HedgedPutCondor = 61,

    /// <summary>
    ///  Cdiagx
    /// </summary>
    HedgedDiagonalCallSpread = 63,

    /// <summary>
    ///  Pdiagx
    /// </summary>
    HedgedDiagonalPutSpread = 64,

    /// <summary>
    ///  Cflyx
    /// </summary>
    HedgedCallButterlfy = 65,

    /// <summary>
    ///  Pflyx
    /// </summary>
    HedgedPutButterlfy = 66,

    /// <summary>
    ///  Gutx
    /// </summary>
    HedgedGutsStrangle = 67,

    /// <summary>
    ///  Icondrx
    /// </summary>
    HedgedIronCondor = 68,

    /// <summary>
    ///  Iflyx
    /// </summary>
    HedgedIronButterfly = 69,

    /// <summary>
    ///  Fencecall
    /// </summary>
    FenceToTheCall = 70,

    /// <summary>
    ///  Fenceput
    /// </summary>
    FenceToThePut = 71,

    /// <summary>
    ///  Fencecallx
    /// </summary>
    HedgedFenceToTheCall = 72,

    /// <summary>
    ///  Fenceputx
    /// </summary>
    HedgedFenceToThePut = 73,

    /// <summary>
    ///  Ratio C Spr 1
    /// </summary>
    1X2CallSpreadToThe1 = 74,

    /// <summary>
    ///  Ratio P Spr 1
    /// </summary>
    1X2PutSpreadToThe1 = 75,

    /// <summary>
    ///  Ratio C Spr 1 X
    /// </summary>
    Hedged1X2CallSpreadToThe1 = 76,

    /// <summary>
    ///  Ratio P Spr 1 X
    /// </summary>
    Hedged1X2PutSpreadToThe1 = 77,

    /// <summary>
    ///  Discspr
    /// </summary>
    DiscountSpreads = 88,

    /// <summary>
    ///  Locspr
    /// </summary>
    LocationSpreads = 89,

    /// <summary>
    ///  Platdifspr
    /// </summary>
    PlattsDiffSpread = 90,

    /// <summary>
    ///  Plattspr
    /// </summary>
    PlattsSpread = 91,

    /// <summary>
    ///  Oilcrack
    /// </summary>
    OtcGasOilCrack = 92,

    /// <summary>
    ///  Balmospr
    /// </summary>
    BalmoOverMonth = 93,

    /// <summary>
    ///  Ratiospr
    /// </summary>
    RatioSpread = 94,

    /// <summary>
    ///  Volspr
    /// </summary>
    VolumetricSpread = 95,

    /// <summary>
    ///  Heatrate
    /// </summary>
    HeatRate = 96,

    /// <summary>
    ///  Crack
    /// </summary>
    CrackSpread = 97,

    /// <summary>
    ///  Combospr
    /// </summary>
    ComboSpread = 98,

    /// <summary>
    ///  Spr
    /// </summary>
    SpreadS = 99,

    /// <summary>
    ///  Pack
    /// </summary>
    PacknoColor = 100,

    /// <summary>
    ///  Whtpack
    /// </summary>
    PackWhite = 101,

    /// <summary>
    ///  Redpack
    /// </summary>
    PackRed = 102,

    /// <summary>
    ///  Grnpack
    /// </summary>
    PackGreen = 103,

    /// <summary>
    ///  Blupack
    /// </summary>
    PackBlue = 104,

    /// <summary>
    ///  Gldpack
    /// </summary>
    PackGold = 105,

    /// <summary>
    ///  Purpack
    /// </summary>
    PackPurple = 106,

    /// <summary>
    ///  Ornpack
    /// </summary>
    PackOrange = 107,

    /// <summary>
    ///  Pnkpack
    /// </summary>
    PackPink = 108,

    /// <summary>
    ///  Silpack
    /// </summary>
    PackSilver = 109,

    /// <summary>
    ///  Coppack
    /// </summary>
    PackCopper = 110,

    /// <summary>
    ///  Bndl
    /// </summary>
    BundleNoColor = 200,

    /// <summary>
    ///  Bndly 2
    /// </summary>
    Bundle2Yr = 201,

    /// <summary>
    ///  Bndly 3
    /// </summary>
    Bundle3Yr = 202,

    /// <summary>
    ///  Bndly 4
    /// </summary>
    Bundle4Yr = 203,

    /// <summary>
    ///  Bndly 5
    /// </summary>
    Bundle5Yr = 204,

    /// <summary>
    ///  Bndly 6
    /// </summary>
    Bundle6Yr = 205,

    /// <summary>
    ///  Bndly 7
    /// </summary>
    Bundle7Yr = 206,

    /// <summary>
    ///  Bndly 8
    /// </summary>
    Bundle8Yr = 207,

    /// <summary>
    ///  Bndly 9
    /// </summary>
    Bundle9Yr = 208,

    /// <summary>
    ///  Bndly 10
    /// </summary>
    Bundle10Yr = 209,

    /// <summary>
    ///  Balmo
    /// </summary>
    Balmo = 400,

    /// <summary>
    ///  Nextday
    /// </summary>
    NextDay = 410,

    /// <summary>
    ///  Cfd
    /// </summary>
    Cfd = 411,

    /// <summary>
    ///  Wknd
    /// </summary>
    Weekend = 412,

    /// <summary>
    ///  Singleday
    /// </summary>
    SingleDay = 413,

    /// <summary>
    ///  Custdaily
    /// </summary>
    CustomDaily = 414,

    /// <summary>
    ///  Hourly
    /// </summary>
    Hourly = 415,

    /// <summary>
    ///  Month
    /// </summary>
    Month = 416,

    /// <summary>
    ///  Balwk
    /// </summary>
    Balweek = 450,

    /// <summary>
    ///  Kyoto
    /// </summary>
    Basket = 500,

    /// <summary>
    ///  Custmonthly
    /// </summary>
    CustomMonthly = 550,

    /// <summary>
    ///  Nextwk
    /// </summary>
    NextWeek = 600,

    /// <summary>
    ///  Period
    /// </summary>
    Period = 700,

    /// <summary>
    ///  Cust Daily Cfd
    /// </summary>
    CustomDailyCfd = 711,

    /// <summary>
    ///  Cust Daily 7 X 8
    /// </summary>
    CustomDaily7X8 = 712,

    /// <summary>
    ///  Cust Daily 7 X 16
    /// </summary>
    CustomDaily7X16 = 713,

    /// <summary>
    ///  Cust Daily 7 X 6
    /// </summary>
    CustomDaily7X6 = 714,

    /// <summary>
    ///  Cust Daily Opx 16
    /// </summary>
    CustomDailyOffPeakX16 = 715,

    /// <summary>
    ///  Qtr
    /// </summary>
    Quarter = 800,

    /// <summary>
    ///  Year
    /// </summary>
    Year = 900,

}
