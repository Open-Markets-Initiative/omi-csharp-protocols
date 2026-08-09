namespace Iex.IexEquities.DeepPlus.IexTp.ConsoleDumpManager;

using global::Iex.IexEquities.DeepPlus.IexTp;
using System.Runtime.CompilerServices;

/// <summary>
///  Implements &lt;c&gt;OnOrderExecutedMessage&lt;/c&gt; for console dumping.
/// </summary>
public sealed partial class ConsoleDumpManager
{
    /// <summary>
    ///  Dumps every field of a &lt;c&gt;OrderExecutedMessage&lt;/c&gt; to the console.
    /// </summary>
    unsafe partial void OnOrderExecutedMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq)
    {
        if (payloadLength < Unsafe.SizeOf<OrderExecutedMessage.Layout>())
        {
            Console.WriteLine($"[seq={seq}] OrderExecutedMessage: short payload ({payloadLength} < {Unsafe.SizeOf<OrderExecutedMessage.Layout>()} bytes) — skipped");
            return;
        }
        ref readonly var msg = ref *(OrderExecutedMessage.Layout*)payload;

        Console.WriteLine($"[seq={seq}] OrderExecutedMessage");
        Console.WriteLine($"  SaleConditionFlags = {msg.SaleConditionFlags}");
        Console.WriteLine($"  Timestamp = {msg.Timestamp}");
        Console.WriteLine($"  Symbol = {msg.Symbol}");
        Console.WriteLine($"  OrderIdReference = {msg.OrderIdReference}");
        Console.WriteLine($"  Size = {msg.Size}");
        Console.WriteLine($"  Price = {msg.Price}");
        Console.WriteLine($"  TradeId = {msg.TradeId}");
    }
}
