using System.Runtime.InteropServices;

namespace Eurex.Eobi;

/// <summary>
///  Add Scaled Simple Instrument
/// </summary>

public partial class AddScaledSimpleInstrument
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
    ///  Security Type
    /// </summary>
    public SecurityType SecurityType => Fields.SecurityType.Value;

    /// <summary>
    ///  Pad 1
    /// </summary>
    public string Pad1 => Fields.Pad1.Value;

    /// <summary>
    ///  Quantity Scaling Factor
    /// </summary>
    public ushort QuantityScalingFactor => Fields.QuantityScalingFactor.Value;

    /// <summary>
    ///  Pad 4
    /// </summary>
    public string Pad4 => Fields.Pad4.Value;

    /// <summary>
    ///  Related Instrument Grp Comp
    /// </summary>
    public string RelatedInstrumentGrpComp => Fields.RelatedInstrumentGrpComp.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public SecurityId SecurityId;
        public TransactTime TransactTime;
        public SecurityDesc SecurityDesc;
        public SecurityType SecurityType;
        public Pad1 Pad1;
        public QuantityScalingFactor QuantityScalingFactor;
        public Pad4 Pad4;
        public RelatedInstrumentGrpComp RelatedInstrumentGrpComp;
    };

    protected Layout Fields;
};
