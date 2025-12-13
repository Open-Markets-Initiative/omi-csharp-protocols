using System.Runtime.InteropServices;

namespace Eurex.Eobi;

/// <summary>
///  Order Mass Delete
/// </summary>

public partial class OrderMassDelete
{
    /// <summary>
    ///  Security Id
    /// </summary>
    public long SecurityId => Fields.SecurityId.Value;

    /// <summary>
    ///  Transact Time
    /// </summary>
    public DateTime TransactTime => Fields.TransactTime.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public SecurityId SecurityId;
        public TransactTime TransactTime;
    };

    protected Layout Fields;
};
