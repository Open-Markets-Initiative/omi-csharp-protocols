using System.Runtime.InteropServices;

namespace Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
///  Represents the SecurityDirectoryMessage message from the DeepPlus protocol.
/// </summary>

public partial class SecurityDirectoryMessage
{
    /// <summary>
    ///  Security Directory Flags
    /// </summary>
    public SecurityDirectoryFlags SecurityDirectoryFlags => Fields.SecurityDirectoryFlags;

    /// <summary>
    ///  Time stamp of the system event
    /// </summary>
    public DateTime Timestamp => Fields.Timestamp.Value;

    /// <summary>
    ///  Security identifier
    /// </summary>
    public string Symbol => Fields.Symbol.Value;

    /// <summary>
    ///  Number of shares that represent a round lot
    /// </summary>
    public uint RoundLotSize => Fields.RoundLotSize.Value;

    /// <summary>
    ///  Corporate action adjusted previous official closing price
    /// </summary>
    public decimal AdjustedPocPrice => Fields.AdjustedPocPrice.Value;

    /// <summary>
    ///  Indicates which Limit Up-Limit Down price band calculation parameter is to be used
    /// </summary>
    public LuldTier LuldTier => Fields.LuldTier;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public SecurityDirectoryFlags SecurityDirectoryFlags;
        public Timestamp Timestamp;
        public Symbol Symbol;
        public RoundLotSize RoundLotSize;
        public AdjustedPocPrice AdjustedPocPrice;
        public LuldTier LuldTier;
    };

    protected Layout Fields;
};
