using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Repo Underlyings Group: Number of underlying entries
/// </summary>

public partial class RepoUnderlyingsGroup
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public UnderlyingSymbol UnderlyingSymbol;
        public UnderlyingSecurityIdOptional UnderlyingSecurityIdOptional;
        public UnderlyingSecurityAltId UnderlyingSecurityAltId;
        public UnderlyingSecurityAltIdSource UnderlyingSecurityAltIdSource;
        public UnderlyingFinancialInstrumentFullName UnderlyingFinancialInstrumentFullName;
        public UnderlyingSecurityType UnderlyingSecurityType;
        public UnderlyingCountryOfIssue UnderlyingCountryOfIssue;
        public UnderlyingIssuer UnderlyingIssuer;
        public UnderlyingMaxLifeTime UnderlyingMaxLifeTime;
        public UnderlyingMinDaysToMaturity UnderlyingMinDaysToMaturity;
        public UnderlyingInstrumentGuidOptional UnderlyingInstrumentGuidOptional;
        public UnderlyingMaturityDate UnderlyingMaturityDate;
    };
};
