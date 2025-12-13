using System.Runtime.InteropServices;

namespace Eurex.Eobi;

/// <summary>
///  Add Flexible Instrument
/// </summary>

public partial class AddFlexibleInstrument
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
    ///  Put Or Call
    /// </summary>
    public PutOrCall PutOrCall => Fields.PutOrCall.Value;

    /// <summary>
    ///  Exercise Style
    /// </summary>
    public ExerciseStyle ExerciseStyle => Fields.ExerciseStyle.Value;

    /// <summary>
    ///  Settl Method
    /// </summary>
    public SettlMethod SettlMethod => Fields.SettlMethod.Value;

    /// <summary>
    ///  Maturity Date
    /// </summary>
    public uint MaturityDate => Fields.MaturityDate.Value;

    /// <summary>
    ///  Strike Price
    /// </summary>
    public ulong StrikePrice => Fields.StrikePrice.Value;

    /// <summary>
    ///  Opt Attribute
    /// </summary>
    public uint OptAttribute => Fields.OptAttribute.Value;

    /// <summary>
    ///  Pad 4
    /// </summary>
    public string Pad4 => Fields.Pad4.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public SecurityId SecurityId;
        public TransactTime TransactTime;
        public SecurityDesc SecurityDesc;
        public SecurityType SecurityType;
        public PutOrCall PutOrCall;
        public ExerciseStyle ExerciseStyle;
        public SettlMethod SettlMethod;
        public MaturityDate MaturityDate;
        public StrikePrice StrikePrice;
        public OptAttribute OptAttribute;
        public Pad4 Pad4;
    };

    protected Layout Fields;
};
