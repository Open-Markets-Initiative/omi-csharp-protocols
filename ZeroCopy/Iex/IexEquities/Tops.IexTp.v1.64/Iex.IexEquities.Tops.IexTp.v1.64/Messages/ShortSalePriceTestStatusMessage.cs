using System.Runtime.InteropServices;

namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Represents the ShortSalePriceTestStatusMessage message from the Tops protocol.
/// </summary>

public partial class ShortSalePriceTestStatusMessage
{
    /// <summary>
    ///  Reg. SHO short sale price test restriction status
    /// </summary>
    public ShortSalePriceTestStatus ShortSalePriceTestStatus => Fields.ShortSalePriceTestStatus;

    /// <summary>
    ///  Time stamp of the system event
    /// </summary>
    public DateTime Timestamp => Fields.Timestamp.Value;

    /// <summary>
    ///  Security identifier
    /// </summary>
    public string Symbol => Fields.Symbol.Value;

    /// <summary>
    ///  Detail of the Reg. SHO short sale price test restriction status
    /// </summary>
    public char Detail => Fields.Detail.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public ShortSalePriceTestStatus ShortSalePriceTestStatus;
        public Timestamp Timestamp;
        public Symbol Symbol;
        public Detail Detail;
    };

    protected Layout Fields;
};
