using System.Runtime.InteropServices;

namespace Ice.iMpact;

/// <summary>
///  Extra Leg Definition: Extra Leg Definition
/// </summary>

public partial class ExtraLegDefinition
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public LegBodyLength LegBodyLength;
        public LegMarketId LegMarketId;
        public Reserved2 Reserved2;
        public LegSide LegSide;
        public LegStrategyCode LegStrategyCode;
        public LegRatioQtyNumerator LegRatioQtyNumerator;
        public LegRatioQtyDenominator LegRatioQtyDenominator;
        public LegRatioPriceNumerator LegRatioPriceNumerator;
        public LegRatioPriceDenominator LegRatioPriceDenominator;
    };
};
