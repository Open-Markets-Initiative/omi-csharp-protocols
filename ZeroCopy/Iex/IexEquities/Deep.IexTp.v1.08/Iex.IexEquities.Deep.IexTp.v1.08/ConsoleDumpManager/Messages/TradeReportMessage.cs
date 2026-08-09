namespace Iex.IexEquities.Deep.IexTp.ConsoleDumpManager;

using global::Iex.IexEquities.Deep.IexTp;
using System.Runtime.CompilerServices;

/// <summary>
///  Implements &lt;c&gt;OnTradeReportMessage&lt;/c&gt; for console dumping.
/// </summary>
public sealed partial class ConsoleDumpManager
{
    /// <summary>
    ///  Dumps every field of a &lt;c&gt;TradeReportMessage&lt;/c&gt; to the console.
    /// </summary>
    unsafe partial void OnTradeReportMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq)
    {
        if (payloadLength < Unsafe.SizeOf<TradeReportMessage.Layout>())
        {
            Console.WriteLine($"[seq={seq}] TradeReportMessage: short payload ({payloadLength} < {Unsafe.SizeOf<TradeReportMessage.Layout>()} bytes) — skipped");
            return;
        }
        ref readonly var msg = ref *(TradeReportMessage.Layout*)payload;

        Console.WriteLine($"[seq={seq}] TradeReportMessage");
        Console.WriteLine($"  SaleConditionFlags = {msg.SaleConditionFlags}");
        Console.WriteLine($"  Timestamp = {msg.Timestamp}");
        Console.WriteLine($"  Symbol = {msg.Symbol}");
        Console.WriteLine($"  Size = {msg.Size}");
        Console.WriteLine($"  Price = {msg.Price}");
        Console.WriteLine($"  TradeId = {msg.TradeId}");
    }
}
