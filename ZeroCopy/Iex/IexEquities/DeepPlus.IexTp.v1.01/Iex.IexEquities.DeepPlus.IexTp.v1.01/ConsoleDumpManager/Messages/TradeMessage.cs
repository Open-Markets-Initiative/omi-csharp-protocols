namespace Iex.IexEquities.DeepPlus.IexTp.ConsoleDumpManager;

using global::Iex.IexEquities.DeepPlus.IexTp;
using System.Runtime.CompilerServices;

/// <summary>
///  Implements &lt;c&gt;OnTradeMessage&lt;/c&gt; for console dumping.
/// </summary>
public sealed partial class ConsoleDumpManager
{
    /// <summary>
    ///  Dumps every field of a &lt;c&gt;TradeMessage&lt;/c&gt; to the console.
    /// </summary>
    unsafe partial void OnTradeMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq)
    {
        if (payloadLength < Unsafe.SizeOf<TradeMessage.Layout>())
        {
            Console.WriteLine($"[seq={seq}] TradeMessage: short payload ({payloadLength} < {Unsafe.SizeOf<TradeMessage.Layout>()} bytes) — skipped");
            return;
        }
        ref readonly var msg = ref *(TradeMessage.Layout*)payload;

        Console.WriteLine($"[seq={seq}] TradeMessage");
        Console.WriteLine($"  SaleConditionFlags = {msg.SaleConditionFlags}");
        Console.WriteLine($"  Timestamp = {msg.Timestamp}");
        Console.WriteLine($"  Symbol = {msg.Symbol}");
        Console.WriteLine($"  Size = {msg.Size}");
        Console.WriteLine($"  Price = {msg.Price}");
        Console.WriteLine($"  TradeId = {msg.TradeId}");
    }
}
