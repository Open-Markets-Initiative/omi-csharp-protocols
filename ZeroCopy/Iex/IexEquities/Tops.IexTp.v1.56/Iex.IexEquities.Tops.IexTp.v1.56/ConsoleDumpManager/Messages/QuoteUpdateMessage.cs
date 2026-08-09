namespace Iex.IexEquities.Tops.IexTp.ConsoleDumpManager;

using global::Iex.IexEquities.Tops.IexTp;
using System.Runtime.CompilerServices;

/// <summary>
///  Implements &lt;c&gt;OnQuoteUpdateMessage&lt;/c&gt; for console dumping.
/// </summary>
public sealed partial class ConsoleDumpManager
{
    /// <summary>
    ///  Dumps every field of a &lt;c&gt;QuoteUpdateMessage&lt;/c&gt; to the console.
    /// </summary>
    unsafe partial void OnQuoteUpdateMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq)
    {
        if (payloadLength < Unsafe.SizeOf<QuoteUpdateMessage.Layout>())
        {
            Console.WriteLine($"[seq={seq}] QuoteUpdateMessage: short payload ({payloadLength} < {Unsafe.SizeOf<QuoteUpdateMessage.Layout>()} bytes) — skipped");
            return;
        }
        ref readonly var msg = ref *(QuoteUpdateMessage.Layout*)payload;

        Console.WriteLine($"[seq={seq}] QuoteUpdateMessage");
        Console.WriteLine($"  QuoteUpdateFlags = {msg.QuoteUpdateFlags}");
        Console.WriteLine($"  Timestamp = {msg.Timestamp}");
        Console.WriteLine($"  Symbol = {msg.Symbol}");
        Console.WriteLine($"  BidSize = {msg.BidSize}");
        Console.WriteLine($"  BidPrice = {msg.BidPrice}");
        Console.WriteLine($"  AskPrice = {msg.AskPrice}");
        Console.WriteLine($"  AskSize = {msg.AskSize}");
    }
}
