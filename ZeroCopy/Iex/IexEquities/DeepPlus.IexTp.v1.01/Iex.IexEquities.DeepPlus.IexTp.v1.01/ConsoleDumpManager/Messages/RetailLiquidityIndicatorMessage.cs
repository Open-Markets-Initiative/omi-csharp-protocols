namespace Iex.IexEquities.DeepPlus.IexTp.ConsoleDumpManager;

using global::Iex.IexEquities.DeepPlus.IexTp;
using System.Runtime.CompilerServices;

/// <summary>
///  Implements &lt;c&gt;OnRetailLiquidityIndicatorMessage&lt;/c&gt; for console dumping.
/// </summary>
public sealed partial class ConsoleDumpManager
{
    /// <summary>
    ///  Dumps every field of a &lt;c&gt;RetailLiquidityIndicatorMessage&lt;/c&gt; to the console.
    /// </summary>
    unsafe partial void OnRetailLiquidityIndicatorMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq)
    {
        if (payloadLength < Unsafe.SizeOf<RetailLiquidityIndicatorMessage.Layout>())
        {
            Console.WriteLine($"[seq={seq}] RetailLiquidityIndicatorMessage: short payload ({payloadLength} < {Unsafe.SizeOf<RetailLiquidityIndicatorMessage.Layout>()} bytes) — skipped");
            return;
        }
        ref readonly var msg = ref *(RetailLiquidityIndicatorMessage.Layout*)payload;

        Console.WriteLine($"[seq={seq}] RetailLiquidityIndicatorMessage");
        Console.WriteLine($"  RetailLiquidityIndicator = {msg.RetailLiquidityIndicator}");
        Console.WriteLine($"  Timestamp = {msg.Timestamp}");
        Console.WriteLine($"  Symbol = {msg.Symbol}");
    }
}
