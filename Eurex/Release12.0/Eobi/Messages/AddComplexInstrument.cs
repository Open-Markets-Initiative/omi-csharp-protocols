using System.Runtime.InteropServices;

namespace Eurex.Eobi;

/// <summary>
///  Add Complex Instrument
/// </summary>

public partial class AddComplexInstrument
{
    /// <summary>
    ///  Security Id
    /// </summary>
    public long SecurityId => Fields.SecurityId.Value;

    /// <summary>
    ///  Transact Time
    /// </summary>
    public DateTime TransactTime => Fields.TransactTime.Value;

    /// <summary>
    ///  Security Desc
    /// </summary>
    public string SecurityDesc => Fields.SecurityDesc.Value;

    /// <summary>
    ///  Security Sub Type
    /// </summary>
    public int SecuritySubType => Fields.SecuritySubType.Value;

    /// <summary>
    ///  Product Complex
    /// </summary>
    public ProductComplex ProductComplex => Fields.ProductComplex.Value;

    /// <summary>
    ///  Implied Market Indicator
    /// </summary>
    public ImpliedMarketIndicator ImpliedMarketIndicator => Fields.ImpliedMarketIndicator.Value;

    /// <summary>
    ///  Quantity Scaling Factor
    /// </summary>
    public ushort QuantityScalingFactor => Fields.QuantityScalingFactor.Value;

    /// <summary>
    ///  Leg Ratio Multiplier
    /// </summary>
    public uint LegRatioMultiplier => Fields.LegRatioMultiplier.Value;

    /// <summary>
    ///  No Legs
    /// </summary>
    public byte NoLegs => Fields.NoLegs.Value;

    /// <summary>
    ///  Pad 2
    /// </summary>
    public string Pad2 => Fields.Pad2.Value;

    /// <summary>
    ///  Last Fragment
    /// </summary>
    public LastFragment LastFragment => Fields.LastFragment.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public SecurityId SecurityId;
        public TransactTime TransactTime;
        public SecurityDesc SecurityDesc;
        public SecuritySubType SecuritySubType;
        public ProductComplex ProductComplex;
        public ImpliedMarketIndicator ImpliedMarketIndicator;
        public QuantityScalingFactor QuantityScalingFactor;
        public LegRatioMultiplier LegRatioMultiplier;
        public NoLegs NoLegs;
        public Pad2 Pad2;
        public LastFragment LastFragment;
    };

    protected Layout Fields;
};
