namespace Iex.IexEquities.DeepPlus.IexTp.ConsoleDumpManager;

using global::Iex.IexEquities.DeepPlus.IexTp;
using System.Runtime.CompilerServices;

/// <summary>
///  Implements &lt;c&gt;OnTradeBreakMessage&lt;/c&gt; for console dumping.
/// </summary>
public sealed partial class ConsoleDumpManager
{
    /// <summary>
    ///  Dumps every field of a &lt;c&gt;TradeBreakMessage&lt;/c&gt; to the console.
    /// </summary>
    unsafe partial void OnTradeBreakMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq)
    {
        if (payloadLength < Unsafe.SizeOf<TradeBreakMessage.Layout>())
        {
            Console.WriteLine($"[seq={seq}] TradeBreakMessage: short payload ({payloadLength} < {Unsafe.SizeOf<TradeBreakMessage.Layout>()} bytes) — skipped");
            return;
        }
        ref readonly var msg = ref *(TradeBreakMessage.Layout*)payload;

        Console.WriteLine($"[seq={seq}] TradeBreakMessage");
        Console.WriteLine($"  SaleConditionFlags = {msg.SaleConditionFlags}");
        Console.WriteLine($"  Timestamp = {msg.Timestamp}");
        Console.WriteLine($"  Symbol = {msg.Symbol}");
        Console.WriteLine($"  Size = {msg.Size}");
        Console.WriteLine($"  Price = {msg.Price}");
        Console.WriteLine($"  TradeId = {msg.TradeId}");
    }
}
