namespace Iex.IexEquities.Deep.IexTp.ConsoleDumpManager;

using global::Iex.IexEquities.Deep.IexTp;
using System.Runtime.CompilerServices;

/// <summary>
///  Implements &lt;c&gt;OnPriceLevelBuyUpdateMessage&lt;/c&gt; for console dumping.
/// </summary>
public sealed partial class ConsoleDumpManager
{
    /// <summary>
    ///  Dumps every field of a &lt;c&gt;PriceLevelBuyUpdateMessage&lt;/c&gt; to the console.
    /// </summary>
    unsafe partial void OnPriceLevelBuyUpdateMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq)
    {
        if (payloadLength < Unsafe.SizeOf<PriceLevelBuyUpdateMessage.Layout>())
        {
            Console.WriteLine($"[seq={seq}] PriceLevelBuyUpdateMessage: short payload ({payloadLength} < {Unsafe.SizeOf<PriceLevelBuyUpdateMessage.Layout>()} bytes) — skipped");
            return;
        }
        ref readonly var msg = ref *(PriceLevelBuyUpdateMessage.Layout*)payload;

        Console.WriteLine($"[seq={seq}] PriceLevelBuyUpdateMessage");
        Console.WriteLine($"  EventFlags = {msg.EventFlags}");
        Console.WriteLine($"  Timestamp = {msg.Timestamp}");
        Console.WriteLine($"  Symbol = {msg.Symbol}");
        Console.WriteLine($"  Size = {msg.Size}");
        Console.WriteLine($"  Price = {msg.Price}");
    }
}
