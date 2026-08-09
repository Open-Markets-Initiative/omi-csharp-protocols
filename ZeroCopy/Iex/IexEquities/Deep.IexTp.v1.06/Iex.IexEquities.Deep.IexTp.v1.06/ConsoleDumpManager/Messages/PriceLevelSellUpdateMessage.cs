namespace Iex.IexEquities.Deep.IexTp.ConsoleDumpManager;

using global::Iex.IexEquities.Deep.IexTp;
using System.Runtime.CompilerServices;

/// <summary>
///  Implements &lt;c&gt;OnPriceLevelSellUpdateMessage&lt;/c&gt; for console dumping.
/// </summary>
public sealed partial class ConsoleDumpManager
{
    /// <summary>
    ///  Dumps every field of a &lt;c&gt;PriceLevelSellUpdateMessage&lt;/c&gt; to the console.
    /// </summary>
    unsafe partial void OnPriceLevelSellUpdateMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq)
    {
        if (payloadLength < Unsafe.SizeOf<PriceLevelSellUpdateMessage.Layout>())
        {
            Console.WriteLine($"[seq={seq}] PriceLevelSellUpdateMessage: short payload ({payloadLength} < {Unsafe.SizeOf<PriceLevelSellUpdateMessage.Layout>()} bytes) — skipped");
            return;
        }
        ref readonly var msg = ref *(PriceLevelSellUpdateMessage.Layout*)payload;

        Console.WriteLine($"[seq={seq}] PriceLevelSellUpdateMessage");
        Console.WriteLine($"  EventFlags = {msg.EventFlags}");
        Console.WriteLine($"  Timestamp = {msg.Timestamp}");
        Console.WriteLine($"  Symbol = {msg.Symbol}");
        Console.WriteLine($"  Size = {msg.Size}");
        Console.WriteLine($"  Price = {msg.Price}");
    }
}
