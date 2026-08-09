using System.Runtime.InteropServices;

namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Represents the RetailLiquidityIndicatorMessage message from the Tops protocol.
/// </summary>

public partial class RetailLiquidityIndicatorMessage
{
    /// <summary>
    ///  Retail Liquidity Indicator identifier
    /// </summary>
    public char RetailLiquidityIndicator => Fields.RetailLiquidityIndicator.Value;

    /// <summary>
    ///  Time stamp of the system event
    /// </summary>
    public DateTime Timestamp => Fields.Timestamp.Value;

    /// <summary>
    ///  Security identifier
    /// </summary>
    public string Symbol => Fields.Symbol.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public RetailLiquidityIndicator RetailLiquidityIndicator;
        public Timestamp Timestamp;
        public Symbol Symbol;
    };

    protected Layout Fields;
};
